using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProcessHardwareLocations;
using ProcessHardwareLocations.Data;


namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IProcessHardwareLocations _processHardware;
        public MainWindow()
        {
            _processHardware = new ProcessHardwareLocations.ProcessHardwareLocations();
            InitializeComponent();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var temp = new Hardware
            {
                HardwareType = TBArt.Text,
                HardwareName = TBName.Text,
                BuildingName = TBBuilding.Text,
                RoomName = TBRoom.Text
            };
            _processHardware.CaptureHardware(temp);
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnRead_Click(object sender, RoutedEventArgs e)
        {
            listViewHardware.DataContext = _processHardware.GetHardware();
            listViewHardware.Items.Refresh();
        }
    }
}
