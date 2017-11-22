using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LojaAPIClient
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:60852/api/carrinho/1/produto/6237/quantidade");
            request.Method = "PUT";

            string xml = "<Produto xmlns:i='http://www.w3.org/2001/XMLSchema-instance' xmlns='http://schemas.datacontract.org/2004/07/Loja.Models'><Id>6237</Id><Nome>Videogame 4</Nome><Preco>4000</Preco><Quantidade>10</Quantidade></Produto>";

            byte[] xmlBytes = Encoding.UTF8.GetBytes(xml);
            request.GetRequestStream().Write(xmlBytes, 0, xmlBytes.Length);

            request.ContentType = "Application/xml";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Console.WriteLine(response.StatusCode);

            Console.Read();
        }

        static void TestaGet(string[] args)
        {
            string conteudo;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:60852/api/carrinho/1");
            request.Method = "GET";

            WebResponse response = request.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                conteudo = reader.ReadToEnd();
            }

            Console.Write(conteudo);
            Console.Read();
        }
    }
}
