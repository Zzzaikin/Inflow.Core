namespace Inflow.Core.Common.Exceptions;

public class TooMuchSelectedRecordException(string message) : ArgumentException(message);