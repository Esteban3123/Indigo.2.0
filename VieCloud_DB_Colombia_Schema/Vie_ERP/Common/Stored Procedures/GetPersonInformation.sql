-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Common].[GetPersonInformation]
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta el perfil completo de una persona (paciente, profesional u otro actor del sistema) a partir de su número de identificación o cédula. Combina los datos del registro maestro de personas con los catálogos de ciudades y departamentos para devolver, en una sola respuesta, la información de identidad (tipo y número de documento, fecha de expedición, ciudad y departamento de expedición), datos de nacimiento (fecha, ciudad y departamento de nacimiento), características demográficas (nombres completos, género, estado civil, fecha de defunción si aplica) y datos socioeconómicos y de salud (grupo sanguíneo, RH, estrato, tipo de vivienda, consumo de cigarrillo, práctica deportiva). Se usa para buscar y mostrar la ficha personal de un individuo en cualquier módulo del ERP/EHR que necesite identificar a una persona por su cédula o documento.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'GetPersonInformation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'GetPersonInformation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera la información demográfica y geográfica completa de una persona a partir de su número de identificación, incluyendo ciudad y departamento de expedición y de nacimiento.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La persona debe existir con el número de identificación suministrado; La ciudad de identificación y la ciudad de nacimiento de la persona deben estar registradas en el catálogo de ciudades; Las ciudades referenciadas deben tener un departamento asociado válido', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan personas cuya ciudad de identificación y ciudad de nacimiento existan en el catálogo y tengan departamento asociado (uso de INNER JOIN); La búsqueda se realiza por coincidencia exacta del número de identificación; Siempre se entregan en una misma fila los nombres de ciudad y departamento tanto de identificación como de nacimiento', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Persona; Identificación; Ciudad de expedición; Ciudad de nacimiento; Departamento; Datos demográficos; Género; Estado civil; Grupo sanguíneo y RH; Estrato socioeconómico; Libreta militar', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Common.Person: Retorna los datos de la persona y su geografía cuando IdentificationNumber coincide con el valor recibido y existen joins válidos con City y Department tanto para identificación como para nacimiento', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.Person; Common.City; Common.Department', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonInformation';
-- GO
