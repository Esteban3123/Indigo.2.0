
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que calcula el valor base (monto sujeto a descuento por pronto pago) de una cuenta por pagar (CxP) a proveedor, considerando el tipo de origen del documento: comprobante de entrada de inventario, carga masiva, saldo inicial o entrada de activo fijo. Según el origen indicado, consulta la tabla correspondiente (EntranceVoucher, LoadMassiveAccountPayable, InitialBalanceAccountPayable o FixedAssetEntry) para obtener el valor de la factura, el IVA y la tasa de impuesto; si no hay origen específico, calcula el valor base sumando los conceptos de detalle de la cuenta por pagar según la clasificación contable. Adicionalmente, ajusta el valor base incorporando notas de pago (créditos o débitos) que afecten la base de descuento, y lo anula completamente si existe alguna nota de pago que no permita aplicar descuento por pronto pago. El resultado es una tabla con el valor base, IVA y tasa aplicable que se usa para calcular el descuento por pronto pago ofrecido al proveedor.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'FUNCTION', @level1name = N'GetPromptPaymentCxP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'FUNCTION', @level1name = N'GetPromptPaymentCxP';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula el valor base, IVA y tasa de IVA aplicables a una cuenta por pagar para determinar el descuento por pronto pago al proveedor, considerando origen del documento y notas que afectan la base.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'FUNCTION', @level1name=N'GetPromptPaymentCxP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El documento origen (entrada de inventario, carga masiva, saldo inicial o entrada de activo fijo) debe existir y coincidir por Id, número de factura y proveedor; Si no se indica entidad origen, debe existir la cuenta por pagar y sus conceptos de detalle en AccountPayableDetailConcept; Para calcular tasa de IVA en EntranceVoucher y FixedAssetEntry, el valor del documento no puede ser cero (se usa en división)', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'FUNCTION', @level1name=N'GetPromptPaymentCxP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran conceptos sin retención (IdRetentionConcept IS NULL); Se excluyen del cálculo las cuentas configuradas como cuentas de IVA en GeneralLedgerIVA (compras, ventas, débito y crédito fiscal); Solo aplican cuentas tipo 2 (IVA) o tipo 1 de naturaleza 1 sin tipo de retención; Las notas de pago solo afectan la base si AffectBaseToDiscount=1; Si alguna nota anticipada no permite descuento por pronto pago, el valor base se anula sin importar el cálculo previo; Las devoluciones solo se consideran cuando su Status=2', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'FUNCTION', @level1name=N'GetPromptPaymentCxP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuenta por pagar; Descuento por pronto pago; Proveedor; Factura; IVA; Retención; Devolución de compra; Activo fijo; Saldo inicial; Carga masiva de cuentas por pagar; Notas de pago; Anticipo a proveedor; Plan de cuentas (PUC); Naturaleza contable', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'FUNCTION', @level1name=N'GetPromptPaymentCxP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DetailValueAccountPayable: Cuando entidad=''EntranceVoucher'': inserta valor base = Value menos devoluciones con Status=2 de EntranceVoucherDevolution, junto con ValueTax y tasa = (ValueTax*100)/Value; [INSERT] @DetailValueAccountPayable: Cuando entidad=''LoadMassive'': inserta InvoiceValue de LoadMassiveAccountPayable como base, con IVA y tasa en 0; [INSERT] @DetailValueAccountPayable: Cuando entidad=''InitialBalance'': inserta Value de InitialBalanceAccountPayable como base, con IVA y tasa en 0; [INSERT] @DetailValueAccountPayable: Cuando entidad=''FixedAssetEntry'': inserta Value, ValueTax y tasa = (ValueTax*100)/Value desde FixedAssetEntry; [INSERT] @DetailValueAccountPayable: Cuando no se especifica entidad (o es nula/vacía y distinta de ''CostDistributionDirectCost''): suma BaseValue, IvaValue y promedia RateIva de AccountPayableDetailConcept con IdRetentionConcept NULL y donde la cuenta es de tipo 2 (IVA) o tipo 1 con naturaleza 1, sin retención y que no esté en cuentas configuradas de IVA; [UPDATE] @DetailValueAccountPayable: Suma al BaseValue el neto de notas de pago (PaymentNotes con AffectBaseToDiscount=1) asociadas a la cuenta por pagar, restando las de naturaleza 1 y sumando las demás, filtradas por las mismas reglas contables; [UPDATE] @DetailValueAccountPayable: Si existe alguna nota de pago anticipada (PaymentNotesAccountPayableAdvance) sobre la cuenta por pagar con AllowDiscountPromptPayment=0, se establece BaseValue=0 anulando el descuento', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'FUNCTION', @level1name=N'GetPromptPaymentCxP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Entidad no nula, no vacía y distinta de ''CostDistributionDirectCost'' → Calcula valor base desde la tabla origen específica (EntranceVoucher/LoadMassive/InitialBalance/FixedAssetEntry) else Calcula valor base agregando conceptos de detalle de la cuenta por pagar filtrados por clasificación contable; si Entidad = ''EntranceVoucher'' → Resta devoluciones con Status=2 al valor base y calcula tasa IVA real; si Entidad = ''LoadMassive'' o ''InitialBalance'' → Toma valor de factura/saldo inicial sin IVA ni tasa; si Entidad = ''FixedAssetEntry'' → Toma valor, IVA y calcula tasa de IVA del activo fijo; si Existe nota de pago anticipada con AllowDiscountPromptPayment=0 → Anula el BaseValue forzándolo a 0; si Nota de pago con Nature=1 (en cuenta de naturaleza crédito) → Resta su BaseValue a la base else Suma su BaseValue a la base', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'FUNCTION', @level1name=N'GetPromptPaymentCxP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.EntranceVoucher; Inventory.EntranceVoucherDevolution; Payments.LoadMassiveAccountPayable; Payments.InitialBalanceAccountPayable; FixedAsset.FixedAssetEntry; GeneralLedger.GeneralLedgerIVA; Payments.AccountPayableDetailConcept; GeneralLedger.MainAccounts; GeneralLedger.MainAccountClasses; Payments.AccountPayable; Payments.PaymentNotes; Payments.PaymentNotesAccountPayableAdvance; Payments.PaymentsNoteDetails', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'FUNCTION', @level1name=N'GetPromptPaymentCxP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'FUNCTION', @level1name=N'GetPromptPaymentCxP';
GO
