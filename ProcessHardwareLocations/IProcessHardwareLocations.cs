using System;
using System.Collections.Generic;
using ProcessHardwareLocations.Data;

namespace ProcessHardwareLocations
{
   public interface IProcessHardwareLocations
   {
      IResult CaptureHardware(IHardware newHardware);
      List<IProcessHardwareLocations> GetHardware(string buildingName, string roomName);
      List<IProcessHardwareLocations> GetHardware();
      IResult LoadHardware(string filepath);
      IResult UpdateHardware(IHardware updatedHardware);
      IResult SaveHardware(string filepath);
      IResult DeleteHardware(Guid hardware_ID);

   }
}
