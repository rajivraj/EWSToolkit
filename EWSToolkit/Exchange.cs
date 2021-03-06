﻿using System;
using Microsoft.Exchange.WebServices.Data;

namespace EWS
{
    public class Exchange
    {
        public static ExchangeService NewExchangeService(string email, string password)
        {
            Console.WriteLine(" [>] Connecting to EWS...");

            System.Net.ServicePointManager
                    .ServerCertificateValidationCallback +=
                    (sender, cert, chain, sslPolicyErrors) => true;

            ExchangeService exchange = new ExchangeService(ExchangeVersion.Exchange2010_SP2);
            exchange.UseDefaultCredentials = false;
            exchange.Credentials = new WebCredentials(email, password);
            exchange.AutodiscoverUrl(email, RedirectionUrlValidationCallback);

            return exchange;
        }

        private static bool RedirectionUrlValidationCallback(string redirectionUrl)
        {
            bool result = false;
            Uri redirectionUri = new Uri(redirectionUrl);

            if (redirectionUri.Scheme == "https")
            {
                result = true;
            }

            return result;
        }
    }
}
