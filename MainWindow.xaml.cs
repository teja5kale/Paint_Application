using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paint
{
    public partial class MainWindow : Window
    {
        private bool isDrawingComplete = false; 
        private Point currentPoint = new Point(); 

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                currentPoint = e.GetPosition(paintCanvas);
                isDrawingComplete = false;
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && GetSelectedDrawStyle() == "FreeDraw")
            {
                Line line = CreateLine(currentPoint, e.GetPosition(paintCanvas));
                paintCanvas.Children.Add(line);
                currentPoint = e.GetPosition(paintCanvas); 
            }
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!isDrawingComplete && GetSelectedDrawStyle() == "StraightLine")
            {
                Line line = CreateLine(currentPoint, e.GetPosition(paintCanvas));
                paintCanvas.Children.Add(line);
                isDrawingComplete = true; 
            }
        }

        private string GetSelectedDrawStyle()
        {
            return ((ComboBoxItem)drawStyle.SelectedItem)?.Name ?? "FreeDraw";
        }

        private Line CreateLine(Point start, Point end)
        {
            return new Line
            {
                X1 = start.X,
                Y1 = start.Y,
                X2 = end.X,
                Y2 = end.Y,
                Stroke = GetSelectedBrush(),
                StrokeThickness = GetSelectedThickness()
            };
        }

        private Brush GetSelectedBrush()
        {
            string colorName = ((ComboBoxItem)colorSelector.SelectedItem).Name;
            return colorName switch
            {
                "red" => Brushes.Red,
                "green" => Brushes.Green,
                "blue" => Brushes.Blue,
                "yellow" => Brushes.Yellow,
                "black"=> Brushes.Black,
            };
        }

        private double GetSelectedThickness()
        {
            string thicknessName = ((ComboBoxItem)thicknessSelector.SelectedItem).Name;
            return thicknessName switch
            {
                "one" => 2,
                "two" => 4,
                "three" => 6,
                "four" => 8,
                "five" => 10,

            };
        }

        private void ClearCanvas(object sender, RoutedEventArgs e)
        {
            paintCanvas.Children.Clear();
        }
    }
}
