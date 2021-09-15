using CmdExecuter.Core.Models;

using OneOf;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdExecuter.Actions {
    internal class ErrorExporter {
        private List<CommandExecutionError> Errors { get; init; }
        private string Report { get; set; }

        /// <summary>
        /// Initializes the class
        /// </summary>
        /// <param name="errors">List of execution errors</param>
        /// <remarks>
        /// Continue with .CreateReport() -> Export()
        /// </remarks>
        public ErrorExporter(List<CommandExecutionError> errors) {
            Errors = errors;
        }

        /// <summary>
        /// Creates the report
        /// </summary>
        public void CreateReport() {
            if (!Errors.Any()) {
                return;
            }
            const string PageStart = @"<!DOCTYPE html>
<html>
<head>
<style>
table {
  font-family: arial;
  border-collapse: collapse;
  font-size: small;
  width: 100%;
}
td, th {
  border: 1px solid #dddddd;
  text-align: left;
  padding: 8px;
}
th:first-child {
  width: 15%;
}
tr:first-child {
	color: white;
    background-color: #00ff87;
}
tr {
  background-color: #FFFFFF;
}
h1 {
	color: #00ff87;
}
</style>
</head>
<body>";
            const string PageEnd = @"</body>
</html>";
            const string StartTable = "<table>";
            const string EndTable = "</table>";
            const string TableTitle = @"<tr>
    <th>Command</th>
    <th>Error</th>
  </tr>";
            var builder = new StringBuilder();
            _ = builder.AppendLine(PageStart);
                _ = builder.AppendLine("<h1>Execution Errors:</h1>");
                _ = builder.AppendLine(StartTable);
                _ = builder.AppendLine(TableTitle);
                foreach (var error in Errors) {
                    _ = builder.AppendLine($"<tr><th>{error.Command}</th>");
                    _ = builder.AppendLine($"<th>{error.Error}</th></tr>");
                }
                _ = builder.AppendLine(EndTable);
            _ = builder.AppendLine(PageEnd);
            Report = builder.ToString().Trim('\r', '\n');
        }

        /// <summary>
        /// Exports the report
        /// </summary>
        public OneOf<Success, Error> Export() {
            try {
                using TextWriter w = new StreamWriter("ExecutionErrors.html");
                w.Write(Report);
            } catch (Exception ex) {
                return new Error(ex.Message);
            }
            return new Success("Successfully export report.");
        }
    }
}
