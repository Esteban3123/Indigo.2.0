




/*******************************************************************************************************************
Nombre: [Report].[ViewConciliacionProveedores]
Tipo:Vista
Observacion:Conciliación de proveedores 
Profesional: Nilsson Miguel Galindo Lopez
Fecha:
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Version 1
Persona que modifico: Nilsson Miguel Galindo Lopez
Fecha:09-02-2023
Ovservaciones:Se reordenan los campos y se agrega el campo de subtotal
------------------------------------------------------------------------------------------
Version 2
Persona que modifico: Nilsson Miguel Galindo Lopez
Fecha:29-02-2024
Observaciones:se agrega el cte sub total
--------------------------------------------------------------------------------------------
Version 3
Persona que modifico:Nilsson Miguel Galindo Lopez
Fecha:26-03-2024
Observaciones:Se agrgan las devoluciones de compra y tambien los saldos iniciales.
***********************************************************************************************************************************/

CREATE view [Report].[ViewConciliacionProveedores] as


WITH

CTE_CONCEPTOS AS
(
SELECT 
AD.IdAccountPayable,
SUM(AD.[VALUE]) AS [VALUE],
AD.NATURE
FROM 
Payments.AccountPayableDetailConcept AD INNER JOIN
Payments.AccountPayableConcepts AC ON AD.IdConceptAccountPayable=AC.Id AND IdRetentionConcept IS NULL
--WHERE AD.IdAccountPayable=6823
GROUP BY AD.IdAccountPayable,AD.NATURE
),

CTE_IVA AS
(
	SELECT 
	AD.IdAccountPayable,
	SUM(AD.[VALUE]) AS [TOTAL],
	SUM(BaseValue) AS IVA
	FROM 
	Payments.AccountPayableDetailConcept AD INNER JOIN
	Payments.AccountPayableConcepts AC ON AD.IdConceptAccountPayable=AC.Id AND AC.[Name] LIKE '%RETENCION IVA%'
	--WHERE AD.IdAccountPayable=4989
	GROUP BY AD.IdAccountPayable
),
CTE_RTF AS
(
	SELECT 
	AD.IdAccountPayable,
	SUM(AD.[VALUE]) AS [TOTAL]
	FROM 
	Payments.AccountPayableDetailConcept AD INNER JOIN
	Payments.AccountPayableConcepts AC ON AD.IdConceptAccountPayable=AC.Id AND (AC.[Name] LIKE '%RETENCION HONORARIOS%' OR  AC.[Name] LIKE '%RETENCION COMPRAS%' 
																														OR AC.[Name] LIKE '%RETENCION TRANSPORTE%'
																														OR  AC.[Name] LIKE '%RETENCION SERVICIOS ASEO%'
																														OR  AC.[Name] LIKE '%RETENCION SERVICIOS HOTELES%'
																														OR  AC.[Name] LIKE '%RETENCION SERVICIOS DECLARANTES%'
																														OR  AC.[Name] LIKE '%RETENCION SERVICIOS NO%'
																														OR  AC.[Name] LIKE '%RETENCION ARRENDAMIENTO%'
																														OR  AC.[Name] LIKE '%RETENCION PAGOS LAB%'
																														OR  AC.[Name] LIKE '%RETENCION RENDIMIENTOS%'
																														OR  AC.[Name] LIKE '%RETENCION COMBUSTIBLE%'
																														OR  AC.[Name] LIKE '%RETENCION CONTRATOS%')
	--WHERE AD.IdAccountPayable=6823
	GROUP BY AD.IdAccountPayable
),
CTE_ICA AS
(
	SELECT 
	AD.IdAccountPayable,
	SUM(AD.[VALUE]) AS [TOTAL]
	FROM 
	Payments.AccountPayableDetailConcept AD INNER JOIN
	Payments.AccountPayableConcepts AC ON AD.IdConceptAccountPayable=AC.Id AND AC.[Name] LIKE '%RETENCION DE ICA%'
	--WHERE AD.IdAccountPayable=4989
	GROUP BY AD.IdAccountPayable
),

CTE_DESCUENTOS_FINANCIEROS AS
(
	SELECT 
	AccountPayableId,
	SUM(PNAP.AdjustmentValueShare) AS [VALUE],'CREDITO' AS NATURE
	FROM
	Payments.PaymentNotesAccountPayableAdvance PNAP  INNER JOIN
	Payments.PaymentNotes NCP ON PNAP.PaymentNoteId=NCP.Id AND NCP.Status=2 INNER JOIN
	Payments.PaymentsNoteDetails NCPD ON NCP.ID=NCPD.IdPaymentsNote AND IdAccount=2998
	--where AccountPayableid=3638
	GROUP BY AccountPayableId
),
CTE_NUMERO_CCP AS 
(
	SELECT 
	AD.IdAccountPayable,
	COUNT(AD.ID) AS NOTAS,
	AD.NATURE
	FROM 
	Payments.AccountPayableDetailConcept AD INNER JOIN
	Payments.AccountPayableConcepts AC ON AD.IdConceptAccountPayable=AC.Id AND IdRetentionConcept IS NULL --AND AC.[Name] NOT LIKE '%DESCUENTO FINANCIERO%'
	--WHERE AD.IdAccountPayable=6823
	GROUP BY AD.IdAccountPayable,AD.NATURE
),

CTE_NUMERO AS 
(
	SELECT
	AccountPayableId,
	COUNT(AccountPayableId) AS NOTAS
	FROM
	Payments.PaymentNotesAccountPayableAdvance PNAP  INNER JOIN
	Payments.PaymentNotes NCP ON PNAP.PaymentNoteId=NCP.Id AND NCP.Status=2 INNER JOIN
	Payments.PaymentsNoteDetails NCPD ON NCP.ID=NCPD.IdPaymentsNote AND IdAccount!=2998
	GROUP BY AccountPayableId
),

CTE_NOTAS AS 
(
	SELECT 
	IIF(NCPD.Nature=1,SUM(PNAP.ADJUSTMENTVALUESHARE),1) AS DEBITO,
	IIF(NCPD.Nature=2,SUM(PNAP.ADJUSTMENTVALUESHARE),2) AS CREDITO,
	PNAP.AccountPayableId
	FROM
	Payments.PaymentNotesAccountPayableAdvance PNAP  INNER JOIN
	Payments.PaymentNotes NCP ON PNAP.PaymentNoteId=NCP.Id AND NCP.Status=2 INNER JOIN
	Payments.PaymentsNoteDetails NCPD ON NCP.ID=NCPD.IdPaymentsNote AND IdAccount!=2998
	--where PNAP.AccountPayableId=6823
	GROUP BY PNAP.AccountPayableId,NCPD.Nature

	--SELECT * FROM Payments.PaymentsNoteDetails WHERE IdPaymentsNote=2364
	--SELECT * FROM Payments.AccountPayableConcepts WHERE [NAME]LIKE '%DESCUENTO FINAN%'
),

CTE_CUENTAS_BANCARIAS AS
(
	SELECT 
	ROW_NUMBER ( )   
	OVER (PARTITION BY SupplierId  order by SupplierId DESC) 'NUMERO',
	SupplierId,Number,BAN.Name
	FROM
	Common.SupplierBankAccount CB INNER JOIN
	Payroll.Bank BAN ON CB.BankId=BAN.Id
),

CTE_SUBTOTAL AS
(
SELECT 
IdAccountPayable,
null AS SUBTOTAL
FROM 
Payments.AccountPayableDetailConcept 
WHERE Percentage IS NULL --AND IdAccountPayable=31854
GROUP BY IdAccountPayable
),
--IN V3
CTE_CONCEPTOS_NOTAS AS
(
SELECT 
IdPaymentsNote,
COUNT(NATURE) AS NUMERO,
SUM(VALUE) AS VALOR,
CASE Nature WHEN 1 THEN 'DEBITO' ELSE 'CREDITO' END AS NATURALEZA
FROM Payments.PaymentsNoteDetails 
WHERE Comments NOT LIKE '%RTF%' AND Comments NOT LIKE '%RETENCION ICA%' AND Comments NOT LIKE '%RETENCION IVA%'
GROUP BY IdPaymentsNote,Nature
),

CTE_NOTA_BASE AS
(
SELECT 
IdPaymentsNote,
BaseValue
FROM Payments.PaymentsNoteDetails 
WHERE Comments LIKE '%RTF%' AND Comments LIKE '%RETENCION ICA%'
GROUP BY IdPaymentsNote,BaseValue
),

CTE_NOTA_BASE_IVA AS
(
SELECT 
IdPaymentsNote,
BaseValue
FROM Payments.PaymentsNoteDetails 
WHERE Comments LIKE '%RETENCION IVA%'
GROUP BY IdPaymentsNote,BaseValue
),

CTE_NOTA_RTF AS
(
SELECT 
IdPaymentsNote,
COUNT(NATURE) AS NUMERO,
SUM(VALUE) AS VALOR,
CASE Nature WHEN 1 THEN 'DEBITO' ELSE 'CREDITO' END AS NATURALEZA
FROM Payments.PaymentsNoteDetails 
WHERE Comments LIKE '%RTF%'
GROUP BY IdPaymentsNote,Nature
),

CTE_NOTA_ICA AS
(
SELECT 
IdPaymentsNote,
COUNT(NATURE) AS NUMERO,
SUM(VALUE) AS VALOR,
CASE Nature WHEN 1 THEN 'DEBITO' ELSE 'CREDITO' END AS NATURALEZA
FROM Payments.PaymentsNoteDetails 
WHERE Comments LIKE '%RETENCION ICA%'
GROUP BY IdPaymentsNote,Nature
),
CTE_NOTA_IVA AS
(
SELECT 
IdPaymentsNote,
COUNT(NATURE) AS NUMERO,
SUM(VALUE) AS VALOR,
SUM(BaseValue) AS TOTAL,
CASE Nature WHEN 1 THEN 'DEBITO' ELSE 'CREDITO' END AS NATURALEZA
FROM Payments.PaymentsNoteDetails 
WHERE Comments LIKE '%RETENCION IVA%'
GROUP BY IdPaymentsNote,Nature
),
CTE_BASE AS
--TRAE LE VALOR VASE PARA REALIZAR EL CALCULOS DE LAS DIFERENTES RETENCIONES.
(
SELECT 
APDC.BaseValue AS [BASE RETENCION],
APDC.IdAccountPayable
FROM 
Payments.AccountPayableDetailConcept APDC WHERE DETAIL LIKE '%RTF%'
GROUP BY APDC.IdAccountPayable,APDC.BaseValue
--DETAIL LIKE '%IVA TARIFA%'
--DETAIL LIKE '%IVA ARTICULO%'
),
CTE_BASE_IVA AS
--TRAE LE VALOR VASE PARA REALIZAR EL CALCULOS DE LAS DIFERENTES RETENCIONES.
(
SELECT 
APDC.BaseValue AS [BASE RETENCION],
APDC.IdAccountPayable
FROM 
Payments.AccountPayableDetailConcept APDC WHERE DETAIL LIKE '%IVA TARIFA%' OR DETAIL LIKE '%IVA ARTICULO%'
GROUP BY APDC.IdAccountPayable,APDC.BaseValue 
)
--FN V3

SELECT 
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
CP.ConfirmationDate AS [FECHA DEL DOCUMENTO],
CASE CP.EntityName WHEN 'EntranceVoucher' THEN 'COMPROBANTE DE ENTRADA' 
				   WHEN 'InitialBalance' THEN 'SALDO INICIAL' 
				   WHEN 'FixedAssetEntry' THEN 'ENTRADA DE ACTIVOS FIJOS' ELSE 'CUENTAS POR PAGAR' END [TIPO DE DOCUMENTO],
CP.Code AS [DOCUMENTO CxP],
PRO.Code+' - '+PRO.Name AS PROVEEDOR,
CP.BillNumber AS FACTURA,
CP.InvoiceValue as [VALOR TOTAL FACTURA],
CASE CP.STATUS WHEN 1 THEN 'REGISTRADO'
			   WHEN 2 THEN 'CONFIRMADO'
			   WHEN 3 THEN 'ANULADO' END AS ESTADO,
NT1.NOTAS AS [#CONCEPTO DEBITO CxP],
DEB.[Value] AS [VALOR CONCEPTO DEBITO],
BIVA.[BASE RETENCION] AS [BASE IVA],
IVA.IVA,
SUB.SUBTOTAL AS [VALOR SUBTOTAL],
NT2.NOTAS AS [#CONCEPTO CREDITO CxP],
CRE.[Value] AS [VALOR CONCEPTO CREDITO],
B.[BASE RETENCION],
RTF.TOTAL AS [RETEFUENTE],
ICA.TOTAL AS [RETEICA],
IVA.TOTAL AS [RETEIVA],
CP.[Value] AS [VALOR CxP],
NUM.NOTAS AS [#NOTAS CxP],
NOTAS.CREDITO AS [DEBITO NOTA CxP],
NOTA.DEBITO AS [CREDITO NOTA CxP],
CP.BALANCE AS [SALDO CxP],
PP.DiscountRate AS [PORCENTAJE PRONTO PAGO],--Porcentaje de descuento
DFN.[VALUE] AS [DESCUENTOS FINANCIERO],
CE.Code AS [COMPROBANTE EGRESO],
CE.[VALUE] AS [VALOR TOTAL COMPROBANTE EGRESO],
CE.CreationDate AS [FECHA COMPROBANTE EGRESO],
FD.AdvancedValue AS [COMPROBANTE EGRESO FACTURA],
FE.NameConcept AS [FLUJO DE EFECTIVO],
CB.Number AS [CUENTA BANCARIA],
CB.[NAME] AS BANCO,
NULL AS [CODIGO DEL DOCUMENTO],
NULL AS [CODIGO NOTA CxP],
NULL AS [TOTAL CxP],
NULL AS [VALOR TOTAL C.E],
NULL AS [FECHA C.E],
NULL AS [C.E FACTURA],
NULL AS [VALOR ANTICIPO],
NULL AS [SALDO],
1 as 'CANTIDAD',
CAST(CP.ConfirmationDate AS date) AS 'FECHA BUSQUEDA',
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM 
Payments.AccountPayable CP 
INNER JOIN Common.Supplier PRO ON CP.IdSupplier=PRO.Id 
INNER JOIN CTE_CONCEPTOS DEB ON CP.ID=DEB.IdAccountPayable AND DEB.NATURE=1 
LEFT JOIN CTE_SUBTOTAL SUB ON CP.Id=SUB.IdAccountPayable
LEFT JOIN CTE_NUMERO_CCP NT1 ON CP.ID=NT1.IdAccountPayable AND NT1.NATURE=1 
LEFT JOIN CTE_CONCEPTOS CRE ON CP.Id=CRE.IdAccountPayable AND CRE.NATURE=2 
LEFT JOIN CTE_NUMERO_CCP NT2 ON CP.ID=NT2.IdAccountPayable AND NT2.NATURE=2 
LEFT JOIN CTE_NUMERO NUM ON CP.Id=NUM.AccountPayableId 
LEFT JOIN CTE_NOTAS NOTA ON CP.Id=NOTA.AccountPayableId AND NOTA.CREDITO='2.00' 
LEFT JOIN CTE_NOTAS NOTAS ON CP.Id=NOTAS.AccountPayableId AND NOTAS.DEBITO='1.00' 
LEFT JOIN CTE_IVA IVA ON CP.Id=IVA.IdAccountPayable 
LEFT JOIN CTE_RTF RTF ON CP.Id=RTF.IdAccountPayable 
LEFT JOIN CTE_ICA ICA ON CP.Id=ICA.IdAccountPayable 
LEFT JOIN CTE_DESCUENTOS_FINANCIEROS DFN ON CP.Id=DFN.AccountPayableId 
LEFT JOIN Treasury.DischargeBill FD ON CP.ID=FD.IdAccountPayable AND FD.ID=(SELECT MAX(F.ID) FROM Treasury.DischargeBill F WHERE FD.IdAccountPayable=F.IdAccountPayable) 
LEFT JOIN Treasury.VoucherTransactionDetails CED ON FD.IdVoucherTransactionD=CED.Id 
LEFT JOIN Treasury.VoucherTransaction CE ON CED.IdVoucherTransaction=CE.Id AND CE.STATUS=2 
LEFT JOIN CTE_CUENTAS_BANCARIAS CB ON PRO.ID=CB.SupplierId AND CB.NUMERO=1  
LEFT JOIN Treasury.CashFlowConcept FE ON CED.IdCashFlowConcept=FE.Id 
LEFT JOIN Common.PromptPaymentDiscount PP ON PRO.ID=PP.SUPPLIERID
LEFT JOIN CTE_BASE B ON CP.ID=B.IdAccountPayable
LEFT JOIN CTE_BASE_IVA BIVA ON CP.Id=BIVA.IdAccountPayable
--WHERE CP.BillNumber='FECT1132'

UNION ALL
-----------------------------------DEVOLUCIÓN DE COMPRA------------------------------------------------------------------------------
SELECT
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
PN.NoteDate AS [FECHA DEL DOCUMENTO],
CASE PN.EntityName WHEN 'EntranceVoucherDevolution' THEN 'DEVOLUCION DE COMPRA' ELSE 'NOTA CxP' END AS [TIPO DE DOCUMENTO],
CP.Code AS [DOCUMENTO CxP],
PRO.Code+' - '+PRO.Name AS PROVEEDOR,
CP.BillNumber AS FACTURA,
-(CP.InvoiceValue) as [VALOR TOTAL FACTURA],
CASE PN.STATUS WHEN 1 THEN 'REGISTRADO'
			   WHEN 2 THEN 'CONFIRMADO'
			   WHEN 3 THEN 'ANULADO' END AS ESTADO,
CCND.NUMERO AS [#CONCEPTO DEBITO CxP],
CCND.VALOR AS [VALOR CONCEPTO DEBITO],
BIVA.BaseValue AS [BASE IVA],
-(IVA.VALOR) AS [IVA],
NULL AS [VALOR SUBTOTAL],
CCNC.NUMERO AS [#CONCEPTO CREDITO CxP],
CCNC.VALOR AS [VALOR CONCEPTO CREDITO],
B.BaseValue AS [BASE RETENCION],
-(RTF.VALOR) AS [RETEFUENTE],
-(ICA.VALOR) AS [RETEICA],
-(IVA.TOTAL) AS [RETEIVA],
-(CP.Value) AS [VALOR CxP],
NULL AS [#NOTAS CxP],
NULL AS [DEBITO NOTA CxP],
NULL AS [CREDITO NOTA CxP],
CP.BALANCE AS [SALDO CxP],
NULL AS [PORCENTAJE PRONTO PAGO],--Porcentaje de descuento
NULL AS [DESCUENTOS FINANCIERO],
NULL AS [COMPROBANTE EGRESO],
NULL AS [VALOR TOTAL COMPROBANTE EGRESO],
NULL AS [FECHA COMPROBANTE EGRESO],
NULL AS [COMPROBANTE EGRESO FACTURA],
NULL AS [FLUJO DE EFECTIVO],
NULL AS [CUENTA BANCARIA],
NULL AS BANCO,
PN.EntityCode AS [CODIGO DEL DOCUMENTO],
PN.Code AS [CODIGO NOTA CxP],
-(CCND.VALOR) AS [TOTAL CxP],
NULL AS [VALOR TOTAL C.E],
NULL AS [FECHA C.E],
NULL AS [C.E FACTURA],
NULL AS [VALOR ANTICIPO],
NULL AS [SALDO],
1 as 'CANTIDAD',
CAST(CP.DocumentDate AS date) AS 'FECHA BUSQUEDA',
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM 
Payments.PaymentNotes PN
INNER JOIN Payments.PaymentNotesAccountPayableAdvance PNAPA ON PN.Id=PNAPA.PaymentNoteId AND PN.Status=2
INNER JOIN Payments.AccountPayable CP ON PNAPA.AccountPayableId=CP.Id
INNER JOIN Common.Supplier PRO ON CP.IdSupplier=PRO.Id
LEFT JOIN CTE_CONCEPTOS_NOTAS CCND ON PN.Id=CCND.IdPaymentsNote AND CCND.NATURALEZA='DEBITO'
LEFT JOIN CTE_CONCEPTOS_NOTAS CCNC ON PN.Id=CCNC.IdPaymentsNote AND CCNC.NATURALEZA='CREDITO'
LEFT JOIN CTE_NOTA_BASE B ON PN.Id=B.IdPaymentsNote
LEFT JOIN CTE_NOTA_RTF RTF ON PN.Id=RTF.IdPaymentsNote
LEFT JOIN CTE_NOTA_ICA ICA ON PN.Id=ICA.IdPaymentsNote
LEFT JOIN CTE_NOTA_IVA IVA ON PN.Id=IVA.IdPaymentsNote
LEFT JOIN CTE_NOTA_BASE_IVA BIVA ON PN.Id=BIVA.IdPaymentsNote
UNION ALL 
------------------------------------------------------------------------------------------------------------------
SELECT 
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
PA.DocumentDate AS [FECHA DEL DOCUMENTO],
CASE WHEN IBA.ID IS NOT NULL THEN 'ANTICIPOS SALDOS INICIALES' ELSE 'ANTICPOS' END AS [TIPO DE DOCUMENTO],
CP.Code AS [DOCUMENTO CxP],
PRO.Code+' - '+PRO.Name AS PROVEEDOR,
CP.BillNumber AS FACTURA,
CP.Value AS [VALOR TOTAL FACTURA],
CASE PA.Status WHEN 1 THEN 'Registrado'
			   WHEN 2 THEN 'Confirmado'
			   WHEN 3 THEN 'Anulado' END AS ESTADO,
NULL AS [#CONCEPTO DEBITO CxP],
NULL AS [VALOR CONCEPTO DEBITO],
NULL AS [BASE IVA],
NULL AS [IVA],
NULL AS [VALOR SUBTOTAL],
NULL AS [#CONCEPTO CREDITO CxP],
NULL AS [VALOR CONCEPTO CREDITO],
NULL AS [BASE RETENCION],
NULL AS [RETEFUENTE],
NULL AS [RETEICA],
NULL AS [RETEIVA],
CP.Value AS [VALOR CxP],
NULL AS [#NOTAS CxP],
NULL AS [DEBITO NOTA CxP],
NULL AS [CREDITO NOTA CxP],
NULL AS [SALDO CxP],
NULL AS [PORCENTAJE PRONTO PAGO],--Porcentaje de descuento
NULL AS [DESCUENTOS FINANCIERO],
NULL AS [COMPROBANTE EGRESO],
NULL AS [VALOR TOTAL COMPROBANTE EGRESO],
NULL AS [FECHA COMPROBANTE EGRESO],
NULL AS [COMPROBANTE EGRESO FACTURA],
NULL AS [FLUJO DE EFECTIVO],
NULL AS [CUENTA BANCARIA],
NULL AS BANCO,
PA.CODE AS [CODIGO DEL DOCUMENTO],
NULL AS [CODIGO NOTA CxP],
NULL AS [TOTAL CxP],
NULL AS [VALOR TOTAL C.E],
NULL AS [FECHA C.E],
NULL AS [C.E FACTURA],
CP.Value AS [VALOR ANTICIPO],
CP.Balance AS [SALDO],
1 as 'CANTIDAD',
CAST(PT.DocumentDate AS date) AS 'FECHA BUSQUEDA',
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM 
Payments.AdvancePayments PA
left JOIN Payments.PaymentTransfer PT ON PA.Id=PT.AdvancePaymentId
left JOIN Payments.PaymentTransferDetail PTD ON PT.Id=PTD.PaymentTransferId
left JOIN Common.Supplier PRO ON PA.IdSupplier=PRO.Id
LEFT JOIN Payments.AccountPayable CP ON PTD.AccountPayableId=CP.Id
LEFT JOIN Payments.InitialBalanceAdvance IBA ON PA.ID=IBA.AdvancePaymentsId



--SELECT * FROM Payments.AccountPayable WHERE Code='0000026222'
--SELECT * FROM Payments.AccountPayableDetailConcept WHERE IdAccountPayable=31854
--SELECT * FROM Payments.AccountPayableConcepts where ID=889
--SELECT * FROM Payments.PaymentNotesAccountPayableAdvance where PaymentNoteID=3638
--SELECT * FROM Payments.PaymentNotes WHERE CODE IN (0305)
--SELECT * FROM Payments.PaymentsNoteDetails WHERE IdPaymentsNote IN (0305)
--SELECT * FROM Treasury.VoucherTransaction WHERE CODE='0000002293'
--select * from Treasury.VoucherTransactionDetails where idvouchertransaction=2304
--IdExpenseConcept

--select * from Treasury.DischargeBill where IdAccountPayable=3638

--select * from Treasury.ExpenseConcepts where description like '%GASTO%'
--SELECT * FROM Treasury.DischargeBill where IdAccountPayable='3744'
--SELECT * FROM Treasury.VoucherTransactionDetails WHERE id in (2627,2750)
--SELECT * FROM Treasury.VoucherTransaction WHERE id in (2293,2380)
--SELECT * FROM Payments.AccountPayableConcepts  WHERE Name LIKE '%GASTO%' AND Name LIKE '%FINAN%'
 --select * from Common.SupplierBankAccount WHERE NUMBER LIKE '%11100501%' OR Number LIKE '%102-33300-2%'

--SELECT * FROM Common.Supplier where id=11335
--select * from Common.ThirdParty where id=346351
--select * from Common.PromptPaymentDiscount where supplierid=11335
--830013234 - CORALMEDICA LTDA.

--SELECT * FROM Treasury.SchedulePaymentDetail where supplierid=11335