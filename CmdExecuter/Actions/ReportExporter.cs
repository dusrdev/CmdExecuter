using CmdExecuter.Core.Models;

using OneOf;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CmdExecuter.Actions {
    internal class ReportExporter {
        private SortedSet<FileExecutionOutput> FileOutputs { get; init; }
        private string Report { get; set; }

        /// <summary>
        /// Initializes the class
        /// </summary>
        /// <param name="outputs">List of execution errors</param>
        /// <remarks>
        /// Continue with .CreateReport() -> Export()
        /// </remarks>
        public ReportExporter(SortedSet<FileExecutionOutput> outputs) {
            FileOutputs = outputs;
        }

        /// <summary>
        /// Creates the report
        /// </summary>
        public void CreateReport() {
            if (!FileOutputs.Any()) {
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
	color: black;
    background-color: #d787ff;
}
tr {
  background-color: #FFFFFF;
}
h1 {
	color: black;
}
.success {
    color: black;
    background-color: #00ff87;
}
.error {
    color: white;
    background-color: #990000;
}
</style>
</head>
<body>";
            const string PageEnd = @"</body>
</html>";
            const string StartTable = "<table>";
            const string EndTable = "</table>";
            const string TableTitle = @"<tr>
    <th>Result</th>
    <th>Command</th>
    <th>Output</th>
  </tr>";
            var builder = new StringBuilder();
            _ = builder.AppendLine(PageStart);

            foreach (var file in FileOutputs) {
                _ = builder.AppendLine($"<h1>{file.FileName}</h1>");
                _ = builder.AppendLine(StartTable);
                _ = builder.AppendLine(TableTitle);

                if (file.HasSuccesses) {
                    foreach (var success in file.Successes) {
                        _ = builder.AppendLine("<tr><th class=\"success\">Success</th>");
                        _ = builder.AppendLine($"<th>{success.Command}</th>");
                        _ = builder.AppendLine($"<th>{success.Output}</th></tr>");
                    }
                }

                if (file.HasErrors) {
                    foreach (var error in file.Errors) {
                        _ = builder.AppendLine($"<tr><th class=\"error\">Error</th>");
                        _ = builder.AppendLine($"<th>{error.Command}</th>");
                        _ = builder.AppendLine($"<th>{error.Output}</th></tr>");
                    }
                }
                _ = builder.AppendLine(EndTable);
            }
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
            return new Success("Successfully exported report.");
        }
    }
}
