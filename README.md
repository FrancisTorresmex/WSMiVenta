# WSMiVenta

WebService creado en NetCore 5

## Paquetes :cactus:
> Microsoft.Entity.FrameworkCore.SqlServer 5.

> Microsoft.Entity.FrameworkCore.tools 5.

> Microsoft.AspNetCore.Authentication.JwtBearer 5.

## Notas: 🌎
> Web Service creado con DataBase First, por lo que se debera ejecutar primero el archivo script `MiVenta.sql` para la creación de la base de datos y todo lo necesario.

> Cuando ya se ejecuto el script desde SQL Server, se puede pasar al paso `Conexión a base por consola`.



## Conexión a base por consola :corn:
> Si el usuario en SQL Server es de windows

PM> `Scaffold-DBContext "Server=localhost;Database=MiVenta;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models`

> Si la cuenta es otra

PM> `Scaffold-DBContext "Server=localhost;Database=MiVenta;User=sa;Password=contraseña;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models`

> Nuevos elementos como tablas

PM> `Scaffold-DBContext "Server=localhost;DataBase=MiVenta;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force`

> El  `-outputDir` es porque sin el se genera en la raiz del proyecto y en este caso se queria generar en la carpeta llamada Models del proyecto.
