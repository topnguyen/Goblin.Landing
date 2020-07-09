using System;

namespace Goblin.Landing.Models
{
    public static class NoImageHelper
    {
        public static string GetRandomNoImageUrl()
        {
            var random = new Random();

            var noImageName = random.Next(1, 10);

            var noImageUrl = $"/img/no-image/no-image-{noImageName}.png";

            return noImageUrl;
        }
    }
}