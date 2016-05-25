using Microsoft.Win32;
using ProcessHardwareLocations;
using ProcessHardwareLocations.Data;
using System;
using System.Collections.Generic;
using System.Windows;

namespace WpfApplication1
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      private List<Hardware> _hardwareList = new List<Hardware>();
      private IProcessHardwareLocations _processHardware;
      private Hardware _selected;

      public MainWindow()
      {
         InitializeComponent();
         TBName.Focus();
         DatePicker.SelectedDate = DateTime.Today;

         initCB();

         listViewHardware.ItemsSource = this._hardwareList;
         _processHardware = new ProcessHardwareLocations.ProcessHardwareLocations();
      }

      private void BtnEdit_Click(object sender, RoutedEventArgs e)
      {
         if (inputHasValues())
         {
            if (_selected != null)
            {
               var temp = new Hardware
               {
                  HardwareType = TBArt.Text,
                  HardwareName = TBName.Text,
                  BuildingName = TBBuilding.Text,
                  RoomName = TBRoom.Text,
                  DateOfFirstUsage = DatePicker.SelectedDate,
                  Id = _selected.Id
               };
               hasError(_processHardware.UpdateHardware(temp));
            }
            else
            {
               MessageBox.Show("Keine Hardware zum Updaten ausgewählt.", "Fehler", MessageBoxButton.OK);
            }
         }
         TBName.Focus();
      }

      private void BtnDelete_Click(object sender, RoutedEventArgs e)
      {
         if (_selected != null)
         {
            hasError(_processHardware.DeleteHardware(_selected.Id));
         }
         else
         {
            MessageBox.Show("Keine Hardware zum Löschen ausgewählt.", "Fehler", MessageBoxButton.OK);
         }
         TBName.Focus();
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
               DateOfFirstUsage = DatePicker.SelectedDate
            };

            if (!hasError(_processHardware.CaptureHardware(temp)))
            {
               TBName.Clear();
               TBArt.Clear();
               TBBuilding.Clear();
               TBRoom.Clear();
               DatePicker.SelectedDate = DateTime.Today;
            }
         }
         updateListView();
         TBName.Focus();
      }

      private void BtnSave_Click(object sender, RoutedEventArgs e)
      {
         SaveFileDialog sfd = new SaveFileDialog();
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

      private void listViewHardware_SelectionChanged_1(object sender,
         System.Windows.Controls.SelectionChangedEventArgs e)
      {
         _selected = (Hardware) listViewHardware.SelectedItem;
         if (_selected != null)
         {
            TBName.Text = _selected.HardwareName;
            TBArt.Text = _selected.HardwareType;
            TBBuilding.Text = _selected.BuildingName;
            TBRoom.Text = _selected.RoomName;
            DatePicker.SelectedDate = _selected.DateOfFirstUsage;
         }
      }

      private void updateListView()
      {
         initCB();

         foreach (string room in _processHardware.GetRooms())
         {
            CBroom.Items.Add(room);
         }

         foreach (string building in _processHardware.GetBuildings())
         {
            CBbuilding.Items.Add(building);
         }

         listViewHardware.ItemsSource = _processHardware.GetHardware();
         listViewHardware.Items.Refresh();
      }

      private void BtnFilter_Click(object sender, RoutedEventArgs e)
      {
         string room = CBroom.SelectedValue.ToString();
         string building = CBbuilding.SelectedValue.ToString();

         if (building == "Alle")
            building = null;

         if (room == "Alle")
            room = null;

         listViewHardware.ItemsSource = _processHardware.GetHardware(building, room);
         listViewHardware.Items.Refresh();
      }

      private void BtnResetFilter_Click(object sender, RoutedEventArgs e)
      {
         updateListView();
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

      private void initCB()
      {
         CBbuilding.Items.Clear();
         CBroom.Items.Clear();
         CBroom.Items.Add("Alle");
         CBroom.SelectedItem = "Alle";
         CBbuilding.Items.Add("Alle");
         CBbuilding.SelectedItem = "Alle";
      }
   }
}
