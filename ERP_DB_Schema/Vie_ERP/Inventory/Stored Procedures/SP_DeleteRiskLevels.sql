

-- ================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 27/06/2019
-- Description:	Procedimiento para eliminar los niveles de riesgo
-- ================================================
CREATE PROCEDURE [Inventory].[SP_DeleteRiskLevels]
	@Id int
AS
BEGIN
		
	--Código del nivel de riesgo
	declare @Code varchar(20)

	Begin try

		--Se obtiene el código del nivel de riesgo
		select @Code = Code from Inventory.InventoryRiskLevel where Id = @Id

		--Se elimina de Crystal
		delete from .INIVERIES where CODNIVRIE = @Code

		--Se elimina de VIE
		delete from Inventory.InventoryRiskLevel where Id = @Id

		--Se retorna el ok
		select 0 as CodeMessage, 'Se eliminó correctamente' as Message

	End try
	Begin Catch

		--Se retorna el error
		select 999 as CodeMessage, ERROR_MESSAGE() as Message

	End Catch

END