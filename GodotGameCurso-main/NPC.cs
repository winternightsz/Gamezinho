using Godot;

public partial class NPC : Area2D
{
    [Export]
    public string[] falas;

    private int indiceAtual = 0;

    public void Interagir()
    {
		GD.Print("Interagindo com NPC...");
        HUD hud = GetNode<HUD>("/root/CenarioPrincipal/HUD");
        if (indiceAtual < falas.Length)
        {
			GD.Print($"NPC: {falas[indiceAtual]}");
            hud.MostrarDialogo(falas[indiceAtual]);
            indiceAtual++;
        }
        else
        {
            hud.MostrarDialogo("Não tenho mais nada a dizer!");
        }
    }
}
