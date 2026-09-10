

CREATE VIEW [GeneralLedger].[VRetentionsReports] AS 
(
	SELECT
		jvd.Id
	   ,CAST(jv.VoucherDate AS DATE) AS VoucherDate
	   ,ma.Number
	   ,ma.Name AS Cuenta
	   ,t.Nit
	   ,t.Name AS Tercero
	   ,jvd.DebitValue
	   ,jvd.CreditValue
	   ,jvd.Detail
	   ,ISNULL(rc.Name, '') AS Retencion
	   ,ISNULL(jvd.RetentionRate, 0) AS RetentionRate
	   ,ISNULL(CASE ISNULL(jvd.RetentionRate, 0)
			WHEN 0 THEN CAST(jvd.BaseValue AS BIGINT)
			WHEN 1 THEN CAST(jvd.BaseValue AS BIGINT)
			ELSE CAST(ROUND((jvd.CreditValue * 100 / jvd.RetentionRate), 1) AS BIGINT)
		END, 0) AS Base
	   ,CASE jv.Status
			WHEN 1 THEN 'Registrado'
			WHEN 2 THEN 'Confirmado'
			WHEN 3 THEN 'Anulado'
		END Estado
	   ,IdRetention = ISNULL(jvd.IdRetention, 0)
	   ,CodeNameVoucherType = JVT.Code + ' ' + JVT.Name
	   ,jv.Consecutive
	   ,OriginDocument =
		CASE JV.EntityName
			WHEN 'AccountPayable' THEN 'Cuenta por Pagar'
			WHEN 'BasicBilling' THEN 'Factura básica'
			WHEN 'CashReceipts' THEN 'Recibo de caja'
			WHEN 'PaymentNotes' THEN 'Nota'
			WHEN 'PayrollLiquidation' THEN 'Liquidación de nomina'
			WHEN 'PortfolioNote' THEN 'Nota'
			WHEN 'TreasuryNote' THEN 'Nota'
			WHEN 'VoucherTransaction' THEN 'Egreso'
			WHEN 'PortfolioTransfer' THEN 'Cruce de anticipo vs CxC'
			WHEN 'PaymentTransfer' THEN 'Cruce de anticipo vs CxP'
			WHEN 'PortfolioReclassification' THEN 'Reclasificación documento de cartera'
			ELSE JVT.NAME
		END
		+ ' ' + ISNULL(jv.EntityCode, '')
	FROM GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK)
	JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ma.Id = jvd.IdMainAccount
	JOIN GeneralLedger.LegalBook lb WITH (NOLOCK) ON ma.LegalBookId = lb.Id AND lb.OfficialBook = 1
	JOIN GeneralLedger.JournalVouchers jv WITH (NOLOCK) ON jv.Id = jvd.IdAccounting
	INNER JOIN GeneralLedger.JournalVoucherTypes JVT WITH (NOLOCK) ON JVT.ID = JV.IdJournalVoucher
	LEFT JOIN Common.ThirdParty t WITH (NOLOCK) ON t.Id = jvd.IdThirdParty
	LEFT JOIN GeneralLedger.RetentionConcepts rc WITH (NOLOCK) ON rc.Id = jvd.IdRetention
	WHERE ma.AllowsMovement = 1 
		AND jv.Status = 2 
		AND ma.RetencionType <> 0
)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de retenciones tributarias para reportería contable. Consolida los movimientos contables del libro mayor oficial que corresponden a retenciones (retención en la fuente, ICA, IVA, entre otras), cruzando los detalles del comprobante contable con la cuenta contable, el tercero (proveedor, contratista o entidad) y el concepto de retención aplicado. Muestra el valor base gravable, la tarifa de retención, los valores débito y crédito, el estado del comprobante (Registrado, Confirmado, Anulado) y el documento de origen (cuenta por pagar, factura, recibo de caja, egreso, nómina, nota, etc.). Está diseñada para generar informes de retenciones practicadas a terceros, soportar la declaración tributaria y auditar los comprobantes contables con retención en el libro oficial confirmado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'VIEW', @level1name = N'VRetentionsReports';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'VIEW', @level1name = N'VRetentionsReports';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los movimientos contables confirmados asociados a cuentas de retención, con su base, tarifa, tercero y documento origen, para alimentar reportes tributarios de retenciones.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'VRetentionsReports';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La cuenta contable debe permitir movimiento (AllowsMovement=1); El comprobante debe estar en estado Confirmado (Status=2); La cuenta debe estar marcada como cuenta de retención (RetencionType<>0); La cuenta debe pertenecer a un libro legal oficial (LegalBook.OfficialBook=1)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'VRetentionsReports';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan movimientos de comprobantes confirmados; Solo cuentas de tipo retención (RetencionType<>0) entran al reporte; La base nunca es NULL: se reemplaza por 0 si no se puede calcular; El concepto de retención y el IdRetention nunca son NULL en el resultado (se reemplazan por '''' y 0); Solo se consideran cuentas pertenecientes al libro oficial', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'VRetentionsReports';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Retención tributaria; Comprobante contable; Tercero; Cuenta contable; Libro oficial; Base de retención; Tarifa de retención; Cuenta por pagar; Factura; Recibo de caja; Nota contable; Liquidación de nómina; Egreso; Anticipo; Cartera', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'VRetentionsReports';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] GeneralLedger.VRetentionsReports: Devuelve el detalle del comprobante solo cuando AllowsMovement=1, Status=2 y RetencionType<>0, uniendo cuentas, terceros y conceptos de retención', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'VRetentionsReports';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si RetentionRate IS NULL o 0 → La base se toma directamente de BaseValue else La base se calcula como CreditValue*100/RetentionRate redondeado; si RetentionRate = 1 → La base se toma directamente de BaseValue (no se aplica la fórmula de cálculo); si Status del comprobante → Traduce 1=Registrado, 2=Confirmado, 3=Anulado a etiqueta legible (aunque solo expone Status=2); si EntityName del comprobante → Mapea el origen a etiqueta de negocio (Cuenta por Pagar, Factura básica, Recibo de caja, Nota, Liquidación de nómina, Egreso, Cruce de anticipo vs CxC/CxP, Reclasificación documento de cartera) else Si no coincide, usa el nombre del tipo de comprobante (JVT.Name)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'VRetentionsReports';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.JournalVoucherDetails; GeneralLedger.MainAccounts; GeneralLedger.LegalBook; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherTypes; Common.ThirdParty; GeneralLedger.RetentionConcepts', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'VRetentionsReports';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'VRetentionsReports';
GO
