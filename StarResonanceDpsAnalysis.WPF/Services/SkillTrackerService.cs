using CommunityToolkit.Mvvm.ComponentModel;
using StarResonanceDpsAnalysis.Core.Data;
using StarResonanceDpsAnalysis.Core.Data.Models;
using StarResonanceDpsAnalysis.WPF.Config;
using StarResonanceDpsAnalysis.WPF.Localization;
using StarResonanceDpsAnalysis.WPF.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace StarResonanceDpsAnalysis.WPF.Services;

public partial class SkillTrackerService : ObservableObject, ISkillTrackerService, IDisposable
{
    private readonly IDataStorage _dataStorage;
    private readonly IConfigManager _configManager;
    private bool _disposed;

    [ObservableProperty]
    private bool _isWaiting;

    public ObservableCollection<SkillTrackerItem> TrackedSkills { get; } = new();

    public SkillTrackerService(IDataStorage dataStorage, IConfigManager configManager)
    {
        _dataStorage = dataStorage;
        _configManager = configManager;
        _dataStorage.BattleLogCreated += OnBattleLogCreated;
    }

    public void StartWaiting()
    {
        IsWaiting = true;
    }

    public void CancelWaiting()
    {
        IsWaiting = false;
    }

    public void RemoveSkill(long skillId)
    {
        Application.Current?.Dispatcher.Invoke(() =>
        {
            var item = TrackedSkills.FirstOrDefault(s => s.SkillId == skillId);
            if (item != null)
                TrackedSkills.Remove(item);
        });
    }

    public void Clear()
    {
        Application.Current?.Dispatcher.Invoke(() =>
        {
            TrackedSkills.Clear();
            IsWaiting = false;
        });
    }

    private void OnBattleLogCreated(BattleLog battleLog)
    {
        // 현재 플레이어 스킬만 처리
        var currentPlayerUid = _configManager.CurrentConfig.Uid;
        if (currentPlayerUid == 0 || battleLog.AttackerUuid != currentPlayerUid)
            return;

        // 미스나 힐은 제외
        if (battleLog.IsMiss)
            return;

        var skillId = battleLog.SkillID;
        var skillName = LocalizationManager.Instance.GetString($"JsonDictionary:Skills:{(int)skillId}");
        if (string.IsNullOrEmpty(skillName) || skillName == skillId.ToString())
            skillName = $"Unknown ({skillId})";

        Application.Current?.Dispatcher.Invoke(() =>
        {
            if (IsWaiting)
            {
                // 아직 등록 안 된 스킬이면 등록
                var existing = TrackedSkills.FirstOrDefault(s => s.SkillId == skillId);
                if (existing == null)
                {
                    TrackedSkills.Add(new SkillTrackerItem
                    {
                        SkillId = skillId,
                        SkillName = skillName,
                        Count = 1,
                        TotalDamage = battleLog.Value
                    });
                }
                else
                {
                    existing.Count++;
                    existing.TotalDamage += battleLog.Value;
                }
                IsWaiting = false;
                return;
            }

            // 이미 추적 중인 스킬이면 카운트 증가
            var tracked = TrackedSkills.FirstOrDefault(s => s.SkillId == skillId);
            if (tracked != null)
            {
                tracked.Count++;
                tracked.TotalDamage += battleLog.Value;
            }
        });
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        _dataStorage.BattleLogCreated -= OnBattleLogCreated;
    }
}
