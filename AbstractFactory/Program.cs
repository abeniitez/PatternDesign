using System;

namespace AbstractFactory
{   
     // La interfaz Abstract Factory declara un conjunto de métodos que devuelven diferentes productos abstractos. Estos productos se denominan familia y están relacionados por un tema o concepto de alto nivel. Los productos de una familia suelen poder colaborar entre sí. Una familia de productos puede tener varias variantes, pero los productos de una variante son incompatibles con productos de otro.
    public interface IAbstractFactory
    {
        IAbstractProductA CreateProductA();
        IAbstractProductB CreateProductB();
        
    }


    //Las fábricas producen una familia de productos que pertenecen a un solo
    //variante. La fábrica garantiza que los productos resultantes son compatibles.
    //Tenga en cuenta que las firmas de los métodos de Concrete Factory devuelven un resumen
    //producto, mientras que dentro del método se instancia un producto concreto.
    class ConcreteFactory1 : IAbstractFactory
    {
        public IAbstractProductA CreateProductA()
        {
            return new ConcreteProductA1();
        }

        public IAbstractProductB CreateProductB()
        {
            return new ConcreteProductB1();
        }
    }

    // Cada Fábrica tiene una variante de producto correspondiente.
    class ConcreteFactory2 : IAbstractFactory
    {
        public IAbstractProductA CreateProductA()
        {
            return new ConcreteProductA2();
        }

        public IAbstractProductB CreateProductB()
        {
            return new ConcreteProductB2();
        }
    }

    //Cada producto distinto de una familia de productos debe tener una interfaz básica.
    //Todas las variantes del producto deben implementar esta interfaz.
    public interface IAbstractProductA
    {
        string UsefulFunctionA();
        
    }

    // Los productos de hormigón son creados por las correspondientes fábricas de hormigón.
    class ConcreteProductA1 : IAbstractProductA
    {
        public string UsefulFunctionA()
        {
            return "The result of the product A1.";
        }
        
    }

    class ConcreteProductA2 : IAbstractProductA
    {
        public string UsefulFunctionA()
        {
            return "The result of the product A2.";

        }
    }

    // Aquí está la interfaz base de otro producto. Todos los productos pueden
    // interactuar entre sí, pero la interacción adecuada solo es posible entre
    // productos de la misma variante concreta.
    public interface IAbstractProductB
    {
        // El producto B puede hacer sus propias cosas ...
        string UsefulFunctionB();

        // ... pero también puede colaborar con ProductA.
        //
        // The Abstract Factory se asegura de que todos los productos que crea sean de
        // la misma variante y, por tanto, compatible.
        string AnotherUsefulFunctionB(IAbstractProductA collaborator);


    }

    // Los productos de hormigón son creados por las correspondientes fábricas de hormigón.
    class ConcreteProductB1 : IAbstractProductB
    {
        public string UsefulFunctionB()
        {
            return "The result of the product B1.";
        }

        // La variante, Producto B1, solo puede funcionar correctamente con el
        // variante, Producto A1. No obstante, acepta cualquier instancia de
        // AbstractProductA como argumento.
        public string AnotherUsefulFunctionB(IAbstractProductA collaborator)
        {
            var result = collaborator.UsefulFunctionA();

            return $"The result of the B1 collaborating with the ({result})";
        }
    }

    class ConcreteProductB2 : IAbstractProductB
    {
        public string UsefulFunctionB()
        {
            return "The result of the product B2.";
        }

       // La variante, Producto B2, solo puede funcionar correctamente con el
       // variante, Producto A2. No obstante, acepta cualquier instancia de
       // AbstractProductA como argumento.
        public string AnotherUsefulFunctionB(IAbstractProductA collaborator)
        {
            var result = collaborator.UsefulFunctionA();

            return $"The result of the B2 collaborating with the ({result})";
        }
    }

    // El código de cliente funciona con fábricas y productos solo a través de resumen
    // tipos: AbstractFactory y AbstractProduct. Esto le permite pasar cualquier
    // fábrica o subclase de producto al código del cliente sin romperlo.
    class Client
    {
        public void Main()
        {
            // El código del cliente puede funcionar con cualquier clase de fábrica concreta.
            Console.WriteLine("Client: Testing client code with the first factory type...");
            ClientMethod(new ConcreteFactory1());
            Console.WriteLine();

            Console.WriteLine("Client: Testing the same client code with the second factory type...");
            ClientMethod(new ConcreteFactory2());
        }

        public void ClientMethod(IAbstractFactory factory)
        {
            var productA = factory.CreateProductA();
            var productB = factory.CreateProductB();

            Console.WriteLine(productB.UsefulFunctionB());
            Console.WriteLine(productB.AnotherUsefulFunctionB(productA));
        }
    }




    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
