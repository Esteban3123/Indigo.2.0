
-- =============================================
-- Author:        <Andres Felipe Bonilla Tocora>
-- Create date:   <14/12/2017>
-- Description:   <Mapa quirurgico>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_MapaQuirurgicoProfesionales]
(
    @fechaInicial DATETIME,
    @fechaFinal DATETIME,
	@codigoProfesional CHAR(20)
	--,@DesdeRegistro INT
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;
    SELECT DISTINCT CONVERT(VARCHAR(20), A.FECHORAIN, 103) AS Fecha,
        CONCAT(FORMAT(DATEPART(HOUR, A.FECHORAIN), '00'), ':', FORMAT(DATEPART(MINUTE, A.FECHORAIN), '00')) AS 'Hora Inicial',
        CONCAT(FORMAT(DATEPART(HOUR, A.FECHORAFI), '00'), ':', FORMAT(DATEPART(MINUTE, A.FECHORAFI), '00')) AS 'Hora final',
        CONCAT(B.CODIGSALA, ' - ', B.DESCRIPSAL) AS Sala,
        STUFF((Select ', ' + ACT.DESACTMED AS [text()]
                From dbo.AGACTIMED ACT
                INNER JOIN AGAGEMEDD DET1 ON DET1.CODACTMED=ACT.CODACTMED
                Where A.CODAUTONU=DET1.CODAUTONU
                For XML PATH ('')), 1, 2,'') As Actividades
    FROM dbo.AGAGEMEDC A
        INNER JOIN AGAGEMEDD DET ON A.CODAUTONU=DET.CODAUTONU
        INNER JOIN dbo.AGENSALAC B
            ON A.AGENSALAC = B.CODCONCEC
        INNER JOIN dbo.INPROFSAL C
            ON A.CODPROSAL = C.CODPROSAL
    WHERE A.TIPAGEMED = '0'
          AND A.AGENSALAC IS NOT NULL
          AND A.FECHORAIN >= @fechaInicial
          AND A.FECHORAFI <= @fechaFinal
		  AND A.CODPROSAL = @codigoProfesional
    ORDER BY Fecha, 'Hora Inicial'

END;
-------------------------------------------------------------
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el mapa quirúrgico de un profesional de la salud para un rango de fechas determinado. Consulta los bloques de agenda médica (AGAGEMEDC) de tipo quirúrgico, cruza la información con las salas o quirófanos asignados (AGENSALAC) y consolida las actividades o procedimientos programados (AGACTIMED/AGAGEMEDD) para cada bloque horario. El resultado muestra, por cada intervención, la fecha, hora de inicio, hora de fin, sala o quirófano y los procedimientos quirúrgicos planificados, permitiendo visualizar la ocupación quirúrgica diaria de un cirujano o especialista.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_MapaQuirurgicoProfesionales';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_MapaQuirurgicoProfesionales';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta el mapa quirúrgico de un profesional: lista las agendas en sala, con fecha, horario, sala asignada y actividades médicas asociadas, dentro de un rango de fechas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_MapaQuirurgicoProfesionales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir relación entre la agenda (AGAGEMEDC) y su detalle (AGAGEMEDD) por CODAUTONU.; La agenda debe tener sala asociada en AGENSALAC y profesional registrado en INPROFSAL.; Se requiere un código de profesional válido y un rango de fechas para filtrar.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_MapaQuirurgicoProfesionales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan agendas de tipo ''0'' (TIPAGEMED=''0''), interpretado como agenda quirúrgica/sala.; Solo se incluyen agendas asociadas a una sala (AGENSALAC IS NOT NULL).; El rango temporal se aplica con FECHORAIN >= fechaInicial y FECHORAFI <= fechaFinal, es decir, la cita debe estar completamente contenida en el rango.; Las actividades médicas se concatenan separadas por coma en una sola cadena por agenda mediante STUFF/FOR XML.; El resultado se ordena por fecha y hora inicial ascendente.; Las filas devueltas son DISTINCT (no se duplican agendas con múltiples detalles).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_MapaQuirurgicoProfesionales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Mapa quirúrgico; Agenda médica; Sala quirúrgica; Profesional de la salud; Actividad médica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_MapaQuirurgicoProfesionales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.AGAGEMEDC: Cuando TIPAGEMED=''0'', AGENSALAC no es nulo, FECHORAIN/FECHORAFI están dentro del rango y CODPROSAL coincide con el profesional, se retorna fecha, hora inicial, hora final, sala y actividades concatenadas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_MapaQuirurgicoProfesionales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGAGEMEDC; dbo.AGAGEMEDD; dbo.AGENSALAC; dbo.INPROFSAL; dbo.AGACTIMED', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_MapaQuirurgicoProfesionales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_MapaQuirurgicoProfesionales';
-- GO
