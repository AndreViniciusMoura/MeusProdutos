using System;
using System.Web;
using System.Web.Mvc;

namespace DevIO.AppMvc.Extensions
{
    public static class RazorExtensions
    {
        public static string FormatarDocumento(this WebViewPage page, int tipoPessoa, string documento) =>
            tipoPessoa == 1 ? Convert.ToUInt64(documento).ToString(@"000\.000\.000\-00") : Convert.ToUInt64(documento).ToString();

        public static bool ExibirNaURL(this WebViewPage page, Guid id)
        {
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            var urlTarget = urlHelper.Action("edit", "Fornecedores", new { id = id });
            var urlTarget2 = urlHelper.Action("ObterEndereco", "Fornecedores", new { id = id });

            var urlEmUso = HttpContext.Current.Request.Path;

            return urlTarget == urlEmUso || urlTarget2 == urlEmUso;
        }
    }
}