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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que reclasifica el concepto de flujo de efectivo asignado a los documentos de tesorería, permitiendo corregir o cambiar la categoría de flujo (ingreso, egreso, traslado, etc.) en los detalles de recibos de caja, comprobantes de egreso/pago, notas de tesorería, cruce de cuentas por cobrar y por pagar, y documentos de factoring. Recibe como parámetro un XML con la lista de documentos afectados, el tipo de documento (recibo de caja, voucher de pago, nota de tesorería, cruce de cuentas o factoring), el concepto anterior y el nuevo concepto de flujo de caja a aplicar. Se utiliza en el módulo de tesorería para corregir la clasificación del flujo de efectivo en el estado de flujos de efectivo contable, garantizando que cada movimiento quede asociado al concepto correcto antes del cierre o la generación de reportes financieros.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_CashFlowReclassification';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_CashFlowReclassification';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reclasifica el concepto de flujo de efectivo en los detalles de distintos documentos de tesorería (recibos, comprobantes, notas, cruces y factoring) según el tipo de documento indicado en el XML.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_CashFlowReclassification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe tener nodo raíz /CFR con un elemento DT (DocumentType) y nodos /CFR/CFRD con DI (DocumentId), PCFC (PreviousCashFlowConcept), CCFC (CurrentCashFlowConcept), CreditValue y DebitValue.; DocumentType debe ser uno de {1, 2, 4, 5, 6}; otros valores no producen actualización alguna.; Los DocumentId referenciados deben existir en la tabla maestra correspondiente al tipo de documento.; El concepto de flujo previo enviado (PCFC) debe coincidir con el actualmente almacenado en el detalle (o ambos ser nulos/cero) para que el registro sea reclasificado.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_CashFlowReclassification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Concepto de flujo de efectivo; Recibo de caja; Comprobante de egreso (pago); Nota de tesorería; Cruce de cuentas; Cuentas por cobrar (CxC); Cuentas por pagar (CxP); Factoring; Proveedor; Naturaleza débito/crédito', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_CashFlowReclassification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Treasury.CashReceiptDetails: Si DocumentType=1, actualiza IdCashFlowConcept al nuevo valor en líneas con Nature=2 cuyo IdCashFlowConcept actual coincide con PreviousCashFlowConcept (o ambos son nulos/cero), del recibo de caja indicado.; [UPDATE] Treasury.VoucherTransactionDetails: Si DocumentType=2, actualiza IdCashFlowConcept en los detalles del comprobante cuya cabecera tiene VoucherClass=1 (PAGOS) y cuyo concepto previo coincide con PreviousCashFlowConcept (o ambos nulos/cero).; [UPDATE] Treasury.TreasuryNoteDetail: Si DocumentType=4, actualiza IdCashFlowConcept en los detalles de la nota de tesorería cuyo concepto previo coincide con PreviousCashFlowConcept (o ambos nulos/cero).; [UPDATE] Treasury.CrossingAccountDetailCxC: Si DocumentType=5, actualiza IdCashFlowConcept en el detalle CxC del cruce de cuentas cuyo concepto previo coincide con PreviousCashFlowConcept (o ambos nulos/cero).; [UPDATE] Treasury.CrossingAccountDetailCxP: Si DocumentType=5, actualiza IdCashFlowConcept en el detalle CxP del cruce de cuentas cuyo concepto previo coincide con PreviousCashFlowConcept (o ambos nulos/cero).; [UPDATE] Treasury.CrossingAccountDetailOtherConcept: Si DocumentType=5, actualiza IdCashFlowConcept en el detalle de otros conceptos del cruce cuyo concepto previo coincide con PreviousCashFlowConcept (o ambos nulos/cero).; [UPDATE] Payments.FactoringDocumentDetail: Si DocumentType=6 y la línea es débito (CreditValue=0 AND DebitValue>0), actualiza IdCashFlowConceptExpense con el concepto nuevo (débito al saldo del proveedor anterior).; [UPDATE] Payments.FactoringDocumentDetail: Si DocumentType=6 y la línea es crédito (CreditValue>0 AND DebitValue=0), actualiza IdCashFlowConceptIncome con el concepto nuevo (crédito al saldo del nuevo proveedor).; [RETURN_RESULT] (resultset): Al finalizar exitosamente devuelve CodeMessage=''0'', mensaje de éxito y Status=1; ante excepción devuelve CodeMessage=''0'', ERROR_MESSAGE()+línea y Status=3.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_CashFlowReclassification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @DocumentType = 1 → Reclasifica IdCashFlowConcept en CashReceiptDetails (sólo líneas con Nature=2) del recibo de caja.; si @DocumentType = 2 → Reclasifica IdCashFlowConcept en VoucherTransactionDetails de comprobantes con VoucherClass=1 (pagos).; si @DocumentType = 4 → Reclasifica IdCashFlowConcept en TreasuryNoteDetail de la nota de tesorería.; si @DocumentType = 5 → Reclasifica IdCashFlowConcept en los tres detalles del cruce de cuentas: CxC, CxP y OtherConcept.; si @DocumentType = 6 → Reclasifica en FactoringDocumentDetail: IdCashFlowConceptExpense para líneas débito y IdCashFlowConceptIncome para líneas crédito. else Si DocumentType no es 1,2,4,5,6 no se ejecuta ninguna actualización pero igual se retorna mensaje de éxito.; si CRD.IdCashFlowConcept IS NULL AND ISNULL(PreviousCashFlowConcept,0)=0  OR  CRD.IdCashFlowConcept = ISNULL(PreviousCashFlowConcept,0) → Sólo se reclasifican los detalles cuyo concepto actual coincide con el concepto previo informado (tratando NULL como 0); los demás se conservan.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_CashFlowReclassification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.CashReceipts; Treasury.CashReceiptDetails; Treasury.VoucherTransaction; Treasury.VoucherTransactionDetails; Treasury.TreasuryNote; Treasury.TreasuryNoteDetail; Treasury.CrossingAccount; Treasury.CrossingAccountDetailCxC; Treasury.CrossingAccountDetailCxP; Treasury.CrossingAccountDetailOtherConcept; Payments.FactoringDocumentDetail', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_CashFlowReclassification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_CashFlowReclassification';
-- GO
