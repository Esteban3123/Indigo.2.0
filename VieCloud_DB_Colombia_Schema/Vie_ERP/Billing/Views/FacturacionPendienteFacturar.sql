CREATE VIEW [Billing].[FacturacionPendienteFacturar]
AS
SELECT Billing.RevenueControlDetail.CareGroupId, Billing.RevenueControlDetail.id, 'Tocancipa' AS Sede, ING.IESTADOIN AS EstadoIngreso, so.AdmissionNumber AS Ingreso, ing.ifechaing AS FechaIngreso, Billing.RevenueControl.PatientCode AS Identificación, p.IPNOMCOMP AS Paciente, 
             t .Name AS Entidad, Billing.RevenueControlDetail.FolioOrder AS [Folios a facturar], CASE Billing.RevenueControlDetail.FolioType WHEN '1' THEN 'EAPB con contrato' WHEN '2' THEN 'EAPB sin contrato' WHEN '3' THEN 'Particulares' WHEN '4' THEN 'Aseguradoras' END AS Tipo, 
             CASE Billing.RevenueControlDetail.LiquidationType WHEN '1' THEN 'Pago por servicios' WHEN '2' THEN 'Capitacion' WHEN '3' THEN 'Factura Global' WHEN '4' THEN 'Capitacion Global' END AS [Tipo de liquidacion], ea.HealthEntityCode AS [Entidad Administradora], 
             t .Nit AS [Nit Entidad], ga.Code AS [Grupo Atención], ga.Name AS [Descripción Grupo Atención], Billing.RevenueControlDetail.TotalFolio AS [Vr total folio pendiente facturar], dq.InvoicedQuantity AS [Cantidad QX], dq.TotalSalesPrice AS [Total Facturado QX], 
             dq.PerformsHealthProfessionalCode AS [Cód Profesional QX], rtrim(medqx.CODPROSAL) + ' - ' + ltrim(medqx.NOMMEDICO) AS [Profesional QX], 
             CASE Billing.RevenueControlDetail.ResponsibleRecoveryFee WHEN '1' THEN 'Ninguno' WHEN '2' THEN 'Paciente' WHEN '3' THEN 'Tercero' END AS [Responsable cuota recuperación], Billing.RevenueControlDetail.TotalPatientWithDiscount AS [Vr cobrado a Paciente], 
             Billing.RevenueControlDetail.ValueCopay AS [Vr cuota recuperación folio], Billing.RevenueControlDetail.ValueFeeModerator AS [Vr cuota moderadora folio], Billing.RevenueControlDetail.Observation AS [Observaciones], cat.Name AS [Categoría para RIPS], 
             CASE Billing.RevenueControlDetail.Status WHEN '1' THEN 'Registrado' WHEN '2' THEN 'Facturado' WHEN '3' THEN 'Bloqueado' END AS 'Estado', per.Fullname AS [Usuario Crea], Billing.RevenueControlDetail.CreationDate AS [Fecha creación], PERM .Fullname AS [Usuario modifica], 
             Billing.RevenueControlDetail.ModificationDate AS [Fecha modificación], CASE Billing.ServiceOrderDetail.ServiceType WHEN '1' THEN 'SOAT' WHEN '2' THEN 'ISS' WHEN '3' THEN 'CUPS' END AS [Tipo Servicio], 
             CASE Billing.ServiceOrderDetail.RecordType WHEN '1' THEN 'Servicios' WHEN '2' THEN 'Medicamentos' END AS [Servicios/Medicamentos], CUPS.Code AS [Código CUPS], CUPS.Description AS [Descripción CUPS], ServicioSIPS.Code AS [Código Servicio], 
             ServicioSIPS.Name AS [Descripción Servicio], Billing.ServiceOrderDetail.Packaging AS [Servicio incluido en Paquete], 
             CASE Billing.ServiceOrderDetail.Presentation WHEN '1' THEN 'No Quirúrgico' WHEN '2' THEN 'Quirúrgico' WHEN '3' THEN 'Paquete' END AS [Presentación Servicio], pr.Code AS [Cód Producto], pr.Name AS [Descripción Producto], 
             Billing.ServiceOrderDetail.SupplyQuantity AS [Cantidad entregada], Billing.ServiceOrderDetail.DevolutionQuantity AS [Cantidad devuelta], Billing.ServiceOrderDetail.InvoicedQuantity AS [Cantidad a facturar], 
             Billing.ServiceOrderDetail.RateManualSalePrice AS [Valor unitario  a facturar ], (Billing.ServiceOrderDetail.InvoicedQuantity) * (Billing.ServiceOrderDetail.RateManualSalePrice) AS [Total a facturar], Billing.RevenueControlDetail.TotalPatientSalesPrice AS [Total Cuota Recuperación], 
             Billing.ServiceOrderDetail.ServiceDate AS [Fecha Servicio], Billing.ServiceOrderDetail.AuthorizationNumber AS [Autorización], uf.Code AS [Unidad Funcional], uf.Name AS [Descripción Unidad Funcional], salida.fecaltpac AS [Fecha Alta médica], 
             Billing.RevenueControlDetail.TimeStamp, ing.ufuegrmed AS UnidadEgreso, Billing.ServiceOrderDetail.CostValue
			 
FROM   Billing.RevenueControlDetail WITH (nolock) INNER JOIN
             Billing.ServiceOrderDetailDistribution WITH (nolock) ON Billing.RevenueControlDetail.Id = Billing.ServiceOrderDetailDistribution.RevenueControlDetailId AND Billing.RevenueControlDetail.Status IN ('1', '3') INNER JOIN
             Billing.ServiceOrderDetail WITH (nolock) ON Billing.ServiceOrderDetailDistribution.ServiceOrderDetailId = Billing.ServiceOrderDetail.Id INNER JOIN
             Billing.RevenueControl WITH (nolock) ON Billing.RevenueControlDetail.RevenueControlId = Billing.RevenueControl.Id LEFT OUTER JOIN
             dbo.INPACIENT AS p WITH (nolock) ON p.IPCODPACI = Billing.RevenueControl.PatientCode LEFT OUTER JOIN
             Contract.ContractEntity AS e WITH (nolock) ON e.Id = Billing.RevenueControlDetail.ContractEntityId LEFT OUTER JOIN
             Contract.CareGroup AS ga WITH (nolock) ON ga.Id = Billing.RevenueControlDetail.CareGroupId LEFT OUTER JOIN
             Billing.InvoiceCategories AS cat WITH (nolock) ON cat.Id = Billing.RevenueControlDetail.InvoiceCategoryId LEFT OUTER JOIN
             Security.[User] AS u ON u.UserCode = Billing.RevenueControlDetail.CreationUser LEFT OUTER JOIN
             Security.Person AS per ON per.Id = u.IdPerson LEFT OUTER JOIN
             Security.[User] AS um ON um.UserCode = Billing.RevenueControlDetail.ModificationUser LEFT OUTER JOIN
             Security.Person AS PERM ON PERM .Id = um.IdPerson LEFT OUTER JOIN
             Contract.CUPSEntity AS cups WITH (nolock) ON cups.id = Billing.ServiceOrderDetail.CUPSEntityId LEFT OUTER JOIN
             Contract.IPSService AS ServiciosIPS WITH (nolock) ON ServiciosIPS.id = Billing.ServiceOrderDetail.IPSServiceId LEFT OUTER JOIN
             Inventory.InventoryProduct AS pr WITH (nolock) ON pr.id = Billing.ServiceOrderDetail.ProductId LEFT OUTER JOIN
             Payroll.FunctionalUnit AS UF WITH (nolock) ON uf.Id = Billing.ServiceOrderDetail.PerformsFunctionalUnitId LEFT OUTER JOIN
             dbo.HCREGEGRE AS salida WITH (nolock) ON cast(salida.numingres AS int) = Billing.RevenueControl.AdmissionNumber LEFT OUTER JOIN
             Contract.HealthAdministrator AS ea WITH (nolock) ON ea.id = Billing.RevenueControlDetail.HealthAdministratorId LEFT OUTER JOIN
             Common.ThirdParty AS t WITH (nolock) ON t .id = Billing.ServiceOrderDetail.ThirdPartyId LEFT OUTER JOIN
             DBO.ADINGRESO AS ing WITH (nolock) ON cast(ing.numingres AS int) = Billing.RevenueControl.AdmissionNumber LEFT OUTER JOIN
             Billing.ServiceOrderDetailSurgical AS dq WITH (nolock) ON dq.ServiceOrderDetailId = Billing.ServiceOrderDetail.id LEFT OUTER JOIN
             Contract.IPSService AS ServiciosIPSQ WITH (nolock) ON ServiciosIPSQ.id = dq.IPSServiceId LEFT OUTER JOIN
             dbo.INPROFSAL AS medqx WITH (nolock) ON medqx.CODPROSAL = dq.PerformsHealthProfessionalCode LEFT OUTER JOIN
             Billing.ServiceOrder AS so ON so.id = Billing.ServiceOrderDetail.ServiceOrderId
WHERE Billing.RevenueControlDetail.Status IN ('1', '3') AND Billing.ServiceOrderDetail.IsDelete = '0' AND ing.iestadoin <> 'A' AND ing.iestadoin <> 'F' AND ing.iestadoin <> 'C'
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida todos los folios de facturación pendientes de facturar (en estado Registrado o Bloqueado) para la sede Tocancipa. Integra el control de ingresos del paciente (RevenueControl y RevenueControlDetail) con el detalle de servicios facturables (ServiceOrderDetail y su distribución financiera), enriqueciendo la información con datos del paciente, entidad contratante (EPS/aseguradora), grupo de atención, categoría para RIPS, profesional quirúrgico, unidad funcional, fecha de alta médica y usuarios de creación/modificación. Permite a los equipos de facturación identificar qué servicios —procedimientos, medicamentos o insumos— están listos para emitir factura, con sus valores totales, cuotas moderadoras, copagos, cuotas de recuperación, códigos CUPS y tipo de liquidación (pago por servicios, capitación, factura global), sirviendo como base para reportes de cartera pendiente, auditoría de RIPS y gestión de glosas.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'FacturacionPendienteFacturar';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'FacturacionPendienteFacturar';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listado consolidado de folios de facturación pendientes por facturar para la sede Tocancipá, cruzando control de ingresos, detalle de órdenes de servicio, datos del paciente, entidad responsable, profesional quirúrgico y egreso hospitalario.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'FacturacionPendienteFacturar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El folio de detalle (RevenueControlDetail) debe estar en estado Registrado (''1'') o Bloqueado (''3'').; El detalle de la orden de servicio (ServiceOrderDetail) no debe estar marcado como eliminado (IsDelete = ''0'').; El ingreso (ADINGRESO) debe existir y su estado no puede ser ''A'', ''F'' ni ''C''.; Las columnas numingres de HCREGEGRE y ADINGRESO deben ser convertibles a INT para enlazar con RevenueControl.AdmissionNumber.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'FacturacionPendienteFacturar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La sede reportada siempre se rotula como ''Tocancipa'' (literal fijo en la vista).; Solo se exponen folios en estado Registrado o Bloqueado; los folios ya facturados (Status=''2'') quedan excluidos por el WHERE.; Se excluyen ingresos en estado ''A'' (Anulado), ''F'' y ''C'', por lo que la vista representa ingresos activos/no cerrados administrativamente.; El total a facturar por línea se calcula como InvoicedQuantity * RateManualSalePrice.; El cruce con egreso hospitalario, ADINGRESO y datos quirúrgicos es opcional (LEFT JOIN); su ausencia no excluye el folio.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'FacturacionPendienteFacturar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Folio de facturación; Cuota moderadora; Cuota de recuperación; Copago; EAPB (con/sin contrato); Capitación / Factura global; CUPS; Servicios IPS; Autorización; Grupo de atención; Unidad funcional; Egreso / Alta médica; Procedimiento quirúrgico; RIPS; Administradora de salud (EPS/ARS); Paquete de servicios; Ingreso hospitalario', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'FacturacionPendienteFacturar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.FacturacionPendienteFacturar: Devuelve los folios pendientes de facturar (Status IN (''1'',''3'')) cuyo ServiceOrderDetail no está borrado y cuyo ingreso no está en estados ''A'',''F'',''C'', etiquetados con sede fija ''Tocancipa''.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'FacturacionPendienteFacturar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si RevenueControlDetail.FolioType IN (''1'',''2'',''3'',''4'') → Traduce a ''EAPB con contrato'', ''EAPB sin contrato'', ''Particulares'' o ''Aseguradoras'' respectivamente.; si RevenueControlDetail.LiquidationType IN (''1'',''2'',''3'',''4'') → Traduce a ''Pago por servicios'', ''Capitacion'', ''Factura Global'' o ''Capitacion Global''.; si RevenueControlDetail.ResponsibleRecoveryFee IN (''1'',''2'',''3'') → Traduce el responsable de cuota de recuperación a ''Ninguno'', ''Paciente'' o ''Tercero''.; si RevenueControlDetail.Status IN (''1'',''2'',''3'') → Traduce el estado a ''Registrado'', ''Facturado'' o ''Bloqueado'' (aunque el WHERE solo deja ''1'' y ''3'').; si ServiceOrderDetail.ServiceType IN (''1'',''2'',''3'') → Traduce a ''SOAT'', ''ISS'' o ''CUPS''.; si ServiceOrderDetail.RecordType IN (''1'',''2'') → Distingue entre ''Servicios'' y ''Medicamentos''.; si ServiceOrderDetail.Presentation IN (''1'',''2'',''3'') → Traduce la presentación a ''No Quirúrgico'', ''Quirúrgico'' o ''Paquete''.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'FacturacionPendienteFacturar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.RevenueControlDetail; Billing.ServiceOrderDetailDistribution; Billing.ServiceOrderDetail; Billing.RevenueControl; dbo.INPACIENT; Contract.ContractEntity; Contract.CareGroup; Billing.InvoiceCategories; Security.User; Security.Person; Contract.CUPSEntity; Contract.IPSService; Inventory.InventoryProduct; Payroll.FunctionalUnit; dbo.HCREGEGRE; Contract.HealthAdministrator; Common.ThirdParty; dbo.ADINGRESO; Billing.ServiceOrderDetailSurgical; dbo.INPROFSAL; Billing.ServiceOrder', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'FacturacionPendienteFacturar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'FacturacionPendienteFacturar';
GO
