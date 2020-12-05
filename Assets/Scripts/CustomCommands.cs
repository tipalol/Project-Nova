using UnityEngine;
using CommandTerminal;

public class CustomCommands : MonoBehaviour
{
    [RegisterCommand(Help = "Set last saved level", MinArgCount = 1, MaxArgCount = 1)]
    static void CommandSetLastLevel(CommandArg[] args)
    {
        string name = args[0].String;

        if (Terminal.IssuedError) return;
        
        PlayerPrefs.SetString("LastSceneName", name);
        Terminal.Log($"{name} will be loaded if continue", name);
    }

    [RegisterCommand(Help = "Load level", MinArgCount = 1, MaxArgCount = 1)]
    static void CommandLoadLevel(CommandArg[] args)
    {
        string name = args[0].String;

        if (Terminal.IssuedError) return;

        Initiate.Fade(name, new Color(0, 0, 0), 1f);
    }

    [RegisterCommand(Help = "WTF???", MinArgCount = 0, MaxArgCount = 0)]
    static void CommandWTF(CommandArg[] args)
    {
        if (Terminal.IssuedError) return;

        var spawner = Instantiate(Resources.Load<Spawner>("Prefabs/Spawner"), new Vector3(10, 13, 0), Quaternion.identity);
        Destroy(spawner, 15f);
    }

    [RegisterCommand(Help = "Die", MinArgCount = 0, MaxArgCount = 0)]
    static void CommandDie(CommandArg[] args)
    {
        if (Terminal.IssuedError)
            return;

        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.Die();
        Terminal.Log("ok....");
    }

    [RegisterCommand(Help = "Makes you like developer", MinArgCount = 0, MaxArgCount = 0)]
    static void CommandBecomeGod(CommandArg[] args)
    {
        if (Terminal.IssuedError)
            return;

        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.IsInvulnerable = true;
        Terminal.Log("Done");
    }

    [RegisterCommand(Help = "Add exp", MinArgCount = 1, MaxArgCount = 1)]
    static void CommandAddExp(CommandArg[] args)
    {
        int exp = args[0].Int;

        if (Terminal.IssuedError) return;
        if (exp < 0)
        {
            Terminal.Log("Must be 0 or more");
            return;
        }

        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        player.CollectCoin(exp);

        player.ExpCollected?.Invoke(exp);
        Terminal.Log("Done");
    }

    [RegisterCommand(Help = "Sets player's speed", MinArgCount = 1, MaxArgCount = 1)]
    static void CommandSetSpeed(CommandArg[] args)
    {
        int exp = args[0].Int;

        if (Terminal.IssuedError) return;
        if (exp < 0)
        {
            Terminal.Log("Must be 0 or more");
            return;
        }

        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Moving>();

        player._speed = exp;

        Terminal.Log("Done");
    }

    [RegisterCommand(Help = "Sets player's jump power", MinArgCount = 1, MaxArgCount = 1)]
    static void CommandSetJumpPower(CommandArg[] args)
    {
        int exp = args[0].Int;

        if (Terminal.IssuedError) return;
        if (exp < 0)
        {
            Terminal.Log("Must be 0 or more");
            return;
        }

        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Jumping>();

        player._jumpForce = exp;

        Terminal.Log("Done");
    }

    [RegisterCommand(Help = "Sets world's gravity", MinArgCount = 1, MaxArgCount = 1)]
    static void CommandSetGravity(CommandArg[] args)
    {
        float exp = args[0].Float;

        if (Terminal.IssuedError) return;


        Physics2D.gravity = new Vector2(0, exp);

        Terminal.Log("Done");
    }

    [RegisterCommand(Help = "Debug kill unity", MinArgCount = 0, MaxArgCount = 0)]
    static void CommandDebugKill(CommandArg[] args)
    {
        if (Terminal.IssuedError)
            return;

        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        player.MobKilled?.Invoke();

        Terminal.Log("Done");
    }
}
