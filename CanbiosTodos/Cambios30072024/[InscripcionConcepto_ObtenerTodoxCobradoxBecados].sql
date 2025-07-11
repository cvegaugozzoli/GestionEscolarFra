USE [GestionEscolarFra]
GO
/****** Object:  StoredProcedure [dbo].[InscripcionConcepto_ObtenerTodoxCobradoxBecados]    Script Date: 01/08/2024 9:41:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[InscripcionConcepto_ObtenerTodoxCobradoxBecados]
(
@insId int,
@curId int,
@Anio int,
@CuotaDesde int,
@CuotaHasta int,
@Concepto int,
@CarId int,
@CntId int
)

as
if @Concepto >0 
	SELECT *
	FROM
	(
	select 
 
	Alumno.aluNombre as [Nombre], 
	Conceptos_conId.conNombre as [Conceptos], 
	Conceptos_conId.conAnioLectivo as [AnioLectivo],  
	ComprobantesDetalle.cdeImporte as [Importe],
	InscripcionConcepto.icoNroCuota as [NroCuota],
	Curso.curNombre as [Curso]
	from InscripcionConcepto
	left outer join InscripcionCursado as InscripcionCursado_icuId on InscripcionConcepto.icuId = InscripcionCursado_icuId.icuId
	left outer join Curso on Curso.curId = InscripcionCursado_icuId.curId
	left outer join Alumno on Alumno.aluId = InscripcionCursado_icuId.aluId
	left outer join Conceptos as Conceptos_conId on InscripcionConcepto.conId = Conceptos_conId.conId
	left outer join ConceptosIntereses on ConceptosIntereses.conId = Conceptos_conId.conId
	left outer join  ComprobantesDetalle on ComprobantesDetalle.icoId= InscripcionConcepto.icoId

	left outer join Becas as Becas_becId on InscripcionConcepto.becId = Becas_becId.becId
	left outer join Usuario as Usuario_usuIdCreacion on InscripcionConcepto.usuIdCreacion = Usuario_usuIdCreacion.usuId
	left outer join Usuario as Usuario_usuidUltimaModificacion on InscripcionConcepto.usuidUltimaModificacion = Usuario_usuidUltimaModificacion.usuid

	where 1 = 1 

	and (@curId = 0 or InscripcionCursado_icuId.curId = @curId)
	and InscripcionCursado_icuId.insId = @insId
	and InscripcionCursado_icuId.icuAnoCursado = @Anio
	and InscripcionConcepto.icoNroCuota>=@CuotaDesde
	and InscripcionConcepto.icoNroCuota<=@CuotaHasta
	and ComprobantesDetalle.cdeActivo = 1
	and (@Concepto = 0 or InscripcionConcepto.conId= @Concepto)
	--and (@CarId = 0 or Conceptos_conId.tcaid = @CarId)
	and (@CntId = 0 or Conceptos_conId.cntId = @CntId)
	and InscripcionConcepto.becId<>1
	and  
	EXISTS( SELECT * FROM ComprobantesDetalle 
	left outer join InscripcionConcepto on InscripcionConcepto.icoId = ComprobantesDetalle.icoId
	left outer join InscripcionCursado on InscripcionConcepto.icuId = InscripcionCursado.icuId
	left outer join Conceptos on InscripcionConcepto.conId = Conceptos.conId
	left outer join ConceptosIntereses on ConceptosIntereses.conId = Conceptos.conId
	WHERE  
	(@curId = 0 or InscripcionCursado.curId = @curId)
	and InscripcionCursado.insId = @insId
	and InscripcionCursado.icuAnoCursado = @Anio
	and InscripcionConcepto.icoNroCuota>=@CuotaDesde
	and InscripcionConcepto.icoNroCuota<=@CuotaHasta	
	and ComprobantesDetalle.cdeActivo = 1
	and (@Concepto = 0 or InscripcionConcepto.conId= @Concepto)
	--and (@CarId = 0 or Conceptos_conId.tcaid = @CarId)
	and (@CntId = 0 or Conceptos_conId.cntId = @CntId)
	and InscripcionConcepto.becId<>1
	)
	)AS [Algo]

	PIVOT(max([Importe]) FOR [NroCuota] IN([1],[2],[3],[4],[5],[6],
	[7],[8],[9],[10]
	)) AS PivotTable
	order by Curso, Nombre;

else

	SELECT *
	FROM
	(
	select 
 
	Alumno.aluNombre as [Nombre], 
	CT.cntNombre as [Conceptos], 
	Conceptos_conId.conAnioLectivo as [AnioLectivo],  
	ComprobantesDetalle.cdeImporte as [Importe],
	InscripcionConcepto.icoNroCuota as [NroCuota],
	Curso.curNombre as [Curso]
	from InscripcionConcepto
	left outer join InscripcionCursado as InscripcionCursado_icuId on InscripcionConcepto.icuId = InscripcionCursado_icuId.icuId
	left outer join Curso on Curso.curId = InscripcionCursado_icuId.curId
	left outer join Alumno on Alumno.aluId = InscripcionCursado_icuId.aluId
	left outer join Conceptos as Conceptos_conId on InscripcionConcepto.conId = Conceptos_conId.conId
	left join ConceptosTipos CT on CT.cntId =  Conceptos_conId.cntId	
	left outer join ConceptosIntereses on ConceptosIntereses.conId = Conceptos_conId.conId
	left outer join  ComprobantesDetalle on ComprobantesDetalle.icoId= InscripcionConcepto.icoId

	left outer join Becas as Becas_becId on InscripcionConcepto.becId = Becas_becId.becId
	left outer join Usuario as Usuario_usuIdCreacion on InscripcionConcepto.usuIdCreacion = Usuario_usuIdCreacion.usuId
	left outer join Usuario as Usuario_usuidUltimaModificacion on InscripcionConcepto.usuidUltimaModificacion = Usuario_usuidUltimaModificacion.usuid

	where 1 = 1 

	and (@curId = 0 or InscripcionCursado_icuId.curId = @curId)
	and InscripcionCursado_icuId.insId = @insId
	and InscripcionCursado_icuId.icuAnoCursado = @Anio
	and InscripcionConcepto.icoNroCuota>=@CuotaDesde
	and InscripcionConcepto.icoNroCuota<=@CuotaHasta
	and ComprobantesDetalle.cdeActivo = 1
	and (@Concepto = 0 or InscripcionConcepto.conId= @Concepto)
	and (@CarId = 0 or Conceptos_conId.tcaid = @CarId)
	and (@CntId = 0 or Conceptos_conId.cntId = @CntId)
	and InscripcionConcepto.becId<>1
	and  
	EXISTS( SELECT * FROM ComprobantesDetalle 
	left outer join InscripcionConcepto on InscripcionConcepto.icoId = ComprobantesDetalle.icoId
	left outer join InscripcionCursado on InscripcionConcepto.icuId = InscripcionCursado.icuId
	left outer join Conceptos on InscripcionConcepto.conId = Conceptos.conId
	left join ConceptosTipos CT on CT.cntId =  Conceptos_conId.cntId	
	left outer join ConceptosIntereses on ConceptosIntereses.conId = Conceptos.conId
	WHERE  
	(@curId = 0 or InscripcionCursado.curId = @curId)
	and InscripcionCursado.insId = @insId
	and InscripcionCursado.icuAnoCursado = @Anio
	and InscripcionConcepto.icoNroCuota>=@CuotaDesde
	and InscripcionConcepto.icoNroCuota<=@CuotaHasta	
	and ComprobantesDetalle.cdeActivo = 1
	and (@Concepto = 0 or InscripcionConcepto.conId= @Concepto)
	and (@CarId = 0 or Conceptos_conId.tcaid = @CarId)
	and (@CntId = 0 or Conceptos_conId.cntId = @CntId)
	and InscripcionConcepto.becId<>1
	)
	)AS [Algo]

	PIVOT(max([Importe]) FOR [NroCuota] IN([1],[2],[3],[4],[5],[6],
	[7],[8],[9],[10]
	)) AS PivotTable
	order by Curso, Nombre;
