using System;
using System.Collections.Generic;

namespace DesignPatterns.Solid
{
    public class SingleResponsibility
    {
        private readonly List<string> _entries = new List<string>();

        private int count = 0;
        public int AddEntry(string entry)
        {
            _entries.Add($"{++count}: {entry}");
            return count;
        }
        public void RemoveEntry(int index)
        {
            _entries.RemoveAt(index);
        }

        public override string ToString() => string.Join(Environment.NewLine, this._entries);
    }

    public class SolidPrinciple
    {
        public static void Test(string[] args)
        {
            var j = new SingleResponsibility();
            j.AddEntry("I Cried Today!");
            j.AddEntry("I ate a bug");
            Console.WriteLine(j);
        }
    }
}