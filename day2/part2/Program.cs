using System;
using System.Collections.Generic;
using System.IO;

namespace day2
{
    class Solution
    {
        static void Main(string[] args)
        {
            try
            {
                int totalScore = 0;
                foreach (string line in File.ReadLines(@"./input.txt")){
                    var trimmed = line.Trim();
                    var opponent = trimmed[0];
                    var winlosedraw = trimmed[2];
                    var mymove = PartTwoMoveCalc(winlosedraw, opponent);

                    totalScore += EvaluateSolution(mymove, opponent);
                }
                Console.WriteLine($"Part 2 answer is: {totalScore}");
                
            } catch (Exception e){
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        public static char PartTwoMoveCalc(char winlosedraw, char theirs){
            /*
            In part 2, the second column is used to determine if we
            are supposed to draw, win, or lose.

            X is lose, Y is draw, Z is win

            Our output will follow the same mapping as in part 1
            so that we can reuse EvaluateSolution:

            X is ROCK, Y is PAPER, Z is SCISSORS for us
            A is ROCK, B is PAPER, C is SCISSORS for THEM
            */

            switch (theirs){
                case 'A': //ROCK
                    switch (winlosedraw){
                        case 'X':
                            return 'Z';
                        case 'Y':
                            return 'X';
                        case 'Z':
                            return 'Y';
                        default:
                            throw new Exception("Invalid move");
                    }
                case 'B': //PAPER
                    switch (winlosedraw){
                        case 'X':
                            return 'X';
                        case 'Y':
                            return 'Y';
                        case 'Z':
                            return 'Z';
                        default:
                            throw new Exception("Invalid move");
                    }
                case 'C': //SCISSORS
                    switch (winlosedraw){
                        case 'X':
                            return 'Y';
                        case 'Y':
                            return 'Z';
                        case 'Z':
                            return 'X';
                        default:
                            throw new Exception("Invalid move");
                    }
                default:
                    throw new Exception("Invalid move");
            }
            throw new Exception("How did I get here?");
        }

        public static int EvaluateSolution(char winlosedraw, char theirs){
            /*
            Evaluate the solution's score based on the advent challenge...
            This is all pretty arbitrary.

            This function only evaluates "my" score, not theirs.
            */
            
            /*
            Points are awarded in two ways.

            First, depending on the shape thrown, you get points.
            ROCK gets 1, PAPER gets 2, SCISSORS gets 3.

            Second, points are awarded based on who wins.
            I get 0 points if I lose.
                  3 points is it's a draw.
                  6 points if I win.
            */
            
            int shapeScore = 0;
            int winBonus = 0;
            switch (winlosedraw){
                case 'X': //X is "ROCK"
                    shapeScore = 1;
                    switch (theirs){
                        case 'A': //A is "ROCK"
                            winBonus = 3;
                            break;
                        case 'B': //B is "PAPER"
                            winBonus = 0;
                            break;
                        case 'C': //C is "SCISSORS"
                            winBonus = 6;
                            break;
                        default:
                            throw new Exception("Opponent is invalid");
                    }
                    break;
                case 'Y': //Y is "PAPER"
                    shapeScore = 2;
                    switch (theirs){
                        case 'A': //A is "ROCK"
                            winBonus = 6;
                            break;
                        case 'B': //B is "PAPER"
                            winBonus = 3;
                            break;
                        case 'C': //C is "SCISSORS"
                            winBonus = 0;
                            break;
                        default:
                            throw new Exception("Opponent is invalid");
                    }
                    break;
                case 'Z': //Z is "SCISSORS"
                    shapeScore = 3;
                    switch (theirs){
                        case 'A': //A is "ROCK"
                            winBonus = 0;
                            break;
                        case 'B': //B is "PAPER"
                            winBonus = 6;
                            break;
                        case 'C': //C is "SCISSORS"
                            winBonus = 3;
                            break;
                        default:
                            throw new Exception("Opponent is invalid");
                    }
                    break;
                default:
                    throw new Exception("Your move is invalid");
            }
            return shapeScore+winBonus;
        }
    }
}