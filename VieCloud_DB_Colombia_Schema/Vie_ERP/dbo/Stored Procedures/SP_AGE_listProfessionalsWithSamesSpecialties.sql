

CREATE PROCEDURE [dbo].[SP_AGE_listProfessionalsWithSamesSpecialties] (@CodigoProfesional Varchar(25), @Especialidades Varchar(50)) AS BEGIN 

SET NOCOUNT ON;

DECLARE @CantidadEspecialidades AS INT = LEN(@Especialidades) - LEN(REPLACE(@Especialidades, ',', '')) + 1;

WITH Profesionales AS (
	SELECT CODESPECIA, CODPROSAL, RTRIM(NOMMEDICO) AS NOMMEDICO, AssistedConsultation
	FROM INPROFSAL
	UNPIVOT
	(
	[CODESPECIA]
	FOR [Especialidad] in (CODESPEC1,CODESPEC2,CODESPEC3)
	) as p WHERE RTRIM(CODPROSAL) <> @CodigoProfesional AND (ESTADOMED='1' AND REACONEXT='True') and [CODESPECIA] in (SELECT * FROM [dbo].[SplitString](@Especialidades))
),
ConteoProfecionales AS (
	Select CODESPECIA, CODPROSAL, NOMMEDICO, AssistedConsultation, COUNT(CODPROSAL) OVER (PARTITION BY CODPROSAL) AS Repeticiones From Profesionales
)

Select Distinct Rtrim(CODPROSAL) AS CODPROSAL, NOMMEDICO, AssistedConsultation From ConteoProfecionales where Repeticiones = @CantidadEspecialidades

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Busca y devuelve la lista de profesionales de la salud (médicos, especialistas) que comparten exactamente el mismo conjunto de especialidades que se indica como parámetro, excluyendo al profesional de referencia. Consulta el maestro de profesionales (INPROFSAL) expandiendo sus tres campos de especialidad y filtrando solo los que estén activos y habilitados para atención externa. Utiliza la función auxiliar SplitString para descomponer la lista de especialidades recibida como texto separado por comas, y luego valida que el profesional encontrado cumpla con la totalidad de especialidades requeridas (no solo algunas). Se usa en agendamiento para encontrar profesionales sustitutos o equivalentes cuando se necesita reasignar citas a un médico con el mismo perfil de especialidades.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_listProfessionalsWithSamesSpecialties';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_listProfessionalsWithSamesSpecialties';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los profesionales de salud activos y habilitados para reasignación externa que comparten todas las especialidades indicadas, excluyendo al profesional de referencia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_listProfessionalsWithSamesSpecialties';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La lista de especialidades debe venir separada por comas para que el conteo (LEN - LEN(REPLACE)) sea correcto.; Debe existir la función dbo.SplitString para separar la cadena de especialidades.; La tabla de profesionales debe tener pobladas las columnas CODESPEC1, CODESPEC2, CODESPEC3, ESTADOMED y REACONEXT.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_listProfessionalsWithSamesSpecialties';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Excluye al profesional de referencia de los resultados.; Solo considera profesionales con estado activo (''1'') y habilitados para reasignación externa (REACONEXT=''True'').; Un profesional solo se retorna si cubre TODAS las especialidades solicitadas (su conteo de coincidencias iguala la cantidad de especialidades pasadas).; Las especialidades se evalúan sobre tres columnas (CODESPEC1, CODESPEC2, CODESPEC3) mediante UNPIVOT.; El listado final es DISTINCT por código de profesional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_listProfessionalsWithSamesSpecialties';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'profesional de salud; especialidad médica; médico activo; reasignación/atención externa', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_listProfessionalsWithSamesSpecialties';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.INPROFSAL: Cuando un profesional distinto al indicado tiene ESTADOMED=''1'' y REACONEXT=''True'' y sus especialidades (CODESPEC1..3) cubren todas las especialidades de la lista, se retorna su CODPROSAL y NOMMEDICO.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_listProfessionalsWithSamesSpecialties';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPROFSAL; dbo.SplitString', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_listProfessionalsWithSamesSpecialties';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_listProfessionalsWithSamesSpecialties';
-- GO
