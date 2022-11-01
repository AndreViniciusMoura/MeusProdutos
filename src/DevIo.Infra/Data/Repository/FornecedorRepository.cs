using DevIO.Business.Models.Fornecedores;
using System;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DevIo.Infra.Data.Repository
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public async Task<Fornecedor> ObterFornecedorEndereco(Guid id) =>
            await Db.Fornecedores.AsNoTracking()
            .Include(f => f.Endereco)
            .FirstOrDefaultAsync(f => f.Id == id);

        public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id) =>
            await Db.Fornecedores.AsNoTracking()
            .Include(f => f.Endereco)
            .Include(f => f.Produtos)
            .FirstOrDefaultAsync(f => f.Id == id);

        public override async Task Remover(Guid id)
        {
            var fornecedor = await ObterPorId(id);
            fornecedor.Ativo = false;

            await Atualizar(fornecedor);
        }
    }
}
