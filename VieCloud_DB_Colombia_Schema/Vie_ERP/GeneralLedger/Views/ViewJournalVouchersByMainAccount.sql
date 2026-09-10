
CREATE VIEW [GeneralLedger].[ViewJournalVouchersByMainAccount] AS
	SELECT	
			jv.Id AS JournalVouchersId,
			jv.LegalBookId,
			jvt.Code JournalVoucherTypeCode,
			jvt.Name JournalVoucherTypeName, 		
			jv.Consecutive, 
			jv.VoucherDate,
			en.Description EntityName,
			jv.EntityCode,
			jv.Detail,
			ISNULL(jvd.Value, 0) Value,
			CASE jv.Status
				WHEN 1 THEN 'Registrado'
				WHEN 2 THEN 'Confirmado'
				WHEN 3 THEN 'Anulado'
			END StatusName,	
			jvd.IdMainAccount
	FROM GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK)
	JOIN GeneralLedger.JournalVouchers jv WITH (NOLOCK) ON jvt.Id = jv.IdJournalVoucher
	LEFT JOIN Common.GetEntityNameDescriptions() en ON jv.EntityName = en.EntityName
	LEFT JOIN 
	(
		SELECT IdAccounting, SUM(DebitValue) value,IdMainAccount
		FROM GeneralLedger.JournalVoucherDetails WITH (NOLOCK)
		GROUP BY IdAccounting,IdMainAccount
	) jvd ON jv.Id = jvd.IdAccounting
	WHERE en.EntityName NOT IN ( 'AccountPayable', 'PaymentNotes','PaymentTransfer','VoucherTransaction','CashReceipts','CrossingAccount')
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista contable que consolida los comprobantes de diario (vouchers) agrupados por cuenta principal (cuenta mayor), mostrando para cada comprobante su tipo, consecutivo, fecha, entidad relacionada, detalle descriptivo, valor débito acumulado y estado (Registrado, Confirmado o Anulado). Integra los tipos de comprobante, los encabezados de comprobante, el nombre legible de la entidad de negocio asociada y el detalle de movimientos contables sumados por cuenta principal, excluyendo entidades como cuentas por pagar, notas de pago, transferencias, transacciones de voucher, recaudos y cruce de cuentas. Sirve para reportería contable y auditoría del libro legal, permitiendo consultar qué comprobantes afectan cada cuenta mayor del plan de cuentas.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'VIEW', @level1name = N'ViewJournalVouchersByMainAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'VIEW', @level1name = N'ViewJournalVouchersByMainAccount';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los comprobantes de diario agrupados por cuenta principal, mostrando totales de débito por comprobante y excluyendo orígenes de entidades no contables operativas.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'ViewJournalVouchersByMainAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen tipos de comprobante en GeneralLedger.JournalVoucherTypes asociados a los comprobantes vía IdJournalVoucher; La función Common.GetEntityNameDescriptions() debe devolver descripciones por EntityName', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'ViewJournalVouchersByMainAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor expuesto corresponde únicamente a la suma de DebitValue (no incluye CreditValue); Si un comprobante no tiene detalles, su Value se expone como 0 en lugar de NULL; Solo se consideran tres estados de comprobante: Registrado, Confirmado, Anulado; otros estados producen StatusName NULL; El filtro por EntityName se aplica con INNER (LEFT JOIN + WHERE NOT IN), por lo que comprobantes sin descripción de entidad quedan excluidos', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'ViewJournalVouchersByMainAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante de diario; Tipo de comprobante; Cuenta principal; Débito; Estado del comprobante (Registrado/Confirmado/Anulado); Entidad contable; Libro legal (LegalBook); Consecutivo de comprobante', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'ViewJournalVouchersByMainAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] GeneralLedger.ViewJournalVouchersByMainAccount: Devuelve filas de comprobantes con su valor sumado de DebitValue agrupado por IdAccounting e IdMainAccount; si no hay detalle el valor se retorna como 0 (ISNULL); [RETURN_RESULT] GeneralLedger.ViewJournalVouchersByMainAccount: Excluye registros cuya EntityName esté en (''AccountPayable'',''PaymentNotes'',''PaymentTransfer'',''VoucherTransaction'',''CashReceipts'',''CrossingAccount'')', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'ViewJournalVouchersByMainAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si jv.Status = 1 → StatusName = ''Registrado''; si jv.Status = 2 → StatusName = ''Confirmado''; si jv.Status = 3 → StatusName = ''Anulado''', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'ViewJournalVouchersByMainAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GetEntityNameDescriptions', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'ViewJournalVouchersByMainAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.JournalVoucherTypes; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherDetails; Common.GetEntityNameDescriptions', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'ViewJournalVouchersByMainAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'ViewJournalVouchersByMainAccount';
GO
