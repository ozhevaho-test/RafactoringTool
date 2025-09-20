using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Refactorings
{
    public class ExtractMethodRefactoring : IRefactoring
    {
        public string Name => "Extract Method";

        public string Apply(string code, RefactoringParameters parameters)
        {
            // Отримуємо параметри
            var startLine = parameters.Parameters.ContainsKey("startLine")
                ? int.Parse(parameters.Parameters["startLine"])
                : throw new ArgumentException("");
            var endLine = int.Parse(parameters.Parameters["endLine"]);
            var methodName = parameters.Parameters["methodName"];

            // Розбиваємо код на рядки
            var lines = code.Split('\n');

            // Витягуємо потрібні рядки
            var extractedLines = new List<string>();
            for (int i = startLine; i <= endLine && i < lines.Length; i++)
            {
                extractedLines.Add(lines[i]);
            }

            // Створюємо новий метод
            var newMethod = $@"
        private void {methodName}()
        {{
{string.Join("\n", extractedLines)}
        }}";

            // Замінюємо витягнуті рядки на виклик методу
            var methodCall = $"            {methodName}();";

            // Формуємо результат
            var result = new List<string>();
            for (int i = 0; i < lines.Length; i++)
            {
                if (i == startLine)
                {
                    result.Add(methodCall);
                    i = endLine; // Пропускаємо витягнуті рядки
                }
                else if (i < startLine || i > endLine)
                {
                    result.Add(lines[i]);
                }
            }

            // Додаємо новий метод в кінець
            result.Add(newMethod);

            return string.Join("\n", result);
        }
    }
}