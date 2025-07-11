USE [GestionEscolarFra]
GO
/****** Object:  StoredProcedure [dbo].[Conceptos.ObtenerUno]    Script Date: 12/07/2024 8:06:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[Conceptos.ObtenerUno]
(
@conId int
)

as

select 
Conceptos.conId as [Id], 
isnull(Conceptos.conNombre,' ') as [Nombre], 
Instituciones_insId.insNombre as [Instituciones],
 ConceptosTipos_cntId.cntId as [cntId], 
ConceptosTipos_cntId.cntNombre as [ConceptosTipos], 
Conceptos.conAnioLectivo as [AnioLectivo], 
Conceptos.conImporte as [Importe], 
Conceptos.conCantCuotas as [CantCuotas], 
Conceptos.conCantVtos as [CantVtos], 
Conceptos.conMesInicio as [MesInicio], 
Conceptos.conValorSeleccionado as [ValorSeleccionado], 

Conceptos.conRecargoVtoAbierto as [RecargoVtoAbierto], 
case when Conceptos.conTieneVtoAbierto = 0 then 'No' else 'Si' end as [TieneVtoAbierto], 
isnull(Conceptos.conNotas,' ') as [Notas], 
Conceptos.conInteresMensual as [InteresMensual], 
case when Conceptos.conActivo = 0 then 'No' else 'Si' end as [Activo], 
Usuario_usuIdCreacion.usuNombre as [Usuario Creacion], 
Usuario_usuidUltimaModificacion.usuNombre as [Usuario UltimaModificacion], 
convert(varchar(10),Conceptos.conFechaHoraCreacion,103) as [FechaHoraCreacion], 
convert(varchar(10),Conceptos.conFechaHoraUltimaModificacion,103) as [FechaHoraUltimaModificacion], 
Conceptos.conId,
Conceptos.conNombre,
Conceptos.insId,
Conceptos.cntId,
Conceptos.conAnioLectivo,
Conceptos.conImporte,
Conceptos.conCantCuotas,
Conceptos.conCantVtos,
Conceptos.conMesInicio,
Conceptos.conValorSeleccionado,

Conceptos.conRecargoVtoAbierto,
Conceptos.conTieneVtoAbierto,
Conceptos.conNotas,
Conceptos.conInteresMensual,
Conceptos.conActivo,
Conceptos.usuIdCreacion,
Conceptos.usuidUltimaModificacion,
Conceptos.conFechaHoraCreacion,
Conceptos.conFechaHoraUltimaModificacion,
Conceptos.tcaid

from Conceptos
left outer join Instituciones as Instituciones_insId on Conceptos.insId = Instituciones_insId.insId
left outer join ConceptosTipos as ConceptosTipos_cntId on Conceptos.cntId = ConceptosTipos_cntId.cntId
left outer join Usuario as Usuario_usuIdCreacion on Conceptos.usuIdCreacion = Usuario_usuIdCreacion.usuId
left outer join Usuario as Usuario_usuidUltimaModificacion on Conceptos.usuidUltimaModificacion = Usuario_usuidUltimaModificacion.usuid

where 1 = 1 
and Conceptos.conId = @conId


