-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Common].[SP_GetPersonFreeTimeUse]
-- Add the parameters for the stored procedure here
	@IdentificationNumber Varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 

	[Common].[PersonFreeTimeUse].[Id], 
	[Payroll].[FreeTimeUse].[Code], 
	[Payroll].[FreeTimeUse].[Name] as FreeTimeUseName                            
	from [Payroll].[FreeTimeUse]                        
	inner join [Common].[PersonFreeTimeUse] 
	on [Payroll].[FreeTimeUse].[Id] = [Common].[PersonFreeTimeUse].[FreeTimeUseId] 
	inner join [Common].[person] 
	on [Common].[person].[Id] = [Common].[PersonFreeTimeUse].[PersonId] 
	where [Common].[person].[IdentificationNumber] = @IdentificationNumber;
END