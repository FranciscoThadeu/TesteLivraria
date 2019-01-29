# TesteLivraria
Aplicação em AspNetCore para Teste Avaliação 

# Arquitetura BackEnd
Web Api construída com AspNet Core 2.1.
Documentação da API realizada com Swagger.
Abordagem do padrão DDD com uso do pattern CQRS.
Uso o pattern MediatR para garantir um baixo acoplamento entre os objetos e estabelecer a comunicção entre eles.
Uso do FluentValidations para validação das entidades de domínio.
Teste Unitario e de integração com xUnit utilizando Moq, Bogus e FluentAssertions.

Inicialmente foi utilizado o AspNetIdentiy para criação e gerenciamento usuários. Além de autenticação da API com JWT. Esta funcionalidade foi removida para diminuir a complexidade da aplicação para finalidade de teste. 

# Persistência de Dados
Uso do SqlServer Local (LivrariaDataBase.mdf) localizado na pasta App_Data do projeto WebApi
Uso de EntityFrameworkCore com Dapper

