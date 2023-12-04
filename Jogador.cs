class Jogador
{
    private string nome;
    private string PIN;
    public int qtdCartasMonte;
    public Stack<Cartas> monteJogador = new Stack<Cartas>();
    public Queue<int> ranking;

    public Jogador(string nome, string PIN)
    {
        this.nome = nome;
        this.PIN = PIN;
        this.ranking = new Queue<int>(5);
    }
    public string getNome()
    {
        return nome;
    }
    public int getQtdCartasMonte()
    {
        return monteJogador.Count;
    }
    public string getPin()
    {
        return PIN;
    }
    public Cartas getTopoMonte()
    {
        return monteJogador.Peek();
    }
}