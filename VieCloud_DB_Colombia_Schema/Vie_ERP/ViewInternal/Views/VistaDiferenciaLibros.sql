

CREATE view [ViewInternal].[VistaDiferenciaLibros]
as (
select datacol.Anio, datacol.Mes, datacol.IdMainAccount, datacol.Number + ' - ' + datacol.Name as CuentaColgap, datacol.Nit, datacol.CodeCostCenter, datacol.DebitValue, datacol.CreditValue, manif.Number + ' - ' + manif.Name as CuentaNiif, isnull(datanif.DebitValue,0) as DebitValueNiif, isnull(datanif.CreditValue,0) as CreditValueNiif
from (
select MONTH(jv.VoucherDate) as Mes, year(jv.VoucherDate) as Anio, jvd.IdMainAccount,ma.Number, ma.Name, t.Nit, cc.Code as CodeCostCenter, jvd.IdThirdParty, jvd.IdCostCenter, sum(jvd.DebitValue) as DebitValue, sum(jvd.CreditValue) as CreditValue 
from GeneralLedger.JournalVouchers jv
inner join GeneralLedger.JournalVoucherDetails jvd on jv.Id = jvd.IdAccounting
inner join GeneralLedger.MainAccounts ma on ma.Id = jvd.IdMainAccount
left join Common.ThirdParty t on t.Id = jvd.IdThirdParty
left join Payroll.CostCenter cc on cc.Id = jvd.IdCostCenter
where ma.LegalBookId = 1 and jv.IsClosedYear = 0
group by MONTH(jv.VoucherDate), year(jv.VoucherDate), jvd.IdMainAccount, ma.Number, ma.Name, jvd.IdThirdParty, t.Nit, jvd.IdCostCenter, cc.Code
) as datacol inner join GeneralLedger.HomologationAccount ha on ha.OfficialMainAccountId = datacol.IdMainAccount inner join GeneralLedger.MainAccounts manif on manif.Id = ha.MainAccountId left join
(
select MONTH(jv.VoucherDate) as Mes, year(jv.VoucherDate) as Anio, jvd.IdMainAccount, jvd.IdThirdParty, jvd.IdCostCenter, sum(jvd.DebitValue) as DebitValue, sum(jvd.CreditValue) as CreditValue 
from GeneralLedger.JournalVouchers jv
inner join GeneralLedger.JournalVoucherDetails jvd on jv.Id = jvd.IdAccounting
inner join GeneralLedger.MainAccounts ma on ma.Id = jvd.IdMainAccount
where ma.LegalBookId = 2 and jv.IsClosedYear = 0
group by MONTH(jv.VoucherDate), year(jv.VoucherDate), jvd.IdMainAccount, jvd.IdThirdParty, jvd.IdCostCenter
) as datanif on datacol.Anio = datanif.Anio and datacol.Mes = datanif.Mes and datanif.IdMainAccount = ha.MainAccountId and isnull(datacol.IdThirdParty,0) = isnull(datanif.IdThirdParty,0) and isnull(datacol.IdCostCenter, 0) = isnull(datanif.IdCostCenter,0)
where datacol.DebitValue <> isnull(datanif.DebitValue,0) or datacol.CreditValue <> isnull(datanif.CreditValue,0)
)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting que expone las diferencias entre el libro contable COLGAP (LegalBookId = 1) y el libro NIIF (LegalBookId = 2) para comprobantes de años no cerrados. Cruza los movimientos agrupados por período, cuenta, tercero y centro de costo mediante la tabla de homologación, mostrando los valores débito/crédito de cada libro. Solo retorna registros donde exista discrepancia entre ambos libros, facilitando la conciliación y auditoría contable dual.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaDiferenciaLibros';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaDiferenciaLibros';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Compara mensualmente los saldos débito/crédito por cuenta, tercero y centro de costo entre el libro contable oficial (COLGAAP) y el libro NIIF homologado, listando solo las combinaciones donde existen diferencias.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaDiferenciaLibros';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir homologación en GeneralLedger.HomologationAccount entre la cuenta oficial (COLGAAP, LegalBookId=1) y la cuenta NIIF (LegalBookId=2); Los comprobantes considerados deben pertenecer a años no cerrados (IsClosedYear = 0)', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaDiferenciaLibros';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan movimientos de años contables abiertos (IsClosedYear = 0); El libro oficial se identifica con LegalBookId = 1 y el libro NIIF con LegalBookId = 2; Los valores NIIF inexistentes se tratan como 0 al comparar; La correspondencia entre libros se establece vía HomologationAccount (OfficialMainAccountId → MainAccountId); La comparación se realiza al mismo nivel de granularidad: año, mes, cuenta homologada, tercero y centro de costo (tratando NULL como 0)', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaDiferenciaLibros';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante contable; Libro contable oficial (COLGAAP); Libro NIIF; Homologación de cuentas; Plan de cuentas (PUC); Tercero; Centro de costo; Débito y crédito contable; Cierre de año contable', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaDiferenciaLibros';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve filas agrupadas por Año, Mes, cuenta, tercero y centro de costo donde DebitValue o CreditValue del libro oficial difieren de los del libro NIIF homologado', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaDiferenciaLibros';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ma.LegalBookId = 1 → La cuenta se incluye como cuenta COLGAAP/oficial (datacol); si ma.LegalBookId = 2 → La cuenta se incluye como cuenta NIIF (datanif) para comparación; si datacol.DebitValue <> isnull(datanif.DebitValue,0) OR datacol.CreditValue <> isnull(datanif.CreditValue,0) → La fila se incluye en el resultado por presentar diferencia entre libros else Se excluye del resultado al no haber diferencia', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaDiferenciaLibros';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherDetails; GeneralLedger.MainAccounts; Common.ThirdParty; Payroll.CostCenter; GeneralLedger.HomologationAccount', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaDiferenciaLibros';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaDiferenciaLibros';
GO
