using System;
using System.Threading.Tasks;

public class Utils
{
    // Fyodor Likhachev's Legacy (Also Player, Moving, Ability and Achievements though)
    public static void ExecuteWithDelay(Action action, int seconds = 10)
        => new Task(() => { Task.Delay(seconds * 1000).GetAwaiter().OnCompleted(action); }).Start();
}
