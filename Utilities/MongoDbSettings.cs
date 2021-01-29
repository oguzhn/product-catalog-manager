namespace ProductCatalogManager.Utilities
{
    public class MongoDbSettings
    {
        public string ConnectionString;
        public string DatabaseName;

        public string CollectionName;

        //Configuration için kullanılacak
        #region Const Values

        public const string ConnectionStringValue = nameof(ConnectionString);
        public const string DatabaseNameValue = nameof(DatabaseName);

        public const string CollectionNameValue = nameof(CollectionName);

        #endregion
    }
}