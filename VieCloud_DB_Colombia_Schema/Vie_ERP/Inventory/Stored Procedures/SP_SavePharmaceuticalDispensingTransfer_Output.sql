-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-08-19
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar un traslado de dispensación por ingreso
-- =============================================
CREATE PROCEDURE [Inventory].[SP_SavePharmaceuticalDispensingTransfer_Output]
    @PharmaceuticalDispensingTransferXml AS XML,
	@CodeUser AS VARCHAR(20),
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
	DECLARE @Prefix VARCHAR(4),
			@OperatingUnitId INT,
			@DocumentDate DATETIME,
			@WarehouseId INT,
			@AdmissionNumber CHAR(10),
			@AdmissionNumberDestination CHAR(10),
			@Observation VARCHAR(MAX),
			@Status TINYINT,
			------------------------------
			@IdForm INT = 2187,
			@DocumentTypeControl INT = 18,
			------------------------------
			@Message VARCHAR(MAX),
			------------------------------
			@SubXml XML,
			@Code_Output VARCHAR(10),
			@Message_Output VARCHAR(MAX)

	--Tabla temporal de los detalles
	DECLARE @Detail TABLE
	(
		Id INT,
		PharmaceuticalDispensingTransferId INT,
		PharmaceuticalDispensingDetailBatchSerialId INT,
		Quantity INT,
		ChangeTracker VARCHAR(30)
	)

	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT	@Id = t.x.value('Id[1]','int'),
				@Prefix = t.x.value('Prefix[1]','varchar(4)'),
				@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
				@Code = t.x.value('Code[1]','varchar(20)'),
				@DocumentDate = t.x.value('DocumentDate[1]','datetime'),				
				@WarehouseId = t.x.value('WarehouseId[1]','int'),
				@AdmissionNumber = t.x.value('AdmissionNumber[1]','char(10)'),
				@AdmissionNumberDestination = t.x.value('AdmissionNumberDestination[1]','char(10)'),
				@Observation = t.x.value('Observation[1]','varchar(max)'),
				@Status = t.x.value('Status[1]','tinyint')
		FROM @PharmaceuticalDispensingTransferXml.nodes('/PharmaceuticalDispensingTransfer') t(x)

		IF EXISTS (SELECT 1 FROM Inventory.PharmaceuticalDispensingTransfer WHERE Id = @Id AND Status <> 1)
		BEGIN
			SELECT @CodeResult = 999, 
				   @MessageResult = 'El Traslado de Dispensación se encuentra en estado: ' + CASE Status 
																							   WHEN 2 THEN 'Confirmado'
																							   WHEN 3 THEN 'Anulado'
																							 END
			FROM Inventory.PharmaceuticalDispensingTransfer
			WHERE Id = @Id
			RETURN
		END
		
		IF @Status = 3
		BEGIN
			UPDATE [Inventory].[PharmaceuticalDispensingTransfer]
				SET [Status] = @Status,
					[ModificationUser] = @CodeUser,
					[ModificationDate] = [Common].[GETDATE](),
					[AnnulmentUser] = @CodeUser,
					[AnnulmentDate] = [Common].[GETDATE]()
			WHERE Id = @Id
		END
		ELSE
		BEGIN
			--Se obtiene los detalles que vienen en el xml
			INSERT INTO @Detail
				SELECT	t.x.value('Id[1]','int'),
						t.x.value('PharmaceuticalDispensingTransferId[1]','int'),
						t.x.value('PharmaceuticalDispensingDetailBatchSerialId[1]','int'),
						t.x.value('Quantity[1]','int'),
						t.x.value('ChangeTracker[1]','varchar(30)')
				FROM @PharmaceuticalDispensingTransferXml.nodes('/PharmaceuticalDispensingTransfer/PharmaceuticalDispensingTransferDetail') t(x)

			--se eliminan los detalles marcados para su eliminación
			DELETE pdtd
			FROM Inventory.PharmaceuticalDispensingTransferDetail pdtd
			JOIN @Detail d ON pdtd.Id = d.Id
			WHERE @Id = pdtd.PharmaceuticalDispensingTransferId AND d.ChangeTracker = 'Deleted'

			DELETE d FROM @Detail d WHERE d.ChangeTracker = 'Deleted'

			/*********************************************  VALIDACIONES *********************************************/

			IF @AdmissionNumber = @AdmissionNumberDestination
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'El ingreso origen debe ser diferente del ingreso destino.'
				RETURN
			END

			IF NOT EXISTS
			(
				SELECT 1
				FROM dbo.ADINGRESO ingo
				JOIN dbo.ADINGRESO ingd ON ingo.IPCODPACI = ingd.IPCODPACI
				WHERE ingo.NUMINGRES = @AdmissionNumber AND ingd.NUMINGRES = @AdmissionNumberDestination
			)
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'El paciente del ingreso origen debe ser el mismo del ingreso destino.'
				RETURN
			END

			IF NOT EXISTS (SELECT 1 FROM @Detail)
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'El Traslado de Dispensación por Ingreso no tiene detalles.'
				RETURN
			END

			IF EXISTS 
			(
				SELECT 1 
				FROM @Detail d
				LEFT JOIN Inventory.PharmaceuticalDispensingTransferDetail pdtd ON d.Id = pdtd.Id
				WHERE d.ChangeTracker <> 'Added' AND (@Id <> ISNULL(pdtd.PharmaceuticalDispensingTransferId, 0) OR ISNULL(d.PharmaceuticalDispensingDetailBatchSerialId, 0) <> ISNULL(pdtd.PharmaceuticalDispensingDetailBatchSerialId, 0))
			) 
			BEGIN
				SELECT @CodeResult = 999, 
						@MessageResult = 'La información asociada a los detalles del Traslado de Dispensación por Ingreso han sido alterados.'
				RETURN
			END

			IF EXISTS 
			(
				SELECT 1 
				FROM @Detail d
				JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs ON d.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id
				JOIN Inventory.PharmaceuticalDispensingDetail pdd ON pddbs.PharmaceuticalDispensingDetailId = pdd.Id
				WHERE pdd.WarehouseId <> @WarehouseId
			) 
			BEGIN
				SELECT @CodeResult = 999, 
						@MessageResult = 'Existen detalles de dispensación que no pertenecen al almacen seleccionado.'
				RETURN
			END

			/************************************  INSERTAR / ACTUALIZAR CABECERA ************************************/

			DECLARE @ConfirmationUser VARCHAR(20) = CASE WHEN @Status = 2 THEN @CodeUser ELSE NULL END
			DECLARE @ConfirmationDate DATETIME = CASE WHEN @Status = 2 THEN [Common].[GETDATE]() ELSE NULL END

			IF @Id = 0
			BEGIN
				--Si se esta insertando por primera vez se consulta la secuencia numerica
				DECLARE @IsManual BIT
				
				EXEC Common.SP_GetSequence 190, @IdForm, @OperatingUnitId, @Prefix, NULL, @IsManual OUT, @Code OUT, @Code_Output OUT, @Message_Output OUT

				IF @Code_Output <> 0
				BEGIN
					SELECT	@CodeResult = 999, 
							@MessageResult = REPLACE(@Message_Output, '{0}', 'Traslado de Dispensación por Ingreso')
					RETURN
				END

				--Se inserta la cabecera
				INSERT INTO [Inventory].[PharmaceuticalDispensingTransfer]
				(
					[Code],[OperatingUnitId],[DocumentDate],
					[WarehouseId],[AdmissionNumber],[AdmissionNumberDestination],[Observation],[Status],
					[CreationUser],[CreationDate],[ModificationUser],[ModificationDate],[ConfirmationUser],[ConfirmationDate]
				)
				SELECT	@Code,@OperatingUnitId,@DocumentDate,
						@WarehouseId,@AdmissionNumber,@AdmissionNumberDestination,@Observation,@Status,
						@CodeUser,[Common].[GETDATE](),@ConfirmationUser,@ConfirmationDate,@ConfirmationUser,@ConfirmationDate

				--Obtengo el id de la cabcera
				SET @Id = SCOPE_IDENTITY()
			END
			ELSE --Si se esta actualizando
			BEGIN
				UPDATE [Inventory].[PharmaceuticalDispensingTransfer]
					SET [Code] = @Code,
						[OperatingUnitId] = @OperatingUnitId,
						[DocumentDate] = @DocumentDate,
						[WarehouseId] = @WarehouseId,
						[AdmissionNumber] = @AdmissionNumber,
						[AdmissionNumberDestination] = @AdmissionNumberDestination,
						[Observation] = @Observation,
						[Status] = @Status,
						[ModificationUser] = @CodeUser,
						[ModificationDate] = [Common].[GETDATE](),
						[ConfirmationUser] = @ConfirmationUser,
						[ConfirmationDate] = @ConfirmationDate
				WHERE Id = @Id
			END

			/************************************* INSERTAR / ACTUALIZAR DETALLE *************************************/

			INSERT INTO Inventory.PharmaceuticalDispensingTransferDetail
			(
				PharmaceuticalDispensingTransferId, PharmaceuticalDispensingDetailBatchSerialId, Quantity
			)
			SELECT	@Id, d.PharmaceuticalDispensingDetailBatchSerialId, d.Quantity
			FROM @Detail d
			WHERE d.ChangeTracker = 'Added'

			UPDATE pdtd
				SET pdtd.Quantity = d.Quantity
			FROM @Detail d
			JOIN Inventory.PharmaceuticalDispensingTransferDetail pdtd ON d.Id = pdtd.Id
			WHERE pdtd.PharmaceuticalDispensingTransferId = @Id AND d.ChangeTracker <> 'Added'

			IF @Status = 2
			BEGIN
				/************************************** DEVOLUCION DISPENSACION **************************************/

				SELECT @SubXml = CONVERT
				(
					XML, 
					(
						SELECT *
						FROM 
						(
							SELECT	0 Id,
									pdt.OperatingUnitId,
									pdt.DocumentDate,
									pdt.WarehouseId,
									w.Code CodeNameWarehouse,
									1 IsDashBoard,
									pdt.AdmissionNumber,
									pdt.AdmissionNumberDestination,
									ing.IPCODPACI CodePatient,
									ing.CODCENATE CareCenterCode,
									ing.UFUCODIGO FunctionUnitCode,
									0 DevolutionOrigin,
									pdt.Observation,
									pdt.Status,
									'PharmaceuticalDispensingTransfer' EntityName,
									pdt.Code EntityCode,
									pdt.Id EntityId
							FROM Inventory.PharmaceuticalDispensingTransfer pdt
							JOIN Inventory.Warehouse w ON pdt.WarehouseId = w.Id
							JOIN dbo.ADINGRESO ing ON pdt.AdmissionNumber = ing.NUMINGRES
							WHERE pdt.Id = @Id
						) PharmaceuticalDispensingDevolution
						JOIN
						( 
							SELECT	0 Id,
									COALESCE(ins.Code, atc.Code, ip.Code) CodeProduct,
									pd.Code CodePharmaceuticalDispensing,
									pdd.Id PharmaceuticalDispensingDetailId,
									fu.Code FunctionUnitCode,
									hgqx.ID IDHCHOJAGASTOQX,
									ip.Id ProductId,
									COALESCE(ins.SupplieName, atc.Name, ip.Name) CodeNameProduct,
									pdd.OrderedHealthProfessionalCode,
									pddbs.Id PharmaceuticalDispensingDetailBatchSerialId,
									pdtd.Quantity,
									'Added' EntityState
							FROM Inventory.PharmaceuticalDispensingTransferDetail pdtd
							JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs ON pdtd.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id
							JOIN Inventory.PharmaceuticalDispensingDetail pdd ON pddbs.PharmaceuticalDispensingDetailId = pdd.Id
							JOIN Inventory.PharmaceuticalDispensing pd ON pdd.PharmaceuticalDispensingId = pd.Id
							JOIN Inventory.InventoryProduct ip ON pdd.ProductId = ip.Id
							JOIN Payroll.FunctionalUnit fu ON pdd.FunctionalUnitId = fu.Id
							LEFT JOIN Inventory.ATC atc ON ip.ATCId = atc.Id
							LEFT JOIN Inventory.InventorySupplie ins ON ip.SupplieId = ins.Id
							LEFT JOIN
							(
								SELECT hgqx.NUMINGRES, hgqxd.CODPRODUC, MAX(hgqx.ID) ID
								FROM dbo.HCHOJAGASTOQX hgqx
								JOIN dbo.HCHOJAGASTOQXD hgqxd ON hgqx.ID = hgqxd.IDHCHOJAGASTOQX
								WHERE (hgqxd.CANTIDADENTREGADA - hgqxd.CANTIDADGASTADA - hgqxd.CANTIDADACEPTADADEV) > 0
								GROUP BY hgqx.NUMINGRES, hgqxd.CODPRODUC
							) hgqx ON pd.AdmissionNumber = hgqx.NUMINGRES AND COALESCE(ins.Code, atc.Code, ip.Code) = hgqx.CODPRODUC
							WHERE pdtd.PharmaceuticalDispensingTransferId = @Id
						) PharmaceuticalDispensingDevolutionDetail ON PharmaceuticalDispensingDevolution.Id = PharmaceuticalDispensingDevolutionDetail.Id
						FOR XML AUTO,TYPE, ELEMENTS
					)
				)

				EXEC Inventory.SP_GeneratePharmaceuticalDevolution_Output @SubXml, '', @CodeUser, @Code_Output OUT, @Message_Output OUT, NULL, NULL

				IF ISNULL(@Code_Output, 999) <> '0'
				BEGIN
					SELECT	@CodeResult = 999, 
							@MessageResult = CONCAT('La Devolución de Dispensación no fue generada: ', ISNULL(@Message_Output, ''))
					RETURN 
				END

				SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)

				/*******************************************  DISPENSACION *******************************************/

				SELECT @SubXml = CONVERT
				(
					XML, 
					(
						SELECT *
						FROM 
						(
							SELECT	pdt.Id PharmaceuticalDispensingIdTmp,
									0 Id,
									pdt.OperatingUnitId,
									pdt.DocumentDate,
									1 AffectInventory,
									0 IsPharmaceuticalDispensing,
									1 DispensingIntegration,
									0 ConsecutivePharmacy,
									pdt.AdmissionNumberDestination AdmissionNumber,
									pdt.AdmissionNumber AdmissionNumberOrigin,
									ing.IPCODPACI CodePatient,
									ing.CODCENATE CareCenterCode,
									ing.UFUCODIGO FunctionUnitCode,
									pdt.Observation,
									pdt.Status,
									'PharmaceuticalDispensingTransfer' EntityName,
									pdt.Code EntityCode,
									pdt.Id EntityId
							FROM Inventory.PharmaceuticalDispensingTransfer pdt
							JOIN dbo.ADINGRESO ing ON pdt.AdmissionNumberDestination = ing.NUMINGRES
							WHERE pdt.Id = @Id
						) PharmaceuticalDispensing
						JOIN
						( 
							SELECT	pdtd.Id PharmaceuticalDispensingDetailIdTmp,
									0 Id,
									0 PharmaceuticalDispensingId,
									pdd.CareGroupId,
									pdd.HealthAdministratorId,
									pdd.ThirdPartyId,
									pdd.ProductId,
									pdd.WarehouseId,
									pdtd.Quantity,
									pdd.ServiceDate,
									pdd.FunctionalUnitId,
									fu.Code FunctionalUnitCode,
									pdd.OrderedHealthProfessionalCode,
									pdd.OrderedProfessionalSpecialty,
									pdd.OrderedHealthProfessionalThirdPartyId,
									pdd.LiquidationType,
									pdd.SurchargeApply,
									pdd.SalePrice,
									pdd.AverageCost,
									pdd.DiscountPercentage,
									pdd.DiscountValue,
									pdd.TotalSalesPrice,
									pdd.GrandTotalSalesPrice,
									COALESCE(ins.Code, atc.Code, ip.Code) CodeProduct,
									COALESCE(ins.SupplieName, atc.Name, ip.Name) NameProduct,
									pdtd.Quantity CantidadPendiente,
									pdtd.Quantity CantidadSolicitada,
									ip.ProductTypeId ProductType,
									pd.Code CodePharmaceuticalDispensing,
									pdd.Id PharmaceuticalDispensingDetailId,
									0 idProductoHeon,
									0 GuardaGastoQX,
									0 IdProgramacionQXPrincipal,
									0 Extramural,
									0 Custody,
									0 QuotationId,
									0 AuthorizationOutsourcedServicesId,
									pddbs.Id PharmaceuticalDispensingDetailBatchSerialId,
									'Added' ChangeTracker,
									2 ChangeTrackerState
							FROM Inventory.PharmaceuticalDispensingTransferDetail pdtd
							JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs ON pdtd.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id
							JOIN Inventory.PharmaceuticalDispensingDetail pdd ON pddbs.PharmaceuticalDispensingDetailId = pdd.Id
							JOIN Inventory.PharmaceuticalDispensing pd ON pdd.PharmaceuticalDispensingId = pd.Id
							JOIN Inventory.InventoryProduct ip ON pdd.ProductId = ip.Id
							JOIN Payroll.FunctionalUnit fu ON pdd.FunctionalUnitId = fu.Id
							LEFT JOIN Inventory.ATC atc ON ip.ATCId = atc.Id
							LEFT JOIN Inventory.InventorySupplie ins ON ip.SupplieId = ins.Id
							WHERE pdtd.PharmaceuticalDispensingTransferId = @Id
						) PharmaceuticalDispensingDetail ON PharmaceuticalDispensing.Id = PharmaceuticalDispensingDetail.Id
						JOIN
						(
							SELECT	pdtd.Id PharmaceuticalDispensingDetailIdTmp,
									0 Id,
									0 PharmaceuticalDispensingDetailId,
									pddbs.PhysicalInventoryId,
									pdtd.Quantity,
									pdtd.Quantity OutstandingQuantity,
									pddbs.PhysicalInventoryCustodyId,
									'Added' ChangeTracker
							FROM Inventory.PharmaceuticalDispensingTransferDetail pdtd
							JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs ON pdtd.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id
							JOIN Inventory.PharmaceuticalDispensingDetail pdd ON pddbs.PharmaceuticalDispensingDetailId = pdd.Id
							JOIN Inventory.PharmaceuticalDispensing pd ON pdd.PharmaceuticalDispensingId = pd.Id
							WHERE pdtd.PharmaceuticalDispensingTransferId = @Id
						) PharmaceuticalDispensingDetailBatchSerial ON PharmaceuticalDispensingDetail.PharmaceuticalDispensingDetailIdTmp = PharmaceuticalDispensingDetailBatchSerial.PharmaceuticalDispensingDetailIdTmp
						FOR XML AUTO,TYPE, ELEMENTS
					)
				)

				EXEC Inventory.SP_GeneratePharmaceuticalDispensing_Output @SubXml, '', @CodeUser, '', @Code_Output OUT, @Message_Output OUT, NULL, NULL, NULL

				IF ISNULL(@Code_Output, 999) <> '0'
				BEGIN
					SELECT	@CodeResult = 999, 
							@MessageResult = CONCAT('La Dispensación no fue generada: ', ISNULL(@Message_Output, ''))
					RETURN 
				END

				SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + ISNULL(@Message_Output, ''))
			END
		END

		IF @Status = 1
		BEGIN
			IF NOT EXISTS (SELECT 1 FROM Inventory.InventoryControlDocument WHERE DocumentType = @DocumentTypeControl AND DocumentNumber = @Code)
			BEGIN
				INSERT INTO Inventory.InventoryControlDocument (DocumentNumber, DocumentType, DocumentUser, DocumentDate)
				SELECT @Code, @DocumentTypeControl, @CodeUser, @DocumentDate
			END
		END
		ELSE
		BEGIN
			DELETE FROM Inventory.InventoryControlDocument WHERE DocumentType = @DocumentTypeControl AND DocumentNumber = @Code
		END

		SELECT @CodeResult = 0, 
			   @MessageResult = CASE @Status
				   WHEN 2 THEN CONCAT('Se guardó y confirmó el Traslado de Dispensación por Ingreso con código ', @Code)
				   WHEN 3 THEN CONCAT('Se anuló el Traslado de Dispensación por Ingreso con código ', @Code)
				   ELSE CONCAT('Se guardó el Traslado de Dispensación por Ingreso con código ', @Code)
			   END + IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10) + ISNULL(@Message, ''))
	END TRY
	BEGIN CATCH
		SELECT @CodeResult = 999, 
			   @MessageResult = 'SP_SavePharmaceuticalDispensingTransfer_Output: ' + ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que crea, actualiza, confirma o anula un traslado de dispensación farmacéutica entre ingresos de un mismo paciente. Recibe los datos del traslado en formato XML (cabecera y detalles de ítems por lote o serial) junto con el usuario que ejecuta la operación, y aplica las validaciones de negocio necesarias: que el ingreso origen y destino sean distintos, que ambos correspondan al mismo paciente (cédula en ADINGRESO), que los detalles pertenezcan a la bodega seleccionada y que el traslado no esté ya confirmado o anulado. Según el estado recibido, inserta un nuevo traslado (pendiente), lo actualiza, lo confirma registrando usuario y fecha de confirmación, o lo anula registrando usuario y fecha de anulación en la tabla PharmaceuticalDispensingTransfer; también gestiona la inserción, actualización y eliminación de los ítems en PharmaceuticalDispensingTransferDetail. Retorna parámetros de salida con el resultado de la operación, el identificador interno y el código del documento generado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SavePharmaceuticalDispensingTransfer_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SavePharmaceuticalDispensingTransfer_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Guarda, actualiza, confirma o anula un traslado de dispensación farmacéutica entre dos ingresos del mismo paciente, generando devolución y nueva dispensación al confirmar.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalDispensingTransfer_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El registro no debe estar en estado distinto de 1 (Pendiente); si está Confirmado(2) o Anulado(3) se rechaza.; El número de ingreso origen debe ser distinto del ingreso destino.; Los ingresos origen y destino deben corresponder al mismo paciente (mismo IPCODPACI en dbo.ADINGRESO).; Debe existir al menos un detalle no marcado como ''Deleted''.; Los detalles existentes (no ''Added'') no pueden tener alterado su PharmaceuticalDispensingTransferId ni PharmaceuticalDispensingDetailBatchSerialId.; Todos los detalles deben pertenecer al almacén (WarehouseId) seleccionado en la cabecera.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalDispensingTransfer_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Un traslado de dispensación solo puede modificarse mientras esté en estado 1 (Pendiente).; Origen y destino del traslado siempre son ingresos diferentes pero del mismo paciente.; Todos los detalles trasladados pertenecen al mismo almacén que la cabecera.; El tipo de documento de control usado para este traslado es siempre 18.; El consecutivo del documento se obtiene desde Common.SP_GetSequence con tipo 190 y formulario 2187.; Al confirmar (Status=2) siempre se genera primero la devolución farmacéutica y luego la dispensación destino.; InventoryControlDocument permanece solo cuando el traslado está en estado 1; al confirmar o anular se elimina.; Los detalles cuyo ChangeTracker es ''Deleted'' nunca persisten; los ''Added'' se insertan; el resto solo actualiza Quantity.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalDispensingTransfer_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Inventory.PharmaceuticalDispensingTransfer: Cuando @Status = 3 (anulación), actualiza Status, ModificationUser/Date y AnnulmentUser/Date.; [DELETE] Inventory.PharmaceuticalDispensingTransferDetail: Elimina los detalles cuyo ChangeTracker = ''Deleted'' y pertenezcan al traslado.; [INSERT] Inventory.PharmaceuticalDispensingTransfer: Cuando @Id = 0, obtiene secuencia vía Common.SP_GetSequence e inserta cabecera nueva; si Status=2 también fija ConfirmationUser/Date.; [UPDATE] Inventory.PharmaceuticalDispensingTransfer: Cuando @Id <> 0 y Status <> 3, actualiza la cabecera; si Status=2 establece ConfirmationUser/Date.; [INSERT] Inventory.PharmaceuticalDispensingTransferDetail: Inserta los detalles del XML cuyo ChangeTracker = ''Added''.; [UPDATE] Inventory.PharmaceuticalDispensingTransferDetail: Actualiza Quantity de los detalles existentes (ChangeTracker <> ''Added'') que pertenecen al traslado.; [INSERT] Inventory.InventoryControlDocument: Cuando @Status = 1 y no existe documento de control con DocumentType=18 y ese DocumentNumber, lo inserta.; [DELETE] Inventory.InventoryControlDocument: Cuando @Status <> 1, elimina el documento de control DocumentType=18 con ese DocumentNumber.; [RETURN_RESULT] -: Devuelve @CodeResult=0 y mensaje según Status (guardado / guardado y confirmado / anulado) con el código del traslado; ante error 999 con mensaje específico.; [RAISERROR] -: En CATCH retorna CodeResult=999 con ERROR_MESSAGE y línea del error.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalDispensingTransfer_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe el traslado con Status <> 1 → Retorna error 999 indicando estado Confirmado o Anulado, sin modificar nada.; si @Status = 3 → Solo actualiza cabecera marcándola como anulada (AnnulmentUser/Date).; si @Status <> 3 → Procesa detalles (delete/insert/update) previas validaciones de ingresos, paciente, almacén e integridad. else Salta el procesamiento de detalles.; si @Id = 0 → Solicita consecutivo con Common.SP_GetSequence (190, IdForm=2187) e inserta cabecera nueva. else Actualiza la cabecera existente.; si @Status = 2 (confirmación) → Genera XML y ejecuta SP_GeneratePharmaceuticalDevolution_Output (devolución sobre ingreso origen) y luego SP_GeneratePharmaceuticalDispensing_Output (nueva dispensación sobre ingreso destino).; si Resultado de SP_GeneratePharmaceuticalDevolution_Output o SP_GeneratePharmaceuticalDispensing_Output distinto de 0 → Retorna error 999 con el mensaje del SP invocado y aborta.; si @Status = 1 → Si no existe, inserta InventoryControlDocument (DocumentType=18) para el código. else Elimina InventoryControlDocument con DocumentType=18 y ese DocumentNumber.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalDispensingTransfer_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.SP_GetSequence; Inventory.SP_GeneratePharmaceuticalDevolution_Output; Inventory.SP_GeneratePharmaceuticalDispensing_Output; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalDispensingTransfer_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalDispensingTransfer_Output';
-- GO
