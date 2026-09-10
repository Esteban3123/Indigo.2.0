CREATE PROCEDURE [dbo].[USP_PGPfacturasCirugia] @FInicial AS DATETIME, 
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
                     END AS 'TipoPGP'
              FROM Billing.Invoice AS i
                   INNER JOIN dbo.ADINGRESO AS ad ON ad.NUMINGRES = i.AdmissionNumber
                   INNER JOIN dbo.INPACIENT AS pa ON ad.IPCODPACI = pa.IPCODPACI
                   INNER JOIN
              (
                  SELECT DISTINCT 
                         NUMINGRES, 
                         IPCODPACI, 
                         Q.CODSERIPS, 
                         PR.DESSERIPS, 
                         Q.CODPROSAL, 
                         PS.CODESPEC1, 
                         ESP.DESESPECI, 
                         PS.NOMMEDICO
                  FROM.HCQXINFOR AS Q
                      INNER JOIN dbo.INPROFSAL AS PS ON Q.CODPROSAL = PS.CODPROSAL
                      INNER JOIN.INESPECIA AS ESP ON PS.CODESPEC1 = ESP.CODESPECI
                      INNER JOIN.INCUPSIPS AS PR ON Q.CODSERIPS = PR.CODSERIPS
                  WHERE PR.CODSERIPS NOT IN('740100')
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
                   AND i.STATUS = '1'
                   AND i.InvoiceNumber NOT IN
              (
                  SELECT *
                  FROM TF
              ))
          SELECT DISTINCT 
                 Mes, 
                 EntidadAdministradora, 
                 Cedula, 
                 NombrePaciente, 
                 MunicipioResidencia, 
                 Factura, 
                 Ingreso, 
                 TotalFactura, 
                 CAST((AVG(TotalFactura) OVER(PARTITION BY TipoPGP)) AS INT) AS PromedioPGP, 
                 TotalEntidad, 
                 TipoPGP, 
                 COUNT(TipoPGP) OVER(PARTITION BY TipoPGP) AS NumEventosTipoPGP
          FROM FactuQx AS FQ
          ORDER BY TipoPGP, 
                   TotalFactura DESC;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de reporte de facturación quirúrgica bajo modelo PGP (Pago Global Prospectivo). Dado un rango de fechas, consolida las facturas emitidas a una entidad administradora específica (NIT 900935126) que corresponden a tres grupos de atención: partos y cesáreas (cruza historia clínica de atención del parto con admisiones e información del profesional y su especialidad), servicios de UCI/hospitalización clasificados por categorías de factura, y procedimientos quirúrgicos ambulatorios u hospitalarios (cruza el registro de información quirúrgica con servicios CUPS, profesionales y especialidades). Para cada factura identificada devuelve el mes, la entidad administradora, la cédula y nombre del paciente, municipio de residencia, número de ingreso, total facturado, valor a cargo de la entidad y la clasificación del tipo de PGP (quirúrgico ambulatorio, quirúrgico hospitalario o no aplica), permitiendo el seguimiento y control de la facturación quirúrgica y obstétrica bajo contratos de capitación o pago global.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'USP_PGPfacturasCirugia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'USP_PGPfacturasCirugia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de facturas de cirugía bajo modalidad PGP para un tercero específico (NIT 900935126), excluyendo facturas de partos/cesáreas y de UCI, clasificándolas como ambulatorias u hospitalarias con promedios y conteos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasCirugia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se requiere rango de fechas (inicial/final) para filtrar InvoiceDate.; Debe existir el tercero pagador con Nit ''900935126'' en Common.ThirdParty asociado a HealthAdministrator.; Las facturas deben tener STATUS = ''1'' (activas/válidas).; Los ingresos deben tener registros relacionados en HCQXINFOR (procedimientos quirúrgicos) y en HCREGEGRE/CHREGEGRE para egresos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasCirugia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran facturas con STATUS = ''1''.; Solo se incluyen facturas cuyo tercero pagador tenga NIT ''900935126''.; El rango de fechas es semiabierto: incluye @FInicial y excluye @FFinal.; Las facturas de partos/cesáreas (presentes en HCATINPAR) y las de UCI (categorías 03, 04, 05, 08) nunca aparecen en el resultado de cirugías.; El procedimiento con CODSERIPS ''740100'' no se cuenta como cirugía.; El promedio (PromedioPGP) y el conteo (NumEventosTipoPGP) se calculan particionados por TipoPGP.; Los resultados se ordenan por TipoPGP y por TotalFactura descendente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasCirugia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Facturación; Pago Global Prospectivo (PGP); Cirugía ambulatoria; Cirugía hospitalaria; Parto y cesárea; UCI (Unidad de Cuidados Intensivos); Ingreso/Egreso hospitalario; Administradora de salud (EPS); Tercero pagador / NIT; Procedimiento quirúrgico (CUPS); Especialidad médica; Profesional de la salud; Diagnóstico de egreso; Municipio de residencia del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasCirugia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve facturas quirúrgicas del tercero NIT 900935126 con STATUS=''1'' en el rango [@FInicial, @FFinal), excluyendo las de partos/cesáreas y las de UCI (categorías ''03'',''04'',''05'',''08''), con clasificación TipoPGP y métricas agregadas por tipo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasCirugia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si cg.Name NOT LIKE ''%PGP%'' → TipoPGP = ''NO APLICA'' else Evalúa si la atención fue ambulatoria u hospitalaria según la diferencia de días entre ingreso y egreso.; si DATEDIFF(dd, AD.IFECHAING, EGR.FECEGRESO) = 0 (y el contrato es PGP) → TipoPGP = ''QxAmbulatorio'' else TipoPGP = ''QxHospitalario''; si Categoría de factura IN (''03'',''04'',''05'',''08'') → La factura se clasifica como UCI y se excluye del reporte de cirugías.; si Existe registro en HCATINPAR para el ingreso → La factura se clasifica como Parto/Cesárea y se excluye del reporte de cirugías.; si Procedimiento quirúrgico con CODSERIPS = ''740100'' → Se excluye del conjunto de cirugías consideradas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasCirugia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; dbo.ADINGRESO; dbo.INPACIENT; dbo.HCATINPAR; dbo.INPROFSAL; dbo.INESPECIA; dbo.INUBICACI; dbo.INMUNICIP; Contract.CareGroup; Security.User; Contract.HealthAdministrator; Billing.InvoiceCategories; Common.ThirdParty; Security.Person; dbo.CHREGEGRE; dbo.HCREGEGRE; dbo.INDIAGNOS; dbo.HCQXINFOR; dbo.INCUPSIPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasCirugia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasCirugia';
-- GO
