using GestaoDeEquipamentos.WebApp.Modulos.Equipamentos.Apresentacao;
using GestaoDeEquipamentos.WebApp.Modulos.Equipamentos.Dominio;
using GestaoDeEquipamentos.WebApp.Modulos.Equipamentos.Infraestrutura;
using GestaoDeEquipamentos.WebApp.Modulos.Fabricantes.Infraestrutura;
using Microsoft.AspNetCore.Mvc;

public sealed class EquipamentoController : Controller
{
    private readonly RepositorioEquipamentoEmArquivo repositorioEquipamento;
    private readonly RepositorioFabricanteEmArquivo repositorioFabricante;

    public EquipamentoController(
        RepositorioEquipamentoEmArquivo repositorioEquipamento,
        RepositorioFabricanteEmArquivo repositorioFabricante
    )
    {
        this.repositorioEquipamento = repositorioEquipamento;
        this.repositorioFabricante = repositorioFabricante;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarEquipamentoViewModel> viewModels = new List<ListarEquipamentoViewModel>();

        foreach (Equipamento e in repositorioEquipamento.SelecionarTodos())
        {
            ListarEquipamentoViewModel viewModel = new ListarEquipamentoViewModel(
                e.Id,
                e.Nome,
                e.PrecoAquisicao,
                e.DataFabricacao,
                e.Fabricante.Nome
            );

            viewModels.Add(viewModel);
        }

        return View(viewModels);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarEquipamentoViewModel viewModel = new(
            null,
            null,
            null,
            0,
            ObterFabricantesDisponiveis()
        );

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarEquipamentoViewModel viewModel)
    {
        Fabricante? fabricanteSelecionado =
            repositorioFabricante.SelecionarPorId(viewModel.FabricanteId);

        if (fabricanteSelecionado == null)
            ModelState.AddModelError(nameof(viewModel.FabricanteId), "Selecione um fabricante válido");

        if (!ModelState.IsValid)
        {
            viewModel = viewModel with
            {
                FabricantesDisponiveis = ObterFabricantesDisponiveis()
            };

            return View(viewModel);
        }

        Equipamento equipamento = new(
            viewModel.Nome ?? string.Empty,
            viewModel.PrecoAquisicao.GetValueOrDefault(),
            viewModel.DataFabricacao.GetValueOrDefault(),
            fabricanteSelecionado!
        );

        repositorioEquipamento.Cadastrar(equipamento);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(int id)
    {
        Equipamento? equipamentoSelecionado = repositorioEquipamento.SelecionarPorId(id);

        if (equipamentoSelecionado == null)
            return NotFound();

        EditarEquipamentoViewModel viewModel = new(
            equipamentoSelecionado.Id,
            equipamentoSelecionado.Nome,
            equipamentoSelecionado.PrecoAquisicao,
            equipamentoSelecionado.DataFabricacao,
            equipamentoSelecionado.Fabricante.Id,
            ObterFabricantesDisponiveis()
        );


        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Editar(int id, EditarEquipamentoViewModel viewModel)
    {
        Fabricante? fabricanteSelecionado =
            repositorioFabricante.SelecionarPorId(viewModel.FabricanteId);

        if (fabricanteSelecionado == null)
            ModelState.AddModelError(nameof(viewModel.FabricanteId), "Selecione um fabricante válido.");

        if (!ModelState.IsValid)
        {
            viewModel = viewModel with
            {
                FabricantesDisponiveis = ObterFabricantesDisponiveis()
            };

            return View(viewModel);
        }

        Equipamento equipamentoAtualizado = new(
           viewModel.Nome ?? string.Empty,
           viewModel.PrecoAquisicao.GetValueOrDefault(),
           viewModel.DataFabricacao.GetValueOrDefault(),
           fabricanteSelecionado!
        );

        bool conseguiuEditar = repositorioEquipamento.Editar(id, equipamentoAtualizado);

        if (!conseguiuEditar)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(int id)
    {
        Equipamento? equipamentoSelecionado = repositorioEquipamento.SelecionarPorId(id);

        if (equipamentoSelecionado == null)
            return NotFound();

        ExcluirEquipamentoViewModel viewModel = new(
            equipamentoSelecionado.Id,
            equipamentoSelecionado.Nome
        );

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirEquipamentoViewModel viewModel)
    {
        bool conseguiuExcluir = repositorioEquipamento.Excluir(viewModel.Id);

        if (!conseguiuExcluir)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }

    private List<SelecionarFabricanteViewModel> ObterFabricantesDisponiveis()
    {
        List<SelecionarFabricanteViewModel> viewModels = new();

        foreach (Fabricante f in repositorioFabricante.SelecionarTodos())
        {
            SelecionarFabricanteViewModel viewModel = new(f.Id, f.Nome);

            viewModels.Add(viewModel);
        }

        return viewModels;
    }
}