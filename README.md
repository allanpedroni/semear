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


## Informações Gerais
Foi utilizado a arquitetura hexagonal visando um possível crescimento e integração via adapter secundários com outros microserviços; Foi utilizado Dapper para realização de consultas no banco sqlserver; Foram identificados 3 contextos, onde o modelo de domínio usuários e anuncios são os principais e autenticação trabalha a geração e obtenção do token. O microserviço de autenticação nesse contexto do desafio, nos permite trabalhar de forma incremetal e evolutiva de forma que podemos trabalhar outras autenticações via novos adapters primários /secundários. 

## Possíveis evoluções
Poderiamos identificar a necessidade da autenticação ser multitenant; 
Trabalhar anotações nos dtos de entrada das rotas de forma a facilitar as validações de entrada; 
Trabalhar a rastreabilidade e monitoramento das funcionalidades disponibilizadas. 
A conversão do domínio para viewModels/Dto pode ser realizada com AutoMapper, segregando as identificações from/to no profile; 
Implementar um microserviço como BFF exclusivo para o mobile, o qual trabalhará centralizará as segurança, chamadas e funcionalidades voltadas ao cliente mobile 
