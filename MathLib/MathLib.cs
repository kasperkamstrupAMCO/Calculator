using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;

namespace MathLib;

public static class Basic
{
	public static double add(double num1, double num2)
	{
		return num1 + num2;
	}
	public static double subtract(double minuend, double subtrahend)
	{
		return minuend - subtrahend;
	}
	public static double multiply(double num1, double num2)
	{
		return num1 * num2;
	}
	public static double divide(double dividend, double divisor)
	{
		return dividend / divisor;
	}
}
public static class Advanced
{
	
}
public static class Helpers
{
	public enum ErrorCode : byte
	{
		NoError,
		EmptyInput,
		IllegalCharacter
	}
	public static ErrorCode validateNumberInputEnum(string input)
	{
		if (input == "")						  return ErrorCode.EmptyInput;
		if (Regex.IsMatch(input, @"[\D-[,\.-]]")) return ErrorCode.IllegalCharacter;

												  return ErrorCode.NoError;
	}
	public static void validateNumberInputException(string input)
	{
		if (input == "")						  throw new ArgumentException("Argument was empty", input);
		if (Regex.IsMatch(input, @"[\D-[,\.-]]")) throw new ArgumentException("Arugment contained illegal characters", input);
	}
	public static int getLeastSignificantPlace(string input)
	{
		int output = 0;
	
		if (input.Contains('.'))
		{
			foreach (char character in input.Reverse())
			{
				if (character == '.') break;
				output--;
			}
		}
		else
		{
			foreach (char character in input.Reverse())
			{
				if (character == ',') continue;
				if (character != '0') break;
				output++;
			}
		}
		return output;
	}
	public static int getNumberOfSigFigs(string input)
	{
		int output = 0;
		bool startCount = false;

		if (input.Contains('.'))
		{
			foreach (char character in input)
			{
				if (character != '.' || character != ',') continue;
				output++;
			}
		}
		else
		{
			foreach (char character in input.Reverse())
			{
				if (startCount)
				{
					if (character != ',') output++;
					continue;
				}
				if (character != '0' && character != ',')
				{
					startCount = true;
					output++;
				}
			}
		}
		return output;
	}
}