[![Build Status](https://dev.azure.com/MMarlonMs/MonteOlimpo/_apis/build/status/mmarlonms.monte-olimpo-base)](https://dev.azure.com/MMarlonMs/MonteOlimpo/_build/latest?definitionId=1)

![Logo](https://raw.githubusercontent.com/mmarlonms/monte-olimpo-base/master/docs/monteolimpo-logo.png)


# MonteOlimpo 
Suite de serviços para desenvolvimentos de API's baseados em uma arquitetura exagonal.

O intúito desse projeto é facilitar na criação de apis fornecendo uma configuração padrão. 

Monte Olimpo Base fornece uma suite de pacotes voltadas para questões de infra estrtura e serviços básicos como Ioc de logs, tratamento de exceções e swagger por exempo.


* ___MonteOlimpo.Base.Extensions___
	*	Apresenta Serviços extensão para as aplicações .net core, como por exemplo __RegisterAllTypes__ que realiza o IoC por assembly.
* ___MonteOlimpo.Base.Swagger___
	* Apresenta o método de extensão __AddMonteOlimpoSwagger__ que realiza a configuração do Swagger em uma aplicação .net core.

* ___MonteOlimpo.Base.Log___
	* Apresenta o método de extensão __AddMonteOlimpoLogging__ que realiza a configuração do Serillog em uma aplicação .net core com base em um arquivo __serilogsettings.json__ ( Obs.: A aplicação deve reconhecer o arquivo com serilogsettings.json como arquivo de configuração, o pacote _**MonteOlimpo.Base.Extensions**_ possui implementação para isso ).

* ___MonteOlimpo.Base.CoreException___
	*	Apresenta as classes Base para tratamento de erros de negócio e validação de Models.
	*	Referências Externas: 
		*	[otc-domain-base](https://github.com/OleConsignado/otc-domain-base)

* ___MonteOlimpo.Base.ExceptionHandler.Abstractions___
	*	Apresenta Interface para o Handle de Exceções.
* ___MonteOlimpo.Base.ExceptionHandler___
* Implementação do Handle de Exceções para APIs .net core, no qual identifica o tipo de exceção, serializa e retorna. 
Caso seja um erro 400 retorna serializa o objeto e exibe na tela uma mensagem customizada.
Caso seja um erro 500 cria um Guid, loga o erro com toda a stack e retorna somente o guid, evitando o envio da stack trace para quem realiza as chamadas.
Com isso garantimos maior segurança. 
	*	Referências:
		* [MonteOlimpo.Base.CoreException/](https://www.nuget.org/packages/MonteOlimpo.Base.CoreException/)
		* [MonteOlimpo.Base.ExceptionHandler.Abstractions/](https://www.nuget.org/packages/MonteOlimpo.Base.ExceptionHandler.Abstractions/)
* ___MonteOlimpo.Base.Filters___
	* Apresenta a implementação dos filtros MVC utilizados nas aplicações MonteOlimpo, entre elas está o filtro de exceções que utiliza o  ___MonteOlimpo.Base.ExceptionHandler___ para tratar as exceções.
	* Referências:
		* [MonteOlimpo.Base.CoreException/](https://www.nuget.org/packages/MonteOlimpo.Base.CoreException/)
		* [MonteOlimpo.Base.ExceptionHandler.Abstractions/](https://www.nuget.org/packages/MonteOlimpo.Base.ExceptionHandler.Abstractions/)

* __MonteOlimpo.Base.ValidationHandler.Abstractions__
	*	Apresenta Interface para o Handle de Validations.
* __MonteOlimpo.Base.ValidationHandler__	
	* Apresenta uma implementação do Handle de Action voltados para tratamento de validações de models utilizando [Fluent Validation](https://www.carlrippon.com/fluentvalidation-in-an-asp-net-core-web-api/). Caso haja algum erro no model de entrada é lançado um erro do tipo 420 que é serializado e retornado para o solicitante. Com isso basta implementar implementar as regras em uma classe que herde de AbstractValidator<T> e apontar o assemblie no método __GetValidationAssemblies()__ do __Startup__ da __ApiBoot__
	* Referências:
		* [MonteOlimpo.Base.CoreException/](https://www.nuget.org/packages/MonteOlimpo.Base.CoreException/)
		* [MonteOlimpo.Base.ExceptionHandler.Abstractions/](https://www.nuget.org/packages/MonteOlimpo.Base.ValidationHandler.Abstractions/)
	* Referências Externas:
		* [Fluent Validation in an ASP.NET Core Web API](https://www.carlrippon.com/fluentvalidation-in-an-asp-net-core-web-api/)
	
* __MonteOlimpo.Base.ApiBoot__
	* Apresenta a configuração base das aplicações MonteOlimpo. Através da classe MonteOlimpoBootStrap é possível utilizar toda a configuração dos pacotes acima, realizando a herança do Startup para a mesma.
	* Referências: 
		* [MonteOlimpo.Base.Extensions/](https://www.nuget.org/packages/MonteOlimpo.Base.Extensions/)
		* [MonteOlimpo.Base.Filters/](https://www.nuget.org/packages/MonteOlimpo.Base.Filters/)	
		*  [MonteOlimpo.Base.Log/](https://www.nuget.org/packages/MonteOlimpo.Base.Log/)
		* [MonteOlimpo.Base.ExceptionHandler/](https://www.nuget.org/packages/MonteOlimpo.Base.ExceptionHandler/)
		* [MonteOlimpo.Base.ValidationHandler/](https://www.nuget.org/packages/MonteOlimpo.Base.ValidationHandler/)
		*  [MonteOlimpo.Base.Swagger/](https://www.nuget.org/packages/MonteOlimpo.Base.Swagger/)



Exemplo ([monte-olimpo](https://github.com/mmarlonms/monte-olimpo))

    using MonteOlimpo.Service;
    using System.Collections.Generic;
    using System.Reflection;
    
    namespace MonteOlimpo.WebApi
    {
        public class Startup : MonteOlimpoBootStrap
        {
            public Startup(IConfiguration configuration)
                : base(configuration)
            {
            }
    
            public override void ConfigureServices(IServiceCollection services)
            {
                base.ConfigureServices(services);
            }
    
            protected override IEnumerable<Assembly> GetAditionalAssemblies()
            {
                yield return typeof(GodRepository).Assembly;
                yield return typeof(GodService).Assembly;
            }
        }
    }

Dessa forma temos uma startup enxuta tendo maior tempo desenvolvimento das regras de negócio da aplicação. 
	
  
## Nuget details
|Packages|Version & Downloads|
|---------------------------|:---:|
|*MonteOlimpo.Base.CoreException*|[![NuGet Version and Downloads count](https://buildstats.info/nuget/MonteOlimpo.Base.CoreException)](https://www.nuget.org/packages/MonteOlimpo.Base.CoreException)|
|*MonteOlimpo.Base.ExceptionHandler.Abstractions*|[![NuGet Version and Downloads count](https://buildstats.info/nuget/MonteOlimpo.Base.ExceptionHandler.Abstractions)](https://www.nuget.org/packages/MonteOlimpo.Base.ExceptionHandler.Abstractions)|
|*MonteOlimpo.Base.Filters*|[![NuGet Version and Downloads count](https://buildstats.info/nuget/MonteOlimpo.Base.Filters)](https://www.nuget.org/packages/MonteOlimpo.Base.Filters)|
|*MonteOlimpo.Base.Log*|[![NuGet Version and Downloads count](https://buildstats.info/nuget/MonteOlimpo.Base.Log)](https://www.nuget.org/packages/MonteOlimpo.Base.Log)|
|*MonteOlimpo.Base.ExceptionHandler*|[![NuGet Version and Downloads count](https://buildstats.info/nuget/MonteOlimpo.Base.ExceptionHandler)](https://www.nuget.org/packages/MonteOlimpo.Base.ExceptionHandler)|
|*MonteOlimpo.Base.Extensions*|[![NuGet Version and Downloads count](https://buildstats.info/nuget/MonteOlimpo.Base.Extensions)](https://www.nuget.org/packages/MonteOlimpo.Base.Extensions)|
|*MonteOlimpo.Base.Swagger*|[![NuGet Version and Downloads count](https://buildstats.info/nuget/MonteOlimpo.Base.Swagger)](https://www.nuget.org/packages/MonteOlimpo.Base.Swagger)|
|*MonteOlimpo.Base.ValidationHandler.Abstractions*|[![NuGet Version and Downloads count](https://buildstats.info/nuget/MonteOlimpo.Base.ValidationHandler.Abstractions)](https://www.nuget.org/packages/MonteOlimpo.Base.ValidationHandler.Abstractions)|
|*MonteOlimpo.Base.ValidationHandler*|[![NuGet Version and Downloads count](https://buildstats.info/nuget/MonteOlimpo.Base.ValidationHandler)](https://www.nuget.org/packagesMonteOlimpo.Base.ValidationHandler)|
|*MonteOlimpo.Base.ApiBoot*|[![NuGet Version and Downloads count](https://buildstats.info/nuget/MonteOlimpo.Base.ApiBoot)](https://www.nuget.org/packages/MonteOlimpo.Base.ApiBoot)|


## ![SonarCloud](https://sonarcloud.io/images/project_badges/sonarcloud-white.svg)
[![SonarCloud](https://sonarcloud.io/api/project_badges/measure?project=monte-olimpo-base&metric=ncloc)](https://sonarcloud.io/dashboard?id=monte-olimpo-base)
[![SonarCloud](https://sonarcloud.io/api/project_badges/measure?project=monte-olimpo-base&metric=duplicated_lines_density)](https://sonarcloud.io/dashboard?id=monte-olimpo-base)
[![SonarCloud](https://sonarcloud.io/api/project_badges/measure?project=monte-olimpo-base&metric=bugs)](https://sonarcloud.io/dashboard?id=monte-olimpo-base)
[![SonarCloud](https://sonarcloud.io/api/project_badges/measure?project=monte-olimpo-base&metric=vulnerabilities)](https://sonarcloud.io/dashboard?id=monte-olimpo-base)
[![SonarCloud](https://sonarcloud.io/api/project_badges/measure?project=monte-olimpo-base&metric=code_smells)](https://sonarcloud.io/dashboard?id=monte-olimpo-base)
