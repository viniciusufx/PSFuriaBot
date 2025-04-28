# 🤖 PSFuriaBot - Telegram Bot

Este é um bot desenvolvido em C# (.NET 9) utilizando a API oficial do Telegram criado para fornecer informações sobre o time FURIA, com foco em modalidades competitivas como League of Legends (LoL) e Counter-Strike: Global Offensive (CS:GO), incluindo informações sobre o lineup dos jogadores, próximos e últimos jogos, e curiosidades sobre a organização.

## Funcionalidades
O bot oferece várias funcionalidades organizadas em menus interativos:

- **Curiosidades**: Receba informações interessantes sobre a FURIA.

- **Notícias**: Fique atualizado com as últimas notícias da organização.

- **Competitivo**: Escolha a modalidade competitiva desejada (LoL ou CS:GO).

- **Lineup**: Visualize informações sobre os jogadores da FURIA, incluindo nome, imagem e campeões favoritos (para LoL).

- **Últimos Jogos**: Veja os resultados dos jogos anteriores de cada modalidade.

- **Próximos Jogos**: Consulte os próximos jogos agendados para o time.

## Como usar

1. *Start*: Ao iniciar o bot com o comando /start, o bot envia uma mensagem de boas-vindas e exibe o menu principal.

2. **Menu Principal**: O menu permite ao usuário escolher entre:

    - Curiosidades

    - Notícias

    - Loja da FURIA

    - Competitivo

3. Competitivo: No menu competitivo, você pode escolher entre League of Legends (LoL) ou Counter-Strike (CS:GO).

4. Lineup dos Jogadores: Ao selecionar uma modalidade, o bot oferece detalhes sobre o time e jogadores, com a opção de visualizar informações individuais de cada jogador, como sua posição e campeões favoritos para LoL, ou configurações de hardware para CS:GO.

5. Últimos Jogos e Próximos Jogos: Veja os resultados anteriores e os próximos confrontos do time.

## Tecnologias usadas
- Telegram.Bot: Biblioteca oficial para interagir com a API do Telegram.

- C#: Linguagem de programação usada para o desenvolvimento do bot.

- Async/Await: Implementação assíncrona para otimizar a performance e interação com a API do Telegram.

## Comandos principais
- /start: Inicia a interação com o bot.

- Menu Principal: Apresenta as opções de curiosidades, notícias e loja.

- Modalidade Competitiva: Exibe opções de League of Legends ou CS:GO.

- Lineup de Jogadores: Mostra o time de jogadores da FURIA e suas informações.

- Últimos Jogos: Exibe os resultados mais recentes dos jogos.

- Próximos Jogos: Mostra os próximos jogos agendados.

## Estrutura de Respostas

O bot usa InlineKeyboardMarkup para criar botões interativos que permitem ao usuário navegar pelos menus e interagir com os conteúdos. O bot responde com mensagens de texto e imagens quando necessário.

## Exemplos de Interações

- Curiosidade: "📣 A FURIA foi o primeiro time BR com centro de treino nos EUA!"

- Notícias: "📰 Notícias:\n- FURIA anuncia novo patrocinador\n- FalleN brilha em estreia"

- Jogador de LoL: Mostra o nome, foto e campeões favoritos de cada jogador.

- Jogador de CS: Exibe informações detalhadas sobre o equipamento usado por cada jogador (mouse, teclado, monitor, etc.).

## 🛠️ Tecnologias usadas
- **C#**
- **.NET 9**
- **Telegram.Bot (v22.5.1)**

---

# 🚀 Instalação e Execução - Passo a Passo

## 1. Instalar o Git

Se ainda não tiver o Git instalado:

- [Baixar Git para Windows, Linux e macOS](https://git-scm.com/downloads)

Depois confirme se o Git foi instalado:

```bash
git --version
```

---

## 2. Instalar o .NET 9
Baixe e instale o SDK .NET 9:

- [Download oficial do .NET 9](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)

Confirme a instalação:

```bash
dotnet --version
```

---

## 3. Clonar o Repositório

Clone o projeto na sua máquina:

```bash
git clone https://github.com/viniciusufx/PSFuriaBot.git
cd PSFuriaBot
```

---

## 4. Restaurar as Dependências

Execute o comando abaixo dentro da pasta do projeto:

```bash
dotnet restore
```

Isso irá baixar e instalar todas as bibliotecas necessárias.

---

## 5. Configurar o Token do Bot

No arquivo Program.cs, localize a linha:

```csharp
var botClient = new TelegramBotClient("SEU_TOKEN_AQUI");
```

Substitua "SEU_TOKEN_AQUI" pelo token que você criou no BotFather.

⚠️ Atenção: Nunca compartilhe seu token em lugares públicos!

---

## 6. Executar o Bot
Execute o bot com:
```bash
dotnet run
```