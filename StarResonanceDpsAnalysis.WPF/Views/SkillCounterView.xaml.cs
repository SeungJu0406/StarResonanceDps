using System.Windows;
using System.Windows.Input;
using StarResonanceDpsAnalysis.WPF.ViewModels;

namespace StarResonanceDpsAnalysis.WPF.Views;

public partial class SkillCounterView : Window
{
    private readonly SkillCounterViewModel _viewModel;
    private SkillCounterEditorView? _editorView;

    public SkillCounterView(SkillCounterViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        _viewModel = viewModel;

        // 저장된 위치로 복원
        Left = viewModel.ConfigWindowLeft;
        Top = viewModel.ConfigWindowTop;
    }

    private void DragBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ButtonState == MouseButtonState.Pressed)
            DragMove();
    }

    private void OpenEditor_Click(object sender, RoutedEventArgs e)
    {
        if (_editorView == null || !_editorView.IsVisible)
        {
            _editorView = new SkillCounterEditorView(_viewModel);
            _editorView.Show();
        }
        else
        {
            _editorView.Activate();
        }
    }

    protected override void OnLocationChanged(EventArgs e)
    {
        base.OnLocationChanged(e);
        // 위치 변경 시 저장 (디바운스 없이 간단하게)
        _ = _viewModel.SaveWindowPosition(Left, Top);
    }
}
