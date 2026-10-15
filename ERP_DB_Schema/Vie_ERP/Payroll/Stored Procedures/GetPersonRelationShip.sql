-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Payroll].[GetPersonRelationShip]
	-- Add the parameters for the stored procedure here
	@EmployeeId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Payroll].[Employee].[Id]
      ,[EmployeeId]
      ,[Payroll].[Kinship].[Name]
	  ,[Payroll].[Relationship].[Name]  
      ,[DependsValue]      
	  FROM [Payroll].[Relationship]
	  INNER JOIN [Payroll].[Employee]
	  ON [Payroll].[Employee].[Id] =[Payroll].[Relationship].[EmployeeId]
	  INNER JOIN [Payroll].[Kinship]
	  ON [Payroll].[Kinship].[Id] = [Payroll].[Relationship].[KinshipId]
	  WHERE [Payroll].[Employee].[Id]= @EmployeeId;
END