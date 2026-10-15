-- ================================================
-- Author:		Hector Rodriguez
-- Create date: 13/01/2020
-- Description:	Procedimiento para eliminar el insumo
-- ================================================
CREATE PROCEDURE [Inventory].[SP_DeleteSupplie]
	@Id int
AS
BEGIN
		
	--Código del insumo
	declare @Code varchar(20)

	Begin try

		--Se obtiene el código del medicamento
		select @Code = Code from Inventory.InventorySupplie where Id = @Id

		--Se elimina de Crystal
		delete from .IHLISTPRO where CODPRODUC = @Code
				
		--Se elimina de VIE
		delete from Inventory.InventorySupplie where Id = @Id

		--Se retorna el ok
		select 0 as CodeMessage, 'Se eliminó correctamente' as Message

	End try
	Begin Catch

		--Se retorna el error
		select 999 as CodeMessage, ERROR_MESSAGE() as Message

	End Catch

END