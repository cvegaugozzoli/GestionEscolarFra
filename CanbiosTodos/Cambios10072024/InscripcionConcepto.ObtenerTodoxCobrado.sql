USE [GestionEscolarFra]
GO
/****** Object:  StoredProcedure [dbo].[InscripcionConcepto.ObtenerTodoxCobrado]    Script Date: 12/07/2024 11:10:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[InscripcionConcepto.ObtenerTodoxCobrado]
(
@insId int,
@curId int,
@Anio int,
@CuotaDesde int,
@CuotaHasta int,
@Concepto int,
@CarId int
)

as

SELECT *
FROM
(
select 
Alumno.aluid as [Id],
Alumno.aluNombre as [Nombre], 
Conceptos_conId.conNombre as [Conceptos], 
Conceptos_conId.conAnioLectivo as [AnioLectivo],  
ComprobantesDetalle.cdeImporte as [Importe],
InscripcionConcepto.icoNroCuota as [NroCuota],
Curso.curNombre as [Curso]
from InscripcionConcepto
join InscripcionCursado as InscripcionCursado_icuId on InscripcionConcepto.icuId = InscripcionCursado_icuId.icuId
join Curso on Curso.curId = InscripcionCursado_icuId.curId
join Alumno on Alumno.aluId = InscripcionCursado_icuId.aluId
join Conceptos as Conceptos_conId on InscripcionConcepto.conId = Conceptos_conId.conId
left outer join ConceptosIntereses on ConceptosIntereses.conId = Conceptos_conId.conId
left outer join  ComprobantesDetalle on ComprobantesDetalle.icoId= InscripcionConcepto.icoId
left outer join Becas as Becas_becId on InscripcionConcepto.becId = Becas_becId.becId
left outer join Usuario as Usuario_usuIdCreacion on InscripcionConcepto.usuIdCreacion = Usuario_usuIdCreacion.usuId
left outer join Usuario as Usuario_usuidUltimaModificacion on InscripcionConcepto.usuidUltimaModificacion = Usuario_usuidUltimaModificacion.usuid

where 1 = 1 

and (@curId = 0 or InscripcionCursado_icuId.curId = @curId)
and (@CarId = 0 or InscripcionCursado_icuId.carId = @CarId)
and InscripcionCursado_icuId.insId = @insId
and InscripcionCursado_icuId.icuAnoCursado = @Anio
and InscripcionConcepto.icoNroCuota>=@CuotaDesde
and InscripcionConcepto.icoNroCuota<=@CuotaHasta
and (@Concepto = 0 or InscripcionConcepto.conId= @Concepto)
and ComprobantesDetalle.cdeActivo = 1
and (@CarId = 0 or Conceptos_conId.tcaid = @CarId)
and  EXISTS( SELECT * FROM ComprobantesDetalle 
left outer join InscripcionConcepto on InscripcionConcepto.icoId = ComprobantesDetalle.icoId
left outer join InscripcionCursado on InscripcionConcepto.icuId = InscripcionCursado.icuId
left outer join Conceptos on InscripcionConcepto.conId = Conceptos.conId
left outer join ConceptosIntereses on ConceptosIntereses.conId = Conceptos.conId
WHERE  
(@curId = 0 or InscripcionCursado.curId = @curId)
and (@CarId = 0 or InscripcionCursado_icuId.carId = @CarId)
and InscripcionCursado.insId = @insId
and InscripcionCursado.icuAnoCursado = @Anio
and (@Concepto = 0 or InscripcionConcepto.conId= @Concepto)	
and InscripcionConcepto.icoNroCuota>=@CuotaDesde
and InscripcionConcepto.icoNroCuota<=@CuotaHasta	
and ComprobantesDetalle.cdeActivo = 1
		  ))AS [Algo]

PIVOT(max([Importe]) FOR [NroCuota] IN([1],[2],[3],[4],[5],[6],
[7],[8],[9],[10]
)) AS PivotTable
order by Curso, Nombre;



	

