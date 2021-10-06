# WSMiVenta

WebService creado en NetCore 5

## Paquetes :cactus:
> Microsoft.Entity.FrameworkCore.SqlServer 5.

> Microsoft.Entity.FrameworkCore.tools 5.

> Microsoft.AspNetCore.Authentication.JwtBearer 5.

## Conexión a base por consola :corn:
> Si el usuario en SQL Server es de windows

PM> `Scaffold-DBContext "Server=localhost;Database=MiVenta;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models`

> Si la cuenta es otra

PM> `Scaffold-DBContext "Server=localhost;Database=MiVenta;User=sa;Password=contraseña;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models`

> El  `-outputDir` es porque sin el se genera en la raiz del proyecto y en este caso se queria generar en la carpeta llamada Models del proyecto.
