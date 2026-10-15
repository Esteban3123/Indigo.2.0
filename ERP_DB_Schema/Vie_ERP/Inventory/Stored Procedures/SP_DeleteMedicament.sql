

-- ================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 27/06/2019
-- Description:	Procedimiento para eliminar el ATC(Ahora medicamento)
-- ================================================
CREATE PROCEDURE [Inventory].[SP_DeleteMedicament]
	@Id int
AS
BEGIN
		
	--Código del medicamento
	declare @Code varchar(20)

	Begin try

		--Se obtiene el código del medicamento
		select @Code = Code from Inventory.ATC where Id = @Id

		--Se elimina de Crystal
		delete from .IHLISTPRO where CODPRODUC = @Code
		delete from .INPRODPAT where IPRCODIGO = @Code
		delete from .IHPROINSU WHERE CODPRODUC = @Code
		
		--Se elimina de VIE
		delete from Inventory.TechnicalSheet where ATCId = @Id
		delete from Inventory.POSPathologies where MedicamentId = @Id
		delete from inventory.ATCAdministrationRoute where ATCId = @Id
		delete from Inventory.ATC where Id = @Id

		--Se retorna el ok
		select 0 as CodeMessage, 'Se eliminó correctamente' as Message

	End try
	Begin Catch

		--Se retorna el error
		select 999 as CodeMessage, ERROR_MESSAGE() as Message

	End Catch

END