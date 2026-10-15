-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [HumanTalent].[SP_GetHierarchyOrganizationChartPosition]
	-- Add the parameters for the stored procedure here
	@PositionImmediateBossId int
AS
BEGIN
	declare @TableVariable table(PositionImmediateBossId int)
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	--Select Organization Chart Position
	insert into @TableVariable select * from HumanTalent.[GetHierarchyOrganizationChartPosition](@PositionImmediateBossId)  

	--Select Employee
	SELECT e.Id, tp.Name
	FROM @TableVariable as t
	inner join [Payroll].[Contract]	as c
	on c.[OrganizationChartPositionId] = t.PositionImmediateBossId
	inner join [Payroll].[Employee] as e
	on c.[EmployeeId] = e.[Id]
	inner join [Common].[ThirdParty] as tp
	on e.[ThirdPartyId] = tp.[Id]  
	
END