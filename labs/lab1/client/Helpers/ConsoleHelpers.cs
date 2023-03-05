namespace Lab1Client.Helpers
{
    public static class ConsoleHelpers
    {
        public static int GetIntInput(string message, Func<int, bool>? validationFunc = null)
        {
            int result;
            var input = AskForInput(message);
            while(!int.TryParse(input, out result) || !CallValidationFunc(validationFunc, result)) 
            {
                Console.WriteLine("Invalid input");
                input = AskForInput(message);
            }

            return result;
        }

        private static string? AskForInput(string message)
        {
            Console.WriteLine(message);
            var input = Console.ReadLine();
            return input;
        }

        private static bool CallValidationFunc(Func<int, bool>? validationFunc, int number)
        {
            return validationFunc is not null
                ? validationFunc(number)
                : true;
        }
    }
}