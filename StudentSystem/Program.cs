using StudentSystem.Models.Entity;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace StudentSystem
{
    internal class Program
    {
        static readonly string fileName = "studentsystem.dat";

        static Genericstore<Group> groupStore = new Genericstore<Group>();
        static Genericstore<Student> StudentStore = new Genericstore<Student>();

        [Obsolete]
        static void Main(string[] args)

        {
            Console.Title = "Student System";
            int groupId;

            //groupStore.Add(new Group { Name="P321"});
            //groupStore.Add(new Group { Name = "P322" });
            //groupStore.Add(new Group { Name = "P323" });

            //StudentStore.Add(new Student { Name = "Rauf",Surname= "Aliyev", GroupId=1,BirthDate=DateTime.Today.AddYears(-18)  });

            try
            {
                using (var file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    var db = (StudentContext)bf.Deserialize(file);

                    groupStore = db.Groups;
                    StudentStore = db.Students;

                    Group.Setcounter(groupStore[groupStore.Count-1].Id);
                    Student.Setcounter(StudentStore[StudentStore.Count - 1].Id);

                }
            }
            catch (Exception)
            {
            }

        l1:
            PrintMenu();
        
            switch (Scaner.ReadInteger("Please write menu Id: "))
            {
                case 1:
                    Group g = new Group();
                    g.Name = Scaner.ReadString("Group name: ");
                    groupStore.Add(g);
                    Console.Clear();
                    goto case 4;

                case 2:
                    Console.Clear();    
                    GetAllGroups();

                    l3:
                    groupId = Scaner.ReadInteger("Grup Id: ");
                    //if(!groupStore.Exists(g=>g.Id == groupId))
                    //{
                    //    Console.WriteLine("Choose at list: ");
                    //    goto l3;
                    //}

                    var foundGroup = groupStore.Find(g=>g.Id == groupId);

                    if(foundGroup == null)
                    {
                        Console.WriteLine("Choose at list: ");
                        goto l3;
                    }
                    foundGroup.Name = Scaner.ReadString("Group name: ");

                    goto case 4;

                case 3: 
                    GetAllGroups();
                    l2:
                    groupId = Scaner.ReadInteger("Grup Id: ");

                    if (!groupStore.Exists(x => x.Id == groupId))
                    {
                        Console.WriteLine("Choose at list: ");
                        goto l2;
                    }

                    Group found = groupStore.Find(g=>g.Id == groupId);

                    groupStore.Remove(found);

                    goto case 4;

                case 4:
                    Console.Clear();
                    GetAllGroups();
                    Console.Write("Please press any key go to menu");
                    Console.ReadKey();
                    Console.Clear();
                    goto l1;

                    case 5:
                    Console.Clear();   
                    Console.WriteLine("Choose at the list: ");
                    GetAllGroups();
                    groupId = Scaner.ReadInteger("Group Id: ");

                    var chooseGroup = groupStore.Find(g => g.Id == groupId);

                    if (chooseGroup == null)
                    {
                        //Console.WriteLine("Choose at list: ");

                        Console.WriteLine("This group is not exist,please you create...");
                        goto case 1;
                    }

                    Student student = new Student();
                    student.GroupId = chooseGroup.Id;
                    student.Name = Scaner.ReadString("Student name: ");
                    student.Surname = Scaner.ReadString("Student surname: ");   
                    student.BirthDate = Scaner.ReadDateTime("Student birthdate: ");              

                    StudentStore.Add(student);
                    goto case 8;

                case 8:
                    Console.Clear();
                    GetAllStudents();
                    Console.Write("Please press any key go to menu");
                    Console.ReadKey();
                    Console.Clear();
                    goto l1;

                case 9:
                    Console.Clear();
                    Console.WriteLine("Saving...");
                    Task.Delay(1500).Wait();

                    Console.WriteLine("Saved!");

                    StudentContext db = new StudentContext();
                    db.Groups = groupStore;
                    db.Students = StudentStore;

                    using (var file = new FileStream(fileName,FileMode.OpenOrCreate,FileAccess.Write))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        bf.Serialize(file, db);
                    }

                    goto l1;
                default:
                    Console.WriteLine("absoletuly result give up");
                    goto l1;
            }
      
        }

        private static void GetAllStudents()
        {
            GetAllGroups();
            int groupId = Scaner.ReadInteger("Group Id: ");

            if(groupId > 0)
            {
                var chooseGroup = groupStore.Find(s=>s.Id == groupId);

                var chooseItems = StudentStore.FindAll(s => s.GroupId == groupId);

                Console.WriteLine($"## List of Students => {chooseGroup.Name}##");

                foreach (var item in chooseItems)
                {
                    var group = groupStore.Find(g => g.Id == item.GroupId);

                    Console.WriteLine($"{item}");
                }
            }
            else 
            {
                Console.WriteLine("## List of Students ##");
                foreach (var item in StudentStore)
                {
                    var group = groupStore.Find(g => g.Id == item.GroupId);

                    Console.WriteLine($"{group.Name}: {item}");
                }

            }

            //Console.WriteLine("## List of Students ##");
            //foreach (var item in StudentStore)
            //{
            //    var group = groupStore.Find(g=>g.Id == item.GroupId);

            //    Console.WriteLine($"{group.Name}: {item}");
            //}
        }

        private static void PrintMenu()
        {
            Console.WriteLine("1.Add group");
            Console.WriteLine("2.Edit group");
            Console.WriteLine("3.Remove group");
            Console.WriteLine("4.List of groups");

            Console.WriteLine("5.Add student");
            Console.WriteLine("6.Edit student");
            Console.WriteLine("7.Remove student");
            Console.WriteLine("8.List of students");

            Console.WriteLine("9.Save");
            Console.WriteLine("10.Exit");
        }
        static void GetAllGroups()
        {
            Console.WriteLine("## List of Groups ##");
            foreach (var item in groupStore)
            {
                Console.WriteLine(item);
            }
        }
    }   
}
