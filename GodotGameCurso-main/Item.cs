using Godot;

public partial class Item : Area2D
{
    [Export]
    public string nome;

    public void Coletar()
    {
        GD.Print($"Item coletado: {nome}");
        QueueFree(); // Remove o item da cena
    }
	    private void _on_Item_body_entered(Node corpo)
    {
        if (corpo is Jogador)
        {
            Coletar();
        }
    }
}
