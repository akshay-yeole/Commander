using AutoMapper;
using Commander.Data;
using Commander.Models;
using Commander.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Commander.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo repo;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepo _repo, IMapper mapper)
        {
            repo = _repo;
            _mapper = mapper;
        }

        //GET api/commands
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDTO>> ReadAllCommands()
        {
            var allCommands = repo.ReadAllCommand();
            return Ok(_mapper.Map<IEnumerable<CommandReadDTO>>(allCommands));
        }

        //GET api/commands/2
        [HttpGet("{CommandId}", Name = "ReadCommand")]
        public ActionResult<CommandReadDTO> ReadCommand(int CommandId)
        {
            var command = repo.ReadCommand(CommandId);

            if (command == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CommandReadDTO>(command));
        }

        [HttpPost]
        public ActionResult<CommandCreateDTO> CreateCommand(CommandCreateDTO command)
        {
            var addCommand = _mapper.Map<Command>(command);
            repo.CreateCommand(addCommand);
            repo.SaveChanges();
            var commandReadDto = _mapper.Map<CommandReadDTO>(addCommand);
                return CreatedAtRoute(nameof(ReadCommand), new { CommandId = commandReadDto.CommandId }, commandReadDto);
            }

        [HttpPut("{CommandId}")]
        public ActionResult<CommandUpdateDTO> UpdateCommand(int CommandId, CommandUpdateDTO command)
        {
            var commandRead = repo.ReadCommand(CommandId);
            if (commandRead == null)
            {
                return NotFound();
            }
            _mapper.Map(command, commandRead);
            repo.UpdateCommand(commandRead);
            repo.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{CommandId}")]
        public ActionResult<CommandUpdateDTO> PartialUpdateCommand(int CommandId, JsonPatchDocument<CommandUpdateDTO> patchDocument)
        {
            var commandRead = repo.ReadCommand(CommandId);
            if (commandRead == null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandUpdateDTO>(commandRead);
            patchDocument.ApplyTo(commandToPatch, ModelState);
            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(commandToPatch, commandRead);
            repo.UpdateCommand(commandRead);
            repo.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{CommandId}")]
        public ActionResult DeleteCommand(int CommandId)
        {
            var command = repo.ReadCommand(CommandId);

            if (command == null)
            {
                return NotFound();
            }
            repo.DeleteCommand(command);
            repo.SaveChanges();    
            return NoContent();
        }
    }
}
