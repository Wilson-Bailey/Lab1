using Lab1;
using System.Reflection;
using System.Text;

class Program
{
    static List<VideoGame> videoGames;

    //Main Code
    static void Main()
    {
        // Read and create the list of video games
        videoGames = ReadDataAndCreateList();

        // Check if data was successfully loaded
        if (videoGames != null)
        {
            // Sort the list of video games by name
            videoGames.Sort();


            // Display the sorted list of video games
            foreach (var game in videoGames)
            {
                Console.WriteLine(game);
            }

            // 3. Choose a publisher (e.g., Nintendo) and create a sorted list
            string chosenPublisher = "Nintendo"; // Replace with the desired publisher
            PublisherData(chosenPublisher);

            // 5. Choose a genre (e.g., Role-Playing) and create a sorted list
            string chosenGenre = "Role-Playing"; // Replace with the desired genre
            GenreData(chosenGenre);

            

            // 8. Create a method called PublisherData that allows the user to input the publisher
            Console.Write("Enter a publisher: ");
            string userInputPublisher = Console.ReadLine();
            PublisherDataWithUserInput(userInputPublisher);

            // 9. Create a method called GenreData that allows the user to input the genre
            Console.Write("Enter a genre: ");
            string userInputGenre = Console.ReadLine();
            GenreData(userInputGenre);
        }
        else
        {
            // Display an error message if data loading fails
            Console.WriteLine("Failed to read data from the CSV file. Exiting...");
        }
    }

    // Additional methods...

    // Method to filter and display games based on the chosen publisher
    static void PublisherData(string publisher)
    {
        List<VideoGame> publisherGames = videoGames
            .Where(game => game.Publisher == publisher)
            .OrderBy(game => game.Name)
            .ToList();

        DisplayGamesAndPercentage(publisherGames, publisher);
    }

    // Method to filter and display games based on user input for the publisher
    static void PublisherDataWithUserInput(string userInputPublisher)
    {
        List<VideoGame> userPublisherGames = videoGames
            .Where(game => game.Publisher == userInputPublisher)
            .OrderBy(game => game.Name)
            .ToList();

        DisplayGamesAndPercentage(userPublisherGames, userInputPublisher);
    }

    // Method to filter and display games based on the chosen publisher
    static void GenreData(string genre)
    {
        List<VideoGame> genreGames = videoGames
            .Where(game => game.Genre == genre)
            .OrderBy(game => game.Name)
            .ToList();

        DisplayGamesAndPercentage(genreGames, genre);
    }

    // Method to display games and calculate the percentage
    static void DisplayGamesAndPercentage(List<VideoGame> games, string identifier)
    {
        foreach (var game in games)
        {
            Console.WriteLine(game);
        }

        double totalGames = videoGames.Count;
        double gamesCount = games.Count;
        double percentage = (gamesCount / totalGames) * 100;

        Console.WriteLine($"Out of {totalGames} games, {gamesCount} are {identifier} games, which is {percentage:F2}%");
    }

    static List<VideoGame> ReadDataAndCreateList()
    {
        List<VideoGame> games = new List<VideoGame>();

        try
        {
            // Absolute File Path
            string filePath = @"C:\Users\runmu\source\repos\Lab1\Lab1\videogames.csv";

            // Check if the file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: File not found - {filePath}");
                return null;
            }

            using (StreamReader reader = new StreamReader(filePath))
            {
                // first line headers
                string[] headers = reader.ReadLine()?.Split(',');

                while (!reader.EndOfStream)
                {
                    string[] values = reader.ReadLine()?.Split(',');

                    if (values != null && values.Length == 10) // Ensure correct number of columns
                    {
                        // Map CSV columns to VideoGame properties
                        VideoGame game = new VideoGame
                        {
                            Name = values[0],
                            Platform = values[1],
                            Year = int.Parse(values[2]),
                            Genre = values[3],
                            Publisher = values[4],
                            NA_Sales = double.Parse(values[5]),
                            EU_Sales = double.Parse(values[6]),
                            JP_Sales = double.Parse(values[7]),
                            Other_Sales = double.Parse(values[8]),
                            Global_Sales = double.Parse(values[9])
                        };

                        games.Add(game);
                    }
                    else
                    {
                        Console.WriteLine("Invalid data in the CSV file. Skipping a row.");
                    }
                }
            }

            return games;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading data from CSV file: {ex.Message}");
            return null;
        }
    }
}