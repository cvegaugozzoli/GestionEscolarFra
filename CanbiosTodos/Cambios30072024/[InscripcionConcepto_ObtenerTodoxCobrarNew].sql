USE [GestionEscolarFra]
GO
/****** Object:  StoredProcedure [dbo].[InscripcionConcepto_ObtenerTodoxVencidoNew]    Script Date: 31/07/2024 11:31:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter procedure [dbo].[InscripcionConcepto_ObtenerTodoxCobrarNew]
(
@insId int,
@curId int,
@Anio int,
@CuotaDesde int,
@CuotaHasta int,
@Concepto int,
@CarId int,
@hasta datetime,
@CntId int
)


-- exec [InscripcionConcepto_ObtenerTodoxVencidoNew] 121, 0, 2022, 1, 10, 203, 3, '31-12-2022'
as

--drop table ArancelesVencidos
--select * from ArancelesVencidos
select * into #ArancelesVencidos 
from(
Select Max(CoiFechaVto) as CoiFechaVto, cntId, coiNroCuota 
from Conceptos C
join ConceptosIntereses CI on C.conid = Ci.conId
where 
(conAnioLectivo = @Anio and cntId = @CntId) 
group by cntId, coiNroCuota
) as Q
where CoiFechaVto <= @hasta


if @Concepto >0 

	SELECT * FROM
	(
		SELECT * FROM
		(
			select 
			Alumno.aluNombre as [Nombre], 
			Conceptos_conId.conNombre as [Conceptos], 
			Conceptos_conId.conAnioLectivo as [AnioLectivo],  
			iif( ComprobantesCabecera.cocImporteRendido is not null, '0', 
				iif(Becas_becId.becInteres = '100', 0, 
					iif(Becas_becId.becInteres > '0', (InscripcionConcepto.IcoImporte - ((Becas_becId.becInteres * InscripcionConcepto.IcoImporte)/100)), InscripcionConcepto.IcoImporte )))
					-- iif(ComprobantesDetalle.cdeImporte is null, iif(Becas_becId.becInteres > 0, (InscripcionConcepto.IcoImporte- ((Becas_becId.becInteres * InscripcionConcepto.IcoImporte)/100)),  InscripcionConcepto.IcoImporte), InscripcionConcepto.IcoImporte) ))
			 as [Importe],
			InscripcionConcepto.icoNroCuota as [NroCuota],
			Curso.curNombre as [Curso]
			from InscripcionConcepto
			join InscripcionCursado as InscripcionCursado_icuId on InscripcionConcepto.icuId = InscripcionCursado_icuId.icuId
			join Curso on Curso.curId = InscripcionCursado_icuId.curId
			join Alumno on Alumno.aluId = InscripcionCursado_icuId.aluId
			join Conceptos as Conceptos_conId on InscripcionConcepto.conId = Conceptos_conId.conId
			--join ConceptosIntereses on ConceptosIntereses.conId = Conceptos_conId.conId
			left outer join ComprobantesDetalle on ComprobantesDetalle.icoId= InscripcionConcepto.icoId
			left outer join ComprobantesCabecera on ComprobantesCabecera.cocId = ComprobantesDetalle.cocId -- Agregado 11-03-2023
			left outer join Becas as Becas_becId on InscripcionConcepto.becId = Becas_becId.becId
			left outer join Usuario as Usuario_usuIdCreacion on InscripcionConcepto.usuIdCreacion = Usuario_usuIdCreacion.usuId
			left outer join Usuario as Usuario_usuidUltimaModificacion on InscripcionConcepto.usuidUltimaModificacion = Usuario_usuidUltimaModificacion.usuid
			join #ArancelesVencidos AV on AV.CoiNroCuota = InscripcionConcepto.icoNroCuota
			where 
			(
			(@CarId = 0 or InscripcionCursado_icuId.carId = @CarId)
			and (@curId = 0 or InscripcionCursado_icuId.curId = @curId)
			and InscripcionCursado_icuId.insId = @insId
			and InscripcionCursado_icuId.icuAnoCursado = @Anio
			and InscripcionConcepto.icoNroCuota>=@CuotaDesde
			and InscripcionConcepto.icoNroCuota<=@CuotaHasta
			and InscripcionConcepto.conId= @Concepto
			and InscripcionConcepto.icoActivo = 1
			)
		) AS Q
		WHERE Importe >0

	)AS [Algo]

	PIVOT(max([Importe]) FOR [NroCuota] IN([1],[2],[3],[4],[5],[6],[7],[8],[9],[10]
	)) AS PivotTable
	order by Curso, Nombre;

else

	SELECT * FROM
	(
		SELECT * FROM
		(
			select 
			Alumno.aluNombre as [Nombre], 
			CT.cntNombre as [Conceptos], 
			Conceptos_conId.conAnioLectivo as [AnioLectivo],  
			iif( ComprobantesCabecera.cocImporteRendido is not null, '0', 
				iif(Becas_becId.becInteres = '100', 0, 
					iif(Becas_becId.becInteres > '0', (InscripcionConcepto.IcoImporte - ((Becas_becId.becInteres * InscripcionConcepto.IcoImporte)/100)), InscripcionConcepto.IcoImporte )))
					-- iif(ComprobantesDetalle.cdeImporte is null, iif(Becas_becId.becInteres > 0, (InscripcionConcepto.IcoImporte- ((Becas_becId.becInteres * InscripcionConcepto.IcoImporte)/100)),  InscripcionConcepto.IcoImporte), InscripcionConcepto.IcoImporte) ))
			 as [Importe],
			InscripcionConcepto.icoNroCuota as [NroCuota],
			Curso.curNombre as [Curso]
			from InscripcionConcepto
			join InscripcionCursado as InscripcionCursado_icuId on InscripcionConcepto.icuId = InscripcionCursado_icuId.icuId
			join Curso on Curso.curId = InscripcionCursado_icuId.curId
			join Alumno on Alumno.aluId = InscripcionCursado_icuId.aluId
			join Conceptos as Conceptos_conId on InscripcionConcepto.conId = Conceptos_conId.conId
			left join ConceptosTipos CT on CT.cntId =  Conceptos_conId.cntId
			left outer join ComprobantesDetalle on ComprobantesDetalle.icoId= InscripcionConcepto.icoId
			left outer join ComprobantesCabecera on ComprobantesCabecera.cocId = ComprobantesDetalle.cocId -- Agregado 11-03-2023
			left outer join Becas as Becas_becId on InscripcionConcepto.becId = Becas_becId.becId
			left outer join Usuario as Usuario_usuIdCreacion on InscripcionConcepto.usuIdCreacion = Usuario_usuIdCreacion.usuId
			left outer join Usuario as Usuario_usuidUltimaModificacion on InscripcionConcepto.usuidUltimaModificacion = Usuario_usuidUltimaModificacion.usuid
			join #ArancelesVencidos AV on AV.CoiNroCuota = InscripcionConcepto.icoNroCuota
			where 
			(
			(@CarId = 0 or InscripcionCursado_icuId.carId = @CarId)
			and (@curId = 0 or InscripcionCursado_icuId.curId = @curId)
			and InscripcionCursado_icuId.insId = @insId
			and InscripcionCursado_icuId.icuAnoCursado = @Anio
			and InscripcionConcepto.icoNroCuota>=@CuotaDesde
			and InscripcionConcepto.icoNroCuota<=@CuotaHasta
			and (@Concepto = 0 or InscripcionConcepto.conId= @Concepto)
			and InscripcionConcepto.icoActivo = 1
			-- and (@Concepto = 0 or InscripcionConcepto.conId= @Concepto)
			and (@CarId = 0 or Conceptos_conId.tcaid = @CarId)
			and (@CntId = 0 or Conceptos_conId.cntId = @CntId)
			)

		) AS Q
		WHERE Importe >0
	
	)AS [Algo]

	PIVOT(max([Importe]) FOR [NroCuota] IN([1],[2],[3],[4],[5],[6],[7],[8],[9],[10]
	)) AS PivotTable
	order by Curso, Nombre;


