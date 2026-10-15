

-- ================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 27/06/2019
-- Description:	Procedimiento para eliminar la unidad de medida
-- ================================================
CREATE PROCEDURE [Inventory].[SP_DeleteMeasurementUnit]
	@Id int
AS
BEGIN
		
	--Código de la unidad de medida
	declare @Code varchar(20)

	Begin try

		--Se obtiene el código de la unidad de medida
		select @Code = Code from Inventory.InventoryMeasurementUnit where Id = @Id

		--Se elimina de Crystal
		delete from .INUNIMEDI where CODUNIMED = @Code

		--Se elimina de VIE
		delete from Inventory.InventoryMeasurementUnit where Id = @Id

		--Se retorna el ok
		select 0 as CodeMessage, 'Se eliminó correctamente' as Message

	End try
	Begin Catch

		--Se retorna el error
		select 999 as CodeMessage, ERROR_MESSAGE() as Message

	End Catch

END