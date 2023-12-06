using System.Diagnostics;
class Partida
{
    private Stack<Cartas> cartasMesa = new Stack<Cartas>();
    public List<Cartas> EmbaralharCartas(List<Cartas> cartasBaralho, int quantBaralhos, string caminhoArquivoLogs)
    {
        Random numerosAleatorios = new Random();
        Cartas[] cartasEmbaralhadas = cartasBaralho.ToArray();
        List<int> PosicoesQueJaforam = new List<int>();
        int posicao, i = 0;

        while (i < 53 * quantBaralhos)
        {
            posicao = numerosAleatorios.Next(0, 53 * quantBaralhos);
            if (!PosicoesQueJaforam.Contains(posicao))
            {
                PosicoesQueJaforam.Add(posicao);
                cartasEmbaralhadas[i] = cartasBaralho[posicao];
                i++;
            }
        }
        cartasBaralho = cartasEmbaralhadas.ToList();


        using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs, true))
        {
            escritorArquivo.WriteLine($"- O baralho foi embaralhado");
        }
        
        return cartasBaralho;
    }
    public Stack<Cartas> Baralho(List<Cartas> MonteCompras)
    {
        for (int i = 0; i < 10; i++)
        {
            cartasMesa.Push(MonteCompras[i]);
        }
        return cartasMesa;
    }
    public void IniciarPartida(Stack<Cartas> MonteCompras, Jogador jogador, Cartas cartaDaVez, List<Jogador> listaJogadores, List<Cartas> ListaDeCartasAreaDescarte, string caminhoArquivoLogs)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nTOPO DOS MONTES DOS JOGADORES\n");
        using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs, true))
        {
            escritorArquivo.WriteLine($"\n- Topo dos montes do jogadores:");
            Console.ResetColor();
        }
        
        for (int i = 0; i < listaJogadores.Count; i++)
        {
            if (listaJogadores[i].monteJogador.Count == 0)
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"**** O JOGADOR {listaJogadores[i].getNome()} NÃO TEM UM MONTE ****");
                using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs, true))
                {
                    escritorArquivo.WriteLine($"- O jogador [{listaJogadores[i].getNome()}] não tem um monte");
                }
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"**** O JOGADOR {listaJogadores[i].getNome()} TEM SEU MONTE COM TOPO: {listaJogadores[i].monteJogador.Peek().GetNumero()} {listaJogadores[i].monteJogador.Peek().GetNaipe()} ****");
                using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs, true))
                {
                    escritorArquivo.WriteLine($"- O jogador {listaJogadores[i].getNome()} tem seu monte com topo: {listaJogadores[i].monteJogador.Peek().GetNumero()} {listaJogadores[i].monteJogador.Peek().GetNaipe()}");
                }
                Console.ResetColor();
            }
        }
        bool verificaRepeticaoMenu, sair = false;
        while (!sair)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n**** MENU DE OPÇÕES ****\n");
            Console.ResetColor();
            Console.WriteLine("1) Pegar uma carta na área de descarte com carta da vez\n2) Roubar um monte através da carta da vez\n3) Formar monte com a carta da vez\n4) Colocar carta da vez na área de descarte");
            try
            {
                int opcaoDesejada = int.Parse(Console.ReadLine());
                switch (opcaoDesejada)
                {
                    case 1:
                        using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs, true))
                        {
                            escritorArquivo.WriteLine($"- O jogador [{jogador.getNome()}] escolheu a opção 1");
                        }
                        verificaRepeticaoMenu = PegarAreaDescarteComCartaVez(jogador, cartaDaVez, ListaDeCartasAreaDescarte, caminhoArquivoLogs);
                        if (verificaRepeticaoMenu && MonteCompras.Count > 0)
                        {
                            cartaDaVez = MonteCompras.Pop();
                        }
                        ExibeMenu(cartaDaVez, ListaDeCartasAreaDescarte, listaJogadores, jogador, caminhoArquivoLogs);
                        break;
                    case 2:
                        using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs, true))
                        {
                            escritorArquivo.WriteLine($"- O jogador [{jogador.getNome()}] escolheu a opção 2");
                        }
                        verificaRepeticaoMenu = RoubarMonteComCartaDaVez(jogador, cartaDaVez, listaJogadores, caminhoArquivoLogs);
                        if (verificaRepeticaoMenu && MonteCompras.Count > 0)
                        {
                            cartaDaVez = MonteCompras.Pop();
                        }
                        ExibeMenu(cartaDaVez, ListaDeCartasAreaDescarte, listaJogadores, jogador, caminhoArquivoLogs);
                        break;
                    case 3:
                        using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs, true))
                        {
                            escritorArquivo.WriteLine($"- O jogador [{jogador.getNome()}] escolheu a opção 3");
                        }
                        verificaRepeticaoMenu = ColocarCartaDaVezNoProprioMonte(jogador, cartaDaVez, caminhoArquivoLogs);
                        if (verificaRepeticaoMenu && MonteCompras.Count > 0)
                        {
                            cartaDaVez = MonteCompras.Pop();
                        }
                        ExibeMenu(cartaDaVez, ListaDeCartasAreaDescarte, listaJogadores, jogador, caminhoArquivoLogs);
                        break;
                    case 4:
                        using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs, true))
                        {
                            escritorArquivo.WriteLine($"- O jogador [{jogador.getNome()}] escolheu a opção 4");
                        }
                        ColocarCartaAreaDescarte(cartaDaVez, ListaDeCartasAreaDescarte, jogador, caminhoArquivoLogs);
                        sair = true;
                        break;
                    default:
                        using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs, true))
                        {
                            escritorArquivo.WriteLine($"- O jogador [{jogador.getNome()}] digitou uma opção inválida");

                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("**** OPÇÃO INVÁLIDA ****");
                            escritorArquivo.WriteLine($"O jogador [{jogador.getNome()}] digitou uma opção inválida");
                        }
                        Console.ResetColor();
                        break;
                }
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n**** ERRO!! NOSSO MENU FUNCIONA APENAS COM NÚMEROS ****\n");
                Console.ResetColor();
            }
        }
        Stopwatch cronometroLimparConsole = new Stopwatch();
        cronometroLimparConsole.Start();
        while (true)
        {
            TimeSpan tempo = cronometroLimparConsole.Elapsed;
            if (tempo.Seconds == 3)
            {
                cronometroLimparConsole.Stop();
                Console.Clear();
                break;
            }
        }
    }
    static bool PegarAreaDescarteComCartaVez(Jogador jogador, Cartas cartaDaVez, List<Cartas> ListaDeCartasAreaDescarte, string caminhoArquivoLogs)
    {
        for (int i = 0; i < ListaDeCartasAreaDescarte.Count; i++)
        {
            if (ListaDeCartasAreaDescarte[i].GetNumero() == cartaDaVez.GetNumero() && ListaDeCartasAreaDescarte.Count > 0 || cartaDaVez.GetNumero() == 14)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\n**** A CARTA {ListaDeCartasAreaDescarte[i].GetNumero()} {ListaDeCartasAreaDescarte[i].GetNaipe()} DA ÁREA DE DECARTE FOI COLOCADA EM SEU MONTE JUNTO COM A CARTA DA VEZ {cartaDaVez.GetNumero()} {cartaDaVez.GetNaipe()}. ****\n");
                using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs, true))
                {
                    escritorArquivo.WriteLine($"- A carta [{ListaDeCartasAreaDescarte[i].GetNumero()} {ListaDeCartasAreaDescarte[i].GetNaipe()}] da área de descarte foi colocada em seu monte junto com a carta da vez [{cartaDaVez.GetNumero()} {cartaDaVez.GetNaipe()}]. Jogador da jogada: [{jogador.getNome()}].");
                    Console.ResetColor();
                }
                jogador.monteJogador.Push(ListaDeCartasAreaDescarte[i]);
                ListaDeCartasAreaDescarte.RemoveAt(i);
                jogador.monteJogador.Push(cartaDaVez);
                return true;
            }
        }
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n**** NÃO EXISTE CARTA COM O NÚMERO {cartaDaVez.GetNumero()} NA ÀREA DE DESCARTE ****\n");
        using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs, true))
        {
            escritorArquivo.WriteLine($"- Não existe carta com o número [{cartaDaVez.GetNumero()}] na área de descarte. Jogador da jogada: [{jogador.getNome()}].");
        }
        Console.ResetColor();
        return false;
    }
    static bool RoubarMonteComCartaDaVez(Jogador jogador, Cartas cartaDaVez, List<Jogador> listaJogadores, string caminhoArquivoLogs)
    {
        bool existeJogador = false;
        Console.WriteLine("\nDigite o nome do jogador que deseja roubar o monte");
        string nomeJogador = Console.ReadLine().ToUpper();

        //Verifica se o nome digitado é o nome do jogador que esta jogando
        if (nomeJogador == jogador.getNome())
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n**** NÃO É POSSÍVEL ROUBAR SEU PRÓPRIO MONTE ****\n");
            Console.ResetColor();
            using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs, true))
            {
                escritorArquivo.WriteLine($"- O jogador [{jogador.getNome()}] tentou rouar o seu próprio monte.");
            }
            return false;
        }
        //Loop para verificar se existe jogador com o nome digitado
        for (int i = 0; i < listaJogadores.Count; i++)
        {
            if (nomeJogador == listaJogadores[i].getNome())
            {
                existeJogador = true;
            }
        }
        if (existeJogador)
        {
            for (int i = 0; i < listaJogadores.Count; i++)
            {
                if (listaJogadores[i].getNome() == nomeJogador)
                {
                    if (listaJogadores[i].monteJogador.Count == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\n**** O JOGADOR {nomeJogador} NÃO POSSUI MONTE ****\n");
                        using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs, true))
                        {
                            escritorArquivo.WriteLine($"- O jogador [{jogador.getNome()}] tentou roubar seu próprio monte");
                        }
                        Console.ResetColor();
                        return false;
                    }
                    else if (cartaDaVez.GetNumero() == listaJogadores[i].getTopoMonte().GetNumero() || cartaDaVez.GetNumero() == 14)
                    {
                        while (listaJogadores[i].monteJogador.Count > 0)
                        {
                            jogador.monteJogador.Push(listaJogadores[i].monteJogador.Pop());
                        }
                        jogador.monteJogador.Push(cartaDaVez);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\n**** O MONTE DO JOGADOR {nomeJogador} FOI ROUBADO PELO JOGADOR {jogador.getNome()} ****\n");
                        using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs, true))
                        {
                            escritorArquivo.WriteLine($"- O jogador [{jogador.getNome()}] roubou o monte do jogador {nomeJogador}");
                        }
                        Console.ResetColor();
                        return true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\n**** A CARTA DA VEZ  É DIFERENTE DA CARTA DO TOPO DO MONTE DO JOGADOR {nomeJogador} ****\n");
                        using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs, true))
                        {
                            escritorArquivo.WriteLine($"- A carta da vez é diferente da carta do topo do monte do jogador [{nomeJogador}].");
                        }
                        Console.ResetColor();
                        return false;
                    }
                }
            }
        }
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n**** NÃO EXISTE JOGADOR COM ESSSE NOME ****\n");
        Console.ResetColor();
        using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs, true))
        {
            escritorArquivo.WriteLine($"- O jogador [{jogador.getNome()}] tentou roubar um monte de um jogador que não existe");
        }
        return false;
    }
    static bool ColocarCartaDaVezNoProprioMonte(Jogador jogador, Cartas cartaDaVez, string caminhoArquivoLogs)
    {
        if (jogador.monteJogador.Count > 0)
        {
            if (jogador.getTopoMonte().GetNumero() == cartaDaVez.GetNumero())
            {
                jogador.monteJogador.Push(cartaDaVez);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n**** A CARTA DA VEZ {cartaDaVez.GetNumero()} {cartaDaVez.GetNaipe()} FOI COLOCADA NO TOPO DO SEU MONTE ****");
                using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs, true))
                {
                    escritorArquivo.WriteLine($"- O jogador [{jogador.getNome()}] colocou a carta da vez [{cartaDaVez.GetNumero()} {cartaDaVez.GetNaipe()}] no próprio monte");
                    Console.ResetColor();
                }
                return true;
            }
        }
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n**** NÃO É POSSÍVEL COLOCAR A CARTA DA VEZ {cartaDaVez.GetNumero()} {cartaDaVez.GetNaipe()} NO TOPO DO SEU MONTE ****\n");
        using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs, true))
        {
            escritorArquivo.WriteLine($"- O jogador [{jogador.getNome()}] tentou colocar a carta da vez no topo do monte e não foi possível");
        }
        Console.ResetColor();
        return false;
    }
    static bool ColocarCartaAreaDescarte(Cartas cartaDaVez, List<Cartas> ListaDeCartasAreaDescarte, Jogador jogador, string caminhoArquivoLogs)
    {
        ListaDeCartasAreaDescarte.Add(cartaDaVez);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n**** CARTA {cartaDaVez.GetNumero()} {cartaDaVez.GetNaipe()} FOI ADICIONADA NA ÁREA DE DESCARTE ****\n");
        Console.ResetColor();
        using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs, true))
        {
            escritorArquivo.WriteLine($"- A carta da vez {cartaDaVez.GetNumero()} {cartaDaVez.GetNaipe()} foi adicionada na área de descarte pelo jogador [{jogador.getNome()}]");
        }
        return true;
    }
    static void ExibeMenu(Cartas cartaDaVez, List<Cartas> ListaDeCartasAreaDescarte, List<Jogador> listaJogadores, Jogador jogador, string caminhoArquivoLogs)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"**** A CARTA DA VEZ É: {cartaDaVez.GetNumero()} {cartaDaVez.GetNaipe()} ****\n");
        using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs, true))
        {
            escritorArquivo.WriteLine($"- A carta da vez retirada do baralho pelo jogador [{jogador.getNome()}] é: [{cartaDaVez.GetNumero()} {cartaDaVez.GetNaipe()}]");
            escritorArquivo.WriteLine($"Cartas na área de descarte: ");
        }

        Console.ResetColor();
        Console.WriteLine("------------------------------");
        Console.WriteLine("CARTAS DA MESA (ÁREA DE DESCARTE)\n");
        if (ListaDeCartasAreaDescarte.Count > 0)
        {
            for (int i = 0; i < ListaDeCartasAreaDescarte.Count; i++)
            {
                Console.WriteLine($"{ListaDeCartasAreaDescarte[i].GetNumero()} {ListaDeCartasAreaDescarte[i].GetNaipe()}");
                using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs, true))
                {
                    escritorArquivo.Write($"{ListaDeCartasAreaDescarte[i].GetNumero()} {ListaDeCartasAreaDescarte[i].GetNaipe()}");
                }
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\nÁREA DE DESCARTE VAZIA\n");
            using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs, true))
            {
                escritorArquivo.Write($"- A área de descarte está vazia");
            }
            Console.ResetColor();
        }
        Console.WriteLine("\n------------------------------");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nTOPO DOS MONTES DOS JOGADORES\n");
        using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs, true))
        {
            escritorArquivo.WriteLine($"\n- Topo dos montes do jogadores:");
        }
        Console.ResetColor();
        for (int i = 0; i < listaJogadores.Count; i++)
        {
            if (listaJogadores[i].monteJogador.Count == 0)
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"**** O JOGADOR {listaJogadores[i].getNome()} NÃO TEM UM MONTE ****");
                using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs, true))
                {
                    escritorArquivo.WriteLine($"[**** O jogador [{listaJogadores[i].getNome()}] não tem um monte ****]");
                }
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"**** O JOGADOR {listaJogadores[i].getNome()} TEM SEU MONTE COM TOPO: {listaJogadores[i].monteJogador.Peek().GetNumero()} {listaJogadores[i].monteJogador.Peek().GetNaipe()} ****");
                using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs, true))
                {
                    escritorArquivo.WriteLine($"- O jogador {listaJogadores[i].getNome()} tem seu monte com topo: {listaJogadores[i].monteJogador.Peek().GetNumero()} {listaJogadores[i].monteJogador.Peek().GetNaipe()}");
                }
                Console.ResetColor();
            }
        }
    }
}