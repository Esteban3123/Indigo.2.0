

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Elimina un nivel de riesgo del inventario identificado por su ID interno. Primero obtiene el código del nivel de riesgo desde el catálogo VIE (InventoryRiskLevel) y luego lo borra en cascada tanto en la tabla legacy Crystal (INIVERIES) como en la tabla propia del módulo de inventario, garantizando consistencia entre ambos sistemas. Se usa en la gestión farmacéutica y de almacén cuando se requiere dar de baja una clasificación de riesgo (alto, medio, bajo) que ya no aplica. Retorna un mensaje de éxito o el error correspondiente si la operación falla.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_DeleteRiskLevels';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_DeleteRiskLevels';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Elimina un nivel de riesgo de inventario propagando la baja al catálogo equivalente del sistema Crystal mediante el código asociado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteRiskLevels';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Inventory.InventoryRiskLevel con el Id recibido para poder obtener su Code; de lo contrario el DELETE en INIVERIES no afectará filas.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteRiskLevels';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La eliminación se propaga a dos catálogos: el local (Inventory.InventoryRiskLevel) y el de Crystal (INIVERIES) usando el Code como llave de correspondencia.; El procedimiento siempre devuelve un resultset con estructura (CodeMessage, Message), ya sea en éxito o error.; No se utiliza transacción explícita: si falla el DELETE de InventoryRiskLevel después de borrar en INIVERIES, las eliminaciones quedan desincronizadas.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteRiskLevels';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nivel de riesgo de inventario; Sincronización con sistema Crystal; Catálogo farmacéutico', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteRiskLevels';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] Inventory.InventoryRiskLevel: Elimina la fila cuyo Id coincide con el parámetro de entrada.; [DELETE] INIVERIES: Elimina del catálogo Crystal (INIVERIES) la fila cuyo CODNIVRIE coincide con el Code obtenido previamente desde Inventory.InventoryRiskLevel.; [RETURN_RESULT] -: Si todo se ejecuta sin error, retorna un resultset con CodeMessage=0 y Message=''Se eliminó correctamente''.; [RETURN_RESULT] -: Si ocurre una excepción, retorna CodeMessage=999 y Message=ERROR_MESSAGE() dentro del bloque CATCH.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteRiskLevels';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TRY exitoso → Retorna CodeMessage=0 con mensaje de éxito else En CATCH retorna CodeMessage=999 con el mensaje de error capturado', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteRiskLevels';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryRiskLevel; INIVERIES', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteRiskLevels';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteRiskLevels';
-- GO
