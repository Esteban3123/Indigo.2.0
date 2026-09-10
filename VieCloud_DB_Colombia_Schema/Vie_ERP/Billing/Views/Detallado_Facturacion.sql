

CREATE view [Billing].[Detallado_Facturacion]
AS

SELECT        CASE ing.CODCENATE WHEN '001' THEN 'Tocancipa' END AS Sede, 
                         CASE f.documentType WHEN '1' THEN 'Factura EAPB con Contrato' WHEN '2' THEN 'Factura EAPB Sin Contrato' WHEN '3' THEN 'Factura Particular' WHEN '4' THEN 'Factura Capitada ' WHEN '5' THEN 'Control de Capitacion'
                          WHEN '6' THEN 'Factura Basica' WHEN '7' THEN 'Factura de Venta de Productos' END AS TipoDocumento, cat.Name AS Categoría, F.InvoiceNumber AS Factura, F.AdmissionNumber AS Ingreso, 
                         ing.IFECHAING AS FechaIngreso, 
                         CASE ing.ICAUSAING WHEN '1' THEN 'Heridos_en_combate' WHEN '2' THEN 'Enfermedad_profesional' WHEN '3' THEN 'Enfermedad_gral_adulto' WHEN '4' THEN 'Enfermedad_gral_pediatria' WHEN '5' THEN 'Odontología'
                          WHEN '6' THEN 'Accidente_transito' WHEN '7' THEN 'Catastrofe/Fisalud' WHEN '8' THEN 'Quemados' WHEN '9' THEN 'Maternidad' WHEN '10' THEN 'Accidente_Laboral' WHEN '11' THEN 'Cirugia_Programada' END
                          AS [Causa de Ingreso], CASE ing.TIPOINGRE WHEN '1' THEN 'Ambulatorio' WHEN '2' THEN 'Hospitalario' END AS Tipo_Ingreso, F.PatientCode AS Identificación, P.IPNOMCOMP AS Paciente, 
                         ga.Name AS GrupoAtención, F.TotalInvoice AS TotalFactura, F.InvoiceDate AS [Fecha Factura],F.AnnulmentDate AS FechaAnulación, IIF(f.Status = 1, 'Facturado', 'Anulado') Estado, dos.InvoicedQuantity AS Cantidad, CASE WHEN dq.TotalSalesPrice IS NULL 
                         THEN dos.TotalSalesPrice ELSE dq.TotalSalesPrice END AS ValorUnitario, CASE WHEN dq.TotalSalesPrice IS NULL THEN DF.GrandTotalSalesPrice ELSE dq.TotalSalesPrice END AS ValorTotal, t.Nit, 
                         ea.Code + ' - ' + ea.Name AS [Entidad Administradora], CASE dos.RecordType WHEN '1' THEN 'Servicios' WHEN '2' THEN 'Medicamentos' END AS ServiciosMedicamentos, CASE WHEN pr.Code IS NULL 
                         THEN ServiciosIPS.Code ELSE pr.Code END AS Código, CASE WHEN pr.Name IS NULL THEN ServiciosIPS.Name ELSE pr.Name END AS Descripción, 
						 CASE WHEN PR.POSProduct IS NULL THEN SERVICIOSIPS.POS ELSE PR.POSProduct END AS POSNOPOS,    
                         CASE dos.Presentation WHEN '1' THEN 'No Quirúrgico' WHEN '2' THEN 'Quirúrgico' WHEN '3' THEN 'Paquete' END AS PresentacionServicio, ServiciosIPSQ.Code AS Subcodigo, 
                         ServiciosIPSQ.Name AS Subnombre, CASE WHEN dq.PerformsHealthProfessionalCode IS NULL THEN dos.PerformsHealthProfessionalCode ELSE dq.PerformsHealthProfessionalCode END AS CodigoMèdico, 
                         CASE WHEN rtrim(medqx.NOMMEDICO) IS NULL THEN med.NOMMEDICO ELSE rtrim(medqx.NOMMEDICO) END AS NombreMedico, UF.Code AS UnidadFuncional, UF.Name AS DescripcionUnidadFuncional, 
                         salida.FECALTPAC AS FechaAltaMedica, ing.CODDIAEGR AS CIE10, diag.NOMDIAGNO AS Diagnóstico, CASE WHEN espmed.DESESPECI IS NULL 
                         THEN espqx.DESESPECI ELSE espmed.DESESPECI END AS Especialidad, os.Code AS Orden, os.OrderDate AS FechaOrden, CASE dos.SettlementType WHEN '3' THEN 'Si' ELSE 'No' END AS AplicaProcedimiento,
                          CASE F.IsCutAccount WHEN 'True' THEN 'Si' ELSE 'No' END AS Corte, --per.Fullname AS Usuario, 
						  gf.Name AS [Grupo Facturación], CUPS.Code AS CUPS, CUPS.Description AS [Descripcion CUPS], 
                         BB.UBINOMBRE AS Ubicación, EE.MUNNOMBRE AS Municipio
FROM					 Billing.Invoice AS F WITH (nolock) INNER JOIN
                         Billing.InvoiceDetail AS DF WITH (nolock) ON DF.InvoiceId = F.Id LEFT OUTER JOIN --**INNER JOIN 
                         ---Security.[User] AS u WITH (nolock) ON u.UserCode = F.InvoicedUser LEFT OUTER JOIN --**INNER JOIN 
                         ---Security.Person AS per WITH (nolock) ON per.Id = u.IdPerson INNER JOIN
                         dbo.ADINGRESO AS ing WITH (nolock) ON CAST(ing.NUMINGRES AS int) = F.AdmissionNumber INNER JOIN
                         dbo.INPACIENT AS P WITH (nolock) ON P.IPCODPACI = F.PatientCode INNER JOIN
                         Billing.ServiceOrderDetail AS dos WITH (nolock) ON dos.Id = DF.ServiceOrderDetailId INNER JOIN
                         Common.ThirdParty AS t WITH (nolock) ON t.Id = F.ThirdPartyId INNER JOIN
                         Contract.CareGroup AS ga WITH (nolock) ON ga.Id = F.CareGroupId LEFT OUTER JOIN
                         Contract.HealthAdministrator AS ea WITH (nolock) ON ea.Id = F.HealthAdministratorId INNER JOIN
                         Billing.InvoiceCategories AS cat WITH (nolock) ON cat.Id = F.InvoiceCategoryId LEFT OUTER JOIN
                         Contract.IPSService AS ServiciosIPS WITH (nolock) ON ServiciosIPS.Id = dos.IPSServiceId LEFT OUTER JOIN
                         Inventory.InventoryProduct AS pr WITH (nolock) ON pr.Id = dos.ProductId LEFT OUTER JOIN
                         dbo.INPROFSAL AS med WITH (nolock) ON med.CODPROSAL = dos.PerformsHealthProfessionalCode LEFT OUTER JOIN
                         dbo.INESPECIA AS espmed WITH (nolock) ON espmed.CODESPECI = med.CODESPEC1 LEFT OUTER JOIN
                         dbo.INDIAGNOS AS diag WITH (nolock) ON diag.CODDIAGNO = ing.CODDIAEGR LEFT OUTER JOIN
                         dbo.HCREGEGRE AS salida WITH (nolock) ON CAST(salida.NUMINGRES AS int) = F.AdmissionNumber LEFT OUTER JOIN
                         Billing.ServiceOrderDetailSurgical AS dq WITH (nolock) ON dq.ServiceOrderDetailId = dos.Id AND dq.OnlyMedicalFees = '0' LEFT OUTER JOIN
                         dbo.INPROFSAL AS medqx WITH (nolock) ON medqx.CODPROSAL = dq.PerformsHealthProfessionalCode LEFT OUTER JOIN
                         dbo.INESPECIA AS espqx WITH (nolock) ON espqx.CODESPECI = medqx.CODESPEC1 LEFT OUTER JOIN
                         Contract.IPSService AS ServiciosIPSQ WITH (nolock) ON ServiciosIPSQ.Id = dq.IPSServiceId LEFT OUTER JOIN
                         Payroll.FunctionalUnit AS UF WITH (nolock) ON UF.Id = dos.PerformsFunctionalUnitId LEFT OUTER JOIN
                         Billing.ServiceOrder AS os WITH (nolock) ON os.Id = dos.ServiceOrderId LEFT OUTER JOIN
                         Contract.CUPSEntity AS CUPS WITH (nolock) ON CUPS.Id = dos.CUPSEntityId LEFT OUTER JOIN
                         Billing.BillingGroup AS gf WITH (nolock) ON gf.Id = CUPS.BillingGroupId LEFT OUTER JOIN
                         dbo.INUBICACI AS BB WITH (nolock) ON BB.AUUBICACI = P.AUUBICACI LEFT OUTER JOIN
                         dbo.INMUNICIP AS EE WITH (nolock) ON EE.DEPMUNCOD = BB.DEPMUNCOD
---WHERE   -     (F.Status = '1')  AND (dos.IsDelete = '0') AND dos.SettlementType != 3 AND 
---(CAST(F.InvoiceDate AS date ) between @FechaIni and @FechaFin) 
---AND (dos.IsDelete = '0')
--WHERE f.InvoiceNumber = '1949212'
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el detalle completo de facturación de la institución, integrando encabezado de factura, líneas facturadas (servicios y medicamentos), datos del ingreso o admisión del paciente, información del paciente (nombre, cédula/identificación), entidad administradora de salud (EPS/aseguradora), grupo de atención, categoría y grupo de facturación, profesional de salud que realizó el procedimiento, unidad funcional, diagnóstico de egreso (CIE-10), código CUPS, valores unitarios y totales, estado de la factura (facturado o anulado), fecha de factura y fecha de alta médica, entre otros. Está diseñada para reportería gerencial, auditoría de cuentas médicas, análisis de ingresos por sede, tipo de servicio (servicios vs. medicamentos, POS vs. No POS, quirúrgico vs. no quirúrgico) y conciliación con terceros pagadores. Combina tablas del módulo de facturación (Invoice, InvoiceDetail, ServiceOrderDetail), contratos (CareGroup, HealthAdministrator, IPSService), inventario (InventoryProduct), maestros del ERP legacy (ADINGRESO, INPACIENT, INPROFSAL, INDIAGNOS, HCREGEGRE) y catálogos geográficos, siendo la fuente principal para reportes de cartera, RIPS y análisis de producción por causa de ingreso, tipo de ingreso (ambulatorio, hospitalario) y modalidad de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'Detallado_Facturacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'Detallado_Facturacion';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Una línea de la vista corresponde siempre a un detalle de factura (Billing.InvoiceDetail) ligado a un detalle de orden de servicio (Billing.ServiceOrderDetail) de una factura existente.; Solo se incluyen líneas con ingreso (ADINGRESO) y paciente (INPACIENT) existentes; los joins a estas tablas son INNER.; Cuando existe detalle quirúrgico no anulado (dq.OnlyMedicalFees=''0''), sus valores y profesional priman sobre los del detalle no quirúrgico.; El valor unitario y total de la línea siempre se resuelven con prioridad al componente quirúrgico cuando existe; en caso contrario se usan los valores del detalle de orden y de la factura.; Código y descripción del ítem facturado provienen de Inventory.InventoryProduct si la línea es producto; si no, de Contract.IPSService.; El indicador POS/NO POS se hereda primero del producto de inventario y, en su defecto, del servicio IPS.; La especialidad reportada corresponde al médico no quirúrgico salvo cuando este no tiene especialidad, caso en que se reporta la del médico quirúrgico.; AplicaProcedimiento solo es ''Si'' cuando el tipo de liquidación del detalle es 3.; La sede solo se etiqueta como ''Tocancipa'' para el centro de atención ''001''; cualquier otro código deja la sede nula.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'Detallado_Facturacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Ingreso/admisión; Paciente; Causa de ingreso; Tipo de ingreso (ambulatorio/hospitalario); Entidad administradora (EAPB/EPS); Grupo de atención; Categoría de facturación; Servicio IPS; Medicamento/producto de inventario; POS / NO POS; Servicio quirúrgico vs no quirúrgico; Profesional de la salud y especialidad; Diagnóstico CIE-10; Egreso/alta médica; Orden de servicio; Unidad funcional; CUPS; Grupo de facturación; Cuenta de corte; Sede (Tocancipa); Ubicación/municipio del paciente; Capitación; Liquidación / SettlementType', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'Detallado_Facturacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ing.CODCENATE = ''001'' → Sede se reporta como ''Tocancipa'' else Sede queda NULL (no hay otros casos mapeados); si f.documentType ∈ {1..7} → Mapea a etiquetas: 1=Factura EAPB con Contrato, 2=Factura EAPB Sin Contrato, 3=Factura Particular, 4=Factura Capitada, 5=Control de Capitacion, 6=Factura Basica, 7=Factura de Venta de Productos; si ing.ICAUSAING ∈ {1..11} → Mapea causa de ingreso (Heridos_en_combate, Enfermedad_profesional, Enfermedad_gral_adulto/pediatria, Odontología, Accidente_transito, Catastrofe/Fisalud, Quemados, Maternidad, Accidente_Laboral, Cirugia_Programada); si ing.TIPOINGRE = ''1'' o ''2'' → Tipo de ingreso = ''Ambulatorio'' u ''Hospitalario''; si f.Status = 1 → Estado = ''Facturado'' else Estado = ''Anulado''; si dq.TotalSalesPrice IS NULL (no hay registro quirúrgico) → ValorUnitario = dos.TotalSalesPrice y ValorTotal = DF.GrandTotalSalesPrice else ValorUnitario y ValorTotal toman dq.TotalSalesPrice (prioriza el detalle quirúrgico); si dos.RecordType = ''1'' / ''2'' → Clasifica la línea como ''Servicios'' o ''Medicamentos''; si pr.Code/Name IS NULL (no hay producto de inventario) → Código y Descripción se toman de Contract.IPSService (ServiciosIPS) else Se toman de Inventory.InventoryProduct; si PR.POSProduct IS NULL → POSNOPOS se toma de ServiciosIPS.POS else POSNOPOS = PR.POSProduct; si dos.Presentation ∈ {1,2,3} → Presentación = ''No Quirúrgico'' / ''Quirúrgico'' / ''Paquete''; si dq.PerformsHealthProfessionalCode IS NULL → CódigoMédico y NombreMedico provienen del detalle no quirúrgico (dos / med) else Provienen del detalle quirúrgico (dq / medqx); si espmed.DESESPECI IS NULL → Especialidad se toma de la especialidad quirúrgica (espqx) else Se toma de la especialidad del médico no quirúrgico (espmed); si dos.SettlementType = ''3'' → AplicaProcedimiento = ''Si'' else AplicaProcedimiento = ''No''; si F.IsCutAccount = ''True'' → Corte = ''Si'' else Corte = ''No''', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'Detallado_Facturacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceDetail; dbo.ADINGRESO; dbo.INPACIENT; Billing.ServiceOrderDetail; Common.ThirdParty; Contract.CareGroup; Contract.HealthAdministrator; Billing.InvoiceCategories; Contract.IPSService; Inventory.InventoryProduct; dbo.INPROFSAL; dbo.INESPECIA; dbo.INDIAGNOS; dbo.HCREGEGRE; Billing.ServiceOrderDetailSurgical; Payroll.FunctionalUnit; Billing.ServiceOrder; Contract.CUPSEntity; Billing.BillingGroup; dbo.INUBICACI; dbo.INMUNICIP', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'Detallado_Facturacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'Detallado_Facturacion';
GO
