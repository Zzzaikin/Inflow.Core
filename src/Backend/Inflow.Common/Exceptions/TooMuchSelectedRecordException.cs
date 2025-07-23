namespace Inflow.Common.Exceptions
{
    public class TooMuchSelectedRecordException : ArgumentException 
    {
        public TooMuchSelectedRecordException(string message) : base(message) { }
    }
}
