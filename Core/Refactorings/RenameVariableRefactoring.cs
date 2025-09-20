using Core.Models;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Refactorings
{
    public class RenameVariableRefactoring : IRefactoring
    {
        public string Name => "Rename Variable";

        public string Apply(string code, RefactoringParameters parameters)
        {
            var oldName = parameters.Parameters["oldName"];
            var newName = parameters.Parameters["newName"];

            var pattern = $@"\b{Regex.Escape(oldName)}\b";
            return Regex.Replace(code, pattern, newName);
        }
    }
}