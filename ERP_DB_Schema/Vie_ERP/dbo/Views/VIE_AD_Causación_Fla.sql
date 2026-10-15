


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