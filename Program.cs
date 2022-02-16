using System;
using System.Collections.Generic;

namespace gmgtool
{
    class Program
    {
        static Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();

        static void Main(string[] args)
        {
            string[] graphLines = System.IO.File.ReadAllLines(args[0]);

            foreach(string graphLine in graphLines) {
                string[] graphTokens = graphLine.Split(' ');
                List<string> graphList;
                if (!graph.TryGetValue(graphTokens[1], out graphList)) {
                    graphList = new List<string>();
                    graph.Add(graphTokens[1], graphList);
                }
                graphList.Add(graphTokens[0]);
            }

            List<List<string>> graphBranches = getGraphBranches(new List<string>(new string[] {args[1]}));
            HashSet<string> graphRoots = new HashSet<string>();

            System.Console.Out.WriteLine("graphBranches:");
            foreach(List<string> graphBranch in graphBranches) {
                foreach(string graphLine in graphBranch) {
                    System.Console.Out.WriteLine(graphLine);
                }
                System.Console.Out.WriteLine();

                graphRoots.Add(graphBranch[graphBranch.Count - 2]);
            }

            System.Console.Out.WriteLine("graphRoots:");
            foreach(string graphRoot in graphRoots) {
                System.Console.Out.WriteLine(graphRoot);
            }
        }

        static List<List<string>> getGraphBranches(List<string> branch) {
            List<List<string>> graphBranches = new List<List<string>>();

            List<string> graphList;
            if (graph.TryGetValue(branch[branch.Count - 1], out graphList)) {
                foreach(string graphItem in graphList) {
                    List<string> parentBranch = new List<string>(branch);
                    parentBranch.Add(graphItem);
                    graphBranches.AddRange(getGraphBranches(parentBranch));
                }
            } else {
                graphBranches.Add(branch);
            }

            return graphBranches;
        }
    }
}
