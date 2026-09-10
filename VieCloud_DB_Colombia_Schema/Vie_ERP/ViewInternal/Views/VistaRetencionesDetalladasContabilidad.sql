

CREATE view [ViewInternal].[VistaRetencionesDetalladasContabilidad]
as (
select CAST(jv.VoucherDate AS DATE) AS VoucherDate, ma.Number, ma.Name as Cuenta, t.Nit, t.Name as Tercero,jvd.DebitValue, jvd.CreditValue, jvd.Detail, rc.Name as Retencion, jvd.RetentionRate, case isnull(RetentionRate,0) when 0 then cast(BaseValue as bigint) when 1 then cast(BaseValue as bigint) else cast(ROUND((CreditValue * 100 / RetentionRate),1) as bigint) end as Base, case jv.Status when 1 then 'Registrado' when 2 then 'Confirmado' when 3 then 'Anulado' end Estado
from GeneralLedger.JournalVoucherDetails jvd
inner join GeneralLedger.MainAccounts ma on ma.Id = jvd.IdMainAccount
inner join GeneralLedger.JournalVouchers jv on jv.Id = jvd.IdAccounting
left join Common.ThirdParty t on t.Id = jvd.IdThirdParty
left join GeneralLedger.RetentionConcepts rc on rc.Id = jvd.IdRetention
where ma.AllowsMovement = 1 and (ma.Number like '2436%' or ma.Number like '2445%') and jv.Status = 2 and jv.LegalBookId = 1
)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting contable que consolida los detalles de retenciones tributarias confirmadas (estado=2) del libro legal principal, filtrando únicamente cuentas del plan PUC que inician con 2436 (retención en la fuente) y 2445 (retención IVA). Calcula la base gravable ajustada según la tarifa de retención aplicada y expone débitos, créditos, tercero con NIT, concepto de retención y estado del comprobante para consumo de informes contables y tributarios.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRetencionesDetalladasContabilidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRetencionesDetalladasContabilidad';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el detalle de movimientos contables confirmados del libro legal correspondientes a cuentas de retenciones (códigos 2436 y 2445), calculando la base gravable a partir de la tasa de retención.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRetencionesDetalladasContabilidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El comprobante contable debe estar Confirmado (Status = 2); El comprobante debe pertenecer al libro legal con LegalBookId = 1; La cuenta contable debe permitir movimiento (AllowsMovement = 1); La cuenta contable debe iniciar con ''2436'' o ''2445'' (cuentas de retenciones)', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRetencionesDetalladasContabilidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen movimientos de cuentas que comienzan con 2436 o 2445 (retenciones tributarias); Solo se incluyen comprobantes en estado Confirmado (Status=2), excluyendo Registrados y Anulados; Solo se considera el libro legal (LegalBookId=1); Solo cuentas que permiten movimiento (AllowsMovement=1); La fecha del comprobante se trunca a tipo DATE (sin hora); La base gravable nunca se calcula por división cuando la tasa es 0 o 1, evitando división por cero o resultados erróneos; Tercero y concepto de retención son opcionales (LEFT JOIN)', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRetencionesDetalladasContabilidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Retención en la fuente; Comprobante contable; Cuenta contable (PUC); Tercero; Base gravable; Tasa de retención; Libro legal; Débito/Crédito; Estado de comprobante (Registrado/Confirmado/Anulado)', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRetencionesDetalladasContabilidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ViewInternal.VistaRetencionesDetalladasContabilidad: Devuelve cada línea de detalle contable cuya cuenta sea de retención (2436% o 2445%), con movimiento confirmado en libro legal 1, incluyendo cálculo de base', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRetencionesDetalladasContabilidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si RetentionRate IS NULL o = 0 → Base = BaseValue (convertido a bigint) else Si RetentionRate = 1 → Base = BaseValue; en cualquier otro caso → Base = ROUND(CreditValue * 100 / RetentionRate, 1); si Status del comprobante → 1=''Registrado'', 2=''Confirmado'', 3=''Anulado'' (etiqueta textual del estado)', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRetencionesDetalladasContabilidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.JournalVoucherDetails; GeneralLedger.MainAccounts; GeneralLedger.JournalVouchers; Common.ThirdParty; GeneralLedger.RetentionConcepts', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRetencionesDetalladasContabilidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRetencionesDetalladasContabilidad';
GO
