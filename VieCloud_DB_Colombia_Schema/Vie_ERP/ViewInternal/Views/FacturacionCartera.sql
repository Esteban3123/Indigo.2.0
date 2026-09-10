

CREATE view [ViewInternal].[FacturacionCartera]
as
(
select 
ar.InvoiceNumber as Factura
,AccountReceivableDate as FechaFactura
,t.Nit as NitEntidad
,t.Name as Entidad
,c.Code as Contracto
,cg.Code as CodigoGrupoAtencion
,cg.Name as GrupoAtencion
,ar.PortfolioStatus as EstadoCartera
,ar.OpeningBalance as SaldoInicial
,ar.Value as Valor
,ar.Balance as Saldo
from Portfolio.AccountReceivable ar
inner join Common.ThirdParty t on ar.ThirdPartyId = t.Id
left join Billing.Invoice i on i.Id = ar.InvoiceId
left join Contract.CareGroup cg on cg.Id = i.CareGroupId
left join Contract.Contract c on cg.ContractId = c.Id
where ar.AccountReceivableType = 2 and ar.Balance > 0 and ar.Status = 2

)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de cartera de facturación que consolida las cuentas por cobrar pendientes (saldo mayor a cero, tipo 2 y estado activo) cruzando datos de la factura emitida, el tercero pagador (NIT y nombre), el contrato y el grupo de atención asociado. Está orientada a reporting de gestión de cartera, permitiendo visualizar el estado, saldo inicial y saldo vigente de cada factura por entidad pagadora y contrato.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'FacturacionCartera';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'FacturacionCartera';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone la cartera vigente de facturas con saldo pendiente, asociando cada cuenta por cobrar con su entidad pagadora, contrato y grupo de atención.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'FacturacionCartera';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las cuentas por cobrar deben estar tipificadas como tipo 2 (facturas); El saldo de la cuenta por cobrar debe ser mayor que cero; El estado de la cuenta por cobrar debe ser 2 (activo/vigente)', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'FacturacionCartera';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen cuentas por cobrar de tipo factura (AccountReceivableType=2); Solo se incluyen registros con saldo pendiente (Balance>0); Solo se exponen cuentas por cobrar en estado 2; El tercero (entidad pagadora) siempre debe existir para la cuenta por cobrar (INNER JOIN); La factura, grupo de atención y contrato son opcionales (LEFT JOIN), permitiendo cuentas por cobrar sin factura asociada', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'FacturacionCartera';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cartera; Cuenta por cobrar; Factura; Saldo; Entidad pagadora (tercero/NIT); Contrato; Grupo de atención; Estado de cartera; Saldo inicial', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'FacturacionCartera';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.AccountReceivable: Cuando AccountReceivableType=2 AND Balance>0 AND Status=2, se retorna la cuenta por cobrar enriquecida con tercero, factura, grupo de atención y contrato', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'FacturacionCartera';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.AccountReceivable; Common.ThirdParty; Billing.Invoice; Contract.CareGroup; Contract.Contract', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'FacturacionCartera';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'FacturacionCartera';
GO
