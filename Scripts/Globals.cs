using Godot;
using Godot.Collections;

public partial class Stage : Resource
{
    public Vector2 TotalEnemies {  get; set; }
    public Vector2 EnemySize { get; set;}
    public float EnemySpeed { get; set; }
    public int EnemyAcceleration { get; set; }

    public Stage(Vector2 totalEnemies, Vector2 enemySize, float enemySpeed, int enemyAcceleration)
    {
        TotalEnemies = totalEnemies;
        EnemySize = enemySize;
        EnemySpeed = enemySpeed;
        EnemyAcceleration = enemyAcceleration;
    }
}
public class Stages
{
    public static Array<Stage> stages = [
        new Stage(new Vector2(8, 6), new Vector2(100, 50), 80, 3),
        new Stage(new Vector2(8, 6), new Vector2(100, 50), 90, 4)
        ];
}
