-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-21
-- Description:	Procedimiento el cual se encarga de guardar un otro si
-- =============================================
CREATE PROCEDURE [Inventory].[SP_SaveInventoryContractAssignment]
	@InventoryContractAssignmentXML AS XML,
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
			@SupplierTransferorId INT,
			@SupplierDistributionLineTransferorId INT,
			@SupplierAssigneeId INT,
			@SupplierDistributionLineAssigneeId INT,
			@Description VARCHAR(MAX),
			@Status TINYINT,
			------------------------------
			@IdForm INT = 2116,
			@DocumentTypeControl INT = 16,
			------------------------------
			@Message VARCHAR(MAX),
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)

	--Tabla para los detalles de compromisos
	DECLARE @TableCommitmentDetail TABLE
	(
		Id INT, 
		BudgetaryValidityId INT,
		CommitmentId INT, 
		AvailabilityDetailId INT, 
		CategoryId INT,
		RevenueTypeId INT,
		DateExpired DATETIME,
		Value NUMERIC(20,4)
	)

	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT 
			@Id = t.x.value('Id[1]','int'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
			@Code = t.x.value('Code[1]','varchar(20)'),
			@DocumentDate = t.x.value('DocumentDate[1]','datetime'),
			@ContractId = t.x.value('ContractId[1]','int'),
			@SupplierTransferorId = t.x.value('SupplierTransferorId[1]','int'),
			@SupplierDistributionLineTransferorId = t.x.value('SupplierDistributionLineTransferorId[1]','int'),
			@SupplierAssigneeId = t.x.value('SupplierAssigneeId[1]','int'),
			@SupplierDistributionLineAssigneeId = t.x.value('SupplierDistributionLineAssigneeId[1]','int'),
			@Description = t.x.value('Description[1]','varchar(max)'),
			@Status = t.x.value('Status[1]','tinyint')
		FROM @InventoryContractAssignmentXML.nodes('/InventoryContractAssignment') t(x)

		IF EXISTS (SELECT 1 FROM Inventory.InventoryContractAssignment om WHERE om.Id = @Id AND om.Status <> 1)
		BEGIN
			SELECT	999 AS CodeResult, 
					'La Cesión de Contrato se encuentra en estado: ' + IIF(om.Status = 2, 'Confirmado', 'Anulado') AS MessageResult, 
					0 as Id, '' as Code
			FROM Inventory.InventoryContractAssignment om 
			WHERE om.Id = @Id
			RETURN
		END
		
		IF @Status = 3
		BEGIN
			UPDATE [Inventory].[InventoryContractAssignment]
				SET [Status] = @Status,
					[ModificationUser] = @UserCode,
					[ModificationDate] = [Common].[GETDATE](),
					[AnnulmentUser] = @UserCode,
					[AnnulmentDate] = [Common].[GETDATE]()
			WHERE Id = @Id
		END
		ELSE
		BEGIN
			/*************************************VALIDACIONES************************************/

			-- Valido que el proveedor sea diferente
			IF @SupplierTransferorId = @SupplierAssigneeId OR @SupplierDistributionLineTransferorId = @SupplierDistributionLineAssigneeId
			BEGIN
				SELECT	999 AS CodeResult, 
						'No se puede ceder el contrato al mismo proveedor' AS MessageResult, 
						0 as Id, '' as Code
				RETURN
			END

			-- Valido que el proveedor cedente sea valido
			IF NOT EXISTS (SELECT 1 FROM Common.SuppliersDistributionLines sdl WHERE sdl.Id = @SupplierDistributionLineTransferorId AND sdl.IdSupplier = @SupplierTransferorId)
			BEGIN
				SELECT	999 AS CodeResult, 
						'El proveedor cedente no esta asignado a la linea de distribución seleccionada' AS MessageResult, 
						0 as Id, '' as Code
				RETURN
			END

			-- Valido que el proveedor cesionario sea valido
			IF NOT EXISTS (SELECT 1 FROM Common.SuppliersDistributionLines sdl WHERE sdl.Id = @SupplierDistributionLineAssigneeId AND sdl.IdSupplier = @SupplierAssigneeId)
			BEGIN
				SELECT	999 AS CodeResult, 
						'El proveedor cesionario no esta asignado a la linea de distribución seleccionada' AS MessageResult, 
						0 as Id, '' as Code
				RETURN
			END

			-- Valido que el proveedor cedente sea el actual del contrato
			IF NOT EXISTS (SELECT 1 FROM Inventory.InventoryContract ic WHERE ic.Id = @ContractId AND ic.SupplierDistributionLineId = @SupplierDistributionLineTransferorId AND ic.SupplierId = @SupplierTransferorId)
			BEGIN
				SELECT	999 AS CodeResult, 
						'El proveedor cedente no es el proveedor actual del contrato' AS MessageResult, 
						0 as Id, '' as Code
				RETURN
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
					INSERT INTO [Inventory].[InventoryContractAssignment]
					(
						[OperatingUnitId],[Code],[DocumentDate],[ContractId],[SupplierTransferorId],[SupplierDistributionLineTransferorId],
						[SupplierAssigneeId],[SupplierDistributionLineAssigneeId],[Description],
						[Status],[CreationUser],[CreationDate],[ModificationUser],[ModificationDate],[ConfirmationUser],[ConfirmationDate]
					)
					SELECT	@OperatingUnitId,@Code,@DocumentDate,@ContractId,@SupplierTransferorId,@SupplierDistributionLineTransferorId,
							@SupplierAssigneeId,@SupplierDistributionLineAssigneeId,@Description,
							@Status,@UserCode,[Common].[GETDATE](),@ConfirmationUser,@ConfirmationDate,@ConfirmationUser,@ConfirmationDate

					--Obtengo el id de la cabcera
					SET @Id = SCOPE_IDENTITY()
				END
			END
			ELSE --Si se esta actualizando
			BEGIN
				UPDATE [Inventory].[InventoryContractAssignment]
					SET [OperatingUnitId] = @OperatingUnitId,
						[Code] = @Code,
						[DocumentDate] = @DocumentDate,
						[ContractId] = @ContractId,
						[SupplierTransferorId] = @SupplierTransferorId,
						[SupplierDistributionLineTransferorId] = @SupplierDistributionLineTransferorId,
						[SupplierAssigneeId] = @SupplierAssigneeId,
						[SupplierDistributionLineAssigneeId] = @SupplierDistributionLineAssigneeId,
						[Description] = @Description,
						[Status] = @Status,
						[ModificationUser] = @UserCode,
						[ModificationDate] = [Common].[GETDATE](),
						[ConfirmationUser] = @ConfirmationUser,
						[ConfirmationDate] = @ConfirmationDate
				WHERE Id = @Id
			END

			/*************************************************************************************/

			IF @Status = 2
			BEGIN
				UPDATE ic
					SET ic.SupplierId = @SupplierAssigneeId,
						ic.SupplierDistributionLineId = @SupplierDistributionLineAssigneeId
				FROM Inventory.InventoryContract ic
				WHERE ic.Id = @ContractId

				--Interfaces presupuestales generadas por el contrato
				INSERT INTO @TableCommitmentDetail
					(
						Id, BudgetaryValidityId, CommitmentId, AvailabilityDetailId, CategoryId, RevenueTypeId, DateExpired, Value
					)
					SELECT cd.Id, c.BudgetaryValidityId, c.Id, cd.AvailabilityDetailId, cd.CategoryId, cd.RevenueTypeId, cd.ExpiredDate, cd.Balance
					FROM Inventory.InventoryContract ic
					JOIN Budget.Commitment c ON ic.Id = c.EntityId AND c.EntityName = 'InventoryContract'
					JOIN Budget.CommitmentDetail cd ON c.Id = cd.CommitmentId
					WHERE ic.Id = @ContractId AND c.Status = 2 AND cd.Balance > 0

					UNION ALL

					SELECT cd.Id, c.BudgetaryValidityId, c.Id, cd.AvailabilityDetailId, cd.CategoryId, cd.RevenueTypeId, cd.ExpiredDate, cd.Balance
					FROM Inventory.InventoryContractModification icm
					JOIN Budget.Commitment c ON icm.Id = c.EntityId AND c.EntityName = 'InventoryContractModification'
					JOIN Budget.CommitmentDetail cd ON c.Id = cd.CommitmentId
					WHERE icm.ContractId = @ContractId AND c.Status = 2 AND cd.Balance > 0

					UNION ALL

					SELECT cd.Id, c.BudgetaryValidityId, c.Id, cd.AvailabilityDetailId, cd.CategoryId, cd.RevenueTypeId, cd.ExpiredDate, cd.Balance
					FROM Inventory.InventoryContractModification icm
					JOIN Budget.Commitment c ON icm.Id = c.EntityId AND c.EntityName = 'InventoryContractAssignment'
					JOIN Budget.CommitmentDetail cd ON c.Id = cd.CommitmentId
					WHERE icm.ContractId = @ContractId AND c.Status = 2 AND cd.Balance > 0


				-- Si el contrato una modificación del contrato hizo interfaz, se debe reversar el movimiento
				IF EXISTS ( SELECT 1 FROM @TableCommitmentDetail )
				BEGIN
					IF EXISTS
					(
						SELECT 1
						FROM
						(
							SELECT BudgetaryValidityId
							FROM @TableCommitmentDetail tcd
							GROUP BY BudgetaryValidityId
						) tcd
						GROUP BY BudgetaryValidityId
						HAVING COUNT(*) > 1
					)
					BEGIN
						SELECT	999 AS CodeResult, 
								'Los compromisos asociados al contrato pertenecen a más de una vigencia' AS MessageResult, 
								0 as Id, '' as Code
						RETURN
					END


					--Generamos las modificaciones debito de los compromisos
					DECLARE @CommitmentRows INT = 1,
							@CommitmentId INT = 0

					WHILE @CommitmentRows > 0
					BEGIN
						SELECT TOP 1
							@CommitmentId = CommitmentId
						FROM @TableCommitmentDetail
						WHERE CommitmentId > @CommitmentId
						ORDER BY CommitmentId

						SET @CommitmentRows = @@ROWCOUNT
						IF @CommitmentRows = 0 
						BEGIN
							BREAK
						END

						SELECT @SubXml = CONVERT
						(
							XML, 
							(
								SELECT 
									CommitmentModification.*,
									CommitmentModificationDetail.*
								FROM 
								(
									SELECT TOP 1
										0 Id,						
										'' Code,
										@OperatingUnitId OperatingUnitId,
										c.BudgetaryValidityId,
										@DocumentDate DocumentDate,
										c.Id CommitmentId,
										3 UpTo,
										@Code Document,
										'Modificación generada desde la cesion No. ' + @Code Observations,
										2 Status,
										@Id EntityId,
										@Code EntityCode,
										'InventoryContractAssignment' EntityName
									FROM Budget.Commitment c
									WHERE c.Id = @CommitmentId
								) CommitmentModification
								JOIN
								( 
									SELECT
										0 CommitmentModificationId,
										tcd.Id CommitmentDetailId,
										tcd.AvailabilityDetailId AvailabilityDetailId,
										tcd.CategoryId,
										tcd.RevenueTypeId,
										1 Nature,
										tcd.Value,
										0 IsLogBase
									FROM @TableCommitmentDetail tcd
									WHERE tcd.CommitmentId = @CommitmentId
								) CommitmentModificationDetail ON CommitmentModification.Id = CommitmentModificationDetail.CommitmentModificationId
								For xml AUTO,TYPE, ELEMENTS
							)
						)

						EXEC [Budget].[SP_SaveCommitmentModification_Output] @SubXml, '', @UserCode, @Code_Output OUT, @Message_Output OUT, NULL, NULL, NULL

						IF @Code_Output <> 0
						BEGIN
							SELECT	999 AS CodeResult, 
									ISNULL(@Message_Output, 'No se pudo generar la modificación del compromiso presupuestal') AS MessageResult, 
									0 as Id, '' as Code
							RETURN
						END

						SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
					END

					DECLARE @ThirdPartyId INT,
							@ContractNumber VARCHAR(20)

					SELECT	@ThirdPartyId = s.IdThirdParty,
							@ContractNumber = ic.ContractNumber
					FROM Inventory.InventoryContract ic
					JOIN Common.Supplier s ON ic.SupplierId = s.Id
					WHERE ic.Id = @ContractId

					--Se genera el xml del compromiso
					SELECT @SubXml = CONVERT
					(
						XML, 
						(
							SELECT *
							FROM 
							(
								SELECT TOP 1
										0 Id, 
										'' Code, 
										BudgetaryValidityId, 
										@ThirdPartyId ThirdPartyId, 
										3 DocumentSource, 
										@ContractNumber Document, 
										@DocumentDate DocumentDate, 
										1 CommitmentType, 
										@Description Observations, 
										1 Status, 
										@Id EntityId, 
										@Code EntityCode, 
										'InventoryContractAssignment' EntityName
								FROM @TableCommitmentDetail
							) Commitment
							JOIN 
							( 
								SELECT	0 Id, 
										0 CommitmentId, 
										AvailabilityDetailId, 
										CategoryId, 
										RevenueTypeId, 
										MAX(DateExpired) ExpiredDate, 
										SUM(Value) InitialValue,
										0 IsLogBase
								FROM @TableCommitmentDetail
								GROUP BY AvailabilityDetailId, CategoryId, RevenueTypeId
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

					SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
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
					WHEN 2 THEN CONCAT('Se guardó y confirmó la Cesión de Contrato con código ', @Code, IIF(@Message = '', '', CHAR(13) + CHAR(10) + @Message))
					WHEN 3 THEN CONCAT('Se anuló la Cesión de Contrato con código ', @Code)
					ELSE CONCAT('Se guardó la Cesión de Contrato con código ', @Code)
				END AS MessageResult,
				@Id AS Id,
				@Code AS Code
	END TRY
	BEGIN CATCH
		SELECT	999 AS CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS MessageResult, 0 as Id, '' as Code
	END CATCH
END