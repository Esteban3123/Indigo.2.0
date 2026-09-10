CREATE PROCEDURE [dbo].[ESE_SP_Facturacion_ProductividadPorOdontologo] @FechaIni DATETIME, 
                                                                      @FechaFin DATETIME
AS
     SELECT CEN.NOMCENATE AS CentroAtencion,
            CASE
                WHEN dq.PerformsHealthProfessionalCode IS NULL
                THEN dos.PerformsHealthProfessionalCode
                ELSE dq.PerformsHealthProfessionalCode
            END AS CodigoMedico,
            CASE
                WHEN RTRIM(medqx.NOMMEDICO) IS NULL
                THEN RTRIM(med.NOMMEDICO)
                ELSE RTRIM(medqx.NOMMEDICO)
            END AS NombreMedico,
            CASE dos.RecordType
                WHEN '1'
                THEN 'Servicios'
                WHEN '2'
                THEN 'Medi/Insumo'
            END AS ServiciosMedicamentos,
            CASE
                WHEN pr.Code IS NULL
                THEN Cups.Code
                ELSE pr.Code
            END AS Codigo,
            CASE
                WHEN pr.Name IS NULL
                THEN cups.Description
                ELSE pr.Name
            END AS Descripción, 
            SUM(dos.InvoicedQuantity) AS Cantidad,
            CASE
                WHEN MIN(dq.TotalSalesPrice) IS NULL
                THEN MIN(dos.TotalSalesPrice)
                ELSE MIN(dq.TotalSalesPrice)
            END AS ValorUnitario, 
            (SUM(dos.InvoicedQuantity) * CASE
                                             WHEN MIN(dq.TotalSalesPrice) IS NULL
                                             THEN MIN(dos.TotalSalesPrice)
                                             ELSE MIN(dq.TotalSalesPrice)
                                         END) AS Total
     FROM Billing.Invoice AS F
          INNER JOIN Billing.InvoiceDetail AS DF WITH(NOLOCK) ON DF.InvoiceId = F.Id
          INNER JOIN Security.[User] AS u ON u.UserCode = F.InvoicedUser
          INNER JOIN Security.Person AS per ON per.Id = u.IdPerson
          INNER JOIN dbo.ADINGRESO AS ing WITH(NOLOCK) ON CAST(ing.NUMINGRES AS INT) = F.AdmissionNumber
          INNER JOIN DBO.ADCENATEN AS CEN WITH(NOLOCK) ON CEN.CODCENATE = ing.CODCENATE
          INNER JOIN Billing.ServiceOrderDetail AS dos WITH(NOLOCK) ON dos.Id = DF.ServiceOrderDetailId
          LEFT OUTER JOIN Billing.ServiceOrderDetailSurgical AS dq WITH(NOLOCK) ON dq.ServiceOrderDetailId = dos.Id
                                                                                   AND dq.OnlyMedicalFees = '0'
          LEFT OUTER JOIN Contract.IPSService AS ServiciosIPS WITH(NOLOCK) ON ServiciosIPS.Id = dos.IPSServiceId
          LEFT OUTER JOIN Inventory.InventoryProduct AS pr WITH(NOLOCK) ON pr.Id = dos.ProductId
          LEFT OUTER JOIN Contract.CUPSEntity AS Cups WITH(NOLOCK) ON cups.Id = dos.CUPSEntityId
          LEFT OUTER JOIN dbo.INPROFSAL AS med WITH(NOLOCK) ON med.CODPROSAL = dos.PerformsHealthProfessionalCode
          LEFT OUTER JOIN dbo.INPROFSAL AS medqx WITH(NOLOCK) ON medqx.CODPROSAL = dq.PerformsHealthProfessionalCode
     WHERE((dq.TotalSalesPrice IS NULL
            AND dos.TotalSalesPrice > 0)
           OR ((dos.TotalSalesPrice IS NULL
                AND dq.TotalSalesPrice > 0)))
          AND (F.STATUS = '1')
          AND (dos.IsDelete = '0')
          AND CONVERT(VARCHAR(10), F.InvoiceDate, 105) >= @FechaIni
          AND CONVERT(VARCHAR(10), F.InvoiceDate, 105) <= @FechaFin
          AND ((dq.PerformsHealthProfessionalCode IS NULL)
               OR ((dos.PerformsHealthProfessionalCode IS NULL)))
          AND med.TIPPROFES = 5
     GROUP BY CEN.NOMCENATE,
              CASE
                  WHEN dq.PerformsHealthProfessionalCode IS NULL
                  THEN dos.PerformsHealthProfessionalCode
                  ELSE dq.PerformsHealthProfessionalCode
              END,
              CASE
                  WHEN RTRIM(medqx.NOMMEDICO) IS NULL
                  THEN RTRIM(med.NOMMEDICO)
                  ELSE RTRIM(medqx.NOMMEDICO)
              END,
              CASE dos.RecordType
                  WHEN '1'
                  THEN 'Servicios'
                  WHEN '2'
                  THEN 'Medi/Insumo'
              END,
              CASE
                  WHEN pr.Code IS NULL
                  THEN Cups.Code
                  ELSE pr.Code
              END,
              CASE
                  WHEN pr.Name IS NULL
                  THEN cups.Description
                  ELSE pr.Name
              END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de productividad odontológica por profesional de la salud en un rango de fechas. Consolida los servicios y medicamentos/insumos facturados (facturas activas, no anuladas) agrupados por centro de atención, odontólogo ejecutante, tipo de registro (servicio o medicamento/insumo), código y descripción del procedimiento o producto. Para cada combinación calcula la cantidad facturada, el valor unitario y el total. Cruza facturas (Billing.Invoice) con sus detalles (Billing.InvoiceDetail), órdenes de servicio (Billing.ServiceOrderDetail), detalle quirúrgico (Billing.ServiceOrderDetailSurgical), catálogos de servicios CUPS e IPS, inventario de productos y el maestro de profesionales de la salud (INPROFSAL), filtrando únicamente odontólogos (tipo de profesional = 5). Se utiliza para medir la productividad y los ingresos generados por cada odontólogo en un período determinado, apoyando la gestión de facturación y análisis de rendimiento del equipo odontológico por sede.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Facturacion_ProductividadPorOdontologo';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Facturacion_ProductividadPorOdontologo';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta la productividad facturada (cantidad, valor unitario y total) por odontólogo, agrupando por centro de atención, profesional, tipo de ítem (servicio o medicamento/insumo) y código de servicio o producto en un rango de fechas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_ProductividadPorOdontologo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las fechas de inicio y fin deben estar definidas para acotar la facturación a evaluar.; Existen facturas con estado activo (''1'') y detalles de orden de servicio no eliminados (IsDelete=''0'').; El profesional asociado debe estar registrado con tipo de profesión 5 (odontólogo) en INPROFSAL.; El número de admisión de la factura debe ser convertible a entero para enlazar con ADINGRESO.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_ProductividadPorOdontologo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran facturas en estado ''1'' (activas/vigentes).; Solo se consideran detalles de orden de servicio no eliminados lógicamente (IsDelete=''0'').; Solo se reportan profesionales de tipo odontólogo (INPROFSAL.TIPPROFES = 5).; Una línea solo aporta cuando hay precio en la parte quirúrgica o en la orden de servicio, pero no en ambas (excluyente).; El detalle quirúrgico considerado excluye los registros marcados como solo honorarios médicos (OnlyMedicalFees=''0'').; El profesional reportado proviene de uno solo de los dos orígenes (quirúrgico u orden de servicio), nunca duplicado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_ProductividadPorOdontologo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Productividad médica; Odontólogo; Centro de atención; Factura; Orden de servicio; Detalle quirúrgico; Honorarios médicos; CUPS; Medicamento/Insumo; Servicio de salud (IPS); Admisión/Ingreso del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_ProductividadPorOdontologo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve filas agregadas (SUM de cantidad, MIN de precio) solo cuando F.STATUS=''1'', dos.IsDelete=''0'', el profesional es de tipo odontólogo (TIPPROFES=5) y la fecha de factura está entre @FechaIni y @FechaFin.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_ProductividadPorOdontologo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si dq.PerformsHealthProfessionalCode IS NULL (no hay profesional quirúrgico asignado) → Usa el profesional de la orden de servicio (dos.PerformsHealthProfessionalCode) y el nombre desde med.NOMMEDICO else Usa el profesional quirúrgico (dq.PerformsHealthProfessionalCode) y el nombre desde medqx.NOMMEDICO; si dos.RecordType = ''1'' → Clasifica la línea como ''Servicios'' else Si RecordType=''2'' la clasifica como ''Medi/Insumo''; si pr.Code IS NULL (no es producto de inventario) → Toma el código y descripción desde el catálogo CUPS (Cups.Code, cups.Description) else Toma el código y nombre desde el producto de inventario (pr.Code, pr.Name); si dq.TotalSalesPrice IS NULL y dos.TotalSalesPrice > 0, o dos.TotalSalesPrice IS NULL y dq.TotalSalesPrice > 0 → Incluye la línea en el reporte tomando el precio disponible (no nulo y mayor a 0)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_ProductividadPorOdontologo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceDetail; Security.User; Security.Person; dbo.ADINGRESO; dbo.ADCENATEN; Billing.ServiceOrderDetail; Billing.ServiceOrderDetailSurgical; Contract.IPSService; Inventory.InventoryProduct; Contract.CUPSEntity; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_ProductividadPorOdontologo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_ProductividadPorOdontologo';
-- GO
