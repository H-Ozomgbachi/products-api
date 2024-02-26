namespace Product.API.Helpers
{
    public class UtilityHelper
    {
        public static string Serializer(object obj)
        {
            JsonSerializerSettings options = new()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            return JsonConvert.SerializeObject(obj, options);
        }
        public static T DeSerializer<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public static string GenerateUniqueID()
        {
            string timeStamp = DateTime.UtcNow.Ticks.ToString()[^6..];
            string randomDigit = GetRandomDigits(6);

            return string.Concat(timeStamp, randomDigit);
        }

        public static string GetRandomDigits(int count)
        {
            string charSet = "0123456789";
            Random rnd = new();

            StringBuilder res = new(count);
            for (int i = 0; i < count; i++)
            {
                res.Append(charSet[rnd.Next(charSet.Length)]);
            }

            return res.ToString();
        }
    }
}

