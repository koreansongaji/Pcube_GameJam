using System.Collections.Generic;

namespace Helpers
{
    public abstract class RandomNumberGenerator
    {
        public static List<int> GetRandomNumbers(int count, int min, int max, bool availableDuplicate = false)
        {
            if(count > max - min && !availableDuplicate)
                throw new System.Exception("Count is bigger than max - min");
            
            UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
            List<int> randomNumbers = new List<int>();
            for (int i = 0; i < count; i++)
            {
                int randomNumber = UnityEngine.Random.Range(min, max);
                if (!availableDuplicate)
                {
                    while (randomNumbers.Contains(randomNumber))
                    {
                        randomNumber = UnityEngine.Random.Range(min, max);
                    }
                }

                randomNumbers.Add(randomNumber);
            }

            return randomNumbers;
        }
    }
}