namespace BlackMessup
{
    using System;
    using System.Collections.Generic;

    public class BlackMessup
    {
        private static Dictionary<string, Atom> atoms = new Dictionary<string, Atom>(); // atom name => atom
        private static Dictionary<string, HashSet<Atom>> graph = new Dictionary<string, HashSet<Atom>>(); // atom => connected atoms

        public static void Main()
        {
            var totalAtoms = int.Parse(Console.ReadLine());
            var totalConnections = int.Parse(Console.ReadLine());

            ReadAtoms(totalAtoms);
            BuildGraph(totalConnections);

            var molecules = FindConnectedComponents();

            var maxMass = FindMoleculeMaxMass(molecules);
            Console.WriteLine(maxMass);
        }

        private static int FindMoleculeMaxMass(List<SortedSet<Atom>> molecules)
        {
            var maxMass = 0;

            foreach (var molecule in molecules)
            {
                var currentMass = GetMass(molecule);
                if (currentMass > maxMass)
                {
                    maxMass = currentMass;
                }
            }

            return maxMass;
        }

        private static int GetMass(SortedSet<Atom> molecule)
        {
            var totalMass = 0;
            var maxDecay = 1;
            var count = 0;

            foreach (var atom in molecule)
            {
                if (atom.Decay > maxDecay)
                {
                    maxDecay = atom.Decay;

                    totalMass += atom.Mass;
                    count++;
                }
                else if (maxDecay > count)
                {
                    totalMass += atom.Mass;
                    count++;
                }
            }

            return totalMass;
        }

        private static List<SortedSet<Atom>> FindConnectedComponents()
        {
            var visited = new HashSet<string>();

            var modecules = new List<SortedSet<Atom>>();
            var moleculeIndex = 0;

            foreach (var node in graph.Keys)
            {
                if (!visited.Contains(node))
                {
                    modecules.Add(new SortedSet<Atom>());
                    DFS(node, visited, modecules, moleculeIndex);

                    moleculeIndex++;
                }
            }

            return modecules;
        }

        private static void DFS(string node, HashSet<string> visited, List<SortedSet<Atom>> molecules, int moleculeIndex)
        {
            visited.Add(node);

            molecules[moleculeIndex].Add(atoms[node]);

            foreach (var child in graph[node])
            {
                if (!visited.Contains(child.Name))
                {
                    DFS(child.Name, visited, molecules, moleculeIndex);
                }
            }
        }

        private static void BuildGraph(int totalConnections)
        {
            for (int i = 0; i < totalConnections; i++)
            {
                var tokens = Console.ReadLine().Split();

                var firstName = tokens[0];
                var secondName = tokens[1];

                graph[firstName].Add(atoms[secondName]);
                graph[secondName].Add(atoms[firstName]);
            }
        }

        private static void ReadAtoms(int totalAtoms)
        {
            for (int i = 0; i < totalAtoms; i++)
            {
                var tokens = Console.ReadLine().Split();

                var name = tokens[0];
                var mass = int.Parse(tokens[1]);
                var decay = int.Parse(tokens[2]);

                var atom = new Atom(name, mass, decay);

                atoms[atom.Name] = atom;
                graph[atom.Name] = new HashSet<Atom>();
            }
        }

        private class Atom : IComparable<Atom>
        {
            public Atom(string name, int mass, int decay)
            {
                this.Name = name;
                this.Mass = mass;
                this.Decay = decay;
            }

            public string Name { get; }

            public int Mass { get; }

            public int Decay { get; }

            public int CompareTo(Atom other)
                => other.Mass.CompareTo(this.Mass); // DESC
        }
    }
}
