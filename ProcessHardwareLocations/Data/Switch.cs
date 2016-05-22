using System;

namespace ProcessHardwareLocations.Data
{
   public class Switch : IHardware
   {
       public Guid Id { get; set; }
       public string BuildingName { get; set; }
       public string RoomName { get; set; }
   }
}
