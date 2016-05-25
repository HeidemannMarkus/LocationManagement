using System;

namespace ProcessHardwareLocations
{
   public interface IResult : IEquatable<IResult>
   {
      bool HasError { get; set; }
      string ErrorMessage { get; set; }

   }
}
