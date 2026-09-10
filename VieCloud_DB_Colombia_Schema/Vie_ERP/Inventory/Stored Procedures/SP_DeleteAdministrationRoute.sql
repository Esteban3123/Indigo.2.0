

-- ================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 27/06/2019
-- Description:	Procedimiento para eliminar la via de administracion
-- ================================================
CREATE PROCEDURE [Inventory].[SP_DeleteAdministrationRoute]
	@Id int
AS
BEGIN
		
	--Código de la via de administracion
	declare @Code varchar(20)
	DECLARE @CodeCrystal varchar(20)

	Begin try

		--Se obtiene el código de la via de administracion
		select @Code = Code, @CodeCrystal = CrystalAdministrationRoute from Inventory.AdministrationRoute where Id = @Id

		--Se elimina de Crystal
		delete from .HCVIAADMI where CODVIAADM = @CodeCrystal

		--Se elimina de VIE
		delete from Inventory.AdministrationRoute where Id = @Id

		--Se retorna el ok
		select 0 as CodeMessage, 'Se eliminó correctamente' as Message

	End try
	Begin Catch

		--Se retorna el error
		select 999 as CodeMessage, ERROR_MESSAGE() as Message

	End Catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Elimina una vía de administración de medicamentos del sistema, identificada por su ID interno. Primero obtiene el código correspondiente en el catálogo histórico Crystal (HCVIAADMI) y lo elimina de ese catálogo legado, luego elimina el registro del catálogo propio de farmacia (Inventory.AdministrationRoute). Retorna un mensaje de éxito o de error según el resultado de la operación. Se usa para mantener sincronizados ambos catálogos de vías de administración (oral, intravenosa, intramuscular, etc.) cuando se da de baja una vía que ya no debe estar disponible en prescripción o historia clínica.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_DeleteAdministrationRoute';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_DeleteAdministrationRoute';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Elimina una vía de administración tanto del catálogo interno como de su equivalente en el sistema Crystal, devolviendo un código de resultado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteAdministrationRoute';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Inventory.AdministrationRoute con el Id recibido para poder obtener su Code y CrystalAdministrationRoute; de lo contrario el borrado en HCVIAADMI no afectará filas.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteAdministrationRoute';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La eliminación se replica en ambos sistemas: catálogo principal (Inventory.AdministrationRoute) y catálogo Crystal (HCVIAADMI), manteniendo sincronía.; Cualquier excepción durante el borrado se captura y retorna como código 999 con el mensaje de error, sin propagar el fallo.; Una operación exitosa retorna CodeMessage=0; un fallo retorna CodeMessage=999.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteAdministrationRoute';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Vía de administración; Catálogo de farmacia; Integración Crystal', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteAdministrationRoute';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] HCVIAADMI: Se elimina la fila de HCVIAADMI cuyo CODVIAADM coincida con el CrystalAdministrationRoute obtenido de la vía de administración identificada.; [DELETE] Inventory.AdministrationRoute: Se elimina la vía de administración cuyo Id coincide con el parámetro recibido.; [RETURN_RESULT] (resultset): Si ambas eliminaciones se ejecutan sin excepción, se devuelve un resultset con CodeMessage=0 y mensaje de éxito.; [RETURN_RESULT] (resultset): Si ocurre cualquier error en el bloque TRY, el CATCH retorna un resultset con CodeMessage=999 y ERROR_MESSAGE().', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteAdministrationRoute';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.AdministrationRoute', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteAdministrationRoute';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteAdministrationRoute';
-- GO
