using System.IO;
using System.Text;

namespace Kalyuzhnaya_Project_1
{
    /*
    * Дисциплина: "Программирование"
    * Группа: БПИ245
    * Студент: Калюжная Анна Дмитриевна
    * Дата: 03.09.2024
    * Задача: Получить из input.txt данные, обработать по формуле каждое из корректных значений,
    * вывести результат в output.txt.
    */
    internal class Program
    {
        /// <summary>
        /// Данный метод предназначен для считывания данных из файла 
        /// input.txt и обработки возможных исключений.
        /// </summary>
        /// <returns>Строка с корректными данными extraString</returns>
        public static string ReadAll()
        {
            string path = @"../../../../input.txt"; //переменная для хранения пути файла.
            string readText;
            string separatingStrings = ";";

            while (true) 
            {
                try
                {
                    readText = File.ReadAllText(path);
                    if (readText.Length == 0) // проверяем не пустой ли файл.
                    {
                        throw new ArgumentException("Пустой файл");
                    }
                    string[] values = readText.Replace("\n", ";").Split(separatingStrings, StringSplitOptions.RemoveEmptyEntries);
                    string extraString = ""; // вводим дополнительную строку для хранения корректных значений.
                    for (int i = 0; i < values.GetLength(0); i++) // цикл выявления корректных значений в файле.
                    {
                        try
                        {
                            double element = double.Parse(values[i].Replace('.', ','));
                            extraString += $"{element.ToString()} ";
                        } catch
                        {
                            continue;
                        }
                    }
                    if (extraString.Length == 0) // проверяем остались ли корректные значения в файле.
                    {
                        throw new ArgumentException("Отсутствуют корректные данные");
                    }
                    return extraString;
                }
                //обрабатываем возможные ошибки
                catch (ArgumentException ex)
                {
                    if (ex.Message == "Пустой файл")
                    {
                        Console.WriteLine("Передан пустой файл. Повторите попытку.");
                        return "fake";
                    }
                    else if (ex.Message == "Отсутствуют корректные данные")
                    {
                        Console.WriteLine("Корректных данных в файле нет. Повторите попытку.");
                        return "fake";
                    }
                }
                catch (IOException)
                {
                    Console.WriteLine("Проблемы с открытием файла. Повторите попытку.");
                    return "fake";
                }
                catch (NotSupportedException)
                {
                    Console.WriteLine("Путь имеет недопустимый формат. Повторите попытку.");
                    return "fake";
                }
                catch (Exception)
                {
                    Console.WriteLine("Непредвиденная ошибка. Повторите попытку.");
                    return "fake";
                }
            } 
        }

        /// <summary>
        /// Данный метод предназначен для записи данных в файлы
        /// output-[номер].txt и обработки возможных исключений.
        /// </summary>
        /// <param name="extraString">Строка с корректными данными</param>
        /// <param name="r">Введенное пользователем значение r</param>
        public static void WriteAll(double r, string extraString)
        {
            try
            {
                string textToTransform = Solution(r, extraString);
                if (textToTransform != "mistake")
                {
                    try
                    {
                        string textFromConfig = File.ReadAllText(@"../../../../config.txt");
                        if (textFromConfig.Length == 0)
                        {
                            File.WriteAllText(@"../../../../output-1.txt", textToTransform, Encoding.UTF8);
                            File.WriteAllText(@"../../../../config.txt", "1", Encoding.UTF8);
                        }
                        else
                        {
                            int number = int.Parse(textFromConfig);
                            File.WriteAllText($@"../../../../output-{(number + 1).ToString()}.txt", textToTransform, Encoding.UTF8);
                            File.WriteAllText(@"../../../../config.txt", $"{(number + 1).ToString()}", Encoding.UTF8);
                        }
                    } 
                    catch (Exception)
                    {
                        Console.WriteLine("Ошибка, связанная с файлом config.txt. Проверьте, все ли с ним впорядке и повторите попытку.");
                    }
                } else
                {
                    throw new ArgumentException("Данные неверны");
                }
            }
            // обрабатываем возможные ошибки.
            catch (ArgumentException)
            {
                Console.WriteLine("Данные неверны. Повторите попытку.");
            }
            catch (IOException)
            {
                Console.WriteLine("Проблемы с открытием выбранного для сохранения файла. Повторите попытку.");
            }
            catch (NotSupportedException)
            {
                Console.WriteLine("Путь к сохранению файла имеет недопустимый формат. Повторите попытку.");
            }
        }

        /// <summary>
        /// Данный метод является формулой вычисления необходимых значений.
        /// </summary>
        /// <param name="x">Массив с элементами</param>
        /// <param name="counter">k-й номер значения</param>
        /// <param name="r">Введенное пользователем значение r</param>
        /// <returns>Вычисляемое по формуле значение</returns>
        public static string Algorithm(int counter, double r, double[] x)
        {
            try
            {
                double summa = 0;
                for (int i = 0; i <= counter; i++) // сумма значений массива элементов (от нулевого до k-ого)
                {
                    summa += x[i];
                }
                double t = 1 / r * summa; // представленная в задании формула
                string result = string.Format("{0:f2}", t);
                return result;
            } catch (Exception)
            {
                Console.WriteLine("Данные неверны. Повторите попытку.");
                return "mistake";
            }
        }

        /// <summary>
        /// Данный метод получает значения и перерабатывает их для последующей записи в файл output.txt.
        /// </summary>
        /// <param name="extraString">Строка с корректными данными</param>
        /// <param name="r">Введенное пользователем значение r</param>
        /// <returns>Данные для записи в файл output.txt</returns>
        public static string Solution(double r, string extraString)
        {
            string[] values = extraString.Split(" ", StringSplitOptions.RemoveEmptyEntries); // массив с корректными данными
            double[] x = new double[values.Length];
            for (int i = 0; i < values.GetLength(0); i++) // содание массива с вещественными корректными данными
            {
                x[i] = double.Parse(values[i]);
            }
            int counter = 0;
            string[] answer = new string[x.GetLength(0)]; 
            foreach (double value in x) // вычисление каждого начения массива по формуле
            {
                answer[counter] = Algorithm(counter, r, x);
                counter++;
            }
            string writeText = "";
            bool flag = true; 
            foreach (string elem in answer)
            {
                if (elem != "mistake"){
                    writeText += elem;
                    writeText += " ";
                } else
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                return writeText;
            }
            else
            {
                return "mistake";
            }
        }

        /// <summary>
        /// Данный метод предназначен для работы с пользователем.
        /// </summary>
        public static void Main(string[] args)
        {
            ConsoleKeyInfo keyToExit; // Переменная для сохранения ключа выхода.
            double r; // Число r
            do // Цикл повторения решения.
            {
                do // Проверка на корректность вводимых пользователем данных.
                {
                    Console.Clear();
                    Console.WriteLine("Введите число 0 < r <= 10: ");
                } while ((!double.TryParse(Console.ReadLine(), out r)) || (r <= 0) || (r > 10));
                
                string s = ReadAll();
                
                if (s != "fake")
                {
                    WriteAll(r, s);
                }
                
                Console.WriteLine("Для выхода нажмите Escape");
                Console.WriteLine("Для повторной работы команды нажмите любую другую клавишу");
                keyToExit = Console.ReadKey();

             } while (keyToExit.Key != ConsoleKey.Escape); // Окончание цикла решения.
        }
    }
}
