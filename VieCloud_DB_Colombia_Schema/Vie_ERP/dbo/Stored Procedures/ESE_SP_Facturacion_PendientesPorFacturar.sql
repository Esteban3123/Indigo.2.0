CREATE PROCEDURE [dbo].[ESE_SP_Facturacion_PendientesPorFacturar]
AS
     SELECT DISTINCT 
            ing.CODCENATE AS CodigoCentro, 
            CEN.NOMCENATE AS CentroAtencion, 
            ING.IESTADOIN AS EstadoIngreso, 
            so.AdmissionNumber AS Ingreso, 
            ing.ifechaing AS FechaIngreso, 
            Billing.RevenueControl.PatientCode AS Identificación, 
            P.IPCODPACI AS Codigopac, 
            p.IPNOMCOMP AS Paciente, 
            t.Name AS Entidad, 
            Billing.RevenueControlDetail.FolioOrder AS [Folios a facturar],
            CASE Billing.RevenueControlDetail.FolioType
                WHEN '1'
                THEN 'EAPB con contrato'
                WHEN '2'
                THEN 'EAPB sin contrato'
                WHEN '3'
                THEN 'Particulares'
                WHEN '4'
                THEN 'Aseguradoras'
            END AS Tipo,
            CASE Billing.RevenueControlDetail.LiquidationType
                WHEN '1'
                THEN 'Pago por servicios'
                WHEN '2'
                THEN 'Capitacion'
                WHEN '3'
                THEN 'Factura Global'
                WHEN '4'
                THEN 'Capitacion Global'
            END AS [Tipo de liquidacion], 
            ea.HealthEntityCode AS [Entidad Administradora], 
            t.Nit AS [Nit Entidad], 
            ga.Code AS [Grupo Atención], 
            ga.Name AS [Descripción Grupo Atención], 
            Billing.RevenueControlDetail.TotalFolio AS [Vr total folio pendiente facturar], 
            dq.InvoicedQuantity AS [Cantidad QX], 
            dq.TotalSalesPrice AS [Total Facturado QX], 
            dq.PerformsHealthProfessionalCode AS [Cód Profesional QX], 
            RTRIM(medqx.CODPROSAL) + ' - ' + LTRIM(medqx.NOMMEDICO) AS [Profesional QX],
            CASE Billing.RevenueControlDetail.ResponsibleRecoveryFee
                WHEN '1'
                THEN 'Ninguno'
                WHEN '2'
                THEN 'Paciente'
                WHEN '3'
                THEN 'Tercero'
            END AS [Responsable cuota recuperación], 
            Billing.RevenueControlDetail.TotalPatientWithDiscount AS [Vr cobrado a Paciente], 
            Billing.RevenueControlDetail.ValueCopay AS [Vr cuota recuperación folio], 
            Billing.RevenueControlDetail.ValueFeeModerator AS [Vr cuota moderadora folio], 
            Billing.RevenueControlDetail.Observation AS [Observaciones], 
            cat.Name AS [Categoría para RIPS],
            CASE Billing.RevenueControlDetail.STATUS
                WHEN '1'
                THEN 'Registrado'
                WHEN '2'
                THEN 'Facturado'
                WHEN '3'
                THEN 'Bloqueado'
            END AS 'Estado', 
            per.Fullname AS [Usuario Crea], 
            Billing.RevenueControlDetail.CreationDate AS [Fecha creación], 
            PERM.Fullname AS [Usuario modifica], 
            Billing.RevenueControlDetail.ModificationDate AS [Fecha modificación],
            CASE Billing.ServiceOrderDetail.ServiceType
                WHEN '1'
                THEN 'SOAT'
                WHEN '2'
                THEN 'ISS'
                WHEN '3'
                THEN 'CUPS'
            END AS [Tipo Servicio],
            CASE Billing.ServiceOrderDetail.RecordType
                WHEN '1'
                THEN 'Servicios'
                WHEN '2'
                THEN 'Medicamentos'
            END AS [Servicios/Medicamentos], 
            CUPS.Code AS [Código CUPS], 
            CUPS.Description AS [Descripción CUPS], 
            ServicioSIPS.Code AS [Código Servicio], 
            ServicioSIPS.Name AS [Descripción Servicio], 
            Billing.ServiceOrderDetail.Packaging AS [Servicio incluido en Paquete],
            CASE Billing.ServiceOrderDetail.Presentation
                WHEN '1'
                THEN 'No Quirúrgico'
                WHEN '2'
                THEN 'Quirúrgico'
                WHEN '3'
                THEN 'Paquete'
            END AS [Presentación Servicio], 
            pr.Code AS [Cód Producto], 
            pr.Name AS [Descripción Producto], 
            Billing.ServiceOrderDetail.SupplyQuantity AS [Cantidad entregada], 
            Billing.ServiceOrderDetail.DevolutionQuantity AS [Cantidad devuelta], 
            Billing.ServiceOrderDetail.InvoicedQuantity AS [Cantidad a facturar], 
            Billing.ServiceOrderDetail.RateManualSalePrice AS [Valor unitario  a facturar ], 
            (Billing.ServiceOrderDetail.InvoicedQuantity) * (Billing.ServiceOrderDetail.RateManualSalePrice) AS [Total a facturar], 
            Billing.RevenueControlDetail.TotalPatientSalesPrice AS [Total Cuota Recuperación], 
            Billing.ServiceOrderDetail.ServiceDate AS [Fecha Servicio], 
            Billing.ServiceOrderDetail.AuthorizationNumber AS [Autorización], 
            uf.Code AS [Unidad Funcional], 
            uf.Name AS [Descripción Unidad Funcional], 
            salida.fecaltpac AS [Fecha Alta médica], 
            ing.ufuegrmed AS UnidadEgreso
     FROM Billing.RevenueControlDetail WITH(NOLOCK)
          INNER JOIN Billing.ServiceOrderDetailDistribution WITH(NOLOCK) ON Billing.RevenueControlDetail.Id = Billing.ServiceOrderDetailDistribution.RevenueControlDetailId
                                                                            AND Billing.RevenueControlDetail.STATUS IN('1', '3')
          INNER JOIN Billing.ServiceOrderDetail WITH(NOLOCK) ON Billing.ServiceOrderDetailDistribution.ServiceOrderDetailId = Billing.ServiceOrderDetail.Id
          INNER JOIN Billing.RevenueControl WITH(NOLOCK) ON Billing.RevenueControlDetail.RevenueControlId = Billing.RevenueControl.Id
          LEFT OUTER JOIN dbo.INPACIENT AS p WITH(NOLOCK) ON p.IPCODPACI = Billing.RevenueControl.PatientCode
          LEFT OUTER JOIN Contract.ContractEntity AS e WITH(NOLOCK) ON e.Id = Billing.RevenueControlDetail.ContractEntityId
          LEFT OUTER JOIN Contract.CareGroup AS ga WITH(NOLOCK) ON ga.Id = Billing.RevenueControlDetail.CareGroupId
          LEFT OUTER JOIN Billing.InvoiceCategories AS cat WITH(NOLOCK) ON cat.Id = Billing.RevenueControlDetail.InvoiceCategoryId
          LEFT OUTER JOIN Security.[User] AS u ON u.UserCode = Billing.RevenueControlDetail.CreationUser
          LEFT OUTER JOIN Security.Person AS per ON per.Id = u.IdPerson
          LEFT OUTER JOIN Security.[User] AS um ON um.UserCode = Billing.RevenueControlDetail.ModificationUser
          LEFT OUTER JOIN Security.Person AS PERM ON PERM.Id = um.IdPerson
          LEFT OUTER JOIN Contract.CUPSEntity AS cups WITH(NOLOCK) ON cups.id = Billing.ServiceOrderDetail.CUPSEntityId
          LEFT OUTER JOIN Contract.IPSService AS ServiciosIPS WITH(NOLOCK) ON ServiciosIPS.id = Billing.ServiceOrderDetail.IPSServiceId
          LEFT OUTER JOIN Inventory.InventoryProduct AS pr WITH(NOLOCK) ON pr.id = Billing.ServiceOrderDetail.ProductId
          LEFT OUTER JOIN Payroll.FunctionalUnit AS UF WITH(NOLOCK) ON uf.Id = Billing.ServiceOrderDetail.PerformsFunctionalUnitId
          LEFT OUTER JOIN dbo.HCREGEGRE AS salida WITH(NOLOCK) ON CAST(salida.numingres AS INT) = Billing.RevenueControl.AdmissionNumber
          LEFT OUTER JOIN Contract.HealthAdministrator AS ea WITH(NOLOCK) ON ea.id = Billing.RevenueControlDetail.HealthAdministratorId
          LEFT OUTER JOIN Common.ThirdParty AS t WITH(NOLOCK) ON t.id = Billing.ServiceOrderDetail.ThirdPartyId
          LEFT OUTER JOIN DBO.ADINGRESO AS ing WITH(NOLOCK) ON CAST(ing.numingres AS INT) = Billing.RevenueControl.AdmissionNumber
          LEFT OUTER JOIN DBO.ADCENATEN AS CEN WITH(NOLOCK) ON CEN.CODCENATE = ING.CODCAMACT
          LEFT OUTER JOIN Billing.ServiceOrderDetailSurgical AS dq WITH(NOLOCK) ON dq.ServiceOrderDetailId = Billing.ServiceOrderDetail.id
          LEFT OUTER JOIN Contract.IPSService AS ServiciosIPSQ WITH(NOLOCK) ON ServiciosIPSQ.id = dq.IPSServiceId
          LEFT OUTER JOIN dbo.INPROFSAL AS medqx WITH(NOLOCK) ON medqx.CODPROSAL = dq.PerformsHealthProfessionalCode
          LEFT OUTER JOIN Billing.ServiceOrder AS so ON so.id = Billing.ServiceOrderDetail.ServiceOrderId
     WHERE Billing.RevenueControlDetail.STATUS IN('1', '3')
          AND Billing.ServiceOrderDetail.IsDelete = '0'
          AND ing.iestadoin <> 'A'
          AND ing.iestadoin <> 'F'
          AND ing.iestadoin <> 'C'
          AND p.IPCODPACI = '36067140'
     ORDER BY so.AdmissionNumber;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los folios de facturación pendientes de facturar (en estado Registrado o Bloqueado) para cada ingreso de paciente, integrando información de admisión, identificación del paciente, entidad pagadora (EPS, aseguradora, particular), grupo de atención, categoría RIPS, servicios y medicamentos incluidos en cada folio, valores de cuota moderadora, copago y cuota de recuperación, así como datos del profesional que realizó el procedimiento quirúrgico y del usuario que creó o modificó el registro. Cruza los controles de ingreso (RevenueControl y RevenueControlDetail) con el detalle de ítems facturados (ServiceOrderDetail y su distribución financiera), el maestro de pacientes (INPACIENT), las entidades contratantes y grupos de atención del módulo de contratos, y las categorías de facturación, para producir el reporte operativo que permite al área de facturación identificar qué folios aún no han sido facturados y cuánto representan financieramente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Facturacion_PendientesPorFacturar';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Facturacion_PendientesPorFacturar';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar los folios y servicios pendientes por facturar (estado Registrado o Bloqueado) de un paciente, con su detalle clínico, administrativo y financiero.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_PendientesPorFacturar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existe el paciente con el código consultado en INPACIENT.; Los folios deben tener detalle de orden de servicio no eliminado (IsDelete=''0'').; Los ingresos consultados no deben estar en estado ''A'' (Anulado), ''F'' (Facturado) ni ''C'' (Cerrado).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_PendientesPorFacturar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan folios con estado Registrado (1) o Bloqueado (3); nunca facturados (2).; Se excluyen ítems de orden de servicio marcados como eliminados.; Se excluyen ingresos en estados ''A'', ''F'' y ''C''.; El cálculo ''Total a facturar'' siempre es InvoicedQuantity * RateManualSalePrice.; El resultado se ordena por número de admisión.; El listado se restringe a un único paciente (filtro hardcoded por código de paciente).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_PendientesPorFacturar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Admisión; Centro de atención; Folio de facturación; Cuota de recuperación; Cuota moderadora; Copago; Autorización; EAPB; Aseguradora; Contrato; Grupo de atención; CUPS; RIPS; Servicios y medicamentos; Procedimiento quirúrgico; Profesional de salud; Unidad funcional; Alta médica; Capitación; SOAT; ISS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_PendientesPorFacturar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] result-set: Cuando RevenueControlDetail.STATUS IN (''1'',''3'') y ServiceOrderDetail.IsDelete=''0'' y el ingreso no está en estados ''A'',''F'',''C'' y el paciente coincide, se retorna una fila por servicio pendiente por facturar.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_PendientesPorFacturar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si RevenueControlDetail.FolioType = 1/2/3/4 → Clasifica el folio como ''EAPB con contrato'', ''EAPB sin contrato'', ''Particulares'' o ''Aseguradoras''.; si RevenueControlDetail.LiquidationType = 1/2/3/4 → Etiqueta la liquidación como ''Pago por servicios'', ''Capitacion'', ''Factura Global'' o ''Capitacion Global''.; si RevenueControlDetail.ResponsibleRecoveryFee = 1/2/3 → Define responsable de la cuota de recuperación como ''Ninguno'', ''Paciente'' o ''Tercero''.; si RevenueControlDetail.STATUS = 1/2/3 → Traduce estado a ''Registrado'', ''Facturado'' o ''Bloqueado'' (aunque solo se incluyen 1 y 3).; si ServiceOrderDetail.ServiceType = 1/2/3 → Identifica tipo de servicio como ''SOAT'', ''ISS'' o ''CUPS''.; si ServiceOrderDetail.RecordType = 1/2 → Distingue entre ''Servicios'' y ''Medicamentos''.; si ServiceOrderDetail.Presentation = 1/2/3 → Clasifica la presentación como ''No Quirúrgico'', ''Quirúrgico'' o ''Paquete''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_PendientesPorFacturar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.RevenueControlDetail; Billing.ServiceOrderDetailDistribution; Billing.ServiceOrderDetail; Billing.RevenueControl; dbo.INPACIENT; Contract.ContractEntity; Contract.CareGroup; Billing.InvoiceCategories; Security.User; Security.Person; Contract.CUPSEntity; Contract.IPSService; Inventory.InventoryProduct; Payroll.FunctionalUnit; dbo.HCREGEGRE; Contract.HealthAdministrator; Common.ThirdParty; dbo.ADINGRESO; dbo.ADCENATEN; Billing.ServiceOrderDetailSurgical; dbo.INPROFSAL; Billing.ServiceOrder', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_PendientesPorFacturar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_PendientesPorFacturar';
-- GO
