using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shell;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestForCansat.Charts;
using TestForCansat.RadioSystem;
using TestForCansat.SaveSytem;
using TestForCansat.ScreenUtilities;
using Microsoft.Win32;

namespace Pelayitos_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }

        //Referencing the depencies in charge of rendering the charts
        ColumnChart ColumnChartCreator;
        LineChart LineChartCreator;
        PieChart PieChartCreator;

        //Creating the reference to the class that handles the ports system, and the one in charge of display
        public PortManager PortManager;
        public DisplayManager DisplayManager;

        //Storing reference to the classes that open the dialogues, to save and load
        SaveFileDialog SaveFileDialog;
        OpenFileDialog OpenFileDialog;

        //Declaring the variable to keep track of the current screen
        public int ScreenID = 0;
        #region ScreenData
        //Various variables in charge of keeping data of the current screen
        Screen0 Screen0Data;
        Screen1 Screen1Data;
        Screen2 Screen2Data;
        public Screen2_5 Screen2_5Data;
        Screen3 Screen3Data;
        Screen4 Screen4Data;
        #endregion

        //storing the in-app console log
        public string ConsoleLog;

        #region ImagesDirections
        //Creating arrays to store all of the buttons
        public enum ProgramButtons
        {
            Map = 0,
            Graphics,
            Packets,
            AI,
            Back,
            BoxChart,
            Export,
            Options,
            LineChart,
            Apply,
            LoadPackets,
            SaveAllPackets,
            SaveSessionPackets,
            RefreshRealtimeEnabled,
            RefreshRealtimeDisabled,
        };
        public string[] FreeButtonsImages = new string[15]
        {
            "/Images/MapButton_Free.jpg",
            "/Images/GraphicsButton_Free.jpg",
            "/Images/PacketsButton_Free.jpg",
            "/Images/AIButton_Free.jpg",
            "/Images/BackButton_Free.jpg",
            "/Images/BoxChartButton_Free.jpg",
            "/Images/ExportButton_Free.jpg",
            "/Images/OptionsButton_Free.jpg",
            "/Images/LineChartButton_Free.jpg",
            "/Images/ApplyButton_Free.jpg",
            "/Images/LoadPacketsButton_Free.jpg",
            "/Images/SaveAllPackets_Free.jpg",
            "/Images/SaveButton_Free.jpg",
            "/Images/CheckboxX_Free.jpg",
            "/Images/CheckboxFree_Free.jpg",
        };
        public string[] MouseOverButtonsImages = new string[15]
        {
            "/Images/MapButton_MouseOver.jpg",
            "/Images/GraphicsButton_MouseOver.jpg",
            "/Images/PacketsButton_MouseOver.jpg",
            "/Images/AIButton_MouseOver.jpg",
            "/Images/BackButton_MouseOver.jpg",
            "/Images/BoxChartButton_MouseOver.jpg",
            "/Images/ExportButton_MouseOver.jpg",
            "/Images/OptionsButton_MouseOver.jpg",
            "/Images/LineChartButton_MouseOver.jpg",
            "/Images/ApplyButton_MouseOver.jpg",
            "/Images/LoadPacketsButton_MouseOver.jpg",
            "/Images/SaveAllPackets_MouseOver.jpg",
            "/Images/SaveButton_MouseOver.jpg",
            "/Images/CheckboxX_MouseOver.jpg",
            "/Images/CheckboxFree_MouseOver.jpg",
        };
        #endregion

        //Creating a variable to store the current position in coordinates and an event for when it changes
        public string SatelliteCoordinates;
        public event System.EventHandler PositionChanged;
        public float CurrentMapZoom;
        public Vector2 MapDimensions;

        //Storing the new Map dimensions until downloaded
        private BitmapImage MapImage;

        //Storing the values to remember for the graphics
        public bool RefreshRealtime;
        public int GraphicType; //0-Box, 1-Lines
        public int DisplayVariable; //0-Temperature, 1-pressure, 2-altitude

        //Function called at the start of the run
        public MainWindow()
        {
            //Creating an instance to this class
            Instance = this;

            //Starting the XAML
            InitializeComponent();

            //Initializing the console
            ConsoleLog = "";

            PortManager = new PortManager();
            PortManager.InitializePorts("COM10");

            //Creating the dependencies for the chart rendering
            ColumnChartCreator = new ColumnChart();
            LineChartCreator = new LineChart();
            PieChartCreator = new PieChart();

            //Anexing the screen size changed event to the function
            SizeChanged += ScreenSizeChanged;
            DisplayManager = new DisplayManager();

            //Testing the console
            ConsoleLog = "test \n>> test \n";

            //Setting the default graphics value
            RefreshRealtime = true;
            GraphicType = 0;
            DisplayVariable = 0;

            //Creating the first screen
            CreateScreenNumber0();
            #region Comment
            /*/
            PortManager = new PortManager();
            PortManager.InitializePorts("COM9", Console);

            ScrollViewer.SetVerticalScrollBarVisibility(Console, ScrollBarVisibility.Auto);
            SizeChanged += WindowSizeChanged;

            LineChartCreator.holders = new List<Holder>();
            LineChartCreator.values = new List<Value>()
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
            LineChartCreator.Paint(MainDisplayer);
            /*/
            #endregion
        }

        #region CreateScreens
        public void CreateScreenNumber0()
        {
            //Cleaning the screen
            ClearScreenData();

            //Preparing the storer to be able to get data into it
            Screen0Data = new Screen0();

            //Creating the three big images
            Screen0Data.MapButton = CreateImage_Button(FreeButtonsImages[(int)ProgramButtons.Map], (int)ProgramButtons.Map, true);
            Screen0Data.GraphicsButton = CreateImage_Button(FreeButtonsImages[(int)ProgramButtons.Graphics], (int)ProgramButtons.Graphics, true);
            Screen0Data.PacketsButton = CreateImage_Button(FreeButtonsImages[(int)ProgramButtons.Packets], (int)ProgramButtons.Packets, true);
            Screen0Data.AIButton = CreateImage_Button(FreeButtonsImages[(int)ProgramButtons.AI], (int)ProgramButtons.AI, true);

            //Recalculating dimensions
            DisplayManager.ResizeScreen_0(new Vector2((float)ParentGrid.Width, (float)ParentGrid.Height), Screen0Data);
        }

        public void CreateScreenNumber1()
        {
            //Cleaning the screen
            ClearScreenData();

            //Preparing the storer to be able to get data into it
            Screen1Data = new Screen1();

            //Crating the auxiliar rectangles
            Screen1Data.TopRectangle = CreateRectangle(Brushes.DarkGray);
            //Screen1Data.SliderRectangle = CreateRectangle(Brushes.DarkGray);
            //Creating the three big images
            Screen1Data.BackButton = CreateImage_Button(FreeButtonsImages[(int)ProgramButtons.Back], (int)ProgramButtons.Back, true);
            Screen1Data.Map = CreateImage_Button("Images/descarga.jpg", 0, false);
            //Screen1Data.Slider = CreateSlider(false);

            //Generating mpa
            RedrawMap();

            //Fitting the images
            DisplayManager.ResizeScreen_1(new Vector2((float)MainWindow.GetWindow(ParentGrid).RenderSize.Width, (float)MainWindow.GetWindow(ParentGrid).RenderSize.Height), Screen1Data);
        }

        public void CreateScreenNumber2()
        {
            //First of all, checking wether it came from screen 2.5 or not
            if(Screen2_5Data != null)
            {
                //Came from screen 2.5
                //First, create a new instance of the screen2data
                Screen2Data = new Screen2();
                //Second, make a selective deletion of objects in screen
                #region Selective deletion
                ParentGrid.Children.Remove(Screen2_5Data.SettingsTextScroller);
                ParentGrid.Children.Remove(Screen2_5Data.ColumnsChartButton);
                ParentGrid.Children.Remove(Screen2_5Data.LineChartButton);
                ParentGrid.Children.Remove(Screen2_5Data.RefreshRealtimeCheck);
                ParentGrid.Children.Remove(Screen2_5Data.Apply);
                ParentGrid.Children.Remove(Screen2_5Data.AxisDropdown);
                ParentGrid.Children.Remove(Screen2_5Data.PanelBackground);
                ParentGrid.Children.Remove(Screen2_5Data.ConfigurationButton);
                ParentGrid.Children.Remove(Screen2_5Data.ExportButton);
                ParentGrid.Children.Remove(Screen2_5Data.RefreshRealtimeCheck);
                #endregion
                //Then, copy valuable data
                #region data trespassing
                Screen2Data.TopBarRectangle = Screen2_5Data.TopBarRectangle;
                Screen2Data.BackButton = Screen2_5Data.BackButton;
                Screen2Data.GraphicsText = Screen2_5Data.GraphicsText;
                Screen2Data.Canvas = Screen2_5Data.Canvas;
                Screen2Data.GraphicsTextScroller = Screen2_5Data.GraphicsTextScroller;
                #endregion
                //Then, we create the new button
                Screen2Data.ConfigurationButton = CreateImage_Button(FreeButtonsImages[(int)ProgramButtons.Options], (int)ProgramButtons.Options, true);
                Screen2Data.ExportButton = CreateImage_Button(FreeButtonsImages[(int)ProgramButtons.Export], (int)ProgramButtons.Export, true);
                //Finally, set the reference to screen 2.5 to null
                Screen2_5Data = null;
            }
            else
            {
                //Came from screen 0. Proceed as normal
                //Cleaning the screen
                ClearScreenData();

                //Preparing the storer to be able to get data into it
                Screen2Data = new Screen2();

                //Creating the three big images
                Screen2Data.TopBarRectangle = CreateRectangle(Brushes.DarkGray);
                Screen2Data.GraphicsText = CreateTextBox(Brushes.Transparent, Brushes.Black, out Screen2Data.GraphicsTextScroller, true);
                Screen2Data.ExportButton = CreateImage_Button(FreeButtonsImages[(int)ProgramButtons.Export], (int)ProgramButtons.Export, true);
                Screen2Data.BackButton = CreateImage_Button(FreeButtonsImages[(int)ProgramButtons.Back], (int)ProgramButtons.Back, true);
                Screen2Data.ConfigurationButton = CreateImage_Button(FreeButtonsImages[(int)ProgramButtons.Options], (int)ProgramButtons.Options, true);
                Screen2Data.Canvas = CreateCanvas();
            }

            //Fitting the images
            DisplayManager.ResizeScreen_2(new Vector2((float)MainWindow.GetWindow(ParentGrid).RenderSize.Width, (float)MainWindow.GetWindow(ParentGrid).RenderSize.Height), Screen2Data);
        }

        public void CreateScreenNumber2_5()
        {
            //First, create a class to store it all
            Screen2_5Data = new Screen2_5();
            //Second, heritage all the usable things
            #region Heritage
            Screen2_5Data.TopBarRectangle = Screen2Data.TopBarRectangle;
            Screen2_5Data.BackButton = Screen2Data.BackButton;
            Screen2_5Data.GraphicsText = Screen2Data.GraphicsText;
            Screen2_5Data.ConfigurationButton = Screen2Data.ConfigurationButton;
            Screen2_5Data.Canvas = Screen2Data.Canvas;
            Screen2_5Data.ExportButton = Screen2Data.ExportButton;
            Screen2_5Data.GraphicsTextScroller = Screen2Data.GraphicsTextScroller;
            #endregion
            //Then, create all the new references
            #region Reference creation
            Screen2_5Data.PanelBackground = CreateRectangle(Brushes.Gray);
            Screen2_5Data.SettingsText = CreateTextBox(Brushes.Transparent, Brushes.Black, out Screen2_5Data.SettingsTextScroller, true);
            Screen2_5Data.ColumnsChartButton = CreateImage_Button(FreeButtonsImages[(int)ProgramButtons.BoxChart], (int)ProgramButtons.BoxChart, true);
            Screen2_5Data.LineChartButton = CreateImage_Button(FreeButtonsImages[(int)ProgramButtons.LineChart], (int)ProgramButtons.LineChart, true);
            Screen2_5Data.AxisDropdown = CreateComboBox(new string[3] { "Temperature", "Pressure", "Height" });
            Screen2_5Data.Apply = CreateImage_Button(FreeButtonsImages[(int)ProgramButtons.Apply], (int)ProgramButtons.Apply, true);
            Screen2_5Data.RefreshRealtimeCheck = RefreshRealtime? CreateImage_Button(FreeButtonsImages[(int)ProgramButtons.RefreshRealtimeEnabled], (int)ProgramButtons.RefreshRealtimeEnabled, true) : CreateImage_Button(FreeButtonsImages[(int)ProgramButtons.RefreshRealtimeDisabled], (int)ProgramButtons.RefreshRealtimeDisabled, true);
            //Screen2_5Data.EndPacket = CreateSlider(1, true, true);
            //Screen2_5Data.StartPacket = CreateSlider(0, true, true);
            #endregion

            DisplayManager.ResizeScreen_25(new Vector2((float)MainWindow.GetWindow(ParentGrid).RenderSize.Width, (float)MainWindow.GetWindow(ParentGrid).RenderSize.Height), Screen2_5Data);
        }

        public void CreateScreenNumber3()
        {
            //Cleaning the screen
            ClearScreenData();

            //Preparing the storer to be able to get data into it
            Screen3Data = new Screen3();

            //Creating the three big images
            Screen3Data.TopBar = CreateRectangle(Brushes.DarkGray);
            Screen3Data.BackButton = CreateImage_Button(FreeButtonsImages[(int)ProgramButtons.Back], (int)ProgramButtons.Back, true);
            Screen3Data.console = CreateTextBox(Brushes.Black, Brushes.White, out Screen3Data.ConsoleScroller, false);
            Screen3Data.LoadPackets = CreateImage_Button(FreeButtonsImages[(int)ProgramButtons.LoadPackets], (int)ProgramButtons.LoadPackets, true);
            Screen3Data.SaveSessionPackets = CreateImage_Button(FreeButtonsImages[(int)ProgramButtons.SaveSessionPackets], (int)ProgramButtons.SaveSessionPackets, true);
            Screen3Data.SaveAllPackets = CreateImage_Button(FreeButtonsImages[(int)ProgramButtons.SaveAllPackets], (int)ProgramButtons.SaveAllPackets, true);

            //Writting the console
            Screen3Data.RefreshConsole();
            Screen3Data.PrintToConsole("test");
            Console.WriteLine(ConsoleLog);

            //Referencing the console
            PortManager.PacketsScreenData = Screen3Data;

            //Fitting the images
            DisplayManager.ResizeScreen_3(new Vector2((float)MainWindow.GetWindow(ParentGrid).RenderSize.Width, (float)MainWindow.GetWindow(ParentGrid).RenderSize.Height), Screen3Data);
        }

        public void CreateScreenNumber4()
        {
            //Cleaning the screen
            ClearScreenData();

            //Preparing the storer to be able to get data into it
            Screen4Data = new Screen4();

            //Creating the items
            Screen4Data.TopBar = CreateRectangle(Brushes.DarkGray);
            Screen4Data.BackButton = CreateImage_Button(FreeButtonsImages[(int)ProgramButtons.Back], (int)ProgramButtons.Back, true);

            //Fitting the images
            DisplayManager.ResizeScreen_4(new Vector2((float)MainWindow.GetWindow(ParentGrid).RenderSize.Width, (float)MainWindow.GetWindow(ParentGrid).RenderSize.Height), Screen4Data);
        }
        #endregion

        #region CreateElements
        //Function used to create an image that will be used as a button
        public Image CreateImage_Button(string _route, int _imageID, bool _interacts)
        {
            string _imageName = "a" + _imageID.ToString();
            //Creating the image
            Image _img = new Image()
            {
                //Setting the internal values
                Source = new BitmapImage(new Uri(_route, UriKind.RelativeOrAbsolute)),
                Height = 1000,
                Width = 1000,
                Margin = new Thickness(0, 0, 0, 0),
                Stretch = Stretch.Uniform,
                Name = _imageName
            };
            //Checking if the image is used as a button
            if (_interacts)
            {
                //Subscribing to necessary events
                _img.MouseEnter += MouseEnteredImage;
                _img.MouseLeave += MouseExitedImage;
                _img.MouseDown += MouseClickedImage;
            }

            //Adding it to the viewport
            ParentGrid.Children.Add(_img);

            //returning the image to store it
            return _img;
        }

        public Rectangle CreateRectangle(SolidColorBrush _color)
        {
            Rectangle _rectangle = new Rectangle()
            {
                Fill = _color,
            };
            ParentGrid.Children.Add(_rectangle);

            return _rectangle;
        }

        public TextBox CreateTextBox(SolidColorBrush _background, SolidColorBrush _textColor, out ScrollViewer _scroll, bool Interactable)
        {
            TextBox _textBox = new TextBox()
            {
                Background = _background,
                TextWrapping = TextWrapping.Wrap,
                IsEnabled = false,
                Foreground = _textColor,
                //IsReadOnly = Interactable,
            };

            ScrollViewer _scrollViewer = new ScrollViewer()
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                Content = _textBox,
            };

            ParentGrid.Children.Add(_scrollViewer);

            _scroll = _scrollViewer;
            return _textBox;
        }

        public Slider CreateSlider(int _id, bool _horizontal,  bool _addToViewport)
        {
            Slider _slider = new Slider()
            {
                Name = "a" + _id.ToString(),
                Maximum = 20,
                Minimum = 15,
                Value = 17.5f,
            };

            if (_horizontal)
            {
                _slider.Orientation = Orientation.Horizontal;
            }
            else
            {
                _slider.Orientation = Orientation.Vertical;
            }

            _slider.ValueChanged += SliderValueChanged;

            if(_addToViewport) { ParentGrid.Children.Add(_slider); }

            return _slider;
        }

        public Canvas CreateCanvas()
        {
            Canvas _canvas = new Canvas()
            {
                Background = Brushes.DarkKhaki
            };

            ParentGrid.Children.Add(_canvas);

            return _canvas;
        }

        public ComboBox CreateComboBox(string[] _items)
        {
            ComboBox _comboBox = new ComboBox()
            {
                SelectedIndex = 0,
            };

            foreach(string _item in _items)
            {
                _comboBox.Items.Add(_item);
            }

            _comboBox.SelectionChanged += DropdownChanged;

            ParentGrid.Children.Add(_comboBox);

            return _comboBox;
        }
        #endregion

        #region Events
        //The next two functions are used for alternating the buttons' images, to give the ilussion
        private void MouseEnteredImage(object sender, MouseEventArgs e)
        {
            if (sender.GetType() == typeof(Image))
            {
                Image _img = (Image)sender;
                int _imageID = (int.Parse)(_img.Name.Remove(0, 1));
                _img.Source = new BitmapImage(new Uri(MouseOverButtonsImages[_imageID], UriKind.RelativeOrAbsolute));
            }
        }

        private void MouseExitedImage(object sender, MouseEventArgs e)
        {
            if (sender.GetType() == typeof(Image))
            {
                Image _img = (Image)sender;
                int _imageID = (int.Parse)(_img.Name.Remove(0, 1));
                _img.Source = new BitmapImage(new Uri(FreeButtonsImages[_imageID], UriKind.RelativeOrAbsolute));
            }
        }

        //Function that sends the message when a button is clicked. It also swaps images
        private void MouseClickedImage(object sender, MouseEventArgs e)
        {
            if (sender.GetType() == typeof(Image))
            {
                Image _img = (Image)sender;
                int _imageID = (int.Parse)(_img.Name.Remove(0, 1));
                MouseClickedButton(_imageID);
            }
        }
        
        //Function in charge of processing, on first instance, the changes in screen size
        public void ScreenSizeChanged(object sender, SizeChangedEventArgs _e)
        {
            ParentGrid.Width = _e.NewSize.Width;//(_e.NewSize.Width - 20);
            ParentGrid.Height = _e.NewSize.Height;//(_e.NewSize.Height - 40);

            switch (ScreenID)
            {
                case 0:
                    DisplayManager.ResizeScreen_0(new Vector2((float)(ParentGrid.Width - 20), (float)(ParentGrid.Height - 40)), Screen0Data);
                    Console.WriteLine(ParentGrid.Height);
                    break;

                case 1:
                    MapDimensions = DisplayManager.ResizeScreen_1(new Vector2((float)(ParentGrid.Width - 20), (float)(ParentGrid.Height - 40)), Screen1Data);
                    Console.WriteLine(MapDimensions.X + "      " + MapDimensions.Y);
                    RedrawMap();
                    break;

                case 2:
                    DisplayManager.ResizeScreen_2(new Vector2((float)(ParentGrid.Width - 20), (float)(ParentGrid.Height - 40)), Screen2Data);
                    break;

                case 3:
                    DisplayManager.ResizeScreen_3(new Vector2((float)(ParentGrid.Width - 20), (float)(ParentGrid.Height - 40)), Screen3Data);
                    break;

                case 4:
                    DisplayManager.ResizeScreen_4(new Vector2((float)MainWindow.GetWindow(ParentGrid).RenderSize.Width, (float)(ParentGrid.Height - 40)), Screen4Data);
                    break;

                case 25:
                    DisplayManager.ResizeScreen_25(new Vector2((float)MainWindow.GetWindow(ParentGrid).RenderSize.Width, (float)(ParentGrid.Height - 40)), Screen2_5Data);
                    break;

            }
        }

        //Handling the closing event for the window
        public void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }
        
        //Function in charge of handling the mouse click on any button
        public void MouseClickedButton(int _buttonID)
        {
            switch (_buttonID)
            {
                case (int)ProgramButtons.Map:
                    ScreenID = 1;
                    CreateScreenNumber1();
                    break;

                case (int)ProgramButtons.Back:
                    ScreenID = 0;
                    CreateScreenNumber0();
                    break;

                case (int)ProgramButtons.Graphics:
                    ScreenID = 2;
                    CreateScreenNumber2();
                    break;

                case (int)ProgramButtons.Packets:
                    ScreenID = 3;
                    CreateScreenNumber3();
                    break;

                case (int)ProgramButtons.AI:
                    ScreenID = 4;
                    CreateScreenNumber4();
                    break;

                case (int)ProgramButtons.Options:
                    if(ScreenID == 25)
                    {
                        ScreenID = 2;
                        CreateScreenNumber2();
                    }
                    else
                    {
                        ScreenID = 25;
                        CreateScreenNumber2_5();
                    }
                    break;

                case (int)ProgramButtons.LoadPackets:
                    LoadPackets();
                    break;

                case (int)ProgramButtons.SaveAllPackets:
                    SavePackets(true);
                    break;

                case (int)ProgramButtons.SaveSessionPackets:
                    SavePackets(false);
                    break;

                case (int)ProgramButtons.BoxChart:
                    GraphicType = 0;
                    break;

                case (int)ProgramButtons.LineChart:
                    GraphicType = 1;
                    break;

                case (int)ProgramButtons.Apply:
                    DisplayManager.ResizeScreen_25(new Vector2((float)MainWindow.GetWindow(ParentGrid).RenderSize.Width, (float)MainWindow.GetWindow(ParentGrid).RenderSize.Height), Screen2_5Data);
                    break;

                case (int)ProgramButtons.RefreshRealtimeEnabled:
                    Screen2_5Data.RefreshRealtimeCheck.Name = "a" + ((int)ProgramButtons.RefreshRealtimeDisabled).ToString();
                    Screen2_5Data.RefreshRealtimeCheck.Source = new BitmapImage(new Uri(MouseOverButtonsImages[(int)ProgramButtons.RefreshRealtimeDisabled], UriKind.RelativeOrAbsolute));
                    RefreshRealtime = false;
                    break;

                case (int)ProgramButtons.RefreshRealtimeDisabled:
                    Screen2_5Data.RefreshRealtimeCheck.Name = "a" + ((int)ProgramButtons.RefreshRealtimeEnabled).ToString();
                    Screen2_5Data.RefreshRealtimeCheck.Source = new BitmapImage(new Uri(MouseOverButtonsImages[(int)ProgramButtons.RefreshRealtimeEnabled], UriKind.RelativeOrAbsolute));
                    RefreshRealtime = true;
                    break;
            }
        }

        private void DropdownChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox _comboBox = (ComboBox)sender;
            DisplayVariable = _comboBox.SelectedIndex;
        }

        private void SliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider _slider = (Slider)sender;
            Console.WriteLine("Move");
        }
        #endregion

        #region PacketsButtonsClicked

        public void LoadPackets()
        {
            OpenFileDialog = new OpenFileDialog();

            if(OpenFileDialog.ShowDialog() == true)
            {
                PortManager.PacketsLoaded = new Saver().Load(OpenFileDialog.FileName);
                Console.WriteLine(PortManager.PacketsLoaded.Count);
            }
        }

        public void SavePackets(bool _allPackets)
        {
            SaveFileDialog = new SaveFileDialog();
            SaveFileDialog.AddExtension = true;
            SaveFileDialog.CreatePrompt = true;

            if(SaveFileDialog.ShowDialog() == true)
            {
                if (_allPackets)
                {
                    new Saver().Save(PortManager.PacketsLoaded.Select(item => item.Value).ToList(), SaveFileDialog.FileName);
                }
                else
                {
                    new Saver().Save(PortManager.PacketsReceived.Select(item => item.Value).ToList(), SaveFileDialog.FileName);
                }
            }
        }

        #endregion

        //Function in charge of cleaning previous data when swaping screen
        public void ClearScreenData()
        {
            ParentGrid.Children.Clear();

            Screen0Data = null;
            Screen1Data = null;
            Screen2Data = null;
            Screen2_5Data = null;
            Screen3Data = null;
            Screen4Data = null;
        }

        #region Map
        //Function in charge of handling the change in zoom of the map
        private void MapZoomChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider _slider = (Slider)sender;
            CurrentMapZoom = (float)_slider.Value;
            RedrawMap();
        }

        public void RedrawMap()
        {
            if (ScreenID == 1)
            {
                string _newUrl = $"https://api.mapbox.com/styles/v1/mapbox/satellite-streets-v11/static/-3.3501,40.4492,19.5,0/" + MapDimensions.X.ToString() + "x" + MapDimensions.Y.ToString() + "?access_token=pk.eyJ1IjoieWdyYW11bCIsImEiOiJjbGNydnpubWEwY3ZjM3dwYzBybnJxNGVlIn0.mRU-0U8CfKrlGopdGz7gfA";
                Console.WriteLine(_newUrl);
                MapImage = new BitmapImage();
                MapImage.BeginInit();
                MapImage.UriSource = new Uri(_newUrl, UriKind.RelativeOrAbsolute);
                MapImage.DownloadCompleted += ImageDownloadComplete;
                MapImage.EndInit();
                #region before
                //string _newUrl = $"https://api.mapbox.com/styles/v1/mapbox/satellite-streets-v11/static/-3.3501,40.4492,{CurrentMapZoom},0/{MapDimensions.X}x{MapDimensions.Y}?access_token=pk.eyJ1IjoieWdyYW11bCIsImEiOiJjbGNydnpubWEwY3ZjM3dwYzBybnJxNGVlIn0.mRU-0U8CfKrlGopdGz7gfA";
                //string _newUrl = $"https://api.mapbox.com/styles/v1/mapbox/satellite-streets-v11/static/-3.3501,40.4492,19.5,0/" + MapDimensions.X.ToString() + "x" + MapDimensions.Y.ToString() + "?access_token=pk.eyJ1IjoieWdyYW11bCIsImEiOiJjbGNydnpubWEwY3ZjM3dwYzBybnJxNGVlIn0.mRU-0U8CfKrlGopdGz7gfA";
                //Console.WriteLine(_newUrl);
                //MapImage.UriSource = new Uri(_newUrl, UriKind.RelativeOrAbsolute);
                //Screen1Data.Map.Source = new BitmapImage(new Uri(_newUrl, UriKind.RelativeOrAbsolute));
                #endregion
            }
        }

        private void ImageDownloadComplete(object sender, EventArgs e)
        {
            if (ScreenID == 1)
            {
                Screen1Data.Map.Source = MapImage;
            }
        }
        #endregion

        public void ChangeConsoleText(string _newText)
        {
            ConsoleLog += ">>" + _newText + "\n";
            if(Screen3Data != null)
            {
                //Screen3Data.console.Text = _newText;
            }
        }

        #region Comment
        /*/
        public void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Storing the sizes values
            float _screenWidth = (float)e.NewSize.Width * 0.98f;
            float _screenHeight = (float)e.NewSize.Height * 0.98f;

            //calculating the parameters
            float _screenThirds = _screenWidth / 3;
            float _screenHeightThirds = _screenHeight / 3;

            //Setting the console
            Console.Width = _screenThirds;
            Console.Height = _screenHeight;
            Console.Margin = new Thickness(-_screenThirds * 2, 0, 0, 0);

            //Setting the canvas
            MainDisplayer.Width = _screenThirds * 2;
            MainDisplayer.Height = _screenHeightThirds * 2;
            MainDisplayer.Margin = new Thickness(-_screenThirds, -_screenHeightThirds, 0, 0);

            float _start = _screenHeight * 0.8f;
            //Setting the type of graphic radial buttons
            Box_Chart.Margin = new Thickness(-_screenThirds, _start, 0, 0);
            Line_Chart.Margin = new Thickness(-_screenThirds,(_start - 15), 0, 0);
            Pie_Chart.Margin = new Thickness(-_screenThirds, (_start - 30), 0, 0);

            RenderButton_Click(new object(), new RoutedEventArgs());
        }

        public void WriteToConsole(object _message)
        {
            Console.Text += _message.ToString() + "\n";
        }

        private void RenderButton_Click(object sender, RoutedEventArgs e)
        {
            if(Line_Chart.IsChecked == true)
            {
                WriteToConsole("    Line chart chosen");
                LineChartCreator.Paint(MainDisplayer);
            }
            else if(Box_Chart.IsChecked == true)
            {
                WriteToConsole("    Box chart chosen");
                ColumnChartCreator.Paint(MainDisplayer);
            }
            else if(Pie_Chart.IsChecked == true)
            {
                WriteToConsole("    Pie chart chosen");
                PieChartCreator.Paint(MainDisplayer);
            }
        }
        /*/
        #endregion
    }
}
