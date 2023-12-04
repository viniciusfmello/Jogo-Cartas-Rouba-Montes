class Cartas
{
    private int numero;
    private string naipe = "";
    public void AdicionarCartas(List<Cartas> cartasBaralho)
    {
        //Gerando carta coringa
        Cartas coringa = new Cartas();
        coringa.Contrutor(14, "CORINGA");
        cartasBaralho.Add(coringa);

        //Gerando cartas do paus
        for (int i = 1; i < 14; i++)
        {
            Cartas carta = new Cartas();
            carta.Contrutor(i, "PAUS");
            cartasBaralho.Add(carta);
        }
        //Gerando cartas ouro
        for (int i = 1; i < 14; i++)
        {
            Cartas carta = new Cartas();
            carta.Contrutor(i, "OURO");
            cartasBaralho.Add(carta);

        }
        //Gerando cartas copas
        for (int i = 1; i < 14; i++)
        {
            Cartas carta = new Cartas();
            carta.Contrutor(i, "COPAS");
            cartasBaralho.Add(carta);
        }
        //Gerando cartas Espadas
        for (int i = 1; i < 14; i++)
        {
            Cartas carta = new Cartas();
            carta.Contrutor(i, "ESPADAS");
            cartasBaralho.Add(carta);
        }
    }
    public void Contrutor(int numero, string naipe)
    {
        this.numero = numero;
        this.naipe = naipe;
    }
    public int GetNumero()
    {
        return numero;
    }
    public string GetNaipe()
    {
        return naipe;
    }
}