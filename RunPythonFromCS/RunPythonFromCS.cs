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
              
              If there is a variable in the python script that is being set to a var in this
              program, you must instantiate the variable in the python script using

                    x = sys.argv[1] // sets x to argv[1] in the .cs program
                    number = sys.argv[2]

            */

            var script = @"/Users/gaberivera/Desktop/Spring 21/CS499/intel-signal-processing/main.py";
            var some_arg1 = "input to a function"; // some string for sake of example
            var some_arg2 = 2345; // some arbitarty intiger for sake of example

            // Pass in arguments from .cs file to .py script:
            // argv[script, some_arg1, some_arg2]
            psi.Arguments = $" \"{script}\" \"{some_arg1}\" \"{some_arg2}\" "; 

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
            Console.WriteLine("Results:");
            Console.WriteLine(results);

        }
    }
}
