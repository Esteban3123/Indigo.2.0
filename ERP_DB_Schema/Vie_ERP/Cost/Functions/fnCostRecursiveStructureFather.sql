
create FUNCTION [Cost].[fnCostRecursiveStructureFather]
(
	@StructureId int,
	@Level int = 0
)
RETURNS 
@TableReturn TABLE 
(
	Id int,
	Name varchar(100),
	NumberLevel int
)
AS
BEGIN
	
	
	insert into @TableReturn values(@StructureId,(select Name from [Cost].[CostOrganizationalStructureOfCosts] where Id = @StructureId), @Level)
	
       if (select ParentId from [Cost].[CostOrganizationalStructureOfCosts] where Id = @StructureId) is not null begin
		insert into @TableReturn
		select Id, Name, NumberLevel from [Cost].[fnCostRecursiveStructureFather]((select ParentId from [Cost].[CostOrganizationalStructureOfCosts] where Id = @StructureId), @Level + 1)
		
	end
	

	RETURN 
END