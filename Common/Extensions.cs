namespace Common
{
    public static class Extensions
    {
        public static bool IsNullOrEmpty(this Guid? value)
        {
            return value == Guid.Empty || value == null;
        }

        public static bool IsEmpty(this Guid value)
        {
            return value == Guid.Empty;
        }

        public static string ConvertSqlString(this Guid value)
        {
            return string.Format("'{0}'", value.ToString());
        }

    }
}
