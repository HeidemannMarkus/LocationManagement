namespace ProcessHardwareLocations
{
   public interface IResult
   {
      bool HasError { get; set; }
      string ErrorMessage { get; set; }

   }
}
