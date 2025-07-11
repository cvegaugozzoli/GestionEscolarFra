Use GestionEscolarFra
/*
   viernes, 5 de abril de 202419:07:09
   Usuario: sa
   Servidor: PC\SQL2012
   Base de datos: GestionEscolar
   Aplicación: 
*/

/* Para evitar posibles problemas de pérdida de datos, debe revisar este script detalladamente antes de ejecutarlo fuera del contexto del diseñador de base de datos.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Conceptos
	(
	conId int NOT NULL IDENTITY (1, 1),
	conNombre varchar(250) NULL,
	insId int NULL,
	cntId int NULL,
	conAnioLectivo int NULL,
	conImporte numeric(18, 2) NULL,
	conCantCuotas int NULL,
	conCantVtos int NULL,
	conMesInicio int NULL,
	conValorSeleccionado tinyint NULL,
	conRecargoVtoAbierto numeric(18, 2) NULL,
	conTieneVtoAbierto tinyint NULL,
	conNotas varchar(500) NULL,
	conInteresMensual numeric(18, 13) NULL,
	conActivo tinyint NULL,
	usuIdCreacion int NULL,
	usuidUltimaModificacion int NULL,
	conFechaHoraCreacion datetime NULL,
	conFechaHoraUltimaModificacion datetime NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Conceptos SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Conceptos ON
GO
IF EXISTS(SELECT * FROM dbo.Conceptos)
	 EXEC('INSERT INTO dbo.Tmp_Conceptos (conId, conNombre, insId, cntId, conAnioLectivo, conImporte, conCantCuotas, conCantVtos, conMesInicio, conValorSeleccionado, conRecargoVtoAbierto, conTieneVtoAbierto, conNotas, conInteresMensual, conActivo, usuIdCreacion, usuidUltimaModificacion, conFechaHoraCreacion, conFechaHoraUltimaModificacion)
		SELECT conId, conNombre, insId, cntId, conAnioLectivo, conImporte, conCantCuotas, conCantVtos, conMesInicio, conValorSeleccionado, conRecargoVtoAbierto, conTieneVtoAbierto, conNotas, conInteresMensual, conActivo, usuIdCreacion, usuidUltimaModificacion, conFechaHoraCreacion, conFechaHoraUltimaModificacion FROM dbo.Conceptos WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Conceptos OFF
GO
DROP TABLE dbo.Conceptos
GO
EXECUTE sp_rename N'dbo.Tmp_Conceptos', N'Conceptos', 'OBJECT' 
GO
ALTER TABLE dbo.Conceptos ADD CONSTRAINT
	PK_Conceptos PRIMARY KEY CLUSTERED 
	(
	conId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE NONCLUSTERED INDEX IX_Conceptos ON dbo.Conceptos
	(
	cntId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX IX_Conceptos_1 ON dbo.Conceptos
	(
	insId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Conceptos', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Conceptos', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Conceptos', 'Object', 'CONTROL') as Contr_Per 