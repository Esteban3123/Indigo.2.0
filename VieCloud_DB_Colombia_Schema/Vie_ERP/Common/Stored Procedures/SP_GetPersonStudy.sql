-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Common].[SP_GetPersonStudy] 
	-- Add the parameters for the stored procedure here
	@IdentificationNumber Varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Common].[PersonStudy].[Id]
	,[Common].[PersonStudy].[Name] as PersonStudyName
	,[Payroll].[StudyType].[Name] as PersonStudyTypeName
	FROM [Common].[PersonStudy]
	INNER JOIN [Common].[Person]
	ON  [Common].[PersonStudy].[PersonId] = [Common].[Person].[Id]
	INNER JOIN [Payroll].[StudyType]
	ON [Payroll].[StudyType].[Id] = [Common].[PersonStudy].[StudyTypeId]
	WHERE [Common].[Person].[IdentificationNumber] = @IdentificationNumber
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta y devuelve los estudios académicos y de formación registrados para una persona, identificándola por su número de cédula o documento de identidad. Combina los datos de estudios (nombre del título o curso) con el tipo de nivel educativo (bachillerato, técnico, universitario, posgrado, etc.) y la información de la persona en el registro maestro. Se usa para visualizar el historial educativo y de formación de empleados o profesionales de la salud a partir de su cédula, identificación o documento.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_GetPersonStudy';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_GetPersonStudy';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta los estudios académicos registrados de una persona identificada por su número de identificación, mostrando el nombre del estudio y su tipo.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonStudy';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proveerse un número de identificación de la persona para filtrar los registros.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonStudy';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna estudios de personas existentes con número de identificación válido en el maestro de personas (INNER JOIN con Person).; Solo retorna estudios cuyo tipo de estudio existe en el catálogo Payroll.StudyType (INNER JOIN obligatorio).; Cada fila representa un estudio asociado a la persona identificada.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonStudy';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Persona; Estudios académicos; Tipo de estudio; Identificación', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonStudy';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Common.PersonStudy: Cuando Person.IdentificationNumber coincide con el parámetro recibido, se retorna el listado de estudios con su nombre y el nombre del tipo de estudio asociado.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonStudy';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.PersonStudy; Common.Person; Payroll.StudyType', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonStudy';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonStudy';
-- GO
