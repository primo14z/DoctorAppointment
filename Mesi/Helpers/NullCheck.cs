namespace Mesi.Helpers
{
    public static class NullCheck
    {
        public static T CheckNotNull<T>(T value, string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName, $"{paramName} cannot be null.");
            }

            return value;
        }

        public static int CheckGreaterThanZero(int value, string paramName)
        {
            if (value <= 0)
            {
                throw new ArgumentException($"{paramName} must be greater than 0.", paramName);
            }

            return value;
        }
    }
}