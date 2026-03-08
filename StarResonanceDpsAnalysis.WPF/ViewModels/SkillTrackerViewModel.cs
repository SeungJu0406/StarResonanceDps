using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StarResonanceDpsAnalysis.WPF.Models;
using StarResonanceDpsAnalysis.WPF.Services;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;

namespace StarResonanceDpsAnalysis.WPF.ViewModels;

public partial class SkillTrackerViewModel : ObservableObject
{
    private readonly ISkillTrackerService _skillTrackerService;

    public ObservableCollection<SkillTrackerItem> TrackedSkills => _skillTrackerService.TrackedSkills;

    public bool IsWaiting => _skillTrackerService.IsWaiting;

    public bool IsEmpty => _skillTrackerService.TrackedSkills.Count == 0;

    public SkillTrackerViewModel(ISkillTrackerService skillTrackerService)
    {
        _skillTrackerService = skillTrackerService;
        if (_skillTrackerService is INotifyPropertyChanged notifiable)
        {
            notifiable.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == nameof(ISkillTrackerService.IsWaiting))
                    OnPropertyChanged(nameof(IsWaiting));
            };
        }
        _skillTrackerService.TrackedSkills.CollectionChanged += (_, _) =>
            OnPropertyChanged(nameof(IsEmpty));
    }

    [RelayCommand]
    private void StartTracking()
    {
        if (_skillTrackerService.IsWaiting)
            _skillTrackerService.CancelWaiting();
        else
            _skillTrackerService.StartWaiting();
    }

    [RelayCommand]
    private void RemoveSkill(SkillTrackerItem item)
    {
        _skillTrackerService.RemoveSkill(item.SkillId);
    }

    [RelayCommand]
    private void Clear()
    {
        _skillTrackerService.Clear();
    }

    [RelayCommand]
    private void Close()
    {
        var window = Application.Current?.Windows.OfType<Views.SkillTrackerView>().FirstOrDefault();
        window?.Close();
    }
}
