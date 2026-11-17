using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // TODO Problem 1 - ADD YOUR CODE HERE
        // return [];

        // Create an empty Set to store words we have already visited.
        HashSet<string> visitedWords = new HashSet<string>();

        // Create an empty list to store the symmetric pairs.
        List<string> symmetricPairs = new List<string>();

        // Loop through each word in the input list.
        foreach (string word in words)
        {
            // Reverse the current word to get its possible matching pair.
            string reversedWord = new string(word.Reverse().ToArray());

            // Check whether the reversed word already exists in the HashSet.
            if (visitedWords.Contains(reversedWord))
            {
                // We have found a symmetric pair. Add it to the results.
                symmetricPairs.Add($"{reversedWord} & {word}");
            }
            else
            {
                // Add the current word to the HashSet.
                visitedWords.Add(word);
            }
        }

        // Return the list of pairs.
        return symmetricPairs.ToArray();

    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();


        // Read the file line by line.
        foreach (var line in File.ReadLines(filename))
        {
            // Split each line into columns.
            var fields = line.Split(",");
            // TODO Problem 2 - ADD YOUR CODE HERE

            //Extract column 4(index 3, because C# arrays start at index 0).
            string degree = fields[3].Trim();

            // Check whether the degree already exists in the dictionary.
            if (degrees.ContainsKey(degree))
            {
                // If it does, increment the count for that degree.
                degrees[degree]++;
            }
            // Otherwise, add it to the dictionary with a count of 1.
            else
            {
                degrees.Add(degree, 1);
            }
        }

        // Return the dictionary containing the degree summary.
        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // Remove spaces and convert both words to lowercase.
        word1 = word1.Replace(" ", "").ToLower();
        word2 = word2.Replace(" ", "").ToLower();

        // If the lengths are different, they cannot be anagrams.
        if (word1.Length != word2.Length)
        {
            return false;
        }

        // Create a dictionary to count each letter.
        Dictionary<char, int> letters = new Dictionary<char, int>();

        // Count the letters in the first word.
        foreach (char letter in word1)
        {
            if (letters.ContainsKey(letter))
            {
                letters[letter]++;
            }
            else
            {
                letters.Add(letter, 1);
            }
        }

        // Compare the letters in the second word.
        foreach (char letter in word2)
        {
            if (!letters.ContainsKey(letter))
            {
                return false;
            }

            letters[letter]--;

            if (letters[letter] < 0)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>

    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // Create a list to store the earthquake descriptions.
        var summaries = new List<string>();

        // Go through each earthquake in the collection.
        foreach (var feature in featureCollection.Features)
        {
            // Get the earthquake location and magnitude.
            string place = feature.Properties.Place;
            string magnitude = feature.Properties.Mag?.ToString(
                System.Globalization.CultureInfo.InvariantCulture) ?? "Unknown";

            // Create a formatted description.
            summaries.Add($"{place} - Mag {magnitude}");
        }

        // Convert the list into an array and return it.
        return summaries.ToArray();
    }

}