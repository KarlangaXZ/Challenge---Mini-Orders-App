# Mini Orders App 📦

API REST para manejar órdenes utilizando .NET 9 y Swagger

### Como correr el Backend:

1- clonar el respositorio

```
git clone https://github.com/KarlangaXZ/Challenge---Mini-Orders-App.git
cd Challenge---Mini-Orders-App.git/backend/MiniOrdersApi
```

2- Restaurar dependencias

```
dotnet restore
```
3- Compile la aplicacion

```
dotnet build
```

4- Ejecutar el Backend

```
dotnet run
```

El servidor se levantara en el puerto:
```
http://localhost:5000 o http://localhost:5193
```
En el puerto que tenga disponible lo vera en consola.

Tambien esta disponible la documentacion de SWAGGER UI:

```
http://localhost:5193/swagger
```

Recomiendo desde aqui para hacer pruebas de todos los endpoint (`GET`,`POST`,`PUT`,`DELETE`).

---

Estos son algunos de los endpoints principales:

- `GET /api/orders` → Listar todas las órdenes

- `GET /api/orders/{id}` → Obtener orden por ID

- `POST /api/orders` → Crear nueva orden

- `PUT /api/orders/{id}` → Actualizar orden

- `DELETE /api/orders/{id}` → Eliminar orden

