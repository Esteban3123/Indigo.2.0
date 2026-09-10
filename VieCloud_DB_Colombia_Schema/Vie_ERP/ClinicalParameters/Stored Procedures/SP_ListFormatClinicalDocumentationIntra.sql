

CREATE PROCEDURE [ClinicalParameters].[SP_ListFormatClinicalDocumentationIntra]
(
    @UnitFunctional VARCHAR(10),
    @SpecialtyCode1 VARCHAR(5),
    @SpecialtyCode2 VARCHAR(5),
    @SpecialtyCode3 VARCHAR(5)
)
AS
BEGIN
    SET NOCOUNT ON;
 
 
 -- Caso 1: Solo Unidades funcional sin Especialidades
 SELECT DISTINCT
            A.Code AS CodigoHistoria,
            A.Id AS ConsecutivoHistoria,
            A.[Name] AS NombreHistoria,
            A.[Image] AS ImagenHistoria,
			A.[IsEntryMedicalStory]
        FROM [ClinicalParameters].[ClinicalHistoryFormats] AS A  
        LEFT JOIN [ClinicalParameters].[ClinicalHistoryFormatsFunctionalUnit] AS B ON A.Id = B.IdClinicalHistoryFormats 
        WHERE A.[State] = 1
          AND B.UFUCODIGO = @UnitFunctional 
          AND NOT EXISTS (
              SELECT 1
              FROM [ClinicalParameters].[ClinicalHistoryFormatsSpecialties] AS C
              WHERE C.IdClinicalHistoryFormats = A.Id
						)          

UNION ALL
 -- Caso 2: Solo Especialidad sin Unidad Funcional  
        SELECT DISTINCT
            A.Code AS CodigoHistoria,
            A.Id AS ConsecutivoHistoria,
            A.[Name] AS NombreHistoria,
            A.[Image] AS ImagenHistoria,
			A.[IsEntryMedicalStory]
        FROM [ClinicalParameters].[ClinicalHistoryFormats] AS A  
        LEFT JOIN [ClinicalParameters].[ClinicalHistoryFormatsSpecialties] AS C ON A.Id = C.IdClinicalHistoryFormats
        WHERE A.[State] = 1
          AND C.CODESPECI IN (@SpecialtyCode1, @SpecialtyCode2, @SpecialtyCode3)
          AND NOT EXISTS (
              SELECT 1
              FROM [ClinicalParameters].[ClinicalHistoryFormatsFunctionalUnit] AS B
              WHERE B.IdClinicalHistoryFormats = A.Id
						)
          

UNION ALL

 -- Caso 3: Unidades Funcionales y por Especialidad   
        SELECT DISTINCT
            A.Code AS CodigoHistoria,
            A.Id AS ConsecutivoHistoria,
            A.[Name] AS NombreHistoria,
            A.[Image] AS ImagenHistoria,
			A.[IsEntryMedicalStory]
        FROM [ClinicalParameters].[ClinicalHistoryFormats] AS A  
			LEFT JOIN [ClinicalParameters].[ClinicalHistoryFormatsFunctionalUnit] AS B ON A.Id = B.IdClinicalHistoryFormats 
			LEFT JOIN [ClinicalParameters].[ClinicalHistoryFormatsSpecialties] AS C ON A.Id = C.IdClinicalHistoryFormats
        WHERE A.[State] = 1
            AND (B.UFUCODIGO = @UnitFunctional   AND  (C.CODESPECI IN (@SpecialtyCode1, @SpecialtyCode2, @SpecialtyCode3)))

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los formatos o plantillas de documentación clínica disponibles para uso intrahospitalario, filtrando según la unidad funcional y las especialidades médicas del profesional que realiza la atención. Combina tres escenarios: formatos habilitados solo por unidad funcional, formatos habilitados solo por especialidad, y formatos habilitados por ambos criterios simultáneamente. Retorna el código, nombre, imagen e indicador de si el formato corresponde a una nota de ingreso a historia clínica, mostrando únicamente los formatos activos (vigentes). Se utiliza para presentar al profesional de salud las plantillas clínicas disponibles según el servicio o unidad donde atiende y su especialidad.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'PROCEDURE', @level1name = N'SP_ListFormatClinicalDocumentationIntra';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'PROCEDURE', @level1name = N'SP_ListFormatClinicalDocumentationIntra';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los formatos de historia clínica activos aplicables a una atención intrahospitalaria, combinando formatos asociados solo por unidad funcional, solo por especialidad, o por ambas.', @level0type=N'SCHEMA', @level0name=N'ClinicalParameters', @level1type=N'PROCEDURE', @level1name=N'SP_ListFormatClinicalDocumentationIntra';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los formatos de historia clínica deben estar activos (State = 1) para ser considerados.; Las relaciones de formato con unidad funcional y/o especialidades deben estar previamente parametrizadas en ClinicalHistoryFormatsFunctionalUnit y ClinicalHistoryFormatsSpecialties.', @level0type=N'SCHEMA', @level0name=N'ClinicalParameters', @level1type=N'PROCEDURE', @level1name=N'SP_ListFormatClinicalDocumentationIntra';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan formatos con State = 1 (activos) en los tres casos.; Se admiten hasta tres códigos de especialidad simultáneos para evaluar la coincidencia.; El uso de UNION ALL puede producir duplicados entre casos, aunque cada SELECT individual aplica DISTINCT.', @level0type=N'SCHEMA', @level0name=N'ClinicalParameters', @level1type=N'PROCEDURE', @level1name=N'SP_ListFormatClinicalDocumentationIntra';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Formato de historia clínica; Unidad funcional; Especialidad médica; Atención intrahospitalaria; Plantilla de documentación clínica', @level0type=N'SCHEMA', @level0name=N'ClinicalParameters', @level1type=N'PROCEDURE', @level1name=N'SP_ListFormatClinicalDocumentationIntra';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ClinicalParameters.ClinicalHistoryFormats: Devuelve un resultset con CodigoHistoria, ConsecutivoHistoria, NombreHistoria, ImagenHistoria e IsEntryMedicalStory unificando tres casos vía UNION ALL (solo UF, solo especialidad, UF+especialidad).', @level0type=N'SCHEMA', @level0name=N'ClinicalParameters', @level1type=N'PROCEDURE', @level1name=N'SP_ListFormatClinicalDocumentationIntra';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Caso 1: el formato está vinculado a la unidad funcional indicada (B.UFUCODIGO = @UnitFunctional) y NO tiene ninguna especialidad asociada (NOT EXISTS en ClinicalHistoryFormatsSpecialties) → Se incluye el formato como aplicable solo por unidad funcional; si Caso 2: el formato está vinculado a alguna de las especialidades (@SpecialtyCode1, @SpecialtyCode2, @SpecialtyCode3) y NO tiene unidad funcional asociada (NOT EXISTS en ClinicalHistoryFormatsFunctionalUnit) → Se incluye el formato como aplicable solo por especialidad; si Caso 3: el formato cumple simultáneamente que B.UFUCODIGO = @UnitFunctional y C.CODESPECI está en (@SpecialtyCode1, @SpecialtyCode2, @SpecialtyCode3) → Se incluye el formato como aplicable por unidad funcional y especialidad combinadas', @level0type=N'SCHEMA', @level0name=N'ClinicalParameters', @level1type=N'PROCEDURE', @level1name=N'SP_ListFormatClinicalDocumentationIntra';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'ClinicalParameters.ClinicalHistoryFormats; ClinicalParameters.ClinicalHistoryFormatsFunctionalUnit; ClinicalParameters.ClinicalHistoryFormatsSpecialties', @level0type=N'SCHEMA', @level0name=N'ClinicalParameters', @level1type=N'PROCEDURE', @level1name=N'SP_ListFormatClinicalDocumentationIntra';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'ClinicalParameters', @level1type=N'PROCEDURE', @level1name=N'SP_ListFormatClinicalDocumentationIntra';
-- GO
