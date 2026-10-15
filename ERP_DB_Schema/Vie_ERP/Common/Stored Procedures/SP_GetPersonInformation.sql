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