# Tower
## Testando a aplicação com o docker ##
Instale o docker.
com o docker instalado na maquina, dentro da pasta Arquivo para Testes, será possivel encontrar dois arquivo o docker-comopose e o SQL com o script para importar na base de dados do banco de dados.
O banco de dados é o MYSQL.
Caso faça o teste utilizando o visual studio será necessario configurar as variaveis do aplicativo.
As principais variaveis de ambiente já estão configurados.
### Segue um exemplo como no docker-compose.
   | Variavel de ambiente | Valor | Descrição |
   |---|---|---|
   | DataBase-User | root | usuário do bando de dados |
   | DataBase-Address | mysqldb |Endereço do banco de dados |
   | DataBase-Database | Tower | Nome do database |
   | DataBase-Password | root |  senha do banco de dados |

## A unit-test cria um banco de dados "Tower-UnitTest" utilizando a classe CleanClass.CS, se for utilizar algum banco de dados para teste que não seja local, as configurações podem ser alteradas nele ##


##### Depois de rodar o docker-compose, restaure a versão do banco de dados que se encontra na pasta.
##### Caso utilize o visual studio, depois de configurar as variaveis de ambiente, o comando do entity-framework (update-database) subira uma versão do database com o nome Tower
#### Ao iniciar o aplicativo, caso o banco de dados esteja vazio, ele irá pedir para criar um usuário.
#### Foi utilizado o swagger para documentar e testar o endpoints da API. o caminho possui o endereço exemplo. Caso suba uma a versao do docker-compose o caminho será: http://localhost:36100/swagger/index.html.
#### Para acessar as funções do swagger é necessario gerar um chave bearer na API, utilize o email e senha cadastrado ao iniciar o aplicativo.
