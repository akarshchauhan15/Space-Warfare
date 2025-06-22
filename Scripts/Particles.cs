using Godot;


public partial class Particles : GpuParticles2D
{
    public override void _Ready() => Finished += QueueFree;
}
