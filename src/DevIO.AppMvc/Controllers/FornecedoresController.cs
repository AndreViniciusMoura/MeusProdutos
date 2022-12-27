using AutoMapper;
using DevIO.AppMvc.ViewModels;
using DevIO.Business.Models.Fornecedores;
using DevIO.Business.Models.Fornecedores.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DevIO.AppMvc.Controllers
{
    public class FornecedoresController : BaseController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;

        public FornecedoresController(IFornecedorRepository fornecedorRepository, IFornecedorService fornecedorService, IMapper mapper)
        {
            _fornecedorRepository = fornecedorRepository;
            _fornecedorService = fornecedorService;
            _mapper = mapper;
        }

        [Route("lista-de-fornecedores")]
        public async Task<ActionResult> Index() =>
            View(_mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos()));

        [Route("dados-do-fornecedore/{id:guid}")]
        public async Task<ActionResult> Details(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorEndereco(id);

            if (fornecedorViewModel is null) return HttpNotFound();

            return View(fornecedorViewModel);
        }


        [Route("novo-fornecedor")]
        public ActionResult Create() => View();

        [Route("novo-fornecedor")]
        [HttpPost]
        public async Task<ActionResult> Create(FornecedorViewModel fornecedorViewModel)
        {
            if (!ModelState.IsValid) return View(fornecedorViewModel);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _fornecedorService.Adicionar(fornecedor);

            // TODO:
            // E se nao der certo

            return RedirectToAction("Index");
        }


        [Route("editar-fornecedor/{id:guid}")]
        public async Task<ActionResult> Edit(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorProdutosEndereco(id);

            if (fornecedorViewModel is null) return HttpNotFound();

            return View(fornecedorViewModel);
        }

        [Route("editar-fornecedor/{id:guid}")]
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, FornecedorViewModel fornecedorViewModel)
        {
            if (id != fornecedorViewModel.Id) return HttpNotFound();

            if (!ModelState.IsValid) return View(fornecedorViewModel);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _fornecedorService.Atualizar(fornecedor);

            //TODO:
            //E se nao der certo

            return RedirectToAction("Index");
        }

        [Route("excluir-fornecedor/{id:guid}")]

        public async Task<ActionResult> Delete(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorEndereco(id);

            if (fornecedorViewModel is null) return HttpNotFound();

            return View(fornecedorViewModel);

        }

        [Route("excluir-fornecedor/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);

            if (fornecedor is null) return HttpNotFound();

            await _fornecedorService.Remover(id);

            // TODO:
            // E se nao der certo?

            return RedirectToAction("Index");
        }

        [Route("obter-endereco-fornecedor/{id:guid}")]
        public async Task<ActionResult> ObterEndereco(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);

            if (fornecedor is null) return HttpNotFound();

            return PartialView("_DetalheEndereco", fornecedor);
        }

        [Route("atualizar-endereco-fornecedor/{id:guid}")]
        [HttpGet]
        public async Task<ActionResult> AtualizarEndereco(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);

            if (fornecedor is null) return HttpNotFound();

            return PartialView("_AtualizarEndereco", new FornecedorViewModel { Endereco = fornecedor.Endereco });
        }

        [Route("atualizar-endereco-fornecedor/{id:guid}")]
        [HttpPost]
        public async Task<ActionResult> AtualizarEndereco(FornecedorViewModel fornecedorViewModel)
        {
            ModelState.Remove("Nome");
            ModelState.Remove("Documento");

            if (!ModelState.IsValid) return PartialView("_atualizarEndereco", fornecedorViewModel);

            await _fornecedorService.AtualizarEndereco(_mapper.Map<Endereco>(fornecedorViewModel.Endereco));

            //TODO:
            // E se nao der certo?

            var url = Url.Action("ObterEndereco", "Fornecedores", new { id = fornecedorViewModel.Endereco.FornecedorId });
            return Json(new { success = true, url });
        }

        private async Task<FornecedorViewModel> ObterFornecedorEndereco(Guid id) =>
            _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorEndereco(id));

        private async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(Guid id) =>
            _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _fornecedorRepository.Dispose();
                _fornecedorService.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}