

CREATE PROCEDURE [dbo].[SP_AGE_listProfessionalsWithSamesSpecialties_TransferAppointments] (@CodigoProfesional Varchar(25), @Especialidades Varchar(50)) AS BEGIN 

SET NOCOUNT ON;

DECLARE @CantidadEspecialidades AS INT = LEN(@Especialidades) - LEN(REPLACE(@Especialidades, ',', '')) + 1;

WITH Profesionales AS (
	SELECT CODESPECIA, CODPROSAL, RTRIM(NOMMEDICO) AS NOMMEDICO
	FROM INPROFSAL
	UNPIVOT
	(
	[CODESPECIA]
	FOR [Especialidad] in (CODESPEC1,CODESPEC2,CODESPEC3)
	) as p WHERE /* RTRIM(CODPROSAL) <> @CodigoProfesional AND */ (ESTADOMED='1' AND REACONEXT='True') and [CODESPECIA] in (SELECT * FROM [dbo].[SplitString](@Especialidades))
),
ConteoProfecionales AS (
	Select CODESPECIA, CODPROSAL, NOMMEDICO, COUNT(CODPROSAL) OVER (PARTITION BY CODPROSAL) AS Repeticiones From Profesionales
)

Select Distinct Rtrim(CODPROSAL) AS CODPROSAL, NOMMEDICO From ConteoProfecionales where Repeticiones = @CantidadEspecialidades

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los profesionales de la salud que comparten exactamente las mismas especialidades que un profesional de origen, con el fin de facilitar la transferencia o reasignación de citas médicas. Recibe el código del profesional y una lista separada por comas de códigos de especialidad, luego consulta el maestro de profesionales (INPROFSAL) expandiendo sus tres posibles especialidades (CODESPEC1, CODESPEC2, CODESPEC3) mediante UNPIVOT, y filtra únicamente los profesionales activos y disponibles para atención externa. Devuelve solo aquellos profesionales que cubren la totalidad de las especialidades solicitadas, asegurando que el reemplazo sea equivalente en capacidad clínica. Se usa en el módulo de agendamiento cuando se necesita reasignar la agenda completa de un profesional a otro con el mismo perfil de especialidades.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_listProfessionalsWithSamesSpecialties_TransferAppointments';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_listProfessionalsWithSamesSpecialties_TransferAppointments';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista profesionales activos y habilitados para atención externa que cubren simultáneamente todas las especialidades indicadas, para reasignar/transferir citas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_listProfessionalsWithSamesSpecialties_TransferAppointments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La lista de especialidades debe venir como cadena separada por comas, parseable por dbo.SplitString.; INPROFSAL debe contener las columnas de especialidad (CODESPEC1, CODESPEC2, CODESPEC3) y los flags ESTADOMED y REACONEXT.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_listProfessionalsWithSamesSpecialties_TransferAppointments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran profesionales activos (ESTADOMED=''1'') y habilitados para reconsulta externa (REACONEXT=''True'').; Un profesional solo es retornado si cubre TODAS las especialidades solicitadas (conteo de coincidencias = cantidad de especialidades en la lista).; La cantidad de especialidades requeridas se deriva contando las comas en la lista de entrada más uno.; Se evalúan hasta 3 especialidades por profesional (CODESPEC1, CODESPEC2, CODESPEC3) vía UNPIVOT.; El resultado elimina duplicados y aplica RTRIM al código del profesional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_listProfessionalsWithSamesSpecialties_TransferAppointments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'profesional de salud; especialidad médica; transferencia de citas; atención externa', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_listProfessionalsWithSamesSpecialties_TransferAppointments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.INPROFSAL: Devuelve CODPROSAL y NOMMEDICO de profesionales con ESTADOMED=''1'' y REACONEXT=''True'' cuyas especialidades (CODESPEC1..3) incluyen todas las recibidas en @Especialidades (Repeticiones = cantidad de especialidades).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_listProfessionalsWithSamesSpecialties_TransferAppointments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPROFSAL; dbo.SplitString', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_listProfessionalsWithSamesSpecialties_TransferAppointments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_listProfessionalsWithSamesSpecialties_TransferAppointments';
-- GO
