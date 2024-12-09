using Godot;
using System;

public partial class Area : Area2D
{
    [Export]
    //private int tipo = 1;
    //private int tipo2 = 1;
    private bool dentro = false;

    public void EntrouArea(Node2D corpo)
    {
        if (corpo is Player) {
            dentro = true;
            GetTree().ChangeSceneToFile("res://quiz.tscn");
        }
        
    }

    public void SaiuArea(Node2D corpo)
    {
        if (corpo is Player)
        {
            dentro = false;
        }

        
    }
	

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        //if (dentro)
        //{
            
        //}
	}
}
