CREATE VIEW [Portfolio].[ViewReportInvoiceTraceability]
AS
SELECT	CONCAT(ar.Id, '-', vrear.VoucherName, '-', vrear.MovesCode, '-', vrear.AccountNumber) Id,
		ar.OperatingUnitId,
		ar.Id AS AccountReceivableId,
		ar.InvoiceNumber,
		ar.AccountReceivableDate AS InvoiceDate,
		ar.PortfolioStatus,
		CASE ar.PortfolioStatus
			WHEN 1 THEN 'Sin Radicar'
			WHEN 2 THEN 'Radicada'
			WHEN 3 THEN 'Radicada Entidad'
			WHEN 4 THEN 'Objetada'
			WHEN 5 THEN 'Contestada Radicada'
			WHEN 6 THEN 'Aceptada'
			WHEN 7 THEN 'Certificada Parcial'
			WHEN 8 THEN 'Certificada Total'
			WHEN 9 THEN 'No Subsanable'
			WHEN 10 THEN 'Dificil Recaudo'
			WHEN 11 THEN 'Factura Devuelta'
			WHEN 12 THEN 'Glosa Ratificada'
			WHEN 13 THEN 'Radicacion Tramite Objecion'
			WHEN 14 THEN 'Devolucion Factura'
			WHEN 15 THEN 'Cuenta Dificil Recaudo'
			WHEN 16 THEN 'Cobro Juridico'
		END AS PortfolioStatusName,
		c.Id AS CustomerId,
		c.Nit CustomerNit,
		c.Name AS CustomerName,
		CASE c.State
			WHEN 0 THEN 'Inactivo'
			WHEN 1 THEN 'Activo'
		END AS CustomerStatusName,
		c.EPSCode AS CustomerEPSCode,
		gor.RadicatedConsecutive AS ObjectionsReceptionConsecutive,
		gor.RadicatedDate AS ObjectionsReceptionRadicatedDate,
		gor.DocumentDate AS ObjectionsReceptionDocumentDate,
		gor.ConfirmDate AS ObjectionsReceptionConfirmDate,
		gor.Comment AS ObjectionsReceptionComment,
		gor.CreationUser AS ObjectionsReceptionUserCreation,
		CASE gor.State
			WHEN 1 THEN 'Sin Confirmar'
			WHEN 2 THEN 'Radicado Confirmado'
			WHEN 3 THEN 'Oficio con Respuesta'
			WHEN 4 THEN 'Anulada'
		END AS ObjectionsReceptionStatusName,
		gc.ConciliationConsecutive AS ConciliationConsecutive,
		gc.ConciliationDate AS ConciliationDate,
		gc.DocumentDate AS ConciliationDocumentDate,
		gc.ConfirmDate AS ConciliationConfirmDate,
		CASE
			WHEN gc.State = 1 THEN 'Sin Confirmar'
			WHEN gc.State = 2 THEN 'Confirmado'
		END AS ConciliationStatusName,
		ar.Value EntityValue,
		ISNULL(arp.Value, 0) PatientValue,
		ar.Value + ISNULL(arp.Value, 0) InvoiceValue,
		ISNULL(gpg.ValueGlosado, 0) AS ValueGlosado,
		ISNULL(gpg.ValueAcceptedFirstInstance, 0) AS ValueAcceptedFirstInstance,
		ISNULL(gpg.ValueReiterated, 0) AS ValueReiterated,
		ISNULL(gpg.ValueAcceptedSecondInstance, 0) AS ValueAcceptedSecondInstance,
		ISNULL(gpg.ValueAcceptedIPSconciliation, 0) AS ValueAcceptedIPSconciliation,
		ISNULL(gpg.ValueAcceptedEAPBconciliation, 0) AS ValueAcceptedEAPBconciliation,
		ISNULL(gpg.BalanceGlosa, 0) AS BalanceGlosa,
		ISNULL(gpg.ValuePayments, 0) AS ValuePayments,
		ISNULL(gtjcd.LegalTransferValue, 0) AS LegalTransferValue,
		ar.Balance,
		gpg.Id AS PortfolioGlosadaId,
		gpg.RadicatedNumber AS PortfolioGlosadaRadicatedNumber,
		gpg.RadicatedDate AS PortfolioGlosadaRadicateDate,
		gpg.AccountantAccountCustomers AS PortfolioGlosadaAccountCustomer,
		gpg.EvaluationDateGlosa AS PortfolioGlosadaDate,
		vrear.VoucherName,
		vrear.MovesCode,
		ISNULL(vrear.BillValueInitial, 0) BillValueInitial,
		ISNULL(vrear.MovesDebit, 0) MovesDebit,
		ISNULL(vrear.MovesCredit, 0) MovesCredit,
		ISNULL(vrear.BillCurrentBalance, 0) BillCurrentBalance
FROM Portfolio.AccountReceivable AS ar
LEFT JOIN Common.Customer AS c ON ar.CustomerId = c.Id
LEFT JOIN
(
	SELECT ar.InvoiceNumber, SUM(ar.Value) Value
	FROM Portfolio.AccountReceivable ar
	WHERE ar.AccountReceivableType IN (4, 6)
	GROUP BY ar.InvoiceNumber
) arp ON ar.AccountReceivableType = 2 AND ar.InvoiceNumber = arp.InvoiceNumber
LEFT JOIN Glosas.GlosaPortfolioGlosada AS gpg ON ar.InvoiceNumber = gpg.InvoiceNumber
LEFT JOIN 
(
	SELECT
		D.DocumentType
	   ,D.InvoiceNumber
	   ,D.GlosaObjectionsReceptionCId
	   ,D.PortfolioGlosaId
	FROM Glosas.GlosaObjectionsReceptionD AS D
	INNER JOIN 
	(
		SELECT
			MAX(GlosaObjectionsReceptionCId) AS GlosaObjectionsReceptionCId
		   ,PortfolioGlosaId
		   ,InvoiceNumber
		FROM Glosas.GlosaObjectionsReceptionD
		GROUP BY PortfolioGlosaId,InvoiceNumber
	) AS G
		ON G.GlosaObjectionsReceptionCId = D.GlosaObjectionsReceptionCId
		AND D.InvoiceNumber = G.InvoiceNumber
		AND D.PortfolioGlosaId = G.PortfolioGlosaId
) AS gord ON gord.PortfolioGlosaId = gpg.Id
LEFT JOIN Glosas.GlosaObjectionsReceptionC AS gor ON gord.GlosaObjectionsReceptionCId = gor.Id
LEFT OUTER JOIN 
(
	SELECT
		MAX(ConciliationCId) AS ConciliationCId
	   ,GlosaPortfolioId
	   ,InvoiceNumber
	FROM Glosas.ConciliationD
	GROUP BY GlosaPortfolioId,InvoiceNumber
) AS gcd ON gpg.InvoiceNumber = gcd.InvoiceNumber
LEFT JOIN Glosas.ConciliationC AS gc ON gcd.ConciliationCId = gc.Id
LEFT JOIN 
(
	SELECT InvoiceNumber, SUM(tjdcd.LegalTransferValue) LegalTransferValue
	FROM Glosas.TransferJuridicalDebtCollectionC tjdcc
	JOIN Glosas.TransferJuridicalDebtCollectionD tjdcd ON tjdcc.Id = tjdcd.TransferJuridicalDebtCollectionCId
	WHERE tjdcc.State = '2'
	GROUP BY InvoiceNumber
) AS gtjcd ON gtjcd.InvoiceNumber = gpg.InvoiceNumber
LEFT JOIN 
(
	SELECT AccountReceivableId, VoucherName, MovesCode, AccountNumber, MIN(BillValueInitial) BillValueInitial, MAX(BillCurrentBalance) BillCurrentBalance, Status, SUM(MovesDebit) MovesDebit, SUM(MovesCredit) MovesCredit
	FROM Portfolio.VReportExtractAccountReceivable 
	GROUP BY AccountReceivableId, VoucherName, MovesCode, AccountNumber, Status
) AS vrear ON ar.Id = vrear.AccountReceivableId AND vrear.Status IN (2, 4)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de trazabilidad contable y financiera de facturas de cartera. Consolida, por cada cuenta por cobrar (factura o documento de cobro emitido a una EPS, aseguradora o cliente), el estado de cartera (sin radicar, radicada, objetada, en cobro jurídico, etc.), los datos del cliente o entidad pagadora (NIT, nombre, código EPS), el historial de glosas (valores glosados, aceptados en primera y segunda instancia, conciliados por IPS y EAPB, saldo de glosa y pagos), el último proceso de objeción recibido (consecutivo, fechas de radicación y confirmación, estado y observaciones), la última conciliación de glosa (consecutivo, fechas, estado), el valor trasladado a cobro jurídico, y los movimientos contables asociados al comprobante (débitos, créditos, saldo inicial y saldo actual). Sirve como fuente principal para reportes de seguimiento y auditoría del ciclo de facturación, glosa y recaudo, permitiendo rastrear cada factura desde su emisión hasta su estado contable y financiero vigente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewReportInvoiceTraceability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewReportInvoiceTraceability';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reportería que consolida la trazabilidad financiera y de gestión de glosas de cada factura de cartera, integrando recepción de objeciones, conciliaciones, traslados a cobro jurídico y movimientos contables.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewReportInvoiceTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen cuentas por cobrar en Portfolio.AccountReceivable como base del reporte.; Las facturas se identifican uniformemente por InvoiceNumber para poder cruzar entre Portfolio y Glosas.; Portfolio.VReportExtractAccountReceivable expone los movimientos contables agrupables por VoucherName/MovesCode/AccountNumber.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewReportInvoiceTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El identificador único de cada fila se compone de Id de la cuenta por cobrar concatenado con VoucherName, MovesCode y AccountNumber del movimiento contable.; El valor total de la factura siempre se expresa como suma del valor a la entidad (tipo 2) más el valor a paciente (tipos 4 y 6).; Solo se consideran movimientos contables del extracto con Status IN (2,4).; Solo se consideran traslados a cobro jurídico cuyo encabezado tenga State = ''2''.; Para objeciones recibidas y conciliaciones siempre se reporta únicamente la última versión por factura (MAX del Id cabecera).; Todos los importes monetarios se devuelven con ISNULL a 0 para evitar valores nulos en el reporte.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewReportInvoiceTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Cuenta por cobrar; Cartera; Glosa; Recepción de objeciones; Conciliación de glosas; Cobro jurídico; Radicación de factura; EPS / entidad pagadora; Valor paciente vs valor entidad; Saldo de glosa; Primera y segunda instancia de glosa; Movimientos contables (débito/crédito); Estado de cartera (radicada, objetada, certificada, difícil recaudo, cobro jurídico)', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewReportInvoiceTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.ViewReportInvoiceTraceability: Devuelve una fila por cada combinación de cuenta por cobrar y movimiento contable (VoucherName + MovesCode + AccountNumber) cuyo Status del extracto sea 2 o 4; si no hay movimientos contables, igual retorna la cuenta por cobrar (LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewReportInvoiceTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ar.AccountReceivableType = 2 (factura entidad) y existe AccountReceivable con AccountReceivableType IN (4,6) para el mismo InvoiceNumber → Se asocia el valor del paciente (PatientValue = SUM de Value de tipos 4 y 6) y se calcula InvoiceValue = valor entidad + valor paciente. else PatientValue = 0 y InvoiceValue = ar.Value.; si ar.PortfolioStatus IN (1..16) → Se traduce a etiqueta textual: 1=Sin Radicar, 2=Radicada, 3=Radicada Entidad, 4=Objetada, 5=Contestada Radicada, 6=Aceptada, 7=Certificada Parcial, 8=Certificada Total, 9=No Subsanable, 10=Dificil Recaudo, 11=Factura Devuelta, 12=Glosa Ratificada, 13=Radicacion Tramite Objecion, 14=Devolucion Factura, 15=Cuenta Dificil Recaudo, 16=Cobro Juridico. else PortfolioStatusName queda nulo.; si gor.State entre 1 y 4 → Se mapea estado de recepción de objeciones: 1=Sin Confirmar, 2=Radicado Confirmado, 3=Oficio con Respuesta, 4=Anulada.; si gc.State = 1 o 2 → Estado de conciliación: 1=Sin Confirmar, 2=Confirmado. else ConciliationStatusName nulo.; si c.State = 0 ó 1 → CustomerStatusName: 0=Inactivo, 1=Activo.; si Para cada (PortfolioGlosaId, InvoiceNumber) en GlosaObjectionsReceptionD existen múltiples GlosaObjectionsReceptionCId → Solo se toma la recepción de objeciones más reciente (MAX(GlosaObjectionsReceptionCId)).; si Para cada (GlosaPortfolioId, InvoiceNumber) en ConciliationD existen múltiples conciliaciones → Solo se toma la conciliación más reciente (MAX(ConciliationCId)).; si tjdcc.State = ''2'' (transferencia a cobro jurídico confirmada) → Se suma LegalTransferValue por InvoiceNumber; transferencias en otros estados se ignoran. else LegalTransferValue = 0.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewReportInvoiceTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.AccountReceivable; Common.Customer; Glosas.GlosaPortfolioGlosada; Glosas.GlosaObjectionsReceptionD; Glosas.GlosaObjectionsReceptionC; Glosas.ConciliationD; Glosas.ConciliationC; Glosas.TransferJuridicalDebtCollectionC; Glosas.TransferJuridicalDebtCollectionD; Portfolio.VReportExtractAccountReceivable', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewReportInvoiceTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewReportInvoiceTraceability';
GO
