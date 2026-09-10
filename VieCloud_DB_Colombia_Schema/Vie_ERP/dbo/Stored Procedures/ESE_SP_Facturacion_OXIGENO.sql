CREATE PROCEDURE [dbo].[ESE_SP_Facturacion_OXIGENO] @FechaIni DATETIME, 
                                                   @FechaFin DATETIME
AS
     SELECT DISTINCT 
            F.InvoiceNumber AS Factura, 
            F.AdmissionNumber AS Ingreso, 
            F.PatientCode AS Identificacion, 
            P.IPPRIAPEL AS Apellido1, 
            P.IPSEGAPEL AS Apellido2, 
            P.IPPRINOMB AS Nombre1, 
            P.IPSEGNOMB AS Nombre2, 
            ga.Name AS GrupoAtención, 
            F.InvoiceDate AS [Fecha Factura], 
            dos.InvoicedQuantity AS Cantidad,
            CASE
                WHEN dq.TotalSalesPrice IS NULL
                THEN dos.TotalSalesPrice
                ELSE dq.TotalSalesPrice
            END AS ValorUnitario,
            CASE
                WHEN dq.TotalSalesPrice IS NULL
                THEN DF.GrandTotalSalesPrice
                ELSE dq.TotalSalesPrice
            END AS ValorTotal, 
            t.Nit, 
            ea.Code + ' - ' + ea.Name AS [Entidad Administradora],
            CASE dos.RecordType
                WHEN '1'
                THEN 'Servicios'
                WHEN '2'
                THEN 'Medicamentos'
            END AS ServiciosMedicamentos, 
            Cups.Code AS Cups, 
            cups.Description AS NombreCups,
            CASE
                WHEN pr.Code IS NULL
                THEN ServiciosIPS.Code
                ELSE pr.Code
            END AS Código,
            CASE
                WHEN pr.Name IS NULL
                THEN ServiciosIPS.Name
                ELSE pr.Name
            END AS Descripción,
            CASE dos.Presentation
                WHEN '1'
                THEN 'No Quirúrgico'
                WHEN '2'
                THEN 'Quirúrgico'
                WHEN '3'
                THEN 'Paquete'
            END AS PresentacionServicio, 
            ServiciosIPSQ.Code AS Subcodigo, 
            ServiciosIPSQ.Name AS Subnombre,
            CASE
                WHEN dq.PerformsHealthProfessionalCode IS NULL
                THEN dos.PerformsHealthProfessionalCode
                ELSE dq.PerformsHealthProfessionalCode
            END AS CodigoMèdico, 
            IIF(ISNULL(os.EntityName, '') = 'PharmaceuticalDispensing', (STUFF(
     (
         SELECT DISTINCT 
                CHAR(13) + CHAR(10) + ' - ' + w.Code + ': ' + w.Name
         FROM Inventory.PharmaceuticalDispensing pd
              JOIN Inventory.PharmaceuticalDispensingDetail pdd ON pd.Id = pdd.PharmaceuticalDispensingId
                                                                   AND pdd.ProductId = dos.ProductId
              JOIN Inventory.Warehouse w ON pdd.WarehouseId = w.Id
         WHERE pd.Id = os.EntityId FOR XML PATH(N''), TYPE
     ).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')), '') Almacenes
     FROM Billing.Invoice AS F WITH(NOLOCK)
          INNER JOIN Billing.InvoiceDetail AS DF WITH(NOLOCK) ON DF.InvoiceId = F.Id
          INNER JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) ON rcd.Id = F.RevenueControlDetailId
          INNER JOIN Security.[User] AS u ON u.UserCode = F.InvoicedUser
          INNER JOIN Security.Person AS per ON per.Id = u.IdPerson
          INNER JOIN dbo.ADINGRESO AS ing WITH(NOLOCK) ON CAST(ing.NUMINGRES AS INT) = F.AdmissionNumber
          INNER JOIN DBO.ADCENATEN AS CEN WITH(NOLOCK) ON CEN.CODCENATE = ing.CODCENATE
          INNER JOIN Billing.ServiceOrderDetail AS dos WITH(NOLOCK) ON dos.Id = DF.ServiceOrderDetailId
          INNER JOIN Common.ThirdParty AS t WITH(NOLOCK) ON t.Id = F.ThirdPartyId
          INNER JOIN Contract.CareGroup AS ga WITH(NOLOCK) ON ga.Id = F.CareGroupId
          INNER JOIN Contract.HealthAdministrator AS ea WITH(NOLOCK) ON ea.Id = F.HealthAdministratorId
          INNER JOIN Billing.InvoiceCategories AS cat WITH(NOLOCK) ON cat.Id = F.InvoiceCategoryId
          LEFT OUTER JOIN Contract.IPSService AS ServiciosIPS WITH(NOLOCK) ON ServiciosIPS.Id = dos.IPSServiceId
          LEFT OUTER JOIN Inventory.InventoryProduct AS pr WITH(NOLOCK) ON pr.Id = dos.ProductId
          LEFT OUTER JOIN Contract.CUPSEntity AS Cups WITH(NOLOCK) ON cups.Id = dos.CUPSEntityId
          LEFT OUTER JOIN dbo.INPROFSAL AS med WITH(NOLOCK) ON med.CODPROSAL = dos.PerformsHealthProfessionalCode
          LEFT OUTER JOIN dbo.INESPECIA AS espmed WITH(NOLOCK) ON espmed.CODESPECI = med.CODESPEC1
          LEFT OUTER JOIN dbo.INDIAGNOS AS diag ON diag.CODDIAGNO = ing.CODDIAEGR
          LEFT OUTER JOIN dbo.HCREGEGRE AS salida WITH(NOLOCK) ON CAST(salida.NUMINGRES AS INT) = F.AdmissionNumber
          LEFT OUTER JOIN Billing.ServiceOrderDetailSurgical AS dq WITH(NOLOCK) ON dq.ServiceOrderDetailId = dos.Id
                                                                                   AND dq.OnlyMedicalFees = '0'
          LEFT OUTER JOIN dbo.INPROFSAL AS medqx WITH(NOLOCK) ON medqx.CODPROSAL = dq.PerformsHealthProfessionalCode
          LEFT OUTER JOIN dbo.INESPECIA AS espqx ON espqx.CODESPECI = medqx.CODESPEC1
          LEFT OUTER JOIN dbo.INPACIENT AS P WITH(NOLOCK) ON P.IPCODPACI = ing.IPCODPACI
          LEFT OUTER JOIN dbo.INUBICACI AS Ubi WITH(NOLOCK) ON Ubi.AUUBICACI = P.AUUBICACI
          LEFT OUTER JOIN dbo.ADCOMUNAS AS C WITH(NOLOCK) ON C.Id = Ubi.IDCOMUNA
          LEFT OUTER JOIN Contract.IPSService AS ServiciosIPSQ WITH(NOLOCK) ON ServiciosIPSQ.Id = dq.IPSServiceId
          LEFT OUTER JOIN Payroll.FunctionalUnit AS UF WITH(NOLOCK) ON UF.Id = dos.PerformsFunctionalUnitId
          LEFT OUTER JOIN Billing.ServiceOrder AS os WITH(NOLOCK) ON os.Id = dos.ServiceOrderId
          LEFT OUTER JOIN Billing.BillingGroup bg WITH(NOLOCK) ON bg.Id = IIF(ISNULL(Cups.ApplyRIAS, 0) = 1, Cups.RIASBillingGroupId, Cups.BillingGroupId)
          LEFT OUTER JOIN.RIASCUPS rc WITH(NOLOCK) ON rc.ID = dos.RIASCupsId
          LEFT OUTER JOIN.RIAS r WITH(NOLOCK) ON r.ID = rc.IDRIAS
     WHERE(F.STATUS = '1')
          AND (dos.IsDelete = '0')
          AND (F.InvoiceDate >= @FechaIni)
          AND (F.InvoiceDate <= @FechaFin)
          AND Cups.Code IN('S55201', 'S55202', 'S55203', 'S55204', 'S55205', 'S55206', 'S55207', 'S55208', 'S55209')
     ORDER BY F.InvoiceDate;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de facturación especializado en servicios de oxígeno, que genera un reporte detallado de las facturas emitidas en un rango de fechas. Consolida información de encabezado de factura (número, fecha, paciente, ingreso), líneas de detalle de servicios y medicamentos facturados, entidad administradora (EPS/aseguradora), grupo de atención del contrato y NIT del tercero pagador. Cruza datos del módulo de Billing con la historia clínica y admisiones (ADINGRESO, INPACIENT) para obtener nombre completo del paciente, e incluye códigos CUPS, descripción del servicio o medicamento, cantidad facturada, valor unitario y valor total. Para ítems dispensados desde farmacia, también informa los almacenes de despacho. Sirve para auditoría de facturación, conciliación con EPS y seguimiento de ingresos por oxigenoterapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Facturacion_OXIGENO';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Facturacion_OXIGENO';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de facturación de servicios de oxígeno (CUPS S55201–S55209) emitidos en un rango de fechas, con datos del paciente, ingreso, entidad administradora y almacenes de dispensación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_OXIGENO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las fechas de entrada deben acotar un rango válido sobre InvoiceDate.; Las facturas consideradas deben estar en estado activo (STATUS=''1'').; El detalle de orden de servicio no debe estar marcado como eliminado (IsDelete=''0'').; Los códigos CUPS evaluados deben pertenecer al conjunto de oxígeno (S55201–S55209).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_OXIGENO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen facturas activas (STATUS=''1'').; Solo se incluyen detalles de orden vigentes (IsDelete=''0'').; El reporte se restringe a CUPS de oxígeno: S55201 a S55209.; El detalle quirúrgico se considera únicamente cuando OnlyMedicalFees=''0''.; Los almacenes solo se listan cuando la orden de servicio proviene de una dispensación farmacéutica.; El procedimiento es de solo lectura (no modifica datos).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_OXIGENO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Facturación; Oxígeno (CUPS S55201–S55209); Paciente; Ingreso/Admisión; Entidad Administradora (EPS); Grupo de atención; Servicios y Medicamentos; CUPS; Servicios IPS; Dispensación farmacéutica; Almacenes de inventario; Profesional de salud; Presentación quirúrgica/no quirúrgica/paquete; RIAS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_OXIGENO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas únicas (DISTINCT) de facturas con STATUS=''1'', detalle no borrado y CUPS de oxígeno (S55201–S55209) dentro del rango de fechas, ordenadas por fecha de factura.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_OXIGENO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si dq.TotalSalesPrice IS NULL (no existe detalle quirúrgico aplicable) → Toma valor unitario de dos.TotalSalesPrice y valor total de DF.GrandTotalSalesPrice else Toma valor unitario y total desde dq.TotalSalesPrice (detalle quirúrgico); si dos.RecordType = ''1'' → Clasifica la línea como ''Servicios'' else Si RecordType=''2'' la clasifica como ''Medicamentos''; si pr.Code IS NULL (no hay producto de inventario asociado) → Usa Código y Descripción del servicio IPS (ServiciosIPS) else Usa Código y Descripción del producto de inventario (pr); si dos.Presentation = ''1'' | ''2'' | ''3'' → Etiqueta la presentación como ''No Quirúrgico'', ''Quirúrgico'' o ''Paquete'' respectivamente; si dq.PerformsHealthProfessionalCode IS NULL → Reporta el código médico de la orden no quirúrgica (dos) else Reporta el código médico del detalle quirúrgico (dq); si os.EntityName = ''PharmaceuticalDispensing'' → Concatena los almacenes (Code: Name) de la dispensación farmacéutica asociada al producto else Devuelve cadena vacía en Almacenes; si Cups.ApplyRIAS = 1 → Usa Cups.RIASBillingGroupId para el grupo de facturación else Usa Cups.BillingGroupId', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_OXIGENO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; Inventory.Warehouse; Inventory.InventoryProduct; Billing.Invoice; Billing.InvoiceDetail; Billing.RevenueControlDetail; Billing.ServiceOrderDetail; Billing.ServiceOrderDetailSurgical; Billing.ServiceOrder; Billing.InvoiceCategories; Billing.BillingGroup; Security.User; Security.Person; Common.ThirdParty; Contract.CareGroup; Contract.HealthAdministrator; Contract.IPSService; Contract.CUPSEntity; Payroll.FunctionalUnit; dbo.ADINGRESO; dbo.ADCENATEN; dbo.INPROFSAL; dbo.INESPECIA; dbo.INDIAGNOS; dbo.HCREGEGRE; dbo.INPACIENT; dbo.INUBICACI; dbo.ADCOMUNAS; RIASCUPS (+1 adicionales)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_OXIGENO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_OXIGENO';
-- GO
