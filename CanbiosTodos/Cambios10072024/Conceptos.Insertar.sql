USE [GestionEscolarFra]
GO
/****** Object:  StoredProcedure [dbo].[Conceptos.Insertar]    Script Date: 11/07/2024 20:38:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[Conceptos.Insertar]
(
@conNombre varchar(250),
@insId int,
@cntId int,
@conAnioLectivo int,
@conImporte numeric(18, 2),
@conCantCuotas int,
@conCantVtos int,
@conMesInicio int,
@conValorSeleccionado int,
@conRecargoVtoAbierto numeric(18, 2),
@conTieneVtoAbierto tinyint,
@conNotas varchar(500),
@conInteresMensual numeric(18, 13),
@conActivo tinyint,
@usuIdCreacion int,
@usuidUltimaModificacion int,
@conFechaHoraCreacion datetime,
@conFechaHoraUltimaModificacion datetime,
@tcaid int
)

as

insert into Conceptos
(
conNombre,
insId,
cntId,
conAnioLectivo,
conImporte,
conCantCuotas,
conCantVtos,
conMesInicio,
conValorSeleccionado,
conRecargoVtoAbierto,
conTieneVtoAbierto,
conNotas,
conInteresMensual,
conActivo,
usuIdCreacion,
usuidUltimaModificacion,
conFechaHoraCreacion,
conFechaHoraUltimaModificacion,
tcaid
)

values
(
@conNombre,
@insId,
@cntId,
@conAnioLectivo,
@conImporte,
@conCantCuotas,
@conCantVtos,
@conMesInicio,
@conValorSeleccionado,
@conRecargoVtoAbierto,
@conTieneVtoAbierto,
@conNotas,
@conInteresMensual,
@conActivo,
@usuIdCreacion,
@usuidUltimaModificacion,
@conFechaHoraCreacion,
@conFechaHoraUltimaModificacion,
@tcaid
)

select @@identity as IdMax





