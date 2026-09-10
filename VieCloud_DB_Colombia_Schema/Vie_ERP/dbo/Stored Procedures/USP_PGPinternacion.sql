CREATE PROCEDURE [dbo].[USP_PGPinternacion] @FInicial AS DATETIME, 
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
                     i.InvoiceNumber
              FROM Billing.Invoice AS i
                   INNER JOIN Billing.InvoiceDetail AS id ON id.InvoiceId = i.Id
                   INNER JOIN dbo.ADINGRESO AS ad ON ad.NUMINGRES = i.AdmissionNumber
                   INNER JOIN dbo.CHREGEGRE AS EGR ON ad.NUMINGRES = EGR.NUMINGRES
                   INNER JOIN dbo.INPACIENT AS pa ON ad.IPCODPACI = pa.IPCODPACI
                   INNER JOIN Billing.ServiceOrderDetail AS sod ON sod.Id = id.ServiceOrderDetailId
                   INNER JOIN Billing.ServiceOrder so ON so.Id = sod.ServiceOrderId
                   INNER JOIN Security.[User] sou ON sou.UserCode = so.CreationUser
                   INNER JOIN Security.Person sop ON sop.Id = sou.IdPerson
                   INNER JOIN Contract.CareGroup AS cg ON cg.Id = i.CareGroupId
                   INNER JOIN Contract.CUPSEntity AS ce ON ce.Id = sod.CUPSEntityId
                   INNER JOIN dbo.INPROFSAL AS Pf ON sod.PerformsHealthProfessionalCode = Pf.CODPROSAL
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
              WHERE(i.STATUS = 1)
                   AND (i.InvoiceDate BETWEEN @FInicial AND @FFinal)
                   AND (ter.Nit = '900935126')),
          TF2
          AS (SELECT TF.InvoiceNumber
              FROM TF
              UNION ALL
              SELECT InvoiceNumber
              FROM FactuQx),
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
          IG --Internacion General
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
                         ELSE 'INTERNACION_GENERAL'
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
                  SELECT *
                  FROM TF2
              )
                   AND i.InvoiceNumber IN
              (
                  SELECT FE.InvoiceNumber
                  FROM FE
              )),
          PRE
          AS (SELECT IG.Mes, 
                     IG.EntidadAdministradora, 
                     IG.Cedula, 
                     IG.Factura, 
                     IG.Ingreso, 
                     IG.TotalFactura, 
                     IG.TotalEntidad, 
                     IG.TipoPGP, 
                     CAST((AVG(IG.TotalFactura) OVER(PARTITION BY IG.TipoPGP)) AS INT) AS PromedioPGP, 
                     CAST((SUM(TotalFactura) OVER(PARTITION BY TipoPGP)) AS INT) AS TotalFacturasPGP, 
                     COUNT(TipoPGP) OVER(PARTITION BY TipoPGP) AS NumEventosTipoPGP,
                     CASE
                         WHEN TipoPGP = 'INTERNACION_GENERAL'
                         THEN 238
                         ELSE NULL
                     END EventosMeta,
                     CASE
                         WHEN TipoPGP = 'INTERNACION_GENERAL'
                         THEN 1202086
                         ELSE NULL
                     END PGP_CME,
                     CASE
                         WHEN TipoPGP = 'INTERNACION_GENERAL'
                         THEN(1202086 * 238)
                         ELSE NULL
                     END PGP_VlrContrato
              FROM IG)
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de generación del informe de internación (hospitalización) para un período de fechas dado. Consolida información de facturas emitidas filtrando por una entidad pagadora específica (identificada por NIT 900935126) y combina tres tipos de eventos: partos y cesáreas (cruzando historia clínica de atención del parto con ingresos, pacientes, profesionales y especialidades), ingresos a UCI u otras categorías de hospitalización (agregando diagnósticos de egreso, médico tratante y municipio), y cirugías realizadas durante el ingreso (obteniendo los procedimientos quirúrgicos principales y secundarios por folio y por ingreso con sus conteos y ranking). El resultado integra admisiones, historia clínica, facturación, contratos y maestros de pacientes y profesionales para producir un reporte detallado de internación utilizado en auditoría, control de glosas y seguimiento de producción hospitalaria con la aseguradora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'USP_PGPinternacion';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'USP_PGPinternacion';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula y reporta el valor ajustado del contrato PGP de Internación General para un pagador específico, comparando eventos facturados de hospitalización (excluyendo partos, UCI y quirúrgicos) contra metas contractuales, en un rango de fechas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPinternacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas (@FInicial, @FFinal) debe estar definido; Deben existir facturas con STATUS=1 (activas/emitidas) en el rango; El tercero pagador con NIT ''900935126'' debe estar registrado como HealthAdministrator; Las facturas de estancia deben pertenecer al BillingGroup Id=9; Las categorías de UCI deben corresponder a los códigos ''03'',''04'',''05'',''08'' en InvoiceCategories', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPinternacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran facturas con STATUS=1 (activas); Solo facturas del pagador con NIT ''900935126''; Las facturas de Internación General excluyen explícitamente las que ya fueron clasificadas como partos, UCI o quirúrgicas (NOT IN TF2); Las facturas de Internación General deben contener al menos un servicio de estancia (BillingGroup Id=9); La meta contractual de eventos para Internación General es fija: 238 eventos; El costo medio del evento (PGP_CME) es fijo: 1.202.086; El umbral superior de cumplimiento es 110% de la meta y el inferior 80%; El ajuste por desviación se calcula a la mitad del PGP_CME (PGP_CME/2); Los resultados se ordenan por TotalFactura descendente y luego por TipoPGP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPinternacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Pago Global Prospectivo (PGP); Internación General / Hospitalización; Partos y cesáreas; UCI (Unidad de Cuidados Intensivos); Procedimientos quirúrgicos; Estancias hospitalarias; Facturación a EPS/Administradora de Salud; Especialidad médica; Ingreso/Admisión del paciente; Diagnóstico de egreso; Meta contractual de eventos; Costo Medio por Evento (CME); Ajuste de valor de contrato por desviación de meta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPinternacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve facturas de Internación General del pagador NIT ''900935126'' (excluyendo facturas asociadas a partos, UCI y quirúrgicos) con métricas PGP y valor de contrato ajustado por sobrecumplimiento/subcumplimiento de meta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPinternacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si cg.Name NOT LIKE ''%PGP%'' → Marca TipoPGP=''NO APLICA'' else Marca TipoPGP=''INTERNACION_GENERAL''; si TipoPGP = ''INTERNACION_GENERAL'' → Asigna EventosMeta=238, PGP_CME=1.202.086 y PGP_VlrContrato=1.202.086*238 else Asigna NULL a métricas PGP; si NumEventosTipoPGP > EventosMeta * 1.1 → Ajusta el valor del contrato sumando (exceso sobre 110% de meta) * (PGP_CME/2); si NumEventosTipoPGP < EventosMeta * 0.8 → Ajusta el valor del contrato sumando (déficit bajo 80% de meta) * (PGP_CME/2) (resta, ya que el déficit es negativo) else Mantiene PGP_VlrContrato sin ajuste cuando los eventos están entre 80% y 110% de la meta; si Categoría de factura Cat.Code IN (''03'',''04'',''05'',''08'') → Clasifica la factura como UCI y la excluye del conteo de Internación General; si Factura asociada a HCATINPAR (atención de parto) → Clasifica como Partos/Cesáreas y la excluye del conteo de Internación General; si Factura tiene detalle con CUPSEntity asociado a procedimiento quirúrgico (HCQXINFOR/HCQXREALI) → Clasifica como facturación quirúrgica y la excluye del conteo de Internación General; si Factura pertenece al BillingGroup Id=9 (estancias) → La incluye en el universo de Internación General else La excluye del cálculo PGP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPinternacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Billing.ServiceOrder; Billing.InvoiceCategories; Billing.BillingGroup; Contract.CareGroup; Contract.HealthAdministrator; Contract.CUPSEntity; Common.ThirdParty; Security.User; Security.Person; dbo.ADINGRESO; dbo.INPACIENT; dbo.INUBICACI; dbo.INMUNICIP; dbo.HCATINPAR; dbo.INPROFSAL; dbo.INESPECIA; dbo.CHREGEGRE; dbo.HCREGEGRE; dbo.INDIAGNOS; dbo.CHCAMASHO; dbo.HCQXINFOR; dbo.HCQXREALI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPinternacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_PGPinternacion';
-- GO
