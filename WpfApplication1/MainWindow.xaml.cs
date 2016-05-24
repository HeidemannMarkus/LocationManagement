using Microsoft.Win32;
using ProcessHardwareLocations;
using ProcessHardwareLocations.Data;
using System.Windows;
using System;


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
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var temp = new Hardware
            {
                HardwareType = TBArt.Text,
                HardwareName = TBName.Text,
                BuildingName = TBBuilding.Text,
                RoomName = TBRoom.Text,
                DateOfFirstUsage = DatePicker.SelectedDate
            };
            checkError(_processHardware.UpdateHardware(temp));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            checkError(_processHardware.DeleteHardware(_selected.Id));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var temp = new Hardware
            {
                HardwareType = TBArt.Text,
                HardwareName = TBName.Text,
                BuildingName = TBBuilding.Text,
                RoomName = TBRoom.Text,
                DateOfFirstUsage = DatePicker.SelectedDate
            };
            IResult iresult = _processHardware.CaptureHardware(temp);
            if (iresult.HasError)
            {
                MessageBox.Show("Fehler", iresult.ErrorMessage, MessageBoxButton.OK);
            }
            else
            {
                updateListView();
                TBName.Clear();
                TBArt.Clear();
                TBBuilding.Clear();
                TBRoom.Clear();
                DatePicker.SelectedDate = DateTime.Today;

                TBName.Focus();
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            IResult iresult = null;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".xml";
            sfd.Filter = "Loading files| *.dat; *.xml; *.json";
            if (sfd.ShowDialog() == true)
            {
                checkError(_processHardware.SaveHardware(sfd.FileName));
            }
            TBName.Focus();
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            IResult iresult = null;
            OpenFileDialog ofd = new OpenFileDialog();
            // TODO Dateifilter
            ofd.Filter = "Loading files| *.dat; *.xml; *.csv; *.json";
            if (ofd.ShowDialog() == true)
            {
                checkError(_processHardware.LoadHardware(ofd.FileName));
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
            DatePicker.SelectedDate = _selected.DateOfFirstUsage;
        }

        private void CBbuilding_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            getHardwareWithFilter();
        }

        private void CBroom_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            getHardwareWithFilter();
        }

        private void updateListView()
        {
            listViewHardware.ItemsSource = _processHardware.GetHardware();
            listViewHardware.Items.Refresh();
        }

        private void getHardwareWithFilter()
        {
            listViewHardware.ItemsSource = _processHardware.GetHardware(CBbuilding.SelectedItem.ToString(), CBroom.SelectedItem.ToString());
            listViewHardware.Items.Refresh();
        }

        private void checkError(IResult iresult)
        {
            if (iresult.HasError)
            {
                MessageBox.Show("Fehler", iresult.ErrorMessage, MessageBoxButton.OK);
            }
            else
            {
                updateListView();
            }
           
        }
    }
}
