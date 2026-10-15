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