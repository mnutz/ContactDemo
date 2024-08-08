namespace MNutz.ContactDemo.Ui.Views;

using System.Windows;
using System.Windows.Controls.Primitives;

/// <summary>
/// Interaction logic for MainView.xaml.
/// </summary>
public partial class MainView
{
    private const int ContactsCardMinWidth = 250;

    /// <summary>
    /// Initializes the component.
    /// </summary>
    public MainView() => InitializeComponent();

    private void ContactsGrid_OnSizeChanged(object contactsGrid, SizeChangedEventArgs e) =>
        SetGridColumns(contactsGrid as UniformGrid, ContactsCardMinWidth);

    private void ContactsGrid_OnLoaded(object contactsGrid, RoutedEventArgs e) =>
        SetGridColumns(contactsGrid as UniformGrid, ContactsCardMinWidth);

    private static void SetGridColumns(UniformGrid? grid, int minWidthPerColumn)
    {
        if (grid is null)
        {
            return;
        }

        var columns = Math.Max(1, (int)(grid.ActualWidth / minWidthPerColumn));

        if (columns != grid.Columns)
        {
            grid.Columns = columns;
        }
    }
}
