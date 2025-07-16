using Godot;
using Godot.Collections;
using System;

public partial class Main : CanvasLayer
{
    PackedScene EnemyScene;
    PackedScene BunkerScene;
    PackedScene MothershipScene;
    public static PackedScene DestructionParticleScene;
    public static PackedScene BulletScene;

    Array<Texture2D> EnemyTextures = new Array<Texture2D>();

    public Node EnemyContainer;
    Timer ShiftCooldownTimer;
    Timer MothershipSpawnTimer;
    public Timer RandomShootTimer;

    public static Stage Stage = Stages.stages[0];
    public static Vector2 EnemyDirection = Vector2.Right;
    public static int CurrentStage = 1;

    public static bool IsPlaying = false;
    public static bool EnemyDownCheck = false;
    public static bool IsMothershipInAction = false;
    
    public override void _Ready()
    {
        base._Ready();

        EnemyScene = ResourceLoader.Load<PackedScene>("res://Scenes/enemy.tscn");
        BunkerScene = ResourceLoader.Load<PackedScene>("res://Scenes/bunker.tscn");
        MothershipScene = ResourceLoader.Load<PackedScene>("res://Scenes/mothership.tscn");
        DestructionParticleScene = ResourceLoader.Load<PackedScene>("res://Scenes/Particles/destruction_particles.tscn");
        BulletScene = ResourceLoader.Load<PackedScene>("res://Scenes/bullet.tscn");

        for (int i = 1; i <= 4; i++)
            EnemyTextures.Add(GD.Load<Texture2D>($"res://Assets/Art/Exported/Enemy{i}.png"));

        EnemyContainer = GetNode<Node>("Playground/Enemies");
        ShiftCooldownTimer = GetNode<Timer>("Playground/Timers/ShiftCooldownTimer");
        RandomShootTimer = GetNode<Timer>("Playground/Timers/RandomShootTimer");
        MothershipSpawnTimer = GetNode<Timer>("Playground/Timers/MothershipSpawnTimer");

        GetNode<Area2D>("Playground/Boundaries").BodyEntered += ShiftEnemies;
        GetNode<Area2D>("Playground/DeadZone").BodyEntered += (Node2D Body) => GetNode<Hud>("HUD").EndGame(false);
        RandomShootTimer.Timeout += MakeRandomEnemyShoot;
        MothershipSpawnTimer.Timeout += SpawnMothership;

        StartGame();
    }
    public void StartGame()
    {
        Enemy.Speed = Stage.EnemySpeed;
        SetEnemies();
        SetBunkers();

        IsPlaying = true;
        GetNode<Player>("Playground/Player").Velocity = Vector2.Zero;
        RandomShootTimer.Start(1);
        MothershipSpawnTimer.Start(5);
    }

    private void SetEnemies()
    {
        int Row = 0;

        while (Row < Stage.TotalEnemies.X)
        {
            int Column = 0;

            while (Column < Stage.TotalEnemies.Y)
            {
                InitializeEnemies(Row, Column);
                Column++;
            }
            Row++;
        }
    }
    private void SetBunkers()
    {
        Array<Vector2> CoordsList = Stage.GetBunkerCoords();
        Node BunkerContainer = GetNode<Node>("Playground/Bunkers");

        foreach(Vector2 Coord in CoordsList)
        {
            Bunker Bunker = BunkerScene.Instantiate<Bunker>();
            Bunker.GlobalPosition = Coord;
            BunkerContainer.AddChild(Bunker);
        }
    }
    private void InitializeEnemies(int Row, int Column)
    {
        Enemy Enemy = EnemyScene.Instantiate<Enemy>();
        Enemy.GlobalPosition = Vector2.One * 100 + new Vector2(Row * Stage.EnemySize.X, Column * Stage.EnemySize.Y);
        Enemy.Texture = EnemyTextures[Column%4];

        EnemyContainer.AddChild(Enemy);
    }
    private void ShiftEnemies(Node2D Body)
    {
        if (ShiftCooldownTimer.TimeLeft != 0)
            return;

        ShiftCooldownTimer.Start();
        EnemyDirection = EnemyDirection * -1;
        foreach (Enemy enemy in EnemyContainer.GetChildren())
            enemy.Position += Vector2.Down * 50;
    }
    private void MakeRandomEnemyShoot()
    {
        Random Roll = new Random();
        
        int Enemies = EnemyContainer.GetChildCount();
        int Index = Roll.Next(Enemies);

        if (!EnemyDownCheck && (Enemies <= 5))
            EnemyDownCheck = true;

        if (Enemies <= 0)
            return;

        EnemyContainer.GetChild<Enemy>(Index).Shoot();

        RandomShootTimer.Start(0.6 + Roll.NextDouble() * 2);
    }
    private void SpawnMothership()
    {
        Mothership Mothership = MothershipScene.Instantiate<Mothership>();

        Mothership.Direction = (int) ( 2 * (0.5d - new Random().Next(0, 2)));
        Mothership.GlobalPosition = new Vector2(640 + (700 * Mothership.Direction), 76);

        IsMothershipInAction = true;
        
        GetNode("Playground/Extras").AddChild(Mothership);
    }
}
