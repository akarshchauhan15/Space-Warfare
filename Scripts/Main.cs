using Godot;

public partial class Main : CanvasLayer
{
    PackedScene EnemyScene;

    Node EnemyContainer;

    Vector2 TotalEnemies = new Vector2(8, 6);
    Vector2 EnemySize = new Vector2(100, 60);

    public override void _Ready()
    {
        base._Ready();

        EnemyScene = ResourceLoader.Load<PackedScene>("res://Scenes/enemy.tscn");
        EnemyContainer = GetNode<Node>("Playground/Enemies");

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
}
