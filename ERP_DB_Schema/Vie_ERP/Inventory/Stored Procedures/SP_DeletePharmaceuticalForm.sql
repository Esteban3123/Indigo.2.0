

-- ================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 27/06/2019
-- Description:	Procedimiento para eliminar la forma farmaceutica
-- ================================================
CREATE PROCEDURE [Inventory].[SP_DeletePharmaceuticalForm]
	@Id int
AS
BEGIN
		
	--Código de la forma farmaceutica
	declare @Code varchar(20)

	Begin try

		--Se obtiene el código de la forma farmaceutica
		select @Code = Code from Inventory.PharmaceuticalForm where Id = @Id

		--Se elimina de Crystal
		delete from .IHFORMEDI where CODFORMED = @Code

		--Se elimina de VIE
		delete from Inventory.PharmaceuticalForm where Id = @Id

		--Se retorna el ok
		select 0 as CodeMessage, 'Se eliminó correctamente' as Message

	End try
	Begin Catch

		--Se retorna el error
		select 999 as CodeMessage, ERROR_MESSAGE() as Message

	End Catch

END