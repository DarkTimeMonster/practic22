using System;
using System.Collections.Generic;
using System.Linq;

class Bilet
{
    public string StudentName { get; set; }
    public string Question1 { get; set; }
    public string Question2 { get; set; }
    public string Task { get; set; }
    public int Score1 { get; set; }
    public int Score2 { get; set; }
    public int TaskScore { get; set; }
    public int ExtraScore { get; set; } = -1;
    public string ExamResult { get; set; }
    public DateTime? RetakeDate { get; set; }
}

class Program
{
    static List<string> questions = new List<string>
    {
        "Что такое ООП?",
        "Объясните принцип инкапсуляции",
        "Расскажите про полиморфизм",
        "Что такое абстракция?",
        "Какие бывают модификаторы доступа в C#?",
        "Что такое интерфейс?",
        "Объясните принцип SOLID"
    };

    static List<string> tasks = new List<string>
    {
        "Напишите класс с наследованием",
        "Создайте интерфейс и реализуйте его",
        "Реализуйте инкапсуляцию в классе",
        "Напишите программу с полиморфизмом"
    };

    static Random rand = new Random();

    static int AskQuestion(string question)
    {
        Console.WriteLine($"Ответьте на вопрос: {question} (Введите оценку от 2 до 5): ");
        int score;
        while (!int.TryParse(Console.ReadLine(), out score) || score < 2 || score > 5)
        {
            Console.WriteLine("Некорректный ввод. Введите число от 2 до 5: ");
        }
        return score;
    }

    static void ConductExam(Bilet student)
    {
        Console.WriteLine($"\nСтудент {student.StudentName} выбирает билет...");
        Console.WriteLine($"Вопрос 1: {student.Question1}");
        student.Score1 = AskQuestion(student.Question1);

        Console.WriteLine($"Вопрос 2: {student.Question2}");
        student.Score2 = AskQuestion(student.Question2);

        Console.WriteLine($"Задача: {student.Task}");
        student.TaskScore = AskQuestion(student.Task);

        if (student.Score1 == 2 || student.Score2 == 2)
        {
            string extraQuestion = questions[rand.Next(questions.Count)];
            Console.WriteLine("Дополнительный вопрос назначен!");
            student.ExtraScore = AskQuestion(extraQuestion);
        }

        int totalScore = student.Score1 + student.Score2 + student.TaskScore + (student.ExtraScore > 0 ? student.ExtraScore : 0);
        int divisor = (student.ExtraScore > 0) ? 4 : 3;
        double average = (double)totalScore / divisor;

        student.ExamResult = average >= 3 ? "Зачет" : "Неудовлетворительно";

        if (student.ExamResult == "Неудовлетворительно")
        {
            student.RetakeDate = DateTime.Now.AddDays(7);
        }
    }

    static void DisplayResults(List<Bilet> students)
    {
        Console.WriteLine("\nРезультаты экзамена:");
        foreach (var student in students)
        {
            Console.WriteLine($"{student.StudentName}: {student.ExamResult}" +
                $"{(student.RetakeDate.HasValue ? $", Пересдача: {student.RetakeDate.Value.ToShortDateString()}" : "")}");
        }
    }

    static void Main()
    {
        List<Bilet> students = new List<Bilet>();
        Console.Write("Введите количество студентов: ");
        int studentCount;
        while (!int.TryParse(Console.ReadLine(), out studentCount) || studentCount <= 0)
        {
            Console.WriteLine("Ошибка! Введите положительное число.");
        }

        for (int i = 0; i < studentCount; i++)
        {
            Console.Write($"\nВведите имя студента {i + 1}: ");
            string name = Console.ReadLine();

            string question1 = questions[rand.Next(questions.Count)];
            string question2;
            do
            {
                question2 = questions[rand.Next(questions.Count)];
            } while (question2 == question1); 

            string task = tasks[rand.Next(tasks.Count)];

            students.Add(new Bilet { StudentName = name, Question1 = question1, Question2 = question2, Task = task });
        }

        foreach (var student in students)
        {
            ConductExam(student);
        }

        DisplayResults(students);
    }
}
