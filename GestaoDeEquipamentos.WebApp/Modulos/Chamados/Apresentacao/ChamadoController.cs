using GestaoDeEquipamentos.WebApp.Modulos.Chamados.Dominio;
using GestaoDeEquipamentos.WebApp.Modulos.Chamados.Infraestrutura;
using GestaoDeEquipamentos.WebApp.Modulos.Equipamentos.Dominio;
using GestaoDeEquipamentos.WebApp.Modulos.Equipamentos.Infraestrutura;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeEquipamentos.WebApp.Modulos.Chamados.Apresentacao;

public sealed class ChamadoController : Controller
{
    private readonly RepositorioChamadoEmArquivo repositorioChamado;
    private readonly RepositorioEquipamentoEmArquivo repositorioEquipamento;

    public ChamadoController(
        RepositorioChamadoEmArquivo repositorioChamado,
        RepositorioEquipamentoEmArquivo repositorioEquipamento)
    {
        this.repositorioChamado = repositorioChamado;
        this.repositorioEquipamento = repositorioEquipamento;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarChamadoViewModel> viewModels = new();

        foreach (Chamado chamado in repositorioChamado.SelecionarTodos())
        {
            int diasEmAberto = (DateTime.Today - chamado.DataAbertura.Date).Days;

            viewModels.Add(new ListarChamadoViewModel(
                chamado.Id,
                chamado.Titulo,
                chamado.Equipamento.Nome,
                chamado.DataAbertura,
                diasEmAberto
            ));
        }

        return View(viewModels);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarChamadoViewModel viewModel = new(
            null,
            null,
            0,
            DateTime.Today,
            ObterEquipamentosDisponiveis()
        );

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarChamadoViewModel viewModel)
    {
        Equipamento? equipamentoSelecionado =
            repositorioEquipamento.SelecionarPorId(viewModel.EquipamentoId);

        if (equipamentoSelecionado == null)
            ModelState.AddModelError(nameof(viewModel.EquipamentoId), "Selecione um equipamento válido.");

        if (!ModelState.IsValid)
        {
            viewModel = viewModel with
            {
                EquipamentosDisponiveis = ObterEquipamentosDisponiveis()
            };

            return View(viewModel);
        }

        Chamado chamado = new(
            viewModel.Titulo ?? string.Empty,
            viewModel.Descricao ?? string.Empty,
            equipamentoSelecionado!,
            viewModel.DataAbertura.GetValueOrDefault()
        );

        repositorioChamado.Cadastrar(chamado);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(int id)
    {
        Chamado? chamadoSelecionado = repositorioChamado.SelecionarPorId(id);

        if (chamadoSelecionado == null)
            return NotFound();

        EditarChamadoViewModel viewModel = new(
            chamadoSelecionado.Id,
            chamadoSelecionado.Titulo,
            chamadoSelecionado.Descricao,
            chamadoSelecionado.Equipamento.Id,
            chamadoSelecionado.DataAbertura,
            ObterEquipamentosDisponiveis()
        );

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Editar(int id, EditarChamadoViewModel viewModel)
    {
        Equipamento? equipamentoSelecionado =
            repositorioEquipamento.SelecionarPorId(viewModel.EquipamentoId);

        if (equipamentoSelecionado == null)
            ModelState.AddModelError(nameof(viewModel.EquipamentoId), "Selecione um equipamento válido.");

        if (!ModelState.IsValid)
        {
            viewModel = viewModel with
            {
                EquipamentosDisponiveis = ObterEquipamentosDisponiveis()
            };

            return View(viewModel);
        }

        Chamado chamadoAtualizado = new(
            viewModel.Titulo ?? string.Empty,
            viewModel.Descricao ?? string.Empty,
            equipamentoSelecionado!,
            viewModel.DataAbertura.GetValueOrDefault()
        );

        bool conseguiuEditar = repositorioChamado.Editar(id, chamadoAtualizado);

        if (!conseguiuEditar)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(int id)
    {
        Chamado? chamadoSelecionado = repositorioChamado.SelecionarPorId(id);

        if (chamadoSelecionado == null)
            return NotFound();

        ExcluirChamadoViewModel viewModel = new(
            chamadoSelecionado.Id,
            chamadoSelecionado.Titulo
        );

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirChamadoViewModel viewModel)
    {
        bool conseguiuExcluir = repositorioChamado.Excluir(viewModel.Id);

        if (!conseguiuExcluir)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }

    private List<SelecionarEquipamentoViewModel> ObterEquipamentosDisponiveis()
    {
        List<SelecionarEquipamentoViewModel> viewModels = new();

        foreach (Equipamento equipamento in repositorioEquipamento.SelecionarTodos())
            viewModels.Add(new SelecionarEquipamentoViewModel(equipamento.Id, equipamento.Nome));

        return viewModels;
    }
}
