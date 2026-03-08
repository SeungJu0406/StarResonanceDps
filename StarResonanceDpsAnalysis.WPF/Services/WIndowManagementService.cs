using Microsoft.Extensions.DependencyInjection;
using StarResonanceDpsAnalysis.WPF.Views;
using System.Windows;
using Microsoft.Extensions.Logging;
using StarResonanceDpsAnalysis.WPF.Logging;

namespace StarResonanceDpsAnalysis.WPF.Services;

public class WindowManagementService(IServiceProvider provider, ILogger<WindowManagementService> logger) : IWindowManagementService
{
    private AboutView? _aboutView;
    private BossTrackerView? _bossTrackerView;
    private DamageReferenceView? _damageReferenceView;
    private ModuleSolveView? _moduleSolveView;
    private SettingsView? _settingsView;
    private SkillBreakdownView? _skillBreakDownView;
    private SkillLogView? _skillLogView;
    private SkillTrackerView? _skillTrackerView;

    public AboutView AboutView => _aboutView ??= CreateAboutView();
    public BossTrackerView BossTrackerView => _bossTrackerView ??= CreateBossTrackerView();
    public DamageReferenceView DamageReferenceView => _damageReferenceView ??= CreateDamageReferenceView();
    public MainView MainView => provider.GetRequiredService<MainView>();
    public ModuleSolveView ModuleSolveView => _moduleSolveView ??= CreateModuleSolveView();
    public SettingsView SettingsView => _settingsView ??= CreateSettingsView();
    public SkillBreakdownView SkillBreakdownView => _skillBreakDownView ??= CreateSkillBreakDownView();
    public SkillLogView SkillLogView => _skillLogView ??= CreateSkillLogView();
    public SkillTrackerView SkillTrackerView => _skillTrackerView ??= CreateSkillTrackerView();

    private static void ConfigureOwnedToolWindow(Window view)
    {
        if (Application.Current?.MainWindow is { } main && view.Owner == null && view != main)
        {
            view.Owner = main;
            view.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }
    }

    private AboutView CreateAboutView()
    {
        var view = provider.GetRequiredService<AboutView>();
        ConfigureOwnedToolWindow(view);
        logger.LogDebug(WpfLogEvents.WindowCreated, "Window created: {Window}", nameof(AboutView));
        view.Closed += (_, _) =>
        {
            if (_aboutView == view) _aboutView = null;
            logger.LogDebug(WpfLogEvents.WindowClosed, "Window closed: {Window}", nameof(AboutView));
        };
        return view;
    }

    private BossTrackerView CreateBossTrackerView()
    {
        var view = provider.GetRequiredService<BossTrackerView>();
        ConfigureOwnedToolWindow(view);
        logger.LogDebug(WpfLogEvents.WindowCreated, "Window created: {Window}", nameof(BossTrackerView));
        view.Closed += (_, _) =>
        {
            if (_bossTrackerView == view) _bossTrackerView = null;
            logger.LogDebug(WpfLogEvents.WindowClosed, "Window closed: {Window}", nameof(BossTrackerView));
        };
        return view;
    }

    private DamageReferenceView CreateDamageReferenceView()
    {
        var view = provider.GetRequiredService<DamageReferenceView>();
        ConfigureOwnedToolWindow(view);
        logger.LogDebug(WpfLogEvents.WindowCreated, "Window created: {Window}", nameof(DamageReferenceView));
        view.Closed += (_, _) =>
        {
            if (_damageReferenceView == view) _damageReferenceView = null;
            logger.LogDebug(WpfLogEvents.WindowClosed, "Window closed: {Window}", nameof(DamageReferenceView));
        };
        return view;
    }

    private ModuleSolveView CreateModuleSolveView()
    {
        var view = provider.GetRequiredService<ModuleSolveView>();
        ConfigureOwnedToolWindow(view);
        logger.LogDebug(WpfLogEvents.WindowCreated, "Window created: {Window}", nameof(ModuleSolveView));
        view.Closed += (_, _) =>
        {
            if (_moduleSolveView == view) _moduleSolveView = null;
            logger.LogDebug(WpfLogEvents.WindowClosed, "Window closed: {Window}", nameof(ModuleSolveView));
        };
        return view;
    }

    private SettingsView CreateSettingsView()
    {
        var view = provider.GetRequiredService<SettingsView>();
        ConfigureOwnedToolWindow(view);
        logger.LogDebug(WpfLogEvents.WindowCreated, "Window created: {Window}", nameof(SettingsView));
        view.Closed += (_, _) =>
        {
            if (_settingsView == view) _settingsView = null;
            logger.LogDebug(WpfLogEvents.WindowClosed, "Window closed: {Window}", nameof(SettingsView));
        };
        return view;
    }

    private SkillBreakdownView CreateSkillBreakDownView()
    {
        var view = provider.GetRequiredService<SkillBreakdownView>();
        ConfigureOwnedToolWindow(view);
        logger.LogDebug(WpfLogEvents.WindowCreated, "Window created: {Window}", nameof(SkillBreakdownView));
        view.Closed += (_, _) =>
        {
            if (_skillBreakDownView == view) _skillBreakDownView = null;
            logger.LogDebug(WpfLogEvents.WindowClosed, "Window closed: {Window}", nameof(SkillBreakdownView));
        };
        return view;
    }

    private SkillLogView CreateSkillLogView()
    {
        var view = provider.GetRequiredService<SkillLogView>();
        ConfigureOwnedToolWindow(view);
        logger.LogDebug(WpfLogEvents.WindowCreated, "Window created: {Window}", nameof(SkillLogView));
        view.Closed += (_, _) =>
        {
            if (_skillLogView == view) _skillLogView = null;
            logger.LogDebug(WpfLogEvents.WindowClosed, "Window closed: {Window}", nameof(SkillLogView));
        };
        return view;
    }

    private SkillTrackerView CreateSkillTrackerView()
    {
        var view = provider.GetRequiredService<SkillTrackerView>();
        ConfigureOwnedToolWindow(view);
        logger.LogDebug(WpfLogEvents.WindowCreated, "Window created: {Window}", nameof(SkillTrackerView));
        view.Closed += (_, _) =>
        {
            if (_skillTrackerView == view) _skillTrackerView = null;
            logger.LogDebug(WpfLogEvents.WindowClosed, "Window closed: {Window}", nameof(SkillTrackerView));
        };
        return view;
    }
}

public static class WindowManagementServiceExtensions
{
    public static IServiceCollection AddWindowManagementService(this IServiceCollection services)
    {
        services.AddSingleton<IWindowManagementService, WindowManagementService>();
        return services;
    }
}
