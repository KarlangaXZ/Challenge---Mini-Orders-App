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

### Como correr el Frontend:

Esta es la interface de usuario, construida con Vue3, Typescript y Bootstrap 5.

Consume la API REST del `backend` para ver las lista, buscar y crear, eliminar y modificar como se solicita.

Los siguientes pasos desde la carpeta de `frontend` para arrancar.

1. instalar las dependencias.
```
npm install
```

2. Arrancar el servidor de desarrollo
```
npm run dev
```

Vite iniciara la aplicacion en el `http://localhost:5173`, desde aqui podrar ver la pagina.

3. Configurar la URL de la API

El cliente Axios de esta SPA utiliza un proxy de Vite para redirigir peticiones a `/api` hacia el backend.
Asegúrate de que el archivo `vite.config.ts` tenga la configuracion como esta, en mi caso fue esta:

```ts
server: {
  proxy: {
    '/api': {
      target: 'http://localhost:5193', <-- puerto donde corre el backend
      changeOrigin: true,
      rewrite: (path) => path.replace(/^\/api/, '')
    }
  }
}
```
----

🧪 Ejemplo de uso

Arranca el backend en la carpeta backend/:

```
cd backend/MiniOrdersApi
dotnet run
```

Arranca la SPA desde la carpeta frontend/:

```
npm install
npm run dev
```

Visita `http://localhost:5173` en tu navegador. Verás el listado de órdenes.

Usa el cuadro de búsqueda para filtrar por cliente, navega por las páginas y haz clic en Nueva Orden para crear una nueva.


## Reflexión personal:

Lo más difícil de este reto fue integrar correctamente el frontend con el backend, sobre todo porque al principio intenté trabajar con TailwindCSS y PostCSS y me encontré con múltiples errores de configuración que me hicieron perder bastante tiempo. Esa parte fue frustrante, ya que el proyecto no llegaba ni siquiera a correr.

La forma en que lo resolvi fue tomando una decision: cambiar a Bootstrap, que me permitio avanzar sin complicaciones en los estilos y concentrarme en lo más importante: la comunicacion con la API, el manejo de las ordenes, la dependencia de tailwinds presentaba mucho problemas con PostCSS y la configuracion de Vite, eso fue una de las cosa por la cual tome la decision de cambiar a BootStrap por lo estable y sencilla que es esta.

Otra parte el backend me recordo mucho a proyecto que tenia que montar una REST API con . NET, me estaba guiando de como implemente ahi, aunque me creo mucha dificultad, entonces tuve que usar algo de ayuda para esto, mencionando tambien que como no habia trabajado con Vue tenia algo de dudas mientras trabajaba en el backend, Pero bueno para no alargar mucho esto me enseño que mas alla de utilizar herramientas populares como en este caso que me llegue a trancar con tailwinds, lo importante es priorizar el flujo de trabajo y la resolucion de problemas.

Gracias por la oportunidad de participar en este proyecto, espero vernos pronto.