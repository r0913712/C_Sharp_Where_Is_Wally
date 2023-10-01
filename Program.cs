using System.Diagnostics;

namespace C_Sharp_Where_Is_Wally
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filepath = "names.txt";
            try
            {
                // Read the names from the file into a list
                List<string> names = new List<string>();
                using (StreamReader sr = new StreamReader(filepath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        names.Add(line);
                    }
                }
                List<string> insertionSortedNames = new List<string>(names);
                MeasureSortingTimeAndMemory("Insertion Sort", () => InsertionSort(insertionSortedNames));

                // Measure and sort the names using bubble sort
                List<string> bubbleSortedNames = new List<string>(names);
                MeasureSortingTimeAndMemory("Bubble Sort", () => BubbleSort(bubbleSortedNames));

                // Measure and sort the names using selection sort
                List<string> selectionSortedNames = new List<string>(names);
                MeasureSortingTimeAndMemory("Selection Sort", () => SelectionSort(selectionSortedNames));

                // Measure and sort the names using cocktail shaker sort
                List<string> cocktailSortedNames = new List<string>(names);
                MeasureSortingTimeAndMemory("Cocktail SHaker Sort", () => CocktailShakerSort(cocktailSortedNames));

                // Measure and sort the names using gnome sort
                List<string> gnomeSortedNames = new List<string>(names);
                MeasureSortingTimeAndMemory("Gnome Sort", () => GnomeSort(gnomeSortedNames));

                WriteSortedNamesToFile(insertionSortedNames, "insertion_sorted.txt");
                WriteSortedNamesToFile(bubbleSortedNames, "bubble_sorted.txt");
                WriteSortedNamesToFile(selectionSortedNames, "selection_sorted.txt");

                string searchName = "Wally";
                BinarySearch(searchName, insertionSortedNames);


            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
            static void InsertionSort(List<string> arr)
            {
                for (int i = 1; i < arr.Count; i++)
                {
                    string current = arr[i];
                    int j = i - 1;

                    while (j >= 0 && string.Compare(arr[j], current) > 0)
                    {
                        arr[j + 1] = arr[j];
                        j--;
                    }

                    arr[j + 1] = current;
                }
            }
            static void BubbleSort(List<string> arr)
            {
                for (int i = 0; i < arr.Count - 1; i++)
                {
                    for (int j = 0; j < arr.Count - i - 1; j++)
                    {
                        if (string.Compare(arr[j], arr[j + 1]) > 0)
                        {
                            // Swap arr[j] and arr[j + 1]
                            string temp = arr[j];
                            arr[j] = arr[j + 1];
                            arr[j + 1] = temp;
                        }
                    }
                }
            }
            static void SelectionSort(List<string> arr)
            {
                for (int i = 0; i < arr.Count - 1; i++)
                {
                    int minIndex = i;
                    for (int j = i + 1; j < arr.Count; j++)
                    {
                        if (string.Compare(arr[j], arr[minIndex]) < 0)
                        {
                            minIndex = j;
                        }
                    }

                    // Swap arr[i] and arr[minIndex]
                    string temp = arr[i];
                    arr[i] = arr[minIndex];
                    arr[minIndex] = temp;
                }
            }
            static void CocktailShakerSort(List<string> arr)
            {
                bool swapped;
                do
                {
                    swapped = false;

                    // Sort from left to right (like bubble sort)
                    for (int i = 0; i < arr.Count - 1; i++)
                    {
                        if (string.Compare(arr[i], arr[i + 1]) > 0)
                        {
                            // Swap arr[i] and arr[i+1]
                            string temp = arr[i];
                            arr[i] = arr[i + 1];
                            arr[i + 1] = temp;
                            swapped = true;
                        }
                    }

                    if (!swapped)
                        break;

                    swapped = false;

                    // Sort from right to left
                    for (int i = arr.Count - 2; i >= 0; i--)
                    {
                        if (string.Compare(arr[i], arr[i + 1]) > 0)
                        {
                            // Swap arr[i] and arr[i+1]
                            string temp = arr[i];
                            arr[i] = arr[i + 1];
                            arr[i + 1] = temp;
                            swapped = true;
                        }
                    }
                }
                while (swapped);
            }
            static void GnomeSort(List<string> arr)
            {
                int index = 0;

                while (index < arr.Count)
                {
                    if (index == 0)
                    {
                        index++;
                    }

                    if (string.Compare(arr[index], arr[index - 1]) >= 0)
                    {
                        index++;
                    }
                    else
                    {
                        // Swap arr[index] and arr[index-1]
                        string temp = arr[index];
                        arr[index] = arr[index - 1];
                        arr[index - 1] = temp;
                        index--;
                    }
                }
            }




            static void MeasureSortingTimeAndMemory(string algorithmName, Action sortingAction)
            {
                var watch = Stopwatch.StartNew();
                sortingAction();
                watch.Stop();

                long memoryUsed = GC.GetTotalMemory(false);
                Console.WriteLine($"{algorithmName} took {watch.ElapsedMilliseconds} ms to sort.");
                Console.WriteLine($"{algorithmName} used {memoryUsed / 1024} KB of memory.");
                Console.WriteLine();
            }
            static void WriteSortedNamesToFile(List<string> sortedNames, string fileName)
            {
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    foreach (string name in sortedNames)
                    {
                        sw.WriteLine(name);
                    }
                }
            }

            static void BinarySearch(string searchName, List<string> sortedNames)
            {
                int low = 0;
                int high = sortedNames.Count - 1;

                while (low <= high)
                {
                    int mid = low + (high - low) / 2;
                    int comparisonResult = string.Compare(sortedNames[mid], searchName);

                    if (comparisonResult == 0)
                    {
                        Console.WriteLine($"{searchName} found at index {mid}");
                        return;
                    }
                    else if (comparisonResult < 0)
                    {
                        low = mid + 1;
                    }
                    else
                    {
                        high = mid - 1;
                    }
                }

                Console.WriteLine($"{searchName} not found");
            }
        }
        
    }
}