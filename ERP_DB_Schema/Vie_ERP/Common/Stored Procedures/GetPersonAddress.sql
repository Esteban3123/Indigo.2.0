-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Common].[GetPersonAddress]
	-- Add the parameters for the stored procedure here
	@IdentificationNumber Varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
    [Common].[Address].[Id], [Common].[Address].[Addresss] as AddressName, 
    [Common].[Address].[CityId], 
    [Common].[Address].[DepartmentId], 
    
    [Common].[City].[Name] as CityName, 
    [Common].[Department].[Name] as DepartmentName
    FROM [Common].[Address] 
    INNER JOIN [Common].[Person] 
    ON [Common].[Address].[IdPerson] = [Common].[Person].[Id] 
    INNER JOIN [Common].[City] 
    ON [Common].[City].[Id] = [Common].[Address].[CityId] 
    INNER JOIN [Common].[Department] 
    ON [Common].[Department].[Id] = [Common].[Address].[DepartmentId]
    WHERE[Common].[Person].[IdentificationNumber] = @IdentificationNumber;
END