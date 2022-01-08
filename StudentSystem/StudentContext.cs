using StudentSystem.Models.Entity;
using System;

namespace StudentSystem
{
    [Serializable]
    internal class StudentContext
    {
        public Genericstore<Group> Groups { get; set; }
        public Genericstore<Student> Students { get; set; }

    }
}
