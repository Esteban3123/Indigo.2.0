
-- ===============================================================================================================
-- Author:		Juan Jose Aviles Roa
-- Create date: 2023-08-31
-- Description:	Procedimiento para el soporte de pagos proveedores
-- ==============================================================================================================
CREATE PROCEDURE [Payments].[SP_SupportPaymentSuppliers]
	@xmlFilters As xml
	------------------------------------------------------
AS
SET
  ANSI_NULLS,
  QUOTED_IDENTIFIER,
  CONCAT_NULL_YIELDS_NULL,
  ANSI_WARNINGS,
  ANSI_PADDING
ON;
BEGIN
	SET NOCOUNT ON
			
	DECLARE -- FILTROS --
			@StartDate DATETIME,
			@EndDate DATETIME,
			@Supplier VARCHAR(MAX),
			@Invoice VARCHAR(MAX),
			@Voucher VARCHAR(MAX),
			---------------------------------------------------------------------------------------
			@FilterBySuppliers BIT = 0,
			@FilterByInvoice BIT = 0,
			@FilterByVoucher BIT = 0

	DECLARE @Table_Supplier AS TABLE(Id INT)
	DECLARE @Table_Invoice AS TABLE(Id INT)
	DECLARE @Table_Voucher AS TABLE(Id INT)

	BEGIN TRY

		/********************************** FILTROS **********************************/
		
		--Se obtienen los datos de los filtros
		SELECT	@StartDate = t.x.value('StartDate[1]','DateTime'),
				@EndDate = t.x.value('EndDate[1]','DateTime'),
				@Supplier = t.x.value('Supplier[1]', 'varchar(max)'),
				@Invoice = t.x.value('Invoice[1]', 'varchar(max)'),
				@Voucher = t.x.value('Voucher[1]', 'varchar(max)')
		FROM @xmlFilters .nodes('/Data') t(x)

		-------------------------------------------------------------------------------------------------

		IF ISNULL(@Supplier, '') <> ''
		BEGIN
			SET @FilterBySuppliers = 1

			INSERT INTO @Table_Supplier
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@Supplier, ',')
		END

		IF ISNULL(@Invoice, '') <> ''
		BEGIN
			SET @FilterByInvoice = 1

			INSERT INTO @Table_Invoice
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@Invoice, ',')
		END

		IF ISNULL(@Voucher, '') <> ''
		BEGIN
			SET @FilterByVoucher = 1

			INSERT INTO @Table_Voucher
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@Voucher, ',')
		END
			
	--		/********************************** OBTENCION DE DATOS **********************************/

		; WITH cte_DetailConceptRetencionType AS (
			SELECT	apc.IdAccountPayable,
														sum(apc.BaseValue) BaseValue,
														sum(apc.BillingValue) BillingValue,
														sum(apc.IvaValue) IvaValue,
														sum(apc.TotalConcept) TotalConcept,
														ma.RetencionType
												from Payments.AccountPayableDetailConcept apc WITH(NOLOCK)
												JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = apc.IdAccount
												group by apc.IdAccountPayable, ma.RetencionType
		),

		cte_DetailConcept as (	SELECT	apc.IdAccountPayable,
										sum(apc.BaseValue) BaseValue,
										sum(apc.BillingValue) BillingValue,
										sum(apc.IvaValue) IvaValue,
										sum(apc.TotalConcept) TotalConcept
								from cte_DetailConceptRetencionType apc
								group by apc.IdAccountPayable),

	
		cte_VoucherTransaction AS ( SELECT	db.IdAccountPayable,
											STRING_AGG(vt.Code,' , ') VoucherCode,
											STRING_AGG(vt.Id,' , ') VoucherTransactionId,
											STRING_AGG(vt.DocumentDate,',') DocumentDate,
											sum(ISNULL(vtd.TotalConcept,vtd.Value)) TotalConcept,
											STRING_AGG((CASE vt.ExpenseType
														WHEN 1 THEN 'Pago'
														WHEN 2 THEN 'Reembolso'
														WHEN 3 THEN 'Traslado'
														END),',') ExpenseType,
											STRING_AGG(NoteNumber,',') NoteNumber,
											STRING_AGG(sba.Number,',') SupplierBankAccount,
											vt.CurrencyId
									from Treasury.VoucherTransaction vt WITH(NOLOCK)
									Join Treasury.VoucherTransactionDetails vtd WITH(NOLOCK) on vt.Id=vtd.IdVoucherTransaction
									JOIN Treasury.DischargeBill db WITH(NOLOCK) on db.IdVoucherTransactionD = vtd.Id
									LEFT JOIN Common.SupplierBankAccount sba WITH(NOLOCK) on sba.Id= vtd.SupplierBankAccountId
									LEFT JOIN @Table_Voucher vch ON vch.Id = vt.Id
									WHERE (@FilterByVoucher = 0 OR vch.Id IS NOT NULL)
									group by db.IdAccountPayable, vt.CurrencyId),
	
		cte_Notes as (SELECT	papa.AccountPayableId,
								sum(IIF(pn.Nature = 1,papa.AdjusmentValue,0)) DebitValue,
								sum(IIF(pn.Nature = 1,0,papa.AdjusmentValue)) CreditValue,
								0 PromptPaymentDiscount,
								pn.CurrencyId
						FROM Payments.PaymentNotes pn WITH(NOLOCK)
						JOIN Payments.PaymentsNoteDetails pnd WITH(NOLOCK) on pn.Id = pnd.IdPaymentsNote
						join Payments.PaymentNotesAccountPayableAdvance papa WITH(NOLOCK) on pn.Id=papa.PaymentNoteId
						GROUP by papa.AccountPayableId, pn.CurrencyId)

		--SELECT *
		--FROM CTE_account_payable_details

	SELECT	ap.Id,
			ap.BillDate,
			ap.IdSupplier,
			CONCAT(s.Code,' - ',s.Name) SupplierName,
			ap.BillNumber,
			ap.InvoiceValue,
			apc.BaseValue,
			ap.Balance,
			ap.code,
			ISNULL(apc.IvaValue,0) IvaValue,
			ISNULL(apc.TotalConcept,0) TotalConcept,
			ISNULL(retef.TotalConcept,0) ReteFuente,
			ISNULL(reteIC.TotalConcept,0) ReteICA,
			ISNULL(reteIV.TotalConcept,0) ReteIVA,
			ISNULL(reteO.TotalConcept,0) ReteOthers,
			ap.CurrencyId,
			c.Abbreviation CurrencyAbbreviation,
			cte_vt.VoucherCode,
			cte_vt.VoucherTransactionId,
			cte_vt.DocumentDate VoucherDate,
			cte_vt.TotalConcept VoucherTotalValue,
			cte_vt.ExpenseType VoucherExpenseType,
			cte_vt.NoteNumber VoucherReferencePayment,
			cte_vt.SupplierBankAccount,
			ISNULL(cte_vt.CurrencyId,c.Id) VoucherCurrencyId,
			ISNULL(cte_n.DebitValue,0) DebitValueNote,
			ISNULL(cte_n.CreditValue,0) CreditValueNote,
			ISNULL(cte_n.PromptPaymentDiscount,0) PromptPaymentDiscount,
			ISNULL(cte_n.CurrencyId,c.Id) NoteCurrencyId
	from Payments.AccountPayable ap WITH(NOLOCK)
	JOIN Common.Supplier s WITH(NOLOCK) on ap.IdSupplier = s.Id
	JOIN cte_DetailConcept apc on apc.IdAccountPayable = ap.Id
	JOIN Common.Currency c WITH(NOLOCK) on c.Id= ap.CurrencyId
	LEFT JOIN cte_DetailConceptRetencionType  retef on ap.Id= retef.IdAccountPayable and retef.RetencionType =1
	LEFT JOIN cte_DetailConceptRetencionType  reteIC on ap.Id= reteIC.IdAccountPayable and reteIC.RetencionType =3
	LEFT JOIN cte_DetailConceptRetencionType  reteIV on ap.Id= reteIV.IdAccountPayable and reteIV.RetencionType =2
	LEFT JOIN cte_DetailConceptRetencionType  reteO on ap.Id= reteO.IdAccountPayable and reteO.RetencionType =4
	LEFT JOIN cte_VoucherTransaction cte_vt on ap.Id = cte_vt.IdAccountPayable 
	LEFT JOIN cte_Notes cte_n on ap.Id = cte_n.AccountPayableId
------------------------------------------------------------------
	LEFT JOIN @Table_Supplier spp ON ap.IdSupplier = spp.Id
	LEFT JOIN @Table_Invoice inv ON inv.Id = ap.Id
	WHERE		(@FilterBySuppliers = 0 OR spp.Id IS NOT NULL) 
			AND (@FilterByInvoice = 0 OR inv.Id IS NOT NULL) 
			AND	(ap.BillDate BETWEEN @StartDate and @EndDate)
			AND (@FilterByVoucher = 0 OR cte_vt.IdAccountPayable is NOT NULL)
	ORDER by ap.BillDate

END TRY
BEGIN CATCH
	SELECT 0 as Id ,NULL as IdDS,'999' as Code,CONCAT('', ERROR_MESSAGE(),' - Linea: ',ERROR_LINE()) AS [Message]
END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el reporte de soporte de pagos a proveedores, permitiendo consultar el detalle de cada cuenta por pagar junto con sus comprobantes de egreso asociados. Recibe filtros en formato XML por rango de fechas, proveedor, número de factura y comprobante de tesorería, y los aplica dinámicamente para devolver únicamente los registros relevantes. Consolida información de cuentas por pagar (AccountPayable), sus conceptos contables desglosados por tipo de retención (ReteFuente, ReteICA, ReteIVA, otras retenciones), los comprobantes de pago de tesorería (VoucherTransaction y VoucherTransactionDetails), las cuentas bancarias del proveedor utilizadas en cada pago, y las notas de ajuste (débito/crédito) asociadas a cada obligación. El resultado sirve como documento de soporte contable y financiero para auditar, conciliar y verificar los pagos realizados a proveedores o terceros.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_SupportPaymentSuppliers';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_SupportPaymentSuppliers';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta de soporte/listado de pagos a proveedores que devuelve, por cuenta por pagar, los valores de factura, retenciones discriminadas, comprobantes de egreso asociados y notas de ajuste, filtrable por rango de fechas, proveedor, factura y comprobante.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_SupportPaymentSuppliers';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro XML debe contener el nodo /Data con StartDate y EndDate válidos para la conversión a DateTime y para el filtro por rango de fechas; Los filtros Supplier, Invoice y Voucher, si vienen, deben ser cadenas de IDs enteros separados por coma (se hace CAST a INT); Deben existir las tablas/relaciones referenciadas con datos consistentes (cuentas contables con RetencionType, comprobantes con detalles y descargos, etc.)', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_SupportPaymentSuppliers';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen cuentas por pagar cuya BillDate esté dentro del rango [StartDate, EndDate]; Las retenciones se segregan obligatoriamente por RetencionType (1=Fuente, 2=IVA, 3=ICA, 4=Otras); cualquier otro valor no se reporta en columnas dedicadas; Si una cuenta por pagar no tiene comprobantes, notas o retenciones asociadas, sus columnas correspondientes se devuelven en cero o nulos (vía ISNULL/LEFT JOIN), no se omite la fila salvo que el filtro de comprobante esté activo; Cuando no hay moneda en comprobante o nota, se usa la moneda de la cuenta por pagar como fallback; El filtrado por proveedor/factura/comprobante solo se aplica cuando el parámetro respectivo viene no vacío; en caso contrario se desactiva el filtro; Los listados de comprobantes (códigos, fechas, tipos, notas, cuentas bancarias) se concatenan por cuenta por pagar y moneda mediante STRING_AGG; El procedimiento nunca propaga excepciones: ante error retorna una fila de error estándar con Code=''999''', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_SupportPaymentSuppliers';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuentas por pagar a proveedores; Facturas de proveedor; Retención en la fuente (ReteFuente); Retención de IVA (ReteIVA); Retención de ICA (ReteICA); Otras retenciones; Comprobantes de egreso / tesorería; Tipos de egreso: Pago, Reembolso, Traslado; Notas de pago (débito/crédito); Descuento por pronto pago; Cuentas bancarias del proveedor; Moneda y abreviatura monetaria; Anticipos a proveedores; Plan de cuentas (PUC) y tipo de retención', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_SupportPaymentSuppliers';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve un conjunto de resultados con datos de cuentas por pagar, retenciones por tipo, comprobantes y notas para las facturas cuyo BillDate está entre @StartDate y @EndDate y que cumplen los filtros opcionales activos; [RETURN_RESULT] RESULTSET: En caso de error en TRY, devuelve una única fila con Id=0, IdDS=NULL, Code=''999'' y Message con ERROR_MESSAGE() y ERROR_LINE()', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_SupportPaymentSuppliers';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El filtro de proveedor (Supplier) viene no vacío en el XML → Activa filtrado por proveedores y carga la lista de IDs separados por coma en una tabla temporal para restringir las cuentas por pagar else No se aplica filtro por proveedor (se incluyen todos); si El filtro de factura (Invoice) viene no vacío en el XML → Activa filtrado por factura y carga la lista de IDs de cuentas por pagar para restringir el resultado else No se aplica filtro por factura; si El filtro de comprobante (Voucher) viene no vacío en el XML → Activa filtrado por comprobante, restringiendo tanto el CTE de transacciones de comprobante como exigiendo que la cuenta por pagar tenga al menos un comprobante asociado else No se aplica filtro por comprobante; las cuentas sin comprobante también aparecen vía LEFT JOIN; si RetencionType de la cuenta contable contable = 1 / 2 / 3 / 4 → Se clasifica el total de retenciones como ReteFuente (1), ReteIVA (2), ReteICA (3) o ReteOthers (4) en columnas separadas del resultado; si ExpenseType del comprobante de tesorería = 1, 2 o 3 → Se traduce a etiqueta de negocio ''Pago'', ''Reembolso'' o ''Traslado'' respectivamente; si Naturaleza de la nota de pago (pn.Nature) = 1 → El AdjusmentValue se acumula como DebitValue de la nota else El AdjusmentValue se acumula como CreditValue de la nota; si Ocurre cualquier error en el bloque TRY → Devuelve un registro único con Id=0, Code=''999'' y Message con el mensaje y línea del error en lugar de los datos', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_SupportPaymentSuppliers';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_SupportPaymentSuppliers';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payments.AccountPayableDetailConcept; GeneralLedger.MainAccounts; Treasury.VoucherTransaction; Treasury.VoucherTransactionDetails; Treasury.DischargeBill; Common.SupplierBankAccount; Payments.PaymentNotes; Payments.PaymentsNoteDetails; Payments.PaymentNotesAccountPayableAdvance; Payments.AccountPayable; Common.Supplier; Common.Currency', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_SupportPaymentSuppliers';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_SupportPaymentSuppliers';
-- GO
