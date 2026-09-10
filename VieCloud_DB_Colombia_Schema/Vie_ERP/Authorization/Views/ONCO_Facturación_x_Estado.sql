

CREATE               view [Authorization].[ONCO_Facturación_x_Estado]
AS
SELECT        uo.UnitName AS Sede, 
                         CASE f.DocumentType WHEN '1' THEN 'Factura EAPB con Contrato' WHEN '2' THEN 'Factura EAPB Sin Contrato' WHEN '3' THEN 'Factura Particular' WHEN '4'
                          THEN 'Factura Capita' WHEN '5' THEN 'Control Capitacion' WHEN '6' THEN 'Factura Basica' WHEN '7' THEN 'Factura Venta Productos' END AS [Tipo Documento],
                          CASE ing.ICAUSAING WHEN '11' THEN 'Cirugía programada' END AS [Causa Ingreso], ing.UFUCODIGO + ' - ' + uf.UFUDESCRI AS [Unidad funcional], 
                         f.InvoiceNumber AS [Nro Documento], f.AdmissionNumber AS Ingreso, ing.IFECHAING AS [Fecha Ingreso], eh.FECALTPAC AS [Fecha alta médica], 
                         ea.Code + ' - ' + ea.Name AS [Entidad Administradora], t.Nit, t.DigitVerification AS [Dígito verificación], t.Name AS Entidad, f.PatientCode AS Identificación, 
                         p.IPNOMCOMP AS NombrePaciente, ga.Code + ' - ' + ga.Name AS [Grupo Atención], f.InvoiceDate AS [Fecha Factura], 
                         f.InvoiceExpirationDate AS [Fecha vencimiento], f.TotalInvoice AS [Vr Factura], f.ThirdPartySalesValue AS [Vr Entidad], 
                         f.ThirdPartyDiscountValue AS [Vr. Descuento], 
                         CASE f.ResponsibleRecoveryFee WHEN '1' THEN 'Ninguno' WHEN '2' THEN 'Paciente' WHEN '3' THEN 'Tercero' END AS [Responsable Cuota Recuperacion],
                          f.TotalPatientSalesPrice AS [Vr cuota recuperación], f.PatientDiscount AS [Descuento Cuota recuperación], f.CashReceiptId AS [Recibo Caja Paciente], 
                         f.PatientPaidValue AS [Vr pagado Paciente], f.ThirdPartyAccountReceivableValue AS [Vr CxC], 
                         f.PatientAccountReceivableValue AS [Vr CxC generada a Paciente], 
                         CASE f.PatientType WHEN '1' THEN 'Contributivo' WHEN '2' THEN 'Subsidiado' WHEN '3' THEN 'Vinculado' WHEN '4' THEN 'Particular' WHEN '5' THEN 'Otros'
                          WHEN '6' THEN 'Desplazado Contributivo' WHEN '7' THEN 'Desplazado Subsidiado' WHEN '8' THEN 'Desplazado no Asegurado' END AS [Tipo Paciente], 
                         f.Observation AS Observaciones, C.Name AS Categoría, CASE f.Status WHEN '1' THEN 'Facturado' WHEN '2' THEN 'Anulado' END AS Estado_Documento, 
                         f.InvoicedUser + ' - ' + per.Fullname AS Usuario, f.InvoicedDate AS Fecha, f.AnnulmentUser + ' - ' + ua.Fullname AS [Usuario Anula], 
                         f.AnnulmentDate AS [Fecha anulación]
FROM            Billing.Invoice AS f WITH (nolock) LEFT OUTER JOIN
                         dbo.INPACIENT AS p WITH (nolock) ON p.IPCODPACI = f.PatientCode AND f.InvoiceDate >= '30/06/2016 00:00:00' LEFT OUTER JOIN
                         dbo.ADINGRESO AS ing WITH (nolock) ON CAST(ing.NUMINGRES AS int) = f.AdmissionNumber LEFT OUTER JOIN
                         Contract.CareGroup AS ga WITH (nolock) ON ga.Id = f.CareGroupId LEFT OUTER JOIN
                         Contract.HealthAdministrator AS ea WITH (nolock) ON ea.Id = f.HealthAdministratorId LEFT OUTER JOIN
                         Common.ThirdParty AS t WITH (nolock) ON t.Id = f.ThirdPartyId LEFT OUTER JOIN
                         Common.OperatingUnit AS uo WITH (nolock) ON uo.Id = f.OperatingUnitId LEFT OUTER JOIN
                         Security.[User] AS u ON u.UserCode = f.InvoicedUser LEFT OUTER JOIN
                         Security.Person AS per ON per.Id = u.IdPerson LEFT OUTER JOIN
                         Security.[User] AS uc ON uc.UserCode = f.AnnulmentUser LEFT OUTER JOIN
                         Security.Person AS ua ON ua.Id = uc.IdPerson LEFT OUTER JOIN
                         Billing.InvoiceCategories AS C WITH (nolock) ON C.Id = f.InvoiceCategoryId LEFT OUTER JOIN
                         dbo.INUNIFUNC AS uf WITH (nolock) ON uf.UFUCODIGO = ing.UFUCODIGO LEFT OUTER JOIN
                         dbo.HCREGEGRE AS eh WITH (nolock) ON eh.NUMINGRES = f.AdmissionNumber AND eh.IPCODPACI = f.PatientCode
---WHERE        (f.Status = '1') AND (uo.Id = '14') AND (f.InvoiceDate >= '01/01/2017 00:00:00')

union

SELECT        uo.UnitName AS Sede, 
                         CASE f.DocumentType WHEN '1' THEN 'Factura EAPB con Contrato' WHEN '2' THEN 'Factura EAPB Sin Contrato' WHEN '3' THEN 'Factura Particular' WHEN '4'
                          THEN 'Factura Capita' WHEN '5' THEN 'Control Capitacion' WHEN '6' THEN 'Factura Basica' WHEN '7' THEN 'Factura Venta Productos' END AS [Tipo Documento],
                          CASE ing.ICAUSAING WHEN '11' THEN 'Cirugía programada' END AS [Causa Ingreso], ing.UFUCODIGO + ' - ' + uf.UFUDESCRI AS [Unidad funcional], 
                         f.InvoiceNumber AS [Nro Documento], f.AdmissionNumber AS Ingreso, ing.IFECHAING AS [Fecha Ingreso], eh.FECALTPAC AS [Fecha alta médica], 
                         ea.Code + ' - ' + ea.Name AS [Entidad Administradora], t.Nit, t.DigitVerification AS [Dígito verificación], t.Name AS Entidad, f.PatientCode AS Identificación, 
                         p.IPNOMCOMP AS NombrePaciente, ga.Code + ' - ' + ga.Name AS [Grupo Atención], f.AnnulmentDate AS [Fecha Factura], 
                         f.InvoiceExpirationDate AS [Fecha vencimiento], -f.TotalInvoice AS [Vr Factura], -f.ThirdPartySalesValue AS [Vr Entidad], 
                         -f.ThirdPartyDiscountValue AS [Vr. Descuento], 
                         CASE f.ResponsibleRecoveryFee WHEN '1' THEN 'Ninguno' WHEN '2' THEN 'Paciente' WHEN '3' THEN 'Tercero' END AS [Responsable Cuota Recuperacion],
                          -f.TotalPatientSalesPrice AS [Vr cuota recuperación], -f.PatientDiscount AS [Descuento Cuota recuperación], f.CashReceiptId AS [Recibo Caja Paciente], 
                         -f.PatientPaidValue AS [Vr pagado Paciente], -f.ThirdPartyAccountReceivableValue AS [Vr CxC], 
                         -f.PatientAccountReceivableValue AS [Vr CxC generada a Paciente], 
                         CASE f.PatientType WHEN '1' THEN 'Contributivo' WHEN '2' THEN 'Subsidiado' WHEN '3' THEN 'Vinculado' WHEN '4' THEN 'Particular' WHEN '5' THEN 'Otros'
                          WHEN '6' THEN 'Desplazado Contributivo' WHEN '7' THEN 'Desplazado Subsidiado' WHEN '8' THEN 'Desplazado no Asegurado' END AS [Tipo Paciente], 
                         f.Observation AS Observaciones, C.Name AS Categoría, CASE f.Status WHEN '1' THEN 'Facturado' WHEN '2' THEN 'Anulado' END AS Estado_Documento, 
                         f.InvoicedUser + ' - ' + per.Fullname AS Usuario, f.AnnulmentDate AS Fecha, f.AnnulmentUser + ' - ' + ua.Fullname AS [Usuario Anula], 
                         f.AnnulmentDate AS [Fecha anulación]
FROM            Billing.Invoice AS f WITH (nolock) LEFT OUTER JOIN
                         dbo.INPACIENT AS p WITH (nolock) ON p.IPCODPACI = f.PatientCode AND f.InvoiceDate >= '30/06/2016 00:00:00' LEFT OUTER JOIN
                         dbo.ADINGRESO AS ing WITH (nolock) ON CAST(ing.NUMINGRES AS int) = f.AdmissionNumber LEFT OUTER JOIN
                         Contract.CareGroup AS ga WITH (nolock) ON ga.Id = f.CareGroupId LEFT OUTER JOIN
                         Contract.HealthAdministrator AS ea WITH (nolock) ON ea.Id = f.HealthAdministratorId LEFT OUTER JOIN
                         Common.ThirdParty AS t WITH (nolock) ON t.Id = f.ThirdPartyId LEFT OUTER JOIN
                         Common.OperatingUnit AS uo WITH (nolock) ON uo.Id = f.OperatingUnitId LEFT OUTER JOIN
                         Security.[User] AS u ON u.UserCode = f.InvoicedUser LEFT OUTER JOIN
                         Security.Person AS per ON per.Id = u.IdPerson LEFT OUTER JOIN
                         Security.[User] AS uc ON uc.UserCode = f.AnnulmentUser LEFT OUTER JOIN
                         Security.Person AS ua ON ua.Id = uc.IdPerson LEFT OUTER JOIN
                         Billing.InvoiceCategories AS C WITH (nolock) ON C.Id = f.InvoiceCategoryId LEFT OUTER JOIN
                         dbo.INUNIFUNC AS uf WITH (nolock) ON uf.UFUCODIGO = ing.UFUCODIGO LEFT OUTER JOIN
                         dbo.HCREGEGRE AS eh WITH (nolock) ON eh.NUMINGRES = f.AdmissionNumber AND eh.IPCODPACI = f.PatientCode
WHERE        (f.Status = '2') ---AND (uo.Id = '14') AND (f.InvoiceDate >= '01/01/2017 00:00:00')
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de facturación oncológica por estado que consolida todas las facturas emitidas y anuladas para el programa de oncología, mostrando en una sola consulta el detalle de cada documento de cobro junto con los datos del paciente, el ingreso hospitalario asociado, la entidad administradora de salud (EPS/EAPB), el grupo de atención contratado, la sede donde se prestó el servicio y los valores facturados. Integra información de facturas (Billing.Invoice), datos del paciente (INPACIENT), registros de ingreso y egreso (ADINGRESO, HCREGEGRE), contratos con EPS (HealthAdministrator, CareGroup), terceros pagadores (ThirdParty), unidades operativas o sedes (OperatingUnit) y usuarios de facturación y anulación (Security.User, Security.Person). Combina mediante UNION los registros vigentes con los anulados —estos últimos con valores en negativo— para permitir análisis de facturación neta por estado del documento (facturado o anulado), incluyendo cuotas de recuperación del paciente, cuentas por cobrar a la entidad y al paciente, descuentos, recibos de caja y categorías de factura; útil para reportes de cartera, auditoría de glosas, control de ingresos oncológicos y seguimiento de facturación electrónica por sede y tipo de documento (EAPB con contrato, particular, cápita, venta de productos, entre otros).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ONCO_Facturación_x_Estado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ONCO_Facturación_x_Estado';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único conjunto las facturas vigentes y las anuladas (estas últimas con valores en negativo y fecha de anulación como fecha contable) para reportes de facturación oncológica por sede, tipo de documento y estado.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ONCO_Facturación_x_Estado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las facturas deben existir en Billing.Invoice; los joins a paciente, ingreso, terceros, usuarios y categorías son LEFT OUTER, por lo que faltantes no excluyen filas.; Para enlazar el paciente se exige además que f.InvoiceDate >= ''30/06/2016''; facturas anteriores no traen datos demográficos del paciente.; ing.NUMINGRES debe ser convertible a INT para enlazar con f.AdmissionNumber.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ONCO_Facturación_x_Estado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las facturas anuladas aparecen DOS veces en la vista: una vez con signo positivo (rama 1, sin filtro de Status) y otra con signo negativo (rama 2, Status=''2''), de modo que su neto contable sea cero.; Los valores monetarios de la rama de anulados siempre se expresan con signo invertido respecto a la factura original.; En la rama de anulados, [Fecha Factura], Fecha y [Fecha anulación] son todas iguales a AnnulmentDate.; Estado_Documento solo puede ser ''Facturado'' (Status=1) o ''Anulado'' (Status=2); cualquier otro valor de Status sale como NULL.; La vista no aplica filtros de sede ni de fecha de corte (los WHERE comentados están desactivados).', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ONCO_Facturación_x_Estado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Facturación EAPB; Contrato; Cápita; Cuota de recuperación; Copago/Descuento al paciente; Cuentas por cobrar (CxC); Anulación de factura; Tipo de paciente (régimen); Unidad funcional; Grupo de atención; Sede / Unidad operativa; Ingreso / Admisión; Egreso hospitalario / Alta médica; Causa de ingreso (cirugía programada); Categoría de factura; Tercero pagador / NIT', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ONCO_Facturación_x_Estado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Authorization.ONCO_Facturación_x_Estado: Primer SELECT retorna todas las facturas (sin filtro de Status) con valores monetarios en positivo y Fecha = InvoiceDate.; [RETURN_RESULT] Authorization.ONCO_Facturación_x_Estado: Segundo SELECT (UNION) retorna solo facturas con f.Status = ''2'' (Anulado) invirtiendo el signo de TotalInvoice, ThirdPartySalesValue, ThirdPartyDiscountValue, TotalPatientSalesPrice, PatientDiscount, PatientPaidValue, ThirdPartyAccountReceivableValue y PatientAccountReceivableValue, y usando AnnulmentDate como [Fecha Factura] y Fecha.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ONCO_Facturación_x_Estado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si f.DocumentType IN (''1''..''7'') → Traduce el código a etiqueta: 1=Factura EAPB con Contrato, 2=Factura EAPB Sin Contrato, 3=Factura Particular, 4=Factura Capita, 5=Control Capitacion, 6=Factura Basica, 7=Factura Venta Productos.; si ing.ICAUSAING = ''11'' → Etiqueta Causa Ingreso = ''Cirugía programada'' else NULL para cualquier otro código de causa; si f.ResponsibleRecoveryFee IN (''1'',''2'',''3'') → 1=Ninguno, 2=Paciente, 3=Tercero; si f.PatientType IN (''1''..''8'') → Mapea régimen: Contributivo, Subsidiado, Vinculado, Particular, Otros, Desplazado Contributivo, Desplazado Subsidiado, Desplazado no Asegurado.; si f.Status = ''1'' (rama UNION superior, sin filtro explícito) → Se reportan valores positivos y la fecha contable es InvoiceDate.; si f.Status = ''2'' (rama UNION inferior, WHERE explícito) → Se reportan valores en negativo y la fecha contable es AnnulmentDate (representa el contraasiento de la anulación).', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ONCO_Facturación_x_Estado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; dbo.INPACIENT; dbo.ADINGRESO; Contract.CareGroup; Contract.HealthAdministrator; Common.ThirdParty; Common.OperatingUnit; Security.User; Security.Person; Billing.InvoiceCategories; dbo.INUNIFUNC; dbo.HCREGEGRE', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ONCO_Facturación_x_Estado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ONCO_Facturación_x_Estado';
GO
