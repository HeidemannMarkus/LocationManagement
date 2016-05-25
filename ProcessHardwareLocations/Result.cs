namespace ProcessHardwareLocations
{
   public class Result : IResult
   {
      public bool HasError { get; set; }
      public string ErrorMessage { get; set; }
      public bool Equals(IResult other)
      {
         return HasError == other.HasError && ErrorMessage == other.ErrorMessage;
      }
   }
}
