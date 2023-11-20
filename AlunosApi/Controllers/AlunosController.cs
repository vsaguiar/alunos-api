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



    [HttpGet("{id:int}", Name = "GetAlunoById")]
    public async Task<ActionResult<Aluno>> GetAlunoById(int id)
    {
        try
        {
            var aluno = await _alunoService.GetAlunoById(id);

            if (aluno == null)
            {
                return NotFound($"Não existe aluno com o id '{id}'.");
            }

            return Ok(aluno);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter aluno por id.");
        }
    }



    [HttpPost]
    public async Task<ActionResult> Create(Aluno aluno)
    {
        try
        {
            await _alunoService.CreateAluno(aluno);

            return CreatedAtRoute(nameof(GetAlunoById), new { id = aluno.Id }, aluno);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao criar um novo aluno.");
        }
    }



    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] Aluno aluno)
    {
        try
        {
            if (aluno.Id == id)
            {
                await _alunoService.UpdateAluno(aluno);

                return Ok($"Aluno com o id '{id}' foi atualizado com sucesso!");
            }
            else
            {
                return BadRequest("Dados inconsistentes.");
            }
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar dados do aluno.");
        }
    }

}
