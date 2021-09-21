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
            const string PageStart = @"<!DOCTYPE html><html><head><style>
body {
    white-space: pre-wrap;
    width: 98%;
    height: 100%;
}
table {
  word-break: break-word;
  font-family: 'Helvetica';
  border-collapse: collapse;
  font-size: small;
}
td, th {
  border: 1px solid #dddddd;
  vertical-align: top;
  text-align: left;
  padding: 8px;
}
tr:first-child {
	color: white;
    background-color: black;
}
tr {
  background-color: #FFFFFF;
}
h1 {
    text-decoration: underline;
    font-family: 'Comic Sans MS';
	color: black;
}
.result {
  width: 10%;
}
.command {
  width: 30%;
}
.output {
  width: 60%;
}
.success {
    color: black;
    background-color: #00ff87;
}
.error {
    color: white;
    background-color: #990000;
}
.mix {
    color: black;
    background-color: #87ffff;
}
</style>
</head>
<body>";
            const string PageEnd = @"</body>
</html>";
            const string StartTable = "<table>";
            const string EndTable = "</table>";
            const string TableTitle = @"<tr>
    <th class=""result"">Result</th>
    <th class=""command"">Command</th>
    <th class=""output"">Output</th>
  </tr>";
            var builder = new StringBuilder();
            _ = builder.Append(PageStart);

            foreach (var file in FileOutputs) {
                _ = builder.AppendLine($"<h1>{file.FileName}</h1>");
                _ = builder.AppendLine(StartTable);
                _ = builder.AppendLine(TableTitle);

                foreach (var result in file.Results) {
                    result.Switch(
                        success => {
                            _ = builder.AppendLine("<tr><th class=\"success result\">Success</th>");
                            _ = builder.AppendLine($"<th class=\"command\"><code>{success.Command}</code></th>");
                            _ = builder.AppendLine($"<th class=\"output\">{success.Output}</th></tr>");
                        },
                        error => {
                            _ = builder.AppendLine($"<tr><th class=\"error result\">Error</th>");
                            _ = builder.AppendLine($"<th class=\"command\"><code>{error.Command}</code></th>");
                            _ = builder.AppendLine($"<th class=\"output\">{error.Output}</th></tr>");
                        },
                        mix => {
                            _ = builder.AppendLine($"<tr><th class=\"mix result\">Mixed - Success</th>");
                            _ = builder.AppendLine($"<th class=\"command\"><code>{mix.Command}</code></th>");
                            _ = builder.AppendLine($"<th class=\"output\">{mix.SuccessOutput}</th></tr>");
                            _ = builder.AppendLine($"<tr><th class=\"mix result\">Mixed - Error</th>");
                            _ = builder.AppendLine($"<th class=\"command\"><code>\"\"</code></th>");
                            _ = builder.AppendLine($"<th class=\"output\">{mix.ErrorOutput}</th></tr>");
                        });
                }

                _ = builder.AppendLine(EndTable);
            }
            _ = builder.Append(PageEnd);
            Report = builder.ToString().Trim('\r', '\n');
        }

        /// <summary>
        /// Exports the report
        /// </summary>
        public OneOf<Success, Error> Export() {
            try {
                using TextWriter w = new StreamWriter("Report.html");
                w.Write(Report);
            } catch (Exception ex) {
                return new Error(ex.Message);
            }
            return new Success("Successfully exported report.");
        }
    }
}
