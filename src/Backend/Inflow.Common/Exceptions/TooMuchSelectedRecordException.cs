namespace Inflow.Common.Exceptions;

public class TooMuchSelectedRecordException(string message) : ArgumentException(message);