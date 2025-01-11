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
        private readonly IValidator<NovoEstudanteViewModel> _validador;

        public EstudantesController(ApplicationDbContext dbContext, IValidator<NovoEstudanteViewModel> validator)
        {
            this.dbContext = dbContext;
            _validador = validator;
        }

        [HttpGet]
        public IActionResult Novo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Novo(NovoEstudanteViewModel viewModel)
        {
            // Valida o modelo usando o FluentValidation
            var resultadoValidacao = await _validador.ValidateAsync(viewModel);

            // Se o modelo não for válido, adiciona os erros ao ModelState e retorna a view
            if (!resultadoValidacao.IsValid)
            {
                foreach (var erro in resultadoValidacao.Errors)
                {
                    ModelState.AddModelError(erro.PropertyName, erro.ErrorMessage);
                }
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
            return RedirectToAction("Listar");
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

            return View(estudante);
        }
    }
}
