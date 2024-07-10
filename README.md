<p align="center">  
  <img src="https://github.com/caiodepaulasilva/Spike-Stone/assets/36136627/b3bf510e-d6b0-456b-a556-c3efad278fb5"/>
  <br><br>

  <img src="https://img.shields.io/badge/status-work%20in%20progress-red?style=for-the-badge"/>  
  <img src="https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white"/>  
  <img src="https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white"/>    
</p>


## Introdução

Este trabalho foi desenvolvido como parte de um processo seletivo no qual o objetivo é construir um projeto de desenvolvimento web em que algumas habilidades possam ser exercitadas. Sendo estas:
- Domínio da construção de uma API
- Domínio de capacidade de escrever uma documentação razoável
- Domínio de construção de projeto apoiado em boas práticas
- Domínio de conteinerização com o uso de docker
- Domínio de integração com alguma base de dados
- Domínio de integração com servico de CI e uso de Github Actions
- Domínio da construção de testes unitários
- Domínio do framework .NET 8.0
- Domínio da capacidade analítica, de abstração e de construção de um algoritmo que reflita as boas práticas de programação e o uso razoável da linguagem C# para construção de APIs.


**Anexo**: O exerício proposto consta neste documento: [Desafio Credit III  - API de Contracheque (1).pdf](https://github.com/user-attachments/files/16118082/Desafio.Credit.III.-.API.de.Contracheque.1.pdf)
<br><br>

## Requerimentos

O projeto tem uma construção simples e, portanto, são necessárias apenas algumas poucas dependências para a sua execução. São esperados os seguintes requerimentos:

- Microsoft Visual Studio (versão 2010 ou superior)
- SDK .NET (versão 8.0 ou superior)
- Browser de navegação Web
- SQL Server Managament Studio
- Docker Desktop
- IIS Express

## Utilização:
Para consumir a API publicada, acesse [spike-stone-app](https://spike-stone-app.azurewebsites.net/swagger/index.html) e valide a solução pelo swagger.

## Execução local
Para executar o projeto de maneira local é necessário que algumas configurações sejam feitas. Segue instruções de como realizá-las:

**Clonar o projeto:**
```
cd "diretorio de sua preferencia"
git clone https://github.com/caiodepaulasilva/Spike-Stone.git
```

**Criação dos Containers:**
1. Abra o arquivo docker-compose.yaml e defina uma senha para o servidor, através da variável *"MSSQL_SA_PASSWORD"*
2. Uma vez no Visual Studio, no terminal PowerShell do Desenvolvedor, na raiz do projeto, digite:
```
docker-compose  -f "docker-compose.yml" -p "solution-spike-stone" --ansi never up -d --build --remove-orphans  spike-stone sql-server-db
```
3. Verifique o bom funcionamento através do Docker Desktop. Os containers devem ter sido criados com sucesso e estarão em execução.
4. Verifique se é possível estabelcer conexão com o servidor. Para isso tente realizar a conexão através do SQL Server Managament Studio da seguinte maneira:

| Campo do Formulário de Conexão | Valor                               |
| ------------------------------ | ----------------------------------- |
| Server Type                    | Database Engine                     |
| Server Name                    | localhost,1433                      |
| Authentication                 | SQL Server Authentication           |
| Login                          | sa                                  |
| Password                       | <MSSQL_SA_PASSWORD> (docker-compose.yaml) |
| Trust server certificate       | checked                             |

**Criar a estrutura da base de dados:**
1. Uma vez no Visual Studio, no terminal PowerShell do Desenvolvedor, na raiz do projeto, digite:
```
dotnet clean Spike-Stone
dotnet ef migrations add InitialCreate --project Infrastructure --startup-project Spike-Stone
```
**Acessar a solução:**
<br><br>
Após realizar essas instruções, deve ser possível consumir a solução normalmente. Para isso, acesse: [localhost:5000/swagger/index.html](http://localhost:5000/swagger/index.html)

## Licença

[MIT licensed](./LICENSE).
