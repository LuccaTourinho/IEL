using FluentValidation;
using IEL.Data;
using IEL.Models;
using IEL.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IEL.Controllers
{
    public class EstudantesController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IValidator<NovoEstudanteViewModel> _validadorNovo;
        private readonly IValidator<EditarEstudanteViewModel> _validadorEditar;

        public EstudantesController(
            ApplicationDbContext dbContext, 
            IValidator<NovoEstudanteViewModel> validatorNovo, 
            IValidator<EditarEstudanteViewModel> validadorEditar
        )
        {
            this.dbContext = dbContext;
            _validadorNovo = validatorNovo;
            _validadorEditar = validadorEditar;
        }

        [HttpGet]
        public IActionResult Novo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Novo(NovoEstudanteViewModel viewModel)
        {
            // Validar se já existe um estudante com o mesmo nome ou CPF
            if (await dbContext.Estudantes.AnyAsync(e => e.Nome == viewModel.Nome))
            {
                ModelState.AddModelError("Nome", "Já existe um estudante com este nome.");
            }
            if (await dbContext.Estudantes.AnyAsync(e => e.CPF == viewModel.CPF))
            {
                ModelState.AddModelError("CPF", "Já existe um estudante com este CPF.");
            }

            // Valida o modelo usando o FluentValidation
            var resultadoValidacao = await _validadorNovo.ValidateAsync(viewModel);

            // Se o modelo não for válido, adiciona os erros ao ModelState e retorna a view
            if (!resultadoValidacao.IsValid)
            {
                foreach (var erro in resultadoValidacao.Errors)
                {
                    ModelState.AddModelError(erro.PropertyName, erro.ErrorMessage);
                }
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            // Se o modelo for válido, cria um novo estudante e salva no banco de dados
            var estudante = new Estudante
            {
                Nome = viewModel.Nome,
                CPF = viewModel.CPF,
                Endereco = viewModel.Endereco,
                DataConclusao = viewModel.DataConclusao
            };

            await dbContext.Estudantes.AddAsync(estudante);
            await dbContext.SaveChangesAsync();

            // Redireciona para a página de listagem de estudantes
            return RedirectToAction("Listar", "Estudantes");
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var estudantes = await dbContext.Estudantes.ToListAsync();
            return View(estudantes);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(Guid id)
        {
            var estudante = await dbContext.Estudantes.FindAsync(id);

            if (estudante == null)
            {
                return NotFound();
            }

            var viewModel = new EditarEstudanteViewModel
            {
                Id = estudante.Id,
                Nome = estudante.Nome,
                CPF = estudante.CPF,
                Endereco = estudante.Endereco,
                DataConclusao = estudante.DataConclusao
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(EditarEstudanteViewModel viewModel)
        {
            // Se o nome foi alterado, valida a duplicidade
            if (viewModel.Nome != null && await dbContext.Estudantes.AnyAsync(e => e.Nome == viewModel.Nome && e.Id != viewModel.Id))
            {
                ModelState.AddModelError("Nome", "Já existe um estudante com este nome.");
            }

            // Se o CPF foi alterado, valida a duplicidade
            if (viewModel.CPF != null && await dbContext.Estudantes.AnyAsync(e => e.CPF == viewModel.CPF && e.Id != viewModel.Id))
            {
                ModelState.AddModelError("CPF", "Já existe um estudante com este CPF.");
            }

            // Valida o modelo usando o FluentValidation
            var resultadoValidacao = await _validadorEditar.ValidateAsync(viewModel);
            // Se o modelo não for válido, adiciona os erros ao ModelState e retorna a view
            if (!resultadoValidacao.IsValid)
            {
                foreach (var erro in resultadoValidacao.Errors)
                {
                    ModelState.AddModelError(erro.PropertyName, erro.ErrorMessage);
                }
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            // Se o modelo for válido, busca o estudante no banco de dados e atualiza os dados
            var estudante = await dbContext.Estudantes.FindAsync(viewModel.Id);
            if (estudante == null)
            {
                return NotFound();
            }

            estudante.Nome = viewModel.Nome;
            estudante.CPF = viewModel.CPF;
            estudante.Endereco = viewModel.Endereco;
            estudante.DataConclusao = viewModel.DataConclusao;
            dbContext.Estudantes.Update(estudante);
            await dbContext.SaveChangesAsync();

            // Redireciona para a página de listagem de estudantes
            return RedirectToAction("Listar", "Estudantes");
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var estudante = await dbContext.Estudantes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (estudante == null)
            {
                return NotFound();
            }

            dbContext.Estudantes.Remove(estudante);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Listar", "Estudantes");
        }
    }
}
