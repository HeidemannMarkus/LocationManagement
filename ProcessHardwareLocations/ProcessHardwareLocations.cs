using System;
using System.Collections.Generic;
using ProcessHardwareLocations.Data;

namespace ProcessHardwareLocations
{
    public class ProcessHardwareLocations : IProcessHardwareLocations
    {
       private List<IHardware> HardWareList { get; set; }
       public IResult CaptureHardware(IHardware newHardware)
       {
         var returnModel = new Result{};
          if (newHardware.Id == Guid.Empty)
          {
             newHardware.Id = Guid.NewGuid();
          }
          else
          {
             returnModel.HasError = true;
             returnModel.ErrorMessage = "Id wird intern gesetzt! Feld muss Guid.Empty enthalten";
          }
          HardWareList.Add(newHardware);

          return returnModel;
       }

       public List<IProcessHardwareLocations> GetHardware(string buildingName, string roomName)
       {
          
       }

       public List<IProcessHardwareLocations> GetHardware()
       {
          throw new NotImplementedException();
       }

       public IResult LoadHardware(string filepath)
       {
          throw new NotImplementedException();
       }

       public IResult UpdateHardware(IHardware updatedHardware)
       {
          throw new NotImplementedException();
       }

       public IResult SaveHardware(string filepath)
       {
          throw new NotImplementedException();
       }

       public IResult DeleteHardware(Guid hardware_ID)
       {
          throw new NotImplementedException();
       }
    }
}
