using System;
using System.Runtime.Serialization;

namespace ProcessHardwareLocations.Data
{
   [DataContract]
   public class Hardware
   {
      [DataMember]
      public Guid Id { get; set; }

      [DataMember]
      public string BuildingName { get; set; }

      [DataMember]
      public string RoomName { get; set; }

      [DataMember]
      public string HardwareType { get; set; }

      [DataMember]
      public string HardwareName { get; set; }

      [DataMember]
      public DateTime? DateOfFirstUsage { get; set; }
   }
}
