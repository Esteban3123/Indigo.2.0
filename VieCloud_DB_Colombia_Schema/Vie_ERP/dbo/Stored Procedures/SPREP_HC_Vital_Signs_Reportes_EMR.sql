CREATE PROCEDURE [dbo].[SPREP_HC_Vital_Signs_Reportes_EMR]
(
    @Paciente VARCHAR(20),
    @NumeroIngreso VARCHAR(20),
    @Page int, 
    @PageSize int
)
WITH RECOMPILE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        RTRIM(TENARTSIS) AS TensionArterialSistolica,
        TALLAPACI AS Talla, 
        PESOPACIE/1000 AS Peso,
        PAMIEMBROSUPERIORDERECHO AS PresionArterialMSD, 
        PAMIEMBROSUPERIORIZQUIERDO AS PresionArterialMSI,
        PAMIEMBROINFERIORDERECHO AS PresionArterialMID, 
        PAMIEMBROINFERIORIZQUIERDO AS PresionArterialMII,
        RTRIM(FRECARPAC) AS FrecuenciaCardiaca, 
        RTRIM(REGSO2PAC) AS Saturacion, 
        RTRIM(TEMPERPAC) AS Temperatura, 
        FECREGITE AS FechaRegistro, 
        TENARTDIA AS TensionArterialDiastolica, 
        RTRIM(FRERESPAC) AS FrecuenciaRespiratoria
    FROM 
        HCEXFISIC 
    WHERE 
        IPCODPACI = @Paciente AND NUMINGRES = @NumeroIngreso
    ORDER BY FechaRegistro DESC
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera el historial de signos vitales registrados en la historia clínica de un paciente durante un ingreso hospitalario específico. Consulta la tabla de examen físico (HCEXFISIC) para obtener mediciones como tensión arterial sistólica y diastólica, talla, peso, presión arterial en los cuatro miembros (brazos y piernas), frecuencia cardíaca, frecuencia respiratoria, saturación de oxígeno y temperatura corporal, ordenadas de más reciente a más antigua. Recibe como parámetros el número de ingreso del paciente, el número de página y el tamaño de página para soportar paginación en pantalla. Se usa en el módulo de reportes del EMR (historia clínica electrónica) para visualizar la evolución de los signos vitales del paciente durante su estancia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Vital_Signs_Reportes_EMR';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Vital_Signs_Reportes_EMR';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el histórico de signos vitales y mediciones antropométricas de un paciente en un ingreso específico, ordenado del más reciente al más antiguo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Vital_Signs_Reportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el paciente y el número de ingreso en HCEXFISIC para obtener resultados; El peso almacenado debe estar en gramos (se divide entre 1000 para entregarlo en kilogramos)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Vital_Signs_Reportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El peso se entrega siempre dividido entre 1000 (conversión de gramos a kilogramos); Los campos de texto (tensión sistólica, frecuencia cardiaca, saturación, temperatura, frecuencia respiratoria) se entregan sin espacios a la derecha (RTRIM); El resultado siempre se filtra por paciente e ingreso; El orden cronológico es descendente por fecha de registro', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Vital_Signs_Reportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Signos vitales; Tensión arterial sistólica y diastólica; Presión arterial en miembros superiores e inferiores (MSD, MSI, MID, MII); Frecuencia cardiaca; Frecuencia respiratoria; Saturación de oxígeno; Temperatura; Talla; Peso; Paciente; Ingreso hospitalario; Examen físico (historia clínica)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Vital_Signs_Reportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCEXFISIC: Cuando IPCODPACI=@Paciente AND NUMINGRES=@NumeroIngreso, retorna el conjunto de signos vitales del examen físico ordenado por FECREGITE DESC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Vital_Signs_Reportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCEXFISIC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Vital_Signs_Reportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Vital_Signs_Reportes_EMR';
-- GO
