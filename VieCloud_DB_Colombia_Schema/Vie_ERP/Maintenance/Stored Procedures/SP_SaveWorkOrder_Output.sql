-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-05-17
-- Description:	Procedimiento que se encarga de guardar, actualizar, anular, confirmar una orden de trabajo
-- =============================================
CREATE PROCEDURE [Maintenance].[SP_SaveWorkOrder_Output]
    @WorkOrderXml AS XML,
	@UserCode AS VARCHAR(20),
	------------------------------------------------------
	@CodeResult Int OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT,
	------------------------------------------------------
	@Id INT OUTPUT,
	@Code VARCHAR(20) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	--Se declaran las variables para obtener la cabecera
	DECLARE @Rows INT = 1, 
			@RowId INT = 0, 
			-------------------------------------------------------------------
			@EntityName VARCHAR(250),
			@ProcessType TINYINT,
			-------------------------------------------------------------------
			@Message VARCHAR(MAX),
			-------------------------------------------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)

	DECLARE @WorkOrders AS TABLE
	(
		RowId INT,
		Id INT,
		Consecutive VARCHAR(20),
		OperatingUnitId INT,
		BranchOfficeId INT,
		ProtocolId INT,
		PhysicalAssetId INT,
		MaintenanceResponsibleId INT,
		RequestDate DATETIME,
		ProgramDate DATETIME,
		Description VARCHAR(MAX),
		State TINYINT,		
		EntityId INT,
		EntityCode VARCHAR(20),
		EntityName VARCHAR(250),
		ProcessType TINYINT,
		ReversalReasonId INT,
		DescriptionReversal VARCHAR(MAX)
	)

	DECLARE @WorkOrderActivities AS TABLE
	(
		WorkOrderRowId INT,
		Id INT,
		WorkOrderId INT,
		ProtocolActivityId INT,
		Time INT, 
		Unit TINYINT,
		ChangeTracker VARCHAR(30)
	)

	DECLARE @WorkOrderConsumables AS TABLE
	(
		WorkOrderRowId INT,
		Id INT,
		WorkOrderId INT,
		ProtocolConsumableId INT,
		ChangeTracker VARCHAR(30)
	)

	DECLARE @WorkOrderTools AS TABLE
	(
		WorkOrderRowId INT,
		Id INT,
		WorkOrderId INT,
		ProtocolToolId INT,
		ChangeTracker VARCHAR(30)
	)

	DECLARE @WorkOrderSupplies AS TABLE
	(
		WorkOrderRowId INT,
		Id INT,
		WorkOrderId INT,
		ProtocolSupplyId INT,
		ChangeTracker VARCHAR(30)
	)
	
	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY

		INSERT INTO @WorkOrders
			SELECT	t.x.value('RowId[1]','int'),
					t.x.value('Id[1]','int'),
					t.x.value('Consecutive[1]','varchar(20)'),
					t.x.value('OperatingUnitId[1]','int'),			
					t.x.value('BranchOfficeId[1]','int'),
					t.x.value('ProtocolId[1]','int'),
					t.x.value('PhysicalAssetId[1]','int'),
					t.x.value('MaintenanceResponsibleId[1]','int'),
					TRY_PARSE(t.x.value('RequestDate[1]','varchar(100)') AS DATETIME USING 'es-CO'),
					TRY_PARSE(t.x.value('ProgramDate[1]','varchar(100)') AS DATETIME USING 'es-CO'),
					t.x.value('Description[1]','varchar(max)'),
					t.x.value('State[1]','tinyint'),
					t.x.value('EntityId[1]','int'),
					t.x.value('EntityCode[1]','varchar(20)'),
					t.x.value('EntityName[1]','varchar(250)'),
					t.x.value('ProcessType[1]','tinyint'),
					t.x.value('ReversalReasonId[1]','int'),
					t.x.value('DescriptionReversal[1]','varchar(max)')
			FROM @WorkOrderXml.nodes('/WorkOrder') t(x)

		INSERT INTO @WorkOrderActivities
			SELECT	t.x.value('WorkOrderRowId[1]','int'),
					t.x.value('Id[1]','int'),
					t.x.value('WorkOrderId[1]','int'),			
					t.x.value('ProtocolActivityId[1]','int'),
					t.x.value('Time[1]','int'),
					t.x.value('Unit[1]','tinyint'),
					t.x.value('ChangeTracker[1]','varchar(30)')
			FROM @WorkOrderXml.nodes('/WorkOrder/WorkOrderActivities') t(x)

		INSERT INTO @WorkOrderConsumables
			SELECT	t.x.value('WorkOrderRowId[1]','int'),
					t.x.value('Id[1]','int'),
					t.x.value('WorkOrderId[1]','int'),
					t.x.value('ProtocolConsumableId[1]','int'),
					t.x.value('ChangeTracker[1]','varchar(30)')
			FROM @WorkOrderXml.nodes('/WorkOrder/WorkOrderConsumables') t(x)

		INSERT INTO @WorkOrderTools
			SELECT	t.x.value('WorkOrderRowId[1]','int'),
					t.x.value('Id[1]','int'),
					t.x.value('WorkOrderId[1]','int'),
					t.x.value('ProtocolToolId[1]','int'),
					t.x.value('ChangeTracker[1]','varchar(30)')
			FROM @WorkOrderXml.nodes('/WorkOrder/WorkOrderTools') t(x)

		INSERT INTO @WorkOrderSupplies
			SELECT	t.x.value('WorkOrderRowId[1]','int'),
					t.x.value('Id[1]','int'),
					t.x.value('WorkOrderId[1]','int'),
					t.x.value('ProtocolSupplyId[1]','int'),
					t.x.value('ChangeTracker[1]','varchar(30)')
			FROM @WorkOrderXml.nodes('/WorkOrder/WorkOrderSupplies') t(x)

		---------------------------------------------------------------------------------------------------------------

		WHILE @Rows > 0
		BEGIN
			SELECT TOP 1
				@RowId = RowId,
				---------------------------------------------------------------				
				@EntityName = EntityName,
				@ProcessType = ProcessType
			FROM @WorkOrders
			WHERE RowId > @RowId 
			ORDER BY RowId

			SET @Rows = @@RowCount
			IF @Rows = 0 
				BREAK

			-----------------------------------------------------------------------------------------------------------

			SELECT @SubXml = CONVERT
			(
				XML, 
				(
					SELECT	*,
							(               
								SELECT  woa.Id,               
										woa.WorkOrderId,
										woa.ProtocolActivityId,
										woa.Time,
										woa.Unit,
										woa.ChangeTracker
								FROM @WorkOrderActivities woa
								WHERE woa.WorkOrderRowId = @RowId
								FOR XML PATH('WorkOrderActivities'), TYPE
							),
							(               
								SELECT  woa.Id,               
										woa.WorkOrderId,
										woa.ProtocolConsumableId,
										woa.ChangeTracker
								FROM @WorkOrderConsumables woa
								WHERE woa.WorkOrderRowId = @RowId
								FOR XML PATH('WorkOrderConsumables'), TYPE
							),
							(               
								SELECT  woa.Id,               
										woa.WorkOrderId,
										woa.ProtocolToolId,
										woa.ChangeTracker
								FROM @WorkOrderTools woa
								WHERE woa.WorkOrderRowId = @RowId
								FOR XML PATH('WorkOrderTools'), TYPE
							),
							(               
								SELECT  woa.Id,               
										woa.WorkOrderId,
										woa.ProtocolSupplyId,
										woa.ChangeTracker
								FROM @WorkOrderSupplies woa
								WHERE woa.WorkOrderRowId = @RowId
								FOR XML PATH('WorkOrderSupplies'), TYPE
							)							
					FROM @WorkOrders WorkOrder
					WHERE WorkOrder.RowId = @RowId 
					FOR XML AUTO,TYPE, ELEMENTS
				)
			)

			EXEC [Maintenance].[SP_SaveWorkOrderSingle_Output]	@SubXml, 
														@UserCode, 
														--Salidas
														@Code_Output OUTPUT, 
														@Message_Output OUTPUT,
														@Id OUTPUT, 
														@Code OUTPUT
			
			IF @Code_Output <> 0
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = ISNULL(@Message_Output, 'No se puedo guardar la orden de trabajo.')
				RETURN
			END

			-----------------------------------------------------------------------------------------------------------

			SET @Message = ISNULL(@Message, '') + IIF(ISNULL(@Message_Output, '') = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
		END

		---------------------------------------------------------------------------------------------------------------

		SELECT	@CodeResult = 0,
				@MessageResult = @Message
	END TRY
	BEGIN CATCH
		SELECT	@CodeResult = 999, 
				@MessageResult = CONCAT('Error Guardando las Ordenes de Trabajo: ', ERROR_MESSAGE(),' - Linea: ', ERROR_LINE())
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que gestiona el ciclo de vida completo de las órdenes de trabajo de mantenimiento: permite crear, actualizar, anular y confirmar órdenes de trabajo recibidas en formato XML. Procesa en lote una o varias órdenes junto con sus recursos asociados (actividades, consumibles, herramientas e insumos), y delega el procesamiento individual de cada orden al procedimiento SP_SaveWorkOrderSingle_Output. Retorna el identificador y código de la orden procesada, así como un código y mensaje de resultado para informar el éxito o falla de la operación al sistema llamador.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'PROCEDURE', @level1name = N'SP_SaveWorkOrder_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'PROCEDURE', @level1name = N'SP_SaveWorkOrder_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Orquesta el guardado/actualización/anulación/confirmación de una o varias órdenes de trabajo de mantenimiento recibidas en XML, delegando cada una a SP_SaveWorkOrderSingle_Output y consolidando códigos y mensajes de resultado.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_SaveWorkOrder_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@WorkOrderXml debe respetar la estructura /WorkOrder con nodos hijos WorkOrderActivities, WorkOrderConsumables, WorkOrderTools y WorkOrderSupplies, y cada hijo debe traer WorkOrderRowId que enlace con el RowId de la cabecera.; Las fechas RequestDate y ProgramDate deben venir en formato compatible con la cultura ''es-CO'' para que TRY_PARSE no las descarte como NULL.; @UserCode debe corresponder a un usuario válido reconocido por SP_SaveWorkOrderSingle_Output.; El sub-procedimiento Maintenance.SP_SaveWorkOrderSingle_Output debe existir y aceptar la firma (@SubXml, @UserCode, @Code_Output, @Message_Output, @Id, @Code).', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_SaveWorkOrder_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El XML se descompone en una cabecera (/WorkOrder) y cuatro colecciones hijas: WorkOrderActivities, WorkOrderConsumables, WorkOrderTools y WorkOrderSupplies, vinculadas por WorkOrderRowId.; Las fechas RequestDate y ProgramDate se interpretan con cultura ''es-CO'' usando TRY_PARSE (valores no parseables quedan NULL).; Cada orden se procesa de forma independiente reconstruyendo un sub-XML con sus colecciones filtradas por RowId antes de delegar la persistencia.; Ante el primer error de una orden (Code_Output<>0) se aborta todo el lote; no se continúa con las siguientes órdenes.; Resultado exitoso siempre devuelve @CodeResult=0; cualquier fallo (validado o capturado) devuelve @CodeResult=999.; Los parámetros de salida @Id y @Code reflejan los valores de la última orden procesada por SP_SaveWorkOrderSingle_Output.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_SaveWorkOrder_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de trabajo; Mantenimiento; Protocolo de mantenimiento; Activo físico; Responsable de mantenimiento; Actividades del protocolo; Consumibles; Herramientas; Insumos; Unidad operativa; Sucursal (BranchOffice); Reversión/anulación (ReversalReason)', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_SaveWorkOrder_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Maintenance.SP_SaveWorkOrderSingle_Output: Por cada fila de @WorkOrders se construye un sub-XML con su cabecera y sus colecciones hijas filtradas por RowId y se invoca SP_SaveWorkOrderSingle_Output, propagando @Id y @Code como salidas.; [RETURN_RESULT] OUTPUT @CodeResult/@MessageResult: Si SP_SaveWorkOrderSingle_Output devuelve Code_Output<>0, se retorna @CodeResult=999 y @MessageResult=ISNULL(@Message_Output,''No se puedo guardar la orden de trabajo.'') y termina el SP.; [RETURN_RESULT] OUTPUT @CodeResult/@MessageResult: Si todas las órdenes se procesan sin error, retorna @CodeResult=0 y @MessageResult con la concatenación (CRLF) de los mensajes acumulados.; [RAISERROR] OUTPUT @CodeResult/@MessageResult: En el bloque CATCH retorna @CodeResult=999 y @MessageResult=''Error Guardando las Ordenes de Trabajo: ''+ERROR_MESSAGE()+'' - Linea: ''+ERROR_LINE().', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_SaveWorkOrder_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Code_Output <> 0 tras invocar SP_SaveWorkOrderSingle_Output → Asigna @CodeResult=999, @MessageResult con el mensaje retornado (o ''No se puedo guardar la orden de trabajo.'') y RETURN inmediato, abortando el procesamiento de las órdenes restantes. else Concatena @Message_Output al acumulador @Message separado por CRLF y continúa con la siguiente fila.; si Bloque CATCH ante excepción en el flujo TRY → Devuelve @CodeResult=999 y @MessageResult con ''Error Guardando las Ordenes de Trabajo: '' + ERROR_MESSAGE() + '' - Linea: '' + ERROR_LINE().; si Iteración WHILE @Rows>0 sobre @WorkOrders ordenadas por RowId ascendente → Procesa cada orden de trabajo individualmente; finaliza cuando no hay más filas con RowId > @RowId (SET @Rows=@@ROWCOUNT; IF @Rows=0 BREAK).', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_SaveWorkOrder_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Maintenance.SP_SaveWorkOrderSingle_Output', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_SaveWorkOrder_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_SaveWorkOrder_Output';
-- GO
