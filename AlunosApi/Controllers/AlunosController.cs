using AlunosApi.Models;
using AlunosApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AlunosApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AlunosController : ControllerBase
{
    private IAlunoService _alunoService;

    public AlunosController(IAlunoService alunoService)
    {
        _alunoService = alunoService;
    }



    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunos()
    {
        try
        {
            var alunos = await _alunoService.GetAlunos();
            return Ok(alunos);
        }
        catch 
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter alunos.");
        }
    }



    [HttpGet("Nome")]
    public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunosByName([FromQuery] string nome)
    {
        try
        {
            var alunos = await _alunoService.GetAlunosByNome(nome);

            if (alunos.Count() == 0)
            {
                return NotFound($"Aluno '{nome}' não existe.");
            }

            return Ok(alunos);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter aluno(s) por nome.");
        }
    }

}
