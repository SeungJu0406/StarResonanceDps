using CommunityToolkit.Mvvm.ComponentModel;

namespace StarResonanceDpsAnalysis.WPF.Models;

public partial class SkillTrackerItem : ObservableObject
{
    public long SkillId { get; set; }

    [ObservableProperty]
    private string _skillName = string.Empty;

    [ObservableProperty]
    private int _count;

    [ObservableProperty]
    private long _totalDamage;
}
