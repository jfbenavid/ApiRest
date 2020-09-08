# ApiRest
Api rest con Entity framework core, implementación de open api y JWT.

## Instrucciones
Para ejecutar el proyecto es necesario tener instalado [ASP.NET Core Runtime](https://dotnet.microsoft.com/download/dotnet-core/3.1)
Una vez el proyecto este en ejecucion, puede ingresar a la url **/swagger/index.html** y en ella podrá probar los endpoints, pero antes de probar cualquiera, deberá ingresar un token valido en la parte de autenticación, este es obtenido utilizando cualquiera de los usuarios abajo registrados en el endpoint **/api/Auth/Login**
## Usuarios predeterminados
Con estos usuarios puede acceder a los endpoints del servicio, debido a que tienen diferentes permisos, es posible que algunos endpoints no sean accesibles para los usuarios no administradores.
### Usuario Administrador
	Username: admin
	Password: admin
### Usuario NO Administrador
	Username: user
	Password: password