﻿using AirForceLibrary.Utilis;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AirForceConsole.UI
{
    internal class ConsoleUtility
    {
        public static string name;
        public static int PakNo;
        public static string Password;
        public static void FirstPage()
        {
           // Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("00000000001001100010110001101011101000111101001000100011100000111010111011000001010000001011110100000001011000101010100100011\r\n11010110000101000011000111110000110001101001011001010010100100110111001111101110001000001110110011000111110010110001111110010\r\n01111010011111011111110011100111011001101110110101101111100011011010110010100111111101001111110011010000100100010101101011010\r\n10111011101101010111101000001001000100100101011111111001011000111111010110110010001001101011000000000111000110101001011100111\r\n11101110110010101011100011011110110001100011101111100110011111010011101011000101001100100111111101011110010010011111110101100\r\n10010101111110000011101000111111111001001111101010101001001000011010110100000110110000100110000111000000001101111010001010001\r\n00000001011001101101010110000100110111110011110001110010001101001111010001111111111001010101100111110100001010111011000100000\r\n01100001111000101100001010011110010011100100001011001101010101100010011010100001000000010001111101100110110100111011010100101\r\n11100101110011001110101101011101001010000010010011011111011101101110001000100100011101011000001010100011001101110110101010111\r\n11101001100101110101100000101100010111000111000111010111100010000100110001100111110101010110101010110111100100001000000111001\r\n01011100000100101101100000100001001111010101000000001110010010110100000000111001111011010001101011000010110010110010100110011\r\n10011011110010011101001100100010111001100110001110001001010001110111011011001000110000000011011000001011101001101110100100110\r\n11101110010110010100101110110011000100001110100011011000111111101010010001100001000011011100111010100111011001101000110000011\r\n00011111101111101100011110100100111011100111110111001101010111010001000100011000000000100011000000111011011111010101111011111\r\n10001001000010110011110000011011111011001011011001010111101111101000010000011011001000110000101011000101001100000001111100010\r\n10111001001001111111001101111111010001001010001001001001001110110100111111110001101100010000111010010110011101110001100101001\r\n10010011110111000010011010011100001100110110010111101100111110100110110001110111010100111001001110010010100110111101111010100\r\n01100010110010111000011011010001001011000001110001101111000011010111001110101000111010110000010111010011000000011110101011110\r\n10000011010101011110101011010000100011010011100101111100000001101100001100011001000110010010110110010101010101100000011001000\r\n01010110100110000001101000011100110010011111011110110010001101001011010011110101000011100010011100101101100000010010111101011\r\n00001101100001111010111110001111100011110011100010011110110111001011011011000010010101000101101001010111000000000010000010101\r\n11001111111100010101110100111010100000111101110110001000010011101010000100101010001000110000011010010000100100000001011100101\r\n01111011000011110110011001101010101100110110101011010001101111011101110100101010111101010110101000101111011110001101111000010\r\n01101011010010100001111101111010000100101010000101111001011000111101111110100110110010000001101001001101010011001101111000100\r\n01001100011101101110110100101000110101100001111000000110101100101110110100111111110011011011111011101000100010110110010010101\r\n01100000000010011011100000110100111011110100110000101111001011000001001110011011100110111011111111001111001010011100101010110\r\n00100111110111011000011111101110110011001110000011010101000001001011000000010111001001111001101000100011001101000001110000111\r\n10111011011100111110011101101011010101011000011100011101011101010100110111110110111001011100010001001111011110100001011011011\r\n10110111100110101110011011001010010111101000000010100011000000101110000111000011001111000101001101010110110001101011101010011\r\n01111001101001000101011100001110111111010000010101100010010101000011100011000001010010001011011001111111011001111000100110101\r\n00111110010101100000101001011111100010010111110111011010110100110110111110111001001110010110010010101100101111000101110100011\r\n10101110000101011001010101110000000111001011110011011101101001101001010010101100001001001010000111110010011001100000001010000\r\n01000100010011110011110001100000110110011100000110001000110010010110110010100100100110110011011100101001000001000011101000000\r\n10110100001111100011110101011110101001110010000101100111001101011000100011110110000101100011001110110010001001100111011101101\r\n11000101101110110101010010011101000101110010100000100010101101101010000111001000011011110100011101101111100100011010010010010\r\n01101000011100001010010010011111000101110011011100000011101010101010011101011100110110011110000010011000010110011010010111010\r\n11011100010111011010100101111000000000000011001101101000100111101100001011000010101001001101101110011010110101100000011100111\r\n00010101111010100100001111010110110000100110110010001100101111000000001011011011111100010101111001010001101001010100011011110\r\n11100011000100111110100100111101001001111000010011010011110110000011111000011111111101010001110001011000111100010000000001011\r\n11100111111101011001001010001011010111100101110100101010101001010110010000000100110001100100001100100101101011111011001010110\r\n11111011001011011110000100011010001010010111100011100111001111000110111000100101110100010001101010101000101100011011001100000\r\n10011001101000000001011100000010010010010101111000011101101101100010100100111011001000011001000000010101011011100011101100000\r\n01100100101101111010010001000100011010010011001000111111001111010011010010110111001111100100100101010000011111100101011100101\r\n10001000100110000101111100100000000011110100111011111100111011010110100001001010000011111001011001110111001111111101111010011\r\n00010001101000111011100110111101110001011101110100011000110000010111110101110010111011001101011110100100101000111111011100011\r\n10011011111111010110111011110010101011000100111111111000100010011101100110010011011110000001011001011000000011000001100011101\r\n10010011111111101000001101011011001100011010101111011111011001110011110001110001110101110100001000001001110101010011100110111\r\n10111001111011000110001111111111000001100010100100110111100001000110011011010100010100100100101001111010010000010011111110111\r\n01111111100001100000011000011100000011101011001100010111010010110110100111100111000100101000011110011001110011001011000001100\r\n10010011101110010111100000000111101110111100001000001011110001111110000100110001100110100100001001110100110101111110101101011\r\n00101011100001010101111111000000111011100010101101011110010100001000011001110111111101101110001110101101100101110111000011100\r\n01111110010000001101111110001000000011110110110011111110101100001101100100011111101010001001011010000101110000111011100101100\r\n10111010000001101100111011110011110101001010111000011010100011010001111110010101010010000000100000010001000000100011011110011\r\n01111101101011000000001000110000110111001001001010110111100000101001001111100011000001101000000011111110110000110101101000100\r\n10010000111100001011011001111100001000101000001001100011011011110011010100111100110010010110000010000111111101010110000010001\r\n00111111100011100101111001010110001000010100011001111000011001101111111011100001101100110111101000000101100000101100100111100\r\n11001110111101101011010010010010000110111010111110011100001110111010011010011100110010010110010010011100000001011000000111010\r\n11000110100101110111000011000111011101101000100011010010000101111010110011010000100010011111010100011101000010010100000110001");
            Console.ReadKey();        
        }
        public static void Header()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\r\n░▒▓███████▓▒░ ░▒▓██████▓▒░░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓███████▓▒░▒▓████████▓▒░▒▓██████▓▒░░▒▓███████▓▒░        ░▒▓██████▓▒░░▒▓█▓▒░▒▓███████▓▒░       ░▒▓████████▓▒░▒▓██████▓▒░░▒▓███████▓▒░ ░▒▓██████▓▒░░▒▓████████▓▒░ \r\n░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░         ░▒▓█▓▒░  ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░     ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░        \r\n░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░         ░▒▓█▓▒░  ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░     ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░      ░▒▓█▓▒░        \r\n░▒▓███████▓▒░░▒▓████████▓▒░▒▓███████▓▒░░▒▓█▓▒░░▒▓██████▓▒░   ░▒▓█▓▒░  ░▒▓████████▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓████████▓▒░▒▓█▓▒░▒▓███████▓▒░       ░▒▓██████▓▒░░▒▓█▓▒░░▒▓█▓▒░▒▓███████▓▒░░▒▓█▓▒░      ░▒▓██████▓▒░   \r\n░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░      ░▒▓█▓▒░  ░▒▓█▓▒░  ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░     ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░      ░▒▓█▓▒░        \r\n░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░      ░▒▓█▓▒░  ░▒▓█▓▒░  ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░     ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░        \r\n░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░▒▓███████▓▒░   ░▒▓█▓▒░  ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░      ░▒▓██████▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓██████▓▒░░▒▓████████▓▒░ \r\n                                                                                                                                                                                                                \r\n                                                                                                                                                                                                                \r\n");
        }
        public static void TakeSignIn()
        {
            try
            {
                Console.Clear();
                ConsoleUtility.Header();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Enter Your Name: ");
                name = Console.ReadLine();
                Console.WriteLine("Enter Your PakNo");
                PakNo = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Your Password: ");
                Password = Console.ReadLine();
               
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
           
        }
        public static int DataBaseChoice()
        {
            Console.Clear();
            Header();
            Console.WriteLine("WHICH DATA SERVER DO YOU WANT TO USE?");
            Console.WriteLine("1. DataBase");
            Console.WriteLine("2.File Handling");
            Console.WriteLine("Enter your choice: ");
            int op = int.Parse(Console.ReadLine());
            return op;
        }
    }
}
