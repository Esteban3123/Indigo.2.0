

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Elimina completamente un principio activo (DCI - Denominación Común Internacional) del sistema, asegurando la integridad referencial en todas las tablas relacionadas. Primero obtiene el código del DCI para eliminarlo del sistema Crystal (tabla IHDCIMEDI), luego borra sus asociaciones con categorías ATC (DCIATCEntity), sus interacciones con otros medicamentos (DrugInteraction) y finalmente el registro maestro del principio activo (DCI). Aplica control de errores retornando un mensaje de éxito o el detalle del error según el resultado de la operación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_DeleteDCI';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_DeleteDCI';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Elimina un DCI (principio activo) y sus relaciones dependientes tanto en el módulo de inventario como en el sistema externo Crystal, devolviendo un resultado de éxito o error.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteDCI';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Inventory.DCI con el Id recibido para obtener su Code; si no existe, @Code queda NULL y la eliminación en IHDCIMEDI no afectará filas.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteDCI';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El borrado en cascada se realiza primero en tablas dependientes (IHDCIMEDI, DCIATCEntity, DrugInteraction) y al final en la tabla maestra Inventory.DCI.; La sincronización con Crystal (IHDCIMEDI) se realiza por Code del DCI, no por Id.; Sólo se eliminan interacciones donde el DCI sea el padre (ParentDCIId); no se borran las que lo tengan como hijo.; No se utiliza transacción explícita: cada DELETE se confirma de forma independiente y un fallo intermedio puede dejar datos huérfanos.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteDCI';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'DCI (Denominación Común Internacional); Principio activo; Clasificación ATC; Interacción medicamentosa; Integración con Crystal', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteDCI';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] IHDCIMEDI: Elimina en la tabla externa (Crystal) IHDCIMEDI las filas cuyo CODDCIMED coincida con el Code obtenido del DCI a borrar.; [DELETE] Inventory.DCIATCEntity: Elimina las relaciones DCI-ATC cuyo IdDCI corresponda al DCI eliminado.; [DELETE] Inventory.DrugInteraction: Elimina las interacciones medicamentosas donde ParentDCIId sea el DCI eliminado (sólo el lado padre).; [DELETE] Inventory.DCI: Elimina el registro maestro del DCI con Id = @Id tras borrar sus dependencias.; [RETURN_RESULT] -: Si todo se ejecuta sin error retorna CodeMessage=0 y Message=''Se eliminó correctamente''; si ocurre excepción retorna CodeMessage=999 y Message=ERROR_MESSAGE().', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteDCI';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Bloque TRY ejecuta sin errores → Retorna resultado de éxito (CodeMessage=0). else El CATCH retorna CodeMessage=999 con el mensaje de error capturado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteDCI';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.DCI', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteDCI';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteDCI';
-- GO
