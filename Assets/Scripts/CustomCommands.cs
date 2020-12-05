using UnityEngine;
using CommandTerminal;

public class CustomCommands : MonoBehaviour
{
    private void Start()
    {
        Terminal.Shell.AddCommand("spawn", CommandSpawn, 1, 1, "Spawn an object");
        Terminal.Shell.AddCommand("load", CommandLoad, 1, 1, "Loads a level");
        Terminal.Shell.AddCommand("wtf", CommandWTF, 0, 0, "WTF???");
        Terminal.Shell.AddCommand("die", CommandDie, 0, 0, "You will die");
        Terminal.Shell.AddCommand("god", CommandGod, 0, 0, "Become like a developer");

    }

    [RegisterCommand(Help = "Load level", MinArgCount = 1, MaxArgCount = 1)]
    static void CommandLoad(CommandArg[] args)
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
    static void CommandGod(CommandArg[] args)
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

    //[RegisterCommand(Help = "Debug kill unity", MinArgCount = 0, MaxArgCount = 0)]
    //static void CommandDebugKill(CommandArg[] args)
    //{
    //    if (Terminal.IssuedError)
    //        return;

    //    var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    //    player.MobKilled?.Invoke();

    //    Terminal.Log("Done");
    //}

    [RegisterCommand(Help = "Spawn new object", MinArgCount = 1, MaxArgCount = 1)]
    static void CommandSpawn(CommandArg[] args)
    {
        string name = args[0].String;

        if (Terminal.IssuedError) return;

        GameObject spawningPrefab;

        spawningPrefab = Resources.Load<GameObject>($"Prefabs/{name}");

        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        var spawningPosition = new Vector3(player.position.x + 2, player.position.y, player.position.z);


        Instantiate(spawningPrefab, spawningPosition, Quaternion.identity);

        Terminal.Log("Done");
    }
}
