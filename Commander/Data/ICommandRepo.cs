using Commander.Models;
using System.Collections.Generic;

namespace Commander.Data
{
    public interface ICommandRepo
    {
        IEnumerable<Command> ReadAllCommand();
        Command ReadCommand(int CommandId);
        void CreateCommand(Command command);
        void UpdateCommand(Command command);
        void DeleteCommand(Command command);
        bool SaveChanges();
    }
}
