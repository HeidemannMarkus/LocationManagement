using System;
using System.Collections.Generic;
using ProcessHardwareLocations.Data;

namespace ProcessHardwareLocations
{
   public interface IProcessHardwareLocations
   {
      IResult CaptureHardware(Hardware newHardware);
      List<Hardware> GetHardware(string buildingName, string roomName);
      List<Hardware> GetHardware();
      IResult LoadHardware(string filepath);
      IResult UpdateHardware(Hardware updatedHardware);
      IResult SaveHardware(string filepath);
      IResult DeleteHardware(Guid hardware_ID);

   }
}
