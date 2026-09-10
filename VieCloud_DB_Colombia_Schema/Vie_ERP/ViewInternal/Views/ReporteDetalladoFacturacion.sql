

CREATE VIEW [ViewInternal].[ReporteDetalladoFacturacion]
AS
SELECT        Cedula, Nombre, Ingreso, CodigoServicio, NombreServicio, FechaServicio, Cantidad, ValorUnitario, Descuento, Total, Categoria, GrupoAtencion, EntidadAdministradora, TerceroEntidad, UnidadesFuncionales, CentroCosto, 
                         Factura, UsuarioFacturacion, UsuarioCargo, FechaFactura, IngresaPor, CodigoEntidad, especialidad
FROM            (SELECT        ad.IPCODPACI AS Cedula, pa.IPNOMCOMP AS Nombre, ad.NUMINGRES AS Ingreso, ce.Code AS CodigoServicio, ce.Description AS NombreServicio, CAST(sod.ServiceDate AS date) AS FechaServicio, 
                                                    sod.InvoicedQuantity AS Cantidad, sod.SubTotalSalesPrice AS ValorUnitario, id.GrandTotalDiscount AS Descuento, id.GrandTotalSalesPrice AS Total, ic.Code + ' - ' + ic.Name AS Categoria, 
                                                    cg.Code + ' - ' + cg.Name AS GrupoAtencion, ha.Code + ' - ' + cg.Name AS EntidadAdministradora, t.Nit + ' - ' + t.Name AS TerceroEntidad, fu.Code + ' - ' + fu.Name AS UnidadesFuncionales, 
                                                    cc.Code + ' - ' + cc.Name AS CentroCosto, i.InvoiceNumber AS Factura, i.InvoicedUser AS UsuarioFacturacion, so.CreationUser AS UsuarioCargo, i.InvoiceDate AS FechaFactura, ad.IINGREPOR AS IngresaPor, 
                                                    ha.Code AS CodigoEntidad, esp.DESESPECI AS especialidad
                          FROM            Billing.Invoice AS i INNER JOIN
                                                    Billing.InvoiceCategories AS ic ON ic.Id = i.InvoiceCategoryId INNER JOIN
                                                    Billing.InvoiceDetail AS id ON id.InvoiceId = i.Id INNER JOIN
                                                    dbo.ADINGRESO AS ad ON ad.NUMINGRES = i.AdmissionNumber INNER JOIN
                                                    dbo.INPACIENT AS pa ON ad.IPCODPACI = pa.IPCODPACI INNER JOIN
                                                    Billing.ServiceOrderDetail AS sod ON sod.Id = id.ServiceOrderDetailId INNER JOIN
                                                    Billing.ServiceOrder AS so ON so.Id = sod.ServiceOrderId INNER JOIN
                                                    Contract.CareGroup AS cg ON cg.Id = i.CareGroupId INNER JOIN
                                                    Contract.CUPSEntity AS ce ON ce.Id = sod.CUPSEntityId INNER JOIN
                                                    Payroll.FunctionalUnit AS fu ON fu.Id = sod.PerformsFunctionalUnitId INNER JOIN
                                                    Payroll.CostCenter AS cc ON cc.Id = sod.CostCenterId INNER JOIN
                                                    Security.[UserInt] AS u ON u.UserCode = i.InvoicedUser LEFT OUTER JOIN
                                                    Contract.HealthAdministrator AS ha ON ha.Id = i.HealthAdministratorId LEFT OUTER JOIN
                                                    Common.ThirdParty AS t ON t.Id = ha.ThirdPartyId INNER JOIN
                                                    dbo.INPROFSAL AS med ON sod.PerformsHealthProfessionalCode = med.CODPROSAL INNER JOIN
                                                    dbo.INESPECIA AS esp ON med.CODESPEC1 = esp.CODESPECI INNER JOIN
                                                    dbo.INUBICACI AS ubi ON ad.DEPMUNCOD = ubi.DEPMUNCOD
                          WHERE        (i.Status = 1)
                          UNION ALL
                          SELECT        ad.IPCODPACI AS Cedula, pa.IPNOMCOMP AS Nombre, ad.NUMINGRES AS Ingreso, p.Code AS CodigoServicio, p.Name AS NombreServicio, CAST(sod.ServiceDate AS date) AS FechaServicio, 
                                                   sod.InvoicedQuantity AS Cantidad, sod.SubTotalSalesPrice AS ValorUnitario, id.GrandTotalDiscount AS Descuento, id.GrandTotalSalesPrice AS Total, ic.Code + ' - ' + ic.Name AS Categoria, 
                                                   cg.Code + ' - ' + cg.Name AS GrupoAtencion, ha.Code + ' - ' + cg.Name AS EntidadAdministradora, t.Nit + ' - ' + t.Name AS TerceroEntidad, fu.Code + ' - ' + fu.Name AS UnidadesFuncionales, 
                                                   cc.Code + ' - ' + cc.Name AS CentroCosto, i.InvoiceNumber AS Factura, i.InvoicedUser AS UsuarioFacturacion, so.CreationUser AS UsuarioCargo, i.InvoiceDate AS FechaFactura, ad.IINGREPOR AS IngresaPor, 
                                                   ha.Code AS CodigoEntidad, '' AS especialidad
                          FROM            Billing.Invoice AS i INNER JOIN
                                                   Billing.InvoiceCategories AS ic ON ic.Id = i.InvoiceCategoryId INNER JOIN
                                                   Billing.InvoiceDetail AS id ON id.InvoiceId = i.Id INNER JOIN
                                                   dbo.ADINGRESO AS ad ON ad.NUMINGRES = i.AdmissionNumber INNER JOIN
                                                   dbo.INPACIENT AS pa ON ad.IPCODPACI = pa.IPCODPACI INNER JOIN
                                                   Billing.ServiceOrderDetail AS sod ON sod.Id = id.ServiceOrderDetailId INNER JOIN
                                                   Billing.ServiceOrder AS so ON so.Id = sod.ServiceOrderId INNER JOIN
                                                   Contract.CareGroup AS cg ON cg.Id = i.CareGroupId INNER JOIN
                                                   Inventory.InventoryProduct AS p ON p.Id = sod.ProductId INNER JOIN
                                                   Payroll.FunctionalUnit AS fu ON fu.Id = sod.PerformsFunctionalUnitId INNER JOIN
                                                   Payroll.CostCenter AS cc ON cc.Id = sod.CostCenterId INNER JOIN
                                                   Security.[UserInt] AS u ON u.UserCode = i.InvoicedUser LEFT OUTER JOIN
                                                   Contract.HealthAdministrator AS ha ON ha.Id = i.HealthAdministratorId LEFT OUTER JOIN
                                                   Common.ThirdParty AS t ON t.Id = ha.ThirdPartyId
                          WHERE        (i.Status = 1)) AS datos
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting que consolida el detalle completo de facturación activa (Status=1), combinando mediante UNION ALL servicios/procedimientos CUPS y productos de inventario facturados. Aplanada para consumo de reportes, cruza datos del paciente, admisión, factura, categoría, grupo de atención, entidad administradora (EPS/aseguradora), unidad funcional, centro de costo, valores unitarios, descuentos y totales. El primer bloque incluye la especialidad del profesional ejecutor; el segundo corresponde a ítems de medicamentos e insumos sin especialidad asociada.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'ReporteDetalladoFacturacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'ReporteDetalladoFacturacion';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único reporte el detalle de servicios y productos facturados a pacientes, uniendo procedimientos CUPS (con especialidad del profesional) y medicamentos/insumos de inventario (sin especialidad).', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'ReporteDetalladoFacturacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La factura debe tener Status = 1 (activa/vigente) para ser incluida.; Cada factura debe estar vinculada a una admisión (AdmissionNumber) y a un paciente existente en INPACIENT.; El detalle de factura debe estar asociado a una orden de servicio y a su detalle (ServiceOrderDetail).; Para el bloque de procedimientos: el detalle debe referenciar una entidad CUPS y un profesional de salud con especialidad registrada.; Para el bloque de productos: el detalle debe referenciar un producto de inventario.; El usuario facturador debe existir en Security.UserInt.; La factura debe tener categoría, grupo de atención, unidad funcional y centro de costo asignados.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'ReporteDetalladoFacturacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen facturas activas (Status = 1).; Una misma factura puede aparecer múltiples veces, una por cada línea de detalle facturado.; Las líneas de procedimientos siempre traen especialidad del profesional ejecutante; las líneas de productos siempre traen especialidad vacía.; La fecha del servicio se reporta truncada a fecha (sin hora).; Los campos compuestos (Categoría, GrupoAtención, EntidadAdministradora, TerceroEntidad, UnidadesFuncionales, CentroCosto) se entregan en formato ''Código - Nombre''.; El campo EntidadAdministradora concatena el código de la administradora con el nombre del grupo de atención (no con el nombre de la administradora).', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'ReporteDetalladoFacturacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Admisión; Factura; Detalle de factura; Orden de servicio; Procedimiento CUPS; Medicamento/Insumo de inventario; Entidad administradora de salud (EPS); Tercero pagador; Grupo de atención; Categoría de factura; Unidad funcional; Centro de costo; Especialidad médica; Profesional de salud; Descuento; Valor unitario; Usuario facturador', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'ReporteDetalladoFacturacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ViewInternal.ReporteDetalladoFacturacion: Devuelve la unión (UNION ALL) de dos conjuntos: facturas con Status=1 detalladas por procedimiento CUPS (con especialidad del profesional) y facturas con Status=1 detalladas por productos de inventario (especialidad como cadena vacía).', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'ReporteDetalladoFacturacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si i.Status = 1 y el detalle proviene de ServiceOrderDetail con CUPSEntityId y profesional de salud → Se reporta el ítem como servicio CUPS, tomando código/nombre desde Contract.CUPSEntity y la especialidad desde INESPECIA via INPROFSAL; si i.Status = 1 y el detalle proviene de ServiceOrderDetail con ProductId (medicamento/insumo) → Se reporta el ítem como producto de Inventory.InventoryProduct, dejando especialidad como cadena vacía; si i.HealthAdministratorId existe (LEFT JOIN con HealthAdministrator y ThirdParty) → Se concatenan código de entidad administradora y NIT/Nombre del tercero; en caso contrario quedan en NULL', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'ReporteDetalladoFacturacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceCategories; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Billing.ServiceOrder; Contract.CareGroup; Contract.CUPSEntity; Contract.HealthAdministrator; Common.ThirdParty; Payroll.FunctionalUnit; Payroll.CostCenter; Security.UserInt; Inventory.InventoryProduct; dbo.ADINGRESO; dbo.INPACIENT; dbo.INPROFSAL; dbo.INESPECIA; dbo.INUBICACI', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'ReporteDetalladoFacturacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'ReporteDetalladoFacturacion';
GO
