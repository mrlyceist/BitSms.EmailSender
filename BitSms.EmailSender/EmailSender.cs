using Guvnor.Specification;
using NLog;
using SmsCucumber.Sdk.Models;
using SmsCucumber.Sdk.Specification;
using System;
using System.Threading.Tasks;

namespace BitSms.EmailSender
{
    public class EmailSender : IMessageSender, IPlugin
    {
        private readonly ILogger logger;
        private readonly IConfigLoader configLoader;
        private readonly IEmailClient emailClient;
        private readonly IValidator validator;
        private Config config;

        public EmailSender(ILogger logger,
            IConfigLoader configLoader,
            IEmailClient emailClient,
            IValidator validator)
        {
            this.logger = logger;
            this.configLoader = configLoader;
            this.emailClient = emailClient;
            this.validator = validator;
        }

        public string Name => "EmailSender";
        public Guid PluginId => new Guid("AC6017F2-1543-4674-B347-B1B73F9239AD");

        public void Configure(string configFile)
        {
            try
            {
                //this.config = configLoader.GetConfig(configFile);
                this.config = new Config
                {
                    AddressFrom = "adorogov@ooobit.com",
                    DefaultMessageSubject = "test",
                    HostAddress = "mail.netangels.ru",
                    Login = "adorogov@ooobit.com",
                    NeedsSsl = true,
                    Password = "4XRJPMqCbK",
                    Port = 25
                };
                ValidateConfig(config);
            }
            catch (Exception e)
            {
                logger.Error($"Error configuring EmailSender: {e.Message}");
                throw;
            }
        }

        private void ValidateConfig(Config config)
        {
            if (string.IsNullOrWhiteSpace(config.Login))
                validator.LogAndThrow(logger, config.Login);
            if (string.IsNullOrWhiteSpace(config.AddressFrom))
                validator.LogAndThrow(logger, config.AddressFrom);
            if (string.IsNullOrWhiteSpace(config.HostAddress))
                validator.LogAndThrow(logger, config.HostAddress);
            if (string.IsNullOrWhiteSpace(config.Password))
                validator.LogAndThrow(logger, config.Password);
        }

        public async Task<Status> SendMessage(Message message)
        {
            return await emailClient.Send(message, config);
        }

        public Task<Status> CheckMessageStatus(Message message)
        {
            throw new NotImplementedException();
        }
    }
}