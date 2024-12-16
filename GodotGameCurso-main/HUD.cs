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
    // Inicializa os nós do HUD
    labelDia = GetNode<Label>("LabelDia");
    labelMateria = GetNode<Label>("LabelMateria");
    labelPontuacao = GetNode<Label>("LabelPontuacao");
    labelDialogo = GetNode<Label>("CardDialogo/LabelDialogo");
    cardDialogo = GetNode<Panel>("CardDialogo");

    // Conecta ao sinal de atualização do estado
    Jogo jogo = (Jogo)GetNode("/root/Jogo");
    jogo.Connect("EstadoAtualizado", new Callable(this, nameof(AtualizarHUD)));

    // Atualiza o HUD inicialmente
    AtualizarHUD();
}


    public void AtualizarHUD()
    {
        // Pega os valores diretamente do script global Jogo
        Jogo jogo = (Jogo)GetNode("/root/Jogo");
        labelDia.Text = $"Dia: {jogo.diaAtual}";
        labelMateria.Text = $"Matéria: {jogo.ObterMateriaDoDia()}";
        labelPontuacao.Text = $"Pontuação: {jogo.pontuacaoTotal}";
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
