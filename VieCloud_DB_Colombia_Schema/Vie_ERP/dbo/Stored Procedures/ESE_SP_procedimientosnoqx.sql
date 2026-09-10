-- =============================================
-- Author:		<Author,,William Suaza>
-- Create date: <Create Date, 10/05/2019,>
-- Description:	<Description, Todas las HC de control perinatal ID = 11,>
-- =============================================
CREATE PROCEDURE [dbo].[ESE_SP_procedimientosnoqx] @FechaIni DATETIME, 
                                                  @FechaFin DATETIME
AS
    BEGIN
        SELECT QX.IPCODPACI, 
               PAC.IPPRIAPEL, 
               PAC.IPSEGAPEL, 
               PAC.IPPRINOMB, 
               PAC.IPSEGNOMB, 
               PAC.IPFECNACI, 
               QX.NUMINGRES, 
               QX.CODCENATE, 
               QX.UFUCODIGO, 
               QX.CODPROSAL, 
               MED.NOMMEDICO, 
               QX.FECORDMED, 
               QX.CODSERIPS
        FROM HCORDPRON qx
             INNER JOIN INPACIENT PAC ON qx.IPCODPACI = PAC.IPCODPACI
             INNER JOIN INPROFSAL MED ON QX.CODPROSAL = MED.CODPROSAL
        WHERE qx.FECORDMED BETWEEN @FechaIni AND @FechaFin;
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta los procedimientos no quirúrgicos ordenados en historia clínica dentro de un rango de fechas, combinando las órdenes médicas de procedimientos (HCORDPRON) con los datos del paciente (nombre completo, fecha de nacimiento, cédula) y el profesional de salud que generó la orden. Retorna por cada orden: la identificación del paciente, su nombre, número de ingreso, centro de atención, unidad funcional, médico ordenador y código del servicio o procedimiento (CUPS). Sirve para reportes de producción y seguimiento de procedimientos no quirúrgicos realizados en un período determinado, útil en auditoría clínica y estadísticas de actividad asistencial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_procedimientosnoqx';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_procedimientosnoqx';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes médicas de procedimientos no quirúrgicos emitidas dentro de un rango de fechas, junto con datos del paciente y del profesional que las ordenó.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_procedimientosnoqx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las fechas de inicio y fin deben estar definidas para acotar el rango de FECORDMED; Las órdenes médicas deben tener paciente y profesional asociados válidos para aparecer en el resultado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_procedimientosnoqx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan órdenes cuya fecha de orden médica esté dentro del rango solicitado; Solo se incluyen órdenes con paciente existente en INPACIENT y profesional existente en INPROFSAL (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_procedimientosnoqx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; profesional de la salud; orden médica; procedimientos no quirúrgicos; centro de atención; unidad funcional; servicio IPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_procedimientosnoqx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCORDPRON: Cuando FECORDMED está entre las fechas indicadas, se retorna el detalle de la orden con datos del paciente (INPACIENT) y del profesional (INPROFSAL)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_procedimientosnoqx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDPRON; dbo.INPACIENT; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_procedimientosnoqx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_procedimientosnoqx';
-- GO
