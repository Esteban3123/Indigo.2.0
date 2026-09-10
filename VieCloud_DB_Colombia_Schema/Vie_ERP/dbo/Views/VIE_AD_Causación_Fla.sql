

CREATE VIEW [dbo].[VIE_AD_Causación_Fla]
AS
SELECT uo.UnitName AS Sede,
       CASE F.DocumentType
           WHEN '1' THEN
               'Factura EAPB con Contrato'
           WHEN '2' THEN
               'Factura EAPB Sin Contrato'
           WHEN '3' THEN
               'Factura Particular'
           WHEN '4' THEN
               'Factura Capitada '
           WHEN '5' THEN
               'Control de Capitacion'
           WHEN '6' THEN
               'Factura Basica'
           WHEN '7' THEN
               'Factura de Venta de Productos'
       END AS [Tipo de documento],
       cat.Name AS Categoría,
       F.InvoiceNumber AS [Nro Factura/Registro],
       F.AdmissionNumber AS Ingreso,
       ing.IFECHAING AS [Fecha ingreso],
       F.PatientCode AS Identificación,
       p.IPNOMCOMP AS Paciente,
       ga.Name AS [Grupo de Atención],
       F.TotalInvoice AS [Total Factura/Registro],
       F.InvoiceDate AS [Fecha Registro],
       dos.InvoicedQuantity AS Cantidad,
       CASE
           WHEN dq.TotalSalesPrice IS NULL THEN
               dos.TotalSalesPrice
           ELSE
               dq.TotalSalesPrice
       END AS ValorUnitario,
       CASE
           WHEN dq.TotalSalesPrice IS NULL THEN
               DF.GrandTotalSalesPrice
           ELSE
               dq.TotalSalesPrice
       END AS ValorTotal,
       t.Nit,
       ea.Code + ' - ' + ea.Name AS [Entidad Administradora],
       CASE dos.RecordType
           WHEN '1' THEN
               'Servicios'
           WHEN '2' THEN
               'Medicamentos'
       END AS [Servicios/Medicamentos],
       CASE
           WHEN pr.Code IS NULL THEN
               ServiciosIPS.Code
           ELSE
               pr.Code
       END AS Código,
       CASE
           WHEN pr.Name IS NULL THEN
               ServiciosIPS.Name
           ELSE
               pr.Name
       END AS Descripción,
       CASE dos.Presentation
           WHEN '1' THEN
               'No Quirúrgico'
           WHEN '2' THEN
               'Quirúrgico'
           WHEN '3' THEN
               'Paquete'
       END AS [Presentación Servicio],
       ServiciosIPSQ.Code AS Subcodigo,
       ServiciosIPSQ.Name AS Subnombre,
       CASE
           WHEN dq.PerformsHealthProfessionalCode IS NULL THEN
               dos.PerformsHealthProfessionalCode
           ELSE
               dq.PerformsHealthProfessionalCode
       END AS CodigoMèdico,
       CASE
           WHEN RTRIM(medqx.NOMMEDICO) IS NULL THEN
               med.NOMMEDICO
           ELSE
               RTRIM(medqx.NOMMEDICO)
       END AS NombreMedico,
       UF.Name AS [Descripción Unidad Funcional],
       salida.FECALTPAC AS [Fecha Alta médica],
       ing.CODDIAEGR AS CIE10,
       diag.NOMDIAGNO AS Diagnóstico,
       CASE
           WHEN espmed.DESESPECI IS NULL THEN
               espqx.DESESPECI
           ELSE
               espmed.DESESPECI
       END AS Especialidad,
       per.Fullname AS Usuario,
       dos.IsPackage AS Paquete,
       dos.Packaging AS Incluído
FROM Billing.Invoice AS F WITH (NOLOCK)
    INNER JOIN Billing.InvoiceDetail AS DF WITH (NOLOCK)
        ON DF.InvoiceId = F.Id
    INNER JOIN dbo.ADINGRESO AS ing WITH (NOLOCK)
        ON CAST(ing.NUMINGRES AS INT) = F.AdmissionNumber
    INNER JOIN Billing.ServiceOrderDetail AS dos WITH (NOLOCK)
        ON dos.Id = DF.ServiceOrderDetailId
    INNER JOIN dbo.INPACIENT AS p WITH (NOLOCK)
        ON p.IPCODPACI = F.PatientCode
    INNER JOIN Common.ThirdParty AS t WITH (NOLOCK)
        ON t.Id = F.ThirdPartyId
    INNER JOIN Common.OperatingUnit AS uo WITH (NOLOCK)
        ON uo.Id = F.OperatingUnitId
    INNER JOIN Contract.CareGroup AS ga WITH (NOLOCK)
        ON ga.Id = F.CareGroupId
    LEFT OUTER JOIN Contract.HealthAdministrator AS ea WITH (NOLOCK)
        ON ea.Id = F.HealthAdministratorId
    LEFT OUTER JOIN Security.[User] AS u 
        ON u.UserCode = F.InvoicedUser
    LEFT OUTER JOIN Security.Person AS per
        ON per.Id = u.IdPerson
    LEFT OUTER JOIN Billing.InvoiceCategories AS cat WITH (NOLOCK)
        ON cat.Id = F.InvoiceCategoryId
    LEFT OUTER JOIN Contract.IPSService AS ServiciosIPS WITH (NOLOCK)
        ON ServiciosIPS.Id = dos.IPSServiceId
    LEFT OUTER JOIN Inventory.InventoryProduct AS pr WITH (NOLOCK)
        ON pr.Id = dos.ProductId
    LEFT OUTER JOIN dbo.INPROFSAL AS med WITH (NOLOCK)
        ON med.CODPROSAL = dos.PerformsHealthProfessionalCode
    LEFT OUTER JOIN dbo.INESPECIA AS espmed WITH (NOLOCK)
        ON espmed.CODESPECI = med.CODESPEC1
    LEFT OUTER JOIN dbo.INDIAGNOS AS diag
        ON diag.CODDIAGNO = ing.CODDIAEGR
    LEFT OUTER JOIN dbo.HCREGEGRE AS salida WITH (NOLOCK)
        ON CAST(salida.NUMINGRES AS INT) = F.AdmissionNumber
    LEFT OUTER JOIN Billing.ServiceOrderDetailSurgical AS dq WITH (NOLOCK)
        ON dq.ServiceOrderDetailId = dos.Id
           AND dq.OnlyMedicalFees = '0'
    LEFT OUTER JOIN dbo.INPROFSAL AS medqx WITH (NOLOCK)
        ON medqx.CODPROSAL = dq.PerformsHealthProfessionalCode
    LEFT OUTER JOIN dbo.INESPECIA AS espqx
        ON espqx.CODESPECI = medqx.CODESPEC1
    LEFT OUTER JOIN Contract.IPSService AS ServiciosIPSQ WITH (NOLOCK)
        ON ServiciosIPSQ.Id = dq.IPSServiceId
    LEFT OUTER JOIN Payroll.FunctionalUnit AS UF WITH (NOLOCK)
        ON UF.Id = dos.PerformsFunctionalUnitId
WHERE (F.Status = '1')
      AND (DF.Id NOT IN
           (
               SELECT InvoiceDetailId FROM MedicalFees.MedicalFeesCausation
           )
          )
      AND (uo.Id = '10');
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting para la causación de honorarios médicos pendientes en facturación. Consolida, para la sede operativa 10, las facturas activas (estado=1) cuyos detalles aún no han sido causados en `MedicalFees.MedicalFeesCausation`, mostrando datos del ingreso, paciente, entidad pagadora, tipo de documento, servicios o medicamentos facturados (con código, valor unitario y total), médico ejecutante con su especialidad, unidad funcional, diagnóstico de egreso y fecha de alta médica. Sirve como insumo para el proceso de liquidación y causación de honorarios médicos pendientes.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Causación_Fla';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Causación_Fla';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida información de causación contable de facturas activas de una sede específica, integrando datos clínicos, administrativos, de servicios y profesionales para análisis financiero.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Causación_Fla';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las facturas deben tener Status = ''1'' (activas/vigentes); El detalle de factura no debe estar previamente causado en MedicalFees.MedicalFeesCausation; La unidad operativa debe ser la sede con Id = ''10''; Debe existir relación válida entre factura, detalle de factura, ingreso, paciente, tercero, unidad operativa y grupo de atención (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Causación_Fla';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone facturas activas (Status=''1'') de la sede 10; Excluye sistemáticamente las líneas de factura ya registradas en causación de honorarios médicos; Cuando hay detalle quirúrgico (dq), prevalecen sus valores y profesional sobre los del detalle de servicio (dos); Si la línea corresponde a un producto de inventario, prevalece su código/nombre sobre el servicio IPS; Solo considera detalles quirúrgicos donde OnlyMedicalFees = ''0'' (no exclusivos de honorarios médicos)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Causación_Fla';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura EAPB con/sin contrato; Factura particular; Factura capitada; Control de capitación; Paciente; Ingreso/admisión; Grupo de atención; Entidad administradora (EPS); Servicios y medicamentos; Presentación quirúrgica/no quirúrgica/paquete; Profesional de salud; Especialidad médica; Diagnóstico CIE-10; Unidad funcional; Alta médica; Causación de honorarios médicos; Sede/unidad operativa', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Causación_Fla';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve filas únicamente cuando F.Status = ''1'' AND uo.Id = ''10'' AND el detalle no aparece en MedicalFees.MedicalFeesCausation', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Causación_Fla';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si F.DocumentType in (''1''..''7'') → Etiqueta el tipo de documento como Factura EAPB con/sin Contrato, Particular, Capitada, Control de Capitación, Básica o Venta de Productos; si dq.TotalSalesPrice IS NULL (no existe detalle quirúrgico) → Toma valores y profesional desde ServiceOrderDetail (dos) else Toma valores y profesional desde ServiceOrderDetailSurgical (dq); si dos.RecordType = ''1'' o ''2'' → Clasifica la línea como ''Servicios'' o ''Medicamentos'' respectivamente; si pr.Code IS NULL (no es producto de inventario) → Usa código/nombre del servicio IPS else Usa código/nombre del producto de inventario; si dos.Presentation in (''1'',''2'',''3'') → Clasifica presentación como ''No Quirúrgico'', ''Quirúrgico'' o ''Paquete''; si espmed.DESESPECI IS NULL → Toma especialidad del médico quirúrgico (espqx) else Toma especialidad del médico tratante (espmed)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Causación_Fla';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceDetail; dbo.ADINGRESO; Billing.ServiceOrderDetail; dbo.INPACIENT; Common.ThirdParty; Common.OperatingUnit; Contract.CareGroup; Contract.HealthAdministrator; Security.User; Security.Person; Billing.InvoiceCategories; Contract.IPSService; Inventory.InventoryProduct; dbo.INPROFSAL; dbo.INESPECIA; dbo.INDIAGNOS; dbo.HCREGEGRE; Billing.ServiceOrderDetailSurgical; Payroll.FunctionalUnit; MedicalFees.MedicalFeesCausation', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Causación_Fla';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Causación_Fla';
GO
