using Xunit;
using ProcessHardwareLocations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcessHardwareLocations.Data;

namespace ProcessHardwareLocations.Tests
{
   public class ProcessHardwareLocationsTests
   {
      [Fact()]
      public void CaptureHardware_WithGuid_ShouldReturnError()
      {
         Assert.Equal(new Result
         {
            ErrorMessage = "Id wird intern gesetzt! Feld muss Guid.Empty enthalten",
            HasError = true
         }
            , new ProcessHardwareLocations()
            .CaptureHardware(
               new Hardware
               {
                  Id = Guid.NewGuid()
               }));
      }
   }
}