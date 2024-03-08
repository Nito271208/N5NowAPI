# Introduction and Requirements 

La siguiente API, es un requerimiento sobre una Evaluacion para la empresa N5Now.

# Getting Started and Build

	Para poder Ejecutar la API en modo local, se debe tener instalado los siguientes componentes: 

	1. ASP.NET Core Runtime 8.0.1  (https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
	2. .NET Desktop Runtime 8.0.1  (https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
	3. Visual Studio 2022 + las ultimas actualizaciones.
	4. ElasticSearch Version 7.17.18
	5. Microsoft SQL Server Management Studio 
	
	Para poder Ejecutar el FrontEnd en modo local, se debe tener instalado los siguientes componentes: 
	
	1. Instalar desde https://nodejs.org/en la version Node 20.11.1
	2. Visual Studio Code
	
	Luego se deben realizar las siguientes acciones:

	#API NET 8.0:
	
	1. Configurar el Proyecto "N5Nom.API" para que inicie aqui.
	2. La aplicacion fue desarrollada, con una base de datos en SQL Server, se adjunta en la raiz del repositorio el Script de Creacion de la Base de datos con sus respectivas tablas.
	3. Realizar las diferentes modificaciones en el "appsettings.json" en el Proyecto N5NowAPI, tales como: ConnectionStrings y Elasticsearch.
	4. Hacer click derecho sobre la solucion y ejecutar: Clean Solution y luego Rebuild Solution.
	5. Se debe tener iniciado el Elastic Search antes de ejecutar la API.
	6. Luego Ejecutar la aplicacion con F5.

	#FRONT React:
	
	1. Abrir el proyecto con Visual Studio Code.
	2. Ejecutar el comando "npm install"
	3. Ejecutar el comando "npm start"
	
# Application Architecture

	La API fue desarrollada bajo la Arquitectura CQRS (Command Query Responsibility Segregation), el cual emplea un patron arquitectónico de software que enfatiza la separación de dos responsabilidades distintas dentro de una aplicación, tales como: las operaciones de comando (mutaciones) y operaciones de consulta (solo lectura)
	
	La arquitectura que se usó, define las siguientes capas:
	
	1. N5NowAPI (Capa de Presentación de la API):
		- Contiene los controladores (Controllers) que gestionan las solicitudes HTTP y llaman a los métodos correspondientes en la capa de aplicación.
		- Inicia la aplicación.
		
	2. N5Now.Application (Capa de lógica y operaciones del negocio):
		- Contiene la lógica de negocio y las operaciones de la aplicación.
		- Puede manejar los comandos recibidos desde la capa de presentación.
		
	3. N5Now.Domain (Capa de definición de modelos de datos o dominio):
		- Define los modelos de datos que representan el dominio de la aplicación.
		- Contiene las entidades y objetos de valor que representan el núcleo del negocio.

	4. N5Now.Infrastructure:
		- Define comandos (Commands) y consultas (Queries) utilizados para la interacción con la capa de aplicación.
		- Contiene la implementación de los accesos a los datos, como el acceso a la base de datos.
	
	5. N5Now.Utilities:
		- Contiene clases estáticas que proporcionan utilidades y funciones comunes utilizadas en diferentes partes de la aplicación.

	6. N5NowUnitTes (Capa de Pruebas Unitarias):
		- Contiene pruebas unitarias utilizando MSTest para garantizar la calidad y robustez del código.
	
# Contribute

* Se puede descargar el codigo de la aplicacion a traves del siguiente link: https://github.com/Nito271208/EvaluacionNet

