namespace RefactorThis.Api.Settings
{
    public static class SqliteSettings
    {
        public static string ConnectionString
        {
            get
            {
                return "Data Source=App_Data/products.db";
            }
        }
    }
}