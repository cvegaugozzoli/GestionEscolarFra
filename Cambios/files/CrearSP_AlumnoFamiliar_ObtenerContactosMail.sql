USE [GestionEscolarFra]
GO
/****** Object:  StoredProcedure [dbo].[Alumno.ObtenerUno]    Script Date: 17/09/2026 8:27:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ============================================================================
-- Nuevo SP para la funcionalidad "Enviar Estado de Cuenta por Mail" (CuentaCorriente.aspx)
-- Trae, para un alumno, todos los familiares vinculados (padre, madre, tutor, etc.)
-- con su nombre, mail y descripción de parentesco.
-- No reemplaza a [AlumnoFamiliar.ObtenerTodoxaluId], que ya usan otras pantallas.
-- ============================================================================
Create PROCEDURE [dbo].[AlumnoFamiliar.ObtenerContactosMail]
(
    @aluId INT
)
AS
    SELECT
        f.famId,
        f.famApellido,
        f.famNombre,
        f.famMail,
        af.afaEsTutor,
        p.patDescripcion
    FROM dbo.AlumnoFamiliar af
    INNER JOIN dbo.Familiar f ON f.famId = af.famId
    LEFT JOIN dbo.PARENTESCOS p ON p.patId = af.patId
    WHERE af.aluId = @aluId
    ORDER BY af.afaEsTutor DESC, f.famApellido, f.famNombre;
GO
