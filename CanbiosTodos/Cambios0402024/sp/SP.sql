USE [GestionEscolarFra]
GO
/****** Object:  StoredProcedure [dbo].[Conceptos.Actualizar]    Script Date: 4/4/2024 12:22:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[Conceptos.Actualizar]
(
@conId int,
@conNombre varchar(250),
@insId int,
@cntId int,
@conAnioLectivo int,
@conImporte numeric(18, 2),
@conCantCuotas int,
@conCantVtos int,
@conMesInicio int,
@conValorSeleccionado tinyint,
@conRecargoVtoAbierto numeric(18, 2),
@conTieneVtoAbierto tinyint,
@conNotas varchar(500),
@conInteresMensual numeric(18, 13),
@conActivo tinyint,
@usuIdCreacion int,
@usuidUltimaModificacion int,
@conFechaHoraCreacion datetime,
@conFechaHoraUltimaModificacion datetime
)

as

update Conceptos set
conNombre = @conNombre,
insId = @insId,
cntId = @cntId,
conAnioLectivo = @conAnioLectivo,
conImporte = @conImporte,
conCantCuotas = @conCantCuotas,
conCantVtos = @conCantVtos,
conMesInicio = @conMesInicio,
conValorSeleccionado=@conValorSeleccionado,
conRecargoVtoAbierto = @conRecargoVtoAbierto,
conTieneVtoAbierto = @conTieneVtoAbierto,
conNotas = @conNotas,
conInteresMensual = @conInteresMensual,
conActivo = @conActivo,
usuIdCreacion = @usuIdCreacion,
usuidUltimaModificacion = @usuidUltimaModificacion,
conFechaHoraCreacion = @conFechaHoraCreacion,
conFechaHoraUltimaModificacion = @conFechaHoraUltimaModificacion

where 1 = 1
and conId = @conId




GO
/****** Object:  StoredProcedure [dbo].[Conceptos.Insertar]    Script Date: 4/4/2024 12:22:55 ******/
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
@conFechaHoraUltimaModificacion datetime
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
conFechaHoraUltimaModificacion
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
@conFechaHoraUltimaModificacion
)

select @@identity as IdMax





GO
