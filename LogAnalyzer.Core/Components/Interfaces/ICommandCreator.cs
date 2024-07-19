using System.Windows.Input;

namespace LogAnalyzer.Core.Components.Interfaces;

public interface ICommandCreator
{
    public ICommand? CreateCommand(Type commandType);
}