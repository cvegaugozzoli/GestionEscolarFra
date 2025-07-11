USE [GestionEscolarFra]
GO

/****** Object:  Table [dbo].[Conceptos]    Script Date: 10/07/2024 9:30:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ConceptosGrillaTemp](
	[cgtId] [int] IDENTITY(1,1) NOT NULL,
	[cgtNombre] [varchar](250) NULL,
	[cgtAnioLectivo] [int] NULL,
	[cgtCuotaDesde] [int] NULL,
	[cgtCuotaHasta] [int] NULL,
 CONSTRAINT [PK_ConceptosGrillaTemp] PRIMARY KEY CLUSTERED 
(
	[cgtId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

