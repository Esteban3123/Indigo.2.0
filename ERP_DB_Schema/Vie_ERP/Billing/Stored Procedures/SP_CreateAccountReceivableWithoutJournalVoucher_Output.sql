-- =============================================
-- Author:		Cristian Camilo Bahamon Castaño
-- Create date: 2023-04-13
-- Description:	Generar la cuenta por cobrar sin crear un comprobante contable
-- =============================================
CREATE PROCEDURE [Billing].[SP_CreateAccountReceivableWithoutJournalVoucher_Output]
	@PaymentMethodsXml XML,
	@IdCashReceipt INT,
	@OperativeUnitId INT,
	@IdThirdParty INT,
	@DocumentDate datetime,
	@User VARCHAR(20),
	--Salidas
	@Code_Output int OUTPUT,
	@ResultMessage VARCHAR(MAX) OUTPUT
	AS
	BEGIN
		SET NOCOUNT ON
		
	/*************************************************** VARIABLES ***************************************************/

	DECLARE @FormId VARCHAR(5),
			@IsManual BIT = 0,
			@Message_Output VARCHAR(MAX),
			-------------------------------------------------------------------
			@PortfolioAccountReceivableId INT,
			@PortfolioAccountReceivableCode VARCHAR(20),
			-------------------------------------------------------------------
			@JournalXml XML,
			-------------------------------------------------------------------
			@Message VARCHAR(MAX),
			@_cOfficialCurrencyId int
			SELECT top 1  @_cOfficialCurrencyId = OfficialCurrencyId from GeneralLedger.CompanySettings

	--Se declara la tambla temporal de metodos de pago
    DECLARE @PaymentMethods AS TABLE
    (
		RowId				   INT IDENTITY(1,1) PRIMARY KEY,
		ChangeTracker          VARCHAR(30),
        [Id]                   [INT] NOT NULL,
        [IdCashReceipt]        [INT] NOT NULL,
        [PaymentMethodTypes]   [TINYINT] NOT NULL,
        [Value]                [DECIMAL](18, 2) NOT NULL,
        [IdCostCenter]         [INT] NULL,
		[CurrencyId]           [INT] NOT NULL,
		[TRM]                  [NUMERIC](20,5)NOT NULL default (1),
		[ValueInCurrencyHeader][NUMERIC](20,5)NOT NULL,
		[IdAgreementsRedemptionPoints] [int] NULL,
		[TransactionNumber] [varchar](30) NULL,
		[TransactionDate] [datetime] NULL,
		[RedemptionPoints] [int] NULL
    );
	
		BEGIN TRY

	     --Se insertan los metodos de pagos
        INSERT INTO @PaymentMethods
            SELECT t.x.value('ChangeTracker[1]', 'varchar(30)') AS ChangeTracker,
                    t.x.value('Id[1]', 'int') AS Id,
                    t.x.value('IdCashReceipt[1]', 'int') AS IdCashReceipt,
                    t.x.value('PaymentMethodTypes[1]', 'tinyint') AS PaymentMethodTypes,
                    t.x.value('Value[1]', 'decimal(18, 2)') AS Value,
                    t.x.value('IdCostCenter[1]', 'int') AS IdCostCenter,
					iif(t.x.value('CurrencyId[1]', 'int') is null or t.x.value('CurrencyId[1]', 'int')='',@_cOfficialCurrencyId,t.x.value('CurrencyId[1]', 'int')  ) AS CurrencyId,
					isnull(t.x.value('TRM[1]', 'decimal(20,5)'),1) AS TRM,
					iif(t.x.value('ValueInCurrencyHeader[1]', 'decimal(20,5)') is null or t.x.value('ValueInCurrencyHeader[1]', 'decimal(20,5)') =0 ,t.x.value('Value[1]', 'decimal(18, 2)'),t.x.value('ValueInCurrencyHeader[1]', 'decimal(20,5)')) AS ValueInCurrencyHeader,
					t.x.value('IdAgreementsRedemptionPoints[1]', 'int') AS IdAgreementsRedemptionPoints,
					t.x.value('TransactionNumber[1]', 'varchar(30)') AS TransactionNumber,
					t.x.value('TransactionDate[1]', 'datetime') AS TransactionDate,
					t.x.value('RedemptionPoints[1]', 'int') AS RedemptionPoints
            FROM @PaymentMethodsXml.nodes('/_x0040_PaymentMethods') t(x);

				--Obtengo la secuencia numerica 
				SET @ResultMessage = ''
				SET @FormId =  '682'
				EXEC Common.SP_GetSequence 160, @FormId, @OperativeUnitId, NULL, NULL, @IsManual OUT, @PortfolioAccountReceivableCode OUT, @Code_Output OUT, @Message_Output OUT
				IF @Code_Output <> 0
				BEGIN
					SELECT	@Code_Output = 999, 
							@ResultMessage = REPLACE(@Message_Output, '{0}', 'Cuentas Por Cobrar')
					RETURN
				END
			
			/*************************************************  VALIDACIONES *************************************************/
			
			IF EXISTS (SELECT 1 FROM Portfolio.AccountReceivableDocument WHERE Code = @PortfolioAccountReceivableCode )
			BEGIN
				SELECT	@Code_Output = 999, 
						@ResultMessage = 'Ya existe un documento CxC con el codigo: '+@PortfolioAccountReceivableCode
				RETURN
			END

			-- Valido que se encuentre la unidad operativa
			IF NOT EXISTS (SELECT 1 FROM Portfolio.SettingPortfolio WITH (NOLOCK) WHERE OperatingUnitId = @OperativeUnitId)
			BEGIN
				SELECT	@Code_Output = 999, 
						@ResultMessage = CONCAT('No se encontraron parametros de cartera para la unidad operativa',ISNULL(@OperativeUnitId,0))
				RETURN
			END
			
			-- Valido que se haya ingresado un tercero valido 
			IF NOT EXISTS (SELECT 1 FROM Common.ThirdParty WITH (NOLOCK) WHERE Id = @IdThirdParty)
			BEGIN
				SELECT	@Code_Output = 999, 
						@ResultMessage = 'El tercero no existe'
				RETURN
			END

			-- Valido que el tercero este activo
			IF EXISTS (SELECT 1 FROM Common.ThirdParty WITH (NOLOCK) WHERE Id = @IdThirdParty AND State = 0)
			BEGIN
				SELECT	@Code_Output = 999, 
						@ResultMessage = 'El tercero no se encuentra activo'
				RETURN
			END

			-- Valido que el valor deber ser mayor a 0
			IF (select Value from @PaymentMethods) <= 0
			BEGIN
				SELECT	@Code_Output = 999, 
						@ResultMessage = 'El valor debe ser mayor a 0'
				RETURN
			END

			--Valido que el tipo de metodo de pago sea redencion de puntos
			If (select PaymentMethodTypes from @PaymentMethods ) <> 5
			BEGIN
				SELECT	@Code_Output = 999, 
						@ResultMessage = 'El metodo de pago no es redención de puntos'
				RETURN
			END
			/********************************** INSERTAR **********************************/
			--Generacion de la cuenta por cobrar
					INSERT INTO [Portfolio].[AccountReceivable]
					(
						[Code],[AccountReceivableType],[ThirdPartyId],[CustomerId],[InvoiceNumber],[AccountReceivableDate],
						[Term],[ExpiredDate],[Observations],[PortfolioStatus],[OpeningBalance],[PaymentAgreement],[RegistrationAdjusted],
						[CostCenterId],[OperatingUnitId],[NumberShares],[Value],[Balance],[Status],
						[AccountWithoutRadicateId],AffectBudget,[CreationUser],[CreationDate],[CurrencyId],[TRMValue]
					)
					SELECT	@PortfolioAccountReceivableCode,--[Code]
							2,--[AccountReceivableType]
							c.ThirdPartyId,--[ThirdPartyId]
							arp.CustomerId,--[CustomerId]
							@PortfolioAccountReceivableCode,--[InvoiceNumber] 
							@DocumentDate,--[AccountReceivableDate]
							c.Term,--[Term]
							DATEADD(DAY,30,@DocumentDate),--[ExpiredDate]
							ISNULL(arp.Description,''),--[Observations]
							3,--portfoliostatus, preguntar
							0,--OpeningBalance,Indica si es Saldo Inicial
							0,--PaymentAgreement,Indica si es Acuerdo de pago
							0,--RegistrationAdjusted,Indica si el registro esta ajustado
							NULL,--CostCenterId
							@OperativeUnitId,
							1,--NumberShares numero de cuotas
							pm.Value,--value
							pm.Value,--Balance
							2,--status; confirmado
							c.MainAccountReceivableId,--AccountWithoutRadicateId,Id de la cuenta contable sin Radicar
							0,--AffectBudget afecta presupuesto
							@User,
							[Common].[GETDATE](),
							pm.CurrencyId,
							pm.TRM
					from Treasury.AgreementsRedemptionPoints arp
						join Common.Customer c on arp.CustomerId = c.Id
						join GeneralLedger.MainAccounts ma on c.MainAccountReceivableId = ma.Id
						Join @PaymentMethods pm on arp.Id = pm.IdAgreementsRedemptionPoints
						where arp.Id = pm.IdAgreementsRedemptionPoints

					SET @PortfolioAccountReceivableId = SCOPE_IDENTITY()

					INSERT INTO Portfolio.AccountReceivableShare
					(
						AccountReceivableId,Number,ExpiredDate,[Value],Balance,DebitValue,CreditValue,TransferValue,PaymentValue,
						CrossingValue,InterestValue,SurchargesValue,CapitalRepaymentAgreement,FinancialInterest,RepaymentAgreementInterest
					)
					SELECT	@PortfolioAccountReceivableId,1,ar.ExpiredDate,ar.Value,ar.Balance,0,0,0,0,
							0,0,0,0,0,0
					FROM Portfolio.AccountReceivable ar WITH (NOLOCK)
					WHERE ar.Id = @PortfolioAccountReceivableId

					INSERT INTO Portfolio.AccountReceivableAccounting
					(
						AccountReceivableId,MainAccountId,ThirdPartyId,CostCenterId,[Value],Balance
					)
					SELECT	@PortfolioAccountReceivableId,ma.Id,
							IIF(ma.HandlesThirdParty = 1, ar.ThirdPartyId, NULL),
							IIF(ma.HandlesCostCenter = 1, ar.CostCenterId, NULL),
							ar.Value,ar.Balance
					FROM Portfolio.AccountReceivable ar WITH (NOLOCK)
					JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ar.AccountWithoutRadicateId = ma.Id
					WHERE ar.Id = @PortfolioAccountReceivableId

					insert into [Treasury].[AccountReceivableAgreementsRedemptionPoints]
						select @PortfolioAccountReceivableId,(select IdAgreementsRedemptionPoints from @PaymentMethods),@User,[Common].[GETDATE](),@IdCashReceipt

					SET @Message = CONCAT('Código Cuenta por Cobrar por redención de puntos ',  @PortfolioAccountReceivableCode)
					SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
					
			END TRY
			BEGIN CATCH
				SELECT	@Code_Output = 999,
				@ResultMessage = CONCAT('Error creando las cuentas por cobrar: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE())
			END CATCH
	END