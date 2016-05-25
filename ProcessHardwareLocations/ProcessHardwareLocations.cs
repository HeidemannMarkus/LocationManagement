using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ParserLibrary;
using ProcessHardwareLocations.Data;

namespace ProcessHardwareLocations
{
   public class ProcessHardwareLocations : IProcessHardwareLocations
   {
      private Dictionary<Guid, Hardware> HardWareList { get; set; }

      public ProcessHardwareLocations()
      {
         HardWareList = new Dictionary<Guid, Hardware>();
      }

      public IResult CaptureHardware(Hardware newHardware)
      {
         var returnModel = new Result();
         if (newHardware.Id == Guid.Empty)
         {
            newHardware.Id = Guid.NewGuid();
         }
         else
         {
            returnModel.HasError = true;
            returnModel.ErrorMessage = "Id wird intern gesetzt! Feld muss Guid.Empty enthalten";
         }
         HardWareList.Add(newHardware.Id, newHardware);

         return returnModel;
      }

      public List<Hardware> GetHardware(string buildingName, string roomName)
      {
         return HardWareList.Values.Where(query =>
            (buildingName == null || query.BuildingName == buildingName)
            &&
            (roomName == null || query.RoomName == roomName)
            ).ToList();
      }

      public List<Hardware> GetHardware()
      {
         return HardWareList.Values.ToList();
      }

      public IResult LoadHardware(string filepath)
      {
         var resultModel = new Result();
         var loaded_Hardware = new List<Hardware>();
         switch (Path.GetExtension(filepath))
         {
            case ".dat":
               loaded_Hardware = new BinaryParser().FromFile<List<Hardware>>(filepath);
               break;
            case ".xml":
               loaded_Hardware = new XmlParser().FromFile<List<Hardware>>(filepath);
               break;
            case ".json":
               loaded_Hardware = new JsonParser().FromFile<List<Hardware>>(filepath);
               break;
            case ".csv":
               loaded_Hardware = new CsvParser().FromFile<List<Hardware>>(filepath).Single();
               break;
            default:
               resultModel.HasError = true;
               resultModel.ErrorMessage = "Format Unbekannt!!!!";
               break;
         }
         foreach (var hardware in loaded_Hardware)
         {
            if (!HardWareList.ContainsKey(hardware.Id))
            {
               HardWareList.Add(hardware.Id, hardware);
            }
            else
            {
               if (HardWareList[hardware.Id] != hardware)
               {
                  resultModel.HasError = true;
                  //TODO: ErrorMessage durch Liste ersetzen, damit n Fehlermeldungen zurückgegeben werden können
                  resultModel.ErrorMessage += $",{hardware.Id} ist schon vorhanden";
               }
            }
         }
         return resultModel;
      }

      public IResult UpdateHardware(Hardware updatedHardware)
      {
         var resultModel = new Result();

         if (HardWareList[updatedHardware.Id] == null)
         {
            resultModel.HasError = true;
            resultModel.ErrorMessage = "hardware nicht gefunden";
            return resultModel;
         }

         HardWareList[updatedHardware.Id] = updatedHardware;
         return resultModel;
      }

      public IResult SaveHardware(string filepath)
      {
         var resultModel = new Result();

         if (HardWareList.Count == 0)
         {
            return new Result
            {
               HasError = true,
               ErrorMessage = "Leere Liste kann nicht gespeichert werden!"
            };
         }
         
         List<Hardware> listToWrite = HardWareList.Values.ToList();
         switch (Path.GetExtension(filepath))
         {
            case ".dat":
               new BinaryParser().ToFile(filepath, listToWrite);
               break;
            case ".xml":
               new XmlParser().ToFile(filepath, listToWrite);
               break;
            case ".json":
               new JsonParser().ToFile(filepath, listToWrite);
               break;
            /*case ".csv":
                    new CsvParser().ToFile(filepath, HardWareList.ToList());
                    break;*/
            default:
               resultModel.HasError = true;
               resultModel.ErrorMessage = $"format {Path.GetExtension(filepath)} wird nicht unterstützt";
               break;
         }
         return resultModel;
      }

      public IResult DeleteHardware(Guid hardware_ID)
      {
         var resultModel = new Result();
         if (!HardWareList.Remove(hardware_ID))
         {
            resultModel.HasError = true;
            resultModel.ErrorMessage = "Entfernen Fehlgeschlagen";
         }

         return resultModel;
      }

      public List<string> GetBuildings()
      {
         return HardWareList.Values
            .OrderBy(model => model.BuildingName)
            .Select(model => model.BuildingName)
            .Distinct()
            .ToList();
      }

      public List<string> GetRooms()
      {
         return HardWareList.Values
            .OrderBy(model => model.RoomName)
            .Select(model => model.RoomName)
            .Distinct()
            .ToList();
      }
   }
}
