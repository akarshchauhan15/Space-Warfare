using Godot;

public partial class Main : CanvasLayer
{
    [Signal]
    public delegate void GameStartedEventHandler();

    PackedScene EnemyScene;

    Node EnemyContainer;
    Timer ShiftCooldownTimer;

    Vector2 TotalEnemies = new Vector2(8, 6);
    Vector2 EnemySize = new Vector2(100, 50);

    public static Vector2 Direction = Vector2.Right;
    public static float Speed = 80;

    public override void _Ready()
    {
        base._Ready();

        EnemyScene = ResourceLoader.Load<PackedScene>("res://Scenes/enemy.tscn");

        EnemyContainer = GetNode<Node>("Playground/Enemies");
        ShiftCooldownTimer = GetNode<Timer>("Playground/ShiftCooldownTimer");

        GetNode<Area2D>("Playground/Boundaries").BodyEntered += ShiftEnemies;

        SetEnemies();
    }

    private void SetEnemies()
    {
        int Row = 0;

        while (Row < TotalEnemies.X)
        {
            int Column = 0;

            while (Column < TotalEnemies.Y)
            {
                InitializeEnemies(Row, Column);
                Column++;
            }
            Row++;
        }
    }
    private void InitializeEnemies(int Row, int Column)
    {
        Enemy Enemy = EnemyScene.Instantiate<Enemy>();
        Enemy.GlobalPosition = Vector2.One * 100 + new Vector2(Row * EnemySize.X, Column * EnemySize.Y);
        EnemyContainer.AddChild(Enemy);
    }
    private void ShiftEnemies(Node2D Body)
    {
        if (ShiftCooldownTimer.TimeLeft != 0)
            return;

        ShiftCooldownTimer.Start();
        Direction = Direction * -1;
        foreach (Enemy enemy in EnemyContainer.GetChildren())
            enemy.Position += Vector2.Down * 50;
    }
}
