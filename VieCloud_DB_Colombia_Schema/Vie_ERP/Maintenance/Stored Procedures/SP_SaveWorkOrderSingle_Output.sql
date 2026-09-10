-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-05-17
-- Description:	Procedimiento que se encarga de guardar, actualizar, anular, confirmar una orden de trabajo
-- =============================================
CREATE PROCEDURE [Maintenance].[SP_SaveWorkOrderSingle_Output]
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
	DECLARE @OperatingUnitId INT,
			@BranchOfficeId INT,
			@ProtocolId INT,
			@PhysicalAssetId INT,
			@MaintenanceResponsibleId INT,
			@RequestDate DATETIME,
			@ProgramDate DATETIME,
			@Description VARCHAR(MAX),
			@State TINYINT,
			@EntityId INT,
			@EntityCode VARCHAR(20),
			@EntityName VARCHAR(250),
			@ReversalReasonId INT,
			@DescriptionReversal VARCHAR(MAX),
			-------------------------------------------------------------------
			@Message VARCHAR(MAX),
			-------------------------------------------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)

	DECLARE @WorkOrderActivities AS TABLE
	(
		Id INT,
		WorkOrderId INT,
		ProtocolActivityId INT,
		Time INT, 
		Unit TINYINT,
		ChangeTracker VARCHAR(30)
	)

	DECLARE @WorkOrderConsumables AS TABLE
	(
		Id INT,
		WorkOrderId INT,
		ProtocolConsumableId INT,
		ChangeTracker VARCHAR(30)
	)

	DECLARE @WorkOrderTools AS TABLE
	(
		Id INT,
		WorkOrderId INT,
		ProtocolToolId INT,
		ChangeTracker VARCHAR(30)
	)

	DECLARE @WorkOrderSupplies AS TABLE
	(
		Id INT,
		WorkOrderId INT,
		ProtocolSupplyId INT,
		ChangeTracker VARCHAR(30)
	)

	/************************************************* ************* *************************************************/

	--Se obtienen los datos de la cabecera
	SELECT	@Id = t.x.value('Id[1]','int'),
			@Code = t.x.value('Consecutive[1]','varchar(20)'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),			
			@BranchOfficeId = t.x.value('BranchOfficeId[1]','int'),
			@ProtocolId = t.x.value('ProtocolId[1]','int'),
			@PhysicalAssetId = t.x.value('PhysicalAssetId[1]','int'),
			@MaintenanceResponsibleId = t.x.value('MaintenanceResponsibleId[1]','int'),
			@RequestDate = t.x.value('RequestDate[1]','datetime'),
			@ProgramDate = t.x.value('ProgramDate[1]','datetime'),
			@Description = t.x.value('Description[1]','varchar(max)'),
			@State = t.x.value('State[1]','tinyint'),
			@EntityId = t.x.value('EntityId[1]','int'),
			@EntityCode = t.x.value('EntityCode[1]','varchar(20)'),
			@EntityName = t.x.value('EntityName[1]','varchar(250)'),
			@ReversalReasonId = t.x.value('ReversalReasonId[1]','int'),
			@DescriptionReversal = t.x.value('DescriptionReversal[1]','varchar(max)')
	FROM @WorkOrderXml.nodes('/WorkOrder') t(x)

	/************************************************* ************* *************************************************/

	IF @State IN (4,5)
	BEGIN
		IF EXISTS (SELECT 1 FROM Maintenance.WorkOrder wo WITH (NOLOCK) WHERE wo.Id = @Id AND wo.State <> 2)
		BEGIN
			SELECT	@CodeResult = 999, 
					@MessageResult = CONCAT('La orden de trabajo ', Consecutive, ' se encuentra en estado: ', CASE State
																			WHEN 1 THEN 'Registrado'
																			WHEN 3 THEN 'Anulado'
																			WHEN 4 THEN 'Aprobado'
																			WHEN 5 THEN 'Rechazado'
																		END)
			FROM Maintenance.WorkOrder wo WITH (NOLOCK)
			WHERE wo.Id = @Id
			RETURN
		END
	END
	ELSE IF EXISTS (SELECT 1 FROM Maintenance.WorkOrder wo WITH (NOLOCK) WHERE wo.Id = @Id AND wo.State <> 1)
	BEGIN
		SELECT	@CodeResult = 999, 
				@MessageResult = CONCAT('La orden de trabajo ', Consecutive, ' se encuentra en estado: ', CASE State
																		WHEN 2 THEN 'Confirmado'
																		WHEN 3 THEN 'Anulado'
																		WHEN 4 THEN 'Aprobado'
																		WHEN 5 THEN 'Rechazado'
																	END)
		FROM Maintenance.WorkOrder wo WITH (NOLOCK)
		WHERE wo.Id = @Id
		RETURN
	END

	IF @State IN (3,4,5)
	BEGIN
		UPDATE Maintenance.WorkOrder
			SET State = @State,
				ModificationUser = @UserCode,
				ModificationDate = [Common].[GETDATE](),
				AnullateUser = @UserCode,
				AnullateDate = [Common].[GETDATE](),
				ReversalReasonId = @ReversalReasonId,
				DescriptionReversal = @DescriptionReversal
		WHERE Id = @Id
	END
	ELSE
	BEGIN
		INSERT INTO @WorkOrderActivities
			SELECT	t.x.value('Id[1]','int'),
					t.x.value('WorkOrderId[1]','int'),			
					t.x.value('ProtocolActivityId[1]','int'),
					t.x.value('Time[1]','int'),
					t.x.value('Unit[1]','tinyint'),
					t.x.value('ChangeTracker[1]','varchar(30)')
			FROM @WorkOrderXml.nodes('/WorkOrder/WorkOrderActivities') t(x)

		DELETE woa
		FROM Maintenance.WorkOrderActivities woa
		JOIN @WorkOrderActivities twoa ON woa.Id = twoa.Id
		WHERE @Id = woa.WorkOrderId AND twoa.ChangeTracker = 'Deleted'

		DELETE @WorkOrderActivities WHERE ChangeTracker = 'Deleted'

		-----------------------------------------------------------------------

		INSERT INTO @WorkOrderConsumables
			SELECT	t.x.value('Id[1]','int'),
					t.x.value('WorkOrderId[1]','int'),
					t.x.value('ProtocolConsumableId[1]','int'),
					t.x.value('ChangeTracker[1]','varchar(30)')
			FROM @WorkOrderXml.nodes('/WorkOrder/WorkOrderConsumables') t(x)

		DELETE woc
		FROM Maintenance.WorkOrderConsumables woc
		JOIN @WorkOrderConsumables twoc ON woc.Id = twoc.Id
		WHERE @Id = woc.WorkOrderId AND twoc.ChangeTracker = 'Deleted'

		DELETE @WorkOrderConsumables WHERE ChangeTracker = 'Deleted'

		-----------------------------------------------------------------------

		INSERT INTO @WorkOrderTools
			SELECT	t.x.value('Id[1]','int'),
					t.x.value('WorkOrderId[1]','int'),
					t.x.value('ProtocolToolId[1]','int'),
					t.x.value('ChangeTracker[1]','varchar(30)')
			FROM @WorkOrderXml.nodes('/WorkOrder/WorkOrderTools') t(x)

		DELETE wot
		FROM Maintenance.WorkOrderTools wot
		JOIN @WorkOrderTools twot ON wot.Id = twot.Id
		WHERE @Id = wot.WorkOrderId AND twot.ChangeTracker = 'Deleted'

		DELETE @WorkOrderTools WHERE ChangeTracker = 'Deleted'

		-----------------------------------------------------------------------

		INSERT INTO @WorkOrderSupplies
			SELECT	t.x.value('Id[1]','int'),
					t.x.value('WorkOrderId[1]','int'),
					t.x.value('ProtocolSupplyId[1]','int'),
					t.x.value('ChangeTracker[1]','varchar(30)')
			FROM @WorkOrderXml.nodes('/WorkOrder/WorkOrderSupplies') t(x)

		DELETE wos
		FROM Maintenance.WorkOrderSupplies wos
		JOIN @WorkOrderSupplies twos ON wos.Id = twos.Id
		WHERE @Id = wos.WorkOrderId AND twos.ChangeTracker = 'Deleted'

		DELETE @WorkOrderSupplies WHERE ChangeTracker = 'Deleted'

		/***********************************************  VALIDACIONES ***********************************************/

		IF @EntityId IS NOT NULL AND EXISTS 
		(
			SELECT 1 
			FROM Maintenance.WorkOrder wo WITH (NOLOCK) 
			WHERE @Id <> Id AND wo.EntityId = @EntityId AND wo.EntityName = @EntityName
		)
		BEGIN
			SELECT	@CodeResult = 999, 
					@MessageResult = CONCAT('Ya existe una Orden de Trabajo originada del ', CASE @EntityName
																			WHEN 'MaintenanceFailureRequest' THEN 'Reporte de Falla'
																			WHEN 'MaintenancePlanProgramated' THEN 'Plan de Mantenimiento'
																		END)
			RETURN
		END

		/**************************************  INSERTAR / ACTUALIZAR CABECERA **************************************/

		DECLARE @ConfirmationUser VARCHAR(20) = CASE WHEN @State = 2 THEN @UserCode ELSE NULL END
		DECLARE @ConfirmationDate DATETIME = CASE WHEN @State = 2 THEN [Common].[GETDATE]() ELSE NULL END

		IF @Id = 0
		BEGIN --Si se esta insertando por primera vez se consulta la secuencia numerica
			DECLARE @IsManual BIT
				
			EXEC Common.SP_GetSequence 140, 2134, @OperatingUnitId, NULL, NULL, @IsManual OUT, @Code OUT, @Code_Output OUT, @Message_Output OUT

			IF @Code_Output <> 0
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = REPLACE(@Message_Output, '{0}', 'orden de trabajo')
				RETURN
			END

			--Se inserta la cabecera
			INSERT INTO [Maintenance].[WorkOrder]
			(
				Consecutive, BranchOfficeId, ProtocolId, PhysicalAssetId, MaintenanceResponsibleId, RequestDate, ProgramDate,
				Description, State, CreationUser, CreationDate, ConfirmationUser, ConfirmationDate, EntityId, EntityCode, EntityName
			)
			SELECT	@Code,@BranchOfficeId,@ProtocolId,@PhysicalAssetId,@MaintenanceResponsibleId,@RequestDate,@ProgramDate,
					@Description,@State,@UserCode,[Common].[GETDATE](),@ConfirmationUser,@ConfirmationDate,@EntityId,@EntityCode,@EntityName

			--Obtengo el id de la cabcera
			SET @Id = SCOPE_IDENTITY()
		END
		ELSE --Si se esta actualizando
		BEGIN
			UPDATE [Maintenance].[WorkOrder]
				SET BranchOfficeId = @BranchOfficeId,
					ProtocolId = @ProtocolId,
					PhysicalAssetId = @PhysicalAssetId,
					MaintenanceResponsibleId = @MaintenanceResponsibleId,
					RequestDate = @RequestDate,
					ProgramDate = @ProgramDate,
					Description = @Description,
					State = @State,
					ModificationUser = @UserCode,
					ModificationDate = [Common].[GETDATE](),
					ConfirmationUser = @ConfirmationUser,
					ConfirmationDate = @ConfirmationDate
			WHERE Id = @Id
		END

		/*************************************** INSERTAR / ACTUALIZAR DETALLE ***************************************/

		INSERT INTO Maintenance.WorkOrderActivities
		(
			WorkOrderId, ProtocolActivityId, Time, Unit
		)
		SELECT	@Id, ProtocolActivityId, Time, Unit
		FROM @WorkOrderActivities
		WHERE ChangeTracker = 'Added'

		UPDATE woa
			SET woa.Time = twoa.Time,
				woa.Unit = twoa.Unit
		FROM Maintenance.WorkOrderActivities woa
		JOIN @WorkOrderActivities twoa ON woa.Id = twoa.Id
		WHERE @Id = woa.WorkOrderId AND twoa.ChangeTracker = 'Modified'

		-----------------------------------------------------------------------

		INSERT INTO Maintenance.WorkOrderConsumables
		(
			WorkOrderId, ProtocolConsumableId
		)
		SELECT	@Id, ProtocolConsumableId
		FROM @WorkOrderConsumables
		WHERE ChangeTracker = 'Added'

		-----------------------------------------------------------------------

		INSERT INTO Maintenance.WorkOrderTools
		(
			WorkOrderId, ProtocolToolId
		)
		SELECT	@Id, ProtocolToolId
		FROM @WorkOrderTools
		WHERE ChangeTracker = 'Added'

		-----------------------------------------------------------------------

		INSERT INTO Maintenance.WorkOrderSupplies
		(
			WorkOrderId, ProtocolSupplyId
		)
		SELECT	@Id, ProtocolSupplyId
		FROM @WorkOrderSupplies
		WHERE ChangeTracker = 'Added'

		/***********************************************  CONFIRMACION ***********************************************/

		IF @EntityName = 'MaintenancePlanProgramated'
		BEGIN
			UPDATE Maintenance.MaintenancePlanProgramated
				SET State = 2
			WHERE Id = @EntityId AND State = 1
		END
	END

	/************************************ *************************************** ************************************/

	---------------------------------------------------- RESULTADO ----------------------------------------------------

	SELECT	@CodeResult = 0, 
			@MessageResult = CASE @State
				   WHEN 2 THEN CONCAT('Se guardó y confirmó la Orden de Trabajo con código ', @Code)
				   WHEN 3 THEN CONCAT('Se anuló la Orden de Traslado con Trabajo ', @Code)
				   WHEN 4 THEN CONCAT('Se aprobó la Orden de Traslado con Trabajo ', @Code)
				   WHEN 5 THEN CONCAT('Se rechazó la Orden de Traslado con Trabajo ', @Code)
				   ELSE CONCAT('Se guardó la Orden de Trabajo con código ', @Code)
			   END + IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10) + ISNULL(@Message, ''))
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que permite crear, actualizar, anular, aprobar o rechazar una orden de trabajo de mantenimiento, recibiendo los datos completos (cabecera, actividades, consumibles, herramientas e insumos) en formato XML. Gestiona el ciclo de vida completo de la orden de trabajo sobre la tabla WorkOrder y sus detalles relacionados (WorkOrderActivities, WorkOrderConsumables), aplicando validaciones de estado para impedir transiciones inválidas (por ejemplo, no se puede anular una orden ya confirmada o aprobada). Retorna como parámetros de salida el resultado de la operación, el identificador interno y el número consecutivo de la orden, siendo el punto central de control para el módulo de mantenimiento de activos físicos.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'PROCEDURE', @level1name = N'SP_SaveWorkOrderSingle_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'PROCEDURE', @level1name = N'SP_SaveWorkOrderSingle_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Gestiona el ciclo de vida de una orden de trabajo de mantenimiento (crear, actualizar, confirmar, anular, aprobar, rechazar) junto con su detalle de actividades, consumibles, herramientas e insumos, validando estados y unicidad por entidad origen.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_SaveWorkOrderSingle_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe contener el nodo /WorkOrder con la cabecera y opcionalmente nodos hijos WorkOrderActivities, WorkOrderConsumables, WorkOrderTools y WorkOrderSupplies.; Para aprobar o rechazar (State 4 o 5) la orden debe existir y encontrarse en estado Confirmado (State=2).; Para cualquier otra operación sobre orden existente (anular, modificar) la orden debe estar en estado Registrado (State=1).; Cada ítem de detalle debe traer ChangeTracker con valores ''Added'', ''Modified'' o ''Deleted''.; Si EntityId no es nulo, EntityName debe corresponder a ''MaintenanceFailureRequest'' o ''MaintenancePlanProgramated'' para que el mensaje de duplicidad sea legible.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_SaveWorkOrderSingle_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Maintenance.WorkOrder: Cuando @State IN (3,4,5) se actualiza State, ModificationUser/Date, AnullateUser/Date, ReversalReasonId y DescriptionReversal de la orden indicada por @Id.; [INSERT] Maintenance.WorkOrder: Si @Id = 0 se inserta una nueva cabecera con el consecutivo obtenido vía Common.SP_GetSequence (tipo 140, formulario 2134); si @State = 2 se setean ConfirmationUser y ConfirmationDate, en caso contrario quedan nulos.; [UPDATE] Maintenance.WorkOrder: Si @Id <> 0 y @State NO está en (3,4,5), se actualizan datos de cabecera (sucursal, protocolo, activo, responsable, fechas, descripción, estado) y se registran ConfirmationUser/Date solo cuando @State = 2.; [DELETE] Maintenance.WorkOrderActivities: Se eliminan las actividades cuyo Id viene en el XML con ChangeTracker=''Deleted'' y pertenecen a la orden @Id.; [INSERT] Maintenance.WorkOrderActivities: Se insertan las actividades del XML marcadas con ChangeTracker=''Added'' asociándolas al WorkOrderId @Id.; [UPDATE] Maintenance.WorkOrderActivities: Se actualiza Time y Unit de las actividades del XML marcadas con ChangeTracker=''Modified'' que pertenecen a la orden @Id.; [DELETE] Maintenance.WorkOrderConsumables: Se eliminan los consumibles del XML marcados con ChangeTracker=''Deleted'' que pertenecen a la orden @Id.; [INSERT] Maintenance.WorkOrderConsumables: Se insertan los consumibles del XML marcados con ChangeTracker=''Added'' asociándolos al WorkOrderId @Id.; [DELETE] Maintenance.WorkOrderTools: Se eliminan las herramientas del XML marcadas con ChangeTracker=''Deleted'' que pertenecen a la orden @Id.; [INSERT] Maintenance.WorkOrderTools: Se insertan las herramientas del XML marcadas con ChangeTracker=''Added'' asociándolas al WorkOrderId @Id.; [DELETE] Maintenance.WorkOrderSupplies: Se eliminan los suministros del XML marcados con ChangeTracker=''Deleted'' que pertenecen a la orden @Id.; [INSERT] Maintenance.WorkOrderSupplies: Se insertan los suministros del XML marcados con ChangeTracker=''Added'' asociándolos al WorkOrderId @Id.; [UPDATE] Maintenance.MaintenancePlanProgramated: Cuando EntityName=''MaintenancePlanProgramated'', el plan programado origen (Id=@EntityId) que esté en State=1 pasa a State=2.; [RETURN_RESULT] @CodeResult/@MessageResult: Devuelve @CodeResult=999 con mensaje de error si la orden no está en el estado esperado, si el secuenciador falla o si ya existe otra orden con el mismo EntityId/EntityName; en éxito devuelve @CodeResult=0 con mensaje específico según el estado final (guardado, confirmado, anulado, aprobado o rechazado) y el código asignado.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_SaveWorkOrderSingle_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_SaveWorkOrderSingle_Output';
-- GO
