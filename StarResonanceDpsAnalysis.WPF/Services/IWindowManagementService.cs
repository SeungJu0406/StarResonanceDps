using StarResonanceDpsAnalysis.WPF.Views;

namespace StarResonanceDpsAnalysis.WPF.Services;

public interface IWindowManagementService
{
    SettingsView SettingsView { get; }
    SkillBreakdownView SkillBreakdownView { get; }
    AboutView AboutView { get; }
    DamageReferenceView DamageReferenceView { get; }
    ModuleSolveView ModuleSolveView { get; }
    BossTrackerView BossTrackerView { get; }
    MainView MainView { get; }
    SkillLogView SkillLogView { get; }
    SkillTrackerView SkillTrackerView { get; }
}