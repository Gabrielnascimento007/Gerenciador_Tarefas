using Gerenciador.Application.UseCases.Tarefas.Delete;
using Gerenciador.Application.UseCases.Tarefas.GetAll;
using Gerenciador.Application.UseCases.Tarefas.GetById;
using Gerenciador.Application.UseCases.Tarefas.Register;
using Gerenciador.Application.UseCases.Tarefas.Update;
using Gerenciador.Communication.Requests;
using Gerenciador.Communication.Response;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorTarefas.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GerenciadorTarefasController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterTarefaJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    public IActionResult Register([FromBody] RequestTarefasJson request)
    {
        var response = new RegisterTarefasUseCase().Execute(request);

        return Created(string.Empty, response);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    public IActionResult Update([FromRoute] int id, [FromBody] RequestTarefasJson request)
    {
        var useCase = new UpdateTarefasUseCase();

        useCase.Execute(id, request);

        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseAllTarefasJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult GetAll()
    {
        var useCase = new GetAllTarefasUseCase();

        var response = useCase.Execute();

        if (response.Tarefas.Any())
        {
            return Ok(response);
        }

        return NoContent();
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseTarefaJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]

    public IActionResult Get(int id)
    {
        var useCase = new GetTarefaByIdUseCase();

        var response = useCase.Execute(id);

        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        var useCase = new DeleteTarefaByIdUseCase();

        useCase.Execute(id);

        return NoContent();
    }
}

