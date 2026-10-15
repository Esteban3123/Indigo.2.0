-- =============================================
-- Author:		Hector Rodriguez Rubiano
-- Create date: 2019-11-26
-- Description:	Cambia el concepto de flujo de efectivo
-- =============================================
CREATE PROCEDURE [Treasury].[SP_CashFlowReclassification]
	-- Add the parameters for the stored procedure here
	@Parameters as xml
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @TABLE AS TABLE
	(	DocumentId	int
		,PreviousCashFlowConcept	int null
		,CurrentCashFlowConcept	int,
		CreditValue decimal(18,2) null,
		DebitValue decimal(18,2) null
	)
	DECLARE @DocumentType tinyint
	begin try

		insert into @TABLE 
		select 
		t.x.value('DI[1]','int'),
		t.x.value('PCFC[1]','int'),
		t.x.value('CCFC[1]','int'),
		----Documento factoring diferencias los provedores 
		t.x.value('CreditValue[1]','decimal(18,2)'),
		t.x.value('DebitValue[1]','decimal(18,2)')
		from @Parameters.nodes('/CFR/CFRD') t(x)

		Select 
		@DocumentType =  t.x.value('DT[1]','int')
		from @Parameters.nodes('/CFR') t(x)

		IF @DocumentType = 1
		BEGIN
			update CRD set IdCashFlowConcept = t.CurrentCashFlowConcept 
			FROM @TABLE T
			INNER JOIN Treasury.CashReceipts CR WITH(NOLOCK)
			ON CR.Id = T.DocumentId
			INNER JOIN Treasury.CashReceiptDetails CRD WITH(NOLOCK)
			ON CRD.IdCashReceipt = CR.Id
			AND CRD.Nature = 2
			AND ((CRD.IdCashFlowConcept IS NULL AND ISNULL(T.PreviousCashFlowConcept,0) = 0) OR (CRD.IdCashFlowConcept IS NOT NULL AND CRD.IdCashFlowConcept = ISNULL(T.PreviousCashFlowConcept,0)))
		END

		IF @DocumentType = 2
		BEGIN
			update VTD set IdCashFlowConcept = t.CurrentCashFlowConcept 
			FROM @TABLE T
			INNER JOIN Treasury.VoucherTransaction VT WITH(NOLOCK)
			ON VT.Id = T.DocumentId AND VT.VoucherClass = 1 --PAGOS, Si se quita voucherclass se debe agregar id de caja o id de banco para que no duplique los valores
			INNER JOIN Treasury.VoucherTransactionDetails VTD WITH(NOLOCK)
			ON VTD.IdVoucherTransaction = VT.ID
				AND ((VTD.IdCashFlowConcept IS NULL AND ISNULL(T.PreviousCashFlowConcept,0) = 0) OR (VTD.IdCashFlowConcept IS NOT NULL AND VTD.IdCashFlowConcept = ISNULL(T.PreviousCashFlowConcept,0)))
		END

		IF @DocumentType = 4
		BEGIN
			update TND set IdCashFlowConcept = t.CurrentCashFlowConcept 
			FROM @TABLE T
			INNER JOIN Treasury.TreasuryNote TN WITH(NOLOCK)
			ON TN.Id = T.DocumentId
			INNER JOIN Treasury.TreasuryNoteDetail TND WITH(NOLOCK)
			ON TND.TreasuryNoteId = TN.ID
			AND ((TND.IdCashFlowConcept IS NULL AND ISNULL(T.PreviousCashFlowConcept,0) = 0) OR (TND.IdCashFlowConcept IS NOT NULL AND TND.IdCashFlowConcept = ISNULL(T.PreviousCashFlowConcept,0)))
		END

		IF @DocumentType = 5
		BEGIN
			update CXC set IdCashFlowConcept = t.CurrentCashFlowConcept 
			FROM @TABLE T
			INNER JOIN Treasury.CrossingAccount CA WITH(NOLOCK)
			ON CA.Id = T.DocumentId
			INNER JOIN Treasury.CrossingAccountDetailCxC CXC WITH(NOLOCK)
			ON CXC.CrossingAccountId = CA.ID
			AND ((CXC.IdCashFlowConcept IS NULL AND ISNULL(T.PreviousCashFlowConcept,0) = 0) OR (CXC.IdCashFlowConcept IS NOT NULL AND CXC.IdCashFlowConcept = ISNULL(T.PreviousCashFlowConcept,0)))

			update CXP set IdCashFlowConcept = t.CurrentCashFlowConcept 
			FROM @TABLE T
			INNER JOIN Treasury.CrossingAccount CA WITH(NOLOCK)
			ON CA.Id = T.DocumentId
			INNER JOIN Treasury.CrossingAccountDetailCxP CXP WITH(NOLOCK)
			ON CXP.CrossingAccountId = CA.ID
			AND ((CXP.IdCashFlowConcept IS NULL AND ISNULL(T.PreviousCashFlowConcept,0) = 0) OR (CXP.IdCashFlowConcept IS NOT NULL AND CXP.IdCashFlowConcept = ISNULL(T.PreviousCashFlowConcept,0)))

			update OC set IdCashFlowConcept = t.CurrentCashFlowConcept 
			FROM @TABLE T
			INNER JOIN Treasury.CrossingAccount CA WITH(NOLOCK)
			ON CA.Id = T.DocumentId
			INNER JOIN Treasury.CrossingAccountDetailOtherConcept OC WITH(NOLOCK)
			ON OC.CrossingAccountId = CA.ID
			AND ((OC.IdCashFlowConcept IS NULL AND ISNULL(T.PreviousCashFlowConcept,0) = 0) OR (OC.IdCashFlowConcept IS NOT NULL AND OC.IdCashFlowConcept = ISNULL(T.PreviousCashFlowConcept,0)))
		END

		IF @DocumentType = 6
		BEGIN
		   -- se le debita el saldo del proveedor anterior
			update fdd set fdd.IdCashFlowConceptExpense = t.CurrentCashFlowConcept 			
			from @TABLE T
		    INNER JOIN Payments.FactoringDocumentDetail fdd WITH(NOLOCK) ON fdd.Id = T.DocumentId
			WHERE t.CreditValue  = 0  and t.DebitValue > 0
		
			-- se le acredita el saldo al nuevo proveedor 
			update fdd set fdd.IdCashFlowConceptIncome = t.CurrentCashFlowConcept 					
			from @TABLE T
		   JOIN Payments.FactoringDocumentDetail fdd WITH(NOLOCK) ON fdd.Id = T.DocumentId
			WHERE t.CreditValue > 0  and t.DebitValue = 0

		END

		select '0' as CodeMessage, 'Se actualizó correctamente el concepto de flujo de efectivo de los documentos' as Message, cast(1 as tinyint) as [Status]
	end try
	begin catch
		select '0' as CodeMessage, ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() as varchar(5)) as Message, cast(3 as tinyint) as [Status]
	end catch

END