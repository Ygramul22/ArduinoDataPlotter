using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestForCansat;

namespace TestForCansat.Charts
{
    internal class ColumnChart
    {
        public List<Item> Items { get; set; }
        public TextBox Console;

        public ColumnChart()
        {
            Items = new List<Item>()
            {
                // test 01
                //new Item(){Header= "Item1", Value = 266},
                //new Item(){Header= "Item2", Value = 133},
                //new Item(){Header= "Item3", Value = 222},
                //new Item(){Header= "Item4", Value = 366},
                //new Item(){Header= "Item5", Value = 111},
                //new Item(){Header= "Item6", Value = 377},
                //new Item(){Header= "Item7", Value = 444},
                //new Item(){Header= "Item8", Value = 366},
                //new Item(){Header= "Item9", Value = 288},
                //new Item(){Header= "Item10", Value = 455},

                // test 02
                //new Item(){Header= "Item1", Value = 166},
                //new Item(){Header= "Item2", Value = 433},
                //new Item(){Header= "Item3", Value = 322},
                //new Item(){Header= "Item4", Value = 166},
                //new Item(){Header= "Item5", Value = 21},
                //new Item(){Header= "Item6", Value = 277},
                //new Item(){Header= "Item7", Value = 44},
                //new Item(){Header= "Item8", Value = 166},
                //new Item(){Header= "Item9", Value = 288},
                //new Item(){Header= "Item10", Value = 55},

                // test 03
                //new Item(){Header= "Item1", Value = 66},
                //new Item(){Header= "Item2", Value = 300},
                //new Item(){Header= "Item3", Value = 122},
                //new Item(){Header= "Item4", Value = 200},
                //new Item(){Header= "Item5", Value = 411},
                //new Item(){Header= "Item6", Value = 377},
                //new Item(){Header= "Item7", Value = 144},
                //new Item(){Header= "Item8", Value = 366},
                //new Item(){Header= "Item9", Value = 288},
                //new Item(){Header= "Item10", Value = 155},

                // test 04
                new Item(){Header= "Item1", Value = 101},
                new Item(){Header= "Item2", Value = 208},
                new Item(){Header= "Item3", Value = 75},
                new Item(){Header= "Item4", Value = 135},
                new Item(){Header= "Item5", Value = 300},
                new Item(){Header= "Item6", Value = 400},
                new Item(){Header= "Item7", Value = 360},
                new Item(){Header= "Item8", Value = 499},
                new Item(){Header= "Item9", Value = 233},
                new Item(){Header= "Item10", Value = 122},
            };
        }

        public void Paint(Canvas mainCanvas)
        {
            mainCanvas.Children.Clear();

            try
            {
                //Getting the top value to display
                float _maxValue = GetMaxValue();
                float _minY = GetMinValue();

                //Setting the dimensions of the table
                float chartWidth = (float)mainCanvas.Width;
                float chartHeight = (float)mainCanvas.Height;
                //Setting the margin for the axis
                float axisMargin = chartHeight * 0.15f;
                //Setting the amount of axis there will be, and the interval of numbers between them
                int axisCount = 20;
                float yAxisInterval = (_maxValue - _minY) / axisCount;

                //Getting the reference points
                Point yAxisEndPoint = new Point(axisMargin, axisMargin);
                Point origin = new Point(axisMargin, chartHeight - axisMargin);
                Point xAxisEndPoint = new Point(chartWidth - axisMargin, chartHeight - axisMargin);
                //Setting the physical distance between the axis
                float _distanceBetweenAxis = ((float)(origin.Y - yAxisEndPoint.Y) / axisCount);
                float _scale = (float)(origin.Y - yAxisEndPoint.Y) / (_maxValue - _minY);
                
                //Setting the size of the items
                double _itemSize = (xAxisEndPoint.X - origin.X) / ((Items.Count) + (Items.Count / 2));
                //Setting the width of the blocks
                float blockWidth = (float)_itemSize, blockMargin = (float)_itemSize / 2;

                WriteToConsole($"Number of items: {origin}; {xAxisEndPoint}");

                double yValue = 0;
                var yAxisValue = origin.Y;
                
                //Cycling to create all the axis
                for(int i=0; i<=axisCount; i++)
                {
                    //Declaring the line
                    Line yLine = new Line()
                    {
                        //Setting the color and thickness
                        Stroke = Brushes.LightGray,
                        StrokeThickness = 1,
                        //Setting the points
                        X1 = origin.X,
                        Y1 = (_distanceBetweenAxis * i) + yAxisEndPoint.Y,
                        X2 = xAxisEndPoint.X,
                        Y2 = (_distanceBetweenAxis * i) + yAxisEndPoint.Y,
                    };
                    //Rendering the line
                    mainCanvas.Children.Add(yLine);

                    //Getting the value to display, 2 decimals
                    int _value_int = (int)((_maxValue - (yAxisInterval * i)) * 100);
                    float _realValue = (float)_value_int / (float)100;
                    System.Console.WriteLine($"INT value: {_value_int}, _real: {_realValue}");

                    //Creating the text
                    TextBlock yAxisTextBlock = new TextBlock()
                    {
                        Text = $"{_realValue}",
                        Foreground = Brushes.Black,
                        FontSize = 16,
                    };
                    //Rendering the text
                    mainCanvas.Children.Add(yAxisTextBlock);

                    //measure the textbox
                    yAxisTextBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                    Size _textBlockSize = yAxisTextBlock.DesiredSize;

                    //Setting the anchors
                    Canvas.SetLeft(yAxisTextBlock, origin.X - _textBlockSize.Width);
                    Canvas.SetTop(yAxisTextBlock, ((_distanceBetweenAxis * i) - (_distanceBetweenAxis / 3)) + yAxisEndPoint.Y);

                    //Ending the cycle by increasing the values
                    //yAxisValue -= yAxisInterval;
                    //yValue += yAxisInterval;
                }
                
                var margin = origin.X + blockMargin;
                //Cycling thorugh all the contents, in order to instantiate all rectangles
                foreach (var item in Items)
                {
                    //Instantiating the rectangle
                    Rectangle block = new Rectangle()
                    {
                        //Setting the color
                        Fill = Brushes.Gold,
                        //Setting the dimensions
                        Width = blockWidth,
                        Height = (item.Value - _minY) * _scale,
                    };
                    block.ToolTip = $"{item.Value} \n {item.Header}";
                    //Rendering the object
                    mainCanvas.Children.Add(block);
                    //Positioning the rectangle
                    Canvas.SetLeft(block, margin);
                    Canvas.SetTop(block, origin.Y - block.Height);

                    //Instantiating the text label
                    TextBlock blockHeader = new TextBlock()
                    {
                        Text = item.Header,
                        FontSize = MakeTheThreeRule(70, 20, blockWidth),
                        Foreground = Brushes.Black,
                        HorizontalAlignment = HorizontalAlignment.Center,
                    };
                    //Rendering the text
                    mainCanvas.Children.Add(blockHeader);
                    //Positioning the text
                    Canvas.SetLeft(blockHeader, margin);
                    Canvas.SetTop(blockHeader, origin.Y + 5);


                    margin += (blockWidth + blockMargin);
                }
            }
            catch (Exception exception)
            {
            }
        }

        public float GetMaxValue()
        {
            float _maxValue = 0;

            foreach(Item _item in Items)
            {
                if(_item.Value > _maxValue)
                {
                    _maxValue = _item.Value;
                }
            }

            //Adding a 10%
            _maxValue *= 1.05f;

            return _maxValue;
        }

        public float GetMinValue()
        {
            float _maxValue = Items[0].Value;

            foreach (Item _item in Items)
            {
                if (_item.Value < _maxValue)
                {
                    _maxValue = _item.Value;
                }
            }

            //Substarcting a 10%
            _maxValue *= 0.95f;

            return _maxValue;
        }

        public float MakeTheThreeRule(float _const1, float _const2, float _variable)
        {
            return (_variable * _const2) / _const1;
        }

        public void WriteToConsole(object _message)
        {
            System.Console.WriteLine(_message);
            //Console.Text += _message.ToString() + "\n";
        }
    }

    public class Item
    {
        public string Header { get; set; }
        public float Value { get; set; }
    }
}