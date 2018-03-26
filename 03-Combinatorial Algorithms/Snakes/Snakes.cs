namespace Snakes
{
    using System;
    using System.Collections.Generic;

    public class Snakes
    {
        private const char Up = 'U';
        private const char Down = 'D';
        private const char Left = 'L';
        private const char Right = 'R';

        private static int snakesCount;
        private static char[] snakePath;
        private static HashSet<string> snakes = new HashSet<string>();
        private static HashSet<string> visitedPositions = new HashSet<string>();

        public static void Main()
        {
            InitializeSnakePath();

            GenerateSnake(1, 0, 1, Right); // Priority R D L U

            Console.WriteLine($"Snakes count = {snakesCount}");
        }

        private static void GenerateSnake(int index, int row, int col, char direction)
        {
            if (index >= snakePath.Length)
            {
                var snake = new string(snakePath);

                if (snakePath.Length == 1)
                {
                    Console.WriteLine(snake);
                    return;
                }

                if (!snakes.Contains(snake))
                {
                    MarkIdentical(snake);
                }
            }
            else
            {
                var currentPosition = $"{row} {col}";

                if (!visitedPositions.Contains(currentPosition))
                {
                    visitedPositions.Add(currentPosition);
                    snakePath[index] = direction;

                    // Observe Priority: R D L U
                    GenerateSnake(index + 1, row, col + 1, Right);
                    GenerateSnake(index + 1, row + 1, col, Down);
                    GenerateSnake(index + 1, row, col - 1, Left);
                    GenerateSnake(index + 1, row - 1, col, Up);

                    visitedPositions.Remove(currentPosition);
                }
            }
        }

        private static void MarkIdentical(string snake)
        {
            var reversed = Reverse(snake);
            var flipped = Flip(snake);

            if (!snakes.Contains(reversed) && !snakes.Contains(flipped))
            {
                snakes.Add(reversed);
                snakes.Add(flipped);

                Console.WriteLine(snake);
                snakesCount++;
            }
        }

        private static char[] Rotate(char[] snake)
        {
            while (snake[1] != Right)
            {
                for (int i = 0; i < snake.Length; i++)
                {
                    switch (snake[i])
                    {
                        case Right: snake[i] = Down; break;
                        case Down: snake[i] = Left; break;
                        case Left: snake[i] = Up; break;
                        case Up: snake[i] = Right; break;
                        default: break;
                    }
                }
            }

            return snake;
        }

        private static string Flip(string snake)
        {
            var flipped = new char[snake.Length];

            for (int i = 0; i < snake.Length; i++)
            {
                switch (snake[i])
                {
                    case Up: flipped[i] = Down; break;
                    case Down: flipped[i] = Up; break;
                    default: flipped[i] = snake[i]; break;
                }
            }

            return new string(Rotate(flipped)); // return right rotated
        }

        private static string Reverse(string snake)
        {
            var reversed = new char[snake.Length];
            reversed[0] = snake[0];

            for (int i = 1; i < snake.Length; i++)
            {
                reversed[i] = snake[snake.Length - i];
            }

            return new string(Rotate(reversed)); // return right rotated
        }

        private static void InitializeSnakePath()
        {
            var length = int.Parse(Console.ReadLine());

            snakePath = new char[length];
            snakePath[0] = 'S';

            visitedPositions.Add($"0 0");
        }
    }
}
