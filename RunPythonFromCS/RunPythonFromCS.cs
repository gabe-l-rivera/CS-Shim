/* author: gabe rivera
 * date: april 20, 2021
 * purpose: shim to run python via cs app
 * bugs: none at the moment
 */

using System;
using System.Diagnostics;

namespace RunPythonScriptFromCS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Execute python process...");
            Option1_ExecProcess();

            Console.WriteLine();

        }

        static void Option1_ExecProcess()
        {
            // 1) Create Process Info - find location of python (for windows it may be an .exe file)
            var psi = new ProcessStartInfo();
            psi.FileName = @"/Users/gaberivera/miniconda3/bin/python";

            /* 2) Provide script and arguments for program:
              
              * If a variable in the python script is being set to a var from this
              * program, you must instantiate the variable in the python script using:

              x = sys.argv[1] // sets x to argv[1] in the .cs program
              number = sys.argv[2]

              * where argv[1] and argv[2] are the variables indexed in psi.Arguments string below

            */

            // Set script to the location of the python script we want to execute
            var script = @"/Users/gaberivera/Desktop/Spring 21/CS499/intel-signal-processing/main.py";

            // For our script, all we need to do is call the program, so we just need to include script:
            psi.Arguments = $" \"{script}\" ";

            // 3) Process configuration
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            // 4) Execute process and get output
            var errors = "";
            var results = "";

            using (var process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();
            }

            // 5) Display output
            Console.WriteLine("ERRORS:");
            Console.WriteLine(errors);
            Console.WriteLine();
            Console.WriteLine("RESULTS:");
            Console.WriteLine(results);

        }
    }
}
