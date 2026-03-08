using System.Windows;
using System.Windows.Input;
using StarResonanceDpsAnalysis.WPF.ViewModels;

namespace StarResonanceDpsAnalysis.WPF.Views;

public partial class SkillCounterEditorView : Window
{
    private readonly SkillCounterViewModel _viewModel;

    public SkillCounterEditorView(SkillCounterViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        _viewModel = viewModel;

        // 에디터 열릴 때 EditItems 동기화
        _viewModel.LoadEditItems();
    }

    private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ButtonState == MouseButtonState.Pressed)
            DragMove();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void LookupTriggerSkill_Click(object sender, RoutedEventArgs e)
    {
        _viewModel.LookupTriggerSkillCommand.Execute(null);
    }

    private void LookupResetSkill_Click(object sender, RoutedEventArgs e)
    {
        _viewModel.LookupResetSkillCommand.Execute(null);
    }
}
