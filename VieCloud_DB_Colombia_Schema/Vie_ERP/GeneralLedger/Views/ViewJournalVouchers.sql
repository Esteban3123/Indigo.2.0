CREATE VIEW [GeneralLedger].[ViewJournalVouchers]
AS
	SELECT	jv.Id,
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
			END StatusName		
	FROM GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK)
	JOIN GeneralLedger.JournalVouchers jv WITH (NOLOCK) ON jvt.Id = jv.IdJournalVoucher
	LEFT JOIN Common.GetEntityNameDescriptions() en ON jv.EntityName = en.EntityName
	LEFT JOIN 
	(
		SELECT IdAccounting, SUM(DebitValue) Value
		FROM GeneralLedger.JournalVoucherDetails WITH (NOLOCK)
		GROUP BY IdAccounting
	) jvd ON jv.Id = jvd.IdAccounting
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de comprobantes de diario contable que consolida la información de cada comprobante con su tipo, consecutivo, fecha, entidad relacionada, detalle descriptivo, valor total en débitos y estado (Registrado, Confirmado o Anulado). Integra el catálogo de tipos de comprobante, la descripción legible de la entidad asociada al movimiento contable y el total de débitos calculado desde el detalle de líneas contables. Sirve para reportería y consulta del libro mayor, permitiendo identificar rápidamente cada asiento contable por tipo, período, entidad y su situación actual.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'VIEW', @level1name = N'ViewJournalVouchers';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'VIEW', @level1name = N'ViewJournalVouchers';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los comprobantes contables del libro mayor con su tipo, entidad asociada, valor total debitado y estado descriptivo legible.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'ViewJournalVouchers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor del comprobante se calcula como la suma de DebitValue de sus líneas de detalle agrupadas por IdAccounting; Si no existen detalles asociados, el valor reportado es 0 (ISNULL); Solo se listan comprobantes cuyo tipo exista en JournalVoucherTypes (JOIN interno); La descripción de entidad y el valor son opcionales (LEFT JOIN); ausencia no excluye el comprobante; El estado se traduce únicamente para los valores 1, 2 y 3; otros valores resultan en StatusName NULL', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'ViewJournalVouchers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante contable; Libro mayor; Tipo de comprobante; Asiento contable; Débito; Estado del comprobante (Registrado/Confirmado/Anulado); Entidad contable; Consecutivo de comprobante', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'ViewJournalVouchers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve un renglón por cada comprobante (JournalVouchers) cruzado con su tipo (JournalVoucherTypes); incluye descripción de entidad y suma de débitos del detalle.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'ViewJournalVouchers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Status = 1 → Se muestra como ''Registrado''; si Status = 2 → Se muestra como ''Confirmado''; si Status = 3 → Se muestra como ''Anulado''', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'ViewJournalVouchers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.JournalVoucherTypes; GeneralLedger.JournalVouchers; Common.GetEntityNameDescriptions; GeneralLedger.JournalVoucherDetails', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'ViewJournalVouchers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'ViewJournalVouchers';
GO
