using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Korgopolov6
{
    public class FileManager
    {


        public List<Human> Humans { get; set; }

        public static string GetFilePathFromUser(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }

        public void LoadFile(string path)
        {
            Humans = new List<Human>();
            if (path.EndsWith(".txt"))
            {
                ReadTextFile(path);
            }
            else if (path.EndsWith(".json"))
            {
                ReadJsonFile(path);
            }
            else if (path.EndsWith(".xml"))
            {
                ReadXmlFile(path);
            }
        }

        public void SaveFile(string path)
        {
            if (path.EndsWith(".txt"))
            {
                var text = GetHumanText();
                File.WriteAllText(path, text);
                Console.WriteLine("Файл успешно сохранен в формате .txt");
            }
            else if (path.EndsWith(".xml"))
            {
                XmlSerializer xml = new(typeof(List<Human>));
                using (FileStream fs = new(path, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    xml.Serialize(fs, Humans);
                }

                Console.WriteLine("Файл успешно сохранен в формате .xml");
            }
            else if (path.EndsWith(".json"))
            {
                File.WriteAllText(path, JsonConvert.SerializeObject(Humans));
                Console.WriteLine("Файл успешно сохранен в формате .json");
            }
        }
        private void ReadTextFile(string path)
        {
            string[] lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                var parts = line.Split(":");
                var human = new Human()
                {
                    Name = parts[0],
                    Surname = parts[1],
                    Age = int.Parse(parts[2]),
                };
                Humans.Add(human);
            }
        }

        private void ReadJsonFile(string path)
        {
            var text = File.ReadAllText(path);
            Humans = JsonConvert.DeserializeObject<List<Human>>(text);
        }

        private void ReadXmlFile(string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Human>));
            XmlSerializer xml = xmlSerializer;
            using FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            Humans = (List<Human>)xml.Deserialize(fs);
        }

        private string GetHumanText()
        {
            string result = "";
            foreach (var human in Humans)
            {
                result = $"Name: {human.Name}, Surname: {human.Surname}, Age: {human.Age}\n";
            }
            return result;
        }

        public void DisplayHumans()
        {
            Console.WriteLine("\nСодержимое файла:");
            foreach (var human in Humans)
            {
                Console.WriteLine($"Имя: {human.Name}, Фамилия: {human.Surname}, Возраст: {human.Age}");
            }
            Console.WriteLine();
        }
    }
}
