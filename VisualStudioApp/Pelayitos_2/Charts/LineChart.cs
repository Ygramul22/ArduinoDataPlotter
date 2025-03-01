using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestForCansat.Charts
{
    public class LineChart
    {
        public TextBox Console;

        private Line xAxisLine, yAxisLine;
        private double xAxisStart = 100, yAxisStart = 100, interval = 100;
        private Polyline chartPolyline;

        private Point origin;
        public List<Holder> holders;
        public List<Value> values;

        public LineChart()
        {
            holders = new List<Holder>();
            values = new List<Value>()
            {
                //new Value(0,0),
                //new Value(100,100),
                //new Value(200,200),
                //new Value(300,300),
                //new Value(400,200),
                //new Value(500,500),
                //new Value(600,500),
                //new Value(700,500),
                //new Value(800,500),
                //new Value(900,600),
                //new Value(1000,200),
                //new Value(1100,100),
                //new Value(1200,400),

                //new Value(0,0),
                //new Value(100,200),
                //new Value(200,100),
                //new Value(300,200),
                //new Value(400,300),
                //new Value(500,400),
                //new Value(600,500),
                //new Value(700,400),
                //new Value(800,500),
                //new Value(900,600),
                //new Value(1000,300),
                //new Value(1100,100),
                //new Value(1200,400),

                new Value(0,0),
                new Value(100,100),
                new Value(200,400),
                new Value(300,200),
                new Value(400,400),
                new Value(500,300),
                new Value(600,100),
                new Value(700,700),
                new Value(800,200),
                new Value(900,600),
                new Value(1000,600),
                new Value(1100,0),
                new Value(1200,100),
                new Value(1300,100),
            };
        }

        public void Paint(Canvas chartCanvas)
        {
            double _startValue = values[0].X;
            double _startYValue = GetMinValue_Y();
            //Getting the width, height, and margin
            float width = (float)chartCanvas.Width, height = (float)chartCanvas.Height;
            float _marginPercentage = 0.1f;
            xAxisStart = width * _marginPercentage;
            yAxisStart = height * _marginPercentage;

            //Getting the interval between points, and the amount of vertical lines, and the ratio
            int verticalLinesCount = values.Count;
            float _verticalLinesRatio = MakeTheThreeRule((GetMaxValue_X() - (float)_startValue), (float)(width - (xAxisStart * 2)), 1);

            //Getting all values to setup x axis lines
            int _horizontalLinesCount = 15;
            float _totalVerticalSpace = height - (float)(yAxisStart * 2);
            float _verticalinterval = (GetMaxValue_Y() - GetMinValue_Y()) / _horizontalLinesCount;
            float _horizontalLinesSpacing = _totalVerticalSpace / _horizontalLinesCount;
            float _horizontalLinesRatio = MakeTheThreeRule(GetMaxValue_Y() - GetMinValue_Y(), _totalVerticalSpace, 1);

            try
            {
                if (width > 0 && height > 0)
                {
                    //Cleaning all values
                    chartCanvas.Children.Clear();
                    holders.Clear();

                    //Instantiating the main axis lines
                    xAxisLine = new Line()
                    {
                        X1 = xAxisStart,
                        Y1 = height - yAxisStart,
                        X2 = width - xAxisStart,
                        Y2 = height - yAxisStart,
                        Stroke = Brushes.LightGray,
                        StrokeThickness = 1,
                    };
                    yAxisLine = new Line()
                    {
                        X1 = xAxisStart,
                        Y1 = yAxisStart - 50,
                        X2 = xAxisStart,
                        Y2 = height - yAxisStart,
                        Stroke = Brushes.LightGray,
                        StrokeThickness = 1,
                    };
                    //Rendering the lines
                    chartCanvas.Children.Add(xAxisLine);
                    chartCanvas.Children.Add(yAxisLine);

                    //Establising the origin
                    origin = new Point(xAxisLine.X1, yAxisLine.Y2);

                    //Creating the first text block
                    var xTextBlock0 = new TextBlock() { Text = $"{_startValue}" };
                    //Rendering the text
                    chartCanvas.Children.Add(xTextBlock0);
                    //Positioning the text
                    Canvas.SetLeft(xTextBlock0, origin.X);
                    Canvas.SetTop(xTextBlock0, origin.Y + 5);

                    //Repeating the instantiation code the specified amount of lines
                    for (int i=1; i<verticalLinesCount; i++)
                    {
                        //Instantiating the lines
                        var line = new Line()
                        {
                            //Getting star and end positions
                            X1 = (_verticalLinesRatio * (values[i].X - _startValue)) + xAxisStart,
                            Y1 = yAxisStart - 50,
                            X2 = (_verticalLinesRatio * (values[i].X - _startValue)) + xAxisStart,
                            Y2 = height - yAxisStart,
                            //Setting visual values
                            Stroke = Brushes.LightGray,
                            StrokeThickness = 1,
                            Opacity = 1,
                        };
                        //Rendering the line
                        chartCanvas.Children.Add(line);

                        //Instantiating the text block
                        var textBlock = new TextBlock { Text = $"{values[i].X}", };

                        //Rendering and positioning the text block
                        chartCanvas.Children.Add(textBlock);
                        Canvas.SetLeft(textBlock, ((_verticalLinesRatio * (values[i].X - _startValue)) + xAxisStart));
                        Canvas.SetTop(textBlock, line.Y2 + 5);
                    }

                    //Instantiating the first vertical textblock
                    var yTextBlock0 = new TextBlock() { Text = $"{_startYValue}" };
                    //measure the textbox
                    yTextBlock0.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                    Size _textBlockSize0 = yTextBlock0.DesiredSize;
                    chartCanvas.Children.Add(yTextBlock0);
                    Canvas.SetLeft(yTextBlock0, origin.X - _textBlockSize0.Width);
                    Canvas.SetTop(yTextBlock0, origin.Y - 10);

                    // x axis lines
                    var yValue = yAxisStart;
                    double yPoint = origin.Y - interval;
                    //Repeating the creation code once for each line
                    for(int i=_horizontalLinesCount; i>0; i--)
                    {
                        //Instantiating the line
                        var line = new Line()
                        {
                            //Setting the coordinates
                            X1 = xAxisStart,
                            Y1 = height - yAxisStart -(_horizontalLinesSpacing * i),
                            X2 = width - xAxisStart,
                            Y2 = height - yAxisStart - (_horizontalLinesSpacing * i),
                            //Setting visual values
                            Stroke = Brushes.LightGray,
                            StrokeThickness = 1,
                            Opacity = 1,
                        };
                        //Rendering the line
                        chartCanvas.Children.Add(line);

                        //Instantiating the text block
                        int _integerValue = (int)((float)((_verticalinterval * i) + _startYValue) * 100);
                        float _value = (float)_integerValue / 100f;
                        var textBlock = new TextBlock() { Text = $"{_value}" };
                        //measure the textbox
                        textBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                        Size _textBlockSize = textBlock.DesiredSize;
                        //Rendering and positioning the text
                        chartCanvas.Children.Add(textBlock);
                        Canvas.SetLeft(textBlock, line.X1 - _textBlockSize.Width);
                        Canvas.SetTop(textBlock, (height - yAxisStart - (_horizontalLinesSpacing * i)) - (_horizontalLinesSpacing / 3));

                        yPoint -= interval;
                        yValue += interval;
                    }
                    
                    // connections
                    double xPoint = origin.X;
                    yPoint = origin.Y;
                    foreach(Value _point in values)
                    {
                        System.Console.WriteLine($"{(_point.Y - _startYValue)}, original-> {_point.Y}, ");
                        var holder = new Holder()
                        {
                            X = _point.X,
                            Y = _point.Y,

                            Point = new Point(((_point.X - _startValue) * _verticalLinesRatio) + (xAxisStart), (Math.Abs(GetMaxValue_Y() - _point.Y) * _horizontalLinesRatio) + (yAxisStart)),

                            Value = _point,
                        };

                        holders.Add(holder);
                    }

                    // polyline
                    chartPolyline = new Polyline()
                    {
                        Stroke = new SolidColorBrush(Color.FromRgb(68, 114, 196)),
                        StrokeThickness = 1.5f,
                    };
                    chartCanvas.Children.Add(chartPolyline);

                    foreach (var holder in holders)
                    {
                        chartPolyline.Points.Add(holder.Point);
                    }

                    // showing where are the connections points
                    foreach (var holder in holders)
                    {
                        string tooltip = $"X: {holder.Value.X} \n Y: {holder.Value.Y}";

                        Ellipse oEllipse = new Ellipse()
                        {
                            Fill = Brushes.Blue,
                            Width = 20,
                            Height = 20,
                            Opacity = 0,
                            ToolTip = tooltip
                        };

                        chartCanvas.Children.Add(oEllipse);
                        Canvas.SetLeft(oEllipse, holder.Point.X - (oEllipse.Width / 2));
                        Canvas.SetTop(oEllipse, holder.Point.Y - (oEllipse.Height / 2));
                    }

                    // add connection points to polyline
                    /*/int _counter = 1;
                    foreach (var value in values)
                    {
                        var holder = holders.FirstOrDefault(h => h.X == value.X && h.Y == value.Y);
                        if (holder != null && _counter < (holders.Count() - 1))
                            chartPolyline.Points.Add(holder.Point);
                        _counter++;
                    }
                    /*/
                }
            }
            catch (Exception _ex)
            {
                WriteToConsole(_ex);
                throw;
            }
        }

        public float MakeTheThreeRule(float _const1, float _const2, float _variable)
        {
            return (_variable * _const2) / _const1;
        }

        public void WriteToConsole(object _message)
        {
            Console.Text += _message.ToString() + "\n";
        }

        public float GetMaxValue_X()
        {
            float _maxValue = 0;

            foreach (Value _item in values)
            {
                if (_item.X > _maxValue)
                {
                    _maxValue = (float)_item.X;
                }
            }

            return _maxValue;
        }

        public float GetMaxValue_Y()
        {
            float _maxValue = 0;

            foreach (Value _item in values)
            {
                if (_item.Y > _maxValue)
                {
                    _maxValue = (float)_item.Y;
                }
            }

            //Adding a 5%
            _maxValue *= 1.05f;

            return _maxValue;
        }

        public float GetMinValue_Y()
        {
            float _minValue = (float)values[0].Y;

            foreach (Value _item in values)
            {
                if (_item.Y < _minValue)
                {
                    _minValue = (float)_item.Y;
                }
            }

            int _newValue = (int)(_minValue * 100);
            float _returnedValue = (float)_newValue / 100f;

            //Substracting a 5%
            _returnedValue *= 0.95f;

            System.Console.WriteLine($"Min value: {_minValue}, new Value {_newValue}, result {(float)((float)_newValue / 100f)}");

            return _returnedValue;
        }
    }

    public class Holder
    {
        public double X { get; set; }
        public double Y { get; set; }
        public Point Point { get; set; }

        public Value Value;

        public Holder()
        {
        }
    }

    public class Value
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Value(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
