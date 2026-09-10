

CREATE VIEW [ViewInternal].[VistaRadicadosConfirmadosAgrupados]
as (
select 
rc.RadicatedConsecutive as CuentaCobro,
t.Nit,
t.Name as Entidad,
case MA.Number when '14090103' then 'Contributivo' when '14090304' then 'Subsidiado' when '14090401' then 'Servicio IPS Privada' when '14090501' then 'Medicina Prepagada' when '14090601' then 'Compañias Aseguradoras' when '14090701' then 'Particulares' when '14090901' then 'Servicio IPS Publicas' when '14091004' then 'Regimen Especial' when '14091102' then 'Vinculados - Departamentos' when '14091103' then 'Vinculados Municipios' when '14091201' then 'Arl Riesgos Profesionales' when '14091403' then 'Accidentes de Transito' when '14090201' then 'Otras Cuentas X Cobrar' end as Regimen,
ic.Code + ' - ' + ic.Name as Categoria,
sum(ar.Value) as Valor,
cast(rc.ConfirmDateSystem as date) as FechaConfirmacion
from Portfolio.RadicateInvoiceC rc
inner join Portfolio.RadicateInvoiceD rd on rd.RadicateInvoiceCId = rc.Id
inner join Portfolio.AccountReceivable ar on ar.InvoiceNumber = rd.InvoiceNumber and ar.AccountReceivableType = 2
inner join Common.ThirdParty t on t.Id = ar.ThirdPartyId
inner join GeneralLedger.MainAccounts ma on ma.Id = ar.AccountWithoutRadicateId
left join Billing.Invoice i on i.InvoiceNumber = ar.InvoiceNumber
left join Billing.InvoiceCategories ic on ic.Id = i.InvoiceCategoryId
where ar.AccountReceivableType = 2 and rc.State = 2  and rd.State = 2
group by rc.RadicatedConsecutive, t.Nit, t.Name, MA.Number, ic.Code, ic.Name, cast(rc.ConfirmDateSystem as date)
)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte que consolida facturas radicadas y confirmadas (estado 2) ante entidades pagadoras, agrupando los valores de cuentas por cobrar por radicado, tercero (NIT/entidad), régimen de salud (derivado del PUC contable) y categoría de facturación. Sirve para el seguimiento y análisis de cobros confirmados en cartera, mostrando el valor total facturado y la fecha de confirmación del sistema por cada combinación de radicado y pagador.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicadosConfirmadosAgrupados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicadosConfirmadosAgrupados';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida los radicados de cobro confirmados, agrupando por cuenta de cobro, tercero, régimen contable y categoría de factura, con el valor total radicado.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicadosConfirmadosAgrupados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El radicado de cabecera debe estar en estado 2 (confirmado); El detalle del radicado debe estar en estado 2 (confirmado); Las cuentas por cobrar deben ser de tipo 2 (radicadas); Cada cuenta por cobrar debe tener un tercero y una cuenta contable principal asociada', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicadosConfirmadosAgrupados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen radicados cuya cabecera y detalle están en estado confirmado (State=2); Solo se consideran cuentas por cobrar de tipo radicado (AccountReceivableType=2); La fecha de confirmación se trunca a fecha (sin hora); El régimen se deriva del código de cuenta contable principal asociada a la cartera, mapeando códigos PUC específicos del grupo 1409 a nombres de régimen de salud; La factura y su categoría son opcionales (LEFT JOIN); si no existen, esos campos quedan en NULL', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicadosConfirmadosAgrupados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Radicado de cobro; Cuenta de cobro; Cuentas por cobrar; Régimen de salud (Contributivo, Subsidiado, Medicina Prepagada, ARL, SOAT, etc.); Categoría de factura; Tercero pagador; Cuenta contable PUC (1409 deudores por servicios de salud); Confirmación de radicado', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicadosConfirmadosAgrupados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve filas agrupadas por consecutivo de radicado, NIT/nombre del tercero, número de cuenta contable, código y nombre de categoría de factura, y fecha de confirmación, sumando el valor de las cuentas por cobrar', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicadosConfirmadosAgrupados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'[; {; "; c; o; n; d; i; t; i; o; n; "; :;  ; "; M; a; i; n; A; c; c; o; u; n; t; s; .; N (+1299 adicionales)', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicadosConfirmadosAgrupados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.RadicateInvoiceC; Portfolio.RadicateInvoiceD; Portfolio.AccountReceivable; Common.ThirdParty; GeneralLedger.MainAccounts; Billing.Invoice; Billing.InvoiceCategories', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicadosConfirmadosAgrupados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicadosConfirmadosAgrupados';
GO
