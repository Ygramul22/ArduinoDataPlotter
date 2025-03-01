using Pelayitos_2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using TestForCansat.Charts;
using TestForCansat.RadioSystem;

namespace TestForCansat.ScreenUtilities
{
    public class Screen0
    {
        public Image MapButton;
        public Image GraphicsButton;
        public Image PacketsButton;
        public Image AIButton;
    }

    public class Screen1
    {
        public Image BackButton;
        public Image Map;
        public Rectangle TopRectangle;
    }

    public class Screen2
    {
        public bool RefreshRealtime;

        public Image BackButton;
        public Image ExportButton;
        public Image ConfigurationButton;
        public Canvas Canvas;
        public Rectangle TopBarRectangle;
        public TextBox GraphicsText;
        public ScrollViewer GraphicsTextScroller;
    }

    public class Screen2_5
    {
        public bool RefreshRealtime;

        public Rectangle TopBarRectangle;
        public Image BackButton;
        public TextBox GraphicsText;
        public ScrollViewer GraphicsTextScroller;
        public Image ConfigurationButton;
        public Image ExportButton;
        public Canvas Canvas;
        public Rectangle PanelBackground;
        public TextBox SettingsText;
        public ScrollViewer SettingsTextScroller;
        public Image ColumnsChartButton;
        public Image LineChartButton;
        public Image RefreshRealtimeCheck;
        public Image Apply;
        public ComboBox AxisDropdown;
        public Slider StartPacket;
        public Slider EndPacket;
    }

    public class Screen3
    {
        public Screen3()
        {
            //PrintToConsole("Debug Test");
            //PrintToConsole(MainWindow.Instance.ConsoleLog);
        }

        public Image BackButton;
        public Rectangle TopBar;
        public TextBlock ScreenTitle;
        public TextBox console;
        public ScrollViewer ConsoleScroller;
        public Image LoadPackets;
        public Image SaveSessionPackets;
        public Image SaveAllPackets;

        public void PrintToConsole(object _msg)
        {
            string msg = _msg.ToString();
            if (console != null)
            {
                console.Text += ">> " + msg;
                Console.WriteLine("Written");
            }
        }

        public void RefreshConsole()
        {
            if(console != null)
            {
                console.Text = ">> " + MainWindow.Instance.ConsoleLog;
            }
        }
    }

    public class Screen4
    {
        public Image BackButton;
        public Rectangle TopBar;
    }
}
