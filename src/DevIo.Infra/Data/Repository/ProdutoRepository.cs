using DevIo.Infra.Data.Context;
using DevIO.Business.Models.Produtos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DevIo.Infra.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(MeusProdutosDbContext context): base(context) { }

        public async Task<Produto> ObterProdutoFornecedor(Guid id) =>
            await Db.Produtos.AsNoTracking()
            .Include(f => f.Fornecedor)
            .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<Produto>> ObterProdutosFornecedores() =>
            await Db.Produtos.AsNoTracking()
            .Include(f => f.Fornecedor)
            .OrderBy(p => p.Nome).ToListAsync();

        public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId) =>
            await Buscar(p => p.FornecedorId == fornecedorId);
    }
}
