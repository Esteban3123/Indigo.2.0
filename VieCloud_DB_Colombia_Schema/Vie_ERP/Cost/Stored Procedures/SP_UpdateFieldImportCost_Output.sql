-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-04-29
-- Description:	Procedimiento que se encarga de actualizar el campo de import en la tabla CostLogisticsProductionCenterRecordDetail
-- =============================================
CREATE PROCEDURE [Cost].[SP_UpdateFieldImportCost_Output]
	@ObjectXml AS XML,
	------------------------------------------------------
	@CodeMessageResult Int OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	--Tabla para almacenar los items del listado que viene en el xml y poder actualizar el campo en la tabla
	DECLARE @TableIds TABLE (Id INT)

	BEGIN TRY
		--Se obtiene los detalles que vienen en el xml
		INSERT INTO @TableIds
			SELECT	t.x.value('Id[1]','int') as Id
			FROM @ObjectXml.nodes('/Data') t(x)

		--Actualizo los registros si hay datos
		IF EXISTS (SELECT 1 from @TableIds)
		BEGIN
			UPDATE lpcrd 
				SET lpcrd.Import = 1
			FROM Cost.CostLogisticsProductionCenterRecordDetail lpcrd 
			JOIN @TableIds ti ON ti.Id = lpcrd.Id
		END

		SELECT @CodeMessageResult = 0, 
			   @MessageResult = 'Se actualizó correctamente'
	END TRY
	BEGIN CATCH
		SELECT @CodeMessageResult = 999, 
			   @MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que marca como importados uno o varios registros de detalle logístico por centro de producción en el módulo de costos. Recibe un XML con una lista de identificadores de registros y actualiza el campo ''Import'' a verdadero (1) en la tabla CostLogisticsProductionCenterRecordDetail para cada uno de ellos. Retorna un código y mensaje de resultado indicando si la operación fue exitosa o si ocurrió algún error. Se utiliza para confirmar que ciertos insumos o productos asociados a registros logísticos de centros de producción ya han sido procesados o importados.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_UpdateFieldImportCost_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_UpdateFieldImportCost_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Marca como importados (Import=1) los detalles logísticos de centro de producción cuyos identificadores se reciben en un XML.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateFieldImportCost_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe tener nodos /Data con el elemento Id de tipo entero; Deben existir registros en CostLogisticsProductionCenterRecordDetail cuyos Id coincidan con los enviados', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateFieldImportCost_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se modifica la columna Import, asignándola siempre al valor 1 (nunca la desmarca); Si el XML no aporta Ids, no se altera ninguna fila; Los errores se capturan y se devuelven como código 999 sin propagar la excepción', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateFieldImportCost_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Costos; Centro de producción; Registro logístico; Importación de detalles', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateFieldImportCost_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Cost.CostLogisticsProductionCenterRecordDetail: Cuando el XML contiene al menos un Id (EXISTS sobre la tabla temporal), se establece Import = 1 en los detalles cuyo Id coincide con los recibidos; [RETURN_RESULT] N/A: Si la ejecución es exitosa, retorna CodeMessageResult=0 y mensaje ''Se actualizó correctamente''; ante excepción retorna 999 con ERROR_MESSAGE y línea', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateFieldImportCost_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EXISTS (SELECT 1 FROM @TableIds) → Ejecuta el UPDATE marcando Import=1 en los detalles correspondientes else No realiza ninguna actualización', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateFieldImportCost_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostLogisticsProductionCenterRecordDetail', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateFieldImportCost_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateFieldImportCost_Output';
-- GO
