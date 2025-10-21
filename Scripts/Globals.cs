using Godot;
using Godot.Collections;

public partial class Stage : Resource
{
    public Vector2 TotalEnemies {  get; set; }
    public Vector2 EnemySize { get; set;}
    public float EnemySpeed { get; set; }
    public int EnemyAcceleration { get; set; }
    public int TotalBunkers { get; set; }

    Array<Array<float>> BunkerXCoords = [[265, 1001], [190, 460, 820, 1090]];

    public Stage(Vector2 totalEnemies, Vector2 enemySize, float enemySpeed, int enemyAcceleration, int totalBunkers)
    {
        TotalEnemies = totalEnemies;
        EnemySize = enemySize;
        EnemySpeed = enemySpeed;
        EnemyAcceleration = enemyAcceleration;
        TotalBunkers = totalBunkers;
    }
    public Array<Vector2> GetBunkerCoords()
    {
        Array<Vector2> BunkerCoords = new Array<Vector2>();
        foreach (float X in BunkerXCoords[TotalBunkers/2 - 1])
            BunkerCoords.Add(new Vector2(X, 580));

        return BunkerCoords;
    }
}
public class Stages
{
    public static Array<Stage> stages = [
        new Stage(new Vector2(6, 6), new Vector2(100, 50), 70, 3, 2),
        new Stage(new Vector2(7, 6), new Vector2(100, 50), 80, 4, 2),
        new Stage(new Vector2(8, 6), new Vector2(100, 50), 85, 4, 4),
        new Stage(new Vector2(8, 6), new Vector2(100, 50), 90, 5, 4),
        new Stage(new Vector2(8, 7), new Vector2(100, 50), 95, 5, 4)
        ];

    /*public static Array<Stage> stages = [
    new Stage(new Vector2(2, 2), new Vector2(100, 50), 80, 3, 2),
        new Stage(new Vector2(2, 2), new Vector2(100, 50), 90, 4, 2)
    ];*/
}