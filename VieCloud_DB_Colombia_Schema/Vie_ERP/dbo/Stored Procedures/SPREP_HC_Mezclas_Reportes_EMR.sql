CREATE PROCEDURE [dbo].[SPREP_HC_Mezclas_Reportes_EMR]
(
    @Paciente VARCHAR(20),
    @NumeroIngreso VARCHAR(20)
)
WITH RECOMPILE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT A.IPCODPACI AS CodigoPaciente, RTRIM(A.NUMINGRES) AS NumeroIngreso, CASE RTRIM(A.TIPMEZLIQ)
        WHEN '1' THEN 'mezcla_continua' WHEN '2' THEN 'liquido' WHEN '3' THEN 'mezcla_frecuencia'
        WHEN '4' THEN 'mezcla_magistral' WHEN '5' THEN 'nutricion_parenteral' ELSE 'desconocido' END AS TipoMezcla,
        RTRIM(MEZLIQPAC) AS ComponenteMezcla,
        RTRIM(ADMMEZLIQ) AS ViaAdministracion, FECHAINIC AS FechaInicio, RTRIM(INDAPLMED) AS Indicaciones,
        A.DURACIONFIJA AS Duracion, A.UNIDADDURFIJA AS UnidadDuracion, PatientRiskLevelObservations AS Precauciones,
        RTRIM(B.CODDIAGNO) AS Diagnostico
    FROM dbo.HCINFLIQA A 
    inner join INDIAGNOS B ON A.CODDIAGNO = B.CODDIAGNO
    WHERE A.IPCODPACI = @Paciente AND
        A.NUMINGRES = @NumeroIngreso
END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento de reporte que recupera las órdenes de mezclas intravenosas prescritas a un paciente en un ingreso específico, clasificando el tipo de preparación (mezcla continua, líquido, magistral, nutrición parenteral, entre otros). Combina los datos de infusión con el diagnóstico asociado para generar un resumen clínico que incluye componente, vía de administración, fechas, indicaciones, duración y precauciones de riesgo, orientado al consumo desde un sistema EMR.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Mezclas_Reportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Mezclas_Reportes_EMR';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve, para un paciente e ingreso específicos, las mezclas/líquidos prescritos en historia clínica con su tipo, componente, vía, duración, indicaciones y diagnóstico asociado, para reportes EMR.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Mezclas_Reportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente y número de ingreso deben existir en el registro de líquidos/mezclas de historia clínica.; El diagnóstico referenciado en el registro de mezcla debe existir en el catálogo de diagnósticos (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Mezclas_Reportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan mezclas que tengan un diagnóstico válido en el catálogo de diagnósticos (por uso de INNER JOIN).; El resultado siempre se restringe al paciente e ingreso suministrados.; Los códigos numéricos del tipo de mezcla se traducen siempre a etiquetas legibles fijas.; No se modifica información; es solo consulta.; Se aplica RTRIM a los campos de texto retornados para eliminar espacios sobrantes.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Mezclas_Reportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ingreso/admisión; mezcla intravenosa; líquido endovenoso; nutrición parenteral; mezcla magistral; vía de administración; indicaciones médicas; diagnóstico; nivel de riesgo del paciente; historia clínica/EMR', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Mezclas_Reportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCINFLIQA: Cuando IPCODPACI = paciente y NUMINGRES = ingreso, se retornan las mezclas/líquidos del ingreso enriquecidos con la descripción del tipo de mezcla y el código de diagnóstico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Mezclas_Reportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPMEZLIQ = ''1'' → Se clasifica como ''mezcla_continua''; si TIPMEZLIQ = ''2'' → Se clasifica como ''liquido''; si TIPMEZLIQ = ''3'' → Se clasifica como ''mezcla_frecuencia''; si TIPMEZLIQ = ''4'' → Se clasifica como ''mezcla_magistral''; si TIPMEZLIQ = ''5'' → Se clasifica como ''nutricion_parenteral''; si TIPMEZLIQ con cualquier otro valor → Se clasifica como ''desconocido''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Mezclas_Reportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCINFLIQA; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Mezclas_Reportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Mezclas_Reportes_EMR';
-- GO
