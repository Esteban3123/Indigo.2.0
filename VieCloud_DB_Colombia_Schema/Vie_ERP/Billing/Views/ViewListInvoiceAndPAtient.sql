

CREATE VIEW [Billing].[ViewListInvoiceAndPAtient]
AS

select I.Id, I.InvoiceNumber, I.InvoiceDate, I.AdmissionNumber, RTRIM(INP.IPNOMCOMP) AS PatientName, 
I.PatientCode, IC.Code + ' - ' + IC.Name AS InvoiceCategory
from Billing.Invoice AS  I with (nolock)
LEFT OUTER JOIN Billing.InvoiceCategories as IC  with (nolock) ON I.InvoiceCategoryId = IC.Id
left JOIN dbo.INPACIENT AS INP  with (nolock) ON I.PatientCode = INP.IPCODPACI
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada de facturas emitidas junto con los datos del paciente asociado. Combina el encabezado de cada factura (número, fecha, número de ingreso/admisión) con el nombre completo del paciente obtenido del maestro de pacientes, y enriquece cada registro con la categoría de facturación (ej: hospitalización, urgencias, ambulatorio). Se usa para consultas rápidas de facturación donde se necesita identificar al paciente y el tipo de factura sin navegar múltiples tablas, siendo útil en reportes de cobro, búsqueda de facturas por paciente o admisión, y pantallas de listado en el módulo de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListInvoiceAndPAtient';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListInvoiceAndPAtient';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone un listado consolidado de facturas con su número, fecha, admisión, paciente (nombre y código) y categoría formateada, para soportar consultas y búsquedas en el módulo de facturación.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListInvoiceAndPAtient';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda factura se lista aunque no tenga categoría asociada o paciente en el maestro (LEFT JOIN sobre InvoiceCategories e INPACIENT).; El nombre del paciente se entrega sin espacios a la derecha (RTRIM sobre IPNOMCOMP).; La categoría de factura se presenta como concatenación ''Código - Nombre''.; El cruce con el maestro de pacientes se hace por PatientCode = IPCODPACI.; Las consultas se hacen con NOLOCK, por lo que pueden leerse datos no confirmados (lecturas sucias).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListInvoiceAndPAtient';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Paciente; Categoría de factura; Número de admisión', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListInvoiceAndPAtient';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceCategories; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListInvoiceAndPAtient';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListInvoiceAndPAtient';
GO
