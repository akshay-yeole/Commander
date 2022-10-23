using Commander.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Commander.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly CommandContext _context;

        public CommandRepo(CommandContext context)
        {
            _context = context;
        }

        public void CreateCommand(Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            _context.Commands.Add(command);
            SaveChanges();
        }

        public void DeleteCommand(Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            _context.Commands.Remove(command);
        }

        public IEnumerable<Command> ReadAllCommand()
        {
            var allCommands= _context.Commands.ToList();
            return allCommands;
        }

        public Command ReadCommand(int CommandId)
        {
            var command = _context.Commands.FirstOrDefault(c => c.CommandId == CommandId);
            return command;
        }

        public bool SaveChanges()
        {
           return (_context.SaveChanges() >= 0);
        }

        public void UpdateCommand(Command command)
        {
            //Nothing here
        }
    }
}
