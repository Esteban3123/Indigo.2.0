-- =============================================
-- Author:		Cristian Camilo Bahamon
-- Create date: 2024-08-02
-- Description:	Conciliación de los saldos del mes entre inventario y contabilidad
-- =============================================
CREATE PROCEDURE [Inventory].[SP_ConciliationAccounting]
	 @MonthClosed AS int,
	 @YearClosed AS int
AS
BEGIN	

/* ---------------------------------------- ACCOUNTING ---------------------------------------- */
		
	DECLARE @AllowMainAccount TABLE (
			IdMainAccount INT,
			Number VARCHAR(50)
		);

		INSERT INTO @AllowMainAccount SELECT ma.IdMainAccount, ma.Number
		FROM (
			SELECT
				apc.IdAccount AS IdMainAccount,
				m1.Number
			FROM Inventory.ProductGroup pg WITH(NOLOCK)
			JOIN Payments.AccountPayableConcepts apc WITH(NOLOCK) ON pg.InventoryAccountPayableConceptId = apc.Id
			JOIN GeneralLedger.MainAccounts m1 ON apc.IdAccount = m1.Id
			GROUP BY apc.IdAccount, m1.Number

			UNION ALL

			SELECT
				m2.Id AS IdMainAccount,
				m2.Number
			FROM Inventory.ProductGroup pg
			JOIN GeneralLedger.MainAccounts m2 ON pg.ReferenceInputDebitAccountId = m2.Id
			GROUP BY m2.Id, m2.Number

			UNION ALL

			SELECT
				m3.Id AS IdMainAccount,
				m3.Number
			FROM Inventory.ProductGroup pg
			JOIN GeneralLedger.MainAccounts m3 ON pg.ConsignmentMerchandiseDebitAccountId = m3.Id
			GROUP BY m3.Id, m3.Number
		) ma
		GROUP BY ma.IdMainAccount, ma.Number;

		;WITH CTE_KARDEX_DISPENSACIONES
		AS
		(
			SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
			FROM Inventory .Kardex K WITH (NOLOCK)
			WHERE K.EntityName ='PharmaceuticalDispensing' 
			AND YEAR(K.DocumentDate) = @YearClosed 
			AND MONTH(K.DocumentDate) = @MonthClosed
		 ),

		CTE_CONTABILIDAD_DISPENSACIONES
		AS
		(

			SELECT 
				year(KOT.DocumentDate) [Year],
				month(KOT.DocumentDate) [Month],
				JV.EntityName,
				'DISPENSACION FARMACEUTICA' [Transaction],
				ma.Id MainAccountId,
				MA.NUMBER MainAccountNumber,
				SUM(CAST(DebitValue AS NUMERIC(18,2))) DebitValue,
				SUM(CAST(CreditValue AS NUMERIC(18,2))) CreditValue,
				'NINGUNO' WareHouseTypeName,
				'Confirmado' Status
			FROM GeneralLedger.JournalVouchers JV WITH (NOLOCK)
			INNER JOIN CTE_KARDEX_DISPENSACIONES AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='PharmaceuticalDispensing'
			INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK) ON JVD.IdAccounting=JV.ID
			INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
			WHERE JV.LegalBookId=1
			GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JV.IdJournalVoucher,JV.EntityId,JV.EntityCode,JV.EntityName,
			MA.NUMBER,MA.ID,KOT.DocumentDate,JV.ID
		),

		CTE_KARDEX_DEVOLUCION_DISPENSACIONES
		AS
		(
			SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
			FROM Inventory .Kardex K WITH (NOLOCK)
			WHERE K.EntityName ='PharmaceuticalDispensingDevolution' 
			AND YEAR(K.DocumentDate) = @YearClosed 
			AND MONTH(K.DocumentDate) = @MonthClosed
		),

		CTE_CONTABILIDAD_DEVOLUCION_DISPENSACIONES
		AS
		(

		  SELECT 
		  year(KOT.DocumentDate) [Year],
		  month(KOT.DocumentDate) [Month],
		  JV.EntityName,
		 'DEVOLUCION DISPENSACION FARMACEUTICA' [Transaction],
		  ma.Id MainAccountId,
		  ma.Number MainAccountNumber,
		  SUM(CAST(DebitValue AS NUMERIC(18,2))) DebitValue,
		  SUM(CAST(CreditValue AS NUMERIC(18,2))) CreditValue,
		  'NINGUNO' WareHouseTypeName,
		  'Confirmado' Status
		  FROM GeneralLedger.JournalVouchers JV
		  INNER JOIN CTE_KARDEX_DEVOLUCION_DISPENSACIONES AS KOT WITH (NOLOCK) ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='PharmaceuticalDispensingDevolution'
		  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK) ON JVD.IdAccounting=JV.ID
		  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
		  WHERE JV.LegalBookId=1
		  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JV.IdJournalVoucher,JV.EntityId,JV.EntityCode,JV.EntityName,
		  MA.NUMBER,MA.ID,KOT.DocumentDate,JV.ID
		),

		CTE_KARDEX_AJUSTES
		AS
		(
			SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
			FROM Inventory .Kardex K WITH (NOLOCK)
			WHERE K.EntityName ='InventoryAdjustment' 
			AND YEAR(K.DocumentDate) = @YearClosed 
			AND MONTH(K.DocumentDate) = @MonthClosed
		),

		CTE_CONTABILIDAD_AJUSTES
		AS
		(

		  SELECT
		  year(KOT.DocumentDate) [Year],
		  month(KOT.DocumentDate) [Month],
		  JV.EntityName,
		  'AJUSTE DE INVENTARIOS' [Transaction],
		  ma.Id MainAccountId,
		  ma.Number MainAccountNumber,
		  SUM(CAST(DebitValue AS NUMERIC(18,2))) DebitValue,
		  SUM(CAST(CreditValue AS NUMERIC(18,2))) CreditValue, 
		  'NINGUNO' WareHouseTypeName,
		  'Confirmado' Status
		  FROM GeneralLedger.JournalVouchers JV WITH (NOLOCK)
		  INNER JOIN CTE_KARDEX_AJUSTES AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='InventoryAdjustment'
		  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK)  ON JVD.IdAccounting=JV.ID
		  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
		  WHERE JV.LegalBookId=1
		  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JV.IdJournalVoucher,JV.EntityId,JV.EntityCode,JV.EntityName,
		  MA.NUMBER,MA.ID,KOT.DocumentDate,JV.ID
		),

		CTE_KARDEX_PRESTAMOS
		AS
		(
			SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
			FROM Inventory .Kardex K WITH (NOLOCK)
			WHERE K.EntityName ='LoanMerchandise' 
			AND YEAR(K.DocumentDate) = @YearClosed 
			AND MONTH(K.DocumentDate) = @MonthClosed
		),

		CTE_CONTABILIDAD_PRESTAMOS
		AS
		(

		  SELECT 
			year(KOT.DocumentDate) [Year],
			month(KOT.DocumentDate) [Month],
			JV.EntityName,
			'PRESTAMOS DE MERCANCIAS' [Transaction],
			ma.Id MainAccountId,
			ma.Number MainAccountNumber,
			SUM(CAST(DebitValue AS NUMERIC(18,2))) DebitValue,
			SUM(CAST(CreditValue AS NUMERIC(18,2))) CreditValue, 
			'NINGUNO' WareHouseTypeName,
			'Confirmado' Status
		  FROM GeneralLedger.JournalVouchers JV WITH (NOLOCK)
		  INNER JOIN CTE_KARDEX_PRESTAMOS AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='LoanMerchandise'
		  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK) ON JVD.IdAccounting=JV.ID
		  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
		  WHERE JV.LegalBookId=1
		  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JV.IdJournalVoucher,JV.EntityId,JV.EntityCode,JV.EntityName,
		  MA.NUMBER,MA.ID,KOT.DocumentDate,JV.ID
		),

		CTE_KARDEX_DEVOLUCION_PRESTAMOS
		AS
		(
		SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,k.DocumentDate,'Confirmado' 'ESTADO'
		 FROM Inventory .Kardex K WITH (NOLOCK)
		 WHERE K.EntityName ='LoanMerchandiseDevolution' 
		 AND YEAR(K.DocumentDate) = @YearClosed 
			AND MONTH(K.DocumentDate) = @MonthClosed
		),

		CTE_CONTABILIDAD_DEVOLUCION_PRESTAMOS
		AS
		(

		  SELECT 
			  year(KOT.DocumentDate) [Year],
			  month(KOT.DocumentDate) [Month],
			  JV.EntityName,
			 'DEVOLUCION PRESTAMOS DE MERCANCIAS'  [Transaction],
			  ma.Id MainAccountId,
			  ma.Number MainAccountNumber,
			  SUM(CAST(DebitValue AS NUMERIC(18,2))) DebitValue,
			  SUM(CAST(CreditValue AS NUMERIC(18,2))) CreditValue, 
			  'NINGUNO' WareHouseTypeName,
			  'Confirmado' Status
		  FROM GeneralLedger.JournalVouchers JV WITH (NOLOCK)
		  INNER JOIN CTE_KARDEX_DEVOLUCION_PRESTAMOS AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='LoanMerchandiseDevolution'
		  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK) ON JVD.IdAccounting=JV.ID
		  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
		  WHERE JV.LegalBookId=1
		  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JV.IdJournalVoucher,JV.EntityId,JV.EntityCode,JV.EntityName,
		  MA.NUMBER,MA.ID,KOT.DocumentDate,JV.ID
		),


		CTE_KARDEX_ORDENES_TRASLADO
		AS
		(
		SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,
		CASE TFO.OrderType WHEN 1 THEN 'Traslado' WHEN 2 THEN 'Consumo' WHEN 3 THEN 'Traslado en Transito' END 'TIPO ORDEN',
		CASE TFO.Status WHEN 1 THEN 'Registrado' WHEN 2 THEN 'Entregado' WHEN 3 THEN 'Anulado' WHEN 4 THEN 'En Transito' END 'ESTADO'
		 FROM Inventory .Kardex K WITH (NOLOCK)
		 INNER JOIN Inventory .TransferOrder TFO WITH (NOLOCK) ON TFO.ID=K.EntityId AND TFO.CODE=K.EntityCode
		 WHERE K.EntityName ='TransferOrder' AND TFO.Status<> 3 AND TFO.OrderType<>3 
		 AND YEAR(K.DocumentDate) = @YearClosed 
			AND MONTH(K.DocumentDate) = @MonthClosed

		 UNION ALL

		 SELECT K.EntityId,K.EntityCode, K.EntityName,max(CAST(k.CreationDate AS DATE)) DocumentDate,'Traslado en Transito' AS 'TIPO ORDEN',
		CASE TFO.Status WHEN 1 THEN 'Registrado' WHEN 2 THEN 'Entregado' WHEN 3 THEN 'Anulado' WHEN 4 THEN 'En Transito' END 'ESTADO'
		 FROM Inventory .Kardex K WITH (NOLOCK)
		 INNER JOIN Inventory .TransferOrder TFO WITH (NOLOCK) ON TFO.ID=K.EntityId AND TFO.CODE=K.EntityCode
		 WHERE K.EntityName ='TransferOrder' AND TFO.Status<> 3 AND TFO.OrderType=3 and k.MovementType=1 AND YEAR(K.DocumentDate) = @YearClosed 
			AND MONTH(K.DocumentDate) = @MonthClosed
		 group by K.EntityId,K.EntityCode, K.EntityName,TFO.Status

		),

		CTE_CONTABILIDAD_ORDENES_TRASLADO
		AS
		(

		  SELECT 
			  year(KOT.DocumentDate) [Year],
			  month(KOT.DocumentDate) [Month],
			  JV.EntityName,
			 'ORDENES DE TRASLADO' [Transaction],
			  ma.Id MainAccountId,
			  ma.Number MainAccountNumber,
			  SUM(CAST(DebitValue AS NUMERIC(18,2))) DebitValue,
			  SUM(CAST(CreditValue AS NUMERIC(18,2))) CreditValue, 
			  'NINGUNO' WareHouseTypeName,
			  'Confirmado' Status
		  FROM GeneralLedger.JournalVouchers JV WITH (NOLOCK)
		  INNER JOIN CTE_KARDEX_ORDENES_TRASLADO AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='TransferOrder'
		  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK) ON JVD.IdAccounting=JV.ID
		  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
		  WHERE JV.LegalBookId=1
		  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JV.IdJournalVoucher,JV.EntityId,JV.EntityCode,JV.EntityName,
		  MA.NUMBER,MA.ID,KOT.DocumentDate,JV.ID
		),

		CTE_KARDEX_DEVOLUCION_ORDENES_TRASLADO
		AS
		(
		SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,CASE TFO.OrderType WHEN 1 THEN 'Traslado' WHEN 2 THEN 'Consumo' WHEN 3 THEN 'Traslado en Transito' END 'TIPO ORDEN',
		 CASE TFO.Status WHEN 1 THEN 'Registrado' WHEN 2 THEN 'Entregado' WHEN 3 THEN 'Anulado' WHEN 4 THEN 'En Transito' END 'ESTADO'
		 FROM Inventory .Kardex K WITH (NOLOCK)
		 INNER JOIN Inventory .TransferOrderDevolution AS TOD WITH (NOLOCK) ON TOD.ID=K.EntityId AND TOD.CODE=EntityCode
		INNER JOIN Inventory .TransferOrder TFO WITH (NOLOCK) ON TFO.ID=TOD.TransferOrderId 
		 WHERE K.EntityName ='TransferOrderDevolution'AND TOD.Status<>3 AND TFO.OrderType<>2 
		 AND YEAR(K.DocumentDate) = @YearClosed 
			AND MONTH(K.DocumentDate) = @MonthClosed
		),

		CTE_KARDEX_DEVOLUCION_ORDENES_TRASLADO_CONSUMO
		AS
		(
		SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,CASE TFO.OrderType WHEN 1 THEN 'Traslado' WHEN 2 THEN 'Consumo' WHEN 3 THEN 'Traslado en Transito' END 'TIPO ORDEN',
		 CASE TFO.Status WHEN 1 THEN 'Registrado' WHEN 2 THEN 'Entregado' WHEN 3 THEN 'Anulado' WHEN 4 THEN 'En Transito' END 'ESTADO',TFO.ID 'ID ORDEN TRASLADO',
		 TFO.CODE 'CODIGO ORDEN',K.MovementType,K.AffectInventory
		 FROM Inventory.Kardex K WITH (NOLOCK)
		 INNER JOIN Inventory .TransferOrderDevolution AS TOD WITH (NOLOCK) ON TOD.ID=K.EntityId AND TOD.CODE=EntityCode
		INNER JOIN Inventory .TransferOrder TFO WITH (NOLOCK) ON TFO.ID=TOD.TransferOrderId 
		 WHERE K.EntityName ='TransferOrderDevolution'AND TOD.Status<>3 AND TFO.OrderType=2 AND YEAR(K.DocumentDate) = @YearClosed 
			AND MONTH(K.DocumentDate) = @MonthClosed
		),

		CTE_CONTABILIDAD_DEVOLUCION_ORDENES_TRASLADO
		AS
		(
		  SELECT 
		  year(KOT.DocumentDate) [Year],
		  month(KOT.DocumentDate) [Month],
		  JV.EntityName,
		 'DEVOLUCION ORDENES DE TRASLADO' [Transaction],
		  ma.Id MainAccountId,
		  ma.Number MainAccountNumber,
		  SUM(CAST(DebitValue AS NUMERIC(18,2))) DebitValue,
		  SUM(CAST(CreditValue AS NUMERIC(18,2))) CreditValue, 
		  'NINGUNO' WareHouseTypeName,
		  'Confirmado' Status
		  FROM GeneralLedger.JournalVouchers JV WITH (NOLOCK)
		  INNER JOIN CTE_KARDEX_DEVOLUCION_ORDENES_TRASLADO AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='TransferOrderDevolution'
		  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK) ON JVD.IdAccounting=JV.ID
		  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
		  WHERE JV.LegalBookId=1
		  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JV.IdJournalVoucher,JV.EntityId,JV.EntityCode,JV.EntityName,
		  MA.NUMBER,MA.ID,KOT.DocumentDate,JV.ID
		),

		CTE_CONTABILIDAD_DEVOLUCION_ORDENES_TRASLADO_CONSUMO
		AS
		(

		  SELECT 
		  year(KOT.DocumentDate) [Year],
		  month(KOT.DocumentDate) [Month],
		  JV.EntityName,
		  'DEVOLUCION ORDENES DE TRASLADO' [Transaction],
		  ma.Id MainAccountId,
		  ma.Number MainAccountNumber,
		  SUM(CAST(DebitValue AS NUMERIC(18,2))) DebitValue,
		  SUM(CAST(CreditValue AS NUMERIC(18,2))) CreditValue, 
		  'NINGUNO' WareHouseTypeName,
		  'Confirmado' Status
		  FROM GeneralLedger.JournalVouchers JV WITH (NOLOCK)
		  INNER JOIN CTE_KARDEX_DEVOLUCION_ORDENES_TRASLADO_CONSUMO AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='TransferOrderDevolution'
		  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK) ON JVD.IdAccounting=JV.ID
		  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
		  WHERE JV.LegalBookId=1
		  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JV.IdJournalVoucher,JV.EntityId,JV.EntityCode,JV.EntityName,
		  MA.NUMBER,MA.ID,KOT.DocumentDate,JV.ID
		),

		CTE_KARDEX_COMPROBANTES_ENTRADA
		AS
		(
		 SELECT DISTINCT K.EntityId,K.EntityCode,CAST(k.DocumentDate AS DATE) DocumentDate, K.EntityName ,AP.Id ID_CXP,AP.Code [CODIGO CXP],
		 'SI' 'VALOR',EV.ValueTax 'VALOR IVA','Confirmado' 'ESTADO'
		 FROM Inventory .Kardex K WITH (NOLOCK)
		 INNER JOIN Payments.AccountPayable AS AP WITH (NOLOCK) ON K.EntityId=AP.EntityId AND K.EntityCode=AP.EntityCode
		 INNER JOIN Inventory.EntranceVoucher EV WITH (NOLOCK) ON EV.ID=K.EntityId AND EV.CODE=K.EntityCode
		 INNER JOIN Inventory.EntranceVoucherDetail AS EVD WITH (NOLOCK) ON EV.ID=EVD.EntranceVoucherId
		 WHERE K.EntityName ='EntranceVoucher' AND EV.Status<> 3 AND YEAR(K.DocumentDate) = @YearClosed 
			AND MONTH(K.DocumentDate) = @MonthClosed
		),


		CTE_CONTABILIDAD_COMPROBANTES_ENTRADA
		AS
		(
		  SELECT 
		  year(KOT.DocumentDate) [Year],
		  month(KOT.DocumentDate) [Month],
		  JV.EntityName,
		  'COMPROBANTES DE ENTRADA' [Transaction],
		  ma.Id MainAccountId,
		  ma.Number MainAccountNumber,
		  SUM(CAST(DebitValue AS NUMERIC(18,2))) DebitValue,
		  SUM(CAST(CreditValue AS NUMERIC(18,2))) CreditValue, 
		  'NINGUNO' WareHouseTypeName,
		  'Confirmado' Status
		   FROM GeneralLedger.JournalVouchers JV WITH (NOLOCK)
		  INNER JOIN CTE_KARDEX_COMPROBANTES_ENTRADA AS KOT ON KOT.ID_CXP=JV.EntityId AND KOT.[CODIGO CXP]=JV.EntityCode AND JV.EntityName='AccountPayable'
		  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK) ON JVD.IdAccounting=JV.ID
		  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
		  WHERE JV.LegalBookId=1
		  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JV.IdJournalVoucher,JV.EntityId,JV.EntityCode,JV.EntityName,
		  MA.NUMBER,MA.ID,KOT.EntityCode,KOT.[CODIGO CXP],KOT.DocumentDate,JV.ID
		),

		CTE_CONTABILIDAD_COMPROBANTES_RECLASIFICACION_REMISIONES
		AS
		(

		  SELECT 
		  year(KOT.DocumentDate) [Year],
		  month(KOT.DocumentDate) [Month],
		  JV.EntityName,
		  'RECLASIFICACION REMISIONES' [Transaction],
		  ma.Id MainAccountId,
		  ma.Number MainAccountNumber,
		  SUM(CAST(DebitValue AS NUMERIC(18,2))) DebitValue,
		  SUM(CAST(CreditValue AS NUMERIC(18,2))) CreditValue, 
		  'NINGUNO' WareHouseTypeName,
		  'Confirmado' Status
		  FROM GeneralLedger.JournalVouchers JV WITH (NOLOCK)
		  INNER JOIN CTE_KARDEX_COMPROBANTES_ENTRADA AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='RemissionReclassification'
		  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK) ON JVD.IdAccounting=JV.ID
		  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
		  WHERE JV.LegalBookId=1
		  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JV.IdJournalVoucher,JV.EntityId,JV.EntityCode,JV.EntityName,
		  MA.NUMBER,MA.ID,KOT.EntityCode,KOT.[CODIGO CXP],KOT.DocumentDate,JV.ID
		),


		CTE_KARDEX_DEVOLUCION_COMPROBANTES_ENTRADA
		AS
		(
		SELECT DISTINCT K.EntityId,K.EntityCode,CAST(k.DocumentDate AS DATE) DocumentDate, K.EntityName,PN.ID ID_NOTA,PN.CODE 'CODIGO NOTA',EVD.ID ID_DEVO,EVD.CODE 'CODIGO DEV', EVD.ValueTax 'VALOR IVA',
		CASE EVD.Status WHEN 1 THEN 'Registrado' WHEN 2 THEN 'Confirmado' WHEN 3 THEN 'Anulado' END 'ESTADO'
		 FROM Inventory .Kardex K WITH (NOLOCK)
		 INNER JOIN Inventory.EntranceVoucherDevolution EVD WITH (NOLOCK) ON EVD.ID=K.EntityId AND EVD.CODE=EntityCode
		 INNER JOIN Payments.PaymentNotes as PN WITH (NOLOCK) ON PN.Entitycode=EVD.Code and PN.EntityId= EVD.Id
		 WHERE K.EntityName ='EntranceVoucherDevolution'AND EVD.Status<>3 AND YEAR(K.DocumentDate) = @YearClosed 
			AND MONTH(K.DocumentDate) = @MonthClosed
		),

		CTE_CONTABILIDAD_DEVOLUCION_COMPROBANTES_ENTRADA
		AS
		(

		  SELECT 
			year(KOT.DocumentDate) [Year],
			month(KOT.DocumentDate) [Month],
			JV.EntityName,
			'DEVOLUCION DE COMPRAS' [Transaction],
			ma.Id MainAccountId,
			ma.Number MainAccountNumber,
			SUM(CAST(DebitValue AS NUMERIC(18,2))) DebitValue,
			SUM(CAST(CreditValue AS NUMERIC(18,2))) CreditValue, 
			'NINGUNO' WareHouseTypeName,
			'Confirmado' Status
		  FROM GeneralLedger.JournalVouchers JV WITH (NOLOCK)
		  INNER JOIN CTE_KARDEX_DEVOLUCION_COMPROBANTES_ENTRADA AS KOT ON KOT.ID_NOTA=JV.EntityId AND KOT.[CODIGO NOTA]=JV.EntityCode AND JV.EntityName='PaymentNotes'
		  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK) ON JVD.IdAccounting=JV.ID
		  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
		  WHERE JV.LegalBookId=1
		  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JV.IdJournalVoucher,JV.EntityId,JV.EntityCode,JV.EntityName,
		  MA.NUMBER,MA.ID,KOT.EntityCode,KOT.[CODIGO NOTA],KOT.DocumentDate,JV.ID
		),

CTE_KARDEX_REMISIONES_ENTRADA
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
 FROM Inventory .Kardex K WITH (NOLOCK)
 WHERE K.EntityName ='RemissionEntrance' AND YEAR(K.DocumentDate) = @YearClosed 
			AND MONTH(K.DocumentDate) = @MonthClosed
),


CTE_CONTABILIDAD_REMISIONES_ENTRADA
AS
(

  SELECT 
	year(KOT.DocumentDate) [Year],
	month(KOT.DocumentDate) [Month],
	JV.EntityName,
	'REMISION DE ENTRADA' [Transaction],
	ma.Id MainAccountId,
	ma.Number MainAccountNumber,
	SUM(CAST(DebitValue AS NUMERIC(18,2))) DebitValue,
	SUM(CAST(CreditValue AS NUMERIC(18,2))) CreditValue, 
	'NINGUNO' WareHouseTypeName,
	'Confirmado' Status
   FROM GeneralLedger.JournalVouchers JV WITH (NOLOCK)
  INNER JOIN CTE_KARDEX_REMISIONES_ENTRADA AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='RemissionEntrance'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK)  ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JV.IdJournalVoucher,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.DocumentDate,JV.ID
),

CTE_KARDEX_DEVOLUCION_REMISION_ENTRADA
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
 FROM Inventory .Kardex K WITH (NOLOCK)
 INNER JOIN Inventory.RemissionDevolution AS RV WITH (NOLOCK) ON RV.Id=k.EntityId and RV.Code=K.EntityCode
 WHERE K.EntityName ='RemissionDevolution' AND RV.DevolutionType=1 AND YEAR(K.DocumentDate) = @YearClosed 
			AND MONTH(K.DocumentDate) = @MonthClosed
),

CTE_CONTABILIDAD_DEVOLUCION_REMISIONES_ENTRADA
AS
(

  SELECT
	year(KOT.DocumentDate) [Year],
	month(KOT.DocumentDate) [Month],
	JV.EntityName,
	'DEVOLUCION REMISION DE ENTRADA' [Transaction],
	ma.Id MainAccountId,
	ma.Number MainAccountNumber,
	SUM(CAST(DebitValue AS NUMERIC(18,2))) DebitValue,
	SUM(CAST(CreditValue AS NUMERIC(18,2))) CreditValue, 
	'NINGUNO' WareHouseTypeName,
	'Confirmado' Status
  FROM GeneralLedger.JournalVouchers JV WITH (NOLOCK)
  INNER JOIN CTE_KARDEX_DEVOLUCION_REMISION_ENTRADA AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='RemissionEntranceDevolution'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK) ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JV.IdJournalVoucher,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.DocumentDate,JV.ID
),

CTE_KARDEX_REMISIONES_CONSIGNACION
AS
(
SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
 FROM Inventory .Kardex K WITH (NOLOCK)
 WHERE K.EntityName ='ConsignmentInventoryRemission' AND YEAR(K.DocumentDate) = @YearClosed 
			AND MONTH(K.DocumentDate) = @MonthClosed
),

CTE_CONTABILIDAD_REMISIONES_CONSIGNACION
AS
(
  SELECT
	year(KOT.DocumentDate) [Year],
	month(KOT.DocumentDate) [Month],
	JV.EntityName,
	'REMISION DE INVENTARIO CONSIGNACION' [Transaction],
	ma.Id MainAccountId,
	ma.Number MainAccountNumber,
	SUM(CAST(DebitValue AS NUMERIC(18,2))) DebitValue,
	SUM(CAST(CreditValue AS NUMERIC(18,2))) CreditValue, 
	'NINGUNO' WareHouseTypeName,
	'Confirmado' Status
  FROM GeneralLedger.JournalVouchers JV WITH (NOLOCK)
  INNER JOIN CTE_KARDEX_REMISIONES_CONSIGNACION AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='ConsignmentInventoryRemission'
  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK) ON JVD.IdAccounting=JV.ID
  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
  WHERE JV.LegalBookId=1
  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JV.IdJournalVoucher,JV.EntityId,JV.EntityCode,JV.EntityName,
  MA.NUMBER,MA.ID,KOT.DocumentDate,JV.ID
),

	CTE_KARDEX_DEVOLUCION_REMISION_CONSIGNACION
	AS
	(
	SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
	 FROM Inventory .Kardex K WITH (NOLOCK)
	 INNER JOIN Inventory.RemissionDevolution AS RV WITH (NOLOCK) ON RV.Id=k.EntityId and RV.Code=K.EntityCode
	 WHERE K.EntityName ='RemissionDevolution' AND RV.DevolutionType=3 AND YEAR(K.DocumentDate) = @YearClosed 
				AND MONTH(K.DocumentDate) = @MonthClosed
	),

	CTE_CONTABILIDAD_DEVOLUCION_REMISIONES_CONSIGNACION
	AS
	(

	  SELECT 
		year(KOT.DocumentDate) [Year],
		month(KOT.DocumentDate) [Month],
		JV.EntityName,
		'DEVOLUCION REMISION DE ENTRADA' [Transaction],
		ma.Id MainAccountId,
		ma.Number MainAccountNumber,
		SUM(CAST(DebitValue AS NUMERIC(18,2))) DebitValue,
		SUM(CAST(CreditValue AS NUMERIC(18,2))) CreditValue, 
		'NINGUNO' WareHouseTypeName,
		'Confirmado' Status
	  FROM GeneralLedger.JournalVouchers JV WITH (NOLOCK)
	  INNER JOIN CTE_KARDEX_DEVOLUCION_REMISION_CONSIGNACION AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='ConsignmentInventoryRemissionDevolution'
	  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK) ON JVD.IdAccounting=JV.ID
	  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
	  WHERE JV.LegalBookId=1
	  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JV.IdJournalVoucher,JV.EntityId,JV.EntityCode,JV.EntityName,
	  MA.NUMBER,MA.ID,KOT.DocumentDate,JV.ID
	),

	CTE_KARDEX_FACTURA_PRODUCTOS
	AS
	(
		SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
		FROM Inventory .Kardex K WITH (NOLOCK)
		WHERE K.EntityName ='BasicBilling'
			AND YEAR(K.DocumentDate) = @YearClosed 
			AND MONTH(K.DocumentDate) = @MonthClosed
	),

	CTE_CONTABILIDAD_FACTURA_PRODUCTOS
	AS
	(

	  SELECT 
			year(KOT.DocumentDate) [Year],
			month(KOT.DocumentDate) [Month],
			JV.EntityName,
			'FACTURA BASICA PRODUCTOS' [Transaction],
			ma.Id MainAccountId,
			ma.Number MainAccountNumber,
			SUM(CAST(DebitValue AS NUMERIC(18,2))) DebitValue,
			SUM(CAST(CreditValue AS NUMERIC(18,2))) CreditValue, 
			'NINGUNO' WareHouseTypeName,
			'Confirmado' Status
	  FROM GeneralLedger.JournalVouchers JV WITH (NOLOCK)
	  INNER JOIN CTE_KARDEX_FACTURA_PRODUCTOS AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='BasicBilling'
	  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK) ON JVD.IdAccounting=JV.ID
	  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
	  WHERE JV.LegalBookId=1
	  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JV.IdJournalVoucher,JV.EntityId,JV.EntityCode,JV.EntityName,
	  MA.NUMBER,MA.ID,KOT.DocumentDate,JV.ID
	),

	CTE_KARDEX_DEVOLUCION_FACTURA_PRODUCTOS
	AS
	(
	 SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
	 FROM Inventory .Kardex K WITH (NOLOCK)
	 WHERE K.EntityName ='BasicBillingDevolution' 
		AND YEAR(K.DocumentDate) = @YearClosed 
		AND MONTH(K.DocumentDate) = @MonthClosed
	),

	CTE_CONTABILIDAD_DEVOLUCION_FACTURA_PRODUCTOS
	AS
	(

	  SELECT 
			year(KOT.DocumentDate) [Year],
			month(KOT.DocumentDate) [Month],
			JV.EntityName,
			'DEVOLUCION FACTURA BASICA PRODUCTOS' [Transaction],
			ma.Id MainAccountId,
			ma.Number MainAccountNumber,
			SUM(CAST(DebitValue AS NUMERIC(18,2))) DebitValue,
			SUM(CAST(CreditValue AS NUMERIC(18,2))) CreditValue, 
			'NINGUNO' WareHouseTypeName,
			'Confirmado' Status
	  FROM GeneralLedger.JournalVouchers JV
	  INNER JOIN CTE_KARDEX_DEVOLUCION_FACTURA_PRODUCTOS AS KOT WITH (NOLOCK) ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='BasicBilling'
	  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK) ON JVD.IdAccounting=JV.ID
	  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
	  WHERE JV.LegalBookId=1
	  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JV.IdJournalVoucher,JV.EntityId,JV.EntityCode,JV.EntityName,
	  MA.NUMBER,MA.ID,KOT.DocumentDate,JV.ID
	),		

	CTE_KARDEX_REMISIONES_SALIDA
	AS
	(
	 SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
	 FROM Inventory .Kardex K WITH (NOLOCK)
	 WHERE K.EntityName ='RemissionOutput' 
		AND YEAR(K.DocumentDate) = @YearClosed 
		AND MONTH(K.DocumentDate) = @MonthClosed
	),

	CTE_CONTABILIDAD_REMISIONES_SALIDA
	AS
	(

	  SELECT
			year(KOT.DocumentDate) [Year],
			month(KOT.DocumentDate) [Month],
			JV.EntityName,
			'REMISION DE SALIDA' [Transaction],
			ma.Id MainAccountId,
			ma.Number MainAccountNumber,
			SUM(CAST(DebitValue AS NUMERIC(18,2))) DebitValue,
			SUM(CAST(CreditValue AS NUMERIC(18,2))) CreditValue, 
			'NINGUNO' WareHouseTypeName,
			'Confirmado' Status
	  FROM GeneralLedger.JournalVouchers JV WITH (NOLOCK)
	  INNER JOIN CTE_KARDEX_REMISIONES_SALIDA AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='RemissionOutput'
	  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK) ON JVD.IdAccounting=JV.ID
	  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
	  WHERE JV.LegalBookId=1
	  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JV.IdJournalVoucher,JV.EntityId,JV.EntityCode,JV.EntityName,
	  MA.NUMBER,MA.ID,KOT.DocumentDate,JV.ID
	),

	CTE_KARDEX_DEVOLUCION_REMISION_SALIDA
	AS
	(
	 SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
	 FROM Inventory .Kardex K WITH (NOLOCK)
	 INNER JOIN Inventory.RemissionDevolution AS RV WITH (NOLOCK) ON RV.Id=k.EntityId and RV.Code=K.EntityCode
	 WHERE K.EntityName ='RemissionDevolution' AND RV.DevolutionType=2 
		AND YEAR(K.DocumentDate) = @YearClosed 
		AND MONTH(K.DocumentDate) = @MonthClosed
	),

	CTE_CONTABILIDAD_DEVOLUCION_REMISIONES_SALIDA
	AS
	(

	  SELECT 
			year(KOT.DocumentDate) [Year],
			month(KOT.DocumentDate) [Month],
			JV.EntityName,
			'DEVOLUCION REMISION DE ENTRADA' [Transaction],
			ma.Id MainAccountId,
			ma.Number MainAccountNumber,
			SUM(CAST(DebitValue AS NUMERIC(18,2))) DebitValue,
			SUM(CAST(CreditValue AS NUMERIC(18,2))) CreditValue, 
			'NINGUNO' WareHouseTypeName,
			'Confirmado' Status
	  FROM GeneralLedger.JournalVouchers JV WITH (NOLOCK)
	  INNER JOIN CTE_KARDEX_DEVOLUCION_REMISION_SALIDA AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='RemissionOutputDevolution'
	  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK) ON JVD.IdAccounting=JV.ID
	  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
	  WHERE JV.LegalBookId=1
	  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JV.IdJournalVoucher,JV.EntityId,JV.EntityCode,JV.EntityName,
	  MA.NUMBER,MA.ID,KOT.DocumentDate,JV.ID
	),

	CTE_KARDEX_DOCUMENTO_FACTURA_PRODUCTOS
	AS
	(
		SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
		FROM Inventory .Kardex K WITH (NOLOCK)
		WHERE K.EntityName ='DocumentInvoiceProductSales' 
		AND YEAR(K.DocumentDate) = @YearClosed 
		AND MONTH(K.DocumentDate) = @MonthClosed
	),

	CTE_CONTABILIDAD_DOCUMENTO_FACTURA_PRODUCTOS
	AS
	(
	  SELECT 
			year(KOT.DocumentDate) [Year],
			month(KOT.DocumentDate) [Month],
			JV.EntityName,
			'FACTURA BASICA PRODUCTOS' [Transaction],
			ma.Id MainAccountId,
			ma.Number MainAccountNumber,
			SUM(CAST(DebitValue AS NUMERIC(18,2))) DebitValue,
			SUM(CAST(CreditValue AS NUMERIC(18,2))) CreditValue, 
			'NINGUNO' WareHouseTypeName,
			'Confirmado' Status
	  FROM GeneralLedger.JournalVouchers JV WITH (NOLOCK)
	  INNER JOIN CTE_KARDEX_DOCUMENTO_FACTURA_PRODUCTOS AS KOT ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode AND JV.EntityName='DocumentInvoiceProductSales'
	  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK) ON JVD.IdAccounting=JV.ID
	  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
	  WHERE JV.LegalBookId=1
	  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JV.IdJournalVoucher,JV.EntityId,JV.EntityCode,JV.EntityName,
	  MA.NUMBER,MA.ID,KOT.DocumentDate,JV.ID
	),

	CTE_KARDEX_DEVOLUCION_DOCUMENTO_FACTURA_PRODUCTOS
	AS
	(
	 SELECT DISTINCT K.EntityId,K.EntityCode, K.EntityName,CAST(k.DocumentDate AS DATE) DocumentDate,'Confirmado' 'ESTADO'
	 FROM Inventory .Kardex K WITH (NOLOCK)
	 WHERE K.EntityName ='DocumentInvoiceProductSalesDevolution' 
		AND YEAR(K.DocumentDate) = @YearClosed 
		AND MONTH(K.DocumentDate) = @MonthClosed
	),

	CTE_CONTABILIDAD_DEVOLUCION_DOCUMENTO_FACTURA_PRODUCTOS
	AS
	(
	  SELECT 
			year(KOT.DocumentDate) [Year],
			month(KOT.DocumentDate) [Month],
			JV.EntityName,
			'DEVOLUCION FACTURA BASICA PRODUCTOS' [Transaction],
			ma.Id MainAccountId,
			ma.Number MainAccountNumber,
			SUM(CAST(DebitValue AS NUMERIC(18,2))) DebitValue,
			SUM(CAST(CreditValue AS NUMERIC(18,2))) CreditValue, 
			'NINGUNO' WareHouseTypeName,
			'Confirmado' Status
	  FROM GeneralLedger.JournalVouchers JV
	  INNER JOIN CTE_KARDEX_DEVOLUCION_DOCUMENTO_FACTURA_PRODUCTOS AS KOT WITH (NOLOCK) ON KOT.EntityId=JV.EntityId AND KOT.EntityCode=JV.EntityCode 
	  AND JV.EntityName='DocumentInvoiceProductSalesDevolution'
	  INNER JOIN GeneralLedger.JournalVoucherDetails JVD WITH (NOLOCK) ON JVD.IdAccounting=JV.ID
	  INNER JOIN GeneralLedger.MainAccounts AS MA WITH (NOLOCK) ON MA.ID=JVD.IdMainAccount
	  WHERE JV.LegalBookId=1
	  GROUP BY year(KOT.DocumentDate),month(KOT.DocumentDate),DAY(KOT.DocumentDate),JV.CONSECUTIVE,JV.IdJournalVoucher,JV.EntityId,JV.EntityCode,JV.EntityName,
	  MA.NUMBER,MA.ID,KOT.DocumentDate,JV.ID
	),


CTE_DATOS_MOSTRAR
AS
(
------------------------------******************DISPENSACIONES Y DEVOLUCION DE DISPENSACIONES ***********************------------------------------


SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Dispensación Farmacéutica' OrderType
FROM CTE_CONTABILIDAD_DISPENSACIONES
UNION ALL

SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Devolución Dispensación' OrderType
FROM CTE_CONTABILIDAD_DEVOLUCION_DISPENSACIONES

UNION ALL
------------------------------************************ AJUSTES DE INVENTARIOS **********************************************************------------------------------

SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Ajuste' OrderType
FROM CTE_CONTABILIDAD_AJUSTES

UNION ALL
------------------------------*********************************** PRESTAMOS *********************************************------------------------------

SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Prestamos' OrderType
FROM CTE_CONTABILIDAD_PRESTAMOS
UNION ALL

SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Devolución de Prestamos' OrderType
FROM CTE_CONTABILIDAD_DEVOLUCION_PRESTAMOS

UNION ALL
------------------------------************************** ORDENES DE TRASALDOS *****************************************************------------------------------

SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Ordenes de Traslado' OrderType
FROM CTE_CONTABILIDAD_ORDENES_TRASLADO

UNION ALL

SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Ordenes de Traslado' OrderType
FROM CTE_CONTABILIDAD_DEVOLUCION_ORDENES_TRASLADO

UNION ALL

SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Ordenes de Traslado' OrderType
FROM CTE_CONTABILIDAD_DEVOLUCION_ORDENES_TRASLADO_CONSUMO

UNION ALL
------------------------------************************** COMPROBANTES DE ENTRADA *****************************************************------------------------------

SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Comprobante Entrada' OrderType
FROM CTE_CONTABILIDAD_COMPROBANTES_ENTRADA

UNION ALL

SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Reclasificación remisión' OrderType
FROM CTE_CONTABILIDAD_COMPROBANTES_RECLASIFICACION_REMISIONES
UNION ALL

SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Comprobante Entrada' OrderType
FROM CTE_CONTABILIDAD_DEVOLUCION_COMPROBANTES_ENTRADA
------------------------------***************************************************************************************************------------------------------
UNION ALL
------------------------------**************************** REMISION DE ENTRADA ******************************************------------------------------

SELECT
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Remisión de Entrada' OrderType
FROM CTE_CONTABILIDAD_REMISIONES_ENTRADA

UNION ALL

SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Devolución Remisión de Entrada' OrderType
FROM CTE_CONTABILIDAD_DEVOLUCION_REMISIONES_ENTRADA
------------------------------***************************************************************************************************------------------------------
UNION ALL
------------------------------**************************** REMISION DE INVENTARIOS CONSIGNACION ***********************************------------------------------

SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Remisión Inventario Consignación' OrderType
FROM CTE_CONTABILIDAD_REMISIONES_CONSIGNACION
UNION ALL

SELECT
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Devolución Remisión Inventario Consignación' OrderType
FROM CTE_CONTABILIDAD_DEVOLUCION_REMISIONES_CONSIGNACION


UNION ALL

SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Factura Productos' OrderType
FROM CTE_CONTABILIDAD_FACTURA_PRODUCTOS

UNION ALL

SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Devolución Factura Productos' OrderType
FROM CTE_CONTABILIDAD_DEVOLUCION_FACTURA_PRODUCTOS

UNION ALL

SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Remisión de Salida' OrderType
FROM CTE_CONTABILIDAD_REMISIONES_SALIDA

UNION ALL

SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Devolución Remisión de Salida' OrderType
FROM CTE_CONTABILIDAD_DEVOLUCION_REMISIONES_SALIDA

UNION ALL

SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Factura Documento Productos' OrderType
FROM CTE_CONTABILIDAD_DOCUMENTO_FACTURA_PRODUCTOS

UNION ALL

SELECT 
	[Year],
	[Month],
	EntityName,
	[Transaction],
	MainAccountId,
	MainAccountNumber,
	DebitValue,
	CreditValue,
	WareHouseTypeName,
	Status,
	'Devolución Factura Documento Productos' OrderType
FROM CTE_CONTABILIDAD_DEVOLUCION_DOCUMENTO_FACTURA_PRODUCTOS

)
,


SourceData AS (

    SELECT 
		Year,
		Month,
		EntityName,
		[Transaction],
		MainAccountId,
		MainAccountNumber,
		SUM(DebitValue) TotalDebit,
		SUM(CreditValue) TotalCredit,
		'Accounting' Module
    FROM CTE_DATOS_MOSTRAR cte
	JOIN @AllowMainAccount ama ON cte.MainAccountId = ama.IdMainAccount
    WHERE 
        WareHouseTypeName = 'NINGUNO' 
        AND Status <> 'En Transito' 
        AND OrderType NOT IN (
            'Remisión en Consignación', 
            'Remisión Inventario Consignación', 
            'Devolución Remisión Inventario Consignación'
        )
    GROUP BY 
        Year,
        Month,
        EntityName,
        [Transaction],
        MainAccountId,
        MainAccountNumber
)
	select * from SourceData
END