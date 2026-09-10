

CREATE               view [Authorization].[ONCO_Fact-Reg-Conta]
AS

SELECT                  ---CASE ing.CODCENATE WHEN '001' THEN 'CLINICA NUEVA' END AS Sede, 
                         CASE f.documentType WHEN '1' THEN 'Factura EAPB con Contrato' WHEN '2' THEN 'Factura EAPB Sin Contrato' WHEN '3' THEN 'Factura Particular' WHEN '4' THEN 'Factura Capitada ' WHEN '5' THEN 'Control de Capitacion'
                          WHEN '6' THEN 'Factura Basica' WHEN '7' THEN 'Factura de Venta de Productos' END AS TipoDocumento, --cat.Name AS Categoría, 
						  F.InvoiceNumber AS Factura, F.AdmissionNumber AS Ingreso, 
                         ing.IFECHAING AS FechaIngreso, 
                         --CASE ing.ICAUSAING WHEN '1' THEN 'Heridos_en_combate' WHEN '2' THEN 'Enfermedad_profesional' WHEN '3' THEN 'Enfermedad_gral_adulto' WHEN '4' THEN 'Enfermedad_gral_pediatria' WHEN '5' THEN 'Odontología'
                          ---WHEN '6' THEN 'Accidente_transito' WHEN '7' THEN 'Catastrofe/Fisalud' WHEN '8' THEN 'Quemados' WHEN '9' THEN 'Maternidad' WHEN '10' THEN 'Accidente_Laboral' WHEN '11' THEN 'Cirugia_Programada' END
                          ---AS [Causa de Ingreso], 
						  ---CASE ing.TIPOINGRE WHEN '1' THEN 'Ambulatorio' WHEN '2' THEN 'Hospitalario' END AS Tipo_Ingreso, 
						  F.PatientCode AS Identificación, P.IPNOMCOMP AS Paciente, 
                         ga.Name AS GrupoAtención, F.TotalInvoice AS TotalFactura, F.InvoiceDate AS [Fecha Factura],F.AnnulmentDate AS FechaAnulación, IIF(f.Status = 1, 'Facturado', 'Anulado') Estado, dos.InvoicedQuantity AS Cantidad, CASE WHEN dq.TotalSalesPrice IS NULL 
                         THEN dos.TotalSalesPrice ELSE dq.TotalSalesPrice END AS ValorUnitario, CASE WHEN dq.TotalSalesPrice IS NULL THEN DF.GrandTotalSalesPrice ELSE dq.TotalSalesPrice END AS ValorTotal, t.Nit, 
                         ea.Code + ' - ' + ea.Name AS [Entidad Administradora], CASE dos.RecordType WHEN '1' THEN 'Servicios' WHEN '2' THEN 'Medicamentos' END AS ServiciosMedicamentos, CASE WHEN pr.Code IS NULL 
                         THEN ServiciosIPS.Code ELSE pr.Code END AS Código, CASE WHEN pr.Name IS NULL THEN ServiciosIPS.Name ELSE pr.Name END AS Descripción, 
						 CASE WHEN PR.POSProduct IS NULL THEN SERVICIOSIPS.POS ELSE PR.POSProduct END AS POSNOPOS,    
                         ---CASE dos.Presentation WHEN '1' THEN 'No Quirúrgico' WHEN '2' THEN 'Quirúrgico' WHEN '3' THEN 'Paquete' END AS PresentacionServicio, ServiciosIPSQ.Code AS Subcodigo, 
                         ----ServiciosIPSQ.Name AS Subnombre, CASE WHEN dq.PerformsHealthProfessionalCode IS NULL THEN dos.PerformsHealthProfessionalCode ELSE dq.PerformsHealthProfessionalCode END AS CodigoMèdico, 
                         --CASE WHEN rtrim(medqx.NOMMEDICO) IS NULL THEN med.NOMMEDICO ELSE rtrim(medqx.NOMMEDICO) END AS NombreMedico, 
						 UF.Code AS UnidadFuncional, UF.Name AS DescripcionUnidadFuncional, 
                         ---salida.FECALTPAC AS FechaAltaMedica, ing.CODDIAEGR AS CIE10, diag.NOMDIAGNO AS Diagnóstico, CASE WHEN espmed.DESESPECI IS NULL 
                         ---THEN espqx.DESESPECI ELSE espmed.DESESPECI END AS Especialidad, 
						 os.Code AS Orden, dos.Id AS IDORD,os.OrderDate AS FechaOrden, CASE dos.SettlementType WHEN '3' THEN 'Si' ELSE 'No' END AS AplicaProcedimiento,
                          CASE F.IsCutAccount WHEN 'True' THEN 'Si' ELSE 'No' END AS Corte, ----per.Fullname AS Usuario, gf.Name AS [Grupo Facturación], 
						  CASE WHEN DQ.ID IS NULL THEN PUC.Number ELSE PUC2.Number END AS NroCuenta, CASE WHEN DQ.ID IS NULL THEN PUC.NAME ELSE PUC2.NAME END AS NombreCuenta, CC.Code AS CodCC, cc.Name as CCNombre
						  ---, CUPS.Code AS CUPS, CUPS.Description AS [Descripcion CUPS], 
                         ----BB.UBINOMBRE AS Ubicación, EE.MUNNOMBRE AS Municipio
FROM            Billing.Invoice AS F WITH (nolock) INNER JOIN
                         Billing.InvoiceDetail AS DF WITH (nolock) ON DF.InvoiceId = F.Id LEFT OUTER JOIN --**INNER JOIN 
                         Security.[User] AS u ON u.UserCode = F.InvoicedUser LEFT OUTER JOIN --**INNER JOIN 
                         Security.Person AS per ON per.Id = u.IdPerson INNER JOIN
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
                         dbo.INMUNICIP AS EE WITH (nolock) ON EE.DEPMUNCOD = BB.DEPMUNCOD INNER JOIN
						 GeneralLedger.MainAccounts AS PUC WITH (nolock) ON PUC.ID = DOS.IncomeMainAccountId  LEFT  JOIN
						 GeneralLedger.MainAccounts AS PUC2 WITH (nolock) ON puc2.id = dq.IncomeMainAccountId  INNER JOIN
						 Payroll.CostCenter AS CC WITH (nolock) ON CC.Id = DOS.CostCenterId
---WHERE f.InvoiceNumber = '1973637'
---' and puc.Number in ('4115152035')---, '4115151015','4115150205')
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de facturación oncológica que consolida el registro contable y de facturación para el módulo de oncología. Integra encabezados y detalles de facturas (Billing.Invoice y Billing.InvoiceDetail) con los ingresos o admisiones del paciente (ADINGRESO), datos del paciente (INPACIENT), órdenes de servicio (ServiceOrderDetail), tercero pagador (ThirdParty), grupo de atención del contrato (CareGroup) y entidad administradora de salud o EPS (HealthAdministrator). Por cada línea facturada expone: tipo de documento (factura EAPB con contrato, particular, capitada, etc.), número de factura, ingreso, fecha de ingreso, identificación y nombre del paciente, grupo de atención, total facturado, fecha y estado de la factura o anulación, cantidad, valor unitario y total del ítem, NIT del tercero pagador, entidad administradora, clasificación entre servicios y medicamentos, código y descripción del servicio o producto (CUPS o inventario), indicador POS/No POS, unidad funcional, número y fecha de la orden, número y nombre de cuenta contable (PUC) y centro de costo. Sirve como fuente principal para reportes de auditoría, glosas, liquidación y control contable de la facturación oncológica, cruzando información de cobro, contratos y datos clínico-administrativos del episodio de atención.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ONCO_Fact-Reg-Conta';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ONCO_Fact-Reg-Conta';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida facturación oncológica con datos contables (cuenta PUC, centro de costo), administrativos (paciente, ingreso, EAPB, grupo de atención) y de servicio/medicamento, distinguiendo líneas quirúrgicas de no quirúrgicas para análisis de ingresos.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ONCO_Fact-Reg-Conta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las facturas deben existir en Billing.Invoice con detalle en Billing.InvoiceDetail.; Cada InvoiceDetail debe referenciar un ServiceOrderDetail válido.; Invoice.AdmissionNumber debe corresponder a un ADINGRESO.NUMINGRES convertible a entero.; Invoice.PatientCode debe existir en INPACIENT.; Cada ServiceOrderDetail debe tener IncomeMainAccountId válido en GeneralLedger.MainAccounts y CostCenterId válido en Payroll.CostCenter.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ONCO_Fact-Reg-Conta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen facturas que tengan al menos un detalle (InvoiceDetail) y una orden de servicio detallada (ServiceOrderDetail) asociada vía DF.ServiceOrderDetailId.; El ingreso (ADINGRESO) se enlaza convirtiendo NUMINGRES a entero para coincidir con Invoice.AdmissionNumber, por lo que se asume que AdmissionNumber es numérico.; Se exigen relaciones obligatorias (INNER JOIN) con paciente (INPACIENT), tercero (ThirdParty), grupo de atención (CareGroup), categoría de factura (InvoiceCategories), cuenta contable de ingreso del detalle (MainAccounts vía IncomeMainAccountId) y centro de costo (CostCenter); sin estas, la línea no aparece.; El detalle quirúrgico solo se considera cuando OnlyMedicalFees = ''0'' (no son honorarios médicos exclusivos).; Cuando existe detalle quirúrgico válido, sus valores y cuenta contable prevalecen sobre los del detalle de orden estándar.; La administradora de salud (EAPB) es opcional: facturas particulares u otras pueden no tenerla (LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ONCO_Fact-Reg-Conta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve un conjunto de filas por cada línea de InvoiceDetail facturada, enriquecida con etiquetas de negocio (tipo de documento, estado, POS/NO POS, servicios vs medicamentos, aplica procedimiento, corte) y datos contables (cuenta PUC y centro de costo).', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ONCO_Fact-Reg-Conta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Billing.Invoice.documentType ∈ {1..7} → Se traduce a etiqueta de tipo de documento (''Factura EAPB con Contrato'', ''Factura EAPB Sin Contrato'', ''Factura Particular'', ''Factura Capitada'', ''Control de Capitacion'', ''Factura Basica'', ''Factura de Venta de Productos''); si Billing.Invoice.Status = 1 → Se reporta como ''Facturado'' else Se reporta como ''Anulado''; si ServiceOrderDetailSurgical.TotalSalesPrice IS NULL → Se toma ValorUnitario y ValorTotal desde ServiceOrderDetail / InvoiceDetail.GrandTotalSalesPrice else Se toma ValorUnitario y ValorTotal desde el detalle quirúrgico (ServiceOrderDetailSurgical.TotalSalesPrice); si ServiceOrderDetail.RecordType = 1 vs 2 → Clasifica la línea como ''Servicios'' (1) o ''Medicamentos'' (2); si InventoryProduct.Code IS NULL → Usa Code y Name desde Contract.IPSService (ServiciosIPS) else Usa Code y Name desde Inventory.InventoryProduct; si InventoryProduct.POSProduct IS NULL → Usa POS desde Contract.IPSService else Usa POSProduct desde Inventory.InventoryProduct; si ServiceOrderDetail.SettlementType = 3 → AplicaProcedimiento = ''Si'' else AplicaProcedimiento = ''No''; si Invoice.IsCutAccount = ''True'' → Corte = ''Si'' else Corte = ''No''; si ServiceOrderDetailSurgical.ID IS NULL → NroCuenta y NombreCuenta se toman de la cuenta principal asociada a ServiceOrderDetail.IncomeMainAccountId (PUC) else Se toman de la cuenta principal asociada a ServiceOrderDetailSurgical.IncomeMainAccountId (PUC2)', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ONCO_Fact-Reg-Conta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceDetail; Security.User; Security.Person; dbo.ADINGRESO; dbo.INPACIENT; Billing.ServiceOrderDetail; Common.ThirdParty; Contract.CareGroup; Contract.HealthAdministrator; Billing.InvoiceCategories; Contract.IPSService; Inventory.InventoryProduct; dbo.INPROFSAL; dbo.INESPECIA; dbo.INDIAGNOS; dbo.HCREGEGRE; Billing.ServiceOrderDetailSurgical; Payroll.FunctionalUnit; Billing.ServiceOrder; Contract.CUPSEntity; Billing.BillingGroup; dbo.INUBICACI; dbo.INMUNICIP; GeneralLedger.MainAccounts; Payroll.CostCenter', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ONCO_Fact-Reg-Conta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ONCO_Fact-Reg-Conta';
GO
