

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