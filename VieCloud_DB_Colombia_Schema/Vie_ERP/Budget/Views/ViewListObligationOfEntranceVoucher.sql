CREATE VIEW [Budget].[ViewListObligationOfEntranceVoucher]
AS
select ROW_NUMBER() OVER (ORDER BY ev.Id) Id,
ev.Id EntranceVoucherId,
ap.Id AccountPayableId,
od.Id ObligationDetailId, od.InitialValue, od.Balance, od.EntityId, od.EntityCode, od.EntityName,
o.Id ObligationId, o.Code ObligationCode, o.Document ObligationDocument,
c.Id CategoryId, c.Name CategoryName, c.Code + ' - ' + c.Name CategoryDescription,
rt.Id RevenueTypeId, rt.Code + ' - ' + rt.Name RevenueTypeDescription,
fs.Id FinancialSourceId, fs.Code + ' - ' + fs.Name FinancialSourceDescription,
od.CommitmentDetailId
from Inventory.EntranceVoucher ev
inner join Payments.AccountPayable ap on ap.Id = ev.AccountPayableId
inner join Budget.ObligationDetail od on od.EntityId = ap.Id and od.EntityName = 'AccountPayable'
inner join Budget.Obligation o on o.Id = od.ObligationId
inner join Budget.Category c on c.Id = od.CategoryId
inner join Budget.RevenueType rt on rt.Id = od.RevenueTypeId
left join Budget.FinancialSource fs on fs.Id = c.FinancialSourceId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que relaciona los comprobantes de entrada de mercancía (vales de entrada) con sus obligaciones presupuestarias, permitiendo identificar qué compromiso de gasto respalda cada recepción de productos en bodega. Cruza el vale de entrada con la cuenta por pagar al proveedor, y ésta con el detalle de obligación presupuestal, obteniendo la categoría de gasto, el tipo de ingreso (fuente de renta) y la fuente financiera asociada. Sirve para reportería presupuestal y de tesorería que requiere trazabilidad completa desde la recepción física de mercancía hasta el compromiso formal de pago registrado en el presupuesto, incluyendo valores iniciales, saldos y códigos de clasificación presupuestal.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'VIEW', @level1name = N'ViewListObligationOfEntranceVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'VIEW', @level1name = N'ViewListObligationOfEntranceVoucher';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las obligaciones presupuestarias y su detalle (rubro, tipo de renta, fuente de financiación) asociadas a las cuentas por pagar vinculadas a comprobantes de entrada de inventario.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListObligationOfEntranceVoucher';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada EntranceVoucher debe tener una AccountPayable asociada (AccountPayableId NOT NULL y existente).; Debe existir al menos un ObligationDetail cuyo EntityName=''AccountPayable'' y EntityId apunte al Id de la cuenta por pagar.; El ObligationDetail debe tener Obligation, Category y RevenueType válidos y existentes.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListObligationOfEntranceVoucher';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El vínculo entre obligación presupuestaria y cuenta por pagar se establece exclusivamente mediante ObligationDetail.EntityName=''AccountPayable'' y ObligationDetail.EntityId = AccountPayable.Id (relación polimórfica).; Las descripciones de Categoría, Tipo de Renta y Fuente Financiera se exponen siempre concatenadas como ''Code - Name''.; Solo se incluyen comprobantes de entrada cuya cuenta por pagar tenga obligación con detalle, categoría y tipo de renta (los joins son INNER excepto FinancialSource).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListObligationOfEntranceVoucher';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante de entrada de inventario; Cuenta por pagar; Obligación presupuestaria; Detalle de obligación; Categoría presupuestal; Tipo de renta/ingreso; Fuente de financiación; Compromiso presupuestal', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListObligationOfEntranceVoucher';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Budget.ViewListObligationOfEntranceVoucher: Devuelve un Id secuencial generado con ROW_NUMBER() OVER (ORDER BY ev.Id) por cada combinación de comprobante de entrada y línea de obligación presupuestaria.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListObligationOfEntranceVoucher';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si LEFT JOIN Budget.FinancialSource fs ON fs.Id = c.FinancialSourceId → Si la categoría tiene fuente financiera asociada, se incluye su descripción; en caso contrario los campos de FinancialSource quedan en NULL. else FinancialSourceId y FinancialSourceDescription se devuelven en NULL.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListObligationOfEntranceVoucher';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.EntranceVoucher; Payments.AccountPayable; Budget.ObligationDetail; Budget.Obligation; Budget.Category; Budget.RevenueType; Budget.FinancialSource', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListObligationOfEntranceVoucher';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListObligationOfEntranceVoucher';
GO
