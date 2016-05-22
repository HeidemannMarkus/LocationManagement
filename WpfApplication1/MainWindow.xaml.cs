using Microsoft.Win32;
using ProcessHardwareLocations;
using ProcessHardwareLocations.Data;
using System.Windows;


namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HardwareList _hardwareList = new HardwareList();
        private IProcessHardwareLocations _processHardware;
        private Hardware _selected;

        public MainWindow()
        {
            _processHardware = new ProcessHardwareLocations.ProcessHardwareLocations();
            InitializeComponent();
            // listViewHardware.Items[0].Selected = true;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            _processHardware.DeleteHardware(_selected.Id);

            var temp = new Hardware
            {
                HardwareType = TBArt.Text,
                HardwareName = TBName.Text,
                BuildingName = TBBuilding.Text,
                RoomName = TBRoom.Text
            };
            _processHardware.CaptureHardware(temp);
            listViewHardware.ItemsSource = _processHardware.GetHardware();
            listViewHardware.Items.Refresh();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            _processHardware.DeleteHardware(_selected.Id);
            listViewHardware.ItemsSource = _processHardware.GetHardware();
            listViewHardware.Items.Refresh();
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
            listViewHardware.ItemsSource = _processHardware.GetHardware();
            listViewHardware.Items.Refresh();

            TBName.Clear();
            TBArt.Clear();
            TBBuilding.Clear();
            TBRoom.Clear();

            TBName.Focus();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".xml";
            if (sfd.ShowDialog() == true)
            {
                _processHardware.SaveHardware(sfd.FileName);
            }
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            //ofd.Filter = "|*.dat;*.xml;*.csv;*.json;";
            if (ofd.ShowDialog() == true)
            {
                listViewHardware.ItemsSource = (HardwareList) _processHardware.LoadHardware(ofd.FileName);
                listViewHardware.Items.Refresh();
            }
            TBName.Focus();
        }
        
        private void listViewHardware_SelectionChanged_1(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _selected = (Hardware) listViewHardware.SelectedItem;
            TBName.Text = _selected.HardwareName;
            TBArt.Text = _selected.HardwareType;
            TBBuilding.Text = _selected.BuildingName;
            TBRoom.Text = _selected.RoomName;
        }

        private void CBbuilding_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            listViewHardware.ItemsSource = _processHardware.GetHardware(CBbuilding.SelectedItem.ToString(), CBroom.SelectedItem.ToString());
            listViewHardware.Items.Refresh();
        }

        private void CBroom_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            listViewHardware.ItemsSource = _processHardware.GetHardware(CBbuilding.SelectedItem.ToString(), CBroom.SelectedItem.ToString());
            listViewHardware.Items.Refresh();
        }
    }
}
