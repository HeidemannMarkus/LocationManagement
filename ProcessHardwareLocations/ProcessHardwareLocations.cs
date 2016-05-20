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
       private Dictionary<Guid, IHardware> HardWareList { get; set; }
       public IResult CaptureHardware(IHardware newHardware)
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

       public List<IHardware> GetHardware(string buildingName, string roomName)
       {
          throw new NotImplementedException();
       }

       public List<IHardware> GetHardware()
       {
          return HardWareList.Values.ToList();
       }

       public IResult LoadHardware(string filepath)
       {
         var resultModel = new Result();
         var loaded_Hardware = new Dictionary<Guid, IHardware>();
         switch (Path.GetExtension(filepath))
         {
                case ".dat":
                    loaded_Hardware = new BinaryParser().FromFile<Dictionary<Guid, IHardware>>(filepath);
                    foreach (var hardware in loaded_Hardware)
                    {
                        if (!HardWareList.ContainsKey(hardware.Key))
                        {
                            HardWareList.Add(hardware.Key, hardware.Value);
                        }
                        else
                        {
                            if (HardWareList[hardware.Key] != hardware.Value)
                            {
                                resultModel.HasError = true;
                                //TODO: ErrorMessage durch Liste ersetzen, damit n Fehlermeldungen zurückgegeben werden können
                                resultModel.ErrorMessage += $",{hardware.Key} ist schon vorhanden";
                            }
                        }
                    }
                    break;
                case ".xml":
               new XmlParser().FromFile<Dictionary<Guid, IHardware>>(filepath);
               break;
            case ".json":
               new JsonParser().FromFile<Dictionary<Guid, IHardware>>(filepath);
               break;
            case ".csv":
               new CsvParser().FromFile<Dictionary<Guid, IHardware>>(filepath);
               break;
            default:
               resultModel.HasError = true;
               resultModel.ErrorMessage ="Format Unbekannt!!!!";
               break;
         }
          return resultModel;
       }

       public IResult UpdateHardware(IHardware updatedHardware)
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
          throw new NotImplementedException();
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
    }
}
