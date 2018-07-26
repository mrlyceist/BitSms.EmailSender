namespace BitSms.EmailSender
{
    public class Config
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string HostAddress { get; set; }
        public int Port { get; set; }
        public bool NeedsSsl { get; set; }
        public string DefaultMessageSubject { get; set; }
        public string AddressFrom { get; set; }
    }
}