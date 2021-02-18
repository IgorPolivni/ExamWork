using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ExamWork
{
    class Program
    {
        static void Main(string[] args)
        {
            Student student = new Student();

            LogClass log = new LogClass()
            {
                Time = DateTime.Now.ToString(),
                TypeClass = "Student",
                TypeError = "Error",
                MessageError = "Класс является пустым!"
            };
            List<LogClass> logList = new List<LogClass>()
            {
                log,
                log
            };



            var serializer = new XmlSerializer(logList.GetType());


            XmlDocument xmlDocument = new XmlDocument();
            DateTime date = DateTime.Now;
            string currentDate = date.ToShortDateString();
            
            string FileName = $"{currentDate}.log";



            if (!File.Exists(FileName))
            {
                using (var stream = File.OpenWrite(FileName))
                {
                    serializer.Serialize(stream, logList);
                }

            }
            else
            {
                List<LogClass> result = new List<LogClass>();
                using (var stream = File.OpenRead(FileName))
                {
                    result = serializer.Deserialize(stream) as List<LogClass>;
                    result.Add(log);
                
                }
                using (var stream = File.OpenWrite(FileName))
                {
                    serializer.Serialize(stream, result);
                }

            }


        }
    }
}
