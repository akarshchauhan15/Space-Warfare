using Godot;
using Godot.Collections;
using System;

public class ScoreEntry
{
    public int Score { get; set; }
    public DateTime Date { get; set; }

    public ScoreEntry(int score)
    {
        Score = score;
        Date = DateTime.Now;
    }
}
public partial class GameData : Resource
{
    public static PackedScene DropScene = GD.Load<PackedScene>("res://Scenes/enemy_drop.tscn");

    public enum ScoreEnum { EnemyHit, MothershipHit, DropPickup, NextStage}
    public static Dictionary<ScoreEnum, int> ScoreValues = new Dictionary<ScoreEnum, int> { 
        { ScoreEnum.EnemyHit, 10},
        { ScoreEnum.MothershipHit, 40},
        { ScoreEnum.DropPickup, 20},
        { ScoreEnum.NextStage, 25},
    };
}
public partial class Stage : Resource
{
    public Vector2 TotalEnemies {  get; set; }
    public Vector2 EnemySize { get; set;}
    public float EnemySpeed { get; set; }
    public int EnemyAcceleration { get; set; }
    public int TotalBunkers { get; set; }
    public int RandomSize { get; set; }

    Array<Array<float>> BunkerXCoords = [[265, 1001], [190, 460, 820, 1090]];

    public Stage(Vector2 totalEnemies, Vector2 enemySize, float enemySpeed, int enemyAcceleration, int totalBunkers, int randomSize)
    {
        TotalEnemies = totalEnemies;
        EnemySize = enemySize;
        EnemySpeed = enemySpeed;
        EnemyAcceleration = enemyAcceleration;
        TotalBunkers = totalBunkers;
        RandomSize = randomSize;
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
        new Stage(new Vector2(6, 6), new Vector2(100, 50), 70, 3, 2, 50),
        new Stage(new Vector2(7, 6), new Vector2(100, 50), 80, 4, 2, 40),
        new Stage(new Vector2(8, 6), new Vector2(100, 50), 85, 4, 4, 35),
        new Stage(new Vector2(8, 6), new Vector2(100, 50), 90, 5, 4, 30),
        new Stage(new Vector2(8, 7), new Vector2(100, 50), 95, 5, 4, 25)
        ];
}
public class EnemyDrops
{
    public enum Types { None, Health, Freeze, Slowdown, BunkerHealth }
    public static string[] ShortNames = ["H+", "Fz", "Sd", "B+"];
}