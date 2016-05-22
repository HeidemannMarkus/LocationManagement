using System;

namespace ProcessHardwareLocations.Data
{
   public interface IHardware
   {
        Guid Id { get; set; }
        string BuildingName { get; set; }
        string RoomName { get; set; }
        string HardwareType { get; set; }
        string HardwareName { get; set; }
   }
}
