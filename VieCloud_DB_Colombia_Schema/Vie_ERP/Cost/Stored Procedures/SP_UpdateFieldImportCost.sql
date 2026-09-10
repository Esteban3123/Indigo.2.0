-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 2/12/2016
-- Description:	Procedimiento que se encarga de actualizar el campo de import en la tabla CostLogisticsProductionCenterRecordDetail
-- =============================================
CREATE PROCEDURE [Cost].[SP_UpdateFieldImportCost] 
	@ObjectXml as Xml
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeMessage INT,
			@Message VARCHAR(MAX)

	EXEC [Cost].[SP_UpdateFieldImportCost_Output] 
		@ObjectXml,
		-----------------------------------------
		@CodeMessage OUTPUT, 
		@Message OUTPUT

	SELECT	@CodeMessage as CodeMessage, 
			@Message as Message
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que actualiza el campo de importación (import) en el detalle de registros de costos logísticos por centro de producción (tabla CostLogisticsProductionCenterRecordDetail). Recibe los datos de actualización en formato XML y delega la lógica principal al procedimiento SP_UpdateFieldImportCost_Output, del cual obtiene un código de respuesta y un mensaje de resultado. Se utiliza en el módulo de costos para registrar o corregir el valor de importación asociado a un ítem de costo logístico-productivo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_UpdateFieldImportCost';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_UpdateFieldImportCost';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que delega la actualización del campo de import en el detalle de costos logísticos a un procedimiento interno y retorna código y mensaje de resultado.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateFieldImportCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe recibir un XML con la información de entrada para el procedimiento delegado.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateFieldImportCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La lógica de actualización se delega íntegramente al procedimiento _Output; este wrapper solo expone el resultado vía SELECT.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateFieldImportCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Costos; Centro de producción logística; Importación de costos', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateFieldImportCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (result set): Tras ejecutar el procedimiento delegado, retorna en un result set el CodeMessage y Message obtenidos como OUTPUT.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateFieldImportCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Cost.SP_UpdateFieldImportCost_Output', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateFieldImportCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateFieldImportCost';
-- GO
