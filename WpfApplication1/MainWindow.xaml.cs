using Microsoft.Win32;
using ProcessHardwareLocations;
using ProcessHardwareLocations.Data;
using System.Windows;
using System;
using System.Globalization;

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
            InitializeComponent();
            TBName.Focus();
            DatePicker.SelectedDate = DateTime.Today;
            listViewHardware.ItemsSource = this._hardwareList;
            _processHardware = new ProcessHardwareLocations.ProcessHardwareLocations();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (inputHasValues())
            {
                var temp = new Hardware
                {
                    HardwareType = TBArt.Text,
                    HardwareName = TBName.Text,
                    BuildingName = TBBuilding.Text,
                    RoomName = TBRoom.Text,
                    DateOfFirstUsage = DatePicker.SelectedDate.Value.ToString().Split(' ')[0]
                };
                hasError(_processHardware.UpdateHardware(temp));
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            hasError(_processHardware.DeleteHardware(_selected.Id));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (inputHasValues())
            {
                var temp = new Hardware
                {
                    HardwareType = TBArt.Text,
                    HardwareName = TBName.Text,
                    BuildingName = TBBuilding.Text,
                    RoomName = TBRoom.Text,
                    DateOfFirstUsage = DatePicker.SelectedDate.Value.ToString().Split(' ')[0]
                };

                if (!hasError(_processHardware.CaptureHardware(temp)))
                {
                    TBName.Clear();
                    TBArt.Clear();
                    TBBuilding.Clear();
                    TBRoom.Clear();
                    DatePicker.SelectedDate = DateTime.Today;

                    TBName.Focus();
                }
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
           // sfd.DefaultExt = ".xml";
            sfd.Filter = "Loading files (*.dat;*.xml;*.json)| *.dat; *.xml; *.json";
            if (sfd.ShowDialog() == true)
            {
                hasError(_processHardware.SaveHardware(sfd.FileName));
            }
            TBName.Focus();
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Loading files (*.dat;*.xml;*.csv;*.json)| *.dat; *.xml; *.csv; *.json";
            if (ofd.ShowDialog() == true)
            {
                hasError(_processHardware.LoadHardware(ofd.FileName));
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
            DatePicker.SelectedDate = DateTime.ParseExact(_selected.DateOfFirstUsage, "dd.MM.yyyy", CultureInfo.InvariantCulture);
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
            _hardwareList = (HardwareList)_processHardware.GetHardware();
            CBroom.Items.Clear();
            CBbuilding.Items.Clear();
            foreach (Hardware hardware in _hardwareList)
            {
                String room = hardware.RoomName;
                String building = hardware.BuildingName;
                if (!CBroom.Items.Contains(room))
                {
                    CBroom.Items.Add(room);
                }

                if (!CBbuilding.Items.Contains(building))
                {
                    CBbuilding.Items.Add(building);
                }
            }
            listViewHardware.ItemsSource = this._hardwareList;
            listViewHardware.Items.Refresh();
        }

        private void getHardwareWithFilter()
        {
            listViewHardware.ItemsSource = _processHardware.GetHardware(CBbuilding.SelectedItem.ToString(), CBroom.SelectedItem.ToString());
            listViewHardware.Items.Refresh();
        }

        private bool hasError(IResult iresult)
        {
            if (iresult.HasError)
            {
                MessageBox.Show(iresult.ErrorMessage, "Fehler", MessageBoxButton.OK);
                return true;
            }
            else
            {
                updateListView();
                return false;
            }
           
        }

        private bool inputHasValues()
        {
            if (TBName.Text == "" || TBArt.Text == "" || TBBuilding.Text == "" || TBRoom.Text == "")
            {
                MessageBox.Show("Fehlende Angaben, füllen Sie alle Felder aus.", "Fehler", MessageBoxButton.OK);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
