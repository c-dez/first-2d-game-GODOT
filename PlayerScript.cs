using Godot;

public partial class PlayerScript : Area2D
{
	[Export]
	public int speed = 400;

	private Vector2 velocity;
	public Vector2 screenSize;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		screenSize = GetViewportRect().Size;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		PlayerMovement(delta);
		AnimatePlayer(velocity);
	}

	private void PlayerMovement(double delta)
	{
		velocity = Vector2.Zero;
		if(Input.IsActionPressed("move_right"))
		{
			velocity.X += 1;
		}
		if(Input.IsActionPressed("move_left"))
		{
			velocity.X -= 1;
		}
		if(Input.IsActionPressed("move_up"))
		{
			velocity.Y -= 1;
		}
		if(Input.IsActionPressed("move_down"))
		{
			velocity.Y += 1;
		}

		if(velocity.Length() > 0)
		{
			velocity = velocity.Normalized();
		}

		Position += velocity * speed * (float)delta;
		Position = new Vector2(
			x:Mathf.Clamp(Position.X, 0, screenSize.X),
			y:Mathf.Clamp(Position.Y, 0, screenSize.Y)
		);

		


	}

	private void AnimatePlayer( Vector2 velocity)
	{
		var anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		GD.Print(velocity);

		if(velocity.Length() > 0)
		{
			anim.Play();
		}
		else{
			anim.Stop();
		}

		if(velocity.X != 0)
		{
			anim.Animation = "walk";
			anim.FlipV = false;
			anim.FlipH = velocity.X < 0;
		}
		else if(velocity.Y != 0)
		{
			anim.Animation = "up";
			anim.FlipV = velocity.Y > 0;
		}
	}
}
