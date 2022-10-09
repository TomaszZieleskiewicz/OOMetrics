using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;
using OOMetrics.Abstractions.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOMertics.Helper.Tests
{
    public class ExploratoryTests
    {
        [Fact]
        public async Task HowToHandleSolution()
        {
            var solutionFilePath = Directory.GetFiles($"{TestPathBase.SolutionLocation}{TestPathBase.TestSolutionDir}", $"{TestPathBase.TestSolutionName}.sln", SearchOption.AllDirectories).Single();
            if (!MSBuildLocator.IsRegistered)
            {
                MSBuildLocator.RegisterDefaults();
            }
            
            var workspace = MSBuildWorkspace.Create();
            
            await workspace.OpenSolutionAsync(solutionFilePath);
            var projects = workspace.CurrentSolution.Projects;
            var printout = new HashSet<string>();
            var allDependencies = new HashSet<INamedTypeSymbol>();
            foreach (var project in projects)
            {
                var compilation = await project.GetCompilationAsync();
                var documents = projects.SelectMany(p => p.Documents).ToArray();
                foreach (var document in documents)
                {
                    var semanticModel = await document.GetSemanticModelAsync();
                    if(semanticModel == null)
                    {
                        throw new Exception($"Can not find semantic model for {document.Name}");
                    }
                    var syntaxNodes = semanticModel.SyntaxTree.GetRoot().DescendantNodes(n => true).ToArray();
                    var declarations = syntaxNodes.OfType<BaseTypeDeclarationSyntax>().ToArray();
                    foreach(var declaration in declarations)
                    {
                        if(declaration.Identifier.ToString()== "ClassUsingTypesFromOtherProject")
                        {
                            var descendantNodes = declarations.SelectMany(d => d.DescendantNodes(n => true)).ToArray();

                            var namedTypes = descendantNodes
                                .OfType<IdentifierNameSyntax>()
                                .Select(ins => semanticModel.GetSymbolInfo(ins).Symbol)
                                .ToHashSet(SymbolEqualityComparer.Default)
                                .OfType<INamedTypeSymbol>();

                            var expressionTypes = descendantNodes
                                .OfType<ExpressionSyntax>()
                                .Select(es => semanticModel.GetTypeInfo(es).Type)
                                .ToHashSet(SymbolEqualityComparer.Default)
                                .OfType<INamedTypeSymbol>();
                            allDependencies.UnionWith(namedTypes);
                            allDependencies.UnionWith(expressionTypes);
                            foreach (var type in allDependencies.Where(t=> t!=null))
                            {
                                printout.Add($"Name: {type.Name}, ContainingAssembly:{type.ContainingAssembly.Name}, Kind:{type.Kind}, TypeKind:{type.TypeKind}, IsAbstract: {type.IsAbstract}");
                            }
                            // Przerobić DeclarationHandler na faktorkę do Deklaracji i Deklaracji jako zależności.
                        }
                    }
                }
            }
        }
    }
}
