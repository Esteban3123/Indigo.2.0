

CREATE view [Report].[VIEW_SERVICIOS_TERCERIZADOS] as
select 
 CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
 aos.Code [CODIGO AUTORIZACION],
 aos.DocumentDate [FECHA DOCUMENTO],
 PER.Fullname AS 'USUARIO CREA AUTORIZACION',
 CASE AOS.Status WHEN 1 THEN 'REGISTRADA' WHEN 2 THEN 'CONFIRMADA' WHEN 3 THEN 'ANULADA' END AS 'ESTADO',
 case aos.Type when 1 then 'Hospitalaria' else 'Ambulatoria' END [TIPO AUTORIZACION],
 aos.AdmissionNumber [INGRESO],iif(fac.AdmissionNumber is null,'INGRESO NO FACTURADO','INGRESO FACTURADO')  AS 'FACTURADO' ,FAC.InvoiceNumber 'NRO DE FACTURA',
 TP.Nit [NIT], TP.Name [TERCERO],EST.TECNOLOGIA ,IIF(EST.CUPSEntityId IS NOT NULL,'ESTA EN FACTURA','NO ESTA EN FACTURA') AS 'SERVICIO FACTURADO',
 ce.Code [CODIGO CUPS],ce.Description [CUPS],IPS.Code [CODIGO SERVICIO],IPS.Name [SERVICIO IPS],
 cd.code + ' - ' + cd.name AS DESCRIPCION,
 CASE assod.ServiceType WHEN 1 THEN 'SOAT' WHEN 2 THEN 'OTRO' WHEN 3 THEN 'CUPS' END [TIPO SERVICIO],IPSQX.Code [SUBCODIGO],IPSQX.Name [SUBNOMBRE],
 assod.InvoicedQuantity [CANTIDAD],
 CASE WHEN QX.Id IS NULL THEN assod.SubTotalSalesPrice ELSE QX.TotalSalesPrice END [PRECIO UNITARIO],
 CASE WHEN QX.Id IS NULL THEN assod.GrandTotalSalesPrice ELSE QX.TotalSalesPrice END [PRECIO TOTAL],
 CASE WHEN QX.Id IS NULL THEN '0' ELSE assod.SubTotalSalesPrice END [PRECIO QX],
 ASSOD.AuthorizationNumber [AUTORIZACION],HEA.CODE + ' - ' + HEA.Name [ENTIDAD],CG.Code + ' - ' + CG.Name [GRUPO ATENCION],
 FU.Code + ' - ' + fu.Name [UNIDAD FUNCIONAL] ,SAL.NOMMEDICO [PROFESIONAL],EST.[CODIGO GRUPO ATENCION] 'CODIGO GRUPO DE ATENCION FACTURA',
 EST.[GRUPO DE ATENCION] 'GRUPO ATENCION FACTURA',
 CAST(aos.DocumentDate AS date) AS 'FECHA BUSQUEDA',
 YEAR(aos.DocumentDate) AS 'AÑO BUSQUEDA',
 MONTH(aos.DocumentDate) AS 'MES BUSQUEDA',
 CONCAT(FORMAT(MONTH(aos.DocumentDate), '00') ,' - ', 
   CASE MONTH(aos.DocumentDate) 
    WHEN 1 THEN 'ENERO'
    WHEN 2 THEN 'FEBRERO'
    WHEN 3 THEN 'MARZO'
    WHEN 4 THEN 'ABRIL'
    WHEN 5 THEN 'MAYO'
    WHEN 6 THEN 'JUNIO'
    WHEN 7 THEN 'JULIO'
    WHEN 8 THEN 'AGOSTO'
    WHEN 9 THEN 'SEPTIEMBRE'
    WHEN 10 THEN 'OCTUBRE'
    WHEN 11 THEN 'NOVIEMBRE'
    WHEN 12 THEN 'DICIEMBRE' END) 'MES NOMBRE BUSQUEDA',
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
from 
 [Authorization].AuthorizationOutsourcedServices  aos
 JOIN Security.[User] AS USU ON USU.UserCode=AOS.CreationUser 
 JOIN Security.Person AS PER ON PER.Id =USU.IdPerson 
 LEFT JOIN Common.ThirdParty AS TP ON aos.ThirdPartyId =TP.Id 
 LEFT JOIN [Authorization].AuthorizationOutsourcedServicesServiceOrderDetail AS assod on assod.AuthorizationOutsourcedServicesId =aos.Id 
 LEFT JOIN [Authorization].AuthorizationOutsourcedServicesServiceOrderDetailSurgical AS QX ON QX.AuthorizationOutsourcedServicesServiceOrderDetailId =assod.Id
 left join Contract.CUPSEntity as ce on ce.Id =assod.CUPSEntityId 
 LEFT JOIN Contract.IPSService AS IPS ON IPS.Id =assod.IPSServiceId 
 left join Payroll.FunctionalUnit as fu on fu.Id =assod.PerformsFunctionalUnitId 
 LEFT JOIN dbo.INPROFSAL AS SAL ON SAL.CODPROSAL =assod.PerformsHealthProfessionalCode 
 LEFT JOIN Contract.IPSService AS IPSQX ON IPSQX.Id =QX.IPSServiceId 
 LEFT JOIN Contract.HealthAdministrator AS HEA ON HEA.Id =assod.HealthAdministratorId 
 LEFT JOIN Contract.CareGroup CG ON CG.Id =assod.CareGroupId 
 LEFT JOIN Contract.ContractDescriptions cd ON CAST (assod.CUPSEntityContractDescriptionId as varchar) = cd.code
 LEFT JOIN (select AdmissionNumber,InvoiceNumber   
            from Billing.Invoice 
			where Status =1 group by AdmissionNumber,InvoiceNumber) as fac on fac.AdmissionNumber =aos.AdmissionNumber 
 LEFT JOIN (SELECT F.AdmissionNumber,F.InvoiceNumber ,CUPS.Code ,CUPS.Description, SOD.CUPSEntityId, CAT.Name AS 'TECNOLOGIA',CG.Code 'CODIGO GRUPO ATENCION',CG.Name 'GRUPO DE ATENCION' 
            FROM Billing.Invoice AS F WITH (NOLOCK)
			INNER JOIN Billing.InvoiceDetail AS DF WITH (NOLOCK) ON DF.InvoiceId = F.Id
			INNER JOIN Billing.ServiceOrderDetail AS SOD WITH (NOLOCK) ON SOD.Id = DF.ServiceOrderDetailId
			INNER JOIN Billing.InvoiceCategories AS CAT ON CAT.Id =F.InvoiceCategoryId 
			INNER JOIN Contract.CareGroup AS CG ON CG.Id =F.CareGroupId 
			LEFT OUTER JOIN Billing.ServiceOrderDetailSurgical AS DQ WITH (NOLOCK) ON DQ.ServiceOrderDetailId = SOD.Id AND DQ.OnlyMedicalFees = '0'
			LEFT OUTER JOIN Contract.CUPSEntity AS CUPS WITH (NOLOCK) ON CUPS.Id = SOD.CUPSEntityId
			 WHERE F.Status =1 GROUP BY F.AdmissionNumber,F.InvoiceNumber ,CUPS.Code ,CUPS.Description, SOD.CUPSEntityId,CAT.Name,
			 CG.Code,CG.Name) AS EST ON EST.AdmissionNumber =FAC.AdmissionNumber AND EST.InvoiceNumber =FAC.InvoiceNumber AND EST.CUPSEntityId =assod.CUPSEntityId
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting orientada a la auditoría y conciliación de servicios tercerizados (outsourcing). Cruza las autorizaciones emitidas a terceros con el detalle de sus ítems (CUPS, servicios IPS, procedimientos quirúrgicos), indicando por cada servicio si el ingreso del paciente fue facturado y si el ítem específico aparece en la factura. Consolida información de entidad pagadora, grupo de atención, unidad funcional, profesional y precios unitarios/totales (diferenciando honorarios quirúrgicos). Incluye dimensiones de fecha para filtros temporales en herramientas de BI.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_SERVICIOS_TERCERIZADOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_SERVICIOS_TERCERIZADOS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reportería que consolida las autorizaciones de servicios tercerizados con su detalle, terceros, profesionales, unidades funcionales y el cruce con facturación para identificar si el ingreso y los servicios autorizados quedaron facturados.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_SERVICIOS_TERCERIZADOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las autorizaciones deben existir en Authorization.AuthorizationOutsourcedServices con un usuario creador válido en Security.User y Security.Person (JOIN obligatorio).; Para considerar una factura como vinculada al ingreso, su Status debe ser = 1 (activa) en Billing.Invoice.; Para evaluar el detalle facturado en EST, se requiere factura con Status=1 y servicios quirúrgicos donde DQ.OnlyMedicalFees = ''0''.; El campo CUPSEntityContractDescriptionId se compara con cd.code mediante CAST a varchar (asume compatibilidad de tipos).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_SERVICIOS_TERCERIZADOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'ID_COMPANY siempre se reporta como el nombre de la base de datos actual truncado a 9 caracteres (DB_NAME()).; ULT_ACTUAL siempre se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.; Solo facturas con Status=1 se consideran para determinar si el ingreso o el servicio están facturados; facturas anuladas o en otros estados no cruzan.; En el subquery EST se excluyen detalles quirúrgicos donde OnlyMedicalFees ≠ ''0'', evitando contar honorarios médicos puros como servicio facturado.; El cruce de servicio facturado exige coincidencia simultánea de AdmissionNumber, InvoiceNumber y CUPSEntityId entre la autorización y la factura.; El nombre del mes en español se deriva determinísticamente del número del mes de DocumentDate.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_SERVICIOS_TERCERIZADOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Autorización de servicios tercerizados (outsourcing); Tercero / contratista externo; Procedimiento CUPS; Servicio IPS; Detalle quirúrgico / honorarios médicos; Ingreso (admisión) hospitalaria/ambulatoria; Factura y estado de facturación; Administradora de salud (EPS/aseguradora); Grupo de atención; Unidad funcional; Profesional de la salud; Tipo de servicio SOAT/OTRO/CUPS', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_SERVICIOS_TERCERIZADOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.VIEW_SERVICIOS_TERCERIZADOS: Devuelve una fila por combinación autorización-detalle-detalle quirúrgico, enriquecida con datos de tercero, CUPS, IPSService, unidad funcional, profesional, administradora de salud y grupo de atención, más el estado de facturación del ingreso y del servicio.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_SERVICIOS_TERCERIZADOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AOS.Status = 1 / 2 / 3 → Etiqueta el ESTADO como ''REGISTRADA'', ''CONFIRMADA'' o ''ANULADA'' respectivamente. else NULL; si aos.Type = 1 → Marca TIPO AUTORIZACION = ''Hospitalaria''. else Marca TIPO AUTORIZACION = ''Ambulatoria'' para cualquier otro valor.; si fac.AdmissionNumber IS NULL → El ingreso se reporta como ''INGRESO NO FACTURADO''. else Se reporta como ''INGRESO FACTURADO''.; si EST.CUPSEntityId IS NOT NULL → El servicio se marca como ''ESTA EN FACTURA''. else Se marca como ''NO ESTA EN FACTURA''.; si assod.ServiceType = 1 / 2 / 3 → Clasifica TIPO SERVICIO como ''SOAT'', ''OTRO'' o ''CUPS'' respectivamente. else NULL; si QX.Id IS NULL (no existe detalle quirúrgico) → PRECIO UNITARIO y PRECIO TOTAL toman SubTotalSalesPrice y GrandTotalSalesPrice del detalle no quirúrgico; PRECIO QX = ''0''. else PRECIO UNITARIO y PRECIO TOTAL toman QX.TotalSalesPrice; PRECIO QX toma assod.SubTotalSalesPrice.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_SERVICIOS_TERCERIZADOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.AuthorizationOutsourcedServices; Security.User; Security.Person; Common.ThirdParty; Authorization.AuthorizationOutsourcedServicesServiceOrderDetail; Authorization.AuthorizationOutsourcedServicesServiceOrderDetailSurgical; Contract.CUPSEntity; Contract.IPSService; Payroll.FunctionalUnit; dbo.INPROFSAL; Contract.HealthAdministrator; Contract.CareGroup; Contract.ContractDescriptions; Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Billing.InvoiceCategories; Billing.ServiceOrderDetailSurgical', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_SERVICIOS_TERCERIZADOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_SERVICIOS_TERCERIZADOS';
GO
