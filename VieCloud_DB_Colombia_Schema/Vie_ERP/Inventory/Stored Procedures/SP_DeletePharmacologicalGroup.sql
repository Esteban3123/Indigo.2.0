

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Elimina un grupo farmacológico (familia terapéutica de medicamentos, como antibióticos, analgésicos o antihipertensivos) tanto del catálogo moderno de inventario de farmacia (Inventory.PharmacologicalGroup) como de la tabla legada del sistema Crystal (IHGRUFARM), garantizando consistencia entre ambas bases. Recibe el identificador interno del grupo, obtiene su código, y ejecuta la eliminación en los dos sistemas de forma sincronizada. Devuelve un mensaje de éxito o el error ocurrido si la operación falla.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_DeletePharmacologicalGroup';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_DeletePharmacologicalGroup';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Eliminar un grupo farmacológico tanto del catálogo maestro como de su tabla espejo en el sistema legado, devolviendo un código de resultado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeletePharmacologicalGroup';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Inventory.PharmacologicalGroup con el Id recibido para que la eliminación en el sistema legado (IHGRUFARM) afecte filas; de lo contrario @Code quedará en NULL y el DELETE en IHGRUFARM no eliminará registros.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeletePharmacologicalGroup';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La eliminación se realiza en dos repositorios sincronizados: la tabla maestra Inventory.PharmacologicalGroup (VIE) y la tabla legacy IHGRUFARM (Crystal), usando el Code como llave en el legado y el Id en el maestro.; El procedimiento siempre devuelve exactamente un result set con las columnas CodeMessage y Message, tanto en éxito como en error.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeletePharmacologicalGroup';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Grupo farmacológico; Inventario de farmacia', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeletePharmacologicalGroup';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] IHGRUFARM: Se elimina la fila de IHGRUFARM cuyo CODGRUFAR coincide con el Code obtenido desde Inventory.PharmacologicalGroup para el Id recibido (sincronización con sistema Crystal/legado).; [DELETE] Inventory.PharmacologicalGroup: Se elimina la fila de Inventory.PharmacologicalGroup cuyo Id coincide con el parámetro recibido.; [RETURN_RESULT] (result set): En éxito retorna CodeMessage=0 y Message=''Se eliminó correctamente''; en error retorna CodeMessage=999 y Message=ERROR_MESSAGE().', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeletePharmacologicalGroup';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Ocurre una excepción durante la obtención del código o las eliminaciones (BEGIN CATCH) → Se retorna un result set con CodeMessage=999 y el mensaje de ERROR_MESSAGE() else Se retorna un result set con CodeMessage=0 y mensaje ''Se eliminó correctamente''', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeletePharmacologicalGroup';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmacologicalGroup', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeletePharmacologicalGroup';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_DeletePharmacologicalGroup';
-- GO
