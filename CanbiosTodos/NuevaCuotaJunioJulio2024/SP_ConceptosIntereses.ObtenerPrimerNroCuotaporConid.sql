USE [GestionEscolarFra]
GO
/****** Object:  StoredProcedure [dbo].[ConceptosIntereses.ObtenerInteresxconId]    Script Date: 25/5/2024 18:31:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[ConceptosIntereses.ObtenerPrimerNroCuotaporConid]
(
@conId int

)
as
select min(ConceptosIntereses.coiNroCuota) as PrimerNroCuota
from ConceptosIntereses
where 1 = 1 
and conId = @conId




