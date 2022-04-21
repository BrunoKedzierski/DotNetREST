using Animals.DTOs;

namespace Animals.Services
{
    public interface IDatabaseService
    {
        public IEnumerable<Animal> ShowAllAnimals(string orderParam);
        public void AddNewAnimal(Animal newAnimal);
        public void DeleteAnimal(int idAnimal);

        public void UpdateAnimal(Animal newAnimal, int idToChange);
    }
}
