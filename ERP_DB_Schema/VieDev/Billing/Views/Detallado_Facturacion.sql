







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