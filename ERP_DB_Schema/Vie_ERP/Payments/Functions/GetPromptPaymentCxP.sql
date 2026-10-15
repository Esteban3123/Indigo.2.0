
-- ==========================================================================
-- Author:		Cesar Augusto Collazos Perdomo
-- Create date: 19-10-2023
-- Description:	Función que calcula valor base para una CxP
-- ==========================================================================

CREATE Function [Payments].[GetPromptPaymentCxP]
(	
	@EntityId Int,
	@EntityName varchar(250),
	@SupplierId int,
	@InvoiceNumber varchar(100),
	@AccountPayableId int
	
)
Returns @DetailValueAccountPayable Table
(
	StatusResult Bit,
	MessageResult Varchar(Max),
	
	IdAccountPayable Int,
	BaseValue Decimal(18, 2),
	ValueTax DECIMAL(18,2),
	RateValue DECIMAL(5,2)
) 
As
Begin

Declare @IdEntity Int = @EntityId,
			@NameEntity varchar(250) = @EntityName,
			@BillNumber varchar(100) = @InvoiceNumber,
			@TableName varchar,
			@IdAccountPurchaseService as int,
			@IdAccountSale as int,
			@IdAccountDebitControlFiscal as int,
			@IdAccountCreditControlFiscal as int

			
declare @GeneralLedgerIVATemp table (AccountId int)


	Set @TableName =
		CASE
			WHEN @NameEntity ='EntranceVoucher'
				THEN  'Inventory.EntranceVoucher'

			WHEN @NameEntity ='LoadMassive'
				THEN 'Payments.LoadMassiveAccountPayable'
			
			WHEN @NameEntity ='InitialBalance'
				THEN 'Payments.InitialBalanceAccountPayable'

			WHEN @NameEntity ='FixedAssetEntry'
				THEN 'FixedAsset.FixedAssetEntry'
			END;
			
	IF @NameEntity is not null and @NameEntity <>'' and @NameEntity<>'CostDistributionDirectCost'
		begin
			If @NameEntity = 'EntranceVoucher'
				begin
				insert into @DetailValueAccountPayable
				SELECT 1,null,e.AccountPayableId,(e.Value-(isnull(evd.DevValue,0))),e.ValueTax,((e.ValueTax*100)/e.Value)
				FROM Inventory.EntranceVoucher e WITH(NOLOCK)
				OUTER APPLY(SELECT sum(evd.Value) DevValue 
							from Inventory.EntranceVoucherDevolution evd WITH(NOLOCK)
							where e.Id=evd.EntranceVoucherId and evd.Status = 2) evd
				WHERE e.Id =@IdEntity and e.InvoiceNumber =@BillNumber and e.SupplierId= @SupplierId
			end

			else if @NameEntity ='LoadMassive'
				begin 
				INSERT INTO @DetailValueAccountPayable
				SELECT 1,NULL,lmap.AccountPayableId,lmap.InvoiceValue,0,0
				FROM Payments.LoadMassiveAccountPayable lmap WITH(NOLOCK)
				WHERE lmap.LoadMassiveId=@IdEntity and lmap.BillNumber=@BillNumber and lmap.SupplierId=@SupplierId
			end	

			else if @NameEntity='InitialBalance'
				BEGIN
				INSERT into @DetailValueAccountPayable
				SELECT 1,null,inbap.AccountPayableId,inbap.Value,0,0
				FROM Payments.InitialBalanceAccountPayable inbap WITH(NOLOCK)
				WHERE inbap.InitialBalanceId= @IdEntity AND inbap.BillNumber=@BillNumber and inbap.SupplierId =@SupplierId
			end

			else if @NameEntity='FixedAssetEntry'
				BEGIN
				INSERT into @DetailValueAccountPayable
				SELECT 1,null,fe.AccountPayableId,fe.Value,fe.ValueTax,((fe.ValueTax*100)/fe.Value)
				FROM FixedAsset.FixedAssetEntry fe WITH(NOLOCK)
				WHERE fe.Id= @IdEntity AND fe.InvoiceNumber=@BillNumber and fe.SupplierId = @SupplierId
			end
	END
	ELSE BEGIN
		INSERT INTO @GeneralLedgerIVATemp (AccountId)
			SELECT AccountId
			FROM (
				SELECT IdAccountPurchaseService AS AccountId FROM GeneralLedger.GeneralLedgerIVA
				UNION
				SELECT IdAccountSale FROM GeneralLedger.GeneralLedgerIVA
				UNION
				SELECT IdAccountDebitControlFiscal FROM GeneralLedger.GeneralLedgerIVA
				UNION
				SELECT IdAccountCreditControlFiscal FROM GeneralLedger.GeneralLedgerIVA
			) AS DerivedTable
			WHERE AccountId NOT IN (
				SELECT AccountId FROM @GeneralLedgerIVATemp
			)

		INSERT into @DetailValueAccountPayable
		SELECT 
				1 AS StatusResult,
				'OK' AS MessageResult,
				@AccountPayableId AS IdAccountPayable,
				SUM(apdc.BaseValue) AS BaseValue,
				SUM(apdc.IvaValue) AS ValueTax,
				AVG(apdc.RateIva) AS RateValue

			FROM Payments.AccountPayableDetailConcept apdc
			INNER JOIN GeneralLedger.MainAccounts ma ON apdc.IdAccount = ma.Id
			INNER JOIN GeneralLedger.MainAccountClasses mac ON ma.IdAccountClass = mac.Id
			INNER JOIN Payments.AccountPayable ap ON apdc.IdAccountPayable = ap.Id
			WHERE apdc.IdAccountPayable = @AccountPayableId
			  AND apdc.IdRetentionConcept IS NULL
			  AND( mac.Type = 2
					or (mac.Type = 1 and mac.Nature = 1 and ma.RetencionType = 0 AND ma.Id not in (select AccountId from @GeneralLedgerIVATemp)  )
				)
			GROUP BY
			apdc.IdAccountPayable
	END

	declare @baseNotes DECIMAL(18,2)
	select @baseNotes = isnull(sum(iif(n.Nature = 1, (pnd.BaseValue * -1), pnd.BaseValue)),0)
	from Payments.PaymentNotes n
	inner join Payments.PaymentNotesAccountPayableAdvance p on p.PaymentNoteId = n.Id
	inner join Payments.PaymentsNoteDetails pnd on pnd.IdPaymentsNote = n.Id
	JOIN GeneralLedger.MainAccounts ma ON pnd.IdAccount = ma.Id
	JOIN GeneralLedger.MainAccountClasses mac ON ma.IdAccountClass = mac.Id
	WHERE n.AffectBaseToDiscount = 1
	and p.AccountPayableId = @AccountPayableId
	AND pnd.IdRetentionConcept IS NULL
	AND( mac.Type = 2
			or (mac.Type = 1 and mac.Nature = 1 and ma.RetencionType = 0 AND ma.Id not in (select AccountId from @GeneralLedgerIVATemp)  )
		)
	
	update @DetailValueAccountPayable set BaseValue += @baseNotes

	UPDATE @DetailValueAccountPayable
	SET BaseValue = 0
	WHERE EXISTS (
	SELECT 1
			FROM Payments.PaymentNotesAccountPayableAdvance pnapa 
					JOIN Payments.PaymentNotes pn ON pnapa.PaymentNoteId = pn.Id
					JOIN Payments.PaymentsNoteDetails pnd ON pnapa.PaymentNoteId = pnd.IdPaymentsNote
					WHERE pnapa.AccountPayableId=@AccountPayableId AND pn.AllowDiscountPromptPayment  = 0
	)

	Return

End