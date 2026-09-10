
CREATE PROCEDURE [GeneralLedger].[SP_ReportCertificateRetIVA]
	-- Add the parameters for the stored procedure here
    @DateStart as date,
	@DateEnd as date,
	@AccountStart as varchar(50),
	@AccountEnd as varchar(50),
	@NitStart as varchar(15),
	@NitEnd as varchar(15),
	@RetencionType as integer,
	@LegalBookId as integer
AS
BEGIN
select gr.Rate,ct.Nit,ct.Name as Tercero,ma.Number,ma.Name as Concept,SUM(jvd.BillingValue) as ValorFacturado
,SUM(case mc.[Nature] when 1 then iif(jvd.CreditValue > 0, BaseValue * - 1, BaseValue) else iif(jvd.DebitValue > 0, BaseValue * - 1, BaseValue) end) as BaseRetención,
SUM(IIF(mc.[Nature] = 1,(jvd.DebitValue - jvd.CreditValue),(jvd.CreditValue - jvd.DebitValue))) as ValorRetenido,
'' as LettersValue
from GeneralLedger.JournalVoucherDetails as jvd
inner join GeneralLedger.JournalVouchers as jv on jv.Id = jvd.IdAccounting
left join GeneralLedger.RetentionConcepts as gr on gr.Id =jvd.IdRetention
inner join Common.ThirdParty as ct on ct.Id = jvd.IdThirdParty
inner join GeneralLedger.MainAccounts as ma on ma.Id = jvd.IdMainAccount
inner join GeneralLedger.MainAccountClasses as mc on mc.Id = ma.IdAccountClass 
where cast(jv.VoucherDate as date) between @DateStart and @DateEnd and ma.Number  >= ISNULL(@AccountStart,'0') and ma.Number <= ISNULL(@AccountEnd, 'z') and ct.Nit >= ISNULL(@NitStart,'0') and ct.Nit <= ISNULL(@NitEnd, 'z') and ma.RetencionType = @RetencionType and jv.LegalBookId = @LegalBookId and jv.IsClosedYear = 0 and jv.Status = 2
group by gr.Rate,ct.Nit,ct.Name,ma.Number,ma.Name having SUM(IIF(mc.[Nature] = 1,(jvd.DebitValue - jvd.CreditValue),(jvd.CreditValue - jvd.DebitValue))) > 0
order by ct.Nit,ma.Number
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el certificado de retención de IVA por tercero y cuenta contable para un período de fechas determinado. Consolida los movimientos del libro diario (comprobantes contables) cruzando el detalle de los vouchers con los conceptos de retención, los terceros (proveedores o contratistas identificados por NIT) y las cuentas contables del plan de cuentas, calculando el valor facturado, la base gravable y el valor retenido de IVA. Se filtra por rango de fechas, rango de cuentas contables, rango de NIT, tipo de retención y libro legal, agrupando los resultados por tercero y cuenta para presentar únicamente los registros con valor retenido positivo, tal como lo exige la declaración y certificación tributaria de retención en la fuente de IVA.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportCertificateRetIVA';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportCertificateRetIVA';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte/certificado de retenciones (IVA u otro tipo) por tercero y cuenta, sumando valor facturado, base de retención y valor retenido en un rango de fechas, cuentas y NITs para un libro legal.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCertificateRetIVA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los comprobantes deben pertenecer al LegalBookId indicado; Los comprobantes deben tener Status = 2 (contabilizados/aprobados); Los comprobantes no deben estar en año cerrado (IsClosedYear = 0); Las cuentas consultadas deben tener configurado RetencionType igual al solicitado; Los rangos de cuenta y NIT admiten nulos: si vienen nulos se asume rango total (''0'' a ''z'')', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCertificateRetIVA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se incluyen comprobantes con Status = 2 y no pertenecientes a años cerrados; El cálculo de retención respeta la naturaleza contable (débito/crédito) de la clase de cuenta; Se excluyen agrupaciones cuya retención neta no sea positiva (>0); Filtra estrictamente por el RetencionType configurado en la cuenta y por el LegalBookId del comprobante; El filtro de fecha se aplica sobre VoucherDate convertido a date', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCertificateRetIVA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Retención de IVA; Certificado de retención; Tercero / NIT; Comprobante contable / Journal Voucher; Plan de cuentas (PUC); Naturaleza contable (débito/crédito); Concepto de retención y tarifa (Rate); Libro legal contable; Año cerrado contable; Base de retención; Valor facturado', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCertificateRetIVA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas agrupadas por Rate, Nit, Tercero, Número y Nombre de cuenta sólo cuando el valor retenido neto (según naturaleza de la cuenta) es mayor a 0', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCertificateRetIVA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si MainAccountClasses.Nature = 1 (naturaleza débito) → ValorRetenido = DebitValue - CreditValue y BaseRetención toma BaseValue negativo si hay CreditValue, positivo en otro caso else ValorRetenido = CreditValue - DebitValue y BaseRetención toma BaseValue negativo si hay DebitValue, positivo en otro caso; si Parámetros de rango (@AccountStart, @AccountEnd, @NitStart, @NitEnd) son NULL → Se sustituyen por ''0'' y ''z'' respectivamente para abarcar todo el rango', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCertificateRetIVA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.JournalVoucherDetails; GeneralLedger.JournalVouchers; GeneralLedger.RetentionConcepts; Common.ThirdParty; GeneralLedger.MainAccounts; GeneralLedger.MainAccountClasses', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCertificateRetIVA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCertificateRetIVA';
-- GO
