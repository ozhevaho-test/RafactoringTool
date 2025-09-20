using Core.Models;

namespace Core
{
    public interface IRefactoring
    {
        string Name { get; }
        string Apply(string code, RefactoringParameters parameters);
    }
}