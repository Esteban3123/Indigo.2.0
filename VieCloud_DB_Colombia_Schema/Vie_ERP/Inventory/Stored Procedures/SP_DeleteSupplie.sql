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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Elimina un insumo o dispositivo médico del sistema de inventario de forma sincronizada en dos repositorios: primero lo borra del catálogo maestro de productos farmacéuticos y dispositivos médicos (IHLISTPRO en la base legacy Crystal) usando el código del producto, y luego lo elimina del catálogo de insumos del módulo Inventory de Vie Cloud. Recibe como parámetro el identificador interno del insumo (@Id) y retorna un mensaje de éxito o el error ocurrido. Garantiza consistencia entre ambos sistemas al momento de dar de baja un insumo del inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_DeleteSupplie';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_DeleteSupplie';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Elimina un insumo tanto del catálogo local de inventario como del catálogo externo Crystal, devolviendo un resultado tabular con código y mensaje de éxito o error.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteSupplie';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Inventory.InventorySupplie con el Id recibido para poder obtener el Code que se usa al eliminar en IHLISTPRO; si no existe, @Code queda NULL y el DELETE en IHLISTPRO no afecta filas.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteSupplie';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La eliminación se realiza en dos repositorios sincronizados: el catálogo local (Inventory.InventorySupplie) y el catálogo externo Crystal (IHLISTPRO), usando el código del insumo como llave de enlace.; Cualquier excepción se captura y se devuelve como resultado tabular con CodeMessage=999 en lugar de propagarse.; En éxito devuelve CodeMessage=0; en error devuelve CodeMessage=999 con el mensaje de ERROR_MESSAGE().', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteSupplie';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'insumo; medicamento; inventario; catálogo de productos', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteSupplie';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] IHLISTPRO: Elimina de IHLISTPRO (catálogo Crystal) la fila cuyo CODPRODUC coincide con el Code obtenido previamente desde Inventory.InventorySupplie para el Id recibido.; [DELETE] Inventory.InventorySupplie: Elimina del catálogo local de insumos la fila con Id = @Id, después de haber eliminado el registro espejo en Crystal.; [RETURN_RESULT] (resultset): Si todo el TRY se ejecuta sin error, retorna un resultset con CodeMessage=0 y Message=''Se eliminó correctamente''.; [RETURN_RESULT] (resultset): Si ocurre cualquier excepción dentro del TRY, el CATCH retorna un resultset con CodeMessage=999 y Message=ERROR_MESSAGE().', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteSupplie';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventorySupplie', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteSupplie';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteSupplie';
-- GO
