namespace Mango.Web
{
    public static class StaticDetails
    {
        public static string? ProductApiBase { get; set; }
        public static string? ShoppingCartApi { get; set; }
        internal enum ApiType
        {
            GET,
            POST, 
            DELETE,
            PUT
        }
    }
}
