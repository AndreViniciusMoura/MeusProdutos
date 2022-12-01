using DevIo.Infra.Data.Context;
using DevIO.Business.Models.Fornecedores;
using System;
using System.Threading.Tasks;

namespace DevIo.Infra.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(MeusProdutosDbContext context) : base(context) { }

        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId) =>
            await ObterPorId(fornecedorId);
    }
}
