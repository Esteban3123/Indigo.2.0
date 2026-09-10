-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Common].[SP_GetPersonDiagnosedDesease]
	-- Add the parameters for the stored procedure here
	@IdentificationNumber Varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
	[Common].[PersonDiagnosedDisease].[Id] 
	,[Payroll].[DiagnosedDisease].[Name] as DiagnosedDiseaseName
	,[Payroll].[DiagnosedDisease].[Code]      
	FROM [Common].[PersonDiagnosedDisease]
	INNER JOIN [Common].[Person]
	ON [Common].[Person].[Id] = [Common].[PersonDiagnosedDisease].[PersonId]
	INNER JOIN [Payroll].[DiagnosedDisease] 
	ON [Common].[PersonDiagnosedDisease].[DiagnosedDiseaseId] =  [Payroll].[DiagnosedDisease].[Id]
	WHERE [Common].[Person].[IdentificationNumber] = @IdentificationNumber;
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta las enfermedades o patologías diagnosticadas que tiene registradas una persona, identificándola por su número de cédula o documento de identidad. Combina el registro maestro de personas con la relación de diagnósticos asignados y el catálogo de enfermedades del módulo de nómina, devolviendo el código y nombre de cada enfermedad diagnosticada. Se usa para conocer el historial de diagnósticos de un empleado o paciente en procesos de incapacidad, ausentismo laboral o seguimiento de condiciones médicas preexistentes.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_GetPersonDiagnosedDesease';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_GetPersonDiagnosedDesease';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta el listado de enfermedades diagnosticadas asociadas a una persona, identificada por su número de identificación, devolviendo el nombre y código del diagnóstico.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonDiagnosedDesease';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una persona en Common.Person cuyo IdentificationNumber coincida con el valor proporcionado.; Las enfermedades referenciadas en Common.PersonDiagnosedDisease deben existir en el catálogo Payroll.DiagnosedDisease para ser retornadas.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonDiagnosedDesease';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna enfermedades diagnosticadas asociadas a una persona existente (INNER JOIN con Common.Person).; Solo retorna registros cuyo diagnóstico exista en el catálogo Payroll.DiagnosedDisease (INNER JOIN).; El filtro de búsqueda se realiza por número de identificación de la persona, no por su Id interno.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonDiagnosedDesease';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Persona; Enfermedad diagnosticada; Identificación de persona', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonDiagnosedDesease';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Common.PersonDiagnosedDisease: Cuando IdentificationNumber coincide con una persona, retorna el conjunto de diagnósticos vinculados con su nombre y código desde el catálogo de enfermedades.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonDiagnosedDesease';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.PersonDiagnosedDisease; Common.Person; Payroll.DiagnosedDisease', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonDiagnosedDesease';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonDiagnosedDesease';
-- GO
