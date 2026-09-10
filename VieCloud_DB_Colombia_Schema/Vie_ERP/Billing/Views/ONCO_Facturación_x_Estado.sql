

CREATE                   view [Billing].[ONCO_Facturación_x_Estado]
AS
SELECT        uo.UnitName AS Sede, 
                         CASE f.DocumentType WHEN '1' THEN 'Factura EAPB con Contrato' WHEN '2' THEN 'Factura EAPB Sin Contrato' WHEN '3' THEN 'Factura Particular' WHEN '4'
                          THEN 'Factura Capita' WHEN '5' THEN 'Control Capitacion' WHEN '6' THEN 'Factura Basica' WHEN '7' THEN 'Factura Venta Productos' END AS [TipoDocumento],
                          CASE ing.ICAUSAING WHEN '11' THEN 'Cirugía programada' END AS [CausaIngreso], ing.UFUCODIGO + ' - ' + uf.UFUDESCRI AS [Unidadfuncional], 
                         f.InvoiceNumber AS [NroDocumento], f.AdmissionNumber AS Ingreso, ing.IFECHAING AS [FechaIngreso], eh.FECALTPAC AS [Fechaaltamédica], 
                         ea.Code + ' - ' + ea.Name AS [EntidadAdministradora], t.Nit, t.DigitVerification AS [Dígitoverificación], t.Name AS Entidad, f.PatientCode AS Identificación, 
                         p.IPNOMCOMP AS NombrePaciente, ga.Code + ' - ' + ga.Name AS [Grupo Atención], f.InvoiceDate AS [Fecha Factura], 
                         f.InvoiceExpirationDate AS [Fechavencimiento], f.TotalInvoice AS [VrFactura], f.ThirdPartySalesValue AS [Vr Entidad], 
                         f.ThirdPartyDiscountValue AS [Vr.Descuento], 
                         CASE f.ResponsibleRecoveryFee WHEN '1' THEN 'Ninguno' WHEN '2' THEN 'Paciente' WHEN '3' THEN 'Tercero' END AS [ResponsableCuotaRecuperacion],
                          f.TotalPatientSalesPrice AS [Vrcuotarecuperación], f.PatientDiscount AS [DescuentoCuotarecuperación], f.CashReceiptId AS [ReciboCajaPaciente], 
                         f.PatientPaidValue AS [VrpagadoPaciente], f.ThirdPartyAccountReceivableValue AS [VrCxC], 
                         f.PatientAccountReceivableValue AS [VrCxCgeneradaaPaciente], 
                         CASE f.PatientType WHEN '1' THEN 'Contributivo' WHEN '2' THEN 'Subsidiado' WHEN '3' THEN 'Vinculado' WHEN '4' THEN 'Particular' WHEN '5' THEN 'Otros'
                          WHEN '6' THEN 'Desplazado Contributivo' WHEN '7' THEN 'Desplazado Subsidiado' WHEN '8' THEN 'Desplazado no Asegurado' END AS [TipoPaciente], 
                         f.Observation AS Observaciones, C.Name AS Categoría, CASE f.Status WHEN '1' THEN 'Facturado' WHEN '2' THEN 'Anulado' END AS Estado_Documento, 
                         f.InvoicedUser + ' - ' + per.Fullname AS Usuario, f.InvoicedDate AS Fecha, f.AnnulmentUser + ' - ' + ua.Fullname AS [UsuarioAnula], 
                         f.AnnulmentDate AS [Fechaanulación]
FROM            Billing.Invoice AS f WITH (nolock) LEFT OUTER JOIN
                         dbo.INPACIENT AS p WITH (nolock) ON p.IPCODPACI = f.PatientCode AND f.InvoiceDate >= '30/06/2016 00:00:00' LEFT OUTER JOIN
                         dbo.ADINGRESO AS ing WITH (nolock) ON CAST(ing.NUMINGRES AS int) = f.AdmissionNumber LEFT OUTER JOIN
                         Contract.CareGroup AS ga WITH (nolock) ON ga.Id = f.CareGroupId LEFT OUTER JOIN
                         Contract.HealthAdministrator AS ea WITH (nolock) ON ea.Id = f.HealthAdministratorId LEFT OUTER JOIN
                         Common.ThirdParty AS t WITH (nolock) ON t.Id = f.ThirdPartyId LEFT OUTER JOIN
                         Common.OperatingUnit AS uo WITH (nolock) ON uo.Id = f.OperatingUnitId LEFT OUTER JOIN
                         Security.[User] AS u ON u.UserCode = f.InvoicedUser LEFT OUTER JOIN
                         Security.Person AS per ON per.Id = u.IdPerson LEFT OUTER JOIN
                         Security.[User] AS uc  ON uc.UserCode = f.AnnulmentUser LEFT OUTER JOIN
                         Security.Person AS ua ON ua.Id = uc.IdPerson LEFT OUTER JOIN
                         Billing.InvoiceCategories AS C WITH (nolock) ON C.Id = f.InvoiceCategoryId LEFT OUTER JOIN
                         dbo.INUNIFUNC AS uf WITH (nolock) ON uf.UFUCODIGO = ing.UFUCODIGO LEFT OUTER JOIN
                         dbo.HCREGEGRE AS eh WITH (nolock) ON eh.NUMINGRES = f.AdmissionNumber AND eh.IPCODPACI = f.PatientCode
---WHERE        (f.Status = '1') AND (uo.Id = '14') AND (f.InvoiceDate >= '01/01/2017 00:00:00')

union

SELECT        uo.UnitName AS Sede, 
                         CASE f.DocumentType WHEN '1' THEN 'Factura EAPB con Contrato' WHEN '2' THEN 'Factura EAPB Sin Contrato' WHEN '3' THEN 'Factura Particular' WHEN '4'
                          THEN 'Factura Capita' WHEN '5' THEN 'Control Capitacion' WHEN '6' THEN 'Factura Basica' WHEN '7' THEN 'Factura Venta Productos' END AS [TipoDocumento],
                          CASE ing.ICAUSAING WHEN '11' THEN 'Cirugía programada' END AS [CausaIngreso], ing.UFUCODIGO + ' - ' + uf.UFUDESCRI AS [Unidadfuncional], 
                         f.InvoiceNumber AS [NroDocumento], f.AdmissionNumber AS Ingreso, ing.IFECHAING AS [FechaIngreso], eh.FECALTPAC AS [Fechaaltamédica], 
                         ea.Code + ' - ' + ea.Name AS [EntidadAdministradora], t.Nit, t.DigitVerification AS [Dígitoverificación], t.Name AS Entidad, f.PatientCode AS Identificación, 
                         p.IPNOMCOMP AS NombrePaciente, ga.Code + ' - ' + ga.Name AS [Grupo Atención], f.AnnulmentDate AS [FechaFactura], 
                         f.InvoiceExpirationDate AS [Fechavencimiento], -f.TotalInvoice AS [VrFactura], -f.ThirdPartySalesValue AS [VrEntidad], 
                         -f.ThirdPartyDiscountValue AS [Vr.Descuento], 
                         CASE f.ResponsibleRecoveryFee WHEN '1' THEN 'Ninguno' WHEN '2' THEN 'Paciente' WHEN '3' THEN 'Tercero' END AS [ResponsableCuotaRecuperacion],
                          -f.TotalPatientSalesPrice AS [Vrcuotarecuperación], -f.PatientDiscount AS [DescuentoCuotarecuperación], f.CashReceiptId AS [ReciboCajaPaciente], 
                         -f.PatientPaidValue AS [Vr pagado Paciente], -f.ThirdPartyAccountReceivableValue AS [Vr CxC], 
                         -f.PatientAccountReceivableValue AS [VrCxCgeneradaaPaciente], 
                         CASE f.PatientType WHEN '1' THEN 'Contributivo' WHEN '2' THEN 'Subsidiado' WHEN '3' THEN 'Vinculado' WHEN '4' THEN 'Particular' WHEN '5' THEN 'Otros'
                          WHEN '6' THEN 'Desplazado Contributivo' WHEN '7' THEN 'Desplazado Subsidiado' WHEN '8' THEN 'Desplazado no Asegurado' END AS [Tipo Paciente], 
                         f.Observation AS Observaciones, C.Name AS Categoría, CASE f.Status WHEN '1' THEN 'Facturado' WHEN '2' THEN 'Anulado' END AS Estado_Documento, 
                         f.InvoicedUser + ' - ' + per.Fullname AS Usuario, f.AnnulmentDate AS Fecha, f.AnnulmentUser + ' - ' + ua.Fullname AS [UsuarioAnula], 
                         f.AnnulmentDate AS [Fechaanulación]
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de facturación oncológica por estado que consolida todas las facturas emitidas y anuladas del módulo de oncología, combinando mediante UNION los documentos activos (con sus valores positivos) y los anulados (con sus valores negativos revertidos), para permitir análisis neto por estado del documento. Integra datos del encabezado de factura (número, fecha, tipo de documento, valores cobrados, descuentos, cuota de recuperación, cuentas por cobrar a entidad y paciente), con información del paciente (cédula/identificación, nombre), el ingreso o admisión hospitalaria (número de ingreso, fecha de ingreso, causa de ingreso, unidad funcional), la entidad administradora de salud o EPS (código, nombre, NIT), el grupo de atención del contrato, la sede o unidad operativa donde se facturó, la categoría de factura, y los usuarios que facturaron o anularon el documento. También incorpora la fecha de alta médica del paciente desde la historia clínica de egreso. Es utilizada principalmente para reportes de control y seguimiento de cartera oncológica, conciliación de facturación por estado (facturado vs. anulado), auditoría de documentos y análisis financiero por EPS, sede, tipo de paciente y grupo de atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ONCO_Facturación_x_Estado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ONCO_Facturación_x_Estado';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporte consolidado de facturas oncológicas que muestra cada documento facturado en positivo y, mediante UNION, refleja las facturas anuladas en negativo para neutralizar su valor en informes por estado.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ONCO_Facturación_x_Estado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Billing.Invoice debe contener los registros de facturación con su Status (1=Facturado, 2=Anulado).; Las facturas a cruzar con paciente requieren InvoiceDate >= ''30/06/2016'' (condición fijada en el JOIN con INPACIENT).; AdmissionNumber de Billing.Invoice debe ser convertible a int para emparejar con dbo.ADINGRESO.NUMINGRES.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ONCO_Facturación_x_Estado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las facturas anuladas (Status=''2'') siempre aparecen dos veces: una con valores originales positivos y otra con valores negativos, lo que produce un neto cero al sumar.; En la fila de anulación, las fechas Fecha y FechaFactura se reemplazan por AnnulmentDate.; Solo la causa de ingreso código ''11'' se traduce a texto; el resto queda en NULL.; Todas las consultas usan WITH (NOLOCK) sobre tablas transaccionales, por lo que pueden incluirse lecturas sucias.; El cruce con INPACIENT solo aplica para facturas con InvoiceDate >= 30/06/2016; facturas anteriores no obtienen nombre de paciente.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ONCO_Facturación_x_Estado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Anulación de factura; EAPB / Entidad Administradora de Planes de Beneficios; Contrato de salud; Capitación; Cuota de recuperación; Copago / descuento al paciente; Tipo de paciente (Contributivo, Subsidiado, Vinculado, Desplazado); Ingreso / Admisión hospitalaria; Causa de ingreso (Cirugía programada); Unidad funcional; Egreso hospitalario / Alta médica; Grupo de atención; Sede / Unidad operativa; Cuentas por cobrar (CxC) a tercero y a paciente; Recibo de caja', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ONCO_Facturación_x_Estado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ONCO_Facturación_x_Estado: Para cada factura con cualquier Status devuelve una fila con valores monetarios POSITIVOS y FechaFactura = f.InvoiceDate (primer SELECT del UNION).; [RETURN_RESULT] Billing.ONCO_Facturación_x_Estado: Cuando f.Status=''2'' (Anulado), agrega una fila adicional con TODOS los valores monetarios NEGADOS (-TotalInvoice, -ThirdPartySalesValue, -ThirdPartyDiscountValue, -TotalPatientSalesPrice, -PatientDiscount, -PatientPaidValue, -ThirdPartyAccountReceivableValue, -PatientAccountReceivableValue) y usando AnnulmentDate como FechaFactura y Fecha.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ONCO_Facturación_x_Estado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si f.DocumentType IN (''1''..''7'') → Traduce el código a etiqueta: 1=Factura EAPB con Contrato, 2=Factura EAPB Sin Contrato, 3=Particular, 4=Capita, 5=Control Capitacion, 6=Basica, 7=Venta Productos.; si ing.ICAUSAING = ''11'' → Etiqueta CausaIngreso=''Cirugía programada'' else NULL (no se mapean otras causas); si f.ResponsibleRecoveryFee IN (''1'',''2'',''3'') → Mapea a Ninguno/Paciente/Tercero como responsable de la cuota de recuperación.; si f.PatientType IN (''1''..''8'') → Mapea régimen del paciente: Contributivo, Subsidiado, Vinculado, Particular, Otros, y variantes Desplazado (Contributivo/Subsidiado/no Asegurado).; si f.Status = ''1'' → Estado_Documento=''Facturado''; si f.Status = ''2'' → Estado_Documento=''Anulado'' y se incluye en el segundo SELECT del UNION generando contrapartida con valores negativos.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ONCO_Facturación_x_Estado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; dbo.INPACIENT; dbo.ADINGRESO; Contract.CareGroup; Contract.HealthAdministrator; Common.ThirdParty; Common.OperatingUnit; Security.User; Security.Person; Billing.InvoiceCategories; dbo.INUNIFUNC; dbo.HCREGEGRE', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ONCO_Facturación_x_Estado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ONCO_Facturación_x_Estado';
GO
