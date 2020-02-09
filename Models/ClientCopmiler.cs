using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows;
using System.Reflection;
using Monitoring.Properties;

namespace Monitoring.Models
{
    class ClientCopmiler
    {
        public ClientCopmiler(string ip)
        {
            string name = "client_monitoring";

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters parameters = new CompilerParameters();
            

            parameters.GenerateExecutable = true;
            parameters.OutputAssembly = $"{name}.exe";
            parameters.GenerateInMemory = false;
            parameters.TreatWarningsAsErrors = false;
            parameters.ReferencedAssemblies.Add("System.Management.dll");
            parameters.ReferencedAssemblies.Add("System.dll");


            //CompilerResults results = provider.CompileAssemblyFromFile(parameters, $"{AppDomain.CurrentDomain.BaseDirectory}/{name}.tuo");

            CompilerResults results = provider.CompileAssemblyFromSource(parameters, $"{Resources.part2}{ip}{Resources.part3}");

            if (results.Errors.HasErrors)
            {
                foreach (CompilerError item in results.Errors)
                {
                    MessageBox.Show(item.ToString());
                }
            }
            else
            {
                MessageBox.Show("Program Compileted", "Successful");
                //MessageBox.Show(results.CompiledAssembly.Location);

                Process PrFolder = new Process();
                ProcessStartInfo psi = new ProcessStartInfo();
                string file = $@"{results.CompiledAssembly.Location}";
                psi.CreateNoWindow = true;
                psi.WindowStyle = ProcessWindowStyle.Normal;
                psi.FileName = "explorer";
                psi.Arguments = @"/n, /select, " + file;
                PrFolder.StartInfo = psi;
                PrFolder.Start();

                //Process.Start($@"{results.CompiledAssembly.Location.Replace($"{name}.exe","")}");
            }
        }
    }
    }
