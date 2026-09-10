
CREATE VIEW [Portfolio].[VReportPortfolioByAgB]
as

SELECT ROW_NUMBER() OVER (ORDER BY TPAR.Nit) AS Id
	   ,AR.InvoiceNumber as DocumentCode
	   ,TPAR.Nit as ThirdPartyNit
	   ,TPAR.Name as ThirdPartyName
	   ,PAR.IdentificationType as IdentificationType
	   ,TPAR.Nit as CustomerNit
	   ,TPS.Nit as SellerNit
	   ,RADICATE.ConfirmDate as DocumentDate
	   ,RADICATE.ConfirmDate as expiredDate
	   ,ISNULL(RADICATE.ConfirmDate,convert(datetime, '1990-01-01 00:00:00', 120)) as DocumentDateCalculated
	   ,ISNULL(RADICATE.ConfirmDate, getdate()) as expiredDateCalculated
	   ,DATEDIFF(DAY,ISNULL(RADICATE.ConfirmDate, getdate()),getdate()) as Diferencia
	   ,AR.Balance as Balance
	   ,AR.AccountReceivableType as DocumentType
	   ,AR.PortfolioStatus as PortfolioStatus
	   ,AR.NumberShares as NumberShares
	   ,AR.Term as Term
	   ,AR.OpeningBalance as OpeningBalance
	   ,1 as AdvanceOrAccountReceivable
	   ,cg.EntityType as Regimen
	   ,cg.Code as CodeCareGroup
	   ,cg.Name as NameCareGroup
	   ,MA.Number as NumberAccount
	   ,RADICATE.RadicatedConsecutive as RadicatedConsecutive
	   ,RADICATE.RadicatedDate as RadicatedDate
	   ,cast(RADICATE.CreationUser as varchar(20)) RadicatedUser
	   ,case MA.Number when '14090103' then 'Contributivo' when '14090304' then 'Subsidiado' when '14090401' then 'Servicio IPS Privada' when '14090501' then 'Medicina Prepagada' when '14090601' then 'Compañias Aseguradoras' when '14090701' then 'Particulares' when '14090901' then 'Servicio IPS Publicas' when '14091004' then 'Regimen Especial' when '14091102' then 'Vinculados - Departamentos' when '14091103' then 'Vinculados Municipios' when '14091201' then 'Arl Riesgos Profesionales' when '14091403' then 'Accidentes de Transito' when '14090201' then 'Otras Cuentas X Cobrar' end as RegimenCalculated
  FROM  Portfolio.AccountReceivable AR with (nolock)
  inner join Common.ThirdParty TPAR with (nolock) on TPAR.Id = AR.ThirdPartyId 
  inner join Common.Person PAR with (nolock) on PAR.Id=TPAR.PersonId
  left outer join Common.Seller S with (nolock) on S.Id = AR.SellerId 
  left outer join Common.ThirdParty TPS with (nolock) on TPS.Id = S.ThirdPartyId 
  left outer join Billing.Invoice i with (nolock) on i.Id = AR.InvoiceId 
  left outer join [Contract].[CareGroup] cg with (nolock) on cg.Id = i.CareGroupId
  inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = AR.AccountWithoutRadicateId 
  left outer join 
  (select RIC.ConfirmDate, RIC.RadicatedConsecutive, RIC.RadicatedDate, RID.InvoiceNumber, RIC.CreationUser from Portfolio.RadicateInvoiceD RID with (nolock)
  inner Join Portfolio.RadicateInvoiceC RIC with (nolock) on RIC.Id = RID.RadicateInvoiceCId and ISNULL(RID.State,0) = 2 and RIC.State = 2) as RADICATE on RADICATE.InvoiceNumber = ar.InvoiceNumber
  where AR.Balance <> 0 AND AR.Status = 2  and ar.AccountReceivableType in (1,2)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte de cartera por antigüedad de saldos (AgB = Aging Balance). Consolida las cuentas por cobrar pendientes de cobro (facturas y documentos con saldo distinto de cero y estado activo) cruzando el tercero pagador (EPS, aseguradora, empresa) con su vendedor o asesor comercial, la factura de origen, el grupo de atención del contrato y la cuenta contable del plan general de cartera. Incorpora la información de radicación confirmada ante el pagador (fecha de confirmación, consecutivo y fecha de radicado) para calcular la antigüedad del saldo en días desde la fecha de confirmación hasta hoy, y clasifica el régimen de atención según el número de cuenta contable (contributivo, subsidiado, particular, ARL, SOAT, medicina prepagada, entre otros). Sirve para los informes de cartera por edades, seguimiento de cobro, gestión de glosas y análisis de saldos por tipo de pagador y régimen.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'VReportPortfolioByAgB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'VReportPortfolioByAgB';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte de cartera por edades que consolida cuentas por cobrar activas con saldo pendiente, sus datos de tercero, vendedor, factura, grupo de atención, cuenta contable y datos de radicación ante el pagador.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportPortfolioByAgB';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen cuentas por cobrar en Portfolio.AccountReceivable con Status=2 (activas) y Balance distinto de cero.; El tipo de cuenta por cobrar (AccountReceivableType) debe estar en (1,2).; Cada AccountReceivable debe estar asociada a un ThirdParty existente con Person asociada y a una cuenta contable (MainAccounts) en AccountWithoutRadicateId.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportPortfolioByAgB';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan cuentas por cobrar de tipo 1 o 2 con saldo distinto de cero y estado activo (Status=2).; Solo consideran radicaciones cuyo encabezado y detalle estén ambos en State=2 (confirmadas).; El cálculo de días vencidos (Diferencia) usa la fecha de confirmación de radicación o, en su ausencia, la fecha actual (resultando en 0 días).; El régimen textual se deriva exclusivamente del número de la cuenta contable principal (PUC) asociado a la cuenta por cobrar sin radicar.; El Id de la fila es un consecutivo generado por ROW_NUMBER ordenado por NIT del tercero, no es un identificador persistente.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportPortfolioByAgB';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cartera por edades; Cuenta por cobrar; Saldo pendiente; Radicación de facturas; Régimen de afiliación (Contributivo, Subsidiado, Especial, Vinculados); ARL / Riesgos Profesionales; Accidentes de Tránsito (SOAT); Medicina Prepagada; Plan Único de Cuentas (PUC); Tercero / NIT; Vendedor; Grupo de atención (CareGroup); Factura; Anticipo o cuenta por cobrar', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportPortfolioByAgB';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.VReportPortfolioByAgB: Devuelve una fila por cuenta por cobrar activa (Status=2) con Balance<>0 y AccountReceivableType en (1,2), enriquecida con datos del tercero, vendedor, factura, grupo de atención, cuenta contable y datos de radicación.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportPortfolioByAgB';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AR.Balance <> 0 AND AR.Status = 2 AND AR.AccountReceivableType IN (1,2) → La cuenta por cobrar se incluye en el resultado. else Se excluye del reporte.; si RadicateInvoiceD.State = 2 AND RadicateInvoiceC.State = 2 y RIC.Id = RID.RadicateInvoiceCId → Se traen datos de radicación (ConfirmDate, RadicatedConsecutive, RadicatedDate, CreationUser) emparejados por InvoiceNumber. else Los campos de radicación quedan en NULL (LEFT JOIN).; si MA.Number coincide con un código PUC predefinido (14090103, 14090304, 14090401, 14090501, 14090601, 14090701, 14090901, 14091004, 14091102, 14091103, 14091201, 14091403, 14090201) → Se asigna la etiqueta de régimen correspondiente (Contributivo, Subsidiado, Servicio IPS Privada, Medicina Prepagada, Compañías Aseguradoras, Particulares, Servicio IPS Públicas, Régimen Especial, Vinculados Departamentos/Municipios, ARL, Accidentes de Tránsito, Otras Cuentas X Cobrar). else RegimenCalculated queda en NULL.; si RADICATE.ConfirmDate IS NULL → DocumentDateCalculated toma ''1990-01-01'' y expiredDateCalculated toma GETDATE(); la diferencia de días se calcula contra GETDATE() (Diferencia = 0). else Se usan las fechas reales de confirmación de radicación para el cálculo de edades de cartera.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportPortfolioByAgB';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.AccountReceivable; Common.ThirdParty; Common.Person; Common.Seller; Billing.Invoice; Contract.CareGroup; GeneralLedger.MainAccounts; Portfolio.RadicateInvoiceD; Portfolio.RadicateInvoiceC', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportPortfolioByAgB';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportPortfolioByAgB';
GO
