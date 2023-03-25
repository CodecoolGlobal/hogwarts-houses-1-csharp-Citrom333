using HogwartsHouses.Models.Types;

namespace HogwartsHouses.Models
{
    [System.Serializable]
    public class Student
    {
        public int StudentId { get; }
        public string Name { get; }
        public Gender Gender { get; }
        public PetType Pet { get; set; }
        public HouseType House { get; }

        public Student(int studentId, string name, Gender gender, PetType pet, HouseType house)
        {
            StudentId = studentId;
            Name = name;
            Gender = gender;
            Pet = pet;
            House = house;
        }
    }
}
