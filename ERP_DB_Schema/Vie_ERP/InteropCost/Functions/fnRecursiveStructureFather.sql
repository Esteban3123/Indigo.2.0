
create FUNCTION [InteropCost].[fnRecursiveStructureFather]
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
	
	
	insert into @TableReturn values(@StructureId,(select Name from InteropCost.OrganizationalStructureOfCosts where Id = @StructureId), @Level)
	
       if (select ParentId from InteropCost.OrganizationalStructureOfCosts where Id = @StructureId) is not null begin
		insert into @TableReturn
		select Id, Name, NumberLevel from InteropCost.[fnRecursiveStructureFather]((select ParentId from InteropCost.OrganizationalStructureOfCosts where Id = @StructureId), @Level + 1)
		
	end
	

	RETURN 
END