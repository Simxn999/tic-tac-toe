using static System.Console;

namespace TicTacToe.Navigation;

public class Page
{
    public bool Exit { get; set; }
    public int ReturnValue { get; set; }
    protected string Title { get; set; } = "";
    protected Dictionary<string, Action> Options { get; set; } = new();
    protected Action Content { get; set; } = () => { };
    protected bool HasPreviousPage { get; set; }
    protected bool HasNextPage { get; set; }
    int OptionsCount { get; set; }
    int PageCount { get; set; }
    int CurrentPage { get; set; }
    int StartIndex { get; set; }
    int TotalCount => Options.Count;

    public Page() => Initialize();
    
    void Initialize()
    {
        Exit = false;
        ReturnValue = 0;

        Options.Clear();
        
        HasPreviousPage = false;
        HasNextPage = false;
        
        OptionsCount = 9;
        PageCount = 1;
        CurrentPage = 1;
        StartIndex = 0;

        Validate();
    }

    void Validate()
    {
        if (TotalCount > 9)
            PageCount = TotalCount / 9 + 1;

        if (CurrentPage < 1)
            CurrentPage = 1;

        if (CurrentPage > PageCount)
            CurrentPage = PageCount;

        OptionsCount = TotalCount - (CurrentPage - 1) * 9;
        if (OptionsCount > 9)
            OptionsCount = 9;

        StartIndex = (CurrentPage - 1) * 9;
        HasPreviousPage = CurrentPage > 1;
        HasNextPage = CurrentPage < PageCount;
    }

    public void Run()
    {
        Initialize();
        GetInput();
    }

    protected virtual void UpdateOptions() { }

    void GetInput()
    {
        while (true)
        {
            if (Exit) break;
            
            UpdateOptions();
            Validate();
            var filteredOptions = Options.Where(o => o.Key.Count(c => c == '#') != 2).ToList();
            var currentOptions = Options.Where(o =>
            {
                var index = filteredOptions.IndexOf(o);

                return index >= StartIndex && index <= StartIndex + OptionsCount - 1;
            }).ToList();

            Print();
            
            var key = ReadKey(true).Key;

            switch (key)
            {
                case >= ConsoleKey.D1 and <= ConsoleKey.D9:
                    if (currentOptions.Count > key - ConsoleKey.D1)
                        currentOptions.ElementAt(key - ConsoleKey.D1).Value.Invoke();
                    
                    continue;
                
                case ConsoleKey.LeftArrow:
                    if (!HasPreviousPage)
                        continue;

                    SetPage(CurrentPage - 1);
                    break;

                case ConsoleKey.RightArrow:
                    if (!HasNextPage)
                        continue;

                    SetPage(CurrentPage + 1);
                    break;

                case ConsoleKey.Q:
                    Environment.Exit(0);
                    break;

                case ConsoleKey.Escape:
                    if (GetType().Name.Equals("HomePage"))
                        continue;

                    if (HasPreviousPage)
                        goto case ConsoleKey.LeftArrow;

                    Exit = true;
                    break;

                case ConsoleKey.Enter:
                    var enter = Options.FirstOrDefault(o => o.Key.Contains("#enter#"));
                    
                    if (enter.Equals(default(KeyValuePair<string, Action>))) continue;

                    enter.Value.Invoke();

                    continue;

                case ConsoleKey.Delete:
                    var delete = Options.FirstOrDefault(o => o.Key.Contains("#delete#"));

                    if (delete.Equals(default(KeyValuePair<string, Action>))) continue;

                    delete.Value.Invoke();

                    continue;

                default:
                    continue;
            }

            break;
        }
    }

    void SetPage(int page)
    {
        CurrentPage = page;
        GetInput();
    }

    public void Print()
    {
        Clear();

        WriteLine(Title);

        CursorTop = 0;

        if (PageCount > 1)
            PrintText($"Page: {CurrentPage} / {PageCount}");

        for (var i = StartIndex; i < StartIndex + OptionsCount; i++)
        {
            var key = Options.ElementAt(i).Key;
            
            if (key.Contains("!disabled!"))
            {
                PrintOption(key[10..], $"{i - StartIndex + 1}", true);
                continue;
            }
            if (key.Count(c => c == '#') == 2) continue;
            
            PrintOption(Options.Keys.ElementAt(i), $"{i - StartIndex + 1}");
        }

        var enter = Options.Keys.FirstOrDefault(k => k.Contains("#enter#"));
        if (enter is not null)
            PrintOption(enter[7..], "ENTER");
        
        var delete = Options.Keys.FirstOrDefault(k => k.Contains("#delete#"));
        if (delete is not null)
            PrintOption(delete[8..], "DEL");

        if (HasPreviousPage)
            PrintOption("Previous Page", "<-");

        if (HasNextPage)
            PrintOption("Next Page", "->");

        PrintOption("Go Back", "ESC");

        PrintOption("Exit Application", "Q");

        CursorTop = 2;

        Content.Invoke();
    }

    public static void PrintOption(string title, string key, bool disabled = false, bool ltr = false)
    {
        var output = $"[{key}] - {title}";

        if (!ltr)
        {
            CursorLeft = WindowWidth - output.Length;
            output = $"{title} - [{key}]";
        }

        if (disabled)
            ForegroundColor = ConsoleColor.DarkGray;
        WriteLine(output);
        ResetColor();
    }

    public static void PrintText(string title, bool ltr = false)
    {
        if (!ltr)
            CursorLeft = WindowWidth - title.Length;

        WriteLine(title);
    }

    public static void PrintError(string message)
    {
        Clear();
        WriteLine(message);
        WriteLine("\nPress any key to continue...");
        ReadKey(true);
    }
}