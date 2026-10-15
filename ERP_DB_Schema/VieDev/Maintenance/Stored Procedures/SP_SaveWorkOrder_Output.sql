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