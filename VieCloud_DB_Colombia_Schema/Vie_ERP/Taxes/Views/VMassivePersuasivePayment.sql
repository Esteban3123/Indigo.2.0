

CREATE VIEW [Taxes].[VMassivePersuasivePayment]
AS
select ar.Id, ar.AccountReceivableType as Tipo ,case ar.AccountReceivableType when 3 then 'Industria y Comercio' when 8 then 'Impuesto Predial' end TipoImpuesto, ar.InvoiceNumber as Factura,
tp.nit as NitTercero,  tp.Name as NombreTercero,  ExpiredDate as FechaVencimiento, ar.Value as Valor,  Balance as Saldo, '2017' as Vigencia
from Portfolio.AccountReceivable as ar left outer join Common.ThirdParty as tp on ar.ThirdPartyId = tp.id  where AccountReceivableType in (3,8)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de cuentas por cobrar correspondientes a impuestos municipales: Industria y Comercio (tipo 3) y Predial (tipo 8), diseñada para gestionar y hacer seguimiento al cobro persuasivo masivo de obligaciones tributarias. Integra los documentos de cobro (facturas) registrados en cartera con los datos del tercero contribuyente (NIT y nombre), exponiendo para cada obligación la fecha de vencimiento, el valor original y el saldo pendiente de pago. Sirve como insumo para procesos de cobro persuasivo en lote, reportes de cartera tributaria y gestión de mora en impuestos locales.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'VIEW', @level1name = N'VMassivePersuasivePayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'VIEW', @level1name = N'VMassivePersuasivePayment';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista cuentas por cobrar de Industria y Comercio e Impuesto Predial junto con datos del tercero, para soportar procesos masivos de cobro persuasivo.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'VMassivePersuasivePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en cuentas por cobrar con tipo 3 u 8; Los terceros referenciados pueden o no existir (LEFT JOIN tolera ausencia)', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'VMassivePersuasivePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen cuentas por cobrar de tipo 3 (ICA) u 8 (Predial); La vigencia siempre se reporta como ''2017'' (literal fijo); Si no existe tercero asociado, los campos del tercero quedan en NULL pero la cuenta sigue apareciendo', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'VMassivePersuasivePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cobro persuasivo; Industria y Comercio (ICA); Impuesto Predial; Cuenta por cobrar; Tercero; Saldo; Vigencia tributaria; Fecha de vencimiento; Factura', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'VMassivePersuasivePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.AccountReceivable: Cuando AccountReceivableType IN (3,8) → se retorna la cuenta por cobrar etiquetada como ''Industria y Comercio'' (3) o ''Impuesto Predial'' (8) con datos del tercero', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'VMassivePersuasivePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AccountReceivableType = 3 → Se rotula como ''Industria y Comercio''; si AccountReceivableType = 8 → Se rotula como ''Impuesto Predial''; si AccountReceivableType NOT IN (3,8) → Se excluye del resultado', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'VMassivePersuasivePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.AccountReceivable; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'VMassivePersuasivePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'VMassivePersuasivePayment';
GO
