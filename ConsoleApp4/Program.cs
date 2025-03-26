using System;

class Fraction
{
    public int Numerator { get; private set; } // Числитель
    public int Denominator { get; private set; } // Знаменатель
    public string FractionType { get; private set; } // Тип дроби (правильная, неправильная, целое)
    public string MixedFraction { get; private set; } // Представление в виде смешанной дроби
    public string AbsoluteValue { get; private set; } // Дробь без знака

    public Fraction(int numerator, int denominator)
    {
        if (denominator == 0)
            throw new ArgumentException("Знаменатель не может быть нулем!");

        this.Numerator = numerator;
        this.Denominator = denominator;

        Simplify(); // Упрощаем дробь
        UpdateProperties(); // Обновляем свойства дроби
    }

    private int GCD(int a, int b) // НОД (алгоритм Евклида)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return Math.Abs(a);
    }

    private void Simplify() // Сокращаем дробь
    {
        int gcd = GCD(Numerator, Denominator);
        Numerator /= gcd;
        Denominator /= gcd;
    }

    private void UpdateProperties() // Обновляем свойства дроби
    {
        if (Numerator % Denominator ==0)
        {
            FractionType = "Целое число"; // Например, 6/3 = 2
            MixedFraction = (Numerator / Denominator).ToString();
        }
        else if (Math.Abs(Numerator) >= Math.Abs(Denominator))
        {
            FractionType = "Неправильная"; // Например, 7/3
            int wholePart = Numerator / Denominator;
            int remainder = Math.Abs(Numerator % Denominator);
            MixedFraction = $"{wholePart} {remainder}/{Math.Abs(Denominator)}"; // Например, 7/3 → 2 1/3
        }
        else
        {
            FractionType = "Правильная"; // Например, 3/5
            MixedFraction = $"{Numerator}/{Denominator}";
        }

        AbsoluteValue = $"{Math.Abs(Numerator)}/{Math.Abs(Denominator)}"; // Дробь без знака
    }

    public double ToDecimal() // Перевод в десятичную дробь
    {
        return (double)Numerator / Denominator;
    }

    public static Fraction operator +(Fraction a, Fraction b)
    {
        return new Fraction(a.Numerator * b.Denominator + b.Numerator * a.Denominator, a.Denominator * b.Denominator);
    }

    public static Fraction operator -(Fraction a, Fraction b)
    {
        return new Fraction(a.Numerator * b.Denominator - b.Numerator * a.Denominator, a.Denominator * b.Denominator);
    }

    public static Fraction operator *(Fraction a, Fraction b)
    {
        return new Fraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator);
    }

    public static Fraction operator /(Fraction a, Fraction b)
    {
        if (b.Numerator == 0)
            throw new DivideByZeroException("Деление на ноль невозможно!");
        return new Fraction(a.Numerator * b.Denominator, a.Denominator * b.Numerator);
    }

    public override string ToString()
         {
             return $"{Numerator}/{Denominator}";
         }
}

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Введите первую дробь(числитель , знаменатель)");
            int num1 = int.Parse(Console.ReadLine());
            Console.WriteLine("-");
            int dec1 = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите вторую дробь(числитель , знаменатель)");
            int num2 = int.Parse(Console.ReadLine());
            Console.WriteLine("-");
            int dec2 = int.Parse(Console.ReadLine());

            Fraction f1 = new Fraction(num1, dec1);
            Fraction f2 = new Fraction(num2, dec2);


            // Выводим информацию о дробях
            PrintFractionInfo(f1);
            PrintFractionInfo(f2);

            // Операции с дробями
            Console.WriteLine($"\nСложение: {f1} + {f2} = {f1 + f2}");
            Console.WriteLine($"Вычитание: {f1} - {f2} = {f1 - f2}");
            Console.WriteLine($"Умножение: {f1} * {f2} = {f1 * f2}");
            Console.WriteLine($"Деление: {f1} / {f2} = {f1 / f2}");
            
            Console.WriteLine("Нажмите чтобы продолжить");
            Console.ReadKey();
            Console.Clear();
        }
    }

    static void PrintFractionInfo(Fraction f)
    {
        Console.WriteLine($"\nДробь: {f}");
        Console.WriteLine($"Тип: {f.FractionType}");
        Console.WriteLine($"Смешанная форма: {f.MixedFraction}");
        Console.WriteLine($"Абсолютное значение: {f.AbsoluteValue}");
        Console.WriteLine($"Десятичное представление: {f.ToDecimal():f4}");
    }
}
