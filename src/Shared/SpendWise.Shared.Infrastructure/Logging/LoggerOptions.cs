using SpendWise.Shared.Infrastructure.Logging.Options;
using FileOptions = SpendWise.Shared.Infrastructure.Logging.Options.FileOptions;

namespace SpendWise.Shared.Infrastructure.Logging;

public class LoggerOptions
{
    public string Level { get; set; }
    public ConsoleOptions Console { get; set; }
    public FileOptions File { get; set; }
    public SeqOptions Seq { get; set; }
    public IDictionary<string, string> Overrides { get; set; }
    public IEnumerable<string> ExcludePaths { get; set; }
    public IEnumerable<string> ExcludeProperties { get; set; }
    public IDictionary<string, object> Tags { get; set; }
}