# Billing

Sistema de Faturamento

## Pré-requisitos Ambiente Dev:

- Instalar o Visual Studio 2022 no mínimo.
- Baixar o projéto no github: https://github.com/elyseomm/billing.git

- Instancia do MySQL(versão 8.0 de preferência).
- (optional) Crie um Database no MySQL com o nome 'billing'. O processo de migration ( update-database ) cria o schema do banco de dados;

- Com o VS Instalado e o Projeto Billing baixado em uma pasta de trabalho:
- Vá para a pasta '..\billing\\billing';
- Abra a Solution 'WebApp.sln';
- Certifique-se de que todos os pacotes e dependencias foram instaladas corretamente;
- Depois acesse a opção de menu 'Tools', 'NuGet Package Manager' e em seguida 'Package Manager Console';
- Vamos rodar o 'Migration' para inicializar o Banco de Dados 'billing'.

- No prompt de comando do 'Package Manager Console' na guia 'Default Project' selecione o projeto 'Billing.Core' na caixa de seleção;

- No prompt de comando do 'Package Manager Console' digite 'add-migrations SetupInicial':

  Passo 1 ( Opcional - Como já vai um arquivo de migration pronto no projeto( 20250702152848_SetupInicial.cs ), este passo não é requerido ):

  - PM> add-migrations SetupInicial + <ENTER>, aguarde o término do processo;

    - deverá surgir a pasta 'Migrations' com dois aquivos no projetoi Billing.Core:

      - YYYYMMDDHRMMSS_SetupInicial.cs

  - Migration - Depois digite 'update-database' + <ENTER> no Package Management Console;

  Passo 2 ( Requerido - Execute para criar o database schema) =>

  - PM> update-database, aguarde o término do processo;

- Neste momento, após rodar o Migration, as tabelas Products e Customers deverão ter sido criada no Banco de Dados 'billing', já com alguns dados cadastrados;



- GIT BANCHS


- Temos 2 Branches 'master' e 'development':
        IMPORTANTE!!! O código mais atual está na Branch 'development' portanto recomendamos usarem a branch 'development'. 
---

- RODANDO A APLICAÇÂO

---

- A Aplicação se divide em duas partes(Back-End / Front-End), para que tudo funcione precisamos rodas as duas separadamente:

## BACK-END:

- Para rodar o Back-End em Dev marque o projeto 'Billing.Api' como 'Projeto Inicial' = 'StartUp Project':

  - para isso clique com o botão direito no projeto 'Billing.Api' e escolha a opção 'Set as StartUp Project' e estamos prontos pra rodar.
  - F5 e pãhhh.

  - Em minha máquina o endereço do back-end foi: https://localhost:44306

  - Para a listagem do Swagger: https://localhost:44306/swagger/index.html

  - Configure a ConnectionString para conectar no banco MySQL em Billing.Api-> appsettings.json:
       Ex: 
           "ConnectionStrings": {
                "Billing": "Server=localhost; Database=billing; UID=root; Password=root;SSLMODE=None;"
           }

-(Se desejar instalar numa instância do IIS é só publicar o projeto 'Billing.Api' numa instância do IIS na rede).

## FRONT-END:

- Para rodar o Front-End em Dev precisamos abrir uma nova instancia do Visual Studio( Caso o back-end já esteja rodando noutro VS local)
- Vá para a pasta '..\Billing\WebApp' e abra a Solution 'WebApp.sln' novamente;

E então marque o projeto 'WebApp' como 'Projeto Inicial' = 'StartUp Project': - para isso clique com o botão direito no projeto 'WebApp' e escolha a opção 'Set as StartUp Project' e estamos prontos pra rodar.

IMPORTANTE: Precisamos configurar o FRONT-END com o endereço(URL) da 'Billing.Api' no arquivo 'appsettings.json' dentro do path wwwroot:

    Ex: https://localhost:44306

    - Para isto altere o arquivo 'appsettings.json' dentro do path wwwroot na chave(Key) 'restApi', em valor coloque o endereço do back-end:
    Ex:
        {
            "restApi": "https://localhost:44306"
        }

    Pronto o front já sabe onde consultar os dados na API.

- F5 e agora podemos rodar o sistema.

- Em minha máquina o endereço do front-end foi: https://localhost:44305/ na outra porta '44305'.
