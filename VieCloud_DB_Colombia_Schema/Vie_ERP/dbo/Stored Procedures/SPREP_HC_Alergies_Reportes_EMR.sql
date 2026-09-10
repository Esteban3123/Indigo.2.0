CREATE PROCEDURE [dbo].[SPREP_HC_Alergies_Reportes_EMR]
(
    @Paciente varchar(25),
    @NumeroIngreso VARCHAR(20)
)
WITH RECOMPILE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @CodigoPaciente VARCHAR(20)
    SELECT TOP 1 @CodigoPaciente = IPCODPACI FROM ADINGRESO WHERE NUMINGRES = @NumeroIngreso

    SELECT 
        H.IPCODPACI AS CodigoPaciente,
        H.NUMINGRES AS NumeroIngreso,
        H.CODPRODUC AS CodigoProducto,
        P.DESPRODUC AS NombreProducto,
        H.NIVRIEPAC AS NivelRiesgo,
        CASE H.NIVRIEPAC
            WHEN '1' THEN 'low'
            WHEN '2' THEN 'medium'
            WHEN '3' THEN 'high'
            ELSE 'na'
        END AS DescripcionRiesgo,
        H.OBSNIVRIE AS ObservacionRiesgo,
        H.MOTSUSMED AS MotivoAsociado
    FROM 
        HCMEDRIES H
        LEFT JOIN IHLISTPRO P 
            ON P.CODPRODUC = H.CODPRODUC
    WHERE 
        H.NUMINGRES = @NumeroIngreso
        AND H.ALERGICO = 1 
        AND H.IPCODPACI = @Paciente
        -- AND (H.TIPOREGISTRO IS NULL 
        --      OR H.TIPOREGISTRO = 1)
    -- ORDER BY 
    --     H.FECREGIST DESC;

    UNION ALL

    SELECT
        A.IPCODPACI AS CodigoPaciente,
        NULL AS NumeroIngreso,
        NULL AS CodigoProducto,
        NULL AS NombreProducto,
        NULL AS NivelRiesgo,
        NULL AS DescripcionRiesgo,
        ANTALEPAC AS ObservacionRiesgo,
        NULL AS MotivoAsociado
    FROM HCANTPACH A
    JOIN INPACIENT B ON A.IPCODPACI = B.IPCODPACI
    WHERE A.IPCODPACI = @CodigoPaciente AND ANTALEPAC IS NOT NULL
    AND B.IPCODPACI = @Paciente
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que obtiene el listado de alergias registradas en la historia clínica de un paciente para un ingreso específico. Consulta los riesgos médicos del ingreso (HCMEDRIES) filtrando únicamente los registros marcados como alérgicos, y enriquece la información con el nombre del medicamento o producto desde el catálogo farmacéutico (IHLISTPRO). Devuelve el código y nombre del producto, el nivel de riesgo (bajo, medio, alto), las observaciones asociadas y el motivo de suspensión del medicamento. Se utiliza en reportes clínicos y en la visualización del módulo EMR para alertar sobre alergias del paciente durante la atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Alergies_Reportes_EMR';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Alergies_Reportes_EMR';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida y devuelve las alergias del paciente combinando alergias a medicamentos registradas en el ingreso con los antecedentes alérgicos generales del paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Alergies_Reportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un ingreso registrado cuyo número coincida con el solicitado para resolver el código de paciente asociado.; El paciente proporcionado debe corresponder al titular del ingreso para obtener resultados consistentes en ambos bloques.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Alergies_Reportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven medicamentos con marca de alergia activa (ALERGICO = 1).; El nivel de riesgo textual siempre se mapea a uno de los valores: low, medium, high o na.; Los antecedentes alérgicos solo se incluyen cuando el texto ANTALEPAC no es nulo.; El paciente del ingreso debe coincidir con el paciente solicitado para que aparezcan registros en ambas secciones.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Alergies_Reportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Admisión; Alergias; Antecedentes alérgicos del paciente; Producto/Medicamento; Nivel de riesgo del medicamento; Motivo de suspensión médica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Alergies_Reportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando HCMEDRIES.ALERGICO = 1 y coincide ingreso y paciente, se devuelve el medicamento con su nivel de riesgo traducido (low/medium/high/na), observación y motivo asociado.; [RETURN_RESULT] resultset: Cuando el paciente del ingreso tiene registros en HCANTPACH con ANTALEPAC no nulo, se devuelve adicionalmente cada antecedente alérgico como observación de riesgo, sin datos de medicamento ni nivel.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Alergies_Reportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si NIVRIEPAC = ''1'' → Se reporta nivel de riesgo como ''low''; si NIVRIEPAC = ''2'' → Se reporta nivel de riesgo como ''medium''; si NIVRIEPAC = ''3'' → Se reporta nivel de riesgo como ''high'' else Cualquier otro valor o nulo se reporta como ''na''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Alergies_Reportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.HCMEDRIES; dbo.IHLISTPRO; dbo.HCANTPACH; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Alergies_Reportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Alergies_Reportes_EMR';
-- GO
