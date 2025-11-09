using Godot;

[GlobalClass]
public partial class AnimatedPanel : Panel
{
    [ExportCategory("Duration")]
    [Export]
    float AppearTime = 0.3f;
    float DissappearTime = 0.4f;
    [ExportCategory("Transition")]
    [Export]
    Tween.TransitionType AppearTransition = Tween.TransitionType.Sine;
    [Export]
    Tween.TransitionType DissappearTransition = Tween.TransitionType.Sine;
    [Export]
    Tween.EaseType AppearEasing = Tween.EaseType.Out;
    [Export]
    Tween.EaseType DissappearEasing = Tween.EaseType.In;

    Button CloseButton;

    public override void _Ready()
    {
        CloseButton = GetNode<Button>("CloseButton");
        CloseButton.Pressed += Dissappear;
    }
    public virtual void Appear(NodePath AutoFocusControl = null)
    {
        if (Visible) return;
        Show();
        Tween tween = CreateTween();
        tween.SetParallel(true);
        tween.TweenProperty(this, "modulate:a", 1f, AppearTime).SetTrans(AppearTransition).SetEase(AppearEasing).From(0f);

        if (AutoFocusControl != null)
            GetNode<Control>(AutoFocusControl).GrabFocus();
    }
    public virtual void Dissappear()
    {
        CloseButton.Disabled = true;
        Tween tween = CreateTween();
        tween.SetParallel(true);
        tween.TweenProperty(this, "modulate:a", 0f, DissappearTime).SetTrans(DissappearTransition).SetEase(DissappearEasing);
        tween.SetParallel(false);
        tween.TweenCallback(Callable.From(() => { Hide(); CloseButton.Disabled = false; }));
    }
}
