namespace ProductManagement.Settings
{
    public class AzureSmsServiceOptions
    {
        public string Sender { get; set; }

        public string ConnectionString
        {
            get; set;
        }
    }
}