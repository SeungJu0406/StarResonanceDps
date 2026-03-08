using System.Windows;
using System.Windows.Input;
using StarResonanceDpsAnalysis.WPF.ViewModels;

namespace StarResonanceDpsAnalysis.WPF.Views;

public partial class SkillTrackerView : Window
{
    public SkillTrackerView(SkillTrackerViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }

    private void Header_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ButtonState == MouseButtonState.Pressed)
            DragMove();
    }
}
