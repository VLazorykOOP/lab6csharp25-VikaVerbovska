using System;
using System.Collections;
using System.Collections.Generic;

// Користувацькі інтерфейси
interface IPerson
{
    string Name { get; set; }
    int Age { get; set; }
    void Show();
}

interface IWorker
{
    string Position { get; set; }
}

interface IEngineer
{
    string Specialization { get; set; }
}

class Persona : IPerson, ICloneable
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Persona(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public virtual void Show()
    {
        Console.WriteLine($"Persona: {Name}, Age: {Age}");
    }

    public object Clone()
    {
        return new Persona(Name, Age);
    }
}

class Slujbovec : Persona, IWorker
{
    public string Position { get; set; }

    public Slujbovec(string name, int age, string position)
        : base(name, age)
    {
        Position = position;
    }

    public override void Show()
    {
        Console.WriteLine($"Slujbovec: {Name}, Age: {Age}, Position: {Position}");
    }
}

class Robitnik : Slujbovec
{
    public string Department { get; set; }

    public Robitnik(string name, int age, string position, string department)
        : base(name, age, position)
    {
        Department = department;
    }

    public override void Show()
    {
        Console.WriteLine($"Robitnik: {Name}, Age: {Age}, Position: {Position}, Department: {Department}");
    }
}

class Inzhener : Robitnik, IEngineer
{
    public string Specialization { get; set; }

    public Inzhener(string name, int age, string position, string department, string specialization)
        : base(name, age, position, department)
    {
        Specialization = specialization;
    }

    public override void Show()
    {
        Console.WriteLine($"Inzhener: {Name}, Age: {Age}, Position: {Position}, Department: {Department}, Specialization: {Specialization}");
    }
}

// Завдання 2 - інтерфейс для обчислення функцій
interface IFunction
{
    double Calculate(double x);
}

class Line : IFunction
{
    public double a, b;

    public Line(double a, double b)
    {
        this.a = a;
        this.b = b;
    }

    public double Calculate(double x)
    {
        return a * x + b;
    }
}

class Kub : IFunction
{
    public double a, b, c;

    public Kub(double a, double b, double c)
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }

    public double Calculate(double x)
    {
        return a * x * x + b * x + c;
    }
}

class Hyperbola : IFunction
{
    public double a, b;

    public Hyperbola(double a, double b)
    {
        this.a = a;
        this.b = b;
    }

    public double Calculate(double x)
    {
        return a / x + b;
    }
}

// Завдання 3 - Обробка помилок
class CustomException : Exception
{
    public CustomException(string message) : base(message) { }
}

class DivideByZeroError : CustomException
{
    public DivideByZeroError() : base("Ділення на нуль неможливе!") { }
}

class Program
{
    static void TestPersona()
    {
        try
        {
            Persona person = new Persona("Ivan", 25);
            person.Show();

            Slujbovec employee = new Slujbovec("Petr", 30, "Manager");
            employee.Show();

            Robitnik worker = new Robitnik("Sergiy", 40, "Worker", "IT");
            worker.Show();

            Inzhener engineer = new Inzhener("Olga", 35, "Engineer", "R&D", "Software");
            engineer.Show();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void TestFunctions()
    {
        IFunction line = new Line(2, 3);
        IFunction kub = new Kub(1, -2, 1);
        IFunction hyperbola = new Hyperbola(1, 0);

        double x = 5;
        Console.WriteLine($"Line: {line.Calculate(x)}");
        Console.WriteLine($"Kub: {kub.Calculate(x)}");
        Console.WriteLine($"Hyperbola: {hyperbola.Calculate(x)}");
    }

    static void TestExceptions()
    {
        try
        {
            int numerator = 10;
            int denominator = 0;

            if (denominator == 0)
            {
                throw new DivideByZeroError(); 
            }

            int result = numerator / denominator;
            Console.WriteLine($"Результат: {result}");
        }
        catch (DivideByZeroError ex)
        {
            Console.WriteLine(ex.Message); 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }


    static void TestTriangle()
    {
        Triangle triangle = new Triangle(3, 4, 5, 7);

        foreach (int value in triangle)
        {
            Console.WriteLine(value); 
        }
    }

    static void Main(string[] args)
    {
   
        TestPersona();
        Console.WriteLine();
   
        TestFunctions();
        Console.WriteLine();

        TestExceptions();
        Console.WriteLine();

        TestTriangle();
    }
}

class Triangle : IEnumerable<int>
{
    private int a, b, c;
    private int color;

    public Triangle(int sideA, int sideB, int sideC, int color)
    {
        if (sideA + sideB > sideC && sideA + sideC > sideB && sideB + sideC > sideA)
        {
            a = sideA;
            b = sideB;
            c = sideC;
            this.color = color;
        }
        else
        {
            throw new ArgumentException("Неможливо створити трикутник з такими сторонами!");
        }
    }

    public int this[int index]
    {
        get
        {
            if (index == 0) return a;
            if (index == 1) return b;
            if (index == 2) return c;
            if (index == 3) return color;
            throw new IndexOutOfRangeException("Неправильний індекс");
        }
        set
        {
            if (index == 0) a = value;
            else if (index == 1) b = value;
            else if (index == 2) c = value;
            else if (index == 3) color = value;
            else throw new IndexOutOfRangeException("Неправильний індекс");
        }
    }

    public static Triangle operator ++(Triangle t)
    {
        return new Triangle(t.a + 1, t.b + 1, t.c + 1, t.color);
    }

    public static Triangle operator --(Triangle t)
    {
        return new Triangle(t.a - 1, t.b - 1, t.c - 1, t.color);
    }

    public static Triangle operator *(Triangle t, int scalar)
    {
        return new Triangle(t.a * scalar, t.b * scalar, t.c * scalar, t.color);
    }

    public static bool operator true(Triangle t)
    {
        return t.a + t.b > t.c && t.a + t.c > t.b && t.b + t.c > t.a;
    }

    public static bool operator false(Triangle t)
    {
        return !(t.a + t.b > t.c && t.a + t.c > t.b && t.b + t.c > t.a);
    }

    public static explicit operator string(Triangle t)
    {
        return $"{t.a},{t.b},{t.c},{t.color}";
    }

    public static explicit operator Triangle(string str)
    {
        string[] parts = str.Split(',');
        return new Triangle(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]));
    }

    public void PrintInfo()
    {
        Console.WriteLine($"Сторони: {a}, {b}, {c}, Колір: {color}");
    }

    public IEnumerator<int> GetEnumerator()
    {
        yield return a;
        yield return b;
        yield return c;
        yield return color;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
