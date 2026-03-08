using System.Collections.ObjectModel;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using StarResonanceDpsAnalysis.Core.Data;
using StarResonanceDpsAnalysis.Core.Data.Models;
using StarResonanceDpsAnalysis.WPF.Localization;
using StarResonanceDpsAnalysis.WPF.SkillCounter;

namespace StarResonanceDpsAnalysis.WPF.ViewModels;

public partial class SkillCounterViewModel : ObservableObject
{
    private readonly IDataStorage _dataStorage;
    private readonly SkillCounterConfigService _configService;
    private readonly LocalizationManager _localizationManager;
    private readonly ILogger<SkillCounterViewModel> _logger;
    private readonly Dispatcher _dispatcher;

    private SkillCounterConfig _config;

    public ObservableCollection<SkillAuraState> Auras { get; } = new();

    // 에디터용 목록
    public ObservableCollection<SkillAuraEditItem> EditItems { get; } = new();

    [ObservableProperty] private SkillAuraEditItem? _selectedEditItem;

    public SkillCounterViewModel(
        IDataStorage dataStorage,
        SkillCounterConfigService configService,
        LocalizationManager localizationManager,
        ILogger<SkillCounterViewModel> logger,
        Dispatcher dispatcher)
    {
        _dataStorage = dataStorage;
        _configService = configService;
        _localizationManager = localizationManager;
        _logger = logger;
        _dispatcher = dispatcher;

        _config = configService.Load();
        RebuildAuras();

        _dataStorage.BattleLogCreated += OnBattleLogCreated;
    }

    private void OnBattleLogCreated(BattleLog log)
    {
        var myUid = _dataStorage.CurrentPlayerInfo?.UID;
        if (myUid == null || myUid == 0 || log.AttackerUuid != myUid)
            return;

        _dispatcher.InvokeAsync(() =>
        {
            foreach (var aura in Auras)
            {
                if (!aura.Config.IsEnabled) continue;

                // 리셋 스킬 먼저 확인
                if (aura.Config.ResetSkillId != 0 && log.SkillID == aura.Config.ResetSkillId)
                {
                    aura.Reset();
                    continue;
                }

                // 트리거 스킬 카운트
                if (aura.Config.TriggerSkillId != 0 && log.SkillID == aura.Config.TriggerSkillId)
                {
                    aura.Increment();
                }
            }
        });
    }

    private void RebuildAuras()
    {
        Auras.Clear();
        foreach (var aura in _config.Auras)
            Auras.Add(new SkillAuraState(aura));
    }

    // ──────────────────── 오버레이 커맨드 ────────────────────

    [RelayCommand]
    private void ResetAll()
    {
        foreach (var aura in Auras)
            aura.Reset();
    }

    // ──────────────────── 에디터 커맨드 ────────────────────

    /// <summary>에디터 열 때 EditItems 동기화</summary>
    public void LoadEditItems()
    {
        EditItems.Clear();
        foreach (var aura in _config.Auras)
        {
            var item = new SkillAuraEditItem(aura);
            RefreshSkillNames(item);
            EditItems.Add(item);
        }
        SelectedEditItem = EditItems.FirstOrDefault();
    }

    [RelayCommand]
    private void AddAura()
    {
        var newAura = new SkillCounterAura { Name = $"카운터 {EditItems.Count + 1}" };
        var item = new SkillAuraEditItem(newAura);
        EditItems.Add(item);
        SelectedEditItem = item;
    }

    [RelayCommand]
    private void DeleteAura(SkillAuraEditItem? item)
    {
        if (item == null) return;
        var idx = EditItems.IndexOf(item);
        EditItems.Remove(item);
        SelectedEditItem = EditItems.Count > 0
            ? EditItems[Math.Max(0, idx - 1)]
            : null;
    }

    [RelayCommand]
    private void LookupTriggerSkill()
    {
        if (SelectedEditItem == null) return;
        RefreshSkillNames(SelectedEditItem);
    }

    [RelayCommand]
    private void LookupResetSkill()
    {
        if (SelectedEditItem == null) return;
        RefreshSkillNames(SelectedEditItem);
    }

    private void RefreshSkillNames(SkillAuraEditItem item)
    {
        if (long.TryParse(item.TriggerSkillIdText, out var tid) && tid > 0)
            item.TriggerSkillName = GetSkillName(tid);
        else
            item.TriggerSkillName = string.Empty;

        if (long.TryParse(item.ResetSkillIdText, out var rid) && rid > 0)
            item.ResetSkillName = GetSkillName(rid);
        else
            item.ResetSkillName = string.Empty;
    }

    private string GetSkillName(long skillId)
    {
        var name = _localizationManager.GetString($"JsonDictionary:Skills:{skillId}");
        return string.IsNullOrEmpty(name) || name == skillId.ToString()
            ? $"알 수 없음 ({skillId})"
            : name;
    }

    /// <summary>에디터에서 저장 버튼 클릭 시</summary>
    [RelayCommand]
    private async Task SaveConfig()
    {
        _config.Auras = EditItems.Select(i => i.ToAura()).ToList();
        await _configService.SaveAsync(_config);
        RebuildAuras();
    }

    public double ConfigWindowLeft
    {
        get => _config.WindowLeft;
        set => _config.WindowLeft = value;
    }

    public double ConfigWindowTop
    {
        get => _config.WindowTop;
        set => _config.WindowTop = value;
    }

    public async Task SaveWindowPosition(double left, double top)
    {
        _config.WindowLeft = left;
        _config.WindowTop = top;
        await _configService.SaveAsync(_config);
    }
}
