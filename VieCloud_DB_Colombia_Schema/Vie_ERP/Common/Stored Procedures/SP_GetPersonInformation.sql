-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Common].[SP_GetPersonInformation]
	-- Add the parameters for the stored procedure here
	@Identification Varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT B.[Id], 
	B.[IdentificationNumber], 
	B.[IdentificationType], 
	B.[IdentificacionCityId], 
	D.Id as IdentificationDepartmentId,
	B.[FirstName],
	B.[SecondName], 
	B.[FirstLastName], 
	B.[SecondLastName], 
	B.[BirthDate], 
	B.[BirthCityId], 
	E.Id as BirthDepartmentId,
	B.[IdentificationExpeditionDate], 
	B.[Gender], 
	B.[MilitaryCardId],
	B.[MilitaryCardNumber], 
	B.[DeathDate], 
	B.[BloodGroup], 
	B.[RH],
	B.[MaritalStatus],
	B.[HousingType],
	B.[SocioEconomicStatus], 
	B.[CigaretteConsumption], 
	B.[SportPractice], 
	A.[Id],
	A.[Name] As IdentificationCityName,
	C.[Name] As BirthCityName,
	D.[Name] AS IdentificationDepartmentName,
	E.Name as BirthDepartmentName

	FROM [Common].[Person] AS B
	INNER JOIN [Common].[City] AS A ON B.[IdentificacionCityId] = A.[Id]
	INNER JOIN [Common].[City] AS C ON B.[BirthCityId] = C.[Id]
	INNER JOIN [Common].[Department] AS D ON A.DepartamentId = D.Id  
	INNER JOIN [Common].[Department] AS E ON C.DepartamentId = E.[Id]
	
	WHERE B.[IdentificationNumber] = @Identification;
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta el perfil completo de una persona (paciente, profesional u otro) a partir de su número de identificación o cédula. Retorna datos de identidad (tipo y número de documento, fecha de expedición, ciudad y departamento de expedición), datos personales (nombres completos, fecha de nacimiento, ciudad y departamento de nacimiento, género, estado civil, grupo sanguíneo y RH, fecha de defunción si aplica), información de libreta militar, y datos socioeconómicos (tipo de vivienda, estrato, consumo de cigarrillo, práctica deportiva). Para enriquecer la respuesta, cruza la tabla maestra de personas con el catálogo de ciudades y el catálogo de departamentos, tanto para la ciudad de expedición del documento como para la ciudad de nacimiento. Se usa principalmente para cargar o verificar la ficha demográfica de un paciente o persona en cualquier módulo del EHR que requiera identificar al individuo.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_GetPersonInformation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_GetPersonInformation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera la información demográfica e identitaria de una persona junto con su ciudad y departamento de identificación y de nacimiento, a partir del número de identificación.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La persona debe existir en el maestro de personas con el número de identificación recibido.; La ciudad de identificación y la ciudad de nacimiento deben existir en el catálogo de ciudades.; Las ciudades referenciadas deben tener un departamento asociado válido en el catálogo de departamentos.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan personas que tengan ciudad de identificación y ciudad de nacimiento válidas (INNER JOIN); si falta cualquiera de estas referencias, la persona no aparece en el resultado.; Solo se retornan personas cuyas ciudades estén ligadas a un departamento existente.; La búsqueda se realiza por número de identificación exacto, sin filtrar por tipo de identificación.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Persona; Identificación; Ciudad de identificación; Ciudad de nacimiento; Departamento; Datos demográficos; Grupo sanguíneo y RH; Estado civil; Estrato socioeconómico; Libreta militar', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando existe una persona cuyo número de identificación coincide con el parámetro y sus ciudades de identificación y nacimiento tienen departamento, se devuelve una fila con los datos de la persona enriquecidos con nombres de ciudad y departamento.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.Person; Common.City; Common.Department', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonInformation';
-- GO
