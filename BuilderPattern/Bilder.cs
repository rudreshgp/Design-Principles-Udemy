using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.BuilderPattern
{

    class HtmlElement
    {
        public string Name, Text;
        public List<HtmlElement> Elements = new List<HtmlElement>();
        private const int _indentSize = 2;

        public HtmlElement()
        {

        }

        public HtmlElement(string name, string text)
        {
            Name = name ?? throw new ArgumentException();
            Text = text ?? throw new ArgumentException();
        }
        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', _indentSize * indent);
            sb.AppendLine($"{i}<{Name}>");
            if (!string.IsNullOrWhiteSpace(Text))
            {
                sb.Append(new string(' ', _indentSize * (indent + 1)));
                sb.AppendLine(Text);
            }
            foreach (var element in Elements)
            {
                sb.AppendLine(element.ToStringImpl(indent + 1));
            }
            sb.AppendLine($"{i}</{Name}>");
            return sb.ToString();
        }


        public override string ToString() => this.ToStringImpl(0);
    }

    class HtmlBuilder
    {
        private readonly string _rootName;
        public HtmlElement Root = new HtmlElement();
        public HtmlBuilder(string rootName)
        {
            _rootName = rootName;
            Root.Name = _rootName;
        }

        public HtmlBuilder AddChild(string childName, string childText)
        {
            var e = new HtmlElement(childName, childText);
            Root.Elements.Add(e);
            return this; // Returning refernece to working object is the main goal of Builder pattern
        }

        public override string ToString() => Root.ToString();

        public void Clear()
        {
            Root = new HtmlElement
            {
                Name = _rootName
            };
        }
    }

    class Builder
    {
        public static void Test()
        {
            // var hello = "hello";
            // StringBuilder sb = new StringBuilder();
            // sb.Append("<p>");
            // sb.Append(hello);
            // sb.Append("</p>");

            // //Html list containing two lists

            // var words = new[] { "hello", "world" };
            // sb.Clear();
            // sb.Append("<ul>");
            // foreach (var word in words)
            // {
            //     sb.AppendFormat($"<li>{word}</li>");
            // }
            // sb.Append("</ul>");

            // //Use some HTML Builder instead of low level builders
            // Console.WriteLine(sb);
            var builder = new HtmlBuilder("ul");
            builder.AddChild("li","hello").AddChild("li","world");
            Console.WriteLine(builder);
        }
    }
}