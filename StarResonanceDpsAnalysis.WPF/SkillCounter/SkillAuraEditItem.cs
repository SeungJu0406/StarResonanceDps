using CommunityToolkit.Mvvm.ComponentModel;

namespace StarResonanceDpsAnalysis.WPF.SkillCounter;

/// <summary>
/// 에디터용 오라 편집 아이템 (Observable 래퍼)
/// </summary>
public partial class SkillAuraEditItem : ObservableObject
{
    public Guid Id { get; }

    [ObservableProperty] private string _name;
    [ObservableProperty] private string _triggerSkillIdText;
    [ObservableProperty] private string _thresholdText;
    [ObservableProperty] private string _resetSkillIdText;
    [ObservableProperty] private string _iconText;
    [ObservableProperty] private string _iconColor;
    [ObservableProperty] private bool _isEnabled;

    [ObservableProperty] private string _triggerSkillName = string.Empty;
    [ObservableProperty] private string _resetSkillName = string.Empty;

    public SkillAuraEditItem(SkillCounterAura source)
    {
        Id = source.Id;
        _name = source.Name;
        _triggerSkillIdText = source.TriggerSkillId == 0 ? string.Empty : source.TriggerSkillId.ToString();
        _thresholdText = source.Threshold.ToString();
        _resetSkillIdText = source.ResetSkillId == 0 ? string.Empty : source.ResetSkillId.ToString();
        _iconText = source.IconText;
        _iconColor = source.IconColor;
        _isEnabled = source.IsEnabled;
    }

    public SkillCounterAura ToAura()
    {
        long.TryParse(TriggerSkillIdText, out var triggerId);
        int.TryParse(ThresholdText, out var threshold);
        long.TryParse(ResetSkillIdText, out var resetId);

        return new SkillCounterAura
        {
            Id = Id,
            Name = Name,
            TriggerSkillId = triggerId,
            Threshold = threshold <= 0 ? 1 : threshold,
            ResetSkillId = resetId,
            IconText = string.IsNullOrWhiteSpace(IconText) ? "?" : IconText,
            IconColor = IconColor,
            IsEnabled = IsEnabled
        };
    }
}
