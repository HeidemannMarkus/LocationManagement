using Xunit;
using System;
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

      [Fact]
      public void SaveHardware_WhenListIsEmpty_ShouldThrowError()
      {
         Assert.Equal(
            new Result
            {
               HasError = true,
               ErrorMessage = "Leere Liste kann nicht gespeichert werden!"
            },
            new ProcessHardwareLocations()
            .SaveHardware(@"C:\test.json"));
      }
   }
}