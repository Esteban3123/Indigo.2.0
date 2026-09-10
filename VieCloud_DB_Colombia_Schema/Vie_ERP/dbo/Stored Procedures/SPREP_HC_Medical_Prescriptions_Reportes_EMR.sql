CREATE PROCEDURE [dbo].[SPREP_HC_Medical_Prescriptions_Reportes_EMR]
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

    IF @Page < 1 SET @Page = 1;
    IF @PageSize < 1 SET @PageSize = 20;
    DECLARE @Offset INT = (@Page - 1) * @PageSize;

    SELECT 
        RTRIM(P.CODPRODUC) AS CodigoMedicamento, 
        RTRIM(P.DESPRODUC) AS NombreMedicamento,
        H.DOSISPROD AS Dosis, 
        I.DESUNIMED AS UnidadMedida, 
        RTRIM(B.DESVIAADM) AS ViaAdministracion,
        H.FRECUENCI AS Frecuencia, 
        RTRIM(H.DURACIDOS) AS Duracion, 
        H.FECINIDOS AS FechaInicio,
        RTRIM(A.MEDPRINOM) AS PrimerNombreMed, 
        RTRIM(A.MEDSEGNOM) AS SegundoNombreMed, 
        RTRIM(A.MEDPRIAPEL) AS PrimerApellidoMed,
        RTRIM(A.MEDSEGAPEL) AS SegundoApellidoMed,
        H.INDAPLMED AS Indicaciones
    FROM HCPRESCRA H 
    INNER JOIN dbo.IHLISTPRO P ON H.CODPRODUC = P.CODPRODUC 
    INNER JOIN INUNIMEDI I  ON H.CODUNIMED = I.CODUNIMED 
    INNER JOIN INPROFSAL A ON H.CODPROSAL = A.CODPROSAL 
    INNER JOIN HCVIAADMI B ON H.CODVIAADM = B.CODVIAADM
    WHERE 
        H.IPCODPACI = @Paciente AND NUMINGRES = @NumeroIngreso
    ORDER BY H.FECINIDOS DESC
    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene el listado paginado de prescripciones médicas (recetas) asociadas a un número de ingreso/hospitalización específico. Para cada medicamento recetado, combina información del catálogo de productos farmacéuticos (nombre y código del medicamento), unidad de medida, vía de administración y datos del profesional de la salud que emitió la prescripción. Devuelve campos como dosis, frecuencia, duración del tratamiento, fecha de inicio e indicaciones especiales, ordenados desde la prescripción más reciente. Se usa en el módulo de historia clínica electrónica (EMR) para visualizar o reportar las órdenes de medicamentos de un paciente durante su atención o ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Medical_Prescriptions_Reportes_EMR';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Medical_Prescriptions_Reportes_EMR';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve, paginado, las prescripciones médicas (medicamentos, dosis, vía, frecuencia, duración e indicaciones) asociadas a un paciente y a un ingreso específico, junto con datos del profesional prescriptor.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Medical_Prescriptions_Reportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente y el número de ingreso deben existir y tener prescripciones registradas en HCPRESCRA; Los códigos referenciados (producto, unidad de medida, profesional de salud, vía de administración) deben existir en sus catálogos, ya que se usan INNER JOIN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Medical_Prescriptions_Reportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La paginación siempre arranca en página 1 y tamaño mínimo 20 si los parámetros vienen inválidos; Solo se retornan prescripciones que tengan correspondencia completa en los catálogos de producto, unidad de medida, profesional y vía de administración (INNER JOIN); Los resultados se ordenan siempre de la prescripción más reciente a la más antigua por fecha de inicio de dosis; Los campos de texto se devuelven sin espacios en blanco a la derecha (RTRIM)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Medical_Prescriptions_Reportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Admisión; Prescripción médica; Medicamento; Dosis; Unidad de medida; Vía de administración; Frecuencia; Duración del tratamiento; Indicaciones médicas; Profesional de salud prescriptor', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Medical_Prescriptions_Reportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCPRESCRA: Cuando H.IPCODPACI = @Paciente y NUMINGRES = @NumeroIngreso, retorna prescripciones ordenadas por FECINIDOS DESC con paginación OFFSET/FETCH', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Medical_Prescriptions_Reportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Page < 1 → Se normaliza @Page a 1; si @PageSize < 1 → Se normaliza @PageSize a 20', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Medical_Prescriptions_Reportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPRESCRA; dbo.IHLISTPRO; dbo.INUNIMEDI; dbo.INPROFSAL; dbo.HCVIAADMI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Medical_Prescriptions_Reportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Medical_Prescriptions_Reportes_EMR';
-- GO
