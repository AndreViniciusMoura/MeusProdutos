﻿using System;
using System.Threading.Tasks;

namespace DevIO.Business.Models.Fornecedores.Interfaces.Service
{
    public interface IFornecedorService : IDisposable
    {
        Task Adicionar(Fornecedor fornecedor);

        Task Atualizar(Fornecedor fornecedor);

        Task Remover(Guid id);

        Task AtualizarEndereco(Endereco endereco);
    }
}
