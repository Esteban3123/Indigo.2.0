CREATE PROCEDURE [dbo].[USP_PGPfacturasUrgencias] @FInicial AS DATETIME, 
                                                 @FFinal AS   DATETIME
AS
     WITH FactuUCI
          AS (SELECT i.InvoiceNumber AS Factura, 
                     i.AdmissionNumber AS Ingreso, 
                     'UCI' AS TipoPGP
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
                     i.InvoiceNumber AS Factura, 
                     i.AdmissionNumber AS Ingreso,
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
                     i.InvoiceNumber AS Factura, 
                     i.AdmissionNumber AS Ingreso,
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
                     i.InvoiceNumber AS Factura, 
                     i.AdmissionNumber AS Ingreso,
                     CASE
                         WHEN cg.Name NOT LIKE '%PGP%'
                         THEN 'NO APLICA'
                         ELSE 'INTERNACION_GENERAL'
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
                   AND (qx.IPCODPACI IS NULL)
                   AND par.IPCODPACI IS NULL
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
                   AND (i.InvoiceDate >= @FInicial
                        AND i.InvoiceDate < @FFinal)
                   AND (ter.Nit = '900935126')
                   AND cc.Code IN(1001, 3004)),
          URG  -- URGENCIAS
          AS (SELECT DISTINCT 
                     DATENAME(MONTH, i.InvoiceDate) + '/' + CAST(DATEPART(YEAR, i.InvoiceDate) AS VARCHAR) AS Mes, 
                     cg.Name AS EntidadAdministradora, 
                     ad.IPCODPACI AS Cedula, 
                     i.InvoiceNumber AS Factura, 
                     i.AdmissionNumber AS Ingreso, 
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
          PRE
          AS (SELECT URG.Mes, 
                     URG.EntidadAdministradora, 
                     URG.Cedula, 
                     URG.Factura, 
                     URG.Ingreso, 
                     URG.TotalFactura, 
                     URG.TotalEntidad, 
                     URG.TipoPGP, 
                     CAST((AVG(URG.TotalFactura) OVER(PARTITION BY URG.TipoPGP)) AS INT) AS PromedioPGP, 
                     CAST((SUM(URG.TotalFactura) OVER(PARTITION BY URG.TipoPGP)) AS INT) AS TotalFacturasPGP, 
                     COUNT(URG.TipoPGP) OVER(PARTITION BY URG.TipoPGP) AS NumEventosTipoPGP,
                     CASE
                         WHEN URG.TipoPGP = 'URGENCIAS'
                         THEN 1244
                         ELSE NULL
                     END EventosMeta,
                     CASE
                         WHEN URG.TipoPGP = 'URGENCIAS'
                         THEN 71402
                         ELSE NULL
                     END PGP_CME,
                     CASE
                         WHEN URG.TipoPGP = 'URGENCIAS'
                         THEN(71402 * 1244)
                         ELSE NULL
                     END PGP_VlrContrato
              FROM URG)
          SELECT Mes, 
                 EntidadAdministradora, 
                 Cedula, 
                 Factura, 
                 Ingreso, 
                 TotalFactura, 
                 TotalEntidad, 
                 TipoPGP, 
                 PromedioPGP, 
                 TotalFacturasPGP, 
                 NumEventosTipoPGP, 
                 EventosMeta, 
                 PGP_CME, 
                 PGP_VlrContrato,
                 CASE
                     WHEN NumEventosTipoPGP > (EventosMeta * 1.1)
                     THEN(PGP_VlrContrato + ((NumEventosTipoPGP - (EventosMeta * 1.1)) * (PGP_CME / 2)))
                     WHEN NumEventosTipoPGP < (EventosMeta * 0.8)
                     THEN(PGP_VlrContrato + ((NumEventosTipoPGP - (EventosMeta * 0.8)) * (PGP_CME / 2)))
                     ELSE PGP_VlrContrato
                 END AS PGP_VlrContratoAjustado
          FROM PRE
          ORDER BY TotalFactura DESC, 
                   TipoPGP;
     RETURN 0;

--EXEC [dbo].[USP_PGPfacturasUrgencias] '20180501','20180601'
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el reporte de facturas de urgencias bajo modalidad PGP (Pago Global Prospectivo) para un pagador específico (NIT 900935126), en un rango de fechas dado. Clasifica las facturas en tres grandes grupos: UCI (Unidad de Cuidados Intensivos), Partos (diferenciando parto normal, cesárea o casos que no aplican PGP) y Cirugías (identificando procedimientos quirúrgicos por ingreso). Para ello cruza las facturas emitidas (Billing.Invoice) con los ingresos o admisiones del paciente (ADINGRESO), datos del paciente (INPACIENT), municipio de residencia (INUBICACI, INMUNICIP), grupo de atención del contrato (CareGroup), categoría de factura (InvoiceCategories), la entidad pagadora/EPS (HealthAdministrator, ThirdParty), el usuario que facturó (Security.User), diagnóstico de egreso (INDIAGNOS), registro de egreso (HCREGEGRE, CHREGEGRE), especialidad del médico (INESPECIA, INPROFSAL), atención de partos (HCATINPAR) e información quirúrgica (HCQXINFOR, HCQXREALI). Se utiliza para conciliación, auditoría y reportería de facturación PGP ante la entidad pagadora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'USP_PGPfacturasUrgencias';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'USP_PGPfacturasUrgencias';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Clasifica las facturas de un periodo del tercero NIT 900935126 dentro del modelo PGP de Urgencias y calcula el valor del contrato ajustado según desviación frente a la meta de eventos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasUrgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se requieren fechas inicial y final del rango de facturación; Deben existir facturas del tercero con NIT ''900935126'' en el rango; Las facturas deben estar en estado i.STATUS = ''1'' (activas/vigentes); Los ingresos deben tener egresos registrados (CHREGEGRE/HCREGEGRE) y diagnóstico de egreso para clasificarse', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasUrgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan facturas del tercero con NIT ''900935126''; Solo se incluyen facturas con STATUS = ''1''; Las facturas se clasifican en una única categoría PGP por orden de prioridad: UCI > Partos > Qx > Internación > Urgencias (por exclusión mutua mediante NOT IN); Para PGP de Urgencias se usan parámetros fijos: meta=1244 eventos, PGP_CME=71.402, valor de contrato base = 71402*1244; El ajuste por desviación aplica una banda de tolerancia de 80%-110% sobre la meta de eventos; Fuera de la banda, el ajuste se calcula a la mitad del CME (PGP_CME/2) por evento de desviación; Solo facturas con InvoiceDate dentro del rango [@FInicial, @FFinal) se incluyen', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasUrgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'PGP (Pago Global Prospectivo); Facturación de Urgencias; Facturación UCI; Partos y cesáreas; Cirugía ambulatoria vs hospitalaria; Internación/Estancia hospitalaria; Egreso hospitalario; Diagnóstico de egreso; Centro de costo; Grupo de atención (CareGroup); Administradora de salud; Meta de eventos contractual; CME (Costo Medio por Evento); Ajuste de valor de contrato por desviación; CUPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasUrgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve listado de facturas clasificadas como URGENCIAS con métricas PGP (promedio, total, número de eventos) y el valor de contrato ajustado por desviación de la meta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasUrgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Cat.Code IN (''03'',''04'',''05'',''08'') y NIT=''900935126'' → La factura se clasifica como UCI (FactuUCI) else Se evalúan los demás bloques (Partos, Qx, Internación, Urgencias); si cg.Name NOT LIKE ''%PGP%'' → TipoPGP = ''NO APLICA'' else Se asigna el TipoPGP correspondiente al CTE evaluado; si En FactuPartos: qx.TERTRAPAR LIKE ''%CESAREA%'' → TipoPGP = ''CESAREA'' else TipoPGP = ''PARTO NORMAL''; si En FactuQx: DATEDIFF(dd, AD.IFECHAING, EGR.FECEGRESO) = 0 → TipoPGP = ''QxAmbulatorio'' else TipoPGP = ''QxHospitalario''; si En FE: gr.Id = 9 (grupo de facturación de estancias) → La factura se considera con estancia para clasificarla como Internación else No clasifica como Internación general; si En FUr: cc.Code IN (1001, 3004) → La factura se considera de Urgencias por centro de costo else No entra al universo de Urgencias; si Factura no está en FactuUCI ni FactuPartos ni FactuQx ni FactuInternacion y pertenece a FUr → TipoPGP = ''URGENCIAS'' else Excluida del resultado final; si NumEventosTipoPGP > (EventosMeta * 1.1) → PGP_VlrContratoAjustado = PGP_VlrContrato + (eventos en exceso sobre 110% de meta) * (PGP_CME/2) else Se evalúa el límite inferior; si NumEventosTipoPGP < (EventosMeta * 0.8) → PGP_VlrContratoAjustado = PGP_VlrContrato + (eventos faltantes bajo 80% de meta) * (PGP_CME/2) (descuento) else PGP_VlrContratoAjustado = PGP_VlrContrato sin ajuste', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasUrgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Billing.ServiceOrder; Billing.InvoiceCategories; Billing.BillingGroup; dbo.ADINGRESO; dbo.INPACIENT; dbo.INUBICACI; dbo.INMUNICIP; dbo.CHREGEGRE; dbo.HCREGEGRE; dbo.INDIAGNOS; dbo.INPROFSAL; dbo.INESPECIA; dbo.HCATINPAR; dbo.HCQXINFOR; dbo.HCQXREALI; dbo.CHCAMASHO; Contract.CareGroup; Contract.HealthAdministrator; Contract.CUPSEntity; Common.ThirdParty; Security.User; Security.Person; Payroll.FunctionalUnit; Payroll.CostCenter', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasUrgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasUrgencias';
-- GO
