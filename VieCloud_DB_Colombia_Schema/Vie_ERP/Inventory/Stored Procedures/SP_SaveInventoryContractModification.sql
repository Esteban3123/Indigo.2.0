-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-21
-- Description:	Procedimiento el cual se encarga de guardar una modificación de un contrato
-- =============================================
CREATE PROCEDURE [Inventory].[SP_SaveInventoryContractModification]
	@InventoryContractModificationXML AS XML,
	@UserCode AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	--Se declaran las variables para obtener la cabecera
	DECLARE @Id INT,
			@OperatingUnitId INT,
			@Code VARCHAR(20),
			@DocumentDate DATETIME,
			@ContractId INT,
			@ModificationType TINYINT,
			@EndDate DATETIME,
			@Value DECIMAL(20,4),
			@Description VARCHAR(MAX),
			@Status TINYINT,
			@BudgetaryValidityId INT,
			------------------------------
			@IdForm INT = 2117,
			@DocumentTypeControl INT = 17,
			------------------------------
			@Message VARCHAR(MAX),
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)

	--Tabla para los detalles de disponibilidades
	DECLARE @TableInventoryContractModificationAvailability TABLE
	(
		Id INT, 
		InventoryContractModificationId INT, 
		AvailabilityDetailId INT, 
		Value NUMERIC(20,4), 
		IsDelete BIT
	)

	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT 
			@Id = t.x.value('Id[1]','int'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
			@Code = t.x.value('Code[1]','varchar(20)'),
			@DocumentDate = t.x.value('DocumentDate[1]','datetime'),
			@ContractId = t.x.value('ContractId[1]','int'),
			@ModificationType = t.x.value('ModificationType[1]','tinyint'),
			@EndDate = t.x.value('EndDate[1]','datetime'),
			@Value = t.x.value('Value[1]','decimal(20,4)'),
			@Description = t.x.value('Description[1]','varchar(max)'),
			@Status = t.x.value('Status[1]','tinyint'),
			@BudgetaryValidityId = t.x.value('BudgetaryValidityId[1]','int')
		FROM @InventoryContractModificationXML.nodes('/InventoryContractModification') t(x)

		IF EXISTS (SELECT 1 FROM Inventory.InventoryContractModification om WHERE om.Id = @Id AND om.Status <> 1)
		BEGIN
			SELECT	999 AS CodeResult, 
					'El Otro si de Contrato se encuentra en estado: ' + IIF(om.Status = 2, 'Confirmado', 'Anulado') AS MessageResult, 
					0 as Id, '' as Code
			FROM Inventory.InventoryContractModification om 
			WHERE om.Id = @Id
			RETURN
		END
		
		IF @Status = 3
		BEGIN
			UPDATE [Inventory].[InventoryContractModification]
				SET [Status] = @Status,
					[ModificationUser] = @UserCode,
					[ModificationDate] = [Common].[GETDATE](),
					[AnnulmentUser] = @UserCode,
					[AnnulmentDate] = [Common].[GETDATE]()
			WHERE Id = @Id
		END
		ELSE
		BEGIN
			--Se obtienen los detalles del xml para los productos
			INSERT INTO @TableInventoryContractModificationAvailability
				SELECT 
					t.x.value('Id[1]','INT') as Id,
					t.x.value('InventoryContractModificationId[1]','INT') as InventoryContractModificationId,
					t.x.value('AvailabilityDetailId[1]','INT') as AvailabilityDetailId,
					REPLACE(t.x.value('Value[1]','VARCHAR(20)'), ',', '.') as Value,
					t.x.value('IsDelete[1]','BIT')
				FROM @InventoryContractModificationXML.nodes('/InventoryContractModification/InventoryContractModificationAvailability') t(x)

			--Se eliminan los registros de detalles de disponibilidades
			DELETE ica FROM Inventory.InventoryContractAvailability ica JOIN @TableInventoryContractModificationAvailability tica ON ica.Id = tica.Id WHERE ica.InventoryContractId = @Id AND tica.IsDelete = 1
			DELETE FROM @TableInventoryContractModificationAvailability WHERE IsDelete = 1

			/*************************************VALIDACIONES************************************/

			IF EXISTS (SELECT 1 FROM @TableInventoryContractModificationAvailability WHERE IsDelete = 0)
			BEGIN --Si hay disponibilidades asociadas al contrato
				--Se valida que todos los detalles pertenezcan a la misma vigencia
				IF EXISTS
				(
					SELECT 1
					FROM Budget.Availability a
					JOIN Budget.AvailabilityDetail ad ON a.Id = ad.AvailabilityId
					JOIN @TableInventoryContractModificationAvailability tica ON ad.Id = tica.AvailabilityDetailId
					WHERE tica.IsDelete = 0 AND ISNULL(@BudgetaryValidityId, 0) <> a.BudgetaryValidityId
				)
				BEGIN
					SELECT	999 AS CodeResult, 
							'Los detalles corresponden a más de una vigencia presupuestal.' AS MessageResult, 
							0 as Id, '' as Code
					FROM Inventory.InventoryContractModification om 
					WHERE om.Id = @Id
					RETURN
				END

				IF EXISTS
				(
					SELECT 1
					FROM @TableInventoryContractModificationAvailability 
					WHERE IsDelete = 0
					GROUP BY IsDelete
					HAVING SUM(Value) <> ROUND(@Value, 0)
				)
				BEGIN
					SELECT	999 AS CodeResult, 
							'La sumatoria de los rubros no es igual al valor del contrato.' AS MessageResult, 
							0 as Id, '' as Code
					FROM Inventory.InventoryContractModification om 
					WHERE om.Id = @Id
					RETURN
				END

				--Se valida que el saldo de las disponibilidades no sea menor al valor a ejecutar
				IF EXISTS 
				(
					SELECT 1
					FROM @TableInventoryContractModificationAvailability t
					JOIN Budget.AvailabilityDetail ad ON ad.Id = t.AvailabilityDetailId
					WHERE t.IsDelete = 0 AND t.Value > ad.Balance
				)
				BEGIN
					SELECT @Message = STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) +  ' - El saldo de la disponibilidad ' + a.Code + ' no puede ser menor al valor a ejecutar'
							FROM @TableInventoryContractModificationAvailability t
							JOIN Budget.AvailabilityDetail ad ON ad.Id = t.AvailabilityDetailId
							JOIN Budget.Availability a ON a.Id = ad.AvailabilityId
							WHERE t.IsDelete = 0 AND t.Value > ad.Balance
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(MAX)'), 1, 2, N'')

					SELECT	999 AS CodeResult, 
							ISNULL(@Message, 'Error al validar el saldo de las disponibilidades') AS MessageResult, 
							0 as Id, '' as Code
					FROM Inventory.InventoryContractModification om 
					WHERE om.Id = @Id
					RETURN
				END
			END

			/*************************************************************************************/

			DECLARE @ConfirmationUser VARCHAR(20) = CASE WHEN @Status = 2 THEN @UserCode ELSE NULL END
			DECLARE @ConfirmationDate DATETIME = CASE WHEN @Status = 2 THEN [Common].[GETDATE]() ELSE NULL END

			IF @Id = 0
			BEGIN
				--Si se esta insertando por primera vez se consulta la secuencia numerica
				IF @Code = '' 
				BEGIN
					--Si se esta insertando por primera vez se consulta la secuencia numerica
					DECLARE @IsManual BIT
				
					EXEC Common.SP_GetSequence 190, @IdForm, @OperatingUnitId, NULL, NULL, @IsManual OUT, @Code OUT, @Code_Output OUT, @Message_Output OUT

					IF @Code_Output <> 0
					BEGIN
						SELECT	999 AS CodeResult, 
								REPLACE(@Message_Output, '{0}', 'Cesión de Contrato') AS MessageResult, 
								0 as Id, '' as Code
						RETURN
					END

					--Se inserta la cabecera
					INSERT INTO [Inventory].[InventoryContractModification]
					(
						[OperatingUnitId],[Code],[DocumentDate],[ContractId],[ModificationType],[EndDate],[Value],[Description],
						[Status],[CreationUser],[CreationDate],[ModificationUser],[ModificationDate],[ConfirmationUser],[ConfirmationDate]
					)
					SELECT	@OperatingUnitId,@Code,@DocumentDate,@ContractId,@ModificationType,@EndDate,@Value,@Description,
							@Status,@UserCode,[Common].[GETDATE](),@ConfirmationUser,@ConfirmationDate,@ConfirmationUser,@ConfirmationDate

					--Obtengo el id de la cabcera
					SET @Id = SCOPE_IDENTITY()
				END
			END
			ELSE --Si se esta actualizando
			BEGIN
				UPDATE [Inventory].[InventoryContractModification]
					SET [OperatingUnitId] = @OperatingUnitId,
						[Code] = @Code,
						[DocumentDate] = @DocumentDate,
						[ContractId] = @ContractId,
						[ModificationType] = @ModificationType,
						[EndDate] = @EndDate,
						[Value] = @Value,
						[Description] = @Description,
						[Status] = @Status,
						[ModificationUser] = @UserCode,
						[ModificationDate] = [Common].[GETDATE](),
						[ConfirmationUser] = @ConfirmationUser,
						[ConfirmationDate] = @ConfirmationDate
				WHERE Id = @Id
			END

			/*************************************************************************************/

			--Se actualizan los detalles de disponibilidades
			IF EXISTS (SELECT 1 FROM @TableInventoryContractModificationAvailability WHERE Id > 0 AND IsDelete = 0)
			BEGIN
				UPDATE a 
					SET a.[InventoryContractModificationId] = temp.InventoryContractModificationId, 
						a.[AvailabilityDetailId] = temp.AvailabilityDetailId, 
						a.[Value] = temp.Value
				FROM @TableInventoryContractModificationAvailability temp
				JOIN Inventory.InventoryContractModificationAvailability a ON a.Id = temp.Id
				WHERE temp.Id > 0 AND temp.IsDelete = 0
			END

			--Se guardan los detalles de disponibilidades
			IF EXISTS (SELECT 1 FROM @TableInventoryContractModificationAvailability WHERE Id = 0)
			BEGIN
				INSERT INTO [Inventory].[InventoryContractModificationAvailability]
				(
					[InventoryContractModificationId], [AvailabilityDetailId], [Value]
				)
				SELECT @Id, AvailabilityDetailId, Value
				FROM @TableInventoryContractModificationAvailability
				WHERE Id = 0
			END

			--Si se esta confirmando y hay detalles de disponibilidades se genera el compromiso
			IF @Status = 2
			BEGIN
				UPDATE ic 
					SET ic.ModificationValue = ic.ModificationValue + IIF(@ModificationType IN (1, 3), @Value, 0)
				FROM Inventory.InventoryContract ic
				WHERE ic.Id = @ContractId

				IF @ModificationType IN (1, 3) AND EXISTS(SELECT 1 FROM @TableInventoryContractModificationAvailability)
				BEGIN
					--Se genera el xml del compromiso
					SELECT @SubXml = CONVERT
					(
						XML, 
						(
							SELECT *
							FROM 
							(
								SELECT	0 Id, 
										'' Code, 
										@BudgetaryValidityId BudgetaryValidityId, 
										s.IdThirdParty ThirdPartyId, 
										3 DocumentSource, 
										ic.ContractNumber Document, 
										@DocumentDate DocumentDate, 
										1 CommitmentType, 
										@Description Observations, 
										1 Status, 
										@Id EntityId, 
										@Code EntityCode, 
										'InventoryContractModification' EntityName
								FROM Inventory.InventoryContract ic
								JOIN Common.Supplier s ON ic.SupplierId = s.Id
								WHERE ic.Id = @ContractId
							) Commitment
							JOIN 
							( 
								SELECT	0 Id, 
										0 CommitmentId, 
										ad.Id AvailabilityDetailId, 
										b.CategoryId CategoryId, 
										b.RevenueTypeId RevenueTypeId, 
										@EndDate ExpiredDate, 
										ica.Value InitialValue, 
										0 DebitModificationValue, 
										0 CreditModificationValue, 
										ica.Value TotalCommitment, 
										0 ExecutedValue, 
										ica.Value Balance
								FROM Inventory.InventoryContractModificationAvailability ica
								JOIN Budget.AvailabilityDetail ad ON ad.Id = ica.AvailabilityDetailId
								JOIN Budget.Budget b ON b.Id = ad.BudgetId
								WHERE ica.InventoryContractModificationId = @Id
							) CommitmentDetail ON CommitmentDetail.CommitmentId = Commitment.Id
							FOR XML AUTO,TYPE, ELEMENTS
						)
					)

					--Se ejecuta el sp del compromiso
					EXEC Budget.SP_SaveCommitment_Output @SubXml, '', @UserCode, @Code_Output OUT, @Message_Output OUT, NULL, NULL

					--Se valida el resultado
					IF @Code_Output <> 0
					BEGIN
						SELECT	999 AS CodeResult, 
								ISNULL(@Message_Output, 'Error al generar el compromiso') AS MessageResult, 
								0 as Id, '' as Code
						RETURN
					END
				END
			END
		END

		IF @Status = 1
		BEGIN
			IF NOT EXISTS (SELECT 1 FROM Inventory.InventoryControlDocument WHERE DocumentType = @DocumentTypeControl AND DocumentNumber = @Code)
			BEGIN
				INSERT INTO Inventory.InventoryControlDocument (DocumentNumber, DocumentType, DocumentUser, DocumentDate)
				SELECT @Code, @DocumentTypeControl, @UserCode, @DocumentDate
			END
		END
		ELSE
		BEGIN
			DELETE FROM Inventory.InventoryControlDocument WHERE DocumentType = @DocumentTypeControl AND DocumentNumber = @Code
		END

		SELECT	0 AS CodeResult,
				CASE @Status
					WHEN 2 THEN CONCAT('Se guardó y confirmó el Otro si de Contrato con código ', @Code, IIF(@Message_Output = '', '', CHAR(13) + CHAR(10) + @Message_Output))
					WHEN 3 THEN CONCAT('Se anuló el Otro si de Contrato con código ', @Code)
					ELSE CONCAT('Se guardó el Otro si de Contrato con código ', @Code)
				END AS MessageResult,
				@Id AS Id,
				@Code AS Code
	END TRY
	BEGIN CATCH
		SELECT	999 AS CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS MessageResult, 0 as Id, '' as Code
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda o actualiza una modificación (otrosí o adenda) sobre un contrato de inventario/suministros. Según el estado recibido, puede anular la modificación (marcando usuario y fecha de anulación) o registrar/actualizar sus datos junto con las disponibilidades presupuestales (CDP) asociadas, validando que todos los rubros pertenezcan a la misma vigencia presupuestal, que la sumatoria de valores coincida con el valor del contrato y que el saldo de cada disponibilidad sea suficiente. Administra los registros en las tablas InventoryContractModification e InventoryContractAvailability, cruzando información de disponibilidades presupuestales (Budget.Availability y Budget.AvailabilityDetail) para garantizar la coherencia financiera del contrato antes de confirmar los cambios.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveInventoryContractModification';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveInventoryContractModification';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Guarda, modifica, confirma o anula una modificación (otrosí) de contrato de inventario, validando rubros presupuestales, ajustando saldos y generando el compromiso presupuestal cuando corresponde.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInventoryContractModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La modificación debe estar en estado 1 (borrador) para permitir cambios; si Status<>1 se rechaza.; Si hay detalles de disponibilidad no eliminados, todos deben pertenecer a la misma vigencia presupuestal indicada.; La sumatoria de los valores de los rubros (detalles) debe ser igual al valor (redondeado) del contrato modificado.; El saldo (Balance) de cada AvailabilityDetail asociado debe ser mayor o igual al valor a ejecutar.; Para asignar código automáticamente debe existir secuencia configurada para el formulario 2117 y unidad operativa.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInventoryContractModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Una modificación solo puede editarse mientras está en estado 1 (borrador).; La suma de valores de rubros de la modificación siempre debe igualar el valor del contrato modificado.; Todos los rubros de una modificación pertenecen a una única vigencia presupuestal.; No se permite asignar a un rubro un valor mayor al saldo disponible de su AvailabilityDetail.; El compromiso presupuestal solo se genera para modificaciones tipo 1 o 3 al confirmar y con detalles existentes.; El documento de control (tipo 17) existe solo mientras la modificación está en estado 1.; Confirmación (Status=2) y anulación (Status=3) registran usuario y fecha trazables.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInventoryContractModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Inventory.InventoryContractModification: Cuando Status=3 (anulación), marca el registro como anulado registrando usuario/fecha de modificación y de anulación.; [INSERT] Inventory.InventoryContractModification: Cuando Id=0 y Code vacío, obtiene secuencia vía Common.SP_GetSequence y crea la cabecera; si Status=2 también registra usuario y fecha de confirmación.; [UPDATE] Inventory.InventoryContractModification: Cuando Id<>0 actualiza la cabecera con los datos del XML y, si Status=2, fija usuario/fecha de confirmación.; [DELETE] Inventory.InventoryContractAvailability: Para cada detalle marcado IsDelete=1 elimina la disponibilidad del contrato cuyo Id coincida y pertenezca al contrato actual.; [UPDATE] Inventory.InventoryContractModificationAvailability: Para detalles con Id>0 e IsDelete=0 actualiza la modificación, AvailabilityDetail y valor.; [INSERT] Inventory.InventoryContractModificationAvailability: Para detalles con Id=0 inserta nuevos registros asociados a la modificación recién creada/actualizada.; [UPDATE] Inventory.InventoryContract: Cuando Status=2 (confirmación), incrementa ModificationValue del contrato sumando @Value solo si ModificationType IN (1,3); en otros tipos suma 0.; [INSERT] Inventory.InventoryControlDocument: Cuando Status=1 y no existe documento de control con DocumentType=17 y mismo DocumentNumber, registra el documento de control.; [DELETE] Inventory.InventoryControlDocument: Cuando Status<>1 elimina el documento de control con DocumentType=17 y DocumentNumber igual al código.; [RETURN_RESULT] Budget.SP_SaveCommitment_Output: Cuando Status=2, ModificationType IN (1,3) y existen detalles de disponibilidad, genera XML y ejecuta el SP de compromiso presupuestal.; [RETURN_RESULT] Inventory.InventoryContractModification: Devuelve CodeResult=999 con mensajes de error específicos: estado no editable, vigencias mixtas, sumatoria inválida, saldo insuficiente, error de secuencia o de compromiso.; [RETURN_RESULT] Inventory.InventoryContractModification: Devuelve CodeResult=0 con mensaje según Status: guardado, guardado y confirmado, o anulado, junto con Id y Code.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInventoryContractModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe modificación con Id y Status<>1 → Retorna error indicando que está Confirmado (Status=2) o Anulado (otro) else Continúa el flujo de guardado; si Status=3 → Solo actualiza la cabecera marcando anulación else Procesa detalles, valida y guarda/actualiza; si Id=0 y Code vacío → Obtiene secuencia automática e inserta cabecera nueva else Si Id<>0 actualiza la cabecera existente; si Status=2 (confirmación) → Suma @Value a ModificationValue del contrato (si tipo 1 o 3) y, con detalles, genera compromiso presupuestal else No actualiza saldo del contrato ni genera compromiso; si ModificationType IN (1,3) → Afecta el ModificationValue del contrato y genera compromiso presupuestal else No suma al ModificationValue ni genera compromiso; si Status=1 → Inserta documento de control si no existe else Elimina el documento de control correspondiente', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInventoryContractModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.SP_GetSequence; Budget.SP_SaveCommitment_Output; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInventoryContractModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryContractModification; Inventory.InventoryContractAvailability; Inventory.InventoryContractModificationAvailability; Inventory.InventoryContract; Inventory.InventoryControlDocument; Budget.Availability; Budget.AvailabilityDetail; Budget.Budget; Common.Supplier', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInventoryContractModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInventoryContractModification';
-- GO
