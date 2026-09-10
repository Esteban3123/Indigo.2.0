-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Common].[SP_GetPersonStudyType] 
	-- Add the parameters for the stored procedure here
	@IdentificationNumber Varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Common].[PersonStudy].[Id]
	,[Payroll].[StudyType].[Name] as PersonStudyTypeName
	FROM [Common].[PersonStudy]
	INNER JOIN [Common].[Person]
	ON  [Common].[PersonStudy].[PersonId] = [Common].[Person].[Id]
	INNER JOIN [Payroll].[StudyType]
	ON [Payroll].[StudyType].[Id] = [Common].[PersonStudy].[StudyTypeId]
	WHERE [Common].[Person].[IdentificationNumber] = @IdentificationNumber
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta los tipos de estudio o formación académica registrados para una persona, buscándola por su número de identificación (cédula o documento). Cruza los estudios académicos de la persona (PersonStudy) con el catálogo de tipos de formación (bachillerato, técnico, universitario, posgrado, etc.) y con el registro maestro de personas, devolviendo el nombre del nivel educativo asociado a cada estudio. Se usa en recursos humanos y nómina para conocer el perfil académico de empleados o profesionales de la salud a partir de su documento de identidad.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_GetPersonStudyType';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_GetPersonStudyType';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene los estudios académicos registrados de una persona identificada por su número de identificación, junto con el nombre del tipo de estudio.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonStudyType';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La persona debe existir en el maestro de personas con el número de identificación suministrado; Los estudios de la persona deben tener un tipo de estudio válido referenciado en el catálogo de tipos de estudio', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonStudyType';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan estudios asociados a una persona existente (INNER JOIN con Person); Solo se retornan estudios con tipo de estudio válido en el catálogo (INNER JOIN con StudyType); El filtro se realiza por número de identificación de la persona, no por su Id interno', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonStudyType';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Persona; Estudios académicos de la persona; Tipo de estudio/formación académica; Número de identificación', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonStudyType';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Common.PersonStudy: Devuelve el Id del estudio y el nombre del tipo de estudio para todos los estudios cuya persona coincide con el número de identificación recibido', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonStudyType';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.PersonStudy; Common.Person; Payroll.StudyType', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonStudyType';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonStudyType';
-- GO
