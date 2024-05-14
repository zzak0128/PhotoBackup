using CommandLine;
using PhotoBackup.CLI.CLIWorkflows;
using PhotoBackup.Library.Interfaces;

namespace PhotoBackup.CLI;

public class App
{
    private readonly ISettings _settings;

    public App(ISettings settings)
    {
        _settings = settings;
    }

    public void Run(string[] args)
    {

        Parser.Default.ParseArguments<CLIOptions>(args)
            .WithParsed(RunOptions)
            .WithNotParsed(HandleParseErrors);
    }

    internal void RunOptions(CLIOptions options)
    {
        if(options.DestinationDirectory != null)
        {
            _settings.DirectoryPaths.DestinationDirectory = options.DestinationDirectory;
        }

        if(options.IsIphone)
        {
            try
            {
                IPhoneWorkflow.Run(_settings);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Failed to backup photos from iPhone");
            }
        }
        else
        {
            LocalWorkflow.Run(_settings);
        }
        //Console.WriteLine(options.Verbose);
    }

    internal void HandleParseErrors(IEnumerable<Error> errors)
    {
        Console.WriteLine("There was an error with the option");
    }
}
