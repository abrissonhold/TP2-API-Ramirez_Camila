using Application.Interfaces;
using Application.UserCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public static class ConsoleInputHelper
    {
        public static string ReadString(string inputMessage)
        {
            string input;
            do
            {
                Console.Write(inputMessage);
                Console.ForegroundColor = ConsoleColor.Green;
                input = Console.ReadLine()!;
                Console.ResetColor();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("El campo no puede estar vacío.");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                }

            } while (string.IsNullOrWhiteSpace(input));

            return input;
        }

        public static int ReadInt(string inputMessage, int? min = null, int? max = null)
        {
            int inputNumber;
            bool isValid;
            do
            {
                Console.Write(inputMessage);
                Console.ForegroundColor = ConsoleColor.Green;
                isValid = int.TryParse(Console.ReadLine(), out inputNumber);
                Console.ResetColor();

                if (!isValid || min != null && inputNumber < min || max != null && inputNumber > max)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Valor inválido. Intente nuevamente.");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    isValid = false;
                }

            } while (!isValid);

            return inputNumber;
        }

        public static decimal ReadDecimal(string inputMessage, decimal? min = null, decimal? max = null)
        {
            decimal inputValue;
            bool isValid;
            do
            {
                Console.Write(inputMessage);
                Console.ForegroundColor = ConsoleColor.Green;
                isValid = decimal.TryParse(Console.ReadLine(), out inputValue);
                Console.ForegroundColor = ConsoleColor.DarkGreen;

                if (!isValid || min != null && inputValue < min || max != null && inputValue > max)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ingrese un número decimal válido.");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    isValid = false;
                }

            } while (!isValid);

            return inputValue;
        }

        public static string LeerEmail(string inputMessage)
        {
            string inputEmail;
            var regex = new System.Text.RegularExpressions.Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            do
            {
                Console.Write(inputMessage);
                Console.ForegroundColor = ConsoleColor.Green;
                inputEmail = Console.ReadLine()!;
                Console.ForegroundColor = ConsoleColor.DarkGreen;

                if (!regex.IsMatch(inputEmail))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ingrese un correo electrónico válido.");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    inputEmail = "";
                }

            } while (string.IsNullOrWhiteSpace(inputEmail));

            return inputEmail;
        }
    }
}