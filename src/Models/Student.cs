using HogwartsHouses.Models.Types;

namespace HogwartsHouses.Models
{
    [System.Serializable]
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public PetType Pet { get; set; }
        public HouseType House { get; set; }

        public Student(int studentId, string name, Gender gender, PetType pet, HouseType house)
        {
            StudentId = studentId;
            Name = name;
            Gender = gender;
            Pet = pet;
            House = house;
        }

        public Student()
        {
            
        }
        public override string ToString()
        {
            return
                $"Student ID: {StudentId}, name: {Name}. Gender: {Gender}. Type of pet: {Pet}. " +
                $"The student is in {House} ";
        }
    }
}
