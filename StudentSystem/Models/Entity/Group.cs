using System;

namespace StudentSystem.Models.Entity
{
    [Serializable]
    internal class Group
    {
        private static int counter = 0;

        public Group()
        {
            //counter++;
            this.Id = ++counter;
        }

        static public void Setcounter(int counter)
        {
            Group.counter = counter;
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Id}.{Name}";
        }


    }
}
