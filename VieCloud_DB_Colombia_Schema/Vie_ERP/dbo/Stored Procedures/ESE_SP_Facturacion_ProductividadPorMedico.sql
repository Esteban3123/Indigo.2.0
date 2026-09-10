CREATE PROCEDURE [dbo].[ESE_SP_Facturacion_ProductividadPorMedico] @UserCode VARCHAR(20), 
                                                                  @FechaIni DATETIME, 
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
          AND ((dq.PerformsHealthProfessionalCode IS NULL
                AND dos.PerformsHealthProfessionalCode = @UserCode)
               OR ((dos.PerformsHealthProfessionalCode IS NULL
                    AND dq.PerformsHealthProfessionalCode = @UserCode)))
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Informe de productividad facturada por médico: dado un código de profesional de la salud y un rango de fechas, consolida todos los servicios, procedimientos, medicamentos e insumos que ese médico realizó y que quedaron incluidos en facturas activas (no anuladas). Cruza las facturas con los ingresos o admisiones del paciente, el centro de atención, el detalle de facturación y el detalle quirúrgico, para devolver por cada ítem el código y nombre del servicio o producto (CUPS o inventario), la sede donde se prestó, la cantidad facturada, el valor unitario y el total. Se usa para medir la producción clínica y económica de cada médico en un período determinado, útil en reportes de liquidación de honorarios, control de productividad médica y auditoría de facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Facturacion_ProductividadPorMedico';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Facturacion_ProductividadPorMedico';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta la productividad facturada (cantidades, valor unitario y total) de servicios, medicamentos e insumos atribuidos a un profesional de salud en un rango de fechas, agrupada por centro de atención y código.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_ProductividadPorMedico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las fechas de filtro deben ser válidas y comparables como cadena en formato 105 (dd-mm-yyyy).; El código de usuario corresponde a un profesional de salud registrado en el detalle de orden o en el detalle quirúrgico.; Las facturas deben existir con detalle, ingreso asociado y centro de atención válido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_ProductividadPorMedico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada línea reportada se atribuye a un único profesional: el del detalle quirúrgico si existe, o el del detalle de orden en caso contrario.; Solo se incluyen líneas con un único origen de precio válido (>0) entre detalle de orden o detalle quirúrgico, no ambos simultáneamente.; El total se calcula como cantidad facturada por valor unitario seleccionado.; No se incluyen detalles lógicamente eliminados ni facturas con estado distinto de ''1''.; Los servicios/productos provienen exclusivamente del catálogo de productos de inventario o del catálogo CUPS, con prioridad para productos de inventario.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_ProductividadPorMedico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Detalle de factura; Orden de servicio; Servicio quirúrgico; Honorarios médicos; Profesional de salud; Centro de atención; Ingreso/admisión del paciente; CUPS; Producto de inventario (medicamentos/insumos); Productividad médica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_ProductividadPorMedico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.Invoice: Devuelve solo facturas con STATUS=''1'' (activas/vigentes) cuya fecha de factura esté entre @FechaIni y @FechaFin.; [RETURN_RESULT] Billing.ServiceOrderDetail: Excluye detalles marcados como eliminados (IsDelete=''0'') y devuelve cantidad facturada y valor unitario agregados.; [RETURN_RESULT] Billing.ServiceOrderDetailSurgical: Solo se considera el detalle quirúrgico cuando OnlyMedicalFees=''0'' (no exclusivo de honorarios médicos).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_ProductividadPorMedico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si dq.PerformsHealthProfessionalCode IS NULL → Toma el profesional y nombre desde el detalle de orden (dos/med). else Toma el profesional y nombre desde el detalle quirúrgico (dq/medqx).; si dos.RecordType = ''1'' → Clasifica la línea como ''Servicios''. else Si RecordType=''2'' clasifica como ''Medi/Insumo''.; si pr.Code IS NULL (no es producto de inventario) → Usa el código y descripción del catálogo CUPS. else Usa el código y nombre del producto de inventario.; si dq.TotalSalesPrice IS NULL AND dos.TotalSalesPrice > 0 → Considera el precio del detalle de orden como valor unitario. else Si dos.TotalSalesPrice IS NULL AND dq.TotalSalesPrice > 0, considera el precio del detalle quirúrgico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_ProductividadPorMedico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceDetail; Security.User; Security.Person; dbo.ADINGRESO; dbo.ADCENATEN; Billing.ServiceOrderDetail; Billing.ServiceOrderDetailSurgical; Contract.IPSService; Inventory.InventoryProduct; Contract.CUPSEntity; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_ProductividadPorMedico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_ProductividadPorMedico';
-- GO
