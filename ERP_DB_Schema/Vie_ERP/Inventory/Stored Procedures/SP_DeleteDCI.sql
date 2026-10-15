

-- ================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 27/06/2019
-- Description:	Procedimiento para eliminar el DCI
-- ================================================
CREATE PROCEDURE [Inventory].[SP_DeleteDCI]
	@Id int
AS
BEGIN
		
	--Código del DCI
	declare @Code varchar(20)

	Begin try

		--Se obtiene el código del DCI
		select @Code = Code from Inventory.DCI where Id = @Id

		--Se elimina de Crystal
		delete from .IHDCIMEDI where CODDCIMED = @Code

		--Se elimina de VIE
		delete from Inventory.DCIATCEntity where IdDCI = @Id
		delete from Inventory.DrugInteraction where ParentDCIId = @Id
		delete from Inventory.DCI where Id = @Id

		--Se retorna el ok
		select 0 as CodeMessage, 'Se eliminó correctamente' as Message

	End try
	Begin Catch

		--Se retorna el error
		select 999 as CodeMessage, ERROR_MESSAGE() as Message

	End Catch

END