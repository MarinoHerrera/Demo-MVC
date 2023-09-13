USE [Demo]
GO

/****** Object:  Table [dbo].[Productos]    Script Date: 13/09/2023 03:21:36 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Productos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SKU] [nvarchar](6) NOT NULL,
	[Descripcion] [nvarchar](50) NOT NULL,
	[PrecioDetal] [decimal](10, 2) NOT NULL,
	[PrecioMayor] [decimal](10, 2) NOT NULL,
	[Estiba] [nvarchar](20) NOT NULL,
	[ModDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Productos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


