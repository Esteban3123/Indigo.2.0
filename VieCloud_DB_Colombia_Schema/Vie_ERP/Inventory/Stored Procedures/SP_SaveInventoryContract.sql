-- ===============================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create DATE: 19/11/2019
-- Description:	Procedimiento que se encarga de realizar los contratos del módulo de inventarios
-- ===============================================================================================
CREATE PROCEDURE [Inventory].[SP_SaveInventoryContract]
	@Xml xml,
	@UserCode VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	--Variables para guardar la cabecera
	DECLARE @Id INT, 
			@Code VARCHAR(20), 
			@OperatingUnitId INT, 
			@ContractTypeId INT, 
			@DocumentDate DATETIME, 
			@InitialDate DATETIME, 
			@EndDate DATETIME, 
			@SupplierId INT, 
			@SupplierDistributionLineId INT, 
			@ContractNumber VARCHAR(20), 
			@Description VARCHAR(300), 
			@PaymentMethod VARCHAR(200), 
			@DeliveryMethod VARCHAR(200), 
			@DeliveryPlace VARCHAR(200), 
			@SourceOrder TINYINT, 
			@PurchaseProcess TINYINT, 
			@Exclusivity BIT, 
			@ManageProducts BIT, 
			@OnlyGuarantee BIT, 
			@TechnicalSupervicion VARCHAR(100), 
			@SupervisionExecution VARCHAR(100), 
			@Clauses VARCHAR(MAX), 
			@Attachments VARCHAR(MAX), 
			@Availability VARCHAR(200), 
			@Resolution VARCHAR(200), 
			@ResolutionDate DATETIME, 
			@QuoteNumber VARCHAR(50), 
			@QuoteDate DATE, 
			@RecordNumber VARCHAR(50), 
			@RecordDate DATE, 
			@NegotiationType VARCHAR(200), 
			@Approved VARCHAR(100), 
			@Deadline DATE, 
			@ValidityDate DATE, 
			@Value NUMERIC(20, 4), 
			@DiscountValue NUMERIC(20, 4), 
			@IvaValue NUMERIC(20, 4), 
			@TotalValue NUMERIC(20, 4), 
			@Status TINYINT,
			@BudgetaryValidityId INT,
			@CurrencyId INT,
			------------------------------
			@Message VARCHAR(MAX) = '',
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)

	--Tabla para los detalles de productos
	DECLARE @TableInventoryContractDetail TABLE
	(
		Id INT, 
		InventoryContractId INT, 
		ProductId INT, 
		Quantity INT, 
		OutstandingQuantity INT, 
		CancelledQuantity INT, 
		Value NUMERIC(20,4), 
		SubTotalValue NUMERIC(20,4), 
		IvaPercentage NUMERIC(5,2), 
		IvaValue NUMERIC(20,4), 
		DiscountPercentage NUMERIC(5,2), 
		DiscountValue NUMERIC(20,4), 
		TotalValue NUMERIC(20,4), 
		IsDelete BIT
	)

	--Tabla para los detalles de disponibilidades
	DECLARE @TableInventoryContractAvailability TABLE
	(
		Id INT, 
		InventoryContractId INT, 
		AvailabilityDetailId INT, 
		Value NUMERIC(20,4), 
		IsDelete BIT
	)

	BEGIN TRY
		SELECT @CurrencyId= cs.OfficialCurrencyId FROM GeneralLedger.CompanySettings cs WITH(NOLOCK)
	
		--Se obtienen los datos del xml para la cabecera
		SELECT	@Id = t.x.value('Id[1]','INT'),
				@Code = t.x.value('Code[1]','VARCHAR(20)'),
				@OperatingUnitId = t.x.value('OperatingUnitId[1]','INT'),
				@ContractTypeId = t.x.value('ContractTypeId[1]','INT'),
				@DocumentDate = t.x.value('DocumentDate[1]','DATETIME'),
				@InitialDate = t.x.value('InitialDate[1]','DATETIME'),
				@EndDate = t.x.value('EndDate[1]','DATETIME'),
				@SupplierId = t.x.value('SupplierId[1]','INT'),
				@SupplierDistributionLineId = t.x.value('SupplierDistributionLineId[1]','INT'),
				@ContractNumber = t.x.value('ContractNumber[1]','VARCHAR(20)'),
				@Description = t.x.value('Description[1]','VARCHAR(300)'),
				@PaymentMethod = t.x.value('PaymentMethod[1]','VARCHAR(200)'),
				@DeliveryMethod = IIF(t.x.value('DeliveryMethod[1]','VARCHAR(200)') = '', null, t.x.value('DeliveryMethod[1]','VARCHAR(200)')),
				@DeliveryPlace = IIF(t.x.value('DeliveryPlace[1]','VARCHAR(200)') = '', null, t.x.value('DeliveryPlace[1]','VARCHAR(200)')),
				@SourceOrder = t.x.value('SourceOrder[1]','TINYINT'),
				@PurchaseProcess = t.x.value('PurchaseProcess[1]','TINYINT'),
				@Exclusivity = t.x.value('Exclusivity[1]','BIT'),
				@ManageProducts = t.x.value('ManageProducts[1]','BIT'),
				@OnlyGuarantee = t.x.value('OnlyGuarantee[1]','BIT'),
				@TechnicalSupervicion = IIF(t.x.value('TechnicalSupervicion[1]','VARCHAR(100)') = '', null, t.x.value('TechnicalSupervicion[1]','VARCHAR(100)')),
				@SupervisionExecution = IIF(t.x.value('SupervisionExecution[1]','VARCHAR(100)') = '', null, t.x.value('SupervisionExecution[1]','VARCHAR(100)')),
				@Clauses = IIF(t.x.value('Clauses[1]','VARCHAR(MAX)') = '', null, t.x.value('Clauses[1]','VARCHAR(MAX)')),
				@Attachments = IIF(t.x.value('Attachments[1]','VARCHAR(MAX)') = '', null, t.x.value('Attachments[1]','VARCHAR(MAX)')),
				@Availability = IIF(t.x.value('Availability[1]','VARCHAR(200)') = '', null, t.x.value('Availability[1]','VARCHAR(200)')),
				@Resolution = IIF(t.x.value('Resolution[1]','VARCHAR(200)') = '', null, t.x.value('Resolution[1]','VARCHAR(200)')),
				@ResolutionDate = IIF(t.x.value('ResolutionDate[1]','VARCHAR(20)') = '', null, convert(DATE, t.x.value('ResolutionDate[1]','VARCHAR(20)'), 103)),
				@QuoteNumber = IIF(t.x.value('QuoteNumber[1]','VARCHAR(50)') = '', null, t.x.value('QuoteNumber[1]','VARCHAR(50)')),
				@QuoteDate = IIF(t.x.value('QuoteDate[1]','VARCHAR(20)') = '', null, convert(DATE, t.x.value('QuoteDate[1]','VARCHAR(20)'), 103)),
				@RecordNumber = IIF(t.x.value('RecordNumber[1]','VARCHAR(50)') = '', null, t.x.value('RecordNumber[1]','VARCHAR(50)')),
				@RecordDate = IIF(t.x.value('RecordDate[1]','VARCHAR(20)') = '', null, convert(DATE, t.x.value('RecordDate[1]','VARCHAR(20)'), 103)),
				@NegotiationType = IIF(t.x.value('NegotiationType[1]','VARCHAR(200)') = '', null, t.x.value('NegotiationType[1]','VARCHAR(200)')),
				@Approved = IIF(t.x.value('Approved[1]','VARCHAR(100)') = '', null, t.x.value('Approved[1]','VARCHAR(100)')),
				@Deadline = IIF(t.x.value('Deadline[1]','VARCHAR(20)') = '', null, convert(DATE, t.x.value('Deadline[1]','VARCHAR(20)'), 103)),
				@ValidityDate = IIF(t.x.value('ValidityDate[1]','VARCHAR(20)') = '', null, convert(DATE, t.x.value('ValidityDate[1]','VARCHAR(20)'), 103)),
				@Value = REPLACE(t.x.value('Value[1]','VARCHAR(20)'), ',', '.'),
				@DiscountValue = REPLACE(t.x.value('DiscountValue[1]','VARCHAR(20)'), ',', '.'),
				@IvaValue = REPLACE(t.x.value('IvaValue[1]','VARCHAR(20)'), ',', '.'),
				@TotalValue = REPLACE(t.x.value('TotalValue[1]','VARCHAR(20)'), ',', '.'),
				@Status = t.x.value('Status[1]','TINYINT'),
				@BudgetaryValidityId = t.x.value('BudgetaryValidityId[1]','INT'),
				@CurrencyId = iif( t.x.value('CurrencyId [1]','INT') is null,@CurrencyId,t.x.value('CurrencyId [1]','INT'))
		FROM @Xml.nodes('/InventoryContract') t(x)

		IF @Status = 4
		BEGIN
			IF NOT EXISTS (SELECT 1 FROM Inventory.InventoryContract ic WITH (NOLOCK) WHERE ic.Id = @Id AND ic.Status = 2)
			BEGIN
				SELECT	999 as CodeMessage, 
						'No se encontró el registro en estado confirmado' Message, 
						0 InventoryContractId, 
						'' InventoryContractCode, 
						0 CommitmentId, 
						'' CommitmentCode
				RETURN
			END
		END
		ELSE
		BEGIN
			IF EXISTS (SELECT 1 FROM Inventory.InventoryContract ic WITH (NOLOCK) WHERE ic.Id = @Id AND ic.Status <> 1)
			BEGIN
				SELECT	999 as CodeMessage, 
						'El contrato se encuentra en estado: ' + CASE ic.Status 
							WHEN 2 THEN 'Confirmado'
							WHEN 3 THEN 'Anulado'
							WHEN 4 THEN 'Legalizado'
							ELSE 'N/A'
						END AS Message, 
						0 InventoryContractId, 
						'' InventoryContractCode, 
						0 CommitmentId, 
						'' CommitmentCode
				FROM Inventory.InventoryContract ic WITH (NOLOCK)
				WHERE ic.Id = @Id
				RETURN
			END
		END

		IF @Status = 3
		BEGIN
			UPDATE [Inventory].[InventoryContract]
				SET [Status] = @Status,
					[ModificationUser] = @UserCode,
					[ModificationDate] = [Common].[GETDATE](),
					[AnnulmentUser] = @UserCode,
					[AnnulmentDate] = [Common].[GETDATE]()
			WHERE Id = @Id
		END
		IF @Status = 4
		BEGIN
			UPDATE [Inventory].[InventoryContract]
				SET [InitialDate] = @InitialDate,
					[EndDate] = @EndDate,
					[TechnicalSupervicion] = @TechnicalSupervicion,
					[SupervisionExecution] = @SupervisionExecution,
					[Status] = @Status,
					[ModificationUser] = @UserCode,
					[ModificationDate] = [Common].[GETDATE]()
			WHERE Id = @Id
		END
		ELSE
		BEGIN
			--Se obtienen los detalles del xml para los productos
			INSERT INTO @TableInventoryContractDetail
				SELECT 
					t.x.value('Id[1]','INT') as Id,
					t.x.value('InventoryContractId[1]','INT') as InventoryContractId,
					t.x.value('ProductId[1]','INT') as ProductId,
					t.x.value('Quantity[1]','INT') as Quantity,
					t.x.value('OutstandingQuantity[1]','INT') as OutstandingQuantity,
					t.x.value('CancelledQuantity[1]','INT') as CancelledQuantity,
					REPLACE(t.x.value('Value[1]','VARCHAR(20)'), ',', '.') as Value,
					REPLACE(t.x.value('SubTotalValue[1]','VARCHAR(20)'), ',', '.') as SubTotalValue,
					REPLACE(t.x.value('IvaPercentage[1]','VARCHAR(20)'), ',', '.') as IvaPercentage,
					REPLACE(t.x.value('IvaValue[1]','VARCHAR(20)'), ',', '.') as IvaValue,
					REPLACE(t.x.value('DiscountPercentage[1]','VARCHAR(20)'), ',', '.') as DiscountPercentage,
					REPLACE(t.x.value('DiscountValue[1]','VARCHAR(20)'), ',', '.') as DiscountValue,
					REPLACE(t.x.value('TotalValue[1]','VARCHAR(20)'), ',', '.') as TotalValue,
					t.x.value('IsDelete[1]','BIT')
				FROM @Xml.nodes('/InventoryContract/InventoryContractDetail') t(x)

			--Se obtienen los detalles del xml para los productos
			INSERT INTO @TableInventoryContractAvailability
				SELECT 
					t.x.value('Id[1]','INT') as Id,
					t.x.value('InventoryContractId[1]','INT') as InventoryContractId,
					t.x.value('AvailabilityDetailId[1]','INT') as AvailabilityDetailId,
					REPLACE(t.x.value('Value[1]','VARCHAR(20)'), ',', '.') as Value,
					t.x.value('IsDelete[1]','BIT')
				FROM @Xml.nodes('/InventoryContract/InventoryContractAvailability') t(x)

			--Se eliminan los registros de detalles de productos
			DELETE icd FROM Inventory.InventoryContractDetail icd JOIN @TableInventoryContractDetail ticd ON icd.Id = ticd.Id WHERE ticd.IsDelete = 1
			DELETE FROM @TableInventoryContractDetail WHERE Id > 0 AND IsDelete = 1

			--Se eliminan los registros de detalles de disponibilidades
			DELETE ica FROM Inventory.InventoryContractAvailability ica JOIN @TableInventoryContractAvailability tica ON ica.Id = tica.Id WHERE tica.IsDelete = 1
			DELETE FROM @TableInventoryContractAvailability WHERE Id > 0 AND IsDelete = 1

			/*************************************VALIDACIONES************************************/

			IF EXISTS (SELECT 1 FROM @TableInventoryContractAvailability WHERE IsDelete = 0)
			BEGIN --Si hay disponibilidades asociadas al contrato
				--Se valida que todos los detalles pertenezcan a la misma vigencia
				IF EXISTS
				(
					SELECT 1
					FROM Budget.Availability a WITH (NOLOCK)
					JOIN Budget.AvailabilityDetail ad WITH (NOLOCK) ON a.Id = ad.AvailabilityId
					JOIN @TableInventoryContractAvailability tica ON ad.Id = tica.AvailabilityDetailId
					WHERE tica.IsDelete = 0 AND ISNULL(@BudgetaryValidityId, 0) <> a.BudgetaryValidityId
				)
				BEGIN
					SELECT	999 as CodeMessage, 
							'Los detalles corresponden a más de una vigencia presupuestal.' as Message, 
							0 InventoryContractId, 
							'' InventoryContractCode, 
							0 CommitmentId, 
							'' CommitmentCode
					RETURN
				END

				IF EXISTS
				(
					SELECT 1
					FROM @TableInventoryContractAvailability 
					WHERE IsDelete = 0
					GROUP BY IsDelete
					HAVING SUM(Value) <> ROUND(@TotalValue, 0)
				)
				BEGIN
					SELECT	999 as CodeMessage, 
							'La sumatoria de los rubros no es igual al valor del contrato.' as Message, 
							0 InventoryContractId, 
							'' InventoryContractCode, 
							0 CommitmentId, 
							'' CommitmentCode
					RETURN
				END

				--Se valida que, si se agrego items, y estos tienen asociado un rubro, se hayan seleccionado disponibilidades con saldo suficiente para estos rubros
				IF EXISTS 
				(
					SELECT 1
					FROM 
					(
						SELECT	pg.BudgetId, 
								SUM(t.TotalValue) Value
						FROM @TableInventoryContractDetail t
						JOIN Inventory.InventoryProduct p WITH (NOLOCK) ON t.ProductId = p.Id
						JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON p.ProductGroupId = pg.Id
						WHERE t.IsDelete = 0 AND pg.BudgetId IS NOT NULL
						GROUP BY pg.BudgetId
					) ticd
					LEFT JOIN
					(
						SELECT	ad.BudgetId, 
								SUM(T.Value) Value
						FROM @TableInventoryContractAvailability t
						JOIN Budget.AvailabilityDetail ad WITH (NOLOCK) ON ad.Id = t.AvailabilityDetailId
						WHERE t.IsDelete = 0 
						GROUP BY ad.BudgetId
					) tica ON ticd.BudgetId = tica.BudgetId
					WHERE ticd.Value > ISNULL(tica.Value, 0)
				)
				BEGIN
					SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) +  ' - ' + CONCAT(c.Code, ' - ', c.Name, ' - ' , fs.Code, ' - ', fs.Name, ' - ', rt.Code, ' - ', rt.Name) + ': ' + FORMAT(ticd.Value, 'C0', 'es-CO')
						FROM Budget.Budget b WITH (NOLOCK)
						JOIN Budget.RevenueType rt WITH (NOLOCK) ON b.RevenueTypeId = rt.Id
						JOIN Budget.Category c WITH (NOLOCK) ON b.CategoryId = c.Id
						JOIN Budget.FinancialSource fs WITH (NOLOCK) ON c.FinancialSourceId = fs.Id
						JOIN
						(
							SELECT	pg.BudgetId, 
									SUM(t.TotalValue) Value
							FROM @TableInventoryContractDetail t
							JOIN Inventory.InventoryProduct p WITH (NOLOCK) ON t.ProductId = p.Id
							JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON p.ProductGroupId = pg.Id
							WHERE t.IsDelete = 0 AND pg.BudgetId IS NOT NULL
							GROUP BY pg.BudgetId
						) ticd ON b.Id = ticd.BudgetId
						LEFT JOIN
						(
							SELECT	ad.BudgetId, 
									SUM(T.Value) Value
							FROM @TableInventoryContractAvailability t
							JOIN Budget.AvailabilityDetail ad WITH (NOLOCK) ON ad.Id = t.AvailabilityDetailId
							WHERE t.IsDelete = 0 
							GROUP BY ad.BudgetId
						) tica ON ticd.BudgetId = tica.BudgetId
						WHERE ticd.Value > ISNULL(tica.Value, 0)
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(MAX)'), 1, 2, N'')

					SELECT	999 as CodeMessage, 
							'La sumatoria de los siguientes rubros de los productos no corresponde con el total de los rubros de las disponibilidades: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '') as Message, 
							0 InventoryContractId, 
							'' InventoryContractCode, 
							0 CommitmentId, 
							'' CommitmentCode
					RETURN
				END

				--Se valida que el saldo de las disponibilidades no sea menor al valor a ejecutar
				IF EXISTS 
				(
					SELECT 1
					FROM @TableInventoryContractAvailability t
					JOIN Budget.AvailabilityDetail ad WITH (NOLOCK) ON ad.Id = t.AvailabilityDetailId
					WHERE t.IsDelete = 0 AND t.Value > ad.Balance
				)
				BEGIN
					SELECT @Message = STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) +  ' - El saldo de la disponibilidad ' + a.Code + ' no puede ser menor al valor a ejecutar'
							FROM @TableInventoryContractAvailability t
							JOIN Budget.AvailabilityDetail ad WITH (NOLOCK) ON ad.Id = t.AvailabilityDetailId
							JOIN Budget.Availability a WITH (NOLOCK) ON a.Id = ad.AvailabilityId
							WHERE t.IsDelete = 0 AND t.Value > ad.Balance
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(MAX)'), 1, 2, N'')

					SELECT 999 as CodeMessage, @Message as Message, 0 InventoryContractId, '' InventoryContractCode, 0 CommitmentId, '' CommitmentCode
					RETURN
				END
			END

			/*************************************************************************************/

			DECLARE @ConfirmationUser VARCHAR(20) = CASE WHEN @Status = 2 THEN @UserCode ELSE NULL END
			DECLARE @ConfirmationDate DATETIME = CASE WHEN @Status = 2 THEN [Common].[GETDATE]() ELSE NULL END

			IF @Id = 0
			BEGIN
				--Si se esta insertando por primera vez se consulta la secuencia numerica
				DECLARE @IsManual BIT
				
				EXEC Common.SP_GetSequence 190, 1401, @OperatingUnitId, NULL, NULL, @IsManual OUT, @Code OUT, @Code_Output OUT, @Message_Output OUT

				IF @Code_Output <> 0
				BEGIN
					SELECT	999 as CodeMessage, 
							REPLACE(@Message_Output, '{0}', 'Contratos') Message, 
							0 InventoryContractId, 
							'' InventoryContractCode, 
							0 CommitmentId, 
							'' CommitmentCode
					RETURN
				END

				INSERT INTO [Inventory].[InventoryContract]
				(
					[Code], [OperatingUnitId], [ContractTypeId], [DocumentDate], [InitialDate], [EndDate], [SupplierId], [SupplierDistributionLineId], [ContractNumber], [Description], 
					[PaymentMethod], [DeliveryMethod], [DeliveryPlace], [SourceOrder], [PurchaseProcess], [Exclusivity], [ManageProducts], [OnlyGuarantee], [TechnicalSupervicion], 
					[SupervisionExecution], [Clauses], [Attachments], [Availability], [Resolution], [ResolutionDate], [QuoteNumber], [QuoteDate], [RecordNumber], [RecordDate], 
					[NegotiationType], [Approved], [Deadline], [ValidityDate], [Value], [DiscountValue], [IvaValue], [TotalValue], [Status], [CreationUser], [CreationDate],
					[ModificationUser], [ModificationDate], [ConfirmationUser], [ConfirmationDate],[CurrencyId]
				)
				SELECT	@Code, @OperatingUnitId, @ContractTypeId, @DocumentDate, @InitialDate, @EndDate, @SupplierId, @SupplierDistributionLineId, @ContractNumber, @Description, 
						@PaymentMethod, @DeliveryMethod, @DeliveryPlace, @SourceOrder, @PurchaseProcess, @Exclusivity, @ManageProducts, @OnlyGuarantee, @TechnicalSupervicion, 
						@SupervisionExecution, @Clauses, @Attachments, @Availability, @Resolution, @ResolutionDate, @QuoteNumber, @QuoteDate, @RecordNumber, @RecordDate, 
						@NegotiationType, @Approved, @Deadline, @ValidityDate, @Value, @DiscountValue, @IvaValue, @TotalValue, @Status, @UserCode, [Common].[GETDATE](),
						@ConfirmationUser, @ConfirmationDate, @ConfirmationUser, @ConfirmationDate,@CurrencyId

				--Obtengo el id de la cabcera
				SET @Id = SCOPE_IDENTITY()
			END
			ELSE --Si se esta actualizando
			BEGIN
				UPDATE [Inventory].[InventoryContract] 
					SET [Code] = @Code, 
						[OperatingUnitId] = @OperatingUnitId, 
						[ContractTypeId] = @ContractTypeId, 
						[DocumentDate] = @DocumentDate,
						[InitialDate] = @InitialDate, 
						[EndDate] = @EndDate, 
						[SupplierId] = @SupplierId, 
						[SupplierDistributionLineId] = @SupplierDistributionLineId, 
						[ContractNumber] = @ContractNumber, 
						[Description] = @Description, 
						[PaymentMethod] = @PaymentMethod, 
						[DeliveryMethod] = @DeliveryMethod, 
						[DeliveryPlace] = @DeliveryPlace, 
						[SourceOrder] = @SourceOrder, 
						[PurchaseProcess] = @PurchaseProcess, 
						[Exclusivity] = @Exclusivity, 
						[ManageProducts] = @ManageProducts, 
						[OnlyGuarantee] = @OnlyGuarantee, 
						[TechnicalSupervicion] = @TechnicalSupervicion, 
						[SupervisionExecution] = @SupervisionExecution, 
						[Clauses] = @Clauses, 
						[Attachments] = @Attachments, 
						[Availability] = @Availability, 
						[Resolution] = @Resolution, 
						[ResolutionDate] = @ResolutionDate, 
						[QuoteNumber] = @QuoteNumber, 
						[QuoteDate] = @QuoteDate, 
						[RecordNumber] = @RecordNumber, 
						[RecordDate] = @RecordDate, 
						[NegotiationType] = @NegotiationType, 
						[Approved] = @Approved, 
						[Deadline] = @Deadline, 
						[ValidityDate] = @ValidityDate, 
						[Value] = @Value, 
						[DiscountValue] = @DiscountValue, 
						[IvaValue] = @IvaValue, 
						[TotalValue] = @TotalValue, 
						[Status] = @Status, 
						[ModificationUser] = @UserCode, 
						[ModificationDate] = [Common].[GETDATE](), 
						[ConfirmationUser] = @ConfirmationUser, 
						[ConfirmationDate] = @ConfirmationDate,
						[CurrencyId]=@CurrencyId
 				WHERE Id = @Id
			END

			/*************************************************************************************/

			--Se actualizan los detalles de productos
			IF EXISTS (SELECT 1 FROM @TableInventoryContractDetail WHERE Id > 0 AND IsDelete = 0)
			BEGIN
				UPDATE cd set cd.[InventoryContractId] = temp.InventoryContractId, cd.[ProductId] = temp.ProductId, cd.[Quantity] = temp.Quantity, cd.[OutstandingQuantity] = temp.OutstandingQuantity,
				cd.[CancelledQuantity] = temp.CancelledQuantity, cd.[Value] = temp.Value, cd.[SubTotalValue] = temp.SubTotalValue, cd.[IvaPercentage] = temp.IvaPercentage, cd.[IvaValue] = temp.IvaValue,
				cd.[DiscountPercentage] = temp.DiscountPercentage, cd.[DiscountValue] = temp.DiscountValue, cd.[TotalValue] = temp.TotalValue
				FROM @TableInventoryContractDetail temp
				JOIN Inventory.InventoryContractDetail cd WITH (NOLOCK) ON cd.Id = temp.Id
				WHERE temp.Id > 0 AND temp.IsDelete = 0
			END

			--Se guardan los detalles de productos
			IF EXISTS (SELECT 1 FROM @TableInventoryContractDetail WHERE Id = 0)
			BEGIN
				INSERT INTO [Inventory].[InventoryContractDetail]([InventoryContractId], [ProductId], [Quantity], [OutstandingQuantity], [CancelledQuantity], [Value], [SubTotalValue], [IvaPercentage],
				[IvaValue], [DiscountPercentage], [DiscountValue], [TotalValue])
				SELECT @Id, ProductId, Quantity, OutstandingQuantity, CancelledQuantity, Value, SubTotalValue, IvaPercentage, IvaValue, DiscountPercentage, DiscountValue, TotalValue
				FROM @TableInventoryContractDetail
				WHERE Id = 0
			END

			--Se actualizan los detalles de disponibilidades
			IF EXISTS (SELECT 1 FROM @TableInventoryContractAvailability WHERE Id > 0 AND IsDelete = 0)
			BEGIN
				UPDATE a set a.[InventoryContractId] = temp.InventoryContractId, a.[AvailabilityDetailId] = temp.AvailabilityDetailId, a.[Value] = temp.Value
				FROM @TableInventoryContractAvailability temp
				JOIN Inventory.InventoryContractAvailability a WITH (NOLOCK) ON a.Id = temp.Id
				WHERE temp.Id > 0 AND temp.IsDelete = 0
			END

			--Se guardan los detalles de disponibilidades
			IF EXISTS (SELECT 1 FROM @TableInventoryContractAvailability WHERE Id = 0)
			BEGIN
				INSERT INTO [Inventory].[InventoryContractAvailability]([InventoryContractId], [AvailabilityDetailId], [Value])
				SELECT @Id, AvailabilityDetailId, Value
				FROM @TableInventoryContractAvailability
				WHERE Id = 0
			END

			--Si se esta confirmando y hay detalles de disponibilidades se genera el compromiso
			IF @Status = 2 AND EXISTS(SELECT 1 FROM @TableInventoryContractAvailability)
			BEGIN
				--Tercero para generar el compromiso
				DECLARE @ThirdParty INT = (SELECT IdThirdParty FROM Common.Supplier WITH (NOLOCK) WHERE Id = @SupplierId)
			

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
									ic.DocumentDate DocumentDate, 
									1 CommitmentType, 
									ic.Description Observations, 
									1 Status, 
									ic.Id EntityId, 
									ic.Code EntityCode, 
									'InventoryContract' EntityName
							FROM Inventory.InventoryContract ic WITH (NOLOCK)
							JOIN Common.Supplier s WITH (NOLOCK) ON ic.SupplierId = s.Id
							WHERE ic.Id = @Id
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
							FROM Inventory.InventoryContractAvailability ica WITH (NOLOCK)
							JOIN Budget.AvailabilityDetail ad WITH (NOLOCK) ON ad.Id = ica.AvailabilityDetailId
							JOIN Budget.Budget b WITH (NOLOCK) ON b.Id = ad.BudgetId
							WHERE ica.InventoryContractId = @Id
						) CommitmentDetail ON CommitmentDetail.CommitmentId = Commitment.Id
						FOR XML AUTO,TYPE, ELEMENTS
					)
				)

				--Tabla de resultado para el sp del compromiso
				DECLARE @ResultCommitment TABLE(CodeMessage INT, Message VARCHAR(MAX), CommitmentId INT, CommitmentCode VARCHAR(20))

				--Se ejecuta el sp del compromiso
				insert @ResultCommitment exec Budget.SP_SaveCommitment @SubXml, '', @UserCode

				--Se valida el resultado
				IF EXISTS (SELECT 1 FROM @ResultCommitment WHERE CodeMessage = 999)
				BEGIN
					set @Message = (SELECT Message FROM @ResultCommitment WHERE CodeMessage = 999)
					SELECT 999 as CodeMessage, @Message as Message, 0 InventoryContractId, '' InventoryContractCode, 0 CommitmentId, '' CommitmentCode
					RETURN
				END

				--Se obtiene el código del compromiso generado
				set @Message_Output = (SELECT top 1 CommitmentCode FROM @ResultCommitment)
			END
		END

		set @Message = case @Status 
								when 1 then 'Se guardó el Contrato con código ' + @Code
								when 2 then 'Se guardó y confirmó el Contrato con código ' + @Code + IIF(ISNULL(@Message_Output, '') = '', '', ' y se generó el compromiso con código ' + @Message_Output)
								when 3 then 'Se anuló el Contrato con código ' + @Code
								WHEN 4 THEN 'Se legalizó el Contrato con código ' + @Code
							 END

		--Se retorna el ok
		SELECT 0 as CodeMessage, @Message as Message, @Id InventoryContractId, @Code InventoryContractCode, 0 CommitmentId, @Message_Output CommitmentCode
	END TRY
	BEGIN CATCH
		--Se retorna el error
		SELECT	999 as CodeMessage, 
				ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) as Message, 
				0 InventoryContractId, 
				'' InventoryContractCode, 
				0 CommitmentId, 
				'' CommitmentCode
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que crea o actualiza contratos de compra de insumos y medicamentos con proveedores en el módulo de inventarios. Recibe los datos del contrato en formato XML (encabezado con condiciones comerciales, vigencias, valores, forma de pago y entrega, cláusulas, resoluciones y cotizaciones) junto con el código del usuario que opera. Gestiona el ciclo completo del contrato: si es nuevo lo inserta en InventoryContract; si ya existe permite modificarlo, anularlo o actualizarlo según el estado. También administra el detalle de productos asociados al contrato (InventoryContractDetail), manejando las líneas de ítems con cantidades, precios unitarios, IVA, descuentos y totales, incluyendo la marcación de líneas eliminadas. Consulta la moneda oficial de la compañía desde CompanySettings y valida que el proveedor exista en Common.Supplier antes de registrar el contrato por unidad operativa.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveInventoryContract';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveInventoryContract';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste (crea/actualiza), anula o legaliza un contrato de inventario con su detalle de productos y disponibilidades presupuestales, validando saldos por rubro y generando el compromiso presupuestal cuando se confirma.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInventoryContract';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe contener un nodo /InventoryContract con la cabecera y opcionalmente nodos InventoryContractDetail e InventoryContractAvailability.; Para legalizar (Status=4), debe existir el contrato con Id=@Id y Status=2 (Confirmado).; Para crear/modificar, el contrato (si existe) debe estar en Status=1 (Borrador); no se permite si está Confirmado, Anulado o Legalizado.; Si se incluyen disponibilidades, todas deben pertenecer a la misma BudgetaryValidityId que la enviada en la cabecera.; La sumatoria de Value de las disponibilidades debe igualar ROUND(@TotalValue,0).; El saldo (Balance) de cada AvailabilityDetail debe ser >= valor a ejecutar de esa disponibilidad.; Para cada BudgetId con productos asociados, la sumatoria de TotalValue de los detalles no puede exceder la sumatoria de Value de las disponibilidades del mismo BudgetId.; Para confirmación con compromiso, el SupplierId debe existir en Common.Supplier para obtener el IdThirdParty.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInventoryContract';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Inventory.InventoryContract: Cuando @Status=3 (anular) se actualizan Status, ModificationUser/Date y AnnulmentUser/Date con el usuario y fecha actual donde Id=@Id.; [UPDATE] Inventory.InventoryContract: Cuando @Status=4 (legalizar) y existe el contrato en Status=2, se actualizan InitialDate, EndDate, TechnicalSupervicion, SupervisionExecution y Status=4.; [INSERT] Inventory.InventoryContract: Cuando @Id=0 se obtiene un Code vía Common.SP_GetSequence(190,1401,…) y se inserta la cabecera; si @Status=2 se llenan ConfirmationUser/Date con el usuario y fecha actual.; [UPDATE] Inventory.InventoryContract: Cuando @Id<>0 y @Status no es 3 ni 4, se actualizan todos los campos de la cabecera incluido CurrencyId; ConfirmationUser/Date se setean solo si @Status=2.; [DELETE] Inventory.InventoryContractDetail: Se eliminan las filas cuyas Id coinciden con líneas del XML marcadas con IsDelete=1.; [UPDATE] Inventory.InventoryContractDetail: Para líneas con Id>0 y IsDelete=0 se actualizan cantidades, valores, IVA y descuentos del detalle.; [INSERT] Inventory.InventoryContractDetail: Para líneas con Id=0 se insertan nuevas filas asociadas al InventoryContractId recién creado/actualizado.; [DELETE] Inventory.InventoryContractAvailability: Se eliminan las disponibilidades asociadas marcadas con IsDelete=1 en el XML.; [UPDATE] Inventory.InventoryContractAvailability: Para disponibilidades con Id>0 y IsDelete=0 se actualizan AvailabilityDetailId y Value.; [INSERT] Inventory.InventoryContractAvailability: Para disponibilidades con Id=0 se insertan nuevas filas vinculadas al contrato.; [RETURN_RESULT] Budget.SP_SaveCommitment: Cuando @Status=2 y existen disponibilidades, se construye un XML de compromiso (DocumentSource=3, CommitmentType=1, EntityName=''InventoryContract'') y se ejecuta Budget.SP_SaveCommitment para registrar el compromiso presupuestal.; [RETURN_RESULT] ResultSet: Devuelve CodeMessage=999 con mensaje descriptivo cuando: el contrato no está en estado válido, las disponibilidades pertenecen a más de una vigencia, la sumatoria no coincide con el total, los rubros de productos exceden los de disponibilidades, el saldo es insuficiente, o falla SP_GetSequence/SP_SaveCommitment.; [RETURN_RESULT] ResultSet: Al éxito devuelve CodeMessage=0 con mensaje según @Status (guardado, confirmado y compromiso, anulado o legalizado), InventoryContractId, InventoryContractCode y CommitmentCode.; [RETURN_RESULT] ResultSet: En CATCH devuelve CodeMessage=999 con ERROR_MESSAGE() y línea del error.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInventoryContract';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInventoryContract';
-- GO
