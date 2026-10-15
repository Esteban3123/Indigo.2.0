-- =============================================
-- Author:		Rafael Eduardo Patiño
-- Create date: 01/12/2016
-- Description:	Funcion para determinar si el medicamento es LASA 
-- =============================================
CREATE FUNCTION [dbo].[fnLASA] 
(
	@CodigoProducto as varchar(20)
)
RETURNS bit 
AS
BEGIN
	
	
	declare @Disponible as bit

	declare @DCI as varchar(20)= (	select top 1 CODDCIMED  from dbo.IHLISTPRO where CODPRODUC = @CodigoProducto)

	declare @Count as integer =(select count(*) from  (select  ID
														from dbo.IHPARAMDCI  A inner join 
														dbo.IHDCIMEDI M on A.CODDCIMED = M.CODDCIMED 
														WHERE A.CODDCIMEDPADRE = @DCI  
														union
														select  ID
														from dbo.IHPARAMDCI  A inner join 
														dbo.IHDCIMEDI M on A.CODDCIMEDPADRE = M.CODDCIMED 
														WHERE A.CODDCIMED = @DCI ) as tmp )

    if @count > 0 begin
			set @Disponible =  1
	end else begin
			set @Disponible =  0
	end
	
	 
	return @Disponible
END