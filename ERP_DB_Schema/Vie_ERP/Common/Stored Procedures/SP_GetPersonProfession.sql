-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Common].[SP_GetPersonProfession]
	-- Add the parameters for the stored procedure here
	@IdentificationNumber int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Common].[PersonProfession].[Id]      
	,[Payroll].[Profession].[Name]
	FROM [Common].[PersonProfession]
	INNER JOIN [Common].[Person]
	ON [Common].[Person].[Id] = [Common].[PersonProfession].[PersonId]
	INNER JOIN [Payroll].[Profession]
	ON [Payroll].[Profession].[Id] = [Common].[PersonProfession].[IdProfession]
	Where [Common].[Person].[IdentificationNumber] = @IdentificationNumber


END