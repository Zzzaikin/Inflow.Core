namespace Inflow.Common
{
    public static class Argument
    {
        public static void IsNotNullOrEmpty(string argumentValue, string argumentName)
        {//TODO: Test thread culture info.
            if (string.IsNullOrEmpty(argumentValue))
            {
                var exceptionMessage = string.Format(GlobalResource.ArgumentCanNotBeNullOrEmpty, argumentName);
                throw new ArgumentException(exceptionMessage);
            }
        }

        public static void IsNotNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
            {
                var exceptionMessage = string.Format(GlobalResource.ArgumentCanNotBeNull, argumentName);
                throw new ArgumentException(exceptionMessage);
            }
        }
    }
}
