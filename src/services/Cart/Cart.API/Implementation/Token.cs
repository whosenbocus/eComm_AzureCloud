using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.API.Implementation
{
    public class Token
    {
        public async Task<string> GetSecretAsync()
        {
            var azureServiceTokenProvider = new AzureServiceTokenProvider();

            var keyVaultClient =
              new KeyVaultClient(
                new KeyVaultClient.AuthenticationCallback(
                  azureServiceTokenProvider.KeyVaultTokenCallback));

            var secret = await keyVaultClient
              .GetSecretAsync("https://cartapivault .azure.net", "ServiceBusConnection")
              .ConfigureAwait(false);

            return secret.Value;
        }
    }
}
