﻿namespace VetClinic.Models
{
    public static class PetsRepository
    {
        private static List<Pet> _pets = new List<Pet>()
        {
            new Pet { PetId = 1, Name = "Cats", Description = "Cat" },
            new Pet { PetId = 2, Name = "Dogs", Description = "Dog" },
            new Pet { PetId = 3, Name = "Hamsters", Description = "Hamster" },
            new Pet { PetId = 4, Name = "Birds", Description = "Bird" },
            new Pet { PetId = 5, Name = "Horses", Description = "Horse" }
        };

        public static void AddPet(Pet pet)
        {
            if (_pets != null && _pets.Count > 0)
            {
                var maxId = _pets.Max(x => x.PetId);
                pet.PetId = maxId + 1;
            }
            else
            {
                pet.PetId = 1;
            }
            if (_pets == null) _pets = new List<Pet>();
            _pets.Add(pet);
        }

        public static List<Pet> GetPets() => _pets;

        public static Pet? GetPetById(int petId)
        {
            var pet = _pets.FirstOrDefault(x => x.PetId == petId);
            if (pet != null)
            {
                return new Pet
                {
                    PetId = pet.PetId,
                    Name = pet.Name,
                    Description = pet.Description,
                };
            }

            return null;
        }

        public static void UpdatePet(int petId, Pet pet)
        {
            if (petId != pet.PetId) return;

            var petToUpdate = _pets.FirstOrDefault(x => x.PetId == petId);
            if (petToUpdate != null)
            {
                petToUpdate.Name = pet.Name;
                petToUpdate.Description = pet.Description;
            }
        }

        public static void DeletePet(int petId)
        {
            var pet = _pets.FirstOrDefault(x => x.PetId == petId);
            if (pet != null)
            {
                _pets.Remove(pet);
            }
        }

    }
}
