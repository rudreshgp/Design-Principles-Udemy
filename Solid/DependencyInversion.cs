using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatterns.Solid
{

    public enum RelationShip
    {
        Parent,
        Child,
        Sibling
    }

    public class Person
    {
        public string Name { get; set; }

    }
    //Low Level

    public class RelationShips : IRelationshipBrowser
    {
        private List<(Person, RelationShip, Person)> relations = new List<(Person, RelationShip, Person)>();

        public List<(Person, RelationShip, Person)> Relations => relations;
        public void AddParentAndChild(Person parent, Person child)
        {
            relations.Add((parent, RelationShip.Parent, child));
            relations.Add((child, RelationShip.Child, parent));
        }

        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
            foreach (var r in relations.Where(x => x.Item1.Name == name && x.Item2 == RelationShip.Parent))
            {
                yield return r.Item3;
            }
        }
    }

    public interface IRelationshipBrowser
    {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }

    public class DependencyInversion
    {

        // public DependencyInversion(RelationShips relationShips)
        // {
        //     var relations = relationShips.Relations;
        //     foreach (var r in relations.Where(x => x.Item1.Name == "John" && x.Item2 == RelationShip.Parent))
        //     {
        //         Console.WriteLine($"John has a child called {r.Item3.Name}");
        //     }
        // }

        public DependencyInversion(IRelationshipBrowser browser)
        {
            foreach (var p in browser.FindAllChildrenOf("John"))
            {
                Console.WriteLine($"John has a child called {p.Name}");
            }
        }

        public static void Test()
        {
            var parent = new Person() { Name = "John" };
            var child1 = new Person() { Name = "Chris" };
            var child2 = new Person() { Name = "Mary" };
            var relationships = new RelationShips();
            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);

            new DependencyInversion(relationships);
        }
    }
}