namespace ProcessHardwareLocations
{
   public class Result : IResult
   {
      public bool HasError { get; set; }
      public string ErrorMessage { get; set; }
   }
}
