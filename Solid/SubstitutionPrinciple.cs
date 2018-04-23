using System;

namespace DesignPatterns.Solid
{
    class Rectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public Rectangle()
        {

        }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString() => $"{nameof(Width)}: {Width}, {nameof(Height)}:{Height}";
    }

    #region Wrong approach

    class Square : Rectangle
    {
        public new int Width
        {
            set
            {
                base.Width = base.Height = value;
            }
        }
        public new int Height
        {
            set
            {
                base.Width = base.Height = value;
            }
        }
    }
    #endregion


    class SquareNew : Rectangle
    {
        public override int Width
        {
            set
            {
                base.Width = base.Height = value;
            }
        }
        public override int Height
        {
            set
            {
                base.Width = base.Height = value;
            }
        }
    }

    public class SubstitutionPrinciple
    {
        public static void Test()
        {
            Rectangle rc = new Rectangle(2, 3);
            Console.WriteLine($"{rc} has area {Area(rc)}");

            #region Invalid 
            Rectangle sq = new Square();
            sq.Width = 4;
            Console.WriteLine($"{sq} has area {Area(sq)}");
            #endregion

            Rectangle sqNew = new SquareNew();
            sqNew.Width = 4;
            Console.WriteLine($"{sqNew} has area {Area(sqNew)}");
        }

        private static int Area(Rectangle r) => r.Height * r.Width;
    }
}