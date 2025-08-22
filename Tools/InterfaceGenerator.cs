using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;

class InterfaceGenerator
{
    static void Main()
    {
        var classFile = "/home/runner/work/grunt/grunt/Grunt/Grunt/Core/HaloInfiniteClient.cs";
        var interfaceFile = "/home/runner/work/grunt/grunt/Grunt/Grunt/Core/IHaloInfiniteClient.cs";
        
        GenerateInterface(classFile, interfaceFile);
    }
    
    static void GenerateInterface(string classFilePath, string interfaceFilePath)
    {
        var classContent = File.ReadAllText(classFilePath);
        var lines = classContent.Split('\n');
        
        var interfaceContent = new StringBuilder();
        
        // Add header
        interfaceContent.AppendLine("// <copyright file=\"IHaloInfiniteClient.cs\" company=\"Den Delimarsky\">");
        interfaceContent.AppendLine("// Developed by Den Delimarsky.");
        interfaceContent.AppendLine("// Den Delimarsky licenses this file to you under the MIT license.");
        interfaceContent.AppendLine("// See the LICENSE file in the project root for more information.");
        interfaceContent.AppendLine("// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.");
        interfaceContent.AppendLine("// </copyright>");
        interfaceContent.AppendLine();
        interfaceContent.AppendLine("// THIS FILE IS AUTO-GENERATED. DO NOT EDIT MANUALLY.");
        interfaceContent.AppendLine("// Generated from HaloInfiniteClient.cs");
        interfaceContent.AppendLine("// Any changes to public methods in HaloInfiniteClient will need to be reflected here.");
        interfaceContent.AppendLine();
        interfaceContent.AppendLine("using System;");
        interfaceContent.AppendLine("using System.Collections.Generic;");
        interfaceContent.AppendLine("using System.Threading.Tasks;");
        interfaceContent.AppendLine("using Surprenant.Grunt.Models;");
        interfaceContent.AppendLine("using Surprenant.Grunt.Models.HaloInfinite;");
        interfaceContent.AppendLine("using Surprenant.Grunt.Models.HaloInfinite.ApiIngress;");
        interfaceContent.AppendLine("using Surprenant.Grunt.Models.HaloInfinite.Medals;");
        interfaceContent.AppendLine();
        interfaceContent.AppendLine("namespace Surprenant.Grunt.Core;");
        interfaceContent.AppendLine();
        interfaceContent.AppendLine("/// <summary>");
        interfaceContent.AppendLine("/// Interface for client used to access the Halo Infinite API surface.");
        interfaceContent.AppendLine("/// This interface is automatically generated from the HaloInfiniteClient class.");
        interfaceContent.AppendLine("/// </summary>");
        interfaceContent.AppendLine("public interface IHaloInfiniteClient");
        interfaceContent.AppendLine("{");
        
        bool inClass = false;
        int braceLevel = 0;
        
        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();
            
            // Track when we're inside the class
            if (trimmedLine.Contains("public class HaloInfiniteClient"))
            {
                inClass = true;
                continue;
            }
            
            if (inClass)
            {
                // Track brace levels
                braceLevel += CountOccurrences(line, '{') - CountOccurrences(line, '}');
                
                // If we've closed all braces, we're out of the class
                if (braceLevel <= 0 && trimmedLine == "}")
                {
                    break;
                }
                
                // Extract public properties and methods
                if (trimmedLine.StartsWith("public string") && (trimmedLine.Contains("SpartanToken") || trimmedLine.Contains("Xuid") || trimmedLine.Contains("ClearanceToken")))
                {
                    var propertyLine = trimmedLine.Replace(" = string.Empty;", " { get; set; }");
                    interfaceContent.AppendLine($"    {propertyLine}");
                }
                else if (trimmedLine.StartsWith("public async Task<") && !trimmedLine.Contains("HaloInfiniteClient("))
                {
                    var methodLine = trimmedLine.Replace("public async ", "").Replace("public ", "");
                    if (methodLine.EndsWith(")"))
                    {
                        methodLine += ";";
                    }
                    else if (methodLine.Contains("{"))
                    {
                        methodLine = methodLine.Substring(0, methodLine.IndexOf('{')) + ";";
                    }
                    interfaceContent.AppendLine($"    {methodLine}");
                }
            }
        }
        
        interfaceContent.AppendLine("}");
        
        File.WriteAllText(interfaceFilePath, interfaceContent.ToString());
        Console.WriteLine("Interface generated successfully!");
    }
    
    static int CountOccurrences(string text, char character)
    {
        int count = 0;
        foreach (char c in text)
        {
            if (c == character) count++;
        }
        return count;
    }
}