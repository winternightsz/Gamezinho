using Godot;
using System.Collections.Generic;

public partial class Textbox : CanvasLayer
{
    [Signal]
    public delegate void DialogoConcluidoEventHandler();

    private TextureRect background; // Textura de fundo
    private Label textoLabel; // Label para exibir o texto
    private Queue<string> falas = new Queue<string>(); // Fila de falas
    private bool exibindoTexto = false;

    public override void _Ready()
    {
        var layout = GetNode<Control>("LayoutTextbox");
        // Referências aos nós
        background = layout.GetNode<TextureRect>("Background");
        textoLabel = layout.GetNode<Label>("Texto"); // Label está fora do TextureRect



        // Ajusta a posição e o tamanho do Label com base no TextureRect


        // Inicialmente, ocultar o Textbox
        this.Visible = false;

        GD.Print("Textbox configurado e invisível no início.");
    }

    /// <summary>
    /// Ajusta a posição e o tamanho do Label com base no TextureRect.
    /// </summary>


    /// <summary>
    /// Exibe as falas fornecidas na fila.
    /// </summary>
    public void MostrarFalas(List<string> dialogos)
    {
        falas.Clear();
        foreach (var fala in dialogos)
        {
            falas.Enqueue(fala);
        }

        this.Visible = true;
        MostrarProximaFala();
    }

    /// <summary>
    /// Mostra a próxima fala na fila.
    /// </summary>
    private void MostrarProximaFala()
    {
        if (falas.Count > 0)
        {
            textoLabel.Text = falas.Dequeue();
            exibindoTexto = true;
        }
        else
        {
            OcultarTextbox();
            EmitSignal(nameof(DialogoConcluido)); // Emite o sinal ao finalizar o diálogo
        }
    }

    /// <summary>
    /// Oculta o Textbox.
    /// </summary>
    private void OcultarTextbox()
    {
        this.Visible = false;
        exibindoTexto = false;
    }

    public override void _Input(InputEvent @event)
    {
        // Avança para a próxima fala ao pressionar qualquer tecla
        if (exibindoTexto && @event.IsPressed())
        {
            MostrarProximaFala();
        }
    }
}
