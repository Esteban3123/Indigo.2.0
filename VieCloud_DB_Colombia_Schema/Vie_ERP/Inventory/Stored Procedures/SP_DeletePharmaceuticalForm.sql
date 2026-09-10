

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Elimina una forma farmacéutica (por ejemplo: tableta, cápsula, jarabe, ampolla) del sistema, identificándola por su ID interno. Primero obtiene el código asociado en el catálogo de formas farmacéuticas de Indigo VIE y luego borra el registro en ambas bases: el catálogo legado Crystal (IHFORMEDI, donde se gestiona la forma de administración del medicamento) y el catálogo propio de inventario (PharmaceuticalForm). Retorna un mensaje de éxito o el error ocurrido durante la operación. Se usa para mantener sincronizados los catálogos de presentaciones de medicamentos entre los dos sistemas cuando una forma farmacéutica queda obsoleta o fue ingresada por error.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_DeletePharmaceuticalForm';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_DeletePharmaceuticalForm';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Elimina una forma farmacéutica tanto del catálogo maestro de inventario como de la tabla legacy de Crystal, devolviendo un mensaje de resultado o de error controlado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeletePharmaceuticalForm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Inventory.PharmaceuticalForm con el Id recibido para poder recuperar su Code y propagar la eliminación a IHFORMEDI.; La tabla legacy IHFORMEDI (esquema Crystal) debe ser accesible desde la sesión actual.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeletePharmaceuticalForm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La eliminación se propaga a dos sistemas: tabla legacy IHFORMEDI (Crystal) por código y tabla maestra Inventory.PharmaceuticalForm por Id.; El borrado en IHFORMEDI usa el Code obtenido previamente desde Inventory.PharmaceuticalForm; si el Id no existe, @Code queda NULL y el DELETE en IHFORMEDI no afecta filas.; Cualquier excepción es capturada y devuelta como resultado tabular con CodeMessage=999, evitando que el error se propague al cliente.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeletePharmaceuticalForm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'forma farmacéutica; inventario de farmacia; sincronización con Crystal (IHFORMEDI)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeletePharmaceuticalForm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] IHFORMEDI: Elimina de IHFORMEDI todas las filas cuyo CODFORMED coincida con el Code de la forma farmacéutica identificada por el Id recibido.; [DELETE] Inventory.PharmaceuticalForm: Elimina de Inventory.PharmaceuticalForm la fila cuyo Id coincide con el parámetro recibido.; [RETURN_RESULT] (resultset): Si el flujo finaliza sin excepciones, retorna un resultset con CodeMessage=0 y mensaje ''Se eliminó correctamente''.; [RETURN_RESULT] (resultset): Si ocurre una excepción dentro del TRY, retorna un resultset con CodeMessage=999 y el texto de ERROR_MESSAGE().', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeletePharmaceuticalForm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalForm', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeletePharmaceuticalForm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeletePharmaceuticalForm';
-- GO
