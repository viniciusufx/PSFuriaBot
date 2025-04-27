# 🤖 Bot FURIA para Telegram (.NET 9)

Bot desenvolvido em C# (.NET 9) utilizando a API oficial do Telegram.  
Permite criar menus interativos, divulgar novidades, produtos, informações sobre times de e-sports e muito mais.

---

## 📋 Funcionalidades
- Envio automático de imagem de boas-vindas
- Menus interativos para League of Legends e CS:GO
- Últimas notícias, curiosidades e acesso à loja

---

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