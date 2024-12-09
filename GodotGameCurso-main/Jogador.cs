using Godot;
using System;

public partial class Jogador : CharacterBody2D
{
	private Vector2 velocity;
	[Export] AnimatedSprite2D animar;
	public void animacao () {
		if (Input.IsActionPressed("ui_up")){
			//direcao.Y -= 1;
			animar.Play("walk_back");
		}
		else if (Input.IsActionPressed("ui_down")){
			//direcao.Y += 1;
			animar.Play("walk_front");
		}
		else if (Input.IsActionPressed("ui_right")){
			//direcao.X += 1;
			animar.FlipH = true;
			animar.Play("walk_left");
		}
		else if (Input.IsActionPressed("ui_left")){
			//direcao.X += 1;
			animar.FlipH = false;
			animar.Play("walk_left");
		}
		
	}
	
	public override void _PhysicsProcess(double delta)
	{
		velocity = Velocity;

		

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		
		
		velocity = direction.Normalized() * 100;

		Velocity = velocity;
		animacao();
		MoveAndSlide();
	}
}
