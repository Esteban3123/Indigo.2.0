CREATE PROCEDURE [dbo].[USP_PGPfacturasUCI] @FInicial AS DATETIME, 
                                           @FFinal AS   DATETIME
AS
     WITH FactuUCI
          AS (SELECT DISTINCT 
                     DATENAME(MONTH, i.InvoiceDate) + '/' + CAST(DATEPART(YEAR, i.InvoiceDate) AS VARCHAR) AS Mes, 
                     cg.Name AS EntidadAdministradora, 
                     ad.IPCODPACI AS Cedula, 
                     pa.IPNOMCOMP AS NombrePaciente, 
                     mun.MUNNOMBRE AS MunicipioResidencia, 
                     i.InvoiceNumber AS Factura, 
                     i.AdmissionNumber AS Ingreso, 
                     (i.TotalInvoice) AS TotalFactura, 
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
                   AND Cat.Code IN('03', '04', '05', '08', '16', '17')
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
                     TipoPGP, 
                     COUNT(TipoPGP) OVER(PARTITION BY TipoPGP) AS NumEventosTipoPGP,
                     CASE
                         WHEN TipoPGP = 'UCI'
                         THEN 19
                         ELSE NULL
                     END EventosMeta,
                     CASE
                         WHEN TipoPGP = 'UCI'
                         THEN 9000000
                         ELSE NULL
                     END PGP_CME,
                     CASE
                         WHEN TipoPGP = 'UCI'
                         THEN(9000000 * 19)
                         ELSE NULL
                     END PGP_VlrContrato
              FROM FactuUCI)
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de facturación bajo modelo de Pago Global Prospectivo (PGP) para servicios de UCI, filtrando facturas activas de una entidad pagadora específica (NIT 900935126) dentro de un rango de fechas. Consolida información de facturas, ingresos hospitalarios, datos del paciente (cédula, nombre, municipio de residencia) y grupo de atención del contrato, clasificando cada factura como ''UCI'' o ''NO APLICA'' según si el grupo de atención corresponde a un esquema PGP. Calcula indicadores de desempeño del contrato PGP: promedio y total de facturas, número de eventos, meta de eventos (19), costo medio por evento (CME de $9.000.000) y valor contratado ajustado según si los eventos superan el 110% o están por debajo del 80% de la meta pactada. Se usa para liquidación, seguimiento y control presupuestal del contrato PGP-UCI con la aseguradora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'USP_PGPfacturasUCI';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'USP_PGPfacturasUCI';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula y reporta el ajuste del valor de contrato PGP (Pago Global Prospectivo) de UCI para un tercero específico, comparando los eventos facturados contra una meta de 19 eventos a $9.000.000 c/u.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasUCI';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas de factura debe estar acotado por dos parámetros (inicio inclusivo, fin exclusivo).; Debe existir el tercero con NIT ''900935126'' en Common.ThirdParty asociado a la HealthAdministrator.; Las facturas consideradas deben tener estado ''1'' (activa/válida).; Las categorías de factura deben pertenecer al conjunto {''03'',''04'',''05'',''08'',''16'',''17''}.; Cada factura debe tener admisión con paciente, ubicación, municipio, grupo de cuidado, usuario facturador, categoría, diagnóstico de egreso y profesional de salud asociados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasUCI';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan facturas del tercero con NIT ''900935126''.; Solo se incluyen facturas en estado ''1'' y dentro de las categorías 03,04,05,08,16,17.; La meta contractual UCI está fija en 19 eventos a $9.000.000 cada uno (valor contrato $171.000.000).; La banda de tolerancia sin ajuste va de 80% a 110% de los eventos meta.; Tanto sobreejecución como subejecución se liquidan al 50% del CME.; Se considera ''UCI'' únicamente cuando el CareGroup contiene ''PGP'' en su nombre.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasUCI';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Pago Global Prospectivo (PGP); UCI (Unidad de Cuidados Intensivos); Factura; Ingreso/Admisión hospitalaria; Entidad Administradora (EPS/Pagador); Egreso hospitalario; Diagnóstico de egreso; Profesional de salud y especialidad; Municipio de residencia del paciente; CME (Costo Medio por Evento); Meta de eventos contractuales; Valor de contrato ajustado por desviación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasUCI';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve el listado de facturas filtradas con cálculo de PromedioPGP, TotalFacturasPGP, NumEventosTipoPGP y PGP_VlrContratoAjustado, ordenado por TipoPGP y TotalFactura DESC.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasUCI';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El nombre del CareGroup NO contiene ''PGP'' → Se clasifica el evento como TipoPGP = ''NO APLICA'' else Se clasifica como TipoPGP = ''UCI''; si TipoPGP = ''UCI'' → Se asignan EventosMeta=19, PGP_CME=9.000.000 y PGP_VlrContrato=171.000.000 (9.000.000*19) else EventosMeta, PGP_CME y PGP_VlrContrato quedan NULL; si NumEventosTipoPGP > EventosMeta * 1.1 (sobreejecución >10%) → PGP_VlrContratoAjustado = PGP_VlrContrato + (exceso sobre 110% de la meta) * (PGP_CME/2), pagando los eventos excedentes a la mitad del CME; si NumEventosTipoPGP < EventosMeta * 0.8 (subejecución <80%) → PGP_VlrContratoAjustado = PGP_VlrContrato + (déficit respecto al 80% de la meta) * (PGP_CME/2), descontando proporcionalmente; si NumEventosTipoPGP entre el 80% y el 110% de la meta → PGP_VlrContratoAjustado = PGP_VlrContrato sin ajuste (banda de tolerancia)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasUCI';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; dbo.ADINGRESO; dbo.INPACIENT; dbo.INUBICACI; dbo.INMUNICIP; Contract.CareGroup; Security.User; Contract.HealthAdministrator; Billing.InvoiceCategories; Common.ThirdParty; Security.Person; dbo.CHREGEGRE; dbo.INDIAGNOS; dbo.HCREGEGRE; dbo.INPROFSAL; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasUCI';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPfacturasUCI';
-- GO
