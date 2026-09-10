-- ===============================================================================================================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-07-10
-- Description:	Procedimiento que se encarga de asignar las solicitudes de manera manual
-- ===============================================================================================================================
CREATE PROCEDURE [Authorization].[SP_AssignTraceabilityPaperwork]
	@TraceabilityPaperworkXml AS XML
AS
BEGIN
	SET NOCOUNT ON
	
	--Se declaran las variables
	DECLARE @TraceabilityPaperworkRows INT = 1,
			@TraceabilityPaperworkOrder INT = 0,
			--------------------------------------------------------------------------------------------------------------------
			@MessageResult VARCHAR(MAX) = '',
			@FullAllocation BIT = 1,
			--------------------------------------------------------------------------------------------------------------------
			@SubXml XML,
			@Code_Output INT = 0,
			@Message_Output VARCHAR(MAX)			

	--Tabla temporal de los detalles
	DECLARE @TraceabilityPaperworks TABLE
	(
		[RowHeaderId] [int] NOT NULL,
		[Id] [int] NOT NULL,
		[AdmissionNumber] [varchar](20) NULL,
		[Folio] [varchar](20) NULL,
		[ServiceCode] [varchar](20) NOT NULL,
		[Type] [tinyint] NOT NULL,
		[PatientCode] [varchar](20) NOT NULL,
		[CareCenterCode] [varchar](20) NOT NULL,
		[RequestDate] [datetime] NOT NULL,
		[RequestQuantity] [int] NOT NULL,
		[FunctionalUnitCode] [varchar](20) NOT NULL,
		[EntityId] [int] NULL,
		[EntityName] [varchar](50) NULL,
		[Status] [tinyint] NOT NULL,
		[CancellationReasonsId] [int] NULL,
		[CancellationReasonsObservations] [varchar](max) NULL,
		[CancellationUserCode] [varchar](20) NULL,
		[AssignUserCode] [varchar](20) NULL,
		[AuthorizationSourceId] [int] NULL,		
		[IsManual] [bit] NOT NULL,
		[CareGroupId] [int] NOT NULL,
		[HealthAdministratorId] [int] NULL,
		[ProfessionalCode] [varchar](20) NOT NULL,
		[CareCenterTargetCode] [varchar](20) NULL,
		[FunctionalUnitTargetId] [int] NULL,
		[ServiceId] [int] NOT NULL, 
		[ContractDescriptionId] [int] NULL,
		[Order] [int] NOT NULL
	)

	BEGIN TRY

		IF NOT EXISTS (SELECT 1 FROM [Authorization].SettingsAuthorization WHERE AutomaticAllocation = 0)
		BEGIN
			SELECT 999 AS CodeResult,
				   'La asignación no se encuentra parametrizada de manera Manual' AS MessageResult,
				   0 AS FullAllocation
			RETURN
		END

		/********************************** *************************************** **********************************/

		--Se obtiene los detalles que vienen en el xml
		INSERT INTO @TraceabilityPaperworks
			SELECT	t.x.value('RowHeaderId[1]','int') as RowHeaderId,
					t.x.value('Id[1]','int') as Id,
					IIF(t.x.value('AdmissionNumber[1]','varchar(20)') = '', null, t.x.value('AdmissionNumber[1]','varchar(20)')) as AdmissionNumber,
					IIF(t.x.value('Folio[1]','varchar(20)') = '', null, t.x.value('Folio[1]','varchar(20)')) as Folio,
					t.x.value('ServiceCode[1]','varchar(20)') as ServiceCode,
					t.x.value('Type[1]','tinyint') as Type,
					t.x.value('PatientCode[1]','varchar(20)') as PatientCode,
					t.x.value('CareCenterCode[1]','varchar(20)') as CareCenterCode,
					t.x.value('RequestDate[1]','datetime') as RequestDate,
					t.x.value('RequestQuantity[1]','int') as RequestQuantity,			
					t.x.value('FunctionalUnitCode[1]','varchar(20)') as FunctionalUnitCode,
					IIF(t.x.value('EntityId[1]','varchar(20)') = '', null, t.x.value('EntityId[1]','varchar(20)')) as EntityId,
					IIF(t.x.value('EntityName[1]','varchar(50)') = '', null, t.x.value('EntityName[1]','varchar(50)')) as EntityName,
					t.x.value('Status[1]','tinyint') as Status,
					IIF(t.x.value('CancellationReasonsId[1]','varchar(20)') = '', null, t.x.value('CancellationReasonsId[1]','varchar(20)')) as CancellationReasonsId,
					IIF(t.x.value('CancellationReasonsObservations[1]','varchar(max)') = '', null, t.x.value('CancellationReasonsObservations[1]','varchar(max)')) as CancellationReasonsObservations,
					IIF(t.x.value('CancellationUserCode[1]','varchar(20)') = '', null, t.x.value('CancellationUserCode[1]','varchar(20)')) as CancellationUserCode,
					IIF(t.x.value('AssignUserCode[1]','varchar(20)') = '', null, t.x.value('AssignUserCode[1]','varchar(20)')) as AssignUserCode,
					IIF(t.x.value('AuthorizationSourceId[1]','varchar(20)') = '', null, t.x.value('AuthorizationSourceId[1]','varchar(20)')) as AuthorizationSourceId,
					t.x.value('IsManual[1]','bit') as IsManual,			
					t.x.value('CareGroupId[1]','int') as CareGroupId,
					IIF(t.x.value('HealthAdministratorId[1]','varchar(20)') = '', null, t.x.value('HealthAdministratorId[1]','varchar(20)')) as HealthAdministratorId,
					t.x.value('ProfessionalCode[1]','varchar(20)') as ProfessionalCode,
					IIF(t.x.value('CareCenterTargetCode[1]','varchar(20)') = '', null, t.x.value('CareCenterTargetCode[1]','varchar(20)')) as CareCenterTargetCode,
					IIF(t.x.value('FunctionalUnitTargetId[1]','varchar(20)') = '', null, t.x.value('FunctionalUnitTargetId[1]','varchar(20)')) as FunctionalUnitTargetId,
					t.x.value('ServiceId[1]','int') as ServiceId,
					t.x.value('ContractDescriptionId[1]','int') as ContractDescriptionId,
					t.x.value('Order[1]','int') as [Order]
			FROM @TraceabilityPaperworkXml.nodes('/TraceabilityPaperwork') t(x)

		/********************************** *************************************** **********************************/

		WHILE @TraceabilityPaperworkRows > 0
		BEGIN
			SELECT TOP 1
				@TraceabilityPaperworkOrder = t.[Order]
			FROM @TraceabilityPaperworks t
			WHERE ISNULL(t.AssignUserCode, '') = ''
				AND t.[Order] > @TraceabilityPaperworkOrder				
			ORDER BY t.[Order]

			SET @TraceabilityPaperworkRows = @@ROWCOUNT
			IF @TraceabilityPaperworkRows = 0 OR @Code_Output <> 0
			BEGIN
				BREAK
			END

			/******************************** *************************************** ********************************/

			SELECT @SubXml = CONVERT
			(
				XML, 
				(
					SELECT	TraceabilityPaperwork.*
					FROM @TraceabilityPaperworks TraceabilityPaperwork
					WHERE ISNULL(TraceabilityPaperwork.AssignUserCode, '') = '' AND TraceabilityPaperwork.[Order] = @TraceabilityPaperworkOrder
					For xml AUTO,TYPE, ELEMENTS
				)
			)

			EXEC [Authorization].[SP_AutomaticAllocationTraceabilityPaperwork] @SubXml, @Code_Output OUT, @Message_Output OUT

			IF @Code_Output <> 0
			BEGIN
				SELECT	@FullAllocation = 0

				IF @Code_Output = 999
				BEGIN
					SELECT	999 AS CodeResult, 
							@Message_Output AS MessageResult,
							@FullAllocation AS FullAllocation
					RETURN
				END
			END

			SET @MessageResult = ISNULL(@MessageResult, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@MessageResult, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
		END

		/************************************ *************************************** ************************************/

		SELECT	0 AS CodeResult, 
				@MessageResult AS MessageResult,
				@FullAllocation AS FullAllocation
	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeResult,
			   'SP_AssignTraceabilityPaperwork: ' + ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS MessageResult,
			   0 AS FullAllocation
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que gestiona la asignación manual de solicitudes de trazabilidad (paperworks) en el módulo de autorizaciones. Recibe un listado de solicitudes en formato XML —cada una con datos del paciente, servicio, centro de atención, unidad funcional, profesional y contrato— y las asigna una por una a un usuario responsable, siempre que la configuración del sistema (SettingsAuthorization) indique que la asignación es de tipo manual y no automática. Para cada solicitud sin usuario asignado, invoca el procedimiento SP_AutomaticAllocationTraceabilityPaperwork para ejecutar la lógica de asignación individual, y al finalizar reporta si todas las solicitudes fueron asignadas exitosamente o si alguna quedó pendiente. Es el punto de entrada para que un autorizador o gestor de autorizaciones asigne manualmente trámites de autorización de servicios de salud.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_AssignTraceabilityPaperwork';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_AssignTraceabilityPaperwork';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Orquesta la asignación manual de solicitudes de trazabilidad (paperwork) recorriendo registro por registro un XML de entrada y delegando cada asignación al procedimiento de allocation, consolidando mensajes y un indicador de asignación completa.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_AssignTraceabilityPaperwork';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe seguir la estructura /TraceabilityPaperwork con los nodos esperados (RowHeaderId, Id, ServiceCode, Type, PatientCode, CareCenterCode, RequestDate, RequestQuantity, FunctionalUnitCode, Status, IsManual, CareGroupId, ProfessionalCode, ServiceId, ContractDescriptionId, Order, etc.); Debe existir al menos un registro en Authorization.SettingsAuthorization con AutomaticAllocation = 0 para habilitar la asignación manual; El SP dependiente Authorization.SP_AutomaticAllocationTraceabilityPaperwork debe existir y aceptar el sub-XML por registro', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_AssignTraceabilityPaperwork';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo procesa registros cuyo AssignUserCode esté vacío o NULL (los ya asignados no se reprocesan); Los registros se procesan secuencialmente respetando el campo [Order] ascendente; FullAllocation se mantiene en 1 únicamente si todas las invocaciones al SP de asignación interno retornan código 0; Si la parametrización del módulo no es manual (AutomaticAllocation distinto de 0), el procedimiento no realiza ninguna asignación; Cualquier excepción no controlada se traduce a CodeResult=999 con el mensaje y línea del error, y FullAllocation=0', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_AssignTraceabilityPaperwork';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Asignación manual de autorizaciones; Trazabilidad de solicitudes (TraceabilityPaperwork); Parametrización de asignación automática/manual; Solicitud de servicios (ServiceCode/ServiceId); Centro de atención y unidad funcional; Motivos de cancelación; Administrador de salud / EPS; Profesional asignado; Grupo de atención (CareGroup); Contrato (ContractDescription)', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_AssignTraceabilityPaperwork';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ResultSet: Cuando la parametrización no es manual, retorna {CodeResult=999, MessageResult=''La asignación no se encuentra parametrizada de manera Manual'', FullAllocation=0}; [RETURN_RESULT] ResultSet: Al terminar exitosamente, retorna {CodeResult=0, MessageResult=mensajes concatenados de cada asignación, FullAllocation=1 si todas las asignaciones internas devolvieron 0; 0 en caso contrario}; [RETURN_RESULT] ResultSet: Si el SP interno SP_AutomaticAllocationTraceabilityPaperwork devuelve Code_Output=999, corta el procesamiento y retorna {CodeResult=999, MessageResult=mensaje del SP interno, FullAllocation=0}; [RAISERROR] ResultSet: En el bloque CATCH, ante cualquier error no controlado retorna {CodeResult=999, MessageResult=''SP_AssignTraceabilityPaperwork: '' + ERROR_MESSAGE() + '' - Linea: '' + ERROR_LINE(), FullAllocation=0}', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_AssignTraceabilityPaperwork';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe registro en Authorization.SettingsAuthorization con AutomaticAllocation = 0 (es decir, la asignación NO está parametrizada como manual) → Retorna CodeResult=999, mensaje ''La asignación no se encuentra parametrizada de manera Manual'' y FullAllocation=0, abortando el proceso else Continúa con el procesamiento del XML y la asignación iterativa; si En el bucle, el SP interno devuelve @Code_Output <> 0 → Marca @FullAllocation = 0 (asignación parcial). Si además @Code_Output = 999, retorna inmediatamente con CodeResult=999 y el mensaje de salida else Concatena el mensaje al resultado acumulado y continúa con el siguiente registro; si No quedan más filas pendientes (AssignUserCode vacío) o @Code_Output <> 0 → Termina el bucle WHILE mediante BREAK', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_AssignTraceabilityPaperwork';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Authorization.SP_AutomaticAllocationTraceabilityPaperwork', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_AssignTraceabilityPaperwork';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.SettingsAuthorization', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_AssignTraceabilityPaperwork';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_AssignTraceabilityPaperwork';
-- GO
