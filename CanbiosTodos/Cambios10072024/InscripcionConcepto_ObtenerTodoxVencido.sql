USE [GestionEscolarFra]
GO
/****** Object:  StoredProcedure [dbo].[InscripcionConcepto_ObtenerTodoxVencido]    Script Date: 12/07/2024 21:37:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[InscripcionConcepto_ObtenerTodoxVencido]
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


-- exec [InscripcionConcepto_ObtenerTodoxVencido] 121, 68, 2022, 1, 10, 204, 2

as

if @Concepto >0 

	SELECT * 
	FROM
	(
	select 
	Alumno.aluNombre as [Nombre], 
	Conceptos_conId.conNombre as [Conceptos], 
	Conceptos_conId.conAnioLectivo as [AnioLectivo],  
	iif(Becas_becId.becInteres = '100', 0, 
	iif(ComprobantesDetalle.cdeImporte is null, InscripcionConcepto.IcoImporte, '0')) as [Importe],InscripcionConcepto.icoNroCuota as [NroCuota],
	Curso.curNombre as [Curso]
	from InscripcionConcepto
	join InscripcionCursado as InscripcionCursado_icuId on InscripcionConcepto.icuId = InscripcionCursado_icuId.icuId
	join Curso on Curso.curId = InscripcionCursado_icuId.curId
	join Alumno on Alumno.aluId = InscripcionCursado_icuId.aluId
	join Conceptos as Conceptos_conId on InscripcionConcepto.conId = Conceptos_conId.conId
	--join ConceptosIntereses on ConceptosIntereses.conId = Conceptos_conId.conId
	left outer join ComprobantesDetalle on ComprobantesDetalle.icoId= InscripcionConcepto.icoId
	left outer join Becas as Becas_becId on InscripcionConcepto.becId = Becas_becId.becId
	left outer join Usuario as Usuario_usuIdCreacion on InscripcionConcepto.usuIdCreacion = Usuario_usuIdCreacion.usuId
	left outer join Usuario as Usuario_usuidUltimaModificacion on InscripcionConcepto.usuidUltimaModificacion = Usuario_usuidUltimaModificacion.usuid
	where 1 = 1 
	and (@CarId = 0 or InscripcionCursado_icuId.carId = @CarId)
	and (@curId = 0 or InscripcionCursado_icuId.curId = @curId)
	and InscripcionCursado_icuId.insId = @insId
	and InscripcionCursado_icuId.icuAnoCursado = @Anio
	and InscripcionConcepto.icoNroCuota>=@CuotaDesde
	and InscripcionConcepto.icoNroCuota<=@CuotaHasta
	and InscripcionConcepto.conId= @Concepto
	and InscripcionConcepto.icoActivo = 1
	-- and ComprobantesDetalle.cdeActivo = 1
	)AS [Algo]

	PIVOT(max([Importe]) FOR [NroCuota] IN([1],[2],[3],[4],[5],[6],[7],[8],[9],[10]
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
	iif(Becas_becId.becInteres = '100', 0, 
	iif(ComprobantesDetalle.cdeImporte is null, InscripcionConcepto.IcoImporte, '0')) as [Importe],InscripcionConcepto.icoNroCuota as [NroCuota],
	Curso.curNombre as [Curso]
	from InscripcionConcepto
	join InscripcionCursado as InscripcionCursado_icuId on InscripcionConcepto.icuId = InscripcionCursado_icuId.icuId
	join Curso on Curso.curId = InscripcionCursado_icuId.curId
	join Alumno on Alumno.aluId = InscripcionCursado_icuId.aluId
	join Conceptos as Conceptos_conId on InscripcionConcepto.conId = Conceptos_conId.conId
	left join ConceptosTipos CT on CT.cntId =  Conceptos_conId.cntId
	left outer join ComprobantesDetalle on ComprobantesDetalle.icoId= InscripcionConcepto.icoId
	left outer join Becas as Becas_becId on InscripcionConcepto.becId = Becas_becId.becId
	left outer join Usuario as Usuario_usuIdCreacion on InscripcionConcepto.usuIdCreacion = Usuario_usuIdCreacion.usuId
	left outer join Usuario as Usuario_usuidUltimaModificacion on InscripcionConcepto.usuidUltimaModificacion = Usuario_usuidUltimaModificacion.usuid
	where 1 = 1 
	and (@CarId = 0 or InscripcionCursado_icuId.carId = @CarId)
	and (@curId = 0 or InscripcionCursado_icuId.curId = @curId)
	and InscripcionCursado_icuId.insId = @insId
	and InscripcionCursado_icuId.icuAnoCursado = @Anio
	and InscripcionConcepto.icoNroCuota>=@CuotaDesde
	and InscripcionConcepto.icoNroCuota<=@CuotaHasta
	and (@Concepto = 0 or InscripcionConcepto.conId= @Concepto)
	and InscripcionConcepto.icoActivo = 1
	and (@CarId = 0 or Conceptos_conId.tcaid = @CarId)
	and (@CntId = 0 or Conceptos_conId.cntId = @CntId)
	)AS [Algo]

	PIVOT(max([Importe]) FOR [NroCuota] IN([1],[2],[3],[4],[5],[6],[7],[8],[9],[10]
	)) AS PivotTable
	order by Curso, Nombre;

