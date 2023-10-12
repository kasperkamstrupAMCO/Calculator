using MathLib;
using System.Reflection.PortableExecutable;
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
	Clear();


	/************ Variable Declarations ************/

	double	num1;
	double	num2;
	int		temp		= 0;
	int		sigfigs;
	var		rawInput	= new string[2];
	var		output		= new System.Text.StringBuilder();
	



	/************ Requests First usr Number ************/

	FirstNumber:
	WriteLine("You've chosen addition! Please type the numbers you wish to add.\n\n");
	Write("First number  >>: ");
	rawInput[0] = ReadLine();

	/************ Validation of First usr Number ************/

	if (rawInput[0] == "")
	{
		Clear();
		WriteLine("You didn't type anything >:(");
		goto FirstNumber;
	}

	if (Regex.IsMatch(rawInput[0], @"[\D-[,\.-]]"))
	{
		Clear();
		WriteLine("Hey! That's not allowed!!!\nType something different >:(\n");
		goto FirstNumber;
	}

	/************ Counts Significant Figures ************/

	if (rawInput[0].Contains('.'))
	{
		foreach (char character in rawInput[0].Reverse())
		{
			if (character == '.') break;
			temp--;
		}
	} 
	else 
	{
		foreach (char character in rawInput[0].Reverse())
		{
			if (character != '0') break;
			temp++;
		}
	}

	sigfigs = temp;
	temp	= 0;


	/************ Requests Second usr Number ************/

	SecondNumber:
	WriteLine("                +");
	Write("Second Number >>: ");
	rawInput[1] = ReadLine();

	/************ Validation of Second usr Number ************/

	if (Regex.IsMatch(rawInput[1], @"[\D-[,\.-]]"))
	{
		Clear();
		WriteLine("Hey! That's not allowed!!!\nType something different >:(\n");
		WriteLine("You've chosen addition! Please type the numbers you wish to add.\n\n");
		WriteLine($"First number  >>: {rawInput[0]}");
		goto SecondNumber;
	}

	/************ Counts Significant Figures ************/

	if (rawInput[1].Contains('.'))
	{
		foreach (char character in rawInput[1].Reverse())
		{
			if (character == '.') break;
			temp--;
		}
	}
	else
	{
		foreach (char character in rawInput[1].Reverse())
		{
			if (character != '0') break;
			temp++;
		}
	}

	if (temp > sigfigs) sigfigs = temp;


	/************ Tries to cast usr nums to double, then adds ************/

	try
	{
		num1	= Convert.ToDouble(rawInput[0]);
		num2	= Convert.ToDouble(rawInput[1]);

		if (sigfigs < 0)
		{
			output.Append(MathLib.Basic.add(num1, num2).ToString($"N{Math.Abs(sigfigs)}"));
			goto FinalResult;
		}
		else
		{
			output.Append(MathLib.Basic.add(num1, num2).ToString($"N0"));
		}

		
	}
	catch (FormatException e)
	{
		WriteLine($"Argument could not cast to a double, try a different format.\n\nError message: {e.Message}");
		ReadLine();
		return;
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
	if (!Regex.IsMatch(output.ToString(), @"[\d-[0]]") & output[0] == '-')
	{
		output.Remove(0, 1);
	}

	Write("Result        >>: ");
	WriteLine(output);

	ReadLine();
}
static void Subtraction()
{
	WriteLine($@"		!!!!! NOT YET IMPLEMENTED !!!!!");
	ReadLine();
}
static void Multiplication()
{
	WriteLine($@"		!!!!! NOT YET IMPLEMENTED !!!!!");
	ReadLine();
}
static void Division()
{
	WriteLine($@"		!!!!! NOT YET IMPLEMENTED !!!!!");
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