// Importação de bibliotecas necessárias
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

// Inicializa o bot com o token de acesso
var botClient = new TelegramBotClient("SEU_TOKEN_AQUI");

// Cria um token de cancelamento para interromper o bot manualmente
using var cts = new CancellationTokenSource();

// Define quais tipos de atualizações (mensagens, cliques, etc) o bot vai escutar
var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = Array.Empty<UpdateType>()  // Escuta tudo
};

// Inicia o processo de "escuta" de mensagens
botClient.StartReceiving(
    HandleUpdateAsync,
    HandleErrorAsync,
    receiverOptions,
    cancellationToken: cts.Token
);

// Mensagem no console para indicar que o bot está rodando
Console.WriteLine("Bot FURIA está rodando... Pressione qualquer tecla para sair.");
Console.ReadKey();

// Para o bot quando o usuário pressionar uma tecla
cts.Cancel();

// Função chamada sempre que chega uma atualização nova (mensagem, clique, etc)
async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
{
    // LOG: Se veio uma mensagem
    if (update.Message?.From != null)
    {
        var id = update.Message.From.Id;
        var user = update.Message.From.Username;
        var nome = $"{update.Message.From.FirstName} {update.Message.From.LastName}";
        var text = update.Message.Text;
        Console.WriteLine($"📩 Mensagem de: @{user} | Nome: {nome} | ID: {id} | Message: {text}");
    }
    // LOG: Se foi clique em botão
    else if (update.CallbackQuery?.From != null)
    {
        var id = update.CallbackQuery.From.Id;
        var user = update.CallbackQuery.From.Username;
        var nome = $"{update.CallbackQuery.From.FirstName} {update.CallbackQuery.From.LastName}";
        var text = update.CallbackQuery.Data;
        Console.WriteLine($"🖱️ Clique de: @{user} | Nome: {nome} | ID: {id} | Message: {text}");
    }
    // Trata mensagens de texto recebidas
    if (update.Message is { } message && message.Text is { } messageText)
    {
        var chatId = message.Chat.Id;
        // Apaga mensagens anteriores (deixa o chat "limpo")
        var currentMessageId = message.MessageId - 1;
        while (currentMessageId > 0)
        {
            try
            {
                await bot.DeleteMessage(chatId, currentMessageId, cancellationToken);
            }
            catch
            {
                break; // Se não conseguir apagar, para
            }
            currentMessageId--;
        }

        // Se o comando foi /start
        if (messageText.ToLower() == "/start")
        {
            try
            {
                await bot.SendPhoto(chatId, "http://static.lolesports.com/teams/FURIA---black.png", cancellationToken: cancellationToken);     
            }
            catch (System.Exception)
            {
                // Ignora erro de imagem
            }
            await ShowMainMenu(bot, chatId, cancellationToken);
            return;
        }
    }
    // Trata interações com botões (callback)
    else if (update.CallbackQuery is { } callback)
    {
        var data = callback.Data;
        var chatId = callback.Message!.Chat.Id;

        // Switch case para diferentes opções de botão
        switch (data)
        {
            case "curiosidade":
                await bot.SendMessage(chatId, "📣 A FURIA foi o primeiro time BR com centro de treino nos EUA!", cancellationToken: cancellationToken);
                break;
            case "noticias":
                await bot.SendMessage(chatId, "📰 Notícias:\n- FURIA anuncia novo patrocinador\n- FalleN brilha em estreia", cancellationToken: cancellationToken);
                break;
            case "competitivo":
                await ShowCompetitiveMenu(bot, chatId, cancellationToken);
                break;
            case "lol":
                await ShowLoLMenu(bot, chatId, cancellationToken);
                break;
            case "cs":
                await ShowCSMenu(bot, chatId, cancellationToken);
                break;
            case "lol_lineup":
                await ShowLoLLineup(bot, chatId, cancellationToken);
                break;
            case "lol_lineup_guigo":
                await ShowLoLPlayer(bot, chatId, cancellationToken, "Guigo", "Guilherme Araújo Ruiz", "http://static.lolesports.com/players/1737719411230_image67.png", "Top Laner", ["Sett", "Camille", "Gwen", "Volibear"]);
                break;
            case "lol_lineup_tatu":
                await ShowLoLPlayer(bot, chatId, cancellationToken, "Tatu", "Pedro Seixas", "http://static.lolesports.com/players/1737720151865_image69.png", "Jungler", []);
                break;
            case "lol_lineup_tutsz":
                await ShowLoLPlayer(bot, chatId, cancellationToken, "Tutsz", "Arthur Peixoto Machado", "http://static.lolesports.com/players/1737720319734_image610.png", "Mid Laner", ["Zed", "Kassadin"]);
                break;
            case "lol_lineup_ayu":
                await ShowLoLPlayer(bot, chatId, cancellationToken, "Ayu", "Andrey Saraiva", "http://static.lolesports.com/players/1737718665203_image65.png", "Bot Laner", ["Thresh", "Alistar", "Nautilus", "Rakan", "Bel'Veth"]);
                break;
            case "lol_lineup_jojo":
                await ShowLoLPlayer(bot, chatId, cancellationToken, "Jojo", "Gabriel Dzelme de Oliveira", "http://static.lolesports.com/players/1737719808158_image68.png", "Support", ["Janna", "Rakan", "Pyke"]);
                break;
            case "lol_proximo":
                await bot.SendMessage(chatId, "📅 Próximos jogos:\nFURIA vs RED | 03/05 às 15h\nFURIA vs FXW7 | 11/05 às 12h", cancellationToken: cancellationToken);
                break;
            case "lol_ultimos":
                await bot.SendMessage(chatId, "⚔️ Últimos jogos:\nFURIA 0 - 1 PNG | 12/04\nFURIA 1 - 0 FX7M | 13/04\nFURIA 1 - 0 RED | 19/04\nFURIA 1 - 0 VKS |20/04\nFURIA 0 - 1 LOUD | 21/04\nFURIA 0 - 0 LOUD | 27/04", cancellationToken: cancellationToken);
                break;
            case "cs_lineup":
                await ShowCSLineup(bot, chatId, cancellationToken);
                break;
            case "cs_lineup_yuurih":
                await ShowCSPlayer(bot, chatId, cancellationToken, "yuurih", "Yuri Gomes dos Santos Boian", "https://liquipedia.net/commons/images/thumb/a/a6/Yuurih_at_PGL_Bucharest_2025.jpg/600px-Yuurih_at_PGL_Bucharest_2025.jpg", "Logitech G PRO X SUPERLIGHT (White)", 1000, 400, "1000 Hz", 2.5, 1, true, "HyperX FURY S", "ZOWIE XL2546", "240 Hz", "1024x768", "Stretched", "HyperX Alloy Origins", "HyperX Cloud Orbit", 4, 1, 0, 1, -3, false, true, 1, "Light blue", 4, 250, "775091", "yuurihfps", "yuurih", "yuurih", "yuurihfps", "", "", "76561198164970560", "yuurih", "yuurih", "", "");
                break;
            case "cs_lineup_kscerato":
                await ShowCSPlayer(bot, chatId, cancellationToken, "KSCERATO", "Kaike Silva Cerato", "https://liquipedia.net/commons/images/thumb/9/91/KSCERATO_at_PGL_Bucharest_2025.jpg/600px-KSCERATO_at_PGL_Bucharest_2025.jpg", "VAXEE OUTSET AX", 1069.6, 400, "1000 Hz", 2.674, 1, true, "HyperX FURY S Pro (Large)", "ZOWIE XL2546K", "240 Hz", "1280x960", "Black Bars", "HyperX Alloy Origins", "HyperX Cloud Orbit", 4, 1, 0, 1, -3, false, true, 1, "Light blue", 4, 255, "546689", "kscerato1337", "KSCERATO", "kscerato", "kscerato", "", "", "76561198058500492", "kscerato", "kscerato", "", "");
                break;
            case "cs_lineup_fallen":
                await ShowCSPlayer(bot, chatId, cancellationToken, "FalleN", "Gabriel Toledo de Alcântara Sguario", "https://liquipedia.net/commons/images/thumb/e/ea/FalleN_at_PGL_Bucharest_2025.jpg/600px-FalleN_at_PGL_Bucharest_2025.jpg", "ZOWIE EC1-A", 880, 400, "1000 Hz", 2.2, 1.2, true, "", "Alienware AW2518HF", "240 Hz", "1024x768", "Stretched", "", "GFallen Morcego (Purple)", 4, 2.9, 0.8, 1, -2, false, false, 0, "Green", 1, 255, "370566", "FalleNcs", "gabrielfln", "FalleN", "fallen", "GAFALLEN", "", "76561197960690195", "gafallen", "FalleNCS", "Fallen", "");
                break;
            case "cs_lineup_molodoy":
                await ShowCSPlayer(bot, chatId, cancellationToken, "molodoy", "Данил Голубенко", "https://liquipedia.net/commons/images/thumb/4/40/AMKAL_molodoy_March_2025.png/600px-AMKAL_molodoy_March_2025.png", "", 0, 0, "", 0, 0, false, "", "", "", "", "", "", "", 0, 0, 0, 0, 0, false, false, 0, "", 0, 0, "", "", "", "", "", "", "", "76561198200982290", "molodoy1818", "tvoy_molodoy", "", "");
                break;
            case "cs_lineup_yekindar":
                await ShowCSPlayer(bot, chatId, cancellationToken, "YEKINDAR", "Mareks Gaļinskis", "https://liquipedia.net/commons/images/thumb/1/13/YEKINDAR_at_Copenhagen_Major_2024_AME_RMR.jpg/600px-YEKINDAR_at_Copenhagen_Major_2024_AME_RMR.jpg", "ZOWIE EC2-A", 960, 800, "1000 Hz", 1.2, 1, true, "SteelSeries QcK+", "ZOWIE XL2546", "240 Hz", "1920x1080", "", "HyperX Alloy FPS RGB", "HyperX Cloud II", 4, 2, 0, 1, -3, false, false, 0, "Green", 1, 255, "867533", "mareks.galinskis", "YEKINDAR", "", "yek1ndar", "", "", "76561198134401925", "yekindar", "yek1ndar", "", "yekindararmy");
                break;
            case "cs_proximo":
                await bot.SendMessage(chatId, "📅 Próximo jogo CS: \nNo upcoming matches for FURIA, check back later.", cancellationToken: cancellationToken);
                break;
            case "cs_ultimos":
                await bot.SendMessage(chatId, "⚔️ Últimos jogos CS: PGL Bucharest 2025\nFURIA 0 - 2 The MongolZ\nFURIA 0 - 2 Virtus.pro\nFURIA 1 - 2 Complexity\nFURIA 2 - 0 Betclic", cancellationToken: cancellationToken);
                break;
            case "inicio":
                await ShowMainMenu(bot, chatId, cancellationToken);
                break;
            default:
                await bot.SendMessage(chatId, "❓ Comando não reconhecido.", cancellationToken: cancellationToken);
                break;
        }
        // Responde para o Telegram que o botão foi tratado
        await bot.AnswerCallbackQuery(callback.Id, cancellationToken: cancellationToken);
    }
}

// Mostra o menu principal
async Task ShowMainMenu(ITelegramBotClient bot, long chatId, CancellationToken cancellationToken)
{
    var keyboard = new InlineKeyboardMarkup(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("📣 Curiosidade", "curiosidade"),
            InlineKeyboardButton.WithCallbackData("📰 Notícias", "noticias")
        },
        new[]
        {
            InlineKeyboardButton.WithUrl("🛍️ Loja", "https://www.furia.gg/"),
            InlineKeyboardButton.WithCallbackData("🎮 Competitivo", "competitivo")
        }
    });

    await bot.SendMessage(chatId, "Bem-vindo ao FURIA Bot!\nEscolha uma opção abaixo:", replyMarkup: keyboard, cancellationToken: cancellationToken);
}

// Menu de modalidades competitivas
async Task ShowCompetitiveMenu(ITelegramBotClient bot, long chatId, CancellationToken cancellationToken)
{
    var compKeyboard = new InlineKeyboardMarkup(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("🧠 League of Legends", "lol"),
            InlineKeyboardButton.WithCallbackData("🎯 CS:GO", "cs")
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData("⬅️ Voltar ao início", "inicio")
        }
    });
    await bot.SendMessage(chatId, "🎮 Selecione a modalidade:", replyMarkup: compKeyboard, cancellationToken: cancellationToken);
}

// Menu do time de LoL
async Task ShowLoLMenu(ITelegramBotClient bot, long chatId, CancellationToken cancellationToken)
{
    var lolKeyboard = new InlineKeyboardMarkup(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("👥 Lineup", "lol_lineup")
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData("⚔️ Últimos jogos", "lol_ultimos"),
            InlineKeyboardButton.WithCallbackData("📅 Próximo jogo", "lol_proximo")
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData("⬅️ Voltar", "competitivo"),
            InlineKeyboardButton.WithCallbackData("🏠 Voltar ao início", "inicio")
        }
    });
    await bot.SendMessage(chatId, "🧠 League of Legends - Escolha uma opção:", replyMarkup: lolKeyboard, cancellationToken: cancellationToken);
}


async Task ShowLoLLineup(ITelegramBotClient bot, long chatId, CancellationToken cancellationToken){
    var lolKeyboard = new InlineKeyboardMarkup(new []
    {
        new []
        {
            InlineKeyboardButton.WithCallbackData("Guigo (Top Laner)", "lol_lineup_guigo"),
            InlineKeyboardButton.WithCallbackData("Tatu (Jungler)", "lol_lineup_tatu")
        },new []
        {
            InlineKeyboardButton.WithCallbackData("Tutsz (Mid Laner)", "lol_lineup_tutsz"),
            InlineKeyboardButton.WithCallbackData("Ayu (Bot Laner)", "lol_lineup_ayu")
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData("JoJo (Support)", "lol_lineup_jojo")
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData("⬅️ Voltar", "lol"),
            InlineKeyboardButton.WithCallbackData("🏠 Voltar ao início", "inicio")
        }
    });
    await bot.SendMessage(chatId, "🧠 League of Legends - Escolha um player:", replyMarkup: lolKeyboard, cancellationToken: cancellationToken);
}

async Task ShowLoLPlayer(ITelegramBotClient bot, long chatId, CancellationToken cancellationToken, string username, string nome, string imgUrl, string lane, string[] favoriteChamps){
    try
    {
        await bot.SendPhoto(chatId, imgUrl, cancellationToken: cancellationToken);
    }
    catch
    {
    }
    await bot.SendMessage(chatId, $"{nome} - {username} - {lane}", cancellationToken: cancellationToken);
    if (favoriteChamps.Length > 0)
    {
        await bot.SendMessage(chatId, $"Campeões Favoritos: {string.Join(", ", favoriteChamps)}", cancellationToken: cancellationToken);
    }
    var lolKeyboard = new InlineKeyboardMarkup(new []
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("⬅️ Voltar", "lol_lineup"),
            InlineKeyboardButton.WithCallbackData("🏠 Voltar ao início", "inicio")
        }
    });
    await bot.SendMessage(chatId, "⏬ Continue navegando:", replyMarkup: lolKeyboard, cancellationToken: cancellationToken);
}

// Menu do time de CS
async Task ShowCSMenu(ITelegramBotClient bot, long chatId, CancellationToken cancellationToken)
{
    var csKeyboard = new InlineKeyboardMarkup(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("👥 Lineup", "cs_lineup")
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData("⚔️ Últimos jogos", "cs_ultimos"),
            InlineKeyboardButton.WithCallbackData("📅 Próximo jogo", "cs_proximo")
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData("⬅️ Voltar", "competitivo"),
            InlineKeyboardButton.WithCallbackData("🏠 Voltar ao início", "inicio")
        }
    });
    await bot.SendMessage(chatId, "🎯 CS:GO - Escolha uma opção:", replyMarkup: csKeyboard, cancellationToken: cancellationToken);
}

async Task ShowCSLineup(ITelegramBotClient bot, long chatId, CancellationToken cancellationToken){
    var lolKeyboard = new InlineKeyboardMarkup(new []
    {
        new []
        {
            InlineKeyboardButton.WithCallbackData("yuurih", "cs_lineup_yuurih"),
            InlineKeyboardButton.WithCallbackData("KSCERATO", "cs_lineup_kscerato")
        },new []
        {
            InlineKeyboardButton.WithCallbackData("FalleN", "cs_lineup_fallen"),
            InlineKeyboardButton.WithCallbackData("molodoy", "cs_lineup_molodoy")
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData("YEKINDAR", "cs_lineup_yekindar")
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData("⬅️ Voltar", "cs"),
            InlineKeyboardButton.WithCallbackData("🏠 Voltar ao início", "inicio")
        }
    });
    await bot.SendMessage(chatId, "🎯 CS:GO - Escolha um player:", replyMarkup: lolKeyboard, cancellationToken: cancellationToken);
}

async Task ShowCSPlayer(ITelegramBotClient bot, long chatId, CancellationToken cancellationToken, string username, string nome, string imgUrl, string mouse, double edpi, int dpi, string pollingRate, double sensitivity, double zoom, bool rawInput, string mousepad, string monitor, string refreshRate, string resolutionIG, string scaling, string keyboard, string headset, int crosStyle, double crosSize, double crosThickness, int crosSniper, int crosGap, bool crosOutline, bool crosDot, int crosDotValue, string crosColor, int crosColorValue, int crosAlpha, string esea, string facebook, string faceit, string gamersClub, string instagram, string reddit, string site, string steam, string twitch, string twitter, string youtube, string vk){
    try
    {
        await bot.SendPhoto(chatId, imgUrl, cancellationToken: cancellationToken);
    }
    catch
    {
    }
    await bot.SendMessage(chatId, $"{nome} - {username}", cancellationToken: cancellationToken);

    if (!string.IsNullOrEmpty(mouse))
    {
        await bot.SendMessage(chatId, $"Mouse: {mouse}\neDPI: {edpi}\nDPI: {dpi}\nPolling Rate: {pollingRate}\nSensitivity: {sensitivity}\nZoom: {zoom}\nRaw Input: {(rawInput ? "On": "Off")}", cancellationToken: cancellationToken);
    }

    if (!string.IsNullOrEmpty(monitor))
    {
        await bot.SendMessage(chatId, $"Monitor: {monitor}\nRefresh Rate: {refreshRate}\nResolution in Game: {resolutionIG}{(string.IsNullOrEmpty(scaling) ? "" : $"\nScaling: {scaling}")}", cancellationToken: cancellationToken);
    }

    if (!string.IsNullOrEmpty(keyboard) || !string.IsNullOrEmpty(headset))
    {
        if (!string.IsNullOrEmpty(keyboard) && !string.IsNullOrEmpty(headset))
        {
            await bot.SendMessage(chatId, $"Keyboard: {keyboard}\nHeadset: {headset}", cancellationToken: cancellationToken);
        }else if(!string.IsNullOrEmpty(keyboard))
        {
            await bot.SendMessage(chatId, $"Keyboard: {keyboard}", cancellationToken: cancellationToken);
        }else {
            await bot.SendMessage(chatId, $"Headset: {headset}", cancellationToken: cancellationToken);
        }
    }

    if (!string.IsNullOrEmpty(crosColor))
    {
        await bot.SendMessage(chatId, $"Crosshair Settings\nStyle: {crosStyle}\nSize: {crosSize}\nThickness: {crosThickness}\nSniper: {crosSniper}\nGap: {crosGap}\nOutline: {(crosOutline ? "Yes":"No")}\nDot: {(crosDot ? "Yes":"No")} ({crosDotValue})\nColor: {crosColor} ({crosColorValue})\nAlpha: {crosAlpha}", cancellationToken: cancellationToken);
    }

    var socialButtons = new List<InlineKeyboardButton>();
    if (!string.IsNullOrEmpty(esea)){
        socialButtons.Add(InlineKeyboardButton.WithUrl("ESEA", "https://play.esea.net/users/" + esea));
    }
    if (!string.IsNullOrEmpty(facebook)){
        socialButtons.Add(InlineKeyboardButton.WithUrl("Facebook", "https://www.facebook.com/" + facebook));
    }
    if (!string.IsNullOrEmpty(faceit)){
        socialButtons.Add(InlineKeyboardButton.WithUrl("FACEIT", "https://www.faceit.com/en/players/" + faceit));
    }
    if (!string.IsNullOrEmpty(gamersClub)){
        socialButtons.Add(InlineKeyboardButton.WithUrl("Gamers Club", "https://gamersclub.com.br/player/" + gamersClub));
    }
    if (!string.IsNullOrEmpty(instagram)){
        socialButtons.Add(InlineKeyboardButton.WithUrl("Instagram", "https://www.instagram.com/" + instagram));
    }
    if (!string.IsNullOrEmpty(reddit)){
        socialButtons.Add(InlineKeyboardButton.WithUrl("Reddit", "https://www.reddit.com/user/" + reddit));
    }
    if (!string.IsNullOrEmpty(site)){
        socialButtons.Add(InlineKeyboardButton.WithUrl("Site", site));
    }
    if (!string.IsNullOrEmpty(steam)){
        socialButtons.Add(InlineKeyboardButton.WithUrl("Steam", "https://steamcommunity.com/profiles/" + steam));
    }
    if (!string.IsNullOrEmpty(twitch)){
        socialButtons.Add(InlineKeyboardButton.WithUrl("Twitch", "https://www.twitch.tv/" + twitch));
    }
    if (!string.IsNullOrEmpty(twitter)){
        socialButtons.Add(InlineKeyboardButton.WithUrl("Twitter", "https://x.com/" + twitter));
    }
    if (!string.IsNullOrEmpty(vk))
    {
        socialButtons.Add(InlineKeyboardButton.WithUrl("VK", "https://vk.com/" + vk));
    }
    if (!string.IsNullOrEmpty(youtube)){
        socialButtons.Add(InlineKeyboardButton.WithUrl("YouTube", "https://www.youtube.com/c/" + youtube));
    }

    var socialRows = new List<InlineKeyboardButton[]>();
    for (int i = 0; i < socialButtons.Count; i += 3)
    {
        var slice = socialButtons.Skip(i).Take(3).ToArray();
        socialRows.Add(slice);
    }

    var lolSocialKeyboard = new InlineKeyboardMarkup(socialRows);
    await bot.SendMessage(chatId, "Redes Sociais", replyMarkup: lolSocialKeyboard, cancellationToken: cancellationToken);
    
    var lolBackKeyboard = new InlineKeyboardMarkup(new []
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("⬅️ Voltar", "cs_lineup"),
            InlineKeyboardButton.WithCallbackData("🏠 Voltar ao início", "inicio")
        }
    });
    await bot.SendMessage(chatId, "⏬ Continue navegando:", replyMarkup: lolBackKeyboard, cancellationToken: cancellationToken);
}

// Função chamada se ocorrer erro
Task HandleErrorAsync(ITelegramBotClient bot, Exception exception, CancellationToken cancellationToken)
{
    Console.WriteLine($"Erro: {exception.Message}");
    return Task.CompletedTask;
}
