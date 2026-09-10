

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Elimina una unidad de medida del catálogo de inventario identificándola por su ID interno. Primero obtiene el código de la unidad en el sistema Vie Cloud (InventoryMeasurementUnit) y luego la borra en paralelo tanto en la tabla legada Crystal (INUNIMEDI por CODUNIMED) como en la tabla moderna de inventario, garantizando consistencia entre ambos sistemas. Devuelve un mensaje de éxito o el detalle del error si la operación falla. Se usa en la gestión del catálogo de unidades de medida (unidad, caja, frasco, miligramo, etc.) cuando se requiere dar de baja una unidad que ya no está vigente.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_DeleteMeasurementUnit';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_DeleteMeasurementUnit';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Eliminar una unidad de medida del catálogo de inventario y replicar su baja en la tabla espejo de Crystal, devolviendo un código de resultado estandarizado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteMeasurementUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Inventory.InventoryMeasurementUnit cuyo Id coincida con el identificador recibido para poder obtener su Code; de lo contrario el DELETE en INUNIMEDI no eliminará registros.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteMeasurementUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La eliminación se replica en dos sistemas: la tabla espejo de Crystal (INUNIMEDI por CODUNIMED) y el catálogo VIE (Inventory.InventoryMeasurementUnit por Id), manteniendo consistencia entre ambos.; Ante cualquier excepción se retorna CodeMessage=999 con ERROR_MESSAGE(); en éxito se retorna CodeMessage=0 con mensaje ''Se eliminó correctamente''.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteMeasurementUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Unidad de medida; Inventario; Sincronización con Crystal (INUNIMEDI)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteMeasurementUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] INUNIMEDI: Elimina la fila de Crystal donde CODUNIMED coincide con el Code obtenido previamente desde Inventory.InventoryMeasurementUnit para el Id recibido.; [DELETE] Inventory.InventoryMeasurementUnit: Tras borrar en Crystal, elimina la unidad de medida cuyo Id coincide con el parámetro de entrada.; [RETURN_RESULT] (resultset): Si la transacción TRY finaliza sin error, retorna 0 y ''Se eliminó correctamente''; si ocurre excepción, retorna 999 y el mensaje de ERROR_MESSAGE().', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteMeasurementUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryMeasurementUnit', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteMeasurementUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteMeasurementUnit';
-- GO
