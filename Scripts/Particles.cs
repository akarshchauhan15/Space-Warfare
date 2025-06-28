using Godot;

public partial class Particles : GpuParticles2D
{
    public override void _Ready() => Finished += QueueFree;

    public void SetStyle(float Spread = 180.0f, Vector2? Direction = null)
    {
        ParticleProcessMaterial Material = ProcessMaterial as ParticleProcessMaterial;
        
        Material.Spread = Spread;
        if (Direction != null) Material.Direction = new Vector3(((Vector2)Direction).X, ((Vector2)Direction).Y, 0f);
        ProcessMaterial = Material;
    }
}