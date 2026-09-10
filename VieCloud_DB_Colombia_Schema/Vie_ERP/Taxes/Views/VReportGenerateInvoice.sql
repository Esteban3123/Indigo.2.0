

CREATE VIEW [Taxes].[VReportGenerateInvoice]
AS
select ti.InvoiceNumber,
tid.Id,
tp.id as TaId,
Getdate() as DeadLine,
concat(su.UserCode,sp.Fullname) as CreationUser,
ct.Nit,
ct.Name as Tercero,  
tp.Code,
concat('(415)7709998000506(8020)',ti.InvoiceNumber,'(3900)',REPLACE(STR(ti.TaxValue, 10), SPACE(1), '0'),'(96)',convert(varchar,GETDATE(),112)) as BarCode,
SUBSTRING(tp.Code,1,2) as TypeCode,
SUBSTRING(tp.Code,3,4) as Section,
SUBSTRING(tp.Code,5,8) as Block,
concat(tp.LandArea,' Mts2') as LandArea,
Iif(tp.LandArea=0,'0',(tp.LandArea / 10000)) as Hectare,
concat(tp.BuiltArea,' Mts2') as BuildArea,
tp.Addres,
tid.Value,
ti.Validity,
ti.Appraisal,
ti.TaxValue,
0 as Interest,
0 as Total,
tp.Id as IdProperty,
tlc.Code as CodeLiquidationConcept,
concat(tlc.Code,' ',tlc.Name,' ',tid.PercentageConcept,' %') as LiquidationConcept,
0 as PreviousValidity,
concat(ISNULL((select top 1 cast(PercentageDiscount as float) as PercentageDiscount from Portfolio.AccountReceivablePromptPayment WITH (NOLOCK) where AccountReceivableId = par.id and DeadLine >= cast(getdate() as date) order by DeadLine asc),'0'),' %')as PercentageDiscount,
ISNULL((select top 1 (ti.TaxValue * (PercentageDiscount / 100)) as PercentageDiscount from Portfolio.AccountReceivablePromptPayment WITH (NOLOCK) where AccountReceivableId = par.id and DeadLine >= cast(getdate() as date) order by DeadLine asc),0)as VrDscto,
ISNULL((select top 1 (ti.TaxValue) as PercentageDiscount from Portfolio.AccountReceivablePromptPayment WITH (NOLOCK) where AccountReceivableId = par.id and DeadLine >= cast(getdate() as date) order by DeadLine asc),0)as VrBaseDscto
from Taxes.TaxesInvoiceDetail as tid WITH (NOLOCK)
inner join taxes.TaxesLiquidationConcept as tlc WITH (NOLOCK) on tlc.id = tid.LiquidationConceptId
inner join taxes.TaxesInvoice as ti WITH (NOLOCK) on ti.Id = tid.TaxesInvoiceId
inner join taxes.TaxesProperty as tp WITH (NOLOCK) on tp.Id = ti.TaxesPropertyId
inner join Common.ThirdParty as ct WITH (NOLOCK) on ct.id = ti.ThirdPartyId
inner join [Security].[User] as su on su. UserCode = ti.CreationUser
inner join [Security].Person as sp on sp.id = su.IdPerson
inner join Portfolio.AccountReceivable as par WITH (NOLOCK) on par.InvoiceNumber = ti.InvoiceNumber
where par.Balance > 0
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que genera el reporte de facturas de impuesto predial listas para emitir al contribuyente. Integra el detalle de conceptos de liquidación tributaria, los datos del predio (código catastral, área de terreno, área construida, avalúo), la información del tercero contribuyente (NIT, nombre) y el usuario que creó la factura. Incluye el código de barras para pago, el desglose por concepto de liquidación con su porcentaje, y calcula descuentos por pronto pago vigentes consultando las cuentas por cobrar en cartera. Solo muestra facturas con saldo pendiente de cobro (Balance > 0), siendo la fuente principal para la impresión y entrega de facturas prediales a los contribuyentes.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'VIEW', @level1name = N'VReportGenerateInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'VIEW', @level1name = N'VReportGenerateInvoice';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la información necesaria para imprimir/generar la factura de impuestos prediales: datos del predio, contribuyente, conceptos liquidados, código de barras GS1, y descuentos vigentes por pronto pago, restringido a facturas con saldo pendiente.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'VReportGenerateInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada factura de impuestos debe tener detalle, concepto de liquidación, predio y tercero asociados; El usuario creador de la factura debe existir en Security.User con persona asociada en Security.Person; Debe existir una cuenta por cobrar en Portfolio.AccountReceivable cuyo InvoiceNumber coincida con el de la factura tributaria; La cuenta por cobrar debe tener saldo mayor a cero para ser incluida', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'VReportGenerateInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan facturas cuya cuenta por cobrar asociada tiene saldo pendiente (Balance > 0); Solo se consideran descuentos por pronto pago cuya fecha límite no haya vencido a la fecha actual; Cuando existen múltiples pronto pagos vigentes, prevalece el de menor DeadLine (el más próximo a vencer); El código de barras se construye con el estándar GS1 incluyendo AI 415, 8020, 3900 y 96 con la fecha actual y el valor del impuesto formateado a 10 dígitos rellenado con ceros; El código del predio se descompone en TypeCode (2 dígitos), Section (4 dígitos) y Block (8 dígitos); La hectárea se obtiene dividiendo el área de terreno (Mts2) entre 10.000; El usuario de creación se muestra como concatenación de su código y el nombre completo de la persona asociada; La fecha límite (DeadLine) reportada siempre es la fecha actual del sistema; Interest, Total y PreviousValidity siempre se reportan en cero (no calculados en la vista)', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'VReportGenerateInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura de impuestos; Predio (propiedad tributaria); Concepto de liquidación tributaria; Tercero / contribuyente (NIT); Cuenta por cobrar; Pronto pago / descuento por pronto pago; Vigencia y avalúo; Código de barras de factura (GS1); Área de terreno y área construida; Hectáreas', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'VReportGenerateInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve una fila por cada detalle de factura tributaria cuya cuenta por cobrar tiene Balance > 0, enriquecida con datos del predio, tercero, usuario creador, código de barras GS1 y datos de pronto pago vigente', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'VReportGenerateInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si LandArea = 0 → Se reporta hectáreas como ''0'' else Se calcula hectáreas dividiendo el área de terreno entre 10000; si Existe registro de pronto pago vigente (DeadLine >= fecha actual) para la cuenta por cobrar → Se toma el porcentaje de descuento más próximo a vencer y se calcula el valor del descuento sobre el impuesto y la base else Se asignan 0% de descuento, valor de descuento 0 y base de descuento 0', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'VReportGenerateInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Taxes.TaxesInvoiceDetail; Taxes.TaxesLiquidationConcept; Taxes.TaxesInvoice; Taxes.TaxesProperty; Common.ThirdParty; Security.User; Security.Person; Portfolio.AccountReceivable; Portfolio.AccountReceivablePromptPayment', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'VReportGenerateInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'VReportGenerateInvoice';
GO
