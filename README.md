# FourRussiansBMM
In this repo you could find implementation of Four Russian’s algorithm for Boolean matrix multiplication.
It was created  at HSE university as Computational Complexity Theory course final activity by Starodumov Egor.

It reads two square boolean matrices of same size from files m1.txt (as matrix A) and m2.txt (as matrix B), which should be located in the same directory as compiled programm,
multiplies them respectively (A \* B = C) using Four Russian’s algorithm for Boolean matrix multiplication and outputs result (C) to console.

To compile and run this code you will need .NET 5.0 SDK from https://dotnet.microsoft.com/download/dotnet/5.0 or Visual Studio.

Contents:
1. FourRussians project - main project with algorithm implementation. Has single file Program.cs with two static classes: 
	a) class Program with method Main which is an entrypoint for the program. Contains reading and preprocessing (splitting) of matrices.
	   Reads first matrix as array of columns and second matrix as array of rows to split them easily later. 
	   After splitting of matrices main algorithm method FourRussiansBMM computes answer, which then is written to console.
	b) class FourRussiansMethods which contains main algorithm method FourRussiansBMM and additional methods used in preprocessing of matrices.
	   FourRussiansBMM precomputes all possible row sums of Bi for each pair of splitted matrices, then constructs Ci from them using Ai data. 
	   Resulting matrix C is computed as sum of all of Ci matrices.  

2. Tests project. Contains xUnit tests for all of the methods in main project. 
Runs automaticly on push or pull request to this repo branch 'main' via GitHub Actions.

