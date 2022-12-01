using DevIO.Business.Models.Fornecedores;
using System;
using System.Threading.Tasks;
using System.Data.Entity;
using DevIo.Infra.Data.Context;

namespace DevIo.Infra.Data.Repository
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(MeusProdutosDbContext context) : base(context) { }

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
