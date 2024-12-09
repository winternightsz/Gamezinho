using Godot;
using System.Collections.Generic;

public partial class Jogo : Node
{
    public int diaAtual = 1; // Dia atual no jogo
    public int pontuacaoTotal = 0; // Pontuação acumulada do jogador

    public List<string> materias = new List<string> { "Matemática", "História", "Biologia", "Física", "Química" };
    public Dictionary<int, string> horario = new Dictionary<int, string>();

    public override void _Ready()
    {
        GerarHorario();
        GD.Print($"Jogo inicializado. Dia atual: {diaAtual}, Pontuação: {pontuacaoTotal}");
    }

    private void GerarHorario()
    {
        // Gera a agenda de matérias para cada dia
        for (int dia = 1; dia <= 10; dia++)
        {
            horario[dia] = materias[(dia - 1) % materias.Count];
        }
    }

    public string ObterMateriaDoDia()
    {
        // Retorna a matéria correspondente ao dia atual
        if (horario.ContainsKey(diaAtual))
        {
            return horario[diaAtual];
        }
        GD.PrintErr($"Dia {diaAtual} não encontrado no horário.");
        return "Nenhuma matéria";
    }

    public void AvancarDia()
    {
        if (diaAtual < 10)
        {
            diaAtual++;
            GD.Print($"Avançando para o dia {diaAtual}. Matéria: {ObterMateriaDoDia()}");
			HUD hud = GetNode<HUD>("/root/Main/HUD");
        	hud.AtualizarHUD(diaAtual, ObterMateriaDoDia(), pontuacaoTotal);
        }
        else
        {
            // Finaliza o jogo após o décimo dia
            GD.Print("Jogo finalizado!");
            GetTree().ChangeSceneToFile("res://FinalDoJogo.tscn");
        }
    }

    public void AdicionarPontuacao(int pontos)
    {
        pontuacaoTotal += pontos;
        GD.Print($"Pontuação atual: {pontuacaoTotal}");
    }
}
