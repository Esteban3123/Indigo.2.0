CREATE PROCEDURE [dbo].[USP_PGPfacturasPartosCesareas] @FInicial AS DATETIME, 
                                                      @FFinal AS   DATETIME
AS
     WITH FactuUCI
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
          FactuP
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
                     qx.TERTRAPAR AS TipoParto,
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
                  SELECT *
                  FROM FactuUCI
              )
                   AND i.STATUS = '1'),
          PRE
          AS (SELECT Mes, 
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
                     TipoParto, 
                     TipoPGP, 
                     COUNT(TipoPGP) OVER(PARTITION BY TipoPGP) AS NumEventosTipoPGP,
                     CASE
                         WHEN TipoPGP = 'CESAREA'
                         THEN 17
                         WHEN TipoPGP = 'PARTO NORMAL'
                         THEN 42
                         ELSE NULL
                     END EventosMeta,
                     CASE
                         WHEN TipoPGP = 'CESAREA'
                         THEN 757700
                         WHEN TipoPGP = 'PARTO NORMAL'
                         THEN 658959
                         ELSE NULL
                     END PGP_CME,
                     CASE
                         WHEN TipoPGP = 'CESAREA'
                         THEN(757700 * 17)
                         WHEN TipoPGP = 'PARTO NORMAL'
                         THEN(658959 * 42)
                         ELSE NULL
                     END PGP_VlrContrato
              FROM FactuP)
          SELECT *,
                 CASE
                     WHEN NumEventosTipoPGP > (EventosMeta * 1.1)
                     THEN(PGP_VlrContrato + ((NumEventosTipoPGP - (EventosMeta * 1.1)) * (PGP_CME / 2)))
                     WHEN NumEventosTipoPGP < (EventosMeta * 0.8)
                     THEN(PGP_VlrContrato + ((NumEventosTipoPGP - (EventosMeta * 0.8)) * (PGP_CME / 2))) --(PGP_VlrContrato - ((NumEventosTipoPGP-(EventosMeta * 0.8))*(PGP_CME/2)))
                     ELSE PGP_VlrContrato
                 END AS PGP_VlrContratoAjustado
          FROM PRE
          ORDER BY TipoPGP, 
                   TotalFactura DESC;
     RETURN 0;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera un reporte de facturas de partos y cesáreas facturadas bajo modalidad PGP (Pago Global Prospectivo) para una entidad pagadora específica (NIT 900935126), dentro de un rango de fechas. Cruza facturas de hospitalización con admisiones, datos del paciente, municipio de residencia, grupo de atención contractual y registros de atención en sala de partos (HCATINPAR) para clasificar cada factura como ''PARTO NORMAL'', ''CESAREA'' o ''NO APLICA''. Excluye las facturas correspondientes a estancias en UCI y calcula promedios, totales y conteos por tipo de parto usando funciones de ventana. Entrega un resumen gerencial con valores totales facturados, valor a cargo de la entidad, promedio PGP por tipo y número de eventos, útil para auditoría de contratos de maternidad, liquidación PGP y seguimiento de indicadores obstétricos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'USP_PGPfacturasPartosCesareas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'USP_PGPfacturasPartosCesareas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de facturación PGP (Pago Global Prospectivo) para partos y cesáreas de un tercero específico, calculando promedios, totales y ajuste de valor del contrato según desviación frente a metas pactadas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasPartosCesareas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proveer un rango de fechas de facturación (inicial y final).; Debe existir un tercero (ThirdParty) con NIT ''900935126'' asociado a una administradora de salud.; Las facturas deben tener estado ''1'' (activas) para ser consideradas.; Las categorías de factura involucradas deben existir con códigos ''03'', ''04'', ''05'' u ''08''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasPartosCesareas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen facturas del tercero con NIT ''900935126''.; Solo se consideran facturas con STATUS=''1''.; Solo categorías de factura ''03'',''04'',''05'',''08''.; Las facturas marcadas como UCI (CTE FactuUCI) se excluyen del reporte de partos/cesáreas.; La meta de eventos para CESAREA es 17 y para PARTO NORMAL es 42.; El CME (Costo Medio Esperado) es 757.700 para CESAREA y 658.959 para PARTO NORMAL.; El valor del contrato PGP se ajusta solo cuando los eventos están fuera de la banda 80%-110% de la meta.; El ajuste por desviación se calcula a la mitad del CME (CME/2) por evento fuera de banda.; Si el grupo de cuidado no contiene ''PGP'' en el nombre, no aplica clasificación PGP.; El procedimiento es de solo lectura: no modifica datos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasPartosCesareas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Pago Global Prospectivo (PGP); Parto normal; Cesárea; Factura; Ingreso/Admisión hospitalaria; Administradora de salud (EPS); Tercero pagador (NIT); Grupo de atención (CareGroup); Egreso hospitalario; Diagnóstico de egreso; Profesional de salud y especialidad; Municipio de residencia del paciente; UCI (exclusión); Meta de eventos contractuales; Costo Medio Esperado (CME); Ajuste de valor de contrato por desviación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasPartosCesareas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ResultSet: Devuelve facturas del NIT ''900935126'', con InvoiceDate en [@FInicial, @FFinal), STATUS=''1'' y categoría en (''03'',''04'',''05'',''08''), excluyendo facturas que califiquen como UCI; ordenadas por TipoPGP y TotalFactura DESC.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasPartosCesareas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si cg.Name NOT LIKE ''%PGP%'' → TipoPGP = ''NO APLICA'' else Se evalúa el tipo de parto; si qx.TERTRAPAR LIKE ''%CESAREA%'' → TipoPGP = ''CESAREA'' (meta 17 eventos, CME 757.700, contrato = 757700*17) else TipoPGP = ''PARTO NORMAL'' (meta 42 eventos, CME 658.959, contrato = 658959*42); si NumEventosTipoPGP > EventosMeta * 1.1 (sobreejecución >110%) → PGP_VlrContratoAjustado = PGP_VlrContrato + (exceso sobre 110% * CME/2) else Se evalúa subejecución; si NumEventosTipoPGP < EventosMeta * 0.8 (subejecución <80%) → PGP_VlrContratoAjustado = PGP_VlrContrato + (déficit bajo 80% * CME/2) (penalización por valor negativo) else PGP_VlrContratoAjustado = PGP_VlrContrato (sin ajuste dentro de banda 80%-110%)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasPartosCesareas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; dbo.ADINGRESO; dbo.INPACIENT; dbo.INUBICACI; dbo.INMUNICIP; Contract.CareGroup; Security.User; Contract.HealthAdministrator; Billing.InvoiceCategories; Common.ThirdParty; Security.Person; dbo.CHREGEGRE; dbo.INDIAGNOS; dbo.HCREGEGRE; dbo.INPROFSAL; dbo.INESPECIA; dbo.HCATINPAR', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasPartosCesareas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasPartosCesareas';
-- GO
