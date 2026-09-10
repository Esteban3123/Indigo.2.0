
CREATE VIEW [dbo].[VIE_AD_Billing_Pendiente_Causacion_Kta]
AS
SELECT        'Bogota' AS Sede, 
                         CASE f.documentType WHEN '1' THEN 'Factura EAPB con Contrato' WHEN '2' THEN 'Factura EAPB Sin Contrato' WHEN '3' THEN 'Factura Particular' WHEN '4' THEN 'Factura Capitada ' WHEN '5' THEN 'Control de Capitacion' WHEN
                          '6' THEN 'Factura Basica' WHEN '7' THEN 'Factura de Venta de Productos' END AS [Tipo de documento], cat.Name AS Categoría, F.InvoiceNumber AS [Nro Factura/Registro], F.AdmissionNumber AS Ingreso, 
                         ing.IFECHAING AS [Fecha ingreso], F.PatientCode AS Identificación, p.IPNOMCOMP AS Paciente, ga.Name AS [Grupo de Atención], F.TotalInvoice AS [Total Factura/Registro], F.InvoiceDate AS [Fecha Factura/Registro], 
                         dos.InvoicedQuantity AS Cantidad, CASE WHEN dq.TotalSalesPrice IS NULL THEN dos.TotalSalesPrice ELSE dq.TotalSalesPrice END AS ValorUnitario, CASE WHEN dq.TotalSalesPrice IS NULL 
                         THEN DF.GrandTotalSalesPrice ELSE dq.TotalSalesPrice END AS ValorTotal, t.Nit, ea.Code + ' - ' + ea.Name AS [Entidad Administradora], 
                         CASE dos.RecordType WHEN '1' THEN 'Servicios' WHEN '2' THEN 'Medicamentos' END AS [Servicios/Medicamentos], CASE WHEN pr.Code IS NULL THEN ServiciosIPS.Code ELSE pr.Code END AS Código, 
                         CASE WHEN pr.Name IS NULL THEN ServiciosIPS.Name ELSE pr.Name END AS Descripción, 
                         CASE dos.Presentation WHEN '1' THEN 'No Quirúrgico' WHEN '2' THEN 'Quirúrgico' WHEN '3' THEN 'Paquete' END AS [Presentación Servicio], ServiciosIPSQ.Code AS Subcodigo, ServiciosIPSQ.Name AS Subnombre, 
                         CASE WHEN dq.PerformsHealthProfessionalCode IS NULL THEN dos.PerformsHealthProfessionalCode ELSE dq.PerformsHealthProfessionalCode END AS CodigoMèdico, CASE WHEN rtrim(medqx.NOMMEDICO) IS NULL 
                         THEN med.NOMMEDICO ELSE rtrim(medqx.NOMMEDICO) END AS NombreMedico, UF.Name AS [Descripción Unidad Funcional], salida.FECALTPAC AS [Fecha Alta médica], ing.CODDIAEGR AS CIE10, 
                         diag.NOMDIAGNO AS Diagnóstico, CASE WHEN espmed.DESESPECI IS NULL THEN espqx.DESESPECI ELSE espmed.DESESPECI END AS Especialidad, per.Fullname AS Usuario
FROM            Billing.Invoice AS F WITH (nolock) INNER JOIN
                         Billing.InvoiceDetail AS DF WITH (nolock) ON DF.InvoiceId = F.Id INNER JOIN
                         dbo.ADINGRESO AS ing WITH (nolock) ON CAST(ing.NUMINGRES AS int) = F.AdmissionNumber INNER JOIN
                         Billing.ServiceOrderDetail AS dos WITH (nolock) ON dos.Id = DF.ServiceOrderDetailId INNER JOIN
                         dbo.INPACIENT AS p WITH (nolock) ON p.IPCODPACI = F.PatientCode INNER JOIN
                         Common.ThirdParty AS t WITH (nolock) ON t.Id = F.ThirdPartyId INNER JOIN
                         Common.OperatingUnit AS uo WITH (nolock) ON uo.Id = F.OperatingUnitId INNER JOIN
                         Contract.CareGroup AS ga WITH (nolock) ON ga.Id = F.CareGroupId LEFT OUTER JOIN
                         Contract.HealthAdministrator AS ea WITH (nolock) ON ea.Id = F.HealthAdministratorId LEFT OUTER JOIN
                         Security.[User] AS u ON u.UserCode = F.InvoicedUser LEFT OUTER JOIN
                         Security.Person AS per ON per.Id = u.IdPerson LEFT OUTER JOIN
                         Billing.InvoiceCategories AS cat WITH (nolock) ON cat.Id = F.InvoiceCategoryId LEFT OUTER JOIN
                         Contract.IPSService AS ServiciosIPS WITH (nolock) ON ServiciosIPS.Id = dos.IPSServiceId LEFT OUTER JOIN
                         Inventory.InventoryProduct AS pr WITH (nolock) ON pr.Id = dos.ProductId LEFT OUTER JOIN
                         dbo.INPROFSAL AS med WITH (nolock) ON med.CODPROSAL = dos.PerformsHealthProfessionalCode LEFT OUTER JOIN
                         dbo.INESPECIA AS espmed WITH (nolock) ON espmed.CODESPECI = med.CODESPEC1 LEFT OUTER JOIN
                         dbo.INDIAGNOS AS diag ON diag.CODDIAGNO = ing.CODDIAEGR LEFT OUTER JOIN
                         dbo.HCREGEGRE AS salida WITH (nolock) ON CAST(salida.NUMINGRES AS int) = F.AdmissionNumber LEFT OUTER JOIN
                         Billing.ServiceOrderDetailSurgical AS dq WITH (nolock) ON dq.ServiceOrderDetailId = dos.Id AND dq.OnlyMedicalFees = '0' LEFT OUTER JOIN
                         dbo.INPROFSAL AS medqx WITH (nolock) ON medqx.CODPROSAL = dq.PerformsHealthProfessionalCode LEFT OUTER JOIN
                         dbo.INESPECIA AS espqx ON espqx.CODESPECI = medqx.CODESPEC1 LEFT OUTER JOIN
                         Contract.IPSService AS ServiciosIPSQ WITH (nolock) ON ServiciosIPSQ.Id = dq.IPSServiceId LEFT OUTER JOIN
                         Payroll.FunctionalUnit AS UF WITH (nolock) ON UF.Id = dos.PerformsFunctionalUnitId
WHERE        (uo.Id = '2') AND (F.Status = '1') AND (DF.ServiceOrderDetailId NOT IN
                             (SELECT        ServiceOrderDetailId
                               FROM            MedicalFees.MedicalFeesCausation)) AND (dos.RecordType IN ('1')) AND (F.InvoiceDate > '30/06/2019 23:59:59')
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte para la sede Bogotá (unidad operativa Id=2) que consolida servicios facturados (solo tipo RecordType=1) con estado activo (Status=1), emitidos después del 30/06/2019, cuya orden de servicio aún **no ha sido causada** en honorarios médicos (excluye registros presentes en `MedicalFeesCausation`). Permite identificar las líneas de facturación pendientes de causación contable, incluyendo datos del paciente, entidad administradora, médico tratante, diagnóstico de egreso y valores facturados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Billing_Pendiente_Causacion_Kta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Billing_Pendiente_Causacion_Kta';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista facturas activas de la sede Bogotá emitidas después de junio/2019 cuyas líneas de servicios aún no han sido causadas en honorarios médicos, para gestionar la causación pendiente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Billing_Pendiente_Causacion_Kta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La unidad operativa filtrada debe existir con Id=''2'' (sede Bogotá).; Las facturas deben tener Status=''1'' (activas/vigentes).; Las líneas deben corresponder a RecordType=''1'' (Servicios, no medicamentos).; Las facturas deben tener fecha posterior al 30/06/2019 23:59:59.; AdmissionNumber de la factura debe poder convertirse a int para enlazar con ADINGRESO y HCREGEGRE.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Billing_Pendiente_Causacion_Kta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone facturas de la sede con OperatingUnit Id=''2'' (Bogotá, etiqueta fija).; Excluye líneas que ya tengan registro en MedicalFees.MedicalFeesCausation.; Solo considera líneas de servicios (RecordType=''1''), nunca medicamentos.; Solo considera facturas con Status=''1''.; Solo considera facturas posteriores al 30/06/2019.; Para cirugías, solo toma honorarios donde OnlyMedicalFees=''0''.; Prioriza información quirúrgica sobre la no quirúrgica cuando ambas existen.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Billing_Pendiente_Causacion_Kta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Facturación; Causación de honorarios médicos; EAPB (Entidad Administradora de Planes de Beneficios); Contrato y capitación; Ingreso/Admisión hospitalaria; Paciente; Grupo de atención; Servicios vs Medicamentos; Procedimiento quirúrgico vs no quirúrgico; Profesional de la salud / Especialidad; Diagnóstico CIE10; Alta médica; Unidad funcional; Sede (Bogotá)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Billing_Pendiente_Causacion_Kta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] VIE_AD_Billing_Pendiente_Causacion_Kta: Cuando uo.Id=''2'' AND F.Status=''1'' AND dos.RecordType=''1'' AND F.InvoiceDate>''30/06/2019 23:59:59'' AND DF.ServiceOrderDetailId NOT IN MedicalFees.MedicalFeesCausation, entonces se retorna la línea de factura como pendiente de causación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Billing_Pendiente_Causacion_Kta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si f.documentType en 1..7 → Traduce el código a etiqueta legible (Factura EAPB con/sin Contrato, Particular, Capitada, Control de Capitación, Básica, Venta de Productos).; si dq.TotalSalesPrice IS NULL (no hay detalle quirúrgico) → Toma valores unitario/total y profesional desde ServiceOrderDetail (dos) else Toma valores y profesional desde ServiceOrderDetailSurgical (dq); si pr.Code IS NULL (no es producto de inventario) → Reporta código y nombre desde IPSService else Reporta código y nombre desde InventoryProduct; si medqx.NOMMEDICO IS NULL → Usa el médico de dos (med) else Usa el médico quirúrgico (medqx); si espmed.DESESPECI IS NULL → Usa la especialidad del médico quirúrgico else Usa la especialidad del médico no quirúrgico; si dos.RecordType=''1'' → Etiqueta como ''Servicios''; si dos.Presentation 1/2/3 → Etiqueta como No Quirúrgico / Quirúrgico / Paquete', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Billing_Pendiente_Causacion_Kta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceDetail; dbo.ADINGRESO; Billing.ServiceOrderDetail; dbo.INPACIENT; Common.ThirdParty; Common.OperatingUnit; Contract.CareGroup; Contract.HealthAdministrator; Security.User; Security.Person; Billing.InvoiceCategories; Contract.IPSService; Inventory.InventoryProduct; dbo.INPROFSAL; dbo.INESPECIA; dbo.INDIAGNOS; dbo.HCREGEGRE; Billing.ServiceOrderDetailSurgical; Payroll.FunctionalUnit; MedicalFees.MedicalFeesCausation', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Billing_Pendiente_Causacion_Kta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Billing_Pendiente_Causacion_Kta';
GO
