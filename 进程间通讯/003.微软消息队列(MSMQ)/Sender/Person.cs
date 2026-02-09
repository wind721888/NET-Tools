// Person.cs
using System;

namespace MSMQSender
{
    // [Serializable] 特性对于 BinaryMessageFormatter 和 XmlMessageFormatter (发送对象本身时) 都是必需的
    [Serializable]
    public class Person
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; } = 0;
        public DateTime BirthDate { get; set; } = DateTime.MinValue;

        // 无参构造函数是反序列化的要求
        public Person() { }

        public Person(string name, int age, DateTime birthDate)
        {
            Name = name;
            Age = age;
            BirthDate = birthDate;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Age: {Age}, BirthDate: {BirthDate}";
        }
    }
}