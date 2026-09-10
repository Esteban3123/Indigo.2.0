CREATE PROCEDURE [dbo].[USP_PGPfacturasCirugia2] @FInicial AS DATETIME, 
                                                @FFinal AS   DATETIME
AS
     WITH FactuPartos
     --Numero de facturas de Partos y Cesareas
          AS (SELECT i.InvoiceNumber
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
                   INNER JOIN Contract.CareGroup AS cg ON cg.Id = i.CareGroupId
                   INNER JOIN Security.[User] AS u ON u.UserCode = i.InvoicedUser
                   LEFT OUTER JOIN Contract.HealthAdministrator AS ha ON ha.Id = i.HealthAdministratorId
                   INNER JOIN Security.[User] AS Us ON i.InvoicedUser = Us.UserCode
                   INNER JOIN Billing.InvoiceCategories AS Cat ON i.InvoiceCategoryId = Cat.Id
                                                                  AND i.InvoiceCategoryId = Cat.Id
                                                                  AND i.InvoiceCategoryId = Cat.Id
                   INNER JOIN Common.ThirdParty AS ter ON ha.ThirdPartyId = ter.Id
              WHERE(i.InvoiceDate >= @FInicial
                    AND i.InvoiceDate < @FFinal)
                   AND (ter.Nit = '900935126')
                   AND i.STATUS = '1'),
          FactuUCI
          AS (SELECT i.InvoiceNumber
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
          TF
          AS (SELECT *
              FROM FactuPartos
              UNION ALL
              SELECT *
              FROM FactuUCI),
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
                     cg.Name AS EntidadAdministradora, 
                     ad.IPCODPACI AS Cedula, 
                     pa.IPNOMCOMP AS NombrePaciente, 
                     mun.MUNNOMBRE AS MunicipioResidencia, 
                     i.InvoiceNumber AS Factura, 
                     i.AdmissionNumber AS Ingreso, 
                     (i.TotalInvoice) AS TotalFactura, --AVG(i.TotalInvoice) OVER(PARTITION BY qx.TERTRAPAR) AS PromedioPGP,
                     (i.ThirdPartySalesValue) AS TotalEntidad,
                     CASE
                         WHEN cg.Name NOT LIKE '%PGP%'
                         THEN 'NO APLICA'
                         WHEN DATEDIFF(dd, AD.IFECHAING, EGR.FECEGRESO) = 0
                         THEN 'QxAmbulatorio'
                         ELSE 'QxHospitalario'
                     END AS 'TipoPGP', 
                     NumFoliosPorIngreso AS NumInformesQxs, 
                     NumQxsPorIngreso AS TotalProcedQxsPorIngreso, 
                     QxPpal, 
                     CE2.[Description] AS DescripcionQxPpal, 
                     Especialidad
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
                   AND (ter.Nit = '900935126')),
          PRE
          AS (SELECT DISTINCT 
                     Mes, 
                     EntidadAdministradora, 
                     Cedula, 
                     NombrePaciente, 
                     MunicipioResidencia, 
                     Factura, 
                     Ingreso, 
                     TotalFactura, 
                     CAST((AVG(TotalFactura) OVER(PARTITION BY TipoPGP)) AS INT) AS PromedioPGP, 
                     CAST((SUM(TotalFactura) OVER(PARTITION BY TipoPGP)) AS INT) AS TotalFacturasPGP, 
                     TotalEntidad, 
                     TipoPGP, 
                     NumInformesQxs, 
                     TotalProcedQxsPorIngreso, 
                     QxPpal, 
                     DescripcionQxPpal, 
                     Especialidad, 
                     COUNT(TipoPGP) OVER(PARTITION BY TipoPGP) AS NumEventosTipoPGP,
                     CASE
                         WHEN TipoPGP = 'QxAmbulatorio'
                         THEN 134
                         WHEN TipoPGP = 'QxHospitalario'
                         THEN 140
                         ELSE NULL
                     END EventosMeta,
                     CASE
                         WHEN TipoPGP = 'QxAmbulatorio'
                         THEN 1128148
                         WHEN TipoPGP = 'QxHospitalario'
                         THEN 1338428
                         ELSE NULL
                     END PGP_CME,
                     CASE
                         WHEN TipoPGP = 'QxAmbulatorio'
                         THEN(1128148 * 134)
                         WHEN TipoPGP = 'QxHospitalario'
                         THEN(1338428 * 140)
                         ELSE NULL
                     END PGP_VlrContrato
              FROM FactuQx AS FQ
              WHERE Factura NOT IN
              (
                  SELECT TF.InvoiceNumber
                  FROM TF
              ))
          SELECT Mes, 
                 EntidadAdministradora, 
                 Cedula, 
                 NombrePaciente, 
                 MunicipioResidencia, 
                 Factura, 
                 Ingreso, 
                 TotalFactura, 
                 PromedioPGP, 
                 TotalFacturasPGP, 
                 TotalEntidad, 
                 TipoPGP, 
                 NumInformesQxs, 
                 TotalProcedQxsPorIngreso, 
                 QxPpal, 
                 DescripcionQxPpal, 
                 Especialidad, 
                 NumEventosTipoPGP, 
                 EventosMeta, 
                 PGP_CME, 
                 PGP_VlrContrato,
                 CASE
                     WHEN NumEventosTipoPGP > (EventosMeta * 1.1)
                     THEN(PGP_VlrContrato + ((NumEventosTipoPGP - (EventosMeta * 1.1)) * (PGP_CME / 2)))
                     WHEN NumEventosTipoPGP < (EventosMeta * 0.8)
                     THEN(PGP_VlrContrato + ((NumEventosTipoPGP - (EventosMeta * 0.8)) * (PGP_CME / 2))) --(PGP_VlrContrato - ((NumEventosTipoPGP-(EventosMeta * 0.8))*(PGP_CME/2)))
                     ELSE PGP_VlrContrato
                 END AS PGP_VlrContratoAjustado  --PGP_VlrContrato
          FROM PRE
          ORDER BY TipoPGP, 
                   TotalFactura DESC;
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento que genera un reporte de facturación quirúrgica PGP (Pago Global Prospectivo) para un tercero pagador con NIT específico (900935126), en un rango de fechas. Excluye facturas de partos/cesáreas y de UCI, y clasifica cada cirugía como ambulatoria u hospitalaria según la diferencia entre fecha de ingreso y egreso. Calcula por tipo PGP: promedio y total facturado, número de eventos, metas contractuales y valores de contrato PGP-CME, cruzando procedimientos CUPS quirúrgicos con informes de quirófano.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasCirugia2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasCirugia2';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta facturas de cirugía bajo modelo PGP para una administradora específica (NIT 900935126), clasificándolas en ambulatorias u hospitalarias y calculando el valor del contrato ajustado según desviación frente a metas pactadas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasCirugia2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las facturas deben tener STATUS = 1 (activas/vigentes).; InvoiceDate debe estar dentro del rango [@FInicial, @FFinal).; El tercero asociado a la administradora de salud debe tener NIT = ''900935126''.; Cada ingreso debe tener registro de egreso (CHREGEGRE) para determinar tipo PGP por diferencia de fechas.; Deben existir folios quirúrgicos (HCQXINFOR/HCQXREALI) ligados al ingreso para cruzar con el detalle de la factura.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasCirugia2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan facturas activas (STATUS=1) del tercero con NIT 900935126.; Las facturas de partos/cesáreas y las de UCI (categorías 03, 04, 05, 08) nunca aparecen en el resultado final.; Las metas y valores CME son constantes por tipo: ambulatorio (134 eventos, $1.128.148) y hospitalario (140 eventos, $1.338.428).; El valor base del contrato PGP se calcula como CME * EventosMeta por cada tipo.; La banda de tolerancia sin ajuste es entre el 80% y el 110% de la meta de eventos.; El procedimiento principal (QxPpal) se determina por el ranking QXPRINCIP DESC dentro del folio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasCirugia2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'PGP (Pago Global Prospectivo); Cirugía ambulatoria; Cirugía hospitalaria; Parto y cesárea; UCI (Unidad de Cuidados Intensivos); Administradora de salud / EPS; Ingreso/Egreso hospitalario; Folio quirúrgico; Procedimiento principal (CUPS); Especialidad médica; Meta de eventos contractuales; CME (Costo Medio por Evento); Ajuste de contrato por desviación de eventos; Municipio de residencia del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasCirugia2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve el listado de facturas quirúrgicas PGP del NIT 900935126 dentro del rango de fechas, excluyendo las de partos/cesáreas y las de categorías UCI (''03'',''04'',''05'',''08''), con cálculo de PGP_VlrContratoAjustado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasCirugia2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CareGroup.Name NOT LIKE ''%PGP%'' → TipoPGP = ''NO APLICA'' else Se evalúa si es ambulatorio u hospitalario por fechas de ingreso/egreso; si DATEDIFF(dd, IFECHAING, FECEGRESO) = 0 (mismo día) → TipoPGP = ''QxAmbulatorio'' con meta 134 eventos y CME 1.128.148 else TipoPGP = ''QxHospitalario'' con meta 140 eventos y CME 1.338.428; si NumEventosTipoPGP > EventosMeta * 1.1 (excede 110% de la meta) → PGP_VlrContratoAjustado = PGP_VlrContrato + (exceso sobre 110% * CME/2), penalización por sobreejecución; si NumEventosTipoPGP < EventosMeta * 0.8 (por debajo del 80%) → PGP_VlrContratoAjustado = PGP_VlrContrato + ((eventos - 80% meta) * CME/2), ajuste negativo por subejecución; si NumEventosTipoPGP entre 80% y 110% de la meta → PGP_VlrContratoAjustado = PGP_VlrContrato (sin ajuste, dentro de la banda); si Factura pertenece a FactuPartos o FactuUCI → Se excluye del resultado final (NOT IN TF)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasCirugia2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrder; Billing.ServiceOrderDetail; Billing.InvoiceCategories; dbo.ADINGRESO; dbo.INPACIENT; dbo.HCATINPAR; dbo.INPROFSAL; dbo.INESPECIA; dbo.INUBICACI; dbo.INMUNICIP; dbo.CHREGEGRE; dbo.HCREGEGRE; dbo.INDIAGNOS; dbo.HCQXINFOR; dbo.HCQXREALI; Contract.CareGroup; Contract.HealthAdministrator; Contract.CUPSEntity; Security.User; Security.Person; Common.ThirdParty; Payroll.FunctionalUnit; Payroll.CostCenter', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasCirugia2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasCirugia2';
-- GO
