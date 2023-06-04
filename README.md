# Trabajo Práctico N° 3
## **Universidad Tecnológica Nacional**
### Facultad Regional Pacheco
### Tecnicatura Universitaria en Programación
### Programación III
### 2023

----

#### **Integrantes:**
- **Merayo, Federico** - Github: [FmmerayoUTN ](https://github.com/FmmerayoUTN)
- **Villalba, Diego** - Github: [diego](https://github.com/dmatia)
- **Gomez Nieto, Alejandro** - Github: [alefigure8](https://github.com/alefigure8)

----

#### **Profesores:**
- **Sar Fernandez, Maximiliano**
- **Laurentino Goncalves, Regina**

----

#### **Requerimientos:**
- [x] 1.  **Visual Studio 2019** -  [Descargar](https://visualstudio.microsoft.com/es/vs/)
- [x] 2.  **SQL Server 2019** -  [Descargar](https://www.microsoft.com/es-es/sql-server/sql-server-downloads)
- [x] 3.  **SQL Server Management Studio** -  [Descargar](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15)
- [x] 4.  **.NET Framework 4.7.2** -  [Descargar](https://dotnet.microsoft.com/download/dotnet-framework/net472)

----

### **Carrito de compras**

#### **Objetivo:**
Partiendo de la arquitectura armada en el TP de Catálogo de Productos, construir una aplicación web que permita navegar una lista de productos e ir agregando los productos deseados a un carrito de compras. 

La pantalla home o index, será el listado de productos en el cual se deberá contar con una opción de filtrado. El diseño visual de la página es libre, sin embargo se recomienda bootstrap para el diseño general y cards para cada producto (símil mercado libre, ebay, etc).

Se deberá contar con una pantalla de detalle de producto en la cual se podrá ver todos los datos del mismo y todas sus imágenes en caso de tener más de una. El formato para mostrar las imágenes es libre.

A medida que se agregan items al carrito se debería poder visualizar la cantidad de productos agregados en todo momento en una barra superior; también contar con un botón para navegar al carrito en el cual se podrá ver el listado de productos agregados con el precio total a pagar y donde también se debería poder eliminar productos.

Para los productos ya cuentan con las clases correspondientes. Para el carrito de compras, deberán crear lo necesario. La información del carrito de compras NO se almacena en la base de datos, dicha información deberá mantenerse actualizada en la sesión del servidor.

#### **Consideraciones:**
 - No hay que realizar conexiones nuevas a bases de datos.
 - Pueden mejorar las conexiones existentes agregando la clase Acceso a Datos.
 - Crear el nuevo set de clases necesario para el armado del carrito.
 - Crear un nuevo repositorio y tener en cuenta la distribución de tareas y el manejo con los commits

---

#### **Despliegue:**

El proyecto se encuentra en deplegado en SOMEE. Puede acceder a la aplicación desde el siguiente link: 

[Grupo25-Carrito](http://grupo25.somee.com/)

---

#### **Páginas:**
```csharp
http://localhost:Default.aspx/
http://localhost:Detalle.aspx/
http://localhost:CarritoWeb.aspx/
http://localhost:OrdenCompra.aspx/
http://localhost:Nosotros.aspx/
```

#### **Solución**
```csharp
tp3-equipo25.sln
```

#### **Base de datos**
```csharp
USE MASTER
GO

CREATE DATABASE CATALOGO_P3_DB
GO

USE CATALOGO_P3_DB
GO

USE CATALOGO_P3_DB
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MARCAS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NULL,
 CONSTRAINT [PK_MARCAS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CATEGORIAS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NULL,
 CONSTRAINT [PK_CATEGORIAS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ARTICULOS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [varchar](50) NULL,
	[Nombre] [varchar](50) NULL,
	[Descripcion] [varchar](150) NULL,
	[IdMarca] [int] NULL,
	[IdCategoria] [int] NULL,
	[Precio] [money] NULL,
 CONSTRAINT [PK_ARTICULOS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO

create table IMAGENES(
	Id int IDENTITY(1,1) not null,
	IdArticulo int not null,
	ImagenUrl varchar(1000) not null
)
GO

INSERT INTO MARCAS VALUES ('Samsung'), ('Apple'), ('Sony'), ('Huawei'), ('Motorola')
INSERT INTO CATEGORIAS VALUES ('Celulares'),('Televisores'), ('Media'), ('Audio')
INSERT INTO ARTICULOS VALUES ('S01', 'Galaxy S10', 'Una canoa cara', 1, 1, 69.999),
('M03', 'Moto G Play 7ma Gen', 'Ya siete de estos?', 1, 5, 15699),
('S99', 'Play 4', 'Ya no se cuantas versiones hay', 3, 3, 35000),
('S56', 'Bravia 55', 'Alta tele', 3, 2, 49500),
('A23', 'Apple TV', 'lindo loro', 2, 3, 7850)

INSERT INTO imagenes VALUES
(1,'https://images.samsung.com/is/image/samsung/co-galaxy-s10-sm-g970-sm-g970fzyjcoo-frontcanaryyellow-thumb-149016542'),
(2, 'https://www.motorola.cl/arquivos/moto-g7-play-img-product.png?v=636862863804700000'),
(2, 'https://i.blogs.es/9da288/moto-g7-/1366_2000.jpg'),
(3, 'https://www.euronics.cz/image/product/800x800/532620.jpg'),
(4, 'https://intercompras.com/product_thumb_keepratio_2.php?img=images/product/SONY_KDL-55W950A.jpg&w=650&h=450'),
(5, 'https://cnnespanol2.files.wordpress.com/2015/12/gadgets-mc3a1s-populares-apple-tv-2015-18.jpg?quality=100&strip=info&w=460&h=260&crop=1')
GO
```

---

#### **Screenshot**

![Screenshot](https://i.imgur.com/jW2fDtZ.png)

![Screenshot](https://i.imgur.com/HEW8a13.png)

![Screenshot](https://i.imgur.com/DWFUN5h.png)

#### **Cupón descuento: "GENTESSS"**
