using Loja.DAO;
using Loja.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LojaApi.Controllers
{
    public class CarrinhoController : ApiController
    {
        public HttpResponseMessage Get(int id)
        {
            try
            {
                CarrinhoDAO dao = new CarrinhoDAO();
                Carrinho carrinho = dao.Busca(id);
                return Request.CreateResponse(HttpStatusCode.OK, carrinho);
            }
            catch (KeyNotFoundException)
            {
                string mensagem = string.Format("O carrinho {0} não foi encontrado", id);
                return Request.CreateResponse(HttpStatusCode.NotFound, new HttpError(mensagem));
            }
        }

        public HttpResponseMessage Post([FromBody] Carrinho carrinho)
        {
            CarrinhoDAO dao = new CarrinhoDAO();
            dao.Adiciona(carrinho);

            // link para consultar o carrinho adicionado
            string location = Url.Link("DefaultApi", new { controller = "carrinho", id = carrinho.Id });
            // mensagem de retorno no body
            string mensagem = string.Format("Carrinho {0} cadastrado com sucesso!", carrinho.Id);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, mensagem);
            response.Headers.Location = new Uri(location);

            return response;
        }

        [Route("api/carrinho/{idCarrinho}/produto/{idProduto}")]
        public HttpResponseMessage Delete([FromUri] int idCarrinho, [FromUri] int idProduto)
        {
            try
            {
                CarrinhoDAO dao = new CarrinhoDAO();
                Carrinho carrinho = dao.Busca(idCarrinho);
                carrinho.Remove(idProduto);

                string mensagem = string.Format("Produto {0} deletado com sucesso do carrinho {1}!", idProduto, idCarrinho);
                return Request.CreateResponse(HttpStatusCode.OK, mensagem);
            } catch(KeyNotFoundException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
    }
}
