using LibraryManagementSystem;

public class Program
{
    public static void Main(string[] args)
    {
        Thread t1 = new(new ThreadStart(LibraryManager.PromptUser));
        Thread t2 = new(new ThreadStart(LibraryManager.PromptUser));

        t1.Start();
        t2.Start();

        t1.Join();
        t2.Join();
    }
}