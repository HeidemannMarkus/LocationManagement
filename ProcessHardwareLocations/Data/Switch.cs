using System;

namespace ProcessHardwareLocations.Data
{
   public class Switch : IHardware
   {
       public Guid Id { get; set; }
       public string BuildingName { get; set; }
       public string RoomName { get; set; }
       public string HardwareType { get; set; }
       public string HardwareName { get; set; }
   }
}
