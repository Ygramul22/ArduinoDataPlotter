using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TestForCansat.Charts;
using System.Diagnostics;
using System.Windows.Media;
using System.Globalization;
using Pelayitos_2;
using TestForCansat.RadioSystem;

namespace TestForCansat.ScreenUtilities
{
    public class DisplayManager
    {
        //Function used to relocate and resize the screen components when its size has changed
        public void ResizeScreen_0(Vector2 _newSize, Screen0 _data)
        {
            //First, calculate the new sizes
            float _width = (float)(_newSize.X);
            float _height = (float)(_newSize.Y);

            //This screen has 2 buttons width and 2 buttons height. Calculating the values
            float _buttonWidth = _width / 2;
            float _buttonHeight = _height / 2;

            //Applying size changes
            _data.MapButton.Width = _buttonWidth;
            _data.GraphicsButton.Width = _buttonWidth;
            _data.PacketsButton.Width = _buttonWidth;
            _data.AIButton.Width = _buttonWidth;

            _data.MapButton.Height = _buttonHeight;
            _data.GraphicsButton.Height = _buttonHeight;
            _data.PacketsButton.Height = _buttonHeight;
            _data.AIButton.Height = _buttonHeight;

            //Positoning the buttons
            Canvas.SetLeft(_data.MapButton, 0);
            Canvas.SetLeft(_data.GraphicsButton, 0);
            Canvas.SetLeft(_data.PacketsButton, _buttonWidth);
            Canvas.SetLeft(_data.AIButton, _buttonWidth);

            Canvas.SetTop(_data.MapButton, 0);
            Canvas.SetTop(_data.GraphicsButton, _buttonHeight);
            Canvas.SetTop(_data.PacketsButton, 0);
            Canvas.SetTop(_data.AIButton, _buttonHeight);
            Console.WriteLine(_height);
        }

        public Vector2 ResizeScreen_1(Vector2 _newSize, Screen1 _data)
        {
            //First, calculate the new sizes
            float _width = (float)_newSize.X * 0.98f;
            float _height = (float)_newSize.Y * 0.98f;

            //This screen has a top bar, calculating its height
            float _barHeight = _height / 14;
            float _barWidth = _barHeight * 5;
            //Calculating the slider width
            float _sliderWidth = _width / 20;

            //Applying size changes
            _data.BackButton.Width = _barWidth;
            _data.BackButton.Height = _barHeight;

            _data.Map.Height = _barHeight * 13;
            _data.Map.Width = _width;//(_width - _sliderWidth);

            //_data.Slider.Height = _barHeight * 13;
            //_data.Slider.Width = _sliderWidth;

            _data.TopRectangle.Width = _width;
            _data.TopRectangle.Height = _barHeight;

            //_data.SliderRectangle.Width = _sliderWidth;
            //_data.SliderRectangle.Height = (_height - _barHeight);

            //Positoning the buttons
            Canvas.SetLeft(_data.BackButton, 0);
            Canvas.SetLeft(_data.Map, 0);
            Canvas.SetLeft(_data.TopRectangle, 0);
            //Canvas.SetRight(_data.Slider, 0);
            //Canvas.SetRight(_data.SliderRectangle, _sliderWidth / 2);

            Canvas.SetTop(_data.BackButton, 0);
            Canvas.SetTop(_data.TopRectangle, 0);
            Canvas.SetTop(_data.Map, _barHeight);
            //Canvas.SetTop(_data.Slider, _barHeight);
            //Canvas.SetTop(_data.SliderRectangle, _barHeight);

            _data.BackButton.HorizontalAlignment = HorizontalAlignment.Left;

            //Calculating and returning Map image Size
            Vector2 _mapPropoprtions = new Vector2(((_width) / (_barHeight * 13)), 1);
            Vector2 _mapSize = new Vector2((int)(_mapPropoprtions.X * 300), _mapPropoprtions.Y * 300);
            return _mapSize;
        }

        public void ResizeScreen_2(Vector2 _newSize, Screen2 _data)
        {
            Console.WriteLine(_data.ConfigurationButton);

            //Getting the size of the window
            float _width = _newSize.X;
            float _height = _newSize.Y;

            //Calculating values
            float _topPanelHeight = _height / 14;

            //Applying the sizes
            _data.BackButton.Width = _topPanelHeight * 5;
            _data.TopBarRectangle.Width = _width;
            _data.ExportButton.Width = _topPanelHeight * 5;
            _data.ConfigurationButton.Width = _topPanelHeight;
            _data.Canvas.Width = _width;
            _data.GraphicsText.Width = _width; //(_width - (_data.BackButton.Width + _data.ConfigurationButton.Width + _data.ExportButton.Width));

            _data.BackButton.Height = _topPanelHeight;
            _data.TopBarRectangle.Height = _topPanelHeight;
            _data.ExportButton.Height = _topPanelHeight;
            _data.ConfigurationButton.Height = _topPanelHeight;
            _data.Canvas.Height = _height;
            _data.GraphicsText.Height = _topPanelHeight;

            //Positioning the objects
            Canvas.SetTop(_data.BackButton, 0);
            Canvas.SetTop(_data.TopBarRectangle, 0);
            Canvas.SetTop(_data.ExportButton, 0);
            Canvas.SetTop(_data.ConfigurationButton, 0);
            Canvas.SetTop(_data.Canvas, _topPanelHeight);
            Canvas.SetTop(_data.GraphicsTextScroller, 0);

            Canvas.SetLeft(_data.BackButton, 0);
            Canvas.SetLeft(_data.TopBarRectangle, 0);
            Canvas.SetLeft(_data.GraphicsTextScroller, 0);
            Canvas.SetRight(_data.ExportButton, _topPanelHeight);
            Canvas.SetLeft(_data.ConfigurationButton, (_width - _topPanelHeight));
            Console.WriteLine("Got Here!");
            //Canvas.SetRight(_data.Canvas, 0);

            //Setting alingment
            _data.BackButton.HorizontalAlignment = HorizontalAlignment.Left;
            _data.ConfigurationButton.HorizontalAlignment = HorizontalAlignment.Right;

            //Setting text
            _data.GraphicsText.Text = "Graphics";
            _data.GraphicsText.Foreground = Brushes.Black;
            _data.GraphicsText.HorizontalContentAlignment = HorizontalAlignment.Center;
            _data.GraphicsText.VerticalContentAlignment = VerticalAlignment.Center;
            _data.GraphicsText.FontSize = CalculateMaxFontSize(_data.GraphicsText.Width, _data.GraphicsText.Height, _data.GraphicsText.Text);
            //_data.GraphicsText.Background = Brushes.Black;

            DrawGraphic(0, 70, _data.Canvas);
        }

        public void ResizeScreen_25(Vector2 _newSize, Screen2_5 _data)
        {
            //Getting the size of the window
            float _width = _newSize.X;
            float _height = _newSize.Y;

            //Calculating values
            float _panelWidth = _width / 3;
            float _topPanelHeight = _height / 14;
            float _GraphicsButtonsSizes = _panelWidth / 3;
            float _margin = _GraphicsButtonsSizes / 3;
            float _ApplyHeight = (_panelWidth - (_margin * 2)) / 5;
            float _marging_rest = _margin / 2;
            float _occupyed = _topPanelHeight + _GraphicsButtonsSizes + _ApplyHeight + (_marging_rest * 3) + _margin + (float)_data.Apply.Height;
            float _remainingHeight = _height - _occupyed;

            //Applying the sizes
            _data.BackButton.Width = _topPanelHeight * 5;
            _data.TopBarRectangle.Width = _panelWidth * 2;
            _data.ExportButton.Width = _topPanelHeight * 5;
            _data.ConfigurationButton.Width = _topPanelHeight;
            _data.Canvas.Width = _panelWidth * 2;
            _data.PanelBackground.Width = _panelWidth;
            _data.GraphicsText.Width = _panelWidth * 2;
            _data.SettingsText.Width = _panelWidth;
            _data.ColumnsChartButton.Width = _GraphicsButtonsSizes;
            _data.LineChartButton.Width = _GraphicsButtonsSizes;
            _data.Apply.Width = _panelWidth - (_margin * 2);
            _data.AxisDropdown.Width = _panelWidth - (_margin * 2);
            _data.RefreshRealtimeCheck.Width = _ApplyHeight / 2;
            //_data.StartPacket.Width = _panelWidth;
            //_data.EndPacket.Width = _panelWidth;

            _data.BackButton.Height = _topPanelHeight;
            _data.TopBarRectangle.Height = _topPanelHeight;
            _data.ExportButton.Height = _topPanelHeight;
            _data.ConfigurationButton.Height = _topPanelHeight;
            _data.Canvas.Height = _height;
            _data.PanelBackground.Height = _height;
            _data.GraphicsText.Height = _topPanelHeight;
            _data.SettingsText.Height = _topPanelHeight;
            _data.ColumnsChartButton.Height = _GraphicsButtonsSizes;
            _data.LineChartButton.Height = _GraphicsButtonsSizes;
            _data.AxisDropdown.Height = _ApplyHeight;
            _data.RefreshRealtimeCheck.Height = _ApplyHeight / 2;
            //_data.StartPacket.Height = _remainingHeight / 2;
            //_data.EndPacket.Height = _remainingHeight / 2;

            //Positioning the objects
            Canvas.SetTop(_data.BackButton, 0);
            Canvas.SetTop(_data.TopBarRectangle, 0);
            Canvas.SetTop(_data.ExportButton, 0);
            Canvas.SetTop(_data.ConfigurationButton, 0);
            Canvas.SetTop(_data.Canvas, _topPanelHeight);

            Canvas.SetLeft(_data.BackButton, 0);
            Canvas.SetLeft(_data.TopBarRectangle, 0);
            Canvas.SetRight(_data.ExportButton, _topPanelHeight);
            Canvas.SetLeft(_data.ConfigurationButton, (_panelWidth * 2) - _topPanelHeight);
            Canvas.SetLeft(_data.Canvas, 0);
            Canvas.SetLeft(_data.PanelBackground, _panelWidth * 2);
            Canvas.SetLeft(_data.ExportButton, (_panelWidth * 2) - (_topPanelHeight * 6));
            Canvas.SetRight(_data.SettingsTextScroller, 0);

            Canvas.SetZIndex(_data.ExportButton, 50);
            _data.Apply.VerticalAlignment = VerticalAlignment.Bottom;

            //The positions of the panel may differ if refresh realtime is enabled
            if(MainWindow.Instance.RefreshRealtime)
            {
                Canvas.SetTop(_data.ColumnsChartButton, (_topPanelHeight + _marging_rest));
                Canvas.SetTop(_data.LineChartButton, (_topPanelHeight + _marging_rest));
                Canvas.SetTop(_data.AxisDropdown, _topPanelHeight + _GraphicsButtonsSizes + (_marging_rest * 2));
                Canvas.SetTop(_data.RefreshRealtimeCheck, _topPanelHeight + _GraphicsButtonsSizes + _ApplyHeight + (_marging_rest * 3));
                Canvas.SetBottom(_data.Apply, _margin);

                Canvas.SetRight(_data.ColumnsChartButton, (_GraphicsButtonsSizes / 3));
                Canvas.SetRight(_data.LineChartButton, (((_GraphicsButtonsSizes / 3) * 2) + (_GraphicsButtonsSizes)));
                Canvas.SetRight(_data.AxisDropdown, _margin);
                Canvas.SetRight(_data.RefreshRealtimeCheck, _margin);
                Canvas.SetRight(_data.Apply, _margin);
            }
            else
            {
                Canvas.SetTop(_data.ColumnsChartButton, (_topPanelHeight + (_GraphicsButtonsSizes / 3)));
                Canvas.SetTop(_data.LineChartButton, (_topPanelHeight + (_GraphicsButtonsSizes / 3)));
                Canvas.SetTop(_data.AxisDropdown, _topPanelHeight + (_margin * 2) + _GraphicsButtonsSizes);
                Canvas.SetBottom(_data.Apply, _margin);

                Canvas.SetRight(_data.ColumnsChartButton, (_GraphicsButtonsSizes / 3));
                Canvas.SetRight(_data.LineChartButton, (((_GraphicsButtonsSizes / 3) * 2) + (_GraphicsButtonsSizes)));
                Canvas.SetRight(_data.AxisDropdown, _margin);
            }

            //Setting alingment
            _data.BackButton.HorizontalAlignment = HorizontalAlignment.Left;

            #region Text
            //Setting text
            _data.GraphicsText.Text = "Graphics";
            _data.GraphicsText.Foreground = Brushes.Black;
            _data.GraphicsText.HorizontalContentAlignment = HorizontalAlignment.Center;
            _data.GraphicsText.VerticalContentAlignment = VerticalAlignment.Center;
            _data.GraphicsText.FontSize = CalculateMaxFontSize(_data.GraphicsText.Width, _data.GraphicsText.Height, _data.GraphicsText.Text);
            //_data.GraphicsText.Background = Brushes.Black;

            _data.SettingsText.Text = "Settings";
            _data.SettingsText.Foreground = Brushes.Black;
            _data.SettingsText.HorizontalContentAlignment = HorizontalAlignment.Center;
            _data.SettingsText.VerticalContentAlignment = VerticalAlignment.Center;
            _data.SettingsText.FontSize = CalculateMaxFontSize(_data.SettingsText.Width, _data.GraphicsText.Height, _data.SettingsText.Text);
            //_data.SettingsText.Background = Brushes.Black;

            _data.AxisDropdown.Foreground = Brushes.Black;
            _data.AxisDropdown.HorizontalContentAlignment = HorizontalAlignment.Center;
            _data.AxisDropdown.VerticalContentAlignment = VerticalAlignment.Center;
            _data.AxisDropdown.FontSize = CalculateMaxFontSize(_data.SettingsText.Width, _data.GraphicsText.Height, _data.AxisDropdown.Text);
            //_data.SettingsText.Background = Brushes.Black;
            #endregion

            DrawGraphic(0, 70, _data.Canvas);
        }

        public void ResizeScreen_3(Vector2 _newSize, Screen3 _data)
        {
            //Getting the size of the window
            float _width = _newSize.X;
            float _height = _newSize.Y;

            //Calculating values
            float _topPanelHeight = _height / 14;
            float _buttonSize = (_height - _topPanelHeight) / 4;
            float _consoleWidth = _width / 2;
            float _remainingSpace = _width - (_consoleWidth + _buttonSize);
            float _buttonMargin = _remainingSpace / 2;

            //Applying the sizes
            _data.BackButton.Width = _topPanelHeight * 5;
            _data.TopBar.Width = _width;
            _data.console.Width = _consoleWidth;
            _data.LoadPackets.Width = _buttonSize;
            _data.SaveSessionPackets.Width = _buttonSize;
            _data.SaveAllPackets.Width = _buttonSize;

            _data.BackButton.Height = _topPanelHeight;
            _data.TopBar.Height = _topPanelHeight;
            _data.console.Height = _topPanelHeight * 13;
            _data.LoadPackets.Height = _buttonSize;
            _data.SaveSessionPackets.Height = _buttonSize;
            _data.SaveAllPackets.Height = _buttonSize;

            //Positioning the objects
            Canvas.SetTop(_data.BackButton, 0);
            Canvas.SetTop(_data.TopBar, 0);
            Canvas.SetTop(_data.ConsoleScroller, _topPanelHeight);
            Canvas.SetTop(_data.LoadPackets, (_topPanelHeight + (_buttonSize / 4)));
            Canvas.SetTop(_data.SaveAllPackets, (_topPanelHeight + ((_buttonSize / 4) * 2) + _buttonSize));
            Canvas.SetTop(_data.SaveSessionPackets, (_topPanelHeight + ((_buttonSize / 4) * 3) + (_buttonSize * 2)));

            Canvas.SetLeft(_data.BackButton, 0);
            Canvas.SetLeft(_data.TopBar, 0);
            Canvas.SetLeft(_data.ConsoleScroller, 0);
            Canvas.SetLeft(_data.LoadPackets, (_consoleWidth + _buttonMargin));
            Canvas.SetLeft(_data.SaveAllPackets, (_consoleWidth + _buttonMargin));
            Canvas.SetLeft(_data.SaveSessionPackets, (_consoleWidth + _buttonMargin));

            //Setting alingment
            _data.BackButton.HorizontalAlignment = HorizontalAlignment.Left;
        }

        public void ResizeScreen_4(Vector2 _newSize, Screen4 _data)
        {
            //Getting the size of the window
            float _width = _newSize.X;
            float _height = _newSize.Y;

            //Calculating values
            float _topPanelHeight = _height / 14;

            //Applying the sizes
            _data.BackButton.Width = _topPanelHeight * 5;
            _data.TopBar.Width = _width;

            _data.BackButton.Height = _topPanelHeight;
            _data.TopBar.Height = _topPanelHeight;

            //Positioning the objects
            Canvas.SetTop(_data.BackButton, 0);

            Canvas.SetLeft(_data.BackButton, 0);

            //Setting alingment
            _data.BackButton.HorizontalAlignment = HorizontalAlignment.Left;
        }
        
        public float CalculateMaxFontSize(double textBoxWidth, double textBoxHeight, string text)
        {
            FormattedText formattedText = new FormattedText(
                text,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface("Arial"),
                1,
                Brushes.Black);

            double fontSize = 1;

            while (formattedText.Width < textBoxWidth && formattedText.Height < textBoxHeight)
            {
                fontSize++;
                formattedText.SetFontSize(fontSize);
            }

            // The maximum font size that will fit in the textbox while occupying most of the available space
            double maximumFontSize = fontSize - 1;
            return (float)maximumFontSize;
        }

        public void DrawGraphic(int _startId, int _endId, Canvas _canvas)
        {
            if(MainWindow.Instance.GraphicType == 0)
            {
                ColumnChart _chart = new ColumnChart();

                _chart.Items = new List<Item>();

                foreach(KeyValuePair<int, Packet> _valuePair in MainWindow.Instance.PortManager.PacketsLoaded)
                {
                    Packet _packet = _valuePair.Value;
                    if(_packet.PacketID >= _startId && _packet.PacketID <= _endId)
                    {
                        Item _item = new Item();
                        _item.Header = _packet.PacketID.ToString();

                        switch (MainWindow.Instance.DisplayVariable)
                        {
                            case 0:
                                _item.Value = _packet.Temperature;
                                break;

                            case 1:
                                _item.Value = _packet.Pressure;
                                break;

                            case 2:
                                _item.Value = _packet.Altitude;
                                break;
                        }

                        _chart.Items.Add(_item);
                    }
                }

                _chart.Paint(_canvas);
            }
            else if (MainWindow.Instance.GraphicType == 1)
            {
                LineChart _chart = new LineChart();

                _chart.values = new List<Value>();

                foreach (KeyValuePair<int, Packet> _valuePair in MainWindow.Instance.PortManager.PacketsLoaded)
                {
                    Packet _packet = _valuePair.Value;
                    if (_packet.PacketID >= _startId && _packet.PacketID <= _endId)
                    {
                        float _yAxisValue = 0;

                        switch (MainWindow.Instance.DisplayVariable)
                        {
                            case 0:
                                _yAxisValue = _packet.Temperature;
                                break;

                            case 1:
                                _yAxisValue = _packet.Pressure;
                                break;

                            case 2:
                                _yAxisValue = _packet.Altitude;
                                break;
                        }

                        _chart.values.Add(new Value(_packet.PacketID, _yAxisValue));
                    }
                }

                _chart.Paint(_canvas);
            }
        }
    }
}