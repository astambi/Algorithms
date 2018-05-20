namespace BlackMessup
{
    using System;
    using System.Collections.Generic;

    public class BlackMessup
    {
        private static Dictionary<string, Atom> atoms = new Dictionary<string, Atom>(); // name => atom
        private static Dictionary<string, HashSet<Atom>> graph = new Dictionary<string, HashSet<Atom>>(); // atom => connected atoms

        public static void Main()
        {
            var totalAtoms = int.Parse(Console.ReadLine());
            var totalConnections = int.Parse(Console.ReadLine());

            ReadAtoms(totalAtoms);
            BuildGraph(totalConnections);

            var molecules = FindConnectedComponents();

            var maxMass = FindMaxMass(molecules);
            Console.WriteLine(maxMass);
        }

        private static int FindMaxMass(List<SortedSet<Atom>> molecules)
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

        private static int GetMass(SortedSet<Atom> molecule) // Greedy
        {
            var moleculeMass = 0;
            var maxDecay = 1;
            var count = 0;

            foreach (var atom in molecule)
            {
                if (atom.Decay > maxDecay)
                {
                    maxDecay = atom.Decay;

                    moleculeMass += atom.Mass;
                    count++;
                }
                else if (maxDecay > count)
                {
                    moleculeMass += atom.Mass;
                    count++;
                }
            }

            return moleculeMass;
        }

        private static List<SortedSet<Atom>> FindConnectedComponents()
        {
            var modecules = new List<SortedSet<Atom>>();
            var visited = new HashSet<string>();
            var moleculeIndex = 0;

            foreach (var atomName in graph.Keys)
            {
                if (!visited.Contains(atomName))
                {
                    modecules.Add(new SortedSet<Atom>());
                    MarkConnectedDFS(atomName, visited, modecules, moleculeIndex);

                    moleculeIndex++;
                }
            }

            return modecules;
        }

        private static void MarkConnectedDFS(string atomName, HashSet<string> visited, List<SortedSet<Atom>> molecules, int moleculeIndex)
        {
            visited.Add(atomName);
            molecules[moleculeIndex].Add(atoms[atomName]);

            foreach (var atom in graph[atomName])
            {
                if (!visited.Contains(atom.Name))
                {
                    MarkConnectedDFS(atom.Name, visited, molecules, moleculeIndex);
                }
            }
        }

        private static void BuildGraph(int totalConnections)
        {
            for (int i = 0; i < totalConnections; i++)
            {
                var tokens = Console.ReadLine().Split();

                var firstAtom = tokens[0];
                var secondAtom = tokens[1];

                graph[firstAtom].Add(atoms[secondAtom]);
                graph[secondAtom].Add(atoms[firstAtom]);
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
