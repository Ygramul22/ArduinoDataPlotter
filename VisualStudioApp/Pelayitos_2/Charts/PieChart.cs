﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace TestForCansat.Charts
{
    public class PieChart
    {
        /// <summary>
        /// Interaction logic for MainWindow.xaml
        /// </summary>
        public List<Category> Categories { get; set; }
        public TextBox Console;

        public PieChart()
        {
            Categories = new List<Category>()
            {
                #region test #1
                //new Category
                //{
                //    Title = "Category#01",
                //    Percentage = 10,
                //    ColorBrush = Brushes.Gold,
                //},

                //new Category
                //{
                //    Title = "Category#02",
                //    Percentage = 30,
                //    ColorBrush = Brushes.Pink,
                //},

                //new Category
                //{
                //    Title = "Category#03",
                //    Percentage = 60,
                //    ColorBrush = Brushes.CadetBlue,
                //}, 
                #endregion

                #region test #2
                //new Category
                //{
                //    Title = "Category#01",
                //    Percentage = 20,
                //    ColorBrush = Brushes.Gold,
                //},

                //new Category
                //{
                //    Title = "Category#02",
                //    Percentage = 80,
                //    ColorBrush = Brushes.LightBlue,
                //}, 
                #endregion

                #region test #3
                //new Category
                //{
                //    Title = "Category#01",
                //    Percentage = 50,
                //    ColorBrush = Brushes.Gold,
                //},

                //new Category
                //{
                //    Title = "Category#02",
                //    Percentage = 50,
                //    ColorBrush = Brushes.LightBlue,
                //}, 
                #endregion

                #region test #4
                //new Category
                //{
                //    Title = "Category#01",
                //    Percentage = 30,
                //    ColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4472C4")),
                //},

                //new Category
                //{
                //    Title = "Category#02",
                //    Percentage = 30,
                //    ColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ED7D31")),
                //},

                //new Category
                //{
                //    Title = "Category#03",
                //    Percentage = 20,
                //    ColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC000")),
                //},

                //new Category
                //{
                //    Title = "Category#04",
                //    Percentage = 20,
                //    ColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5B9BD5")),
                //},

                //new Category
                //{
                //    Title = "Category#05",
                //    Percentage = 10,
                //    ColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A5A5A5")),
                //}, 
                #endregion

                #region test #5
                //new Category
                //{
                //    Title = "Category#01",
                //    Percentage = 20,
                //    ColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4472C4")),
                //},

                //new Category
                //{
                //    Title = "Category#02",
                //    Percentage = 30,
                //    ColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ED7D31")),
                //},

                //new Category
                //{
                //    Title = "Category#03",
                //    Percentage = 20,
                //    ColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC000")),
                //},

                //new Category
                //{
                //    Title = "Category#04",
                //    Percentage = 20,
                //    ColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5B9BD5")),
                //},

                //new Category
                //{
                //    Title = "Category#05",
                //    Percentage = 10,
                //    ColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A5A5A5")),
                //}, 
                #endregion

                #region test #6
                new Category
                {
                    Title = "Category#01",
                    Percentage = 20,
                    ColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4472C4")),
                },

                new Category
                {
                    Title = "Category#02",
                    Percentage = 60,
                    ColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ED7D31")),
                },

                new Category
                {
                    Title = "Category#03",
                    Percentage = 5,
                    ColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC000")),
                },

                new Category
                {
                    Title = "Category#04",
                    Percentage = 10,
                    ColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5B9BD5")),
                },

                new Category
                {
                    Title = "Category#05",
                    Percentage = 5,
                    ColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A5A5A5")),
                }, 
                #endregion
            };
        }

        public void Paint(Canvas mainCanvas)//, float radius, float centerX, float centerY)
        {
            //Clearing the children, in order to paint on a new canvas
            mainCanvas.Children.Clear();

            //Delcaring the width and height of the canvas
            float pieWidth;
            float pieHeight;

            //Checking if the canvas is measurable
            if(mainCanvas.Width > 0 && mainCanvas.Height > 0)
            {
                //Checking if we should use the width or height for the circle size
                if (mainCanvas.Width > mainCanvas.Height)
                {
                    //Using the height
                    pieWidth = (float)mainCanvas.Height;
                    pieHeight = (float)mainCanvas.Height;
                }
                else
                {
                    //Using the width
                    pieWidth = (float)mainCanvas.Width;
                    pieHeight = (float)mainCanvas.Width;
                }
            }
            else
            {
                //As canvas is not measurable, using default values in order to avoid error
                pieWidth = 600;
                pieHeight = 600;
            }

            float startMargin = ((float)mainCanvas.Width - pieWidth) / 2;
            WriteToConsole(startMargin + "    " + (pieWidth / 2) + startMargin);
            //Declaring more necessary values
            float centerX = (pieWidth / 2);// + startMargin;
            float centerY = (pieHeight / 2);
            float radius = pieWidth / 2;

            // draw pie
            float angle = 0, prevAngle = 0;
            foreach (var category in Categories)
            {
                double line1X = (radius * Math.Cos(angle * Math.PI / 180)) + centerX;
                double line1Y = (radius * Math.Sin(angle * Math.PI / 180)) + centerY;

                angle = category.Percentage * (float)360 / 100 + prevAngle;
                Debug.WriteLine(angle);

                double arcX = (radius * Math.Cos(angle * Math.PI / 180)) + centerX;
                double arcY = (radius * Math.Sin(angle * Math.PI / 180)) + centerY;

                var line1Segment = new LineSegment(new Point(line1X, line1Y), false);
                double arcWidth = radius, arcHeight = radius;
                bool isLargeArc = category.Percentage > 50;
                var arcSegment = new ArcSegment()
                {
                    Size = new Size(arcWidth, arcHeight),
                    Point = new Point(arcX, arcY),
                    SweepDirection = SweepDirection.Clockwise,
                    IsLargeArc = isLargeArc,
                };
                var line2Segment = new LineSegment(new Point(centerX, centerY), false);

                var pathFigure = new PathFigure(
                    new Point(centerX, centerY),
                    new List<PathSegment>()
                    {
                    line1Segment,
                    arcSegment,
                    line2Segment,
                    },
                    true);

                var pathFigures = new List<PathFigure>() { pathFigure, };
                var pathGeometry = new PathGeometry(pathFigures);
                var path = new Path()
                {
                    Fill = category.ColorBrush,
                    Data = pathGeometry,
                };
                mainCanvas.Children.Add(path);

                prevAngle = angle;


                // draw outlines
                var outline1 = new Line()
                {
                    X1 = centerX,
                    Y1 = centerY,
                    X2 = line1Segment.Point.X,
                    Y2 = line1Segment.Point.Y,
                    Stroke = Brushes.White,
                    StrokeThickness = 5,
                };
                var outline2 = new Line()
                {
                    X1 = centerX,
                    Y1 = centerY,
                    X2 = arcSegment.Point.X,
                    Y2 = arcSegment.Point.Y,
                    Stroke = Brushes.White,
                    StrokeThickness = 5,
                };

                mainCanvas.Children.Add(outline1);
                mainCanvas.Children.Add(outline2);

                //detailsItemsControl.ItemsSource = Categories;
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

        /*/
        public MainWindow()
        {
            InitializeComponent();
        
            float pieWidth = 650, pieHeight = 650, centerX = pieWidth / 2, centerY = pieHeight / 2, radius = pieWidth / 2;
            mainCanvas.Width = pieWidth;
            mainCanvas.Height = pieHeight;

            

            detailsItemsControl.ItemsSource = Categories;
        
            
        /*/
    }
    public class Category
    {
        public float Percentage { get; set; }
        public string Title { get; set; }
        public Brush ColorBrush { get; set; }
    }
}
