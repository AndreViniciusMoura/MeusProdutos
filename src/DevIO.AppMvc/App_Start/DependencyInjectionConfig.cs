using DevIo.Infra.Data.Context;
using DevIo.Infra.Data.Repository;
using DevIO.Business.Core.Notificacoes;
using DevIO.Business.Models.Fornecedores;
using DevIO.Business.Models.Fornecedores.Interfaces.Service;
using DevIO.Business.Models.Fornecedores.Services;
using DevIO.Business.Models.Produtos;
using DevIO.Business.Models.Produtos.Interfaces;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Reflection;
using System.Web.Mvc;

namespace DevIO.AppMvc.App_Start
{
    public class DependencyInjectionConfig
    {
        public static void RegisterDIContainer()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void InitializeContainer(Container container)
        {
            // Lifestyle.Singleton
            // Uma unica instancia por aplicacao

            // Lifestyle.Transient
            // Cria uma unica instancia para cada injecao

            // Lifestyle.Scoped - so para aplicacao web
            // Uma unica instancia por request

            container.Register<MeusProdutosDbContext>(Lifestyle.Scoped);

            #region Repository

            container.Register<IProdutoRepository, ProdutoRepository>(Lifestyle.Scoped);
            container.Register<IFornecedorRepository, FornecedorRepository>(Lifestyle.Scoped);
            container.Register<IEnderecoRepository, EnderecoRepository>(Lifestyle.Scoped);

            #endregion

            #region Service

            container.Register<IProdutoService, ProdutoService>(Lifestyle.Scoped);
            container.Register<IFornecedorService, FornecedorService>(Lifestyle.Scoped);

            #endregion

            container.Register<INotificador, Notificador>(Lifestyle.Scoped);

            container.RegisterSingleton(()=> AutoMapperConfig.GetMapperConfiguration().CreateMapper(container.GetInstance));
        }
    }
}