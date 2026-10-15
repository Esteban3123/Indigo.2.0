

-- ================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 27/06/2019
-- Description:	Procedimiento para eliminar el grupo farmacologico
-- ================================================
CREATE PROCEDURE [Inventory].[SP_DeletePharmacologicalGroup]
	@Id int
AS
BEGIN
		
	--Código del grupo farmacologico
	declare @Code varchar(20)

	Begin try

		--Se obtiene el código del grupo farmacologico
		select @Code = Code from Inventory.PharmacologicalGroup where Id = @Id

		--Se elimina de Crystal
		delete from .IHGRUFARM where CODGRUFAR = @Code

		--Se elimina de VIE
		delete from Inventory.PharmacologicalGroup where Id = @Id

		--Se retorna el ok
		select 0 as CodeMessage, 'Se eliminó correctamente' as Message

	End try
	Begin Catch

		--Se retorna el error
		select 999 as CodeMessage, ERROR_MESSAGE() as Message

	End Catch

END