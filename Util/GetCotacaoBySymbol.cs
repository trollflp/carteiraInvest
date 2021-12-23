using CarteiraSafra.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CarteiraSafra.Util
{
    public class GetCotacaoBySymbol
    {
        public static Cotacao retornaCotacao(string symbol)
        {
            Cotacao cotacao = new Cotacao();

            string action = string.Format("https://yfapi.net/v6/finance/quote?region=US&lang=en&symbols=" + symbol);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, action);
            request.Headers.Add("x-api-key", "koMIvJLd2N7jfGxahhsry6HGjOfxICdZ5lcOf3Bu");

            try
            {
                HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    JObject dados = JObject.Parse(response.Content.ReadAsStringAsync().Result);

                    cotacao.Codigo = dados["quoteResponse"]["result"][0]["symbol"].ToString();
                    cotacao.RazaoSocial = dados["quoteResponse"]["result"][0]["longName"].ToString();
                    cotacao.PrecoCompra = (double)dados["quoteResponse"]["result"][0]["bid"];
                    cotacao.PrecoVenda = (double)dados["quoteResponse"]["result"][0]["ask"];

                }
            } catch
            {
                return null;
            }           

            return cotacao;
        }
    }
}
