# Dominó

> Proyecto de Programación II.  
> Facultad de Matemática y Computación - Universidad de La Habana  
> Curso 2022

Todo lo referente a este proyecto se encuentra en dos soluciones: una conteniendo toda la lógica del dominó implementada, y una para el manejo de una interfaz gráfica.

## La solución Domino.sln

La solución **Domino.sln** contiene la biblioteca de clases **DominoEngine** y el proyecto de console **Tester**. En la biblioteca se encuentran todas las funcionalidades que permiten simular una partida de dominó con múltiples configuraciones posibles, así como distintas implementaciones de los aspectos a configurar. Esta solución está implementada en **C# 10, .NET Core 6**, y para realizar pruebas o modificaciones a la biblioteca, es necesario tener instalado este framework.  
Si se desea probar la biblioteca de clases, se deben implementar las pruebas que se deseen realizar en el **Tester** y acceder a la biblioteca de clases por medio del namespace **DominoEngine**. Para ejecutar este proyecto de consola se debe escribir en la terminal:

```bash
make dev #Linux
dotnet run --project Tester #Windows
```  

Si se realizan cambios en la biblioteca de clases, se deberá compilar dicha biblioteca, ya sea ejecutando el proyecto de consola con los comandos anteriores, o utilizando:

```bash
dotnet build DominoEngine
```

Una vez hecho esto se generará un archivo **DominoEngine.dll**, el cual se enviará automáticamente a un directorio accesible por la aplicación visual.

## La solución DominoGame.sln

**DominoGame.sln** es una solución de Unity, la cual hace uso de la biblioteca de clases **DominoEngine** para mostrar todas las opciones configurables a un usuario de forma visual, permitir que este seleccione la configuración deseada, y simular un juego con dicha configuración. Para poder acceder y trabajar con el editor de esta solución se debe tener instalado el editor de **Unity versión 2021.3.3f1 o posterior**.  
Alternativamente se puede descargar en los **Releases** del repositorio la build correspondiente al sistema operativo que se esté utilizando, y lanzar el ejecutable que se encuentra allí.  
