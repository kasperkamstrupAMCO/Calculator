using MathLib;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

string[] menuOptions = {$"\tAddition",
						$"\tSubtraction",
						$"\tMultiplication",
						$"\tDivision",
						$"\tExponentiation",
						$"\tTrig Function of Angle",
						$"\tSquareroot of a Number",
						$"\tGreatest Common Denominator",
						$"\tLeast Common Multiple",
						$"\tQuadratic Equation Solver"};

int				selection		= 1;
ConsoleKeyInfo	usrKeyInput;
bool			quit			= false;


while (!quit)
{

	menuPrinter(ref menuOptions, selection);

	usrKeyInput = ReadKey(true);

	switch (usrKeyInput.Key)
	{
		case ConsoleKey.DownArrow:
			if (selection < menuOptions.Length) selection++;
			break;
		case ConsoleKey.UpArrow:
			if (selection > 1) selection--;
			break;
		case ConsoleKey.Enter:
			selector(selection);
			break;
		case ConsoleKey.Q:
		case ConsoleKey.Escape:
			quit = true;
			break;

	}
}

Clear();

static void selector(int selection)
{
	switch (selection)
	{
		case 1:
			Addition();
			break;
		case 2:
			Subtraction();
			break;
		case 3:
			Multiplication();
			break;
		case 4:
			Division();
			break;
		case 5:
			Exponentiation();
			break;
		case 6:
			TrigFunctionOfAngle();
			break;
		case 7:
			SquarerootOfNumber();
			break;
		case 8:
			GreatestCommonDenominator();
			break;
		case 9:
			LeastCommonMultiple();
			break;
		case 10:
			QuadraticEquationSolver();
			break;
	}
}
static void menuPrinter(ref string[] optionStrings, int selection)
{
	Clear();
	WriteLine($@"Welcome to CALCULATOR!!");
	WriteLine($@"What would you like to do? Press q or escape to quit.");
	WriteLine($@"");

	for (int i = 0; i < optionStrings.Length; i++)
	{
		if (i == selection - 1)
		{
			Console.ForegroundColor = ConsoleColor.Blue;
			WriteLine(optionStrings[i]);
			Console.ForegroundColor = ConsoleColor.White;
			continue;
		}
		WriteLine(optionStrings[i]);
	}
}
static void Addition()
{
	/************ Variable Declarations ************/
	double	 num1;
	double	 num2;
	int		 temp;
	int		 sigfigs;
	string[] rawInput;
	var		 output	= new System.Text.StringBuilder();

Top:
	Clear();


	rawInput = basicOpInterface("Addition");

	/************ Counts Significant Figures ************/

	sigfigs = getLeastSignificantPlace(rawInput[0]);

	temp = getLeastSignificantPlace(rawInput[1]);

	if (temp > sigfigs) sigfigs = temp;


	/************ Tries to cast usr nums to double, then adds ************/

	try
	{
		num1	= Convert.ToDouble(rawInput[0]);
		num2	= Convert.ToDouble(rawInput[1]);

		if (sigfigs < 0)
		{
			output.Append(Basic.add(num1, num2).ToString($"N{Math.Abs(sigfigs)}"));
			goto FinalResult;
		}
		else
		{
			output.Append(Basic.add(num1, num2).ToString($"N0"));
		}
	}
	catch (FormatException e)
	{
		WriteLine($"Argument could not cast to a double, try a different format.\n\nError message: {e.Message}");
		ReadLine();
		goto Top;
	}


	if (sigfigs > 0)
	{
		for (int i = output.Length - 1; i >= 0; i--)
		{
			if (sigfigs <= 0)	  break;
			if (output[i] == ',') continue;

			sigfigs--;
			output.Remove(i, 1).Insert(i, '0');
		}
	}

FinalResult:
	displayResult(ref output);
}
static void Subtraction()
{
	/************ Variable Declarations ************/
	double num1;
	double num2;
	int temp;
	int sigfigs;
	string[] rawInput;
	var output = new System.Text.StringBuilder();

Top:
	Clear();

	rawInput = basicOpInterface("Subtraction");

	/************ Counts Significant Figures ************/

	sigfigs = getLeastSignificantPlace(rawInput[0]);

	temp = getLeastSignificantPlace(rawInput[1]);

	if (temp > sigfigs) sigfigs = temp;

	/************ Tries to cast usr nums to double, then adds ************/

	try
	{
		num1 = Convert.ToDouble(rawInput[0]);
		num2 = Convert.ToDouble(rawInput[1]);

		if (sigfigs < 0)
		{
			output.Append(Basic.subtract(num1, num2).ToString($"N{Math.Abs(sigfigs)}"));
			goto FinalResult;
		}
		else
		{
			output.Append(Basic.subtract(num1, num2).ToString($"N0"));
		}


	}
	catch (FormatException e)
	{
		WriteLine($"Argument could not cast to a double, try a different format.\n\nError message: {e.Message}");
		ReadLine();
		goto Top;
	}


	if (sigfigs > 0)
	{
		for (int i = output.Length - 1; i >= 0; i--)
		{
			if (sigfigs <= 0) break;
			if (output[i] == ',') continue;

			sigfigs--;
			output.Remove(i, 1).Insert(i, '0');
		}
	}

FinalResult:
	displayResult(ref output);
}
static void Multiplication()
{
	/************ Variable Declarations ************/
	double num1;
	double num2;
	int temp;
	int sigfigs;
	string[] rawInput;
	var output = new System.Text.StringBuilder();

Top:
	Clear();

	WriteLine("!!!!!!BUG: 1 * -1 = 0");

	rawInput = basicOpInterface("Multiplication");


	sigfigs = getNumberOfSigFigs(rawInput[0]);
	temp	= getNumberOfSigFigs(rawInput[1]);

	if (temp < sigfigs) sigfigs = temp;


	try
	{
		num1 = Convert.ToDouble(rawInput[0]);
		num2 = Convert.ToDouble(rawInput[1]);

		output.Append(Basic.multiply(num1, num2).ToString($"N"));
	}
	catch (FormatException e)
	{
		WriteLine($"Argument could not cast to a double, try a different format.\n\nError message: {e.Message}");
		ReadLine();
		goto Top;
	}

	bool decimalReached = false;

	for (int i = 0; i < output.Length; i++)
	{
		if (sigfigs > 0)
		{
			if (output[i] == ',' || output[i] == '.') continue;
			sigfigs--;
			continue;
		}
		if (decimalReached)
		{
			output[i] = '\0';
			continue;
		}
		if (output[i] == '.')
		{
			decimalReached = true;
			output[i] = '\0';
			continue;
		}
		if (output[i] == ',') continue;
		output[i] = '0';
	}

//FinalResult:
	displayResult(ref output);
}
static void Division()
{
	WriteLine($@"		!!!!! NOT YET IMPLEMENTED !!!!!");
	ReadLine();
}
static string[] basicOpInterface(string opName)
{
	/************ Variable Declarations ************/
	var		  rawInput = new string[2];
	ErrorCode error;


	WriteLine($"You've chosen {opName}! Please type the input numbers.\n\n");

	/************ Requests User Input ************/

	
	string getInput(string prompt)
	{
		string? input;
		do
		{
			Write(prompt);
			input = ReadLine();

			error = validateNumberInputEnum(input!);

			if (error == ErrorCode.EmptyInput) WriteLine("Please type something\n");
			if (error == ErrorCode.IllegalCharacter) WriteLine("Illegal character\n");

		} while (error != ErrorCode.NoError);

		return input!;
	}

	rawInput[0] = getInput("First number  >>: ");
	rawInput[1] = getInput("Second number >>: ");

	return rawInput;
}
static void displayResult(ref StringBuilder result)
{
	// This guard clause removes the minus sign from 0 when the result is -0.
	if (!Regex.IsMatch(result.ToString(), @"[\d-[0]]") & result[0] == '-')
	{
		result.Remove(0, 1);
	}

	Write("Result        >>: ");
	WriteLine(result);

	ReadLine();
}
static void Exponentiation()
{
	WriteLine($@"		!!!!! NOT YET IMPLEMENTED !!!!!");
	ReadLine();
}
static void TrigFunctionOfAngle()
{
	WriteLine($@"		!!!!! NOT YET IMPLEMENTED !!!!!");
	ReadLine();
}
static void SquarerootOfNumber()
{
	WriteLine($@"		!!!!! NOT YET IMPLEMENTED !!!!!");
	ReadLine();
}
static void GreatestCommonDenominator()
{
	WriteLine($@"		!!!!! NOT YET IMPLEMENTED !!!!!");
	ReadLine();
}
static void LeastCommonMultiple()
{
	WriteLine($@"		!!!!! NOT YET IMPLEMENTED !!!!!");
	ReadLine();
}
static void QuadraticEquationSolver()
{
	WriteLine($@"		!!!!! NOT YET IMPLEMENTED !!!!!");
	ReadLine();
}