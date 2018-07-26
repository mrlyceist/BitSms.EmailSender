namespace BitSms.EmailSender
{
    public interface IConfigLoader
    {
        Config GetConfig(string path);
    }
}