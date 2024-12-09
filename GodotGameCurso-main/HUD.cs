using Godot;

public partial class HUD : CanvasLayer
{
    private Label labelDia;
    private Label labelMateria;
    private Label labelPontuacao;
    private Label labelDialogo;
    private Panel cardDialogo;

    public override void _Ready()
    {
        labelDia = GetNode<Label>("LabelDia");
        labelMateria = GetNode<Label>("LabelMateria");
        labelPontuacao = GetNode<Label>("LabelPontuacao");
        labelDialogo = GetNode<Label>("CardDialogo/LabelDialogo");
        cardDialogo = GetNode<Panel>("CardDialogo");
    }

    public void AtualizarHUD(int dia, string materia, int pontuacao)
    {
        labelDia.Text = $"Dia: {dia}";
        labelMateria.Text = $"Matéria: {materia}";
        labelPontuacao.Text = $"Pontuação: {pontuacao}";
    }

    public void MostrarDialogo(string texto)
    {
        cardDialogo.Visible = true;
        labelDialogo.Text = texto;
        GetTree().CreateTimer(3).Connect("timeout", new Callable(this, nameof(EsconderDialogo)));
    }

    private void EsconderDialogo()
    {
        cardDialogo.Visible = false;
    }
}
