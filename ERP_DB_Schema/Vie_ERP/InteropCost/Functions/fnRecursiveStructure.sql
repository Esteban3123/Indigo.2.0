-- =============================================
-- Author:		Jhon Jairo Salas
-- Create date: 05/12/2016
-- Description:	Funcion recursiva para obtener todos los Id Hijos
-- =============================================
CREATE FUNCTION [InteropCost].[fnRecursiveStructure]
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
	select Id from InteropCost.OrganizationalStructureOfCosts where ParentId = @StructureId
  
	OPEN structure_cursor  
  
	FETCH NEXT FROM structure_cursor   
	INTO @IdSon 
  
	WHILE @@FETCH_STATUS = 0  
	BEGIN  
		
		insert into @TableReturn
		select Id from InteropCost.fnRecursiveStructure(@IdSon)

		FETCH NEXT FROM structure_cursor   
		INTO @IdSon
	END   
	CLOSE structure_cursor;  
	DEALLOCATE structure_cursor;

	RETURN 
END