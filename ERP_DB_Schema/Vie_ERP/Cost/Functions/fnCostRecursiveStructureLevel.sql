
-- =============================================
-- Author:		Nicolas Pulido
-- Create date: 10/04/2017
-- Description:	Funcion recursiva para obtener la estructura Organizacional
-- =============================================
create FUNCTION [Cost].[fnCostRecursiveStructureLevel]
(
	@StructureId int,
	@Level int = 0
)
RETURNS 
@TableReturn TABLE 
(
	Id int,
	Code varchar(20),
	[Name] varchar(100),
	ParentId int,
	NumberLevel int
)
AS
BEGIN
	
	insert into @TableReturn
	select @StructureId, Code, [Name], ParentId, @Level from Cost.CostOrganizationalStructureOfCosts where Id = @StructureId

	declare @IdSon int
	declare @CodeSon varchar(20)
	declare @NameSon varchar(100)
	declare @ParentIdSon int

	DECLARE structure_cursor CURSOR FOR   
	select Id,Code,[Name],ParentId from Cost.CostOrganizationalStructureOfCosts where ParentId = @StructureId
  
	OPEN structure_cursor  
  
	FETCH NEXT FROM structure_cursor   
	INTO @IdSon,@CodeSon,@NameSon,@ParentIdSon
  
	WHILE @@FETCH_STATUS = 0  
	BEGIN  
		
		insert into @TableReturn		
		select Id,Code,[Name],ParentId,NumberLevel + 1 from Cost.fnCostRecursiveStructureLevel(@IdSon,@Level)

		FETCH NEXT FROM structure_cursor   
		INTO @IdSon,@CodeSon,@NameSon,@ParentIdSon
	END   
	CLOSE structure_cursor;  
	DEALLOCATE structure_cursor;

	RETURN 
END