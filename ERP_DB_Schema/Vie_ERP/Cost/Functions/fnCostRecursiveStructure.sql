
-- =============================================
-- Author:		Jhon Jairo Salas
-- Create date: 05/12/2016
-- Description:	Funcio recursiva para obtener todos los Id Hijos
-- =============================================
create FUNCTION [Cost].[fnCostRecursiveStructure]
(
	@StructureId int
)
RETURNS 
@TableReturn TABLE 
(
	Id int
)
AS
BEGIN
	
	insert into @TableReturn values(@StructureId)

	declare @IdSon int




	DECLARE structure_cursor CURSOR FOR   
	select Id from [Cost].[CostOrganizationalStructureOfCosts] where ParentId = @StructureId
  
	OPEN structure_cursor  
  
	FETCH NEXT FROM structure_cursor   
	INTO @IdSon 
  
	WHILE @@FETCH_STATUS = 0  
	BEGIN  
		
		insert into @TableReturn
		select Id from [Cost].[fnCostRecursiveStructure](@IdSon)

		FETCH NEXT FROM structure_cursor   
		INTO @IdSon
	END   
	CLOSE structure_cursor;  
	DEALLOCATE structure_cursor;

	RETURN 
END