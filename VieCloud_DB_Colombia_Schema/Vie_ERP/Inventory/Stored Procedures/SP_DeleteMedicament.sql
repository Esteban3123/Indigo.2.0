

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Elimina completamente un medicamento del sistema a partir de su identificador interno (Id ATC). Borra todos los registros relacionados al medicamento en las tablas del catálogo de productos farmacéuticos (IHLISTPRO), restricciones por edad y diagnóstico (INPRODPAT), relación con insumos (IHPROINSU), fichas técnicas, patologías POS, vías de administración y finalmente el registro maestro en el catálogo ATC. Se usa para dar de baja un medicamento o insumo de salud garantizando la eliminación en cascada de toda su información asociada en el inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_DeleteMedicament';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_DeleteMedicament';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Elimina en cascada un medicamento (ATC) del catálogo VIE y de las tablas legadas de Crystal, junto con sus relaciones (ficha técnica, patologías POS y vías de administración).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteMedicament';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El Id recibido debe existir en Inventory.ATC para obtener el Code; si no existe, @Code queda NULL y los DELETE en tablas legadas (IHLISTPRO, INPRODPAT, IHPROINSU) no afectarán filas.; Las tablas externas .IHLISTPRO, .INPRODPAT, .IHPROINSU (sistema Crystal) deben ser accesibles desde la conexión actual.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteMedicament';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El borrado se realiza siempre en un orden fijo: primero las tablas legadas Crystal por Code, luego las dependencias en VIE, y por último la fila maestra en Inventory.ATC.; No se utiliza una transacción explícita: cada DELETE se confirma de forma independiente, por lo que un fallo intermedio puede dejar el modelo en estado parcialmente borrado.; Los errores nunca se propagan al cliente como excepción; siempre se devuelven como resultset con CodeMessage=999.; La sincronización entre el sistema VIE (Inventory.ATC) y el sistema legado Crystal se realiza usando el Code del ATC como llave de correspondencia.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteMedicament';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Medicamento; Clasificación ATC; Ficha técnica de medicamento; Patologías POS; Vía de administración; Integración con sistema legado Crystal', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteMedicament';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] Inventory.ATC: Elimina la fila maestra del medicamento donde Id = @Id, ejecutado al final tras borrar sus dependencias.; [DELETE] Inventory.TechnicalSheet: Elimina todas las fichas técnicas asociadas al medicamento (ATCId = @Id).; [DELETE] Inventory.POSPathologies: Elimina las relaciones de patologías POS donde MedicamentId = @Id.; [DELETE] inventory.ATCAdministrationRoute: Elimina las vías de administración asociadas al medicamento (ATCId = @Id).; [DELETE] IHLISTPRO: Elimina del catálogo Crystal IHLISTPRO el producto cuyo CODPRODUC coincide con el Code del ATC.; [DELETE] INPRODPAT: Elimina de INPRODPAT (Crystal) los registros con IPRCODIGO = Code del ATC.; [DELETE] IHPROINSU: Elimina de IHPROINSU (Crystal) los registros con CODPRODUC = Code del ATC.; [RETURN_RESULT] -: Si la operación finaliza sin excepción, retorna un resultset con CodeMessage=0 y Message=''Se eliminó correctamente''.; [RETURN_RESULT] -: Si ocurre cualquier excepción dentro del TRY, el CATCH retorna un resultset con CodeMessage=999 y Message=ERROR_MESSAGE().', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteMedicament';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Bloque TRY ejecutado sin error → Retorna CodeMessage=0 y mensaje de éxito else El bloque CATCH retorna CodeMessage=999 con el mensaje de error capturado por ERROR_MESSAGE()', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteMedicament';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.ATC', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteMedicament';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteMedicament';
-- GO
