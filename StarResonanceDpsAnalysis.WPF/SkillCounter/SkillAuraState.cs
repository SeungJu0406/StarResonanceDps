using CommunityToolkit.Mvvm.ComponentModel;

namespace StarResonanceDpsAnalysis.WPF.SkillCounter;

/// <summary>
/// 런타임 오라 상태 (UI 바인딩용)
/// </summary>
public partial class SkillAuraState : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsCharged))]
    [NotifyPropertyChangedFor(nameof(Progress))]
    [NotifyPropertyChangedFor(nameof(CountText))]
    private int _currentCount;

    public SkillCounterAura Config { get; }

    public SkillAuraState(SkillCounterAura config)
    {
        Config = config;
    }

    /// <summary>임계값 도달 여부</summary>
    public bool IsCharged => Config.Threshold > 0 && CurrentCount >= Config.Threshold;

    /// <summary>진행률 (0.0 ~ 1.0)</summary>
    public double Progress => Config.Threshold > 0
        ? Math.Min(1.0, (double)CurrentCount / Config.Threshold)
        : 0;

    /// <summary>표시 텍스트 (현재/임계값)</summary>
    public string CountText => $"{CurrentCount}/{Config.Threshold}";

    public void Increment() => CurrentCount++;

    public void Reset() => CurrentCount = 0;
}
