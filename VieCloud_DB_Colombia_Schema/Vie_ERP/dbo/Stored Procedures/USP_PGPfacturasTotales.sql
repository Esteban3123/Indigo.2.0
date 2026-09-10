CREATE PROCEDURE [dbo].[USP_PGPfacturasTotales] @FInicial AS DATETIME, 
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
          FactuUrgencias
          AS (SELECT DISTINCT 
                     i.InvoiceNumber AS Factura, 
                     i.AdmissionNumber AS Ingreso,
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
              ))
          SELECT *
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
          FROM FactuUrgencias;
     RETURN 0;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el reporte consolidado de facturas totales bajo modalidad PGP (Pago Global Prospectivo) para un rango de fechas dado. Clasifica cada factura según el tipo de PGP: UCI (unidades de cuidados intensivos), Partos (normal o cesárea) y Cirugías (procedimientos quirúrgicos), filtrando únicamente facturas activas del pagador con NIT 900935126 y categorías de facturación específicas (hospitalización, cirugías, partos, entre otras). Integra información de admisiones de pacientes, grupos de atención contratados, administradoras de salud (EPS/aseguradoras), datos del profesional que facturó, diagnósticos de egreso (CIE-10), procedimientos quirúrgicos realizados y municipio de residencia del paciente. Se usa para liquidación, auditoría y control de facturación PGP, permitiendo identificar qué ingresos y facturas corresponden a cada modalidad de pago global prospectivo en un período determinado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'USP_PGPfacturasTotales';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'USP_PGPfacturasTotales';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Clasifica las facturas emitidas a un tercero pagador específico (NIT 900935126) en un rango de fechas, asignándoles una categoría de PGP (UCI, Partos, Quirúrgico, Internación, Urgencias) según reglas clínico-administrativas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasTotales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se requiere recibir un rango de fechas (inicial y final) para acotar las facturas evaluadas.; Deben existir facturas en estado activo (STATUS = ''1'') asociadas al tercero con NIT ''900935126''.; Las admisiones referenciadas deben tener correspondencia en pacientes, ubicación, municipio, profesional de salud, especialidad y diagnóstico de egreso.; Para clasificación quirúrgica deben existir registros en HCQXINFOR/HCQXREALI vinculados a la admisión y procedimientos mapeados a CUPSEntity.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasTotales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Una misma factura solo puede aparecer en una de las cinco categorías PGP, dado que cada CTE excluye explícitamente las facturas ya clasificadas en las categorías previas.; Solo se consideran facturas activas (STATUS=1) del tercero con NIT ''900935126''.; Las facturas cuyo CareGroup no contiene ''PGP'' siempre se etiquetan como ''NO APLICA'' aunque entren en alguna categoría clínica.; La identificación de UCI depende exclusivamente del código de categoría de factura (''03'',''04'',''05'',''08''), no del servicio prestado.; La clasificación de internación general exige presencia de estancias facturadas (BillingGroup.Id = 9) y ausencia de procedimientos quirúrgicos y atención de parto.; La clasificación de Cesárea vs Parto Normal depende del texto ''CESAREA'' en el campo TERTRAPAR del registro de atención de parto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasTotales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'PGP (Pago Global Prospectivo); UCI; Parto normal; Cesárea; Cirugía ambulatoria; Cirugía hospitalaria; Internación general; Urgencias; Factura; Admisión / ingreso hospitalario; Egreso hospitalario; Especialidad médica; Diagnóstico de egreso; Administradora de salud (EPS); Grupo de atención (CareGroup); CUPS (procedimientos); Estancia hospitalaria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasTotales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve la unión (UNION ALL) de las facturas clasificadas en cinco categorías PGP: UCI, Partos, Quirúrgico, Internación y Urgencias, con su número de ingreso y tipo PGP.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasTotales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Factura del NIT 900935126 cuya InvoiceCategory.Code está en (''03'',''04'',''05'',''08'') → Se clasifica como TipoPGP = ''UCI'' else Se evalúan las siguientes categorías; si Factura no clasificada como UCI y cuya admisión tiene registro en HCATINPAR (atención de parto) → Si CareGroup.Name no contiene ''PGP'' → ''NO APLICA''; si TERTRAPAR contiene ''CESAREA'' → ''CESAREA''; en otro caso → ''PARTO NORMAL''; si Factura no clasificada como UCI ni Partos, con detalle quirúrgico (CUPS de procedimiento ligado a HCQXINFOR/HCQXREALI) → Si CareGroup.Name no contiene ''PGP'' → ''NO APLICA''; si la fecha de ingreso = fecha de egreso (DATEDIFF=0) → ''QxAmbulatorio''; en otro caso → ''QxHospitalario''; si Factura no clasificada en las anteriores, con detalle de estancia (BillingGroup.Id = 9) y sin registros quirúrgicos ni de parto en la admisión → Si CareGroup.Name no contiene ''PGP'' → ''NO APLICA''; en otro caso → ''INTERNACION_GENERAL''; si Factura no clasificada en las anteriores categorías → Si CareGroup.Name no contiene ''PGP'' → ''NO APLICA''; en otro caso → ''URGENCIAS''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasTotales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Billing.ServiceOrder; Billing.InvoiceCategories; Billing.BillingGroup; Contract.CareGroup; Contract.HealthAdministrator; Contract.CUPSEntity; Common.ThirdParty; Security.User; Security.Person; Payroll.FunctionalUnit; Payroll.CostCenter; dbo.ADINGRESO; dbo.INPACIENT; dbo.INUBICACI; dbo.INMUNICIP; dbo.INPROFSAL; dbo.INESPECIA; dbo.INDIAGNOS; dbo.CHREGEGRE; dbo.HCREGEGRE; dbo.CHCAMASHO; dbo.HCATINPAR; dbo.HCQXINFOR; dbo.HCQXREALI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasTotales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasTotales';
-- GO
