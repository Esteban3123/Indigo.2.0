

CREATE View [ViewInternal].[VistaDocumentosLibros]
as (
select cast(am.VoucherDate as date) as DocumentDate, am.EntityId, am.EntityCode, am.EntityName, movcol.Consecutive, movcol.Cuenta, movcol.CreditValue, movcol.DebitValue, movniif.Consecutive as ConsecutiveNiif, isnull(movniif.CreditValue,0) as CreditValueNiif, isnull(movniif.DebitValue,0) as DebitValueNiif
from GeneralLedger.AccountingMovement am
inner join (
select jv.AccountingMovementId,jv.Consecutive, jvt.Code + ' - ' + jvt.Name as JournalVoucherType, ma.Id as IdAccount, ma.Number + ' - ' + ma.Name as Cuenta, sum(jvd.DebitValue) as DebitValue, sum(jvd.CreditValue) as CreditValue
from GeneralLedger.JournalVouchers jv
inner join GeneralLedger.JournalVoucherDetails jvd on jv.Id = jvd.IdAccounting
inner join GeneralLedger.MainAccounts ma on ma.Id = jvd.IdMainAccount
inner join GeneralLedger.JournalVoucherTypes jvt on jvt.Id = jv.IdJournalVoucher
inner join GeneralLedger.LegalBook lb on lb.Id = jv.LegalBookId
where lb.Id = 1
group by jv.AccountingMovementId,jv.Consecutive, jvt.Code, jvt.Name, ma.Id, ma.Number, ma.Name
) as movcol on movcol.AccountingMovementId = am.Id
left join GeneralLedger.HomologationAccount ha on ha.OfficialMainAccountId = movcol.IdAccount
left join (
select jv.AccountingMovementId,jv.Consecutive, jvt.Code + ' - ' + jvt.Name as JournalVoucherType, ma.Id as IdAccount, sum(jvd.DebitValue) as DebitValue, sum(jvd.CreditValue) as CreditValue
from GeneralLedger.JournalVouchers jv
inner join GeneralLedger.JournalVoucherDetails jvd on jv.Id = jvd.IdAccounting
inner join GeneralLedger.MainAccounts ma on ma.Id = jvd.IdMainAccount
inner join GeneralLedger.JournalVoucherTypes jvt on jvt.Id = jv.IdJournalVoucher
inner join GeneralLedger.LegalBook lb on lb.Id = jv.LegalBookId
where lb.Id = 2
group by jv.AccountingMovementId,jv.Consecutive, jvt.Code, jvt.Name, ma.Id
) as movniif on movniif.AccountingMovementId = am.Id and movniif.IdAccount = ha.MainAccountId
where movcol.DebitValue <> isnull(movniif.DebitValue,0) or movcol.CreditValue <> isnull(movniif.CreditValue,0)
)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Consolida en una sola vista los comprobantes contables del libro oficial (Id=1, norma local) comparados con sus equivalentes NIIF (Id=2), cruzando mediante la tabla de homologación de cuentas. Muestra por movimiento contable los valores débito/crédito de ambos libros, filtrando únicamente los casos donde existen diferencias entre ellos. Está orientada a reportes de conciliación y auditoría entre el libro contable local y el libro NIIF.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaDocumentosLibros';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaDocumentosLibros';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Compara los movimientos contables registrados en el libro contable local (COL) frente a su equivalente NIIF, exponiendo solo aquellos asientos cuyos valores débito o crédito difieren entre ambos libros.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaDocumentosLibros';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el libro legal con Id=1 (libro local/COL) y el libro legal con Id=2 (libro NIIF) en GeneralLedger.LegalBook.; Las cuentas del libro local deben estar homologadas en GeneralLedger.HomologationAccount mediante OfficialMainAccountId → MainAccountId para poder cruzar contra el libro NIIF.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaDocumentosLibros';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El libro local siempre se identifica con LegalBookId=1 y el libro NIIF con LegalBookId=2.; La equivalencia entre cuentas locales y NIIF se obtiene exclusivamente vía HomologationAccount (OfficialMainAccountId → MainAccountId).; Los valores NIIF nulos se normalizan a 0 antes de comparar y exponer.; Solo se exponen filas con diferencia entre libro local y NIIF; los movimientos perfectamente conciliados quedan excluidos.; Los valores débito y crédito se totalizan por movimiento y cuenta antes de comparar.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaDocumentosLibros';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Movimiento contable; Comprobante de diario (Journal Voucher); Plan de cuentas (Main Account); Libro legal contable; Libro local (COL); Libro NIIF; Homologación de cuentas; Débito y crédito; Conciliación contable entre libros', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaDocumentosLibros';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve únicamente los movimientos donde ''movcol.DebitValue <> isnull(movniif.DebitValue,0) or movcol.CreditValue <> isnull(movniif.CreditValue,0)'', es decir, donde hay diferencia entre el libro local (LegalBookId=1) y el NIIF (LegalBookId=2).', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaDocumentosLibros';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si lb.Id = 1 en la subconsulta movcol → Agrega los detalles de comprobantes pertenecientes al libro contable local (COL) agrupando por movimiento, consecutivo, tipo de voucher y cuenta.; si lb.Id = 2 en la subconsulta movniif → Agrega los detalles de comprobantes pertenecientes al libro NIIF agrupando por movimiento, consecutivo, tipo de voucher y cuenta.; si LEFT JOIN sobre HomologationAccount y movniif → Si una cuenta local no tiene homologación o no existe contraparte NIIF, los valores NIIF se asumen como 0 mediante ISNULL, exponiendo igualmente la diferencia.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaDocumentosLibros';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.AccountingMovement; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherDetails; GeneralLedger.MainAccounts; GeneralLedger.JournalVoucherTypes; GeneralLedger.LegalBook; GeneralLedger.HomologationAccount', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaDocumentosLibros';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaDocumentosLibros';
GO
