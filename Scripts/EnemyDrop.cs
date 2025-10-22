using Godot;

public partial class EnemyDrop : Area2D
{
    public int Speed;
    public EnemyDrops DropType;

    public override void _Ready()
    {
        Speed = GD.RandRange(50, 80);
        BodyEntered += OnCollision;
    }
    public override void _Process(double delta) => Position += Vector2.Down * Speed * (float) delta;

    public void OnCollision(Node2D Body)
    {
        QueueFree();
        Hud.AddScore(20);

        Player Player = GetNode<Player>("../../Player");
        Node2D EnemyContainer = GetNode<Node2D>("../../Enemies");

        switch (DropType) 
        {
            case EnemyDrops.Health:
                Player.Health += 1;
                Player.Health = Mathf.Min(Player.Health, 3);
                break;

            case EnemyDrops.Freeze:
                Timer FreezeTimer = new Timer();
                FreezeTimer.OneShot = true;
                GetNode("../../Timers").AddChild(FreezeTimer);

                Timer RandomShootTimer = GetNode<Timer>("../../Timers/RandomShootTimer");

                FreezeTimer.Timeout += () =>
                {
                    EnemyContainer.Modulate = new Color(0.557f, 0.678f, 1.0f);
                    EnemyContainer.ProcessMode = ProcessModeEnum.Inherit;
                    RandomShootTimer.Stop();
                    FreezeTimer.QueueFree();
                };


                EnemyContainer.Modulate = new Color(0.557f, 0.678f, 1.0f);
                EnemyContainer.ProcessMode = ProcessModeEnum.Disabled;
                RandomShootTimer.Stop();

                FreezeTimer.Start(3);
         

                /*FreezeTimer.Timeout += () => 
                {
                    foreach (Enemy Enemy in EnemyContainer.GetChildren())
                    {
                        Enemy.ProcessMode = ProcessModeEnum.Inherit
  
                    }
                };

                foreach (Enemy Enemy in EnemyContainer.GetChildren())
                {
                    Enemy.ProcessMode = ProcessModeEnum.Disabled
                    Enemy.GetNode<AnimationPlayer>("AnimationPlayer").Pause();
                    Enemy.GetNode<Sprite2D>("Enemy").SelfModulate = new Color(0.557f, 0.678f, 1.0f);
                }*/

                FreezeTimer.Start(3);
                break;
        }
    }
}
