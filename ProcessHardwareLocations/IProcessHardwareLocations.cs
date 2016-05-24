using System;
using System.Collections.Generic;
using ProcessHardwareLocations.Data;

namespace ProcessHardwareLocations
{
   public interface IProcessHardwareLocations
   {
      IResult CaptureHardware(IHardware newHardware);
      HardwareList GetHardware(string buildingName, string roomName);
      HardwareList GetHardware();
      IResult LoadHardware(string filepath);
      IResult UpdateHardware(IHardware updatedHardware);
      IResult SaveHardware(string filepath);
      IResult DeleteHardware(Guid hardware_ID);

   }
}
