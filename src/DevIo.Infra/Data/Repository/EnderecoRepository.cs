using DevIO.Business.Core.Data;
using DevIO.Business.Models.Fornecedores;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DevIo.Infra.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId) =>
            await ObterPorId(fornecedorId);
    }
}
