using System;
using System.Collections.Generic;
using System.Linq;
namespace DesignPatterns.Solid
{
    public enum Color
    {
        Red, Green, Blue
    }
    public enum Size
    {
        Small, Medium, Large, Yuge
    }
    public class Product
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public Size Size { get; set; }
        public Product(string name, Color color, Size size)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(Name));
            Name = name;
            Color = color;
            Size = size;
        }

        public override string ToString() => $"{this.Name}  => Color : {this.Color}, Size : {this.Size}";
    }

    #region Non Open Closure Principle

    public class ProductFilter
    {
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach (var product in products)
            {
                if (product.Size == size)
                    yield return product;
            }
        }
        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (var product in products)
            {
                if (product.Color == color)
                    yield return product;
            }
        }
        public IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Size size, Color color)
        {
            foreach (var product in products)
            {
                if (product.Size == size && product.Color == color)
                    yield return product;
            }
        }
    }

    #endregion

    #region Open Closure Principle

    public interface ISpecification<T>
    {
        bool IsSatisfied(T item);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> specification);
    }

    public class ColorSpecification : ISpecification<Product>
    {
        private Color _color;

        public ColorSpecification(Color color)
        {
            _color = color;
        }
        public bool IsSatisfied(Product item) => item.Color == _color;
    }

    public class SizeSpecification : ISpecification<Product>
    {
        private Size _size;

        public SizeSpecification(Size size)
        {
            _size = size;
        }
        public bool IsSatisfied(Product item) => item.Size == _size;
    }
    public class AndSpecification<T> : ISpecification<T>
    {
        ISpecification<T> _first, _second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            if (first == null || second == null)
                throw new ArgumentNullException();
            _first = first;
            _second = second;
        }

        public bool IsSatisfied(T item) => _first.IsSatisfied(item) && _second.IsSatisfied(item);

    }

    public class ProductFilterSolid : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> specification)
        {
            foreach (var product in items)
            {
                if (specification.IsSatisfied(product))
                    yield return product;
            }
        }
    }


    public class OpenClosurePrinciple
    {
        public static void Test()
        {
            var products = new List<Product>
            {
                new Product("Apple", Color.Red, Size.Small),
                new Product("Tree", Color.Green, Size.Large),
                new Product("House", Color.Red, Size.Large)
            };
            Console.WriteLine("Without Open Closure Principle");
            var pf = new ProductFilter();
            foreach (var product in pf.FilterByColor(products, Color.Red))
                Console.WriteLine(product);


            Console.WriteLine("Open CLosure Principle");
            var productNewFilter = new ProductFilterSolid();
            foreach (var product in productNewFilter.Filter(products, new ColorSpecification(Color.Red)))
                Console.WriteLine(product);


            Console.WriteLine("Large Size and Green Color");
            foreach (var product in productNewFilter.Filter(products, new AndSpecification<Product>(new ColorSpecification(Color.Green), new SizeSpecification(Size.Large))))
                Console.WriteLine(product);
        }
    }

    #endregion

}