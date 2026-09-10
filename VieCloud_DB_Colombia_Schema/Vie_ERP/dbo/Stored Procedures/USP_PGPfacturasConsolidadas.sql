CREATE PROCEDURE [dbo].[USP_PGPfacturasConsolidadas] @FInicial AS DATETIME, 
                                                    @FFinal AS   DATETIME
AS
     WITH FactuUCI
          AS (SELECT DISTINCT 
                     DATENAME(MONTH, i.InvoiceDate) + '/' + CAST(DATEPART(YEAR, i.InvoiceDate) AS VARCHAR) AS Mes, 
                     i.InvoiceNumber AS Factura, 
                     i.AdmissionNumber AS Ingreso, 
                     cg.Name AS EntidadAdministradora, 
                     i.TotalInvoice AS TotalFactura, 
                     (i.ThirdPartySalesValue) AS TotalEntidad,
                     CASE
                         WHEN cg.Name NOT LIKE '%PGP%'
                         THEN 'NO APLICA'
                         ELSE 'UCI'
                     END AS 'TipoPGP'
              FROM Billing.Invoice AS i
                   INNER JOIN dbo.ADINGRESO AS ad ON ad.NUMINGRES = i.AdmissionNumber
                   INNER JOIN dbo.INPACIENT AS pa ON ad.IPCODPACI = pa.IPCODPACI
                   INNER JOIN.INUBICACI AS ubi ON PA.AUUBICACI = UBI.AUUBICACI
                   INNER JOIN.INMUNICIP AS mun ON ubi.DEPMUNCOD = MUN.DEPMUNCOD
                   INNER JOIN Contract.CareGroup AS cg ON cg.Id = i.CareGroupId
                   INNER JOIN Security.[User] AS u ON u.UserCode = i.InvoicedUser
                   LEFT OUTER JOIN Contract.HealthAdministrator AS ha ON ha.Id = i.HealthAdministratorId
                   INNER JOIN Security.[User] AS Us ON i.InvoicedUser = Us.UserCode
                   INNER JOIN Billing.InvoiceCategories AS Cat ON i.InvoiceCategoryId = Cat.Id
                                                                  AND i.InvoiceCategoryId = Cat.Id
                                                                  AND i.InvoiceCategoryId = Cat.Id
                   INNER JOIN Common.ThirdParty AS ter ON ha.ThirdPartyId = ter.Id
                   INNER JOIN Security.Person AS Pe ON Us.IdPerson = Pe.Id
                   LEFT OUTER JOIN dbo.CHREGEGRE AS EGR ON ad.NUMINGRES = EGR.NUMINGRES
                   INNER JOIN dbo.INDIAGNOS AS dx ON ad.CODDIAEGR = dx.CODDIAGNO
                   INNER JOIN dbo.HCREGEGRE AS eg ON ad.NUMINGRES = eg.NUMINGRES
                   INNER JOIN dbo.INPROFSAL AS ps ON eg.CODPROSAL = ps.CODPROSAL
                   INNER JOIN dbo.INESPECIA AS esp ON PS.CODESPEC1 = esp.CODESPECI
              WHERE(i.InvoiceDate >= @FInicial
                    AND i.InvoiceDate < @FFinal)
                   AND (ter.Nit = '900935126')
                   AND Cat.Code IN('03', '04', '05', '08')
                   AND i.STATUS = '1'),
          FactuPartos
          AS (SELECT DISTINCT 
                     DATENAME(MONTH, i.InvoiceDate) + '/' + CAST(DATEPART(YEAR, i.InvoiceDate) AS VARCHAR) AS Mes, 
                     i.InvoiceNumber AS Factura, 
                     i.AdmissionNumber AS Ingreso, 
                     cg.Name AS EntidadAdministradora, 
                     i.TotalInvoice AS TotalFactura, 
                     (i.ThirdPartySalesValue) AS TotalEntidad,
                     CASE
                         WHEN cg.Name NOT LIKE '%PGP%'
                         THEN 'NO APLICA'
                         WHEN qx.TERTRAPAR LIKE '%CESAREA%'
                         THEN 'CESAREA'
                         ELSE 'PARTO NORMAL'
                     END AS 'TipoPGP'
              FROM Billing.Invoice AS i
                   INNER JOIN dbo.ADINGRESO AS ad ON ad.NUMINGRES = i.AdmissionNumber
                   INNER JOIN dbo.INPACIENT AS pa ON ad.IPCODPACI = pa.IPCODPACI
                   INNER JOIN
              (
                  SELECT NUMINGRES, 
                         IPCODPACI, 
                         par.CODPROSAL, 
                         PS.CODESPEC1, 
                         ESP.DESESPECI, 
                         PS.NOMMEDICO, 
                         par.TERTRAPAR
                  FROM dbo.HCATINPAR AS par
                       INNER JOIN dbo.INPROFSAL AS PS ON par.CODPROSAL = PS.CODPROSAL
                       INNER JOIN.INESPECIA AS ESP ON PS.CODESPEC1 = ESP.CODESPECI
              ) AS qx ON ad.NUMINGRES = qx.NUMINGRES
                   INNER JOIN.INUBICACI AS ubi ON PA.AUUBICACI = UBI.AUUBICACI
                   INNER JOIN.INMUNICIP AS mun ON ubi.DEPMUNCOD = MUN.DEPMUNCOD
                   INNER JOIN Contract.CareGroup AS cg ON cg.Id = i.CareGroupId
                   INNER JOIN Security.[User] AS u ON u.UserCode = i.InvoicedUser
                   LEFT OUTER JOIN Contract.HealthAdministrator AS ha ON ha.Id = i.HealthAdministratorId
                   INNER JOIN Security.[User] AS Us ON i.InvoicedUser = Us.UserCode
                   INNER JOIN Billing.InvoiceCategories AS Cat ON i.InvoiceCategoryId = Cat.Id
                                                                  AND i.InvoiceCategoryId = Cat.Id
                                                                  AND i.InvoiceCategoryId = Cat.Id
                   INNER JOIN Common.ThirdParty AS ter ON ha.ThirdPartyId = ter.Id
                   INNER JOIN Security.Person AS Pe ON Us.IdPerson = Pe.Id
                   LEFT OUTER JOIN dbo.CHREGEGRE AS EGR ON ad.NUMINGRES = EGR.NUMINGRES
                   INNER JOIN dbo.INDIAGNOS AS dx ON ad.CODDIAEGR = dx.CODDIAGNO
              WHERE(i.InvoiceDate >= @FInicial
                    AND i.InvoiceDate < @FFinal)
                   AND (ter.Nit = '900935126')
                   AND i.InvoiceNumber NOT IN
              (
                  SELECT FU.Factura
                  FROM FactuUCI AS FU
              )
                   AND i.STATUS = '1'),
          DisQxs
          AS (SELECT Q.NUMEFOLIO AS Folio, 
                     Q.IPCODPACI AS IdPaciente, 
                     Q.NUMINGRES AS Ingreso, 
                     Q.CODSERIPS AS QxPpal, 
                     PrQxs.CODSERIPS AS Qxs, 
                     ROW_NUMBER() OVER(PARTITION BY PrQxs.NUMEFOLIO, 
                                                    PrQxs.NUMINGRES
                     ORDER BY PrQxs.QXPRINCIP DESC) AS ConsProcedPorQx, 
                     ROW_NUMBER() OVER(PARTITION BY PrQxs.NUMINGRES
                     ORDER BY PrQxs.NUMEFOLIO ASC, 
                              PrQxs.QXPRINCIP DESC) AS ConsProcedPorIngreso, 
                     DENSE_RANK() OVER(PARTITION BY PrQxs.NUMINGRES
                     ORDER BY PrQxs.NUMEFOLIO ASC) AS ConsFolioPorIngreso, 
                     E.DESESPECI AS Especialidad
              FROM dbo.HCQXINFOR AS Q
                   INNER JOIN dbo.HCQXREALI AS PrQxs ON Q.NUMEFOLIO = PrQxs.NUMEFOLIO
                                                        AND Q.NUMINGRES = PrQxs.NUMINGRES
                   INNER JOIN dbo.INPROFSAL AS P ON Q.CODPROSAL = P.CODPROSAL
                   INNER JOIN dbo.INESPECIA AS E ON P.CODESPEC1 = E.CODESPECI),
          DisQxs2
          AS (SELECT Folio, 
                     IdPaciente, 
                     Ingreso, 
                     QxPpal, 
                     Qxs, 
                     ConsProcedPorQx, 
                     ConsProcedPorIngreso, 
                     MAX(ConsProcedPorQx) OVER(PARTITION BY Ingreso, 
                                                            Folio) AS NumQxsFolio, 
                     MAX(ConsFolioPorIngreso) OVER(PARTITION BY Ingreso) AS NumFoliosPorIngreso, 
                     MAX(ConsProcedPorIngreso) OVER(PARTITION BY Ingreso) AS NumQxsPorIngreso, 
                     Especialidad
              FROM DisQxs),
          FactuQx
          AS (SELECT DISTINCT 
                     DATENAME(MONTH, i.InvoiceDate) + '/' + CAST(DATEPART(YEAR, i.InvoiceDate) AS VARCHAR) AS Mes, 
                     i.InvoiceNumber AS Factura, 
                     i.AdmissionNumber AS Ingreso, 
                     cg.Name AS EntidadAdministradora, 
                     i.TotalInvoice AS TotalFactura, 
                     (i.ThirdPartySalesValue) AS TotalEntidad,
                     CASE
                         WHEN cg.Name NOT LIKE '%PGP%'
                         THEN 'NO APLICA'
                         WHEN DATEDIFF(dd, AD.IFECHAING, EGR.FECEGRESO) = 0
                         THEN 'QxAmbulatorio'
                         ELSE 'QxHospitalario'
                     END AS 'TipoPGP'
              FROM Billing.Invoice AS i
                   INNER JOIN Billing.InvoiceDetail AS id ON id.InvoiceId = i.Id
                   INNER JOIN dbo.ADINGRESO AS ad ON ad.NUMINGRES = i.AdmissionNumber
                   INNER JOIN dbo.CHREGEGRE AS EGR ON ad.NUMINGRES = EGR.NUMINGRES
                   INNER JOIN dbo.INPACIENT AS pa ON ad.IPCODPACI = pa.IPCODPACI
                   INNER JOIN.INUBICACI AS ubi ON PA.AUUBICACI = UBI.AUUBICACI
                   INNER JOIN.INMUNICIP AS mun ON ubi.DEPMUNCOD = MUN.DEPMUNCOD
                   INNER JOIN Billing.ServiceOrderDetail AS sod ON sod.Id = id.ServiceOrderDetailId
                   INNER JOIN Billing.ServiceOrder so ON so.Id = sod.ServiceOrderId
                   INNER JOIN Security.[User] sou ON sou.UserCode = so.CreationUser
                   INNER JOIN Security.Person sop ON sop.Id = sou.IdPerson
                   INNER JOIN Contract.CareGroup AS cg ON cg.Id = i.CareGroupId
                   INNER JOIN Contract.CUPSEntity AS ce ON ce.Id = sod.CUPSEntityId
                   INNER JOIN Payroll.FunctionalUnit AS fu ON fu.Id = sod.PerformsFunctionalUnitId
                   INNER JOIN Payroll.CostCenter AS cc ON cc.Id = sod.CostCenterId
                   LEFT JOIN dbo.INPROFSAL AS Pf ON sod.PerformsHealthProfessionalCode = Pf.CODPROSAL
                   LEFT JOIN dbo.INESPECIA AS Esp ON Pf.CODESPEC1 = Esp.CODESPECI
                   LEFT JOIN Security.[User] AS u ON u.UserCode = i.InvoicedUser
                   LEFT JOIN Security.Person ON U.IdPerson = Person.Id
                   LEFT JOIN Billing.InvoiceCategories AS IC ON i.InvoiceCategoryId = IC.Id
                   LEFT OUTER JOIN Contract.HealthAdministrator AS ha ON ha.Id = i.HealthAdministratorId
                   LEFT OUTER JOIN Common.ThirdParty AS ter ON ha.ThirdPartyId = ter.Id
                   CROSS APPLY
              (
                  SELECT NumFoliosPorIngreso, 
                         NumQxsPorIngreso, 
                         QxPpal, 
                         Especialidad
                  FROM DisQxs2 AS Q
                  WHERE Q.Ingreso = ad.NUMINGRES
                        AND Q.Qxs = CE.Code
              ) AS Q
                   INNER JOIN Contract.CUPSEntity AS ce2 ON Q.QxPpal = CE2.Code
              WHERE(i.STATUS = 1)
                   AND (i.InvoiceDate BETWEEN @FInicial AND @FFinal)
                   AND (ter.Nit = '900935126')
                   AND i.InvoiceNumber NOT IN
              (
                  SELECT FU.Factura
                  FROM FactuUCI AS FU
              )
                   AND i.InvoiceNumber NOT IN
              (
                  SELECT FP.Factura
                  FROM FactuPartos AS FP
              )),
          FE	--Facturas con Estancias 
          AS (SELECT DISTINCT 
                     i.INVOICENUMBER
              FROM Billing.Invoice AS i
                   INNER JOIN Billing.InvoiceDetail AS id ON id.InvoiceId = i.Id
                   INNER JOIN Billing.ServiceOrderDetail AS sod ON sod.Id = id.ServiceOrderDetailId
                   INNER JOIN Contract.CUPSEntity AS ce ON ce.Id = sod.CUPSEntityId
                   INNER JOIN Billing.BillingGroup AS Gr ON ce.BillingGroupId = gr.Id
                   INNER JOIN Contract.HealthAdministrator AS ha ON ha.Id = i.HealthAdministratorId
                   INNER JOIN Common.ThirdParty AS t ON ha.ThirdPartyId = t.Id
              --where i.InvoiceNumber='hsp1671180'
              WHERE i.InvoiceDate >= @FInicial
                    AND i.InvoiceDate < @FFinal
                    AND t.Nit = '900935126'
                    AND gr.Id = 9),
          FactuInternacion
          AS (SELECT DISTINCT 
                     DATENAME(MONTH, i.InvoiceDate) + '/' + CAST(DATEPART(YEAR, i.InvoiceDate) AS VARCHAR) AS Mes, 
                     i.InvoiceNumber AS Factura, 
                     i.AdmissionNumber AS Ingreso, 
                     cg.Name AS EntidadAdministradora, 
                     i.TotalInvoice AS TotalFactura, 
                     (i.ThirdPartySalesValue) AS TotalEntidad,
                     CASE
                         WHEN cg.Name NOT LIKE '%PGP%'
                         THEN 'NO APLICA'
                         ELSE 'INTERNACION_GENERAL'
                     END AS 'TipoPGP'
              FROM Billing.Invoice AS i
                   INNER JOIN dbo.ADINGRESO AS ad ON ad.NUMINGRES = i.AdmissionNumber
                   INNER JOIN dbo.INPACIENT AS pa ON ad.IPCODPACI = pa.IPCODPACI
                   INNER JOIN Contract.CareGroup AS cg ON cg.Id = i.CareGroupId
                   INNER JOIN Security.[User] AS u ON u.UserCode = i.InvoicedUser
                   LEFT OUTER JOIN Contract.HealthAdministrator AS ha ON ha.Id = i.HealthAdministratorId
                   INNER JOIN Security.[User] AS Us ON i.InvoicedUser = Us.UserCode
                   INNER JOIN Billing.InvoiceCategories AS Cat ON i.InvoiceCategoryId = Cat.Id
                   INNER JOIN Common.ThirdParty AS ter ON ha.ThirdPartyId = ter.Id
                   INNER JOIN Security.Person AS Pe ON Us.IdPerson = Pe.Id
                   INNER JOIN dbo.CHREGEGRE AS EGR ON ad.NUMINGRES = EGR.NUMINGRES
                   INNER JOIN dbo.INDIAGNOS AS dx ON ad.CODDIAEGR = dx.CODDIAGNO
                   INNER JOIN dbo.CHCAMASHO AS cam ON AD.CODCAMACT = cam.CODICAMAS
                   INNER JOIN dbo.HCREGEGRE AS eg ON ad.NUMINGRES = eg.NUMINGRES
                   INNER JOIN dbo.INPROFSAL AS ps ON eg.CODPROSAL = ps.CODPROSAL
                   INNER JOIN dbo.INESPECIA AS esp ON PS.CODESPEC1 = esp.CODESPECI
              WHERE(i.InvoiceDate >= @FInicial
                    AND i.InvoiceDate < @FFinal)
                   AND (ter.Nit = '900935126')
                   AND i.STATUS = '1'
                   AND i.InvoiceNumber IN
              (
                  SELECT FE.InvoiceNumber
                  FROM FE
              )
                   AND i.InvoiceNumber NOT IN
              (
                  SELECT FU.Factura
                  FROM FactuUCI AS FU
              )
                   AND i.InvoiceNumber NOT IN
              (
                  SELECT FP.Factura
                  FROM FactuPartos AS FP
              )
                   AND i.InvoiceNumber NOT IN
              (
                  SELECT FQ.Factura
                  FROM FactuQx AS FQ
              )),
          FUr -- Facturas Urgencias
          AS (SELECT DISTINCT 
                     i.InvoiceNumber
              FROM Billing.Invoice AS i
                   INNER JOIN Billing.InvoiceDetail AS id ON id.InvoiceId = i.Id
                   INNER JOIN dbo.ADINGRESO AS ad ON ad.NUMINGRES = i.AdmissionNumber
                   INNER JOIN dbo.INPACIENT AS pa ON ad.IPCODPACI = pa.IPCODPACI
                   INNER JOIN Billing.ServiceOrderDetail AS sod ON sod.Id = id.ServiceOrderDetailId
                   INNER JOIN Billing.ServiceOrder so ON so.Id = sod.ServiceOrderId
                   INNER JOIN Security.[User] sou ON sou.UserCode = so.CreationUser
                   INNER JOIN Security.Person sop ON sop.Id = sou.IdPerson
                   INNER JOIN Contract.CareGroup AS cg ON cg.Id = i.CareGroupId
                   INNER JOIN Contract.CUPSEntity AS ce ON ce.Id = sod.CUPSEntityId
                   INNER JOIN Payroll.FunctionalUnit AS fu ON fu.Id = sod.PerformsFunctionalUnitId
                   INNER JOIN Payroll.CostCenter AS cc ON cc.Id = sod.CostCenterId
                   LEFT JOIN dbo.INPROFSAL AS Pf ON sod.PerformsHealthProfessionalCode = Pf.CODPROSAL
                   LEFT JOIN dbo.INESPECIA AS Esp ON Pf.CODESPEC1 = Esp.CODESPECI
                   LEFT JOIN Security.[User] AS u ON u.UserCode = i.InvoicedUser
                   LEFT JOIN Security.Person ON U.IdPerson = Person.Id
                   LEFT JOIN Billing.InvoiceCategories ON i.InvoiceCategoryId = Billing.InvoiceCategories.Id
                                                          AND i.InvoiceCategoryId = Billing.InvoiceCategories.Id
                                                          AND i.InvoiceCategoryId = Billing.InvoiceCategories.Id
                   LEFT OUTER JOIN Contract.HealthAdministrator AS ha ON ha.Id = i.HealthAdministratorId
                   LEFT OUTER JOIN Common.ThirdParty AS ter ON ha.ThirdPartyId = ter.Id
              WHERE(i.STATUS = 1)
                   AND (i.InvoiceDate BETWEEN '20180401' AND '20180501')
                   AND (ter.Nit = '900935126')
                   AND cc.Code IN(1001, 3004)),
          FactuUrg  -- URGENCIAS
          AS (SELECT DISTINCT 
                     DATENAME(MONTH, i.InvoiceDate) + '/' + CAST(DATEPART(YEAR, i.InvoiceDate) AS VARCHAR) AS Mes, 
                     i.InvoiceNumber AS Factura, 
                     i.AdmissionNumber AS Ingreso, 
                     cg.Name AS EntidadAdministradora, 
                     (i.TotalInvoice) AS TotalFactura, 
                     (i.ThirdPartySalesValue) AS TotalEntidad,
                     CASE
                         WHEN cg.Name NOT LIKE '%PGP%'
                         THEN 'NO APLICA'
                         ELSE 'URGENCIAS'
                     END AS 'TipoPGP'
              FROM Billing.Invoice AS i
                   INNER JOIN dbo.ADINGRESO AS ad ON ad.NUMINGRES = i.AdmissionNumber
                   INNER JOIN dbo.INPACIENT AS pa ON ad.IPCODPACI = pa.IPCODPACI
                   LEFT OUTER JOIN
              (
                  SELECT DISTINCT 
                         NUMINGRES, 
                         IPCODPACI
                  FROM.HCQXINFOR
              ) AS qx ON ad.NUMINGRES = qx.NUMINGRES
                   LEFT OUTER JOIN
              (
                  SELECT NUMINGRES, 
                         IPCODPACI
                  FROM dbo.HCATINPAR
              ) AS par ON ad.NUMINGRES = par.NUMINGRES
                   INNER JOIN.INUBICACI AS ubi ON PA.AUUBICACI = UBI.AUUBICACI
                   INNER JOIN.INMUNICIP AS mun ON ubi.DEPMUNCOD = MUN.DEPMUNCOD
                   INNER JOIN Contract.CareGroup AS cg ON cg.Id = i.CareGroupId
                   INNER JOIN Security.[User] AS u ON u.UserCode = i.InvoicedUser
                   LEFT OUTER JOIN Contract.HealthAdministrator AS ha ON ha.Id = i.HealthAdministratorId
                   INNER JOIN Security.[User] AS Us ON i.InvoicedUser = Us.UserCode
                   INNER JOIN Billing.InvoiceCategories AS Cat ON i.InvoiceCategoryId = Cat.Id
                                                                  AND i.InvoiceCategoryId = Cat.Id
                                                                  AND i.InvoiceCategoryId = Cat.Id
                   INNER JOIN Common.ThirdParty AS ter ON ha.ThirdPartyId = ter.Id
                   INNER JOIN Security.Person AS Pe ON Us.IdPerson = Pe.Id
                   INNER JOIN dbo.CHREGEGRE AS EGR ON ad.NUMINGRES = EGR.NUMINGRES
                   INNER JOIN dbo.INDIAGNOS AS dx ON ad.CODDIAEGR = dx.CODDIAGNO
                   INNER JOIN dbo.CHCAMASHO AS cam ON AD.CODCAMACT = cam.CODICAMAS
                   INNER JOIN dbo.HCREGEGRE AS eg ON ad.NUMINGRES = eg.NUMINGRES
                   INNER JOIN dbo.INPROFSAL AS ps ON eg.CODPROSAL = ps.CODPROSAL
                   INNER JOIN dbo.INESPECIA AS esp ON PS.CODESPEC1 = esp.CODESPECI
              WHERE(i.InvoiceDate >= @FInicial
                    AND i.InvoiceDate < @FFinal)
                   AND (ter.Nit = '900935126')
                   AND i.STATUS = '1'
                   AND i.InvoiceNumber IN
              (
                  SELECT FUr.InvoiceNumber
                  FROM FUr
              )
                   AND i.InvoiceNumber NOT IN
              (
                  SELECT FU.Factura
                  FROM FactuUCI AS FU
              )
                   AND i.InvoiceNumber NOT IN
              (
                  SELECT FP.Factura
                  FROM FactuPartos AS FP
              )
                   AND i.InvoiceNumber NOT IN
              (
                  SELECT FQ.Factura
                  FROM FactuQx AS FQ
              )
                   AND i.InvoiceNumber NOT IN
              (
                  SELECT FI.Factura
                  FROM FactuInternacion AS FI
              )),
          CONSOLIDADO
          AS (SELECT *
              FROM FactuUCI
              UNION ALL
              SELECT *
              FROM FactuPartos
              UNION ALL
              SELECT *
              FROM FactuQx
              UNION ALL
              SELECT *
              FROM FactuInternacion
              UNION ALL
              SELECT *
              FROM FactuUrg),
          FactuNoClasif
          AS (SELECT DISTINCT 
                     DATENAME(MONTH, i.InvoiceDate) + '/' + CAST(DATEPART(YEAR, i.InvoiceDate) AS VARCHAR) AS Mes, 
                     i.InvoiceNumber AS Factura, 
                     i.AdmissionNumber AS Ingreso, 
                     cg.Name AS EntidadAdministradora, 
                     i.TotalInvoice AS TotalFactura, 
                     (i.ThirdPartySalesValue) AS TotalEntidad,
                     CASE
                         WHEN cg.Name NOT LIKE '%PGP%'
                         THEN 'NO APLICA'
                         ELSE 'NoClasificada'
                     END AS 'TipoPGP'
              FROM Billing.Invoice AS i
                   INNER JOIN dbo.ADINGRESO AS ad ON ad.NUMINGRES = i.AdmissionNumber
                   INNER JOIN dbo.INPACIENT AS pa ON ad.IPCODPACI = pa.IPCODPACI
                   INNER JOIN.INUBICACI AS ubi ON PA.AUUBICACI = UBI.AUUBICACI
                   INNER JOIN.INMUNICIP AS mun ON ubi.DEPMUNCOD = MUN.DEPMUNCOD
                   INNER JOIN Contract.CareGroup AS cg ON cg.Id = i.CareGroupId
                   INNER JOIN Security.[User] AS u ON u.UserCode = i.InvoicedUser
                   LEFT OUTER JOIN Contract.HealthAdministrator AS ha ON ha.Id = i.HealthAdministratorId
                   INNER JOIN Security.[User] AS Us ON i.InvoicedUser = Us.UserCode
                   INNER JOIN Billing.InvoiceCategories AS Cat ON i.InvoiceCategoryId = Cat.Id
                                                                  AND i.InvoiceCategoryId = Cat.Id
                                                                  AND i.InvoiceCategoryId = Cat.Id
                   INNER JOIN Common.ThirdParty AS ter ON ha.ThirdPartyId = ter.Id
                   INNER JOIN Security.Person AS Pe ON Us.IdPerson = Pe.Id
                   LEFT OUTER JOIN dbo.CHREGEGRE AS EGR ON ad.NUMINGRES = EGR.NUMINGRES
                   INNER JOIN dbo.INDIAGNOS AS dx ON ad.CODDIAEGR = dx.CODDIAGNO
                   INNER JOIN dbo.HCREGEGRE AS eg ON ad.NUMINGRES = eg.NUMINGRES
                   INNER JOIN dbo.INPROFSAL AS ps ON eg.CODPROSAL = ps.CODPROSAL
                   INNER JOIN dbo.INESPECIA AS esp ON PS.CODESPEC1 = esp.CODESPECI
              WHERE(i.InvoiceDate >= @FInicial
                    AND i.InvoiceDate < @FFinal)
                   AND (ter.Nit = '900935126')
                   AND i.InvoiceNumber NOT IN
              (
                  SELECT C.Factura
                  FROM CONSOLIDADO AS C
              )
                   AND i.STATUS = '1'),
          CONSOLIDADO2
          AS (SELECT *
              FROM CONSOLIDADO
              UNION ALL
              SELECT *
              FROM FactuNoClasif)
          SELECT *
          FROM CONSOLIDADO2;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento almacenado que genera un reporte consolidado de facturas de cobro emitidas a un pagador específico (identificado por NIT ''900935126'', típicamente una EPS o aseguradora bajo modelo PGP) dentro de un rango de fechas dado. Clasifica cada factura en tres grupos de negocio: UCI/PGP (hospitalización en unidad de cuidados intensivos bajo contrato PGP), Partos (distinguiendo entre cesárea y parto normal según el procedimiento quirúrgico registrado en la historia clínica) y demás categorías de hospitalización activas (códigos 03, 04, 05, 08). Para construir el resultado combina el encabezado de factura con el ingreso del paciente, su grupo de contrato o cuidado, la entidad administradora de salud, el municipio de residencia, el usuario que facturó, los diagnósticos de egreso, el profesional y especialidad tratante, y los procedimientos quirúrgicos registrados; permitiendo así el seguimiento, auditoría y liquidación de facturas consolidadas bajo esquemas de pago global prospectivo (PGP).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'USP_PGPfacturasConsolidadas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'USP_PGPfacturasConsolidadas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Clasifica y consolida las facturas emitidas a un tercero específico (NIT 900935126) en categorías de Pago Global Prospectivo (UCI, Partos, Quirúrgicas, Internación, Urgencias y No Clasificadas) dentro de un rango de fechas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasConsolidadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas de admisiones, egresos, pacientes, profesionales y catálogos clínicos deben tener integridad referencial con las facturas; Debe existir un tercero con NIT ''900935126'' registrado como HealthAdministrator; Las categorías de factura deben tener los códigos ''03'',''04'',''05'',''08'' para identificar UCI; El BillingGroup con Id=9 debe corresponder a estancias; Los centros de costo 1001 y 3004 deben corresponder a Urgencias; Los CareGroup que aplican a PGP deben contener la cadena ''PGP'' en su Name; @FInicial y @FFinal deben representar un rango válido de fechas de facturación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasConsolidadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran facturas con i.STATUS = 1 (activas); Solo se procesan facturas del tercero (NIT) ''900935126''; El rango de facturación se filtra por InvoiceDate entre @FInicial y @FFinal (excepto el CTE FUr que usa fechas literales ''20180401''-''20180501''); Una factura se asigna a una sola categoría PGP gracias al encadenamiento de NOT IN entre los CTEs (UCI > Partos > Qx > Internación > Urgencias > NoClasificada); Las facturas cuyo CareGroup no contenga ''PGP'' se etiquetan siempre como ''NO APLICA''; El resultado final es la unión de todas las clasificaciones más las no clasificadas (CONSOLIDADO2)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasConsolidadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Pago Global Prospectivo (PGP); UCI; Parto normal; Cesárea; Cirugía ambulatoria; Cirugía hospitalaria; Internación / hospitalización; Urgencias; Factura; Ingreso/admisión; Egreso hospitalario; Entidad administradora de salud; Diagnóstico de egreso; Especialidad médica; Centro de costo; Procedimiento CUPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasConsolidadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve el conjunto CONSOLIDADO2: unión de FactuUCI + FactuPartos + FactuQx + FactuInternacion + FactuUrg + FactuNoClasif con columnas Mes, Factura, Ingreso, EntidadAdministradora, TotalFactura, TotalEntidad y TipoPGP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasConsolidadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si cg.Name NOT LIKE ''%PGP%'' → Marca la factura con TipoPGP = ''NO APLICA'' (no entra al esquema PGP) else Asigna el TipoPGP correspondiente al bloque (UCI, CESAREA/PARTO NORMAL, QxAmbulatorio/QxHospitalario, INTERNACION_GENERAL, URGENCIAS, NoClasificada); si qx.TERTRAPAR LIKE ''%CESAREA%'' (en bloque de partos) → Clasifica como ''CESAREA'' else Clasifica como ''PARTO NORMAL''; si DATEDIFF(dd, AD.IFECHAING, EGR.FECEGRESO) = 0 (en bloque quirúrgico) → Clasifica como ''QxAmbulatorio'' else Clasifica como ''QxHospitalario''; si Cat.Code IN (''03'',''04'',''05'',''08'') → Considera la factura como candidata al grupo UCI; si gr.Id = 9 (BillingGroup) sobre detalles de la factura → Identifica la factura como con estancias y elegible para FactuInternacion; si cc.Code IN (1001, 3004) (centros de costo) → Identifica la factura como de Urgencias (FUr); si Factura no aparece en ninguna categoría previa (UCI, Partos, Qx, Internación, Urgencias) → Se clasifica como ''NoClasificada'' en FactuNoClasif', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasConsolidadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Billing.ServiceOrder; Billing.InvoiceCategories; Billing.BillingGroup; Contract.CareGroup; Contract.HealthAdministrator; Contract.CUPSEntity; Common.ThirdParty; Security.User; Security.Person; Payroll.FunctionalUnit; Payroll.CostCenter; dbo.ADINGRESO; dbo.INPACIENT; dbo.INUBICACI; dbo.INMUNICIP; dbo.CHREGEGRE; dbo.HCREGEGRE; dbo.INDIAGNOS; dbo.INPROFSAL; dbo.INESPECIA; dbo.HCATINPAR; dbo.HCQXINFOR; dbo.HCQXREALI; dbo.CHCAMASHO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasConsolidadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasConsolidadas';
-- GO
