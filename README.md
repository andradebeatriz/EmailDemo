<img width="100%" src="https://capsule-render.vercel.app/api?type=waving&height=100&color=ff79c6"/>

# EmailDemo ۶ৎ 

Aplicação de exemplo para envio de e-mails usando ASP.NET Core, MailKit/MimeKit e Mailtrap. O projeto foi desenvolvido como prática de integração com SMTP: a interface envia os dados para uma API, e o serviço monta e encaminha a mensagem ao servidor configurado.

## Funcionalidades

- Página web simples para informar destinatário, assunto e mensagem.
- Endpoint HTTP `POST /api/email` que recebe os dados da mensagem em JSON.
- Montagem de e-mail HTML e envio assíncrono usando MailKit/MimeKit.
- Configuração do servidor SMTP por `EmailSettings`.
- Interface para o ASP.NET Core em `wwwroot/` e arquivos estáticos na raiz do repositório para GitHub Pages.

## Tecnologias

- .NET 9 / ASP.NET Core Web API
- C#
- MailKit e MimeKit
- Mailtrap (caixa de entrada SMTP para desenvolvimento e testes)
- HTML e CSS

## Pré-requisitos

- .NET 9 SDK
- Conta e caixa de entrada de teste do Mailtrap, com credenciais SMTP

## Configuração

As opções de SMTP estão definidas na seção `EmailSettings` de `appsettings.json`. O host e a porta indicados são os valores padrão do Mailtrap neste projeto. Configure usuário e senha localmente com User Secrets, para não gravar credenciais no repositório:

```powershell
dotnet user-secrets set "EmailSettings:Username" "SEU_USUARIO_MAILTRAP"
dotnet user-secrets set "EmailSettings:Password" "SUA_SENHA_MAILTRAP"
```

Use as credenciais SMTP fornecidas pela sua caixa de entrada do Mailtrap. Não coloque senhas reais em `appsettings.json` nem faça commit delas.

## Executar

No diretório do projeto, execute:

```powershell
dotnet restore
dotnet run --launch-profile http
```

Abra [http://localhost:5021](http://localhost:5021) no navegador. O perfil `http` está configurado em `Properties/launchSettings.json`.

## Enviar uma mensagem pela API

O endpoint recebe `para`, `assunto` e `corpo` como strings. Exemplo em PowerShell:

```powershell
$body = @{
    para = "destino@exemplo.com"
    assunto = "Mensagem de teste"
    corpo = "<h1>Olá!</h1><p>Mensagem enviada pelo EmailDemo.</p>"
} | ConvertTo-Json -Compress

Invoke-RestMethod -Method Post `
  -Uri "http://localhost:5021/api/email" `
  -ContentType "application/json; charset=utf-8" `
  -Body ([System.Text.Encoding]::UTF8.GetBytes($body))
```

Quando o envio for aceito, a API responde `E-mail enviado!`. Com Mailtrap, confira a mensagem na caixa de entrada de teste configurada. Credenciais ausentes/incorretas ou problemas de conexão SMTP impedem o envio.

## Organização do código

| Arquivo | Responsabilidade |
| --- | --- |
| `Program.cs` | Registra controllers, opções de SMTP e serviço de e-mail; configura arquivos estáticos. |
| `Controllers/EmailController.cs` | Expõe `POST /api/email`. |
| `EmailDto.cs` | Define os campos da requisição: destinatário, assunto e corpo. |
| `IEmailService.cs` | Define o contrato do serviço de envio. |
| `EmailService.cs` | Cria a mensagem MIME, conecta ao SMTP via STARTTLS, autentica e envia. |
| `EmailSettings.cs` | Representa as configurações do servidor SMTP. |
| `appsettings.json` | Guarda as configurações não secretas do projeto. |
| `wwwroot/` | Contém a interface web, estilos e favicon. |
| `index.html`, `css/`, `favicon.svg` | Cópia estática da interface na raiz para publicação pelo GitHub Pages. |
| `docs/` | Cópia estática alternativa da interface, caso o Pages seja configurado para publicar essa pasta. |

## Segurança

- Mantenha credenciais SMTP fora do código e do controle de versão.
- Use User Secrets durante o desenvolvimento e um gerenciador de segredos ou variáveis de ambiente em produção.
- Use TLS para a conexão SMTP; este projeto configura STARTTLS na porta 587.
- Mailtrap é apropriado para testes: as mensagens ficam na caixa de entrada de teste, sem serem entregues ao destinatário real.

## Observação

O envio depende de uma configuração SMTP válida e de acesso à rede. Este projeto é uma demonstração didática; antes de usá-lo em produção, acrescente validação e tratamento de erros apropriados à aplicação.

## ۶ৎ Desenvolvedora

Desenvolvido por Beatriz de Andrade Leite, estudante do curso técnico em Desenvolvimento de Sistemas.

- GitHub: [@andradebeatriz](https://github.com/andradebeatriz)
- LinkedIn: [beatrizdeandradeleite](https://linkedin.com/in/beatrizdeandradeleite)

*Este projeto é livre para fins acadêmicos.*

<img width=100% src="https://capsule-render.vercel.app/api?type=waving&color=ff79c6&height=100&section=footer"/>
