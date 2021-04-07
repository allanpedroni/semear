# semear
Teste técnico

## Arquivos

* *Semear_Projecto.postman_collection.json* - Contém a coleção de chamadas Postman;
* *Desenho arquitetura proposta.png* - Desenho da arquitetura proposta diante do desafio;
* *docker-compose.yml* - Responsável por subir os microserviços e o banco de dados; 


## Pastas
`Usuarios.WebApi` - Api do contexto de Usuários

`Autenticacao.WebApi` - Api de autenticação

`Anuncios.WebApi` - Api do contexto de Anuncios

`data` - Scripts para criação e carga das tabelas utilizadas; É realizado um delay específico em relação a subida do banco e após sua criação e carga dos dados são realizados;


## Informações
Foi utilizado a arquitetura hexagonal; Foi utilizado Dapper para realização de consultas no banco sqlserver; Foram identificados 3 contextos, usuários, anuncios e autenticação. Onde a menção a cliente remete ao usuário (linguagem ubiqua adotada no domínio) mobile o qual fornece dados como email, senha. A autenticação foi trabalhada de forma separada dos contextos, deixando-a de forma a trabalhar outros contextos de autenticação no que tange o anuncio e usuários. Não foi trabalhado a questão multitenant na segurança, deixando para uma possível evolução.
