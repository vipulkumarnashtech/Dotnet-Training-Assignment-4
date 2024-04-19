using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rpgAPI.Service
{
    public class CharacterService : ICharacterService
    {


        private static List<Character> _characterList = new List<Character>()
        {
            new Character(),
            new Character(){Name = "Gollum", Id = 1},
        };


        public ServiceResponse<List<Character>> GetAllCharacter()
        {
            var serviceResponse = new ServiceResponse<List<Character>>()
            {
                Data = _characterList
            };
            return serviceResponse;
        }

        public ServiceResponse<List<Character>> AddCharacter(Character newCharacter)
        {
            _characterList.Add(newCharacter);

            var serviceResponse = new ServiceResponse<List<Character>>()
            {
                Data = _characterList
            };
            return serviceResponse;

        }

        public ServiceResponse<Character> GetCharacterById(int id)
        {
            var character = _characterList.FirstOrDefault(c => c.Id == id);

            var serviceResponse = new ServiceResponse<Character>();

            if (character == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Id Doesn't Exist";

                return serviceResponse;
            }

            serviceResponse.Data = character;

            return serviceResponse;
        }

        public ServiceResponse<List<Character>> UpdateCharacter(Character updatedCharacter)
        {
            Character existingCharacter = _characterList.FirstOrDefault(c => c.Id == updatedCharacter.Id);
            var serviceResponse = new ServiceResponse<List<Character>>();
            if (existingCharacter != null)
            {
                existingCharacter.Name = updatedCharacter.Name;
                existingCharacter.HitPoint = updatedCharacter.HitPoint;
                existingCharacter.Strength = updatedCharacter.Strength;
                existingCharacter.Defense = updatedCharacter.Defense;
                existingCharacter.Intelligence = updatedCharacter.Intelligence;
                existingCharacter.CharacterClass = updatedCharacter.CharacterClass;
                serviceResponse.Data = _characterList;
                serviceResponse.Success = true;
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Id Doesn't Exist";
            }
            return serviceResponse;
        }

        public ServiceResponse<List<Character>> DeleteCharacter(int id)
        {
            Character characterToRemove = _characterList.FirstOrDefault(c => c.Id == id);
            var serviceResponse = new ServiceResponse<List<Character>>();
            if (characterToRemove != null)
            {
                _characterList.Remove(characterToRemove);
                serviceResponse.Data = _characterList;
                serviceResponse.Success = true;
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Id Doesn't Exist";
            }
            return serviceResponse;
        }
    }
}