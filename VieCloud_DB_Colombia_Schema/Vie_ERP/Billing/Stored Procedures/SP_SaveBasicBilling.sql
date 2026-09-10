-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2018-11-18
-- Description:	Procedimiento que se encarga de guardar, actualizar la factura basica
-- =============================================
CREATE PROCEDURE [Billing].[SP_SaveBasicBilling] 
    @EntityXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	SET DATEFORMAT DMY
	--Se declaran las variables para obtener la cabecera
	DECLARE @Id INT, 
			@Code VARCHAR(20),
			@DocumentDate DATETIME,
			@Description VARCHAR(MAX),
			@SaleModality TINYINT,
			@Status TINYINT,
			@OperatingUnitId INT,
			@BillingAuthorizationId INT,
			@FunctionalUnitId INT,
			@CustomerId INT,
			@ThirdPartyId int,
			@AddressId INT,
			@Value DECIMAL(18,2),
			@ValueDiscount DECIMAL(18,2),
			@ValueIVA DECIMAL(18,2),
			@WithholdingTax DECIMAL(18,2),
			@RetentionIdIVA INT,
			@RetentionPercentageIVA DECIMAL(6,3),
			@WithholdingIVA DECIMAL(18,2),
			@WithholdingICA DECIMAL(18,2),
			@TotalValue DECIMAL(18,2),
			@RoundLevel INT,
			@BudgetId INT,
			@CurrencyId INT,
			@ThirdPartyEntityCopayId INT,
            @InvoiceId INT,
			@ConditionSalesId INT,
			@EconomicActivityId INT,
			@IsImported BIT,
			@IsMandateBilling BIT,
			@MandatorThirdPartyId INT,
			@IsEconommicActivity Bit;

	--Tabla temporal de BasicBillingDetail
	DECLARE @BasicBillingDetail TABLE
	(
		[Id] [int],
		[TempId] [int],
		[BasicBillingId] [int],
		[DetailType] [tinyint],
		[ProductId] [int],
		[BillingConceptId] [int],
		[PhysicalAssetId] [int],
		[PhysicalAssetPartId] [int],
		[Quantity] [int],
		[Price] [numeric](18,2),
		[Value] [numeric](18,2),
		[PercentageDiscount] [numeric](5,2),
		[ValueDiscount] [numeric](18, 2),
		[PercentageIVA] [numeric](5,2),
		[RetentionIdTax] [int],
		[RetentionPercentageTax] [decimal](6,3),
		[WithholdingTax] [decimal](18, 2),
		[RetentionIdICA] [int],
		[RetentionPercentageICA] [decimal](6,3),
		[WithholdingICA] [decimal](18, 2),
		[WarehouseId][int],
		[ServicesProvidedId][int],
		[SupplierId][int],
		[SalesExecutiveId][int],
		[FeeId][int],
		[FunctionalUnitId][int],
		[IsDelete] [bit],
		[EconomicActivityId][int] null
	)

	--Tabla temporal de BasicBillingDetailItem
	DECLARE @BasicBillingDetailItem TABLE
	(
		[Id] [int],
		[ParentId] [int],
		[BasicBillingDetailId] [int],
		[PhysicalInventoryId] [int],
		[Quantity] [int]
	)

	DECLARE @BasicBillingGifts TABLE
	(
		[Id] [int],
		[TempId] [int],
		[BasicBillingId] [int],
		[ProductId] [int],
		[Quantity] [int],
		[WarehouseId][int],
		[IsDelete] [bit]
	)

	DECLARE @BasicBillingGiftsItem TABLE
	(
		[Id] [int],
		[ParentId] [int],
		[BasicBillingGiftsId] [int],
		[PhysicalInventoryId] [int],
		[Quantity] [int]
	)

	--Tabla temporal para obtener el listado de ids eliminados
	DECLARE @ListBasicBillingDetailDelete TABLE(Id INT)

	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT 
			@Id = t.x.value('Id[1]','int'),
			@Code = t.x.value('Code[1]','varchar(20)'),
			@DocumentDate = t.x.value('DocumentDate[1]','datetime'),
			@Description = t.x.value('Description[1]','varchar(max)'),
			@SaleModality = t.x.value('SaleModality[1]','tinyint'),
			@Status = t.x.value('Status[1]','tinyint'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
			@BillingAuthorizationId = t.x.value('BillingAuthorizationId[1]','int'),
			@FunctionalUnitId = t.x.value('FunctionalUnitId[1]','int'),
			@CustomerId = t.x.value('CustomerId[1]','int'),
			@ThirdPartyId = t.x.value('ThirdPartyId[1]','int'),
			@AddressId = t.x.value('AddressId[1]','int'),
			@Value = t.x.value('Value[1]','decimal(18,2)'),
			@ValueDiscount = t.x.value('ValueDiscount[1]','decimal(18,2)'),
			@ValueIVA = t.x.value('ValueIVA[1]','decimal(18,2)'),
			@WithholdingTax = t.x.value('WithholdingTax[1]','decimal(18,2)'),
			@RetentionIdIVA = t.x.value('RetentionIdIVA[1]','int'),
			@RetentionPercentageIVA = t.x.value('RetentionPercentageIVA[1]','decimal(6,3)'),
			@WithholdingIVA = t.x.value('WithholdingIVA[1]','decimal(18,2)'),
			@WithholdingICA = t.x.value('WithholdingICA[1]','decimal(18,2)'),
			@TotalValue = t.x.value('TotalValue[1]','decimal(18,2)'),
			@RoundLevel = t.x.value('RoundLevel[1]','int'),
			@BudgetId = t.x.value('BudgetId[1]','int'),
			@CurrencyId = t.x.value('CurrencyId[1]','int'),
			@ThirdPartyEntityCopayId = iif(t.x.value('ThirdPartyEntityCopayId[1]','varchar(20)') = '', NULL, t.x.value('ThirdPartyEntityCopayId[1]','int')),
            @InvoiceId = iif(t.x.value('InvoiceId[1]','varchar(20)') = '', NULL, t.x.value('InvoiceId[1]','int')),
			@ConditionSalesId = iif(t.x.value('ConditionSalesId[1]','varchar(20)') = '', NULL, t.x.value('ConditionSalesId[1]','int')),
			@EconomicActivityId = iif(t.x.value('EconomicActivityId[1]','varchar(20)') = '', NULL, t.x.value('EconomicActivityId[1]','int')),
			@IsImported = t.x.value('IsImported[1]','bit'),
			@IsMandateBilling = t.x.value('IsMandateBilling[1]','bit'),
			@MandatorThirdPartyId = iif(t.x.value('MandatorThirdPartyId[1]','varchar(20)') = '', NULL, t.x.value('MandatorThirdPartyId[1]','int'))
		FROM @EntityXml.nodes('/BasicBilling') t(x)

		IF @IsMandateBilling = 0
			SET @MandatorThirdPartyId = NULL

		IF @IsMandateBilling = 1 AND @MandatorThirdPartyId IS NULL
		BEGIN
			SELECT 999 as CodeMessage, 'Debe seleccionar el Tercero Mandante' as Message, '' as Code, 0 as Id
			RETURN
		END

		IF EXISTS (SELECT 1 FROM Billing.BasicBilling bb WHERE bb.Id = @Id AND bb.Status <> 1)
		BEGIN
			SELECT 999 as CodeMessage, 'El registro se encuentra en estado: ' + IIF(bb.Status = 2, 'Confirmado', 'Anulado') as Message, '' as Code, 0 as Id
			FROM Billing.BasicBilling bb
			WHERE bb.Id = @Id
			RETURN
		END
		--Se Obtiene parametros del tenant
		Set @IsEconommicActivity = (SELECT TOP 1 TransactionEconomicActivity FROM GeneralLedger.CompanySettings)

		-- Obtener tolerancia y precisión de redondeo basada en la moneda
		DECLARE @RoundTolerance DECIMAL(18,2) = 0.01
		DECLARE @RoundPrecision INT = 2  -- Precisión para ROUND de SQL (número de decimales)
		IF @CurrencyId IS NOT NULL AND @CurrencyId > 0
		BEGIN
			SELECT @RoundTolerance = Common.GetRoundTolerance(c.RoundingType),
				   @RoundPrecision = Common.GetRoundPrecision(c.RoundingType)
			FROM Common.Currency c WITH(NOLOCK)
			WHERE c.Id = @CurrencyId
		END
		SET @RoundTolerance = ISNULL(@RoundTolerance, 0.01)
		SET @RoundPrecision = ISNULL(@RoundPrecision, 2)

		--Si no se esta anulando
		IF @Status <> 3
		BEGIN
			--Se obtiene BasicBillingDetail
			INSERT INTO @BasicBillingDetail
				SELECT 
					t.x.value('Id[1]','int') as Id,
					t.x.value('TempId[1]','int') as TempId,
					t.x.value('BasicBillingId[1]','int') as BasicBillingId,
					t.x.value('DetailType[1]','tinyint') as DetailType,
					t.x.value('ProductId[1]','int') as ProductId,
					t.x.value('BillingConceptId[1]','int') as BillingConceptId,
					t.x.value('PhysicalAssetId[1]','int') as PhysicalAssetId,
					t.x.value('PhysicalAssetPartId[1]','int') as PhysicalAssetPartId,
					t.x.value('Quantity[1]','int') as Quantity,
					t.x.value('Price[1]','decimal(18,2)') as Price,
					t.x.value('Value[1]','decimal(18,2)') as Value,
					t.x.value('PercentageDiscount[1]','decimal(5,2)') as PercentageDiscount,
					t.x.value('ValueDiscount[1]','decimal(18,2)') as ValueDiscount,
					t.x.value('PercentageIVA[1]','decimal(5,2)') as PercentageIVA,
					IIF(t.x.value('RetentionIdTax[1]','int')='',NULL,t.x.value('RetentionIdTax[1]','int')) as RetentionIdTax,
					t.x.value('RetentionPercentageTax[1]','decimal(6,3)') as RetentionPercentageTax,
					t.x.value('WithholdingTax[1]','decimal(18,2)') as WithholdingTax,
					t.x.value('RetentionIdICA[1]','int') as RetentionIdICA,
					t.x.value('RetentionPercentageICA[1]','decimal(6,3)') as RetentionPercentageICA,
					t.x.value('WithholdingICA[1]','decimal(18,2)') as WithholdingICA,
					t.x.value('WarehouseId[1]','int') as WarehouseId,
					t.x.value('ServicesProvided[1]','int') as ServicesProvidedId,
					t.x.value('Supplier[1]','int') as SupplierId,
					t.x.value('SalesExecutive[1]','int') as SalesExecutiveId,
					t.x.value('FeeId[1]','int') as FeeId,
					t.x.value('FunctionalUnitId[1]','int') as FunctionalUnitId,
					t.x.value('IsDelete[1]','bit') as IsDelete,
					t.x.value('EconomicActivityId[1]', 'int') as EconomicActivityId
				FROM @EntityXml.nodes('/BasicBilling/BasicBillingDetail') t(x)

			--Se obtiene BasicBillingDetailItem
			INSERT INTO @BasicBillingDetailItem
				SELECT 
					t.x.value('Id[1]','int') as Id,
					t.x.value('ParentId[1]','int') as TempId,
					t.x.value('BasicBillingDetailId[1]','int') as BasicBillingDetailId,
					t.x.value('PhysicalInventoryId[1]','int') as PhysicalInventoryId,
					t.x.value('Quantity[1]','int') as Quantity
				FROM @EntityXml.nodes('/BasicBilling/BasicBillingDetail/BasicBillingDetailItem') t(x)

			--Se obtiene BasicBillingGifts
			INSERT INTO @BasicBillingGifts
				SELECT 
					t.x.value('Id[1]','int') as Id,
					t.x.value('TempId[1]','int') as TempId,
					t.x.value('BasicBillingId[1]','int') as BasicBillingId,
					t.x.value('ProductId[1]','int') as ProductId,
					t.x.value('Quantity[1]','int') as Quantity,
					t.x.value('WarehouseId[1]','int') as WarehouseId,
					t.x.value('IsDelete[1]','bit') as IsDelete
				FROM @EntityXml.nodes('/BasicBilling/BasicBillingGifts') t(x)

			INSERT INTO @BasicBillingGiftsItem
				SELECT 
					t.x.value('Id[1]','int') as Id,
					t.x.value('ParentId[1]','int') as TempId,
					t.x.value('BasicBillingGiftsId[1]','int') as BasicBillingGiftsId,
					t.x.value('PhysicalInventoryId[1]','int') as PhysicalInventoryId,
					t.x.value('Quantity[1]','int') as Quantity
				FROM @EntityXml.nodes('/BasicBilling/BasicBillingGifts/BasicBillingGiftsItem') t(x)

			DELETE bbdi
			FROM Billing.BasicBillingDetailItem bbdi
			JOIN @BasicBillingDetail bbd ON bbdi.BasicBillingDetailId = bbd.Id  
			WHERE bbd.IsDelete = 1

			DELETE bb
			FROM Billing.BasicBillingDetail bb
			JOIN @BasicBillingDetail bbd ON bbd.Id = bb.Id
			WHERE bbd.IsDelete = 1

			DELETE bbgi
			FROM Billing.BasicBillingGiftsItem bbgi
			JOIN @BasicBillingGifts bbg ON bbgi.BasicBillingGiftsId = bbg.Id  
			WHERE bbg.IsDelete = 1

			DELETE bbgi
			FROM Billing.BasicBillingGifts bbgi
			JOIN @BasicBillingGifts bbg ON bbgi.Id = bbg.Id
			WHERE bbg.IsDelete = 1

			DELETE FROM @BasicBillingGifts WHERE IsDelete = 1
			DELETE FROM @BasicBillingDetail WHERE IsDelete = 1

			DECLARE @CommercialDiscountAccounting BIT
			SELECT @CommercialDiscountAccounting = sb.AccountsConditionalCommercialDiscount
			FROM Billing.SettingsBilling sb
			WHERE sb.IdOperatingUnit = @OperatingUnitId

			/*Creación del cliente si no se envía*/
			if @CustomerId = 0 begin
				if isnull(@ThirdPartyId, 0) = 0 begin
					select 999 as CodeMessage, 'No se puede crear el cliente ya que no se ha enviado el tercero' as Message, '' as Code, 0 as Id
					return
				end

				declare @MainAccountReceivableId int

				if @InvoiceId is not NULL begin
					select top 1  @MainAccountReceivableId = sas.AccountRecoveryFeeId 
					from Contract.ContractAccountingStructure sas WITH(NOLOCK) 
					join Contract.CareGroup cp WITH(NOLOCK) on sas.Id =cp.ContractAccountingStructureId
					join Billing.Invoice i WITH(NOLOCK) on cp.Id =i.CareGroupId 
					where i.Id =@InvoiceId
				end
				else BEGIN
				select top 1 @MainAccountReceivableId = ClientMainAccountId from Billing.SettingsBilling WITH(NOLOCK)
				where IdOperatingUnit = @OperatingUnitId
				END

				if @MainAccountReceivableId is null begin
					select 999 as CodeMessage, 'No se puede crear el cliente ya que no existe una cuenta contable para asociar' as Message, '' as Code, 0 as Id
					return
				end

				insert into common.Customer (Nit, [Name], ThirdPartyId, MainAccountReceivableId, Term, State, CreationUser, CreationDate)
				select top 1 Nit, [Name], Id, @MainAccountReceivableId, 30, 1, @CodeUser, Common.GETDATE() from Common.ThirdParty where Id = @ThirdPartyId

				set @CustomerId = scope_identity()
			end

			/*************************************VALIDACIONES************************************/

			IF ISNULL(@RetentionIdIVA, 0) = 0
			BEGIN
				SELECT 999 as CodeMessage, 'No se ha parametrizado el concepto de retencion de IVA' as Message, '' as Code, 0 as Id
				RETURN
			END

			IF EXISTS 
			( 
				SELECT bc.Id FROM Billing.BillingConcept bc
				JOIN @BasicBillingDetail bbd ON bbd.BillingConceptId = bc.Id 	
				WHERE ConceptType = 1 
			) 
				BEGIN

				IF EXISTS
				(
					SELECT bbdt.TempId
					FROM @BasicBillingDetail bbdt
					WHERE ISNULL(bbdt.RetentionIdTax, 0) = 0 OR ISNULL(bbdt.RetentionIdICA, 0) = 0 AND NOT bbdt.DetailType = 3
				)
				BEGIN
					SELECT 999 as CodeMessage, 'No se ha parametrizado los conceptos de retencion en algunos de los detalles' as Message, '' as Code, 0 as Id
					RETURN
				END
			END

			--Validacion para los detalles de tipo - Activos Fijos cuando el tercero de la cabecera maneja ICA
			IF EXISTS
				(
					SELECT bbdt.TempId
					FROM @BasicBillingDetail bbdt
					JOIN Common.ThirdParty t ON t.Id = @ThirdPartyId
					WHERE (ISNULL(bbdt.RetentionIdTax, 0) = 0 OR ISNULL(bbdt.RetentionIdICA, 0) = 0 ) AND bbdt.DetailType = 3 AND t.Ica = 1
				)
				BEGIN
					SELECT 999 as CodeMessage, 'No se ha parametrizado los conceptos de retencion del detalle tipo: Activo Fijo' as Message, '' as Code, 0 as Id
					RETURN
				END

			IF EXISTS
			(
				SELECT bbdt.TempId
				FROM @BasicBillingDetailItem bbdit
				JOIN @BasicBillingDetail bbdt ON bbdit.ParentId = bbdt.TempId
				GROUP BY bbdt.TempId, bbdt.Quantity
				HAVING bbdt.Quantity <> SUM(bbdit.Quantity)
			)
			BEGIN
				SELECT 999 as CodeMessage, 'Existen registros donde la cantidad del lote no corresponde con la cantidad del detalle' as Message, '' as Code, 0 as Id
				RETURN
			END

			IF EXISTS
			(
				SELECT bbg.TempId
				FROM @BasicBillingGiftsItem bbgt
				JOIN @BasicBillingGifts bbg ON bbgt.ParentId = bbg.TempId
				GROUP BY bbg.TempId, bbg.Quantity
				HAVING bbg.Quantity <> SUM(bbgt.Quantity)
			)
			BEGIN
				SELECT 999 as CodeMessage, 'Existen registros de obsequios donde la cantidad del lote no corresponde con la cantidad del detalle' as Message, '' as Code, 0 as Id
				RETURN
			END

			IF EXISTS
			(
				--Se debe validar si aplica o no a las retenciones, y si estos superan la base (con tolerancia de redondeo)
				SELECT 1
				FROM @BasicBillingDetail bbd
				WHERE ABS(bbd.Value - (bbd.Quantity * bbd.Price)) > @RoundTolerance OR
					ABS(bbd.ValueDiscount - ROUND(bbd.Value * bbd.PercentageDiscount / 100, @RoundPrecision)) > @RoundTolerance OR
					(bbd.WithholdingTax > 0 AND ABS(bbd.WithholdingTax - ROUND((bbd.Value - bbd.ValueDiscount) * bbd.RetentionPercentageTax / 100, @RoundPrecision)) > @RoundTolerance) OR
					(bbd.WithholdingICA > 0 AND ABS(bbd.WithholdingICA - ROUND((bbd.Value - bbd.ValueDiscount) * bbd.RetentionPercentageICA / 100, @RoundPrecision)) > @RoundTolerance)
			)
			BEGIN
				SELECT 999 as CodeMessage, 'Los calculos de los detalles no son correctos' as Message, '' as Code, 0 as Id
				RETURN
			END

			-- Validar cálculos de cabecera con tolerancia de redondeo
			IF ABS((@Value - @ValueDiscount + @ValueIVA - @WithholdingTax - @WithholdingIVA - @WithholdingICA) - @TotalValue) > @RoundTolerance
			BEGIN
				SELECT 999 as CodeMessage, 'Los calculos de la cabecera no son correctos' as Message, '' as Code, 0 as Id
				RETURN
			END
			
			-- Validar que los totales de los detalles correspondan con los valores de la cabecera (con tolerancia de redondeo)
			-- El IVA se calcula sumando primero y aplicando redondeo al total (coherente con presentación)
			IF EXISTS
			(
				SELECT 1
				FROM
				(
					SELECT 
						SUM(bbd.Value) Value, 
						SUM(bbd.ValueDiscount) ValueDiscount, 
						-- IVA: redondear por ítem antes de sumar (coherente con presentación)
						SUM(ROUND((bbd.Value - bbd.ValueDiscount) * bbd.PercentageIVA / 100, @RoundPrecision)) ValueIVA,
						SUM(bbd.WithholdingTax) WithholdingTax, 
						SUM(bbd.WithholdingICA) WithholdingICA
					FROM
					(
						SELECT 
							bbd.Value, 
							bbd.ValueDiscount, 
							bbd.PercentageIVA,
							bbd.WithholdingTax, 
							bbd.WithholdingICA
						FROM @BasicBillingDetail bbd
						UNION ALL
						SELECT 
							bbd.Value, 
							bbd.ValueDiscount, 
							bbd.PercentageIVA,
							bbd.WithholdingTax, 
							bbd.WithholdingICA
						FROM Billing.BasicBillingDetail bbd
						LEFT JOIN @BasicBillingDetail bbdt ON bbd.Id = bbdt.Id
						WHERE bbd.BasicBillingId = @Id AND bbdt.Id IS NULL
					) bbd
				) bbd
				WHERE ABS(bbd.Value - @Value) > @RoundTolerance 
				   OR ABS(bbd.ValueDiscount - @ValueDiscount) > @RoundTolerance 
				   OR ABS(bbd.ValueIVA - @ValueIVA) > @RoundTolerance 
				   OR ABS(bbd.WithholdingTax - @WithholdingTax) > @RoundTolerance 
				   OR ABS(bbd.WithholdingICA - @WithholdingICA) > @RoundTolerance
			)
			BEGIN
				SELECT 999 as CodeMessage, 'Los Totales no corresponden con los valores de la cabecera' as Message, '' as Code, 0 as Id
				RETURN
			END

			-- Ajuste automático de WithholdingIVA por diferencias de redondeo
			-- Calcula la retención sobre el IVA total (coherente con presentación)
			DECLARE @WithholdingIVARound DECIMAL(18,2) = 0
			DECLARE @IVATotalForRetention DECIMAL(18,2) = 0
			
			-- IVA total: redondear por ítem antes de sumar
			SELECT @IVATotalForRetention = SUM(ROUND((bbd.Value - bbd.ValueDiscount) * bbd.PercentageIVA / 100, @RoundPrecision))
			FROM 
			(
				SELECT bbd.Value, bbd.ValueDiscount, bbd.PercentageIVA
				FROM @BasicBillingDetail bbd
				UNION ALL
				SELECT bbd.Value, bbd.ValueDiscount, bbd.PercentageIVA
				FROM Billing.BasicBillingDetail bbd
				LEFT JOIN @BasicBillingDetail bbdt ON bbd.Id = bbdt.Id
				WHERE bbd.BasicBillingId = @Id AND bbdt.Id IS NULL
			) bbd
			
			-- Calcular la retención sobre el IVA total
			SET @WithholdingIVARound = ROUND(@IVATotalForRetention * @RetentionPercentageIVA / 100, @RoundPrecision)

			-- Ajustar WithholdingIVA si la diferencia está dentro de la tolerancia permitida
			IF ABS(@WithholdingIVA - @WithholdingIVARound) > 0 AND ABS(@WithholdingIVA - @WithholdingIVARound) <= @RoundTolerance * 10
			BEGIN
				SET @WithholdingIVA = @WithholdingIVARound
				SET @TotalValue = (@Value - @ValueDiscount + @ValueIVA - @WithholdingTax - @WithholdingIVA - @WithholdingICA)
			END
			
			-- Ajuste automático de ValueIVA por diferencias de redondeo
			-- IVA: redondear por ítem antes de sumar (coherente con presentación)
			DECLARE @ValueIVACalculated DECIMAL(18,2) = 0
			SELECT @ValueIVACalculated = SUM(ROUND((bbd.Value - bbd.ValueDiscount) * bbd.PercentageIVA / 100, @RoundPrecision))
			FROM 
			(
				SELECT bbd.Value, bbd.ValueDiscount, bbd.PercentageIVA
				FROM @BasicBillingDetail bbd
				UNION ALL
				SELECT bbd.Value, bbd.ValueDiscount, bbd.PercentageIVA
				FROM Billing.BasicBillingDetail bbd
				LEFT JOIN @BasicBillingDetail bbdt ON bbd.Id = bbdt.Id
				WHERE bbd.BasicBillingId = @Id AND bbdt.Id IS NULL
			) bbd

			-- Ajustar ValueIVA si la diferencia está dentro de la tolerancia permitida
			IF ABS(@ValueIVA - @ValueIVACalculated) > 0 AND ABS(@ValueIVA - @ValueIVACalculated) <= @RoundTolerance * 10
			BEGIN
				SET @ValueIVA = @ValueIVACalculated
				SET @TotalValue = (@Value - @ValueDiscount + @ValueIVA - @WithholdingTax - @WithholdingIVA - @WithholdingICA)
			END

			/*************************************************************************************/

			--Si se esta insertando por primera vez se consulta la secuencia numerica
				IF @Code = '' or @code is null
				BEGIN
					--Consultamos si la secuencia es con O o OU
					DECLARE @scope varchar(5) = '',
							@idSequenceDetail int,
							@pattern varchar(300),
							@NextS int,
							@IdForm varchar(5) = '2038'
				
					SELECT @scope = Scope 
					FROM Billing.BillingSequence
					WHERE IdForm = @IdForm

					DECLARE @ReservedSequence TABLE
					(
						Id INT,
						IdSequense INT,
						NextS INT
					)

					--Se valida el scope
					IF @scope = 'O'
					BEGIN
						-- Si el ambito es por organización
						;WITH SequenceToReserve AS
						(
							SELECT TOP (1) bsd.Id, bsd.IdSequense, bsd.[Next]
							FROM Billing.BillingSequenceDetail bsd
							JOIN Billing.BillingSequence bs ON bs.Id = bsd.IdSequenseBillingC
							WHERE bs.IdForm = @IdForm
							ORDER BY bsd.Id
						)
						UPDATE SequenceToReserve
						SET [Next] = [Next] + 1
						OUTPUT inserted.Id, inserted.IdSequense, deleted.[Next]
						INTO @ReservedSequence
					END
					ELSE BEGIN
						-- Si el ambito es por unidad operativa
						;WITH SequenceToReserve AS
						(
							SELECT TOP (1) bsd.Id, bsd.IdSequense, bsd.[Next]
							FROM Billing.BillingSequenceDetail bsd
							JOIN Billing.BillingSequence bs ON bs.Id = bsd.IdSequenseBillingC
							WHERE bs.IdForm = @IdForm AND bsd.IdOperatingUnit = @OperatingUnitId
							ORDER BY bsd.Id
						)
						UPDATE SequenceToReserve
						SET [Next] = [Next] + 1
						OUTPUT inserted.Id, inserted.IdSequense, deleted.[Next]
						INTO @ReservedSequence
					END

					SELECT TOP (1)
						@pattern = cs.Pattern,
						@NextS = rs.NextS,
						@idSequenceDetail = rs.Id
					FROM @ReservedSequence rs
					JOIN Common.Sequense cs ON cs.Id = rs.IdSequense

					IF (@idSequenceDetail IS NULL)
					BEGIN
						SELECT 999 as CodeMessage, 'Secuencia de Facturación Básica no encontrada' as Message, '' as Code, 0 as Id
						RETURN
					END

					SELECT @Code = dbo.GetSequence('', @pattern,@NextS)

				END

							
			IF @Id = 0 or @Id is NULL --Se inserta la cabecera para la factura basica
			BEGIN 
				INSERT INTO [Billing].[BasicBilling]([Code],[DocumentDate],[Description],[SaleModality],[Status],[OperatingUnitId],
				[BillingAuthorizationId],[FunctionalUnitId],[CustomerId],[AddressId],[WarehouseId],[Value],[ValueDiscount],[ValueIVA],
				[WithholdingTax],[RetentionIdIVA],[RetentionPercentageIVA],[WithholdingIVA],[WithholdingICA],[TotalValue],[CreationUser],
				[CreationDate],[RoundLevel],[BudgetId],[CurrencyId],[ThirdPartyEntityCopayId],[CommercialDiscountAccounting], [InvoiceId],
				[ConditionSalesId],[EconomicActivityId],[IsImported],[IsMandateBilling],[MandatorThirdPartyId])
				VALUES (@Code,@DocumentDate,@Description,@SaleModality,@Status,@OperatingUnitId,@BillingAuthorizationId,@FunctionalUnitId,
				@CustomerId,@AddressId,NULL,@Value,@ValueDiscount,@ValueIVA,@WithholdingTax,@RetentionIdIVA,@RetentionPercentageIVA,
				@WithholdingIVA,@WithholdingICA,@TotalValue,@CodeUser,[Common].[GETDATE](),@RoundLevel,@BudgetId,@CurrencyId, @ThirdPartyEntityCopayId,
				@CommercialDiscountAccounting, @InvoiceId, @ConditionSalesId,@EconomicActivityId, @IsImported,@IsMandateBilling,@MandatorThirdPartyId)

				SET @Id = SCOPE_IDENTITY()
			END
			ELSE BEGIN
				UPDATE [Billing].[BasicBilling] SET [Code] = @Code,[DocumentDate] = @DocumentDate,[Description] = @Description,
				[SaleModality] = @SaleModality,[Status] = @Status,[OperatingUnitId] = @OperatingUnitId,[BillingAuthorizationId] = @BillingAuthorizationId,
				[FunctionalUnitId] = NULL,[CustomerId] = @CustomerId,[AddressId] = @AddressId,[WarehouseId] = NULL,[Value] = @Value, 
				[ValueDiscount] = @ValueDiscount,[ValueIVA] = @ValueIVA,[WithholdingTax] = @WithholdingTax,[RetentionIdIVA] = @RetentionIdIVA,
				[RetentionPercentageIVA] = @RetentionPercentageIVA,[WithholdingIVA] = @WithholdingIVA,[WithholdingICA] = @WithholdingICA,
				[TotalValue] = @TotalValue,[ModificationUser] = @CodeUser,[ModificationDate] = [Common].[GETDATE](),[RoundLevel] = @RoundLevel,
				[BudgetId] = @BudgetId, [CurrencyId] = @CurrencyId, [CommercialDiscountAccounting] = @CommercialDiscountAccounting,
				[ConditionSalesId] = @ConditionSalesId,[EconomicActivityId] = @EconomicActivityId,[IsImported] = @IsImported,
				[IsMandateBilling] = @IsMandateBilling,[MandatorThirdPartyId] = @MandatorThirdPartyId
				WHERE Id = @Id
			END

			--Tabla para almacenar los errores de la actividad económica
			DECLARE @ErrorsTable TABLE ( 
				CodeMessage INT,
				Message VARCHAR(MAX),
				Code VARCHAR(20),
				Id INT
			);

			--Se valida la configuración del Tenant para la actividad económica
			IF @IsEconommicActivity = 1
			BEGIN
				--Se actualiza el Id de la actividad económica con el asociado al grupo del producto
				UPDATE @BasicBillingDetail SET EconomicActivityId = INPG.EconomicActivityId
				FROM @BasicBillingDetail B
				JOIN Inventory.InventoryProduct INPR ON B.ProductId = INPR.ID
				JOIN Inventory.ProductGroup INPG ON INPR.ProductGroupId = INPG.ID

				--Se declara el cursor para recorrer los detalles
				DECLARE cursor_bbDetail CURSOR
				FOR
				
				SELECT ProductId, EconomicActivityId
				FROM @BasicBillingDetail
				

				-- Declaración de variables para almacenar los valores del cursor
				DECLARE @ProductIdC Int, @EconomicActivityIdC Int;

				OPEN cursor_bbDetail;

				FETCH NEXT FROM cursor_bbDetail INTO @ProductIdC, @EconomicActivityIdC;

				
				WHILE @@FETCH_STATUS = 0
				BEGIN
					IF ( @EconomicActivityIdC IS NULL ) --De no existir actividad económica para dicho detalle llenamos la tabla de errores
						BEGIN
							INSERT INTO @ErrorsTable (CodeMessage, [Message], Code, Id)
							SELECT 
								999, 
								CONCAT('No se tiene definida una Actividad Económica Generadora de Ingreso en el Grupo ', pg.Name, ' asociado al Producto ', p.Name),
								3, 
								0
								FROM @BasicBillingDetail bbd
								JOIN Inventory.InventoryProduct p on p.Id = bbd.ProductId
								JOIN Inventory.ProductGroup pg on pg.Id = p.ProductGroupId
								WHERE p.id = @ProductIdC;								

					END

					FETCH NEXT FROM cursor_bbDetail INTO @ProductIdC, @EconomicActivityIdC;
				END;

				CLOSE cursor_bbDetail;

				DEALLOCATE cursor_bbDetail;
			END;

			--Validación de la configuración del Tenant para la actividad económica
			IF @IsEconommicActivity = 1
			BEGIN
				--Actualiza el Id de la actividad económica de los detalles con el asociado al Concepto de Facturación correspondiente
				UPDATE @BasicBillingDetail SET EconomicActivityId = BC.EconomicActivityId
				FROM @BasicBillingDetail bbd
				JOIN Billing.BillingConcept BC ON bbd.BillingConceptId = BC.Id

				--Declaramos el cursor
				DECLARE cursor_bbDetailS CURSOR
				FOR
				
				SELECT BasicBillingId, EconomicActivityId
				FROM @BasicBillingDetail
				

				-- Declaración de variables para almacenar los valores del cursor
				DECLARE @BasicBillingIdC Int, @EconomicActivityIdCS Int;

				OPEN cursor_bbDetailS;

				FETCH NEXT FROM cursor_bbDetailS INTO @BasicBillingIdC, @EconomicActivityIdCS;

				
				WHILE @@FETCH_STATUS = 0
				BEGIN

					IF ( @EconomicActivityIdCS IS NULL ) --De no existir actividad económica para dicho detalle llenamos la tabla de errores
						BEGIN
							INSERT INTO @ErrorsTable (CodeMessage, [Message], Code, Id)
							SELECT
								999,
								CONCAT('No se tiene definida una Actividad Económica Generadora de Ingreso en el Concepto de Facturación ', 
										BC.Name, ' asociado al Servicio ', IPSS.name),
								3,
								0
								FROM Billing.BasicBillingDetail bbd
								JOIN Billing.BillingConcept BC on BC.Id = bbd.BillingConceptId
								LEFT JOIN Contract.IPSService IPSS on IPSS.BillingConceptId = BC.Id --Se deja un LEFT JOIN por si el Servicio IPS llega vacío
								WHERE bbd.Id = @BasicBillingIdC;

					END

					FETCH NEXT FROM cursor_bbDetailS INTO @BasicBillingIdC, @EconomicActivityIdCS;
				END;

				CLOSE cursor_bbDetailS;

				DEALLOCATE cursor_bbDetailS;
			END;
			
			IF EXISTS (SELECT 1 FROM @ErrorsTable)
			BEGIN

				DECLARE @AllErrors NVARCHAR(MAX) = N'';
				SELECT @AllErrors = STRING_AGG([Message], CHAR(10)) 
				FROM @ErrorsTable;

				SELECT * FROM @ErrorsTable;

				RETURN;
			END;
			
			--Insertamos los nuevos detalles
			DECLARE @TempId INT,
					@BasicBillingDetailId INT

			DECLARE InfoItem CURSOR FOR 
				SELECT TempId FROM @BasicBillingDetail WHERE Id = 0

			OPEN InfoItem 
			
			FETCH NEXT FROM InfoItem INTO @TempId

			WHILE @@fetch_status = 0
			BEGIN
			--Insertamos Detalles
				INSERT INTO [Billing].[BasicBillingDetail]
				([BasicBillingId],[DetailType],[ProductId],[BillingConceptId],[PhysicalAssetId],[PhysicalAssetPartId],[Quantity],[Price],[Value],[PercentageDiscount],
				[ValueDiscount],[PercentageIVA],[RetentionIdTax],[RetentionPercentageTax],[WithholdingTax],[RetentionIdICA],[RetentionPercentageICA],[WithholdingICA],[WarehouseId],
				[ServicesProvidedId],[SupplierId],[SalesExecutiveId],[FeeId],[FunctionalUnitId],EconomicActivityId)
				SELECT @Id,[DetailType],IIF([DetailType] = 1, [ProductId], NULL),IIF([DetailType] = 2, [BillingConceptId], NULL),IIF([DetailType] = 3, [PhysicalAssetId], NULL),
				IIF([DetailType] = 4, [PhysicalAssetPartId], NULL),[Quantity],[Price],[Value],[PercentageDiscount],[ValueDiscount],[PercentageIVA],[RetentionIdTax],
				[RetentionPercentageTax],[WithholdingTax],[RetentionIdICA],[RetentionPercentageICA],[WithholdingICA],[WarehouseId],
				IIF([DetailType] = 2,IIF([ServicesProvidedId] <> 0 ,[ServicesProvidedId], null), NULL),IIF([DetailType] = 2 and [SupplierId] <> 0,[SupplierId], null),IIF([DetailType] = 2 and [SalesExecutiveId] <> 0,[SalesExecutiveId], null),
				IIF([FeeId]> 0, [FeeId],NULL),[FunctionalUnitId], IIF([EconomicActivityId] > 0, [EconomicActivityId], NULL)
				FROM @BasicBillingDetail
				WHERE TempId = @TempId

				--Obtengo el id del detalle
				SET @BasicBillingDetailId = SCOPE_IDENTITY()

				UPDATE bbdi
					SET bbdi.BasicBillingDetailId = @BasicBillingDetailId
				FROM @BasicBillingDetailItem bbdi
				WHERE bbdi.ParentId = @TempId

				FETCH NEXT FROM InfoItem INTO @TempId
			END

			CLOSE InfoItem
			DEALLOCATE InfoItem

			IF (SELECT count(*) FROM @BasicBillingGifts) > 0 BEGIN

			--Insertamos los nuevos detalles
			DECLARE @TempGiftId INT,
					@BasicBillingGiftId INT

			DECLARE InfoItemGift CURSOR FOR 
				SELECT TempId FROM @BasicBillingGifts WHERE Id = 0

			OPEN InfoItemGift 
			
			FETCH NEXT FROM InfoItemGift INTO @TempGiftId

			WHILE @@fetch_status = 0
			BEGIN
				--Insertamos Detalles
				INSERT INTO [Billing].[BasicBillingGifts] ([BasicBillingId],[ProductId],[Quantity],[WarehouseId])
				SELECT @Id,[ProductId],[Quantity],[WarehouseId]
				FROM @BasicBillingGifts
				WHERE TempId = @TempGiftId

			----Obtengo el id del detalle
				SET @BasicBillingGiftId = SCOPE_IDENTITY()

			--Se actualizan los detalles de obsequios
				UPDATE bbgi
					SET bbgi.BasicBillingGiftsId = @BasicBillingGiftId
				FROM @BasicBillingGiftsItem bbgi
				WHERE bbgi.ParentId = @TempGiftId

				FETCH NEXT FROM InfoItemGift INTO @TempGiftId
			END

			CLOSE InfoItemGift
			DEALLOCATE InfoItemGift

			END
			
			--Se Actualizan los detalles
			UPDATE bbd SET bbd.[DetailType] = bbdt.DetailType,bbd.[ProductId] = IIF(bbdt.DetailType = 1, bbdt.ProductId, NULL),bbd.[BillingConceptId] = IIF(bbdt.DetailType = 2, bbdt.BillingConceptId, NULL),
			bbd.[PhysicalAssetId] = IIF(bbdt.DetailType = 3, bbdt.PhysicalAssetId, NULL),bbd.[PhysicalAssetPartId] = IIF(bbdt.DetailType = 4, bbdt.PhysicalAssetPartId, NULL),
			bbd.[Quantity] = bbdt.Quantity,bbd.[Price] = bbdt.Price,bbd.[Value] = bbdt.Value,bbd.[PercentageDiscount] = bbdt.PercentageDiscount,bbd.[ValueDiscount] = bbdt.ValueDiscount,
			bbd.[PercentageIVA] = bbdt.PercentageIVA,bbd.[RetentionIdTax] = bbdt.RetentionIdTax,bbd.[RetentionPercentageTax] = bbdt.RetentionPercentageTax,bbd.[WithholdingTax] = bbdt.WithholdingTax,
			bbd.[RetentionIdICA] = bbdt.RetentionIdICA,bbd.[RetentionPercentageICA] = bbdt.RetentionPercentageICA,bbd.[WithholdingICA] = bbdt.WithholdingICA,bbd.[WarehouseId] = bbdt.WarehouseId,
			bbd.[ServicesProvidedId] = IIF(bbdt.DetailType = 2 and bbdt.ServicesProvidedId <> 0, bbdt.[ServicesProvidedId], NULL), bbd.[SupplierId] = IIF(bbdt.DetailType = 2  and bbdt.[SupplierId] <> 0, bbdt.[SupplierId], NULL), 
			bbd.[SalesExecutiveId] = IIF(bbdt.DetailType = 2 and bbdt.[SalesExecutiveId] <> 0, bbdt.[SalesExecutiveId], NULL),bbd.[FeeId] = IIF(bbdt.FeeId > 0,bbdt.[FeeId] , NULL ), bbd.[FunctionalUnitId] = bbdt.FunctionalUnitId,
			bbd.economicActivityId = IIF(bbdt.EconomicActivityId > 0, bbdt.EconomicActivityId, NULL)

			FROM [Billing].[BasicBillingDetail] bbd
			JOIN @BasicBillingDetail bbdt ON bbd.Id = bbdt.Id

			UPDATE bbgt SET bbgt.[ProductId] = bbgi.ProductId ,bbgt.[Quantity] = bbgi.Quantity
			FROM [Billing].[BasicBillingGifts] bbgt
			JOIN @BasicBillingGifts bbgi ON bbgt.Id = bbgi.Id

			--Elimino los detalles previos
			DELETE bbdi
			FROM Billing.BasicBillingDetailItem bbdi
			JOIN @BasicBillingDetailItem bbdit ON bbdi.BasicBillingDetailId = bbdit.BasicBillingDetailId

			--Elimino los detalles previos
			DELETE bbgt
			FROM Billing.BasicBillingGiftsItem bbgt
			JOIN @BasicBillingGiftsItem bbgti ON bbgti.BasicBillingGiftsId = bbgt.BasicBillingGiftsId

			--Insertamos los nuevos detalles de los Items
			INSERT INTO [Billing].[BasicBillingDetailItem]
			(
				[BasicBillingDetailId],[PhysicalInventoryId],[Quantity]
			)
			SELECT
				[BasicBillingDetailId],[PhysicalInventoryId],[Quantity]
			FROM @BasicBillingDetailItem

			INSERT INTO [Billing].[BasicBillingGiftsItem] ([BasicBillingGiftsId],[PhysicalInventoryId],[Quantity])
			SELECT [BasicBillingGiftsId],[PhysicalInventoryId],[Quantity]
			From @BasicBillingGiftsItem

		END
		ELSE
		BEGIN
			DECLARE @AnnulmentUser VARCHAR(20) = CASE WHEN @Status <> 3 THEN null ELSE @CodeUser END
			DECLARE @AnnulmentDate DATETIME = CASE WHEN @Status <> 3 THEN null ELSE [Common].[GETDATE]() END

			UPDATE [Billing].[BasicBilling]
				SET [Status] = @Status,
					[AnnulmentUser] = @AnnulmentUser,
					[AnnulmentDate] = @AnnulmentDate
			WHERE Id = @Id
		END

		SELECT 0 AS CodeMessage, 'Se guardó correctamente' AS Message, @Code as Code, @Id as Id		
	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeMessage, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS Message, '' AS Code, 0 AS Id
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que crea o actualiza una factura básica (cotización, prefactura o borrador de factura) en el módulo de facturación. Recibe los datos de la factura en formato XML, incluyendo cabecera (cliente, fecha, modalidad de venta, moneda, condición de pago, actividad económica) y el detalle de líneas facturadas (productos, servicios, activos físicos, conceptos), calculando valores de venta, descuentos, IVA, retención en la fuente, retención de ICA y redondeo según la moneda configurada. Gestiona también los ítems de inventario físico asociados a cada línea de detalle y los obsequios o bonificaciones entregados al cliente en la misma factura. Valida que el documento no esté ya confirmado o anulado antes de permitir modificaciones, y considera la configuración de actividad económica del tenant para el registro contable.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_SaveBasicBilling';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_SaveBasicBilling';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste (inserta/actualiza) una factura básica con sus detalles, ítems y obsequios a partir de un XML, validando cálculos, retenciones y actividad económica, o anula el documento según el estado solicitado.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBasicBilling';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe tener la estructura /BasicBilling con detalles e ítems esperados.; El registro existente no debe estar en estado distinto de 1 (Borrador); si Status=2 o 3 se rechaza.; Debe estar parametrizado un concepto de retención de IVA (RetentionIdIVA distinto de 0).; Si el detalle tiene BillingConcept con ConceptType=1, todos los detalles (excepto DetailType=3) deben tener RetentionIdTax y RetentionIdICA.; Para detalles DetailType=3 (Activo Fijo) cuando el tercero maneja ICA, se requieren RetentionIdTax y RetentionIdICA.; La suma de cantidades de los ítems de lote debe igualar la cantidad del detalle (tanto en detalles como en obsequios).; Si CustomerId=0 debe enviarse ThirdPartyId y debe existir cuenta contable principal (vía contrato del Invoice o vía SettingsBilling) para crear el cliente.; Debe existir secuencia de facturación configurada para IdForm=''2038'' (a nivel organización o unidad operativa).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBasicBilling';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Billing.BasicBilling: Cuando @Id es 0 o NULL y @Status<>3, inserta la cabecera con el código generado por secuencia y los totales validados.; [UPDATE] Billing.BasicBilling: Cuando @Id existe y @Status<>3, actualiza la cabecera; FunctionalUnitId y WarehouseId se fuerzan a NULL.; [UPDATE] Billing.BasicBilling: Cuando @Status=3 (anulación), actualiza Status, AnnulmentUser=@CodeUser y AnnulmentDate=GETDATE().; [INSERT] Billing.BasicBillingDetail: Por cada detalle con Id=0 inserta una nueva línea; ProductId/BillingConceptId/PhysicalAssetId/PhysicalAssetPartId se asignan según DetailType (1/2/3/4); ServicesProvidedId, SupplierId y SalesExecutiveId solo si DetailType=2 y son distintos de 0; FeeId y EconomicActivityId solo si >0.; [UPDATE] Billing.BasicBillingDetail: Para detalles con Id existente actualiza todos los campos aplicando las mismas reglas de DetailType y filtros >0.; [DELETE] Billing.BasicBillingDetail: Cuando IsDelete=1 en el detalle del XML, elimina la fila correspondiente en BasicBillingDetail.; [DELETE] Billing.BasicBillingDetailItem: Elimina ítems de detalles marcados con IsDelete=1 y también elimina ítems previos cuyos BasicBillingDetailId coinciden con los de la tabla temporal antes de re-insertar.; [INSERT] Billing.BasicBillingDetailItem: Inserta los ítems del XML con el BasicBillingDetailId actualizado tras crear el detalle padre.; [INSERT] Billing.BasicBillingGifts: Cuando hay obsequios en el XML con Id=0, inserta nuevas filas asociadas al @Id de la factura.; [UPDATE] Billing.BasicBillingGifts: Actualiza ProductId y Quantity de obsequios existentes.; [DELETE] Billing.BasicBillingGifts: Cuando IsDelete=1, elimina la fila de BasicBillingGifts.; [DELETE] Billing.BasicBillingGiftsItem: Elimina ítems de obsequios marcados con IsDelete=1 y los previos cuyos BasicBillingGiftsId coinciden con la temporal.; [INSERT] Billing.BasicBillingGiftsItem: Inserta los ítems de obsequios con el BasicBillingGiftsId recién creado.; [INSERT] common.Customer: Si @CustomerId=0 y @ThirdPartyId>0, crea cliente con Nit/Name del tercero, Term=30, State=1 y MainAccountReceivableId obtenido del contrato (si hay InvoiceId) o de SettingsBilling.ClientMainAccountId.; [UPDATE] Billing.BillingSequenceDetail: Tras generar el código consecutivo, incrementa [Next] += 1 en el detalle de secuencia utilizado.; [RETURN_RESULT] RESULT: Devuelve CodeMessage=999 con mensaje de error específico cuando falla cualquier validación; CodeMessage=0 con ''Se guardó correctamente'' al finalizar.; [RETURN_RESULT] RESULT: Si la tabla @ErrorsTable contiene registros (faltantes de Actividad Económica), devuelve todos los errores y termina sin persistir cambios posteriores.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBasicBilling';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBasicBilling';
-- GO
