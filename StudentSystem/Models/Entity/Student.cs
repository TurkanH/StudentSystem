using System;

namespace StudentSystem.Models.Entity
{
    [Serializable]
    internal class Student
    {
        /*private*/
        static int counter=0;

        public Student()
        {
            this.Id = ++counter;
        }
        static public void Setcounter(int counter)
        {
            Student.counter = counter;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int GroupId { get; set; }
        public DateTime BirthDate { get; set; }

        public int Age 
        {
            get
            {
                //TimeSpan ts = DateTime.Today- this.BirthDate;
                int age = DateTime.Today.Year - this.BirthDate.Year;

                return age;
            }

            }

        public override string ToString()
        {
            return $"{Id}. {Name} {Surname} | {BirthDate:yyyy.MM.dd} (Age:{Age})";
        }
    }
}
