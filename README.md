# Tower
## Testando a aplicação com o docker ##
Instale o docker.
com o docker instalado na maquina, dentro da pasta Arquivo para Testes, será possivel encontrar dois arquivo o docker-comopose e o SQL com a versão base do banco de dados.
Caso faça o teste utilizando o visual studio será necessario configurar as variaveis do aplicativo.
As principais variaveis de ambiente já estão configurados.
### Segue um exemplo como no docker-compose.
   | Variavel de ambiente | Valor |
   |---|---|
   | DataBase-User | root |
   | DataBase-Address | mysqldb |
   | DataBase-Database | Tower |
   | DataBase-Password | root |

   
Depois de rodar o docker-compose, restaure a versão do banco de dados que se encontra na pasta.

#### Ao iniciar o aplicativo, caso o banco de dados esteja vazio ele irá pedir para criar um usuário.
#### Foi utilizado o swagger para documentar e testar o endpoints da API. o caminho possui o endereço exemplo. Caso suba uma a versao do docker-compose o caminha será http://localhost:36100/swagger/index.html.
#### Para acessar as funções do swagger é necessario gerar um chave bearer ma API, utilize o email e senha cadastrado ao iniciar o aplicativo.
