using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Runtime.ConstrainedExecution;
using Korgopolov6;

class Program
{

    static void Main(string[] args)
    {
        FileManager manager = new FileManager();
        Console.WriteLine("F1 - Сохранить файл");
        Console.WriteLine("Escape - Выйти из программы ");
        Console.WriteLine("".PadRight(120, '-'));
        Console.SetCursorPosition(0, 3);
        string path = FileManager.GetFilePathFromUser("Введите путь файла, который хотите прочитать:");
        manager.LoadFile(path);
        manager.DisplayHumans();

        while (true)
        {

            ConsoleKey key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.F1:
                    var savePath = FileManager.GetFilePathFromUser("Где и в каком формате вы хотите сохранить файл?");
                    manager.SaveFile(savePath);
                    break;

                case ConsoleKey.Escape:
                    return;

                default:
                    Console.WriteLine("Ошибка ! попробуйте использовать предложенные кнопки");
                    break;
            }
        }
    }


}
