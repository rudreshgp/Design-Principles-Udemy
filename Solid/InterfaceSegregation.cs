namespace DesignPatterns.Solid
{

    public class Document { }

    public interface IMachine
    {
        void Print(Document d);
        void Scan(Document d);
        void Fax(Document d);
    }

    public class MultiFunctionPrinter : IMachine
    {
        public void Fax(Document d)
        {
            //
        }

        public void Print(Document d)
        {
            //
        }

        public void Scan(Document d)
        {
            //
        }
    }


    public class OldFashionedPrinter : IMachine
    {
        public void Fax(Document d)
        {
            // This is why interfaces need to be split
            throw new System.NotImplementedException();
        }

        public void Print(Document d)
        {
            //
        }

        public void Scan(Document d)
        {
            throw new System.NotImplementedException();
        }
    }


    public interface IPrinter
    {
        void Print(Document document);
    }


    public interface IScanner
    {
        void Scan(Document document);
    }

    public interface IFax
    {
        void Fax(Document document);
    }

    public class PhotoCopy : IPrinter, IScanner  // Create a interface inheriting from these two interfaces
    {
        private IPrinter _printer;
        private IScanner _scanner;

        public PhotoCopy(IPrinter printer, IScanner scanner)
        {
            _printer = printer;
            _scanner = scanner;
        }
 
        //Declarator Pattern

        public void Print(Document document)
        {
            _printer.Print(document);
        }

        public void Scan(Document document)
        {
            _scanner.Scan(document);
        }
    }

    public class InterfaceSegregation
    {
        public static void Test()
        {

        }
    }
}