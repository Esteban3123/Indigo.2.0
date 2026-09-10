

-- =============================================
-- Author:		Diego A. Roldan Lozano
-- Create date:	2017-09-29
-- Description:	Genera los datos para el archivo plano MegaRIPS por los Id de las facturas
-- =============================================
CREATE PROCEDURE [Billing].[SP_GenerateMegaRIPSBilling]
	@InvoiceListIds AS xml, 
	@companyCode as varchar(3)
AS
BEGIN
	SET NOCOUNT ON;

	declare  @companyNit varchar(50)
	select top 1 @companyNit = CompanyNit  from Security.Containers where Code = @companyCode

	print 'Okkk'

	declare @tmpInvoiceListIds as table (InvoiceId int)
	insert into @tmpInvoiceListIds
	select 
	t.x.value('Id[1]','int') as InvoiceId
	from @InvoiceListIds.nodes('/InvoiceIds') t(x)
	
select * from (
select 
au.InvoicePrefix as Prefijo,REPLACE(F.InvoiceNumber, au.InvoicePrefix,'') as Nume_Fac,
'NI' as Tipo_Doc_IPS, cast(@companyNit as int) as Num_Cod_IPS,
dbo.CleanSpecialChars( U.IPSCode) as Cod_Hab_IPS,
E.HealthEntityCode as Cod_Eps,
/********CODIGO EPS CONTRI-SUB**************/
--case E.Code
--when 'EA0024' then 'EPS003'
--when 'EA0025' then 'EPSS03' 
--end as Cod_Eps,
case when G.LiquidationType=1 then 
2 when G.LiquidationType=2 then 
1 when G.LiquidationType=3 then 
6 when G.LiquidationType=4 then 
1 end as Cod_Cuenta,
'' as Cod_Contrato,F.InvoiceDate as Fecha_Fact,
F.TotalInvoice as ValorBruto,
CASE WHEN DF.RecoveryFeeType=3 THEN 

F.TotalPatientWithDiscount
WHEN DF.RecoveryFeeType=1 THEN F.TotalPatientWithDiscount else '0' end as Copago,

0 as Valor_Copago_Compartido,
0as Valor_Iva,0 as Valor_Ico,

CASE WHEN DF.RecoveryFeeType=2 THEN 
F.TotalPatientWithDiscount else 0 end as Valor_Moderadora,

F.PatientDiscount as Descuento,
'0' as Con_Des,
F.ThirdPartySalesValue as Valor_Neto,
'' as Periodo,7 as Cod_Regional,
case when ing.ICAUSAING='3' then 
1 when ing.ICAUSAING='10' then 
2 when ing.ICAUSAING='2' then 
3 when ing.ICAUSAING='6' then 4
when ing.ICAUSAING='7' then 5 else 1 end as Clasificacion_Origen,
'' as Tipo_Servicio,'' as Tipo_Paquete,'' as Fin_Consulta,'' as Dias_Trat,
dbo.TipoDocumento(p.IPTIPODOC)as Tdoc_Paciente,
SUBSTRING(p.IPCODPACI, PATINDEX('%[^0]%', p.IPCODPACI+'.'), LEN(p.IPCODPACI)) as Ndoc_Paciente,
p.IPPRINOMB as Nombre ,p.IPSEGNOMB as SegNombre ,p.IPPRIAPEL as Apellido,p.IPSEGAPEL as SegApellido, 
(cast(datediff(dd,p.IPFECNACI,[Common].[GETDATE]()) / 365.25 as int)) as Edad,
case when p.IPSEXOPAC ='1' then 'M'  when p.IPSEXOPAC='2' then 'F' end as Sexo,case coalesce(sal.ESTPACEGR, 1) when '3' then 0 when '1' then 1
when '2' then 1 when '4' then 1 else 1 end as Estado_Paciente,'N' as Discapacidad,

CASE WHEN (CASE 
WHEN pr.Code IS NULL THEN cups.RIPSCode 
WHEN cups.RIPSCode IS NULL THEN pr.Code
END) LIKE 'D%'THEN 'I'
WHEN  ISNUMERIC(CASE 
WHEN pr.Code IS NULL THEN cups.RIPSCode 
WHEN cups.RIPSCode IS NULL THEN pr.Code
END) <> 0   THEN 'P'
WHEN (CASE 
WHEN pr.Code IS NULL THEN cups.RIPSCode 
WHEN cups.RIPSCode IS NULL THEN pr.Code
END) LIKE 'S%'THEN 'P'
ELSE 'M'
END AS Tipo_Prestacion,

CASE 
WHEN pr.Code IS NULL THEN cups.RIPSCode 
WHEN cups.RIPSCode IS NULL THEN 

(CASE  WHEN pr.Code LIKE 'M-%'  THEN pr.CodeAlternativeTwo
ELSE pr.Code
END)

END AS 'Codigo_facturacion_principal',
CASE WHEN Portfolio.GetCodeOrDescriptionCUPSMegaRIPS(dq.Id,1) IS NULL THEN '0'
ELSE 
[Contract].[Codigo_ProceQ](dq.Id)
END 
as Cod_procedi_Detalle,

UPPER(
CASE 
WHEN Portfolio.GetCodeOrDescriptionCUPSMegaRIPS(dq.Id,0) IS NULL 
	THEN (CASE when pr.Name
			is NULL THEN dbo.CleanSpecialChars(CUPS.RIPSDescription) 
			else
			case when CUPS.RIPSDescription
			is null then dbo.CleanSpecialChars(pr.Name) 
			end
	END) 
ELSE Portfolio.GetCodeOrDescriptionCUPSMegaRIPS(dq.Id,0) 
END) AS Descripcion_procedi,

os.OrderDate as FechaProcedi,
'' as HoraProcedi,
dos.InvoicedQuantity AS CantidadProcedi
,CASE WHEN dq.TotalSalesPrice IS NULL THEN dos.TotalSalesPrice ELSE dq.TotalSalesPrice END AS ValorUnitario,
0 as VALOR_COMPARTIDO_PACIENTE,

CASE WHEN DF.RecoveryFeeType=2 THEN 
cast(F.TotalPatientWithDiscount as int) else 0 end as VALOR_MODERADORA_PACIENTE,

/**************************/

CASE WHEN DF.RecoveryFeeType=3 THEN
(CASE WHEN dq.TotalSalesPrice IS NULL THEN 
(CASE WHEN portfolio.GetCopayValueMegaRIPS(F.InvoiceNumber,1)= DF.GrandTotalSalesPrice 
THEN CAST( F.TotalPatientWithDiscount  as int)
ELSE 0
END)
ELSE (CASE
WHEN portfolio.GetCopayValueMegaRIPS(F.InvoiceNumber,0)= dq.TotalSalesPrice 
THEN CAST( F.TotalPatientWithDiscount  as int)
ELSE 0
END)
END)
/*************************/
WHEN DF.RecoveryFeeType=1 THEN
(CASE WHEN dq.TotalSalesPrice IS NULL THEN 
(CASE WHEN portfolio.GetCopayValueMegaRIPS(F.InvoiceNumber,1)= DF.GrandTotalSalesPrice 
THEN cast(F.TotalPatientWithDiscount  as int)
ELSE 0
END)
ELSE (CASE
WHEN portfolio.GetCopayValueMegaRIPS(F.InvoiceNumber,0)= dq.TotalSalesPrice 
THEN CAST( F.TotalPatientWithDiscount  as int)
ELSE 0
END)
END)
else 0 end AS VALOR_COPAGO_PACIENTE,
/***********/
CASE WHEN DF.RecoveryFeeType=3 THEN(
CASE WHEN dq.TotalSalesPrice IS NULL 
THEN (CASE
WHEN portfolio.GetCopayValueMegaRIPS(F.InvoiceNumber,1)= DF.GrandTotalSalesPrice 
THEN cast( (DF.GrandTotalSalesPrice - F.TotalPatientWithDiscount) as int)
ELSE cast(DF.GrandTotalSalesPrice as int)
END) 
ELSE (CASE
WHEN portfolio.GetCopayValueMegaRIPS(F.InvoiceNumber,0)= dq.TotalSalesPrice  
THEN cast( (dq.TotalSalesPrice  - F.TotalPatientWithDiscount) as int)
ELSE CAST( dq.TotalSalesPrice as int)
END)
END)
/***********BONO PACIENTE********************/
WHEN DF.RecoveryFeeType=1 THEN(
CASE WHEN dq.TotalSalesPrice IS NULL 
THEN (CASE
WHEN portfolio.GetCopayValueMegaRIPS(F.InvoiceNumber,1)= DF.GrandTotalSalesPrice 
THEN (DF.GrandTotalSalesPrice - F.TotalPatientWithDiscount) 
ELSE DF.GrandTotalSalesPrice
END) 
ELSE (CASE
WHEN portfolio.GetCopayValueMegaRIPS(F.InvoiceNumber,0)= dq.TotalSalesPrice  
THEN (dq.TotalSalesPrice  - F.TotalPatientWithDiscount) 
ELSE dq.TotalSalesPrice 
END)
END)
else 
(CASE WHEN dq.TotalSalesPrice IS NULL 
THEN (CASE
WHEN DF.RecoveryFeeType=2 
THEN (DF.GrandTotalSalesPrice - F.TotalPatientWithDiscount) 
ELSE DF.GrandTotalSalesPrice
END) 
ELSE (CASE
WHEN DF.RecoveryFeeType=2  
THEN (dq.TotalSalesPrice  - F.TotalPatientWithDiscount) 
ELSE dq.TotalSalesPrice 
END)
END)
end AS ValorTotalServicio,
/*************/
dos.AuthorizationNumber as CodAutorizacion
--,ing.CODDIAEGR as DiagnosticoPrincipal,
,F.OutputDiagnosis as DiagnosticoPrincipal,
'' as TIPO_DIAG,
'' as DIAGNOSTICO_SECUNDARIO_1,
'' as DIAGNOSTICO_SECUNDARIO_2,
ing.IFECHAING as FECHA_ENTRADA, 
'' as HORA_ENTRADA,

case when sal.FECALTPAC is null then F.OutputDate
else sal.FECALTPAC
end 
as FECHA_SALIDA,

'' as HORA_SALIDA, 
dos.ServiceDate
	FROM   
	Billing.Invoice AS F WITH (nolock) inner join
	Billing.RevenueControlDetail rcd on rcd.Id = F.RevenueControlDetailId inner join
	Billing.BillingAuthorization au on au.Id = rcd.BillingAuthorizationId inner join
	Common .ThirdParty as T on F.ThirdPartyId =T.Id inner join
	Billing.InvoiceDetail AS DF WITH (nolock) ON DF.InvoiceId = F.Id INNER JOIN
	Billing.ServiceOrderDetail AS dos WITH (nolock) ON dos.Id = DF.ServiceOrderDetailId INNER JOIN
	Billing.ServiceOrder AS os WITH (nolock) ON os.Id = dos.ServiceOrderId LEFT OUTER JOIN
	Billing.ServiceOrderDetailSurgical AS dq WITH (nolock) ON dq.ServiceOrderDetailId = dos.Id AND dq.OnlyMedicalFees = '0' LEFT OUTER JOIN
	[Contract].CUPSEntity AS cups on dos.CUPSEntityId = cups.Id INNER JOIN
	Common.OperatingUnit as U WITH (nolock) on F.operatingUnitid=U.id INNER JOIN
	Contract.HealthAdministrator as E WITH (nolock) on F.HealthAdministratorId =E.Id INNER JOIN
	Contract.CareGroup as G WITH (nolock) on F.caregroupid=G.id INNER JOIN
	dbo.ADINGRESO AS ing WITH (nolock) ON ing.NUMINGRES = F.AdmissionNumber INNER JOIN
	dbo.INPACIENT AS p WITH (nolock) ON p.IPCODPACI = F.PatientCode LEFT OUTER JOIN
	dbo.HCREGEGRE AS sal WITH (nolock) ON sal.NUMINGRES = F.AdmissionNumber LEFT OUTER JOIN
	Inventory.InventoryProduct AS pr WITH (nolock) ON pr.Id = dos.ProductId LEFT OUTER JOIN
	Contract.IPSService AS ServiciosIPS WITH (nolock) ON ServiciosIPS.Id = dos.IPSServiceId --LEFT OUTER JOIN
	--Portfolio.RadicateInvoiceD AS Dr on F.InvoiceNumber=Dr.InvoiceNumber LEFT OUTER JOIN
	--Portfolio.RadicateInvoiceC AS Cr ON Dr.RadicateInvoiceCId=Cr.Id
	WHERE (F.Status = '1') AND (dos.IsDelete = '0') AND F.Id In (select distinct InvoiceId from @tmpInvoiceListIds)--AND Cr.Id = @RadicateInvoiceCId

	union all

select au.InvoicePrefix as Prefijo,REPLACE(F.InvoiceNumber,au.InvoicePrefix,'') as Nume_Fac,
'NI' as Tipo_Doc_IPS,cast(@companyNit as int) as Num_Cod_IPS,
dbo.CleanSpecialChars( U.IPSCode) as Cod_Hab_IPS,
E.HealthEntityCode as Cod_Eps,
case when G.LiquidationType=1 then 
2 when G.LiquidationType=2 then 
1 when G.LiquidationType=3 then 
6 when G.LiquidationType=4 then 
1 end as Cod_Cuenta,
'' as Cod_Contrato,F.InvoiceDate as Fecha_Fact,
F.TotalInvoice as ValorBruto,
CASE WHEN DF.RecoveryFeeType=3 THEN 
F.TotalPatientWithDiscount
WHEN DF.RecoveryFeeType=1 THEN F.TotalPatientWithDiscount else '0' end as Copago,
0 as Valor_Copago_Compartido,
0as Valor_Iva,0 as Valor_Ico,
CASE WHEN DF.RecoveryFeeType=2 THEN 
F.TotalPatientWithDiscount else 0 end as Valor_Moderadora,
F.PatientDiscount as Descuento,
'0' as Con_Des,
F.ThirdPartySalesValue as Valor_Neto,
'' as Periodo,7 as Cod_Regional,
case when ing.ICAUSAING='3' then 
1 when ing.ICAUSAING='10' then 
2 when ing.ICAUSAING='2' then 
3 when ing.ICAUSAING='6' then 4
when ing.ICAUSAING='7' then 5 else 1 end as Clasificacion_Origen,
'' as Tipo_Servicio,'' as Tipo_Paquete,'' as Fin_Consulta,'' as Dias_Trat,
dbo.TipoDocumento(p.IPTIPODOC)as Tdoc_Paciente,
SUBSTRING(p.IPCODPACI, PATINDEX('%[^0]%', p.IPCODPACI+'.'), LEN(p.IPCODPACI)) as Ndoc_Paciente,
p.IPPRINOMB as Nombre ,p.IPSEGNOMB as SegNombre ,p.IPPRIAPEL as Apellido,p.IPSEGAPEL as SegApellido, 
(cast(datediff(dd,p.IPFECNACI,[Common].[GETDATE]()) / 365.25 as int)) as Edad,
case when p.IPSEXOPAC ='1' then 'M'  when p.IPSEXOPAC='2' then 'F' end as Sexo,case coalesce(sal.ESTPACEGR, 1) when '3' then 0 when '1' then 1
when '2' then 1 when '4' then 1 else 1 end as Estado_Paciente,'N' as Discapacidad,
CASE WHEN (CASE 
WHEN pr.Code IS NULL THEN cups.RIPSCode 
WHEN cups.RIPSCode IS NULL THEN pr.Code
END) LIKE 'D%'THEN 'I'
WHEN  ISNUMERIC(CASE 
WHEN pr.Code IS NULL THEN cups.RIPSCode 
WHEN cups.RIPSCode IS NULL THEN pr.Code
END) <> 0   THEN 'P'
WHEN (CASE 
WHEN pr.Code IS NULL THEN cups.RIPSCode 
WHEN cups.RIPSCode IS NULL THEN pr.Code
END) LIKE 'S%'THEN 'P'
ELSE 'M'
END AS Tipo_Prestacion,
CASE 
WHEN pr.Code IS NULL THEN cups.RIPSCode 
WHEN cups.RIPSCode IS NULL THEN 
(CASE  WHEN pr.Code LIKE 'M-%'  THEN pr.CodeAlternativeTwo
ELSE pr.Code
END)
END AS 'Codigo_facturacion_principal',
'0' as Cod_procedi_Detalle,
'' AS Descripcion_procedi,
os.OrderDate as FechaProcedi,
'' as HoraProcedi,
dos.InvoicedQuantity AS CantidadProcedi,
0 AS ValorUnitario,
0 as VALOR_COMPARTIDO_PACIENTE,
CASE WHEN DF.RecoveryFeeType=2 THEN 
cast(F.TotalPatientWithDiscount as int) else 0 end as VALOR_MODERADORA_PACIENTE,
0 AS VALOR_COPAGO_PACIENTE,
0 AS ValorTotalServicio,
dos.AuthorizationNumber as CodAutorizacion
--,ing.CODDIAEGR as DiagnosticoPrincipal,
,F.OutputDiagnosis as DiagnosticoPrincipal,
'' as TIPO_DIAG,
'' as DIAGNOSTICO_SECUNDARIO_1,
'' as DIAGNOSTICO_SECUNDARIO_2,
ing.IFECHAING as FECHA_ENTRADA, 
'' as HORA_ENTRADA,
case when sal.FECALTPAC is null then F.OutputDate
else sal.FECALTPAC
end 
as FECHA_SALIDA,
'' as HORA_SALIDA,
dos.ServiceDate
from
	Billing.Invoice AS F WITH (nolock) inner join
	Billing.RevenueControlDetail rcd on rcd.Id = F.RevenueControlDetailId inner join
	Billing.BillingAuthorization au on au.Id = rcd.BillingAuthorizationId inner join
	Common .ThirdParty as T on F.ThirdPartyId =T.Id inner join
	Billing.InvoiceDetail AS DF WITH (nolock) ON DF.InvoiceId = F.Id INNER JOIN
	Billing.ServiceOrderDetail AS dos WITH (nolock) ON dos.Id = DF.ServiceOrderDetailId INNER JOIN
	Billing.ServiceOrder AS os WITH (nolock) ON os.Id = dos.ServiceOrderId LEFT OUTER JOIN
	[Contract].CUPSEntity AS cups on dos.CUPSEntityId = cups.Id INNER JOIN
	Common.OperatingUnit as U WITH (nolock) on F.operatingUnitid=U.id INNER JOIN
	Contract.HealthAdministrator as E WITH (nolock) on F.HealthAdministratorId =E.Id INNER JOIN
	Contract.CareGroup as G WITH (nolock) on F.caregroupid=G.id INNER JOIN
	dbo.ADINGRESO AS ing WITH (nolock) ON ing.NUMINGRES = F.AdmissionNumber INNER JOIN
	dbo.INPACIENT AS p WITH (nolock) ON p.IPCODPACI = F.PatientCode LEFT OUTER JOIN
	dbo.HCREGEGRE AS sal WITH (nolock) ON sal.NUMINGRES = F.AdmissionNumber LEFT OUTER JOIN
	Inventory.InventoryProduct AS pr WITH (nolock) ON pr.Id = dos.ProductId LEFT OUTER JOIN
	Contract.IPSService AS ServiciosIPS WITH (nolock) ON ServiciosIPS.Id = dos.IPSServiceId --LEFT OUTER JOIN
	--Portfolio.RadicateInvoiceD AS Dr on F.InvoiceNumber=Dr.InvoiceNumber LEFT OUTER JOIN
	--Portfolio.RadicateInvoiceC AS Cr ON Dr.RadicateInvoiceCId=Cr.Id
	WHERE (F.Status = '1') AND (dos.IsDelete = '0') And dos.Presentation = 2 And F.Id In (select distinct * from @tmpInvoiceListIds)) T
	order by T.ServiceDate
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el archivo plano MegaRIPS de facturación para un conjunto de facturas identificadas por sus IDs. Toma como entrada una lista de IDs de facturas en formato XML y el código de la empresa, luego compone la información requerida por el sistema RIPS cruzando datos de facturas, detalles de facturación, órdenes de servicio, autorizaciones DIAN, terceros pagadores (EPS/aseguradoras), datos demográficos del paciente (cédula, nombre, sexo, edad), diagnósticos, procedimientos CUPS y tipo de prestación (consulta, procedimiento, medicamento, imágenes). Distribuye los valores entre copagos, cuotas moderadoras, bonos y valores netos a cargo del tercero pagador, clasifica el origen de atención (urgencias, hospitalización, etc.) y determina el código RIPS y descripción de cada servicio facturado. Su propósito es cumplir con el reporte obligatorio de RIPS ante entidades de salud y aseguradoras en Colombia.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateMegaRIPSBilling';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateMegaRIPSBilling';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el dataset plano MegaRIPS (encabezado y detalle de servicios) para un conjunto de facturas activas de una compañía, mapeando códigos clínicos, demográficos y de copago/cuota moderadora al formato esperado por el archivo MegaRIPS.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateMegaRIPSBilling';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML @InvoiceListIds debe contener nodos /InvoiceIds con elementos Id de tipo entero.; Debe existir un Security.Containers con Code = @companyCode para obtener el NIT de la compañía.; Las facturas deben estar en estado activo (Invoice.Status = ''1'') para ser incluidas.; Los detalles de orden de servicio deben no estar borrados lógicamente (ServiceOrderDetail.IsDelete = ''0'').; Cada factura debe tener ingreso asociado en dbo.ADINGRESO (NUMINGRES = AdmissionNumber) y paciente en dbo.INPACIENT (IPCODPACI = PatientCode).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateMegaRIPSBilling';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El NIT de la compañía se obtiene por TOP 1 de Security.Containers filtrado por Code = @companyCode.; Tipo_Doc_IPS siempre se emite como ''NI'' y Cod_Regional siempre como 7.; Discapacidad siempre se reporta como ''N''.; Solo se exportan facturas con Status=''1'' y líneas con IsDelete=''0''.; La edad se calcula en años enteros como datediff(días)/365.25 desde IPFECNACI hasta la fecha del sistema [Common].[GETDATE]().; El número de documento del paciente se emite sin ceros a la izquierda (SUBSTRING + PATINDEX ''%[^0]%'').; El número de factura se publica sin el prefijo de la autorización DIAN (REPLACE(InvoiceNumber, InvoicePrefix,'''')).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateMegaRIPSBilling';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un único resultset UNION ALL: la primera rama incluye servicios con detalle quirúrgico (ServiceOrderDetailSurgical con OnlyMedicalFees=''0''); la segunda rama agrega servicios con dos.Presentation = 2 sin valores unitarios ni descripción de procedimiento, ordenado por ServiceDate.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateMegaRIPSBilling';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CareGroup.LiquidationType → Mapea Cod_Cuenta: 1→2, 2→1, 3→6, 4→1 (tipo de cuenta MegaRIPS según tipo de liquidación del grupo de atención).; si InvoiceDetail.RecoveryFeeType = 3 o 1 → Asigna Copago = TotalPatientWithDiscount; calcula VALOR_COPAGO_PACIENTE comparando portfolio.GetCopayValueMegaRIPS contra TotalSalesPrice/GrandTotalSalesPrice para decidir si carga el valor o 0. else Copago = 0 y VALOR_COPAGO_PACIENTE = 0.; si InvoiceDetail.RecoveryFeeType = 2 → Asigna Valor_Moderadora y VALOR_MODERADORA_PACIENTE = TotalPatientWithDiscount (cuota moderadora). else Esos valores quedan en 0.; si ADINGRESO.ICAUSAING (causa de ingreso) → Mapea Clasificacion_Origen: 3→1, 10→2, 2→3, 6→4, 7→5, otros→1.; si INPACIENT.IPSEXOPAC → Sexo: ''1''→''M'', ''2''→''F''.; si HCREGEGRE.ESTPACEGR (estado al egreso, default 1) → Estado_Paciente: ''3''→0 (fallecido), resto→1.; si Código del producto/CUPS empieza por ''D'', es numérico, o empieza por ''S'' → Tipo_Prestacion: ''D%''→''I'' (insumo), numérico o ''S%''→''P'' (procedimiento), resto→''M'' (medicamento).; si pr.Code IS NULL / cups.RIPSCode IS NULL / pr.Code LIKE ''M-%'' → Resuelve Codigo_facturacion_principal priorizando RIPSCode de CUPS o Code/CodeAlternativeTwo del producto cuando empieza con ''M-''.; si Portfolio.GetCodeOrDescriptionCUPSMegaRIPS(dq.Id,1) IS NULL → Cod_procedi_Detalle = ''0''; en caso contrario llama a Contract.Codigo_ProceQ(dq.Id).; si HCREGEGRE.FECALTPAC IS NULL → FECHA_SALIDA = Invoice.OutputDate. else FECHA_SALIDA = HCREGEGRE.FECALTPAC.; si ServiceOrderDetail.Presentation = 2 (segunda rama del UNION ALL) → Se incluye el registro sin detalle quirúrgico, con ValorUnitario=0, VALOR_COPAGO_PACIENTE=0, ValorTotalServicio=0 y Descripcion_procedi vacía.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateMegaRIPSBilling';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.CleanSpecialChars; dbo.TipoDocumento; Common.GETDATE; Portfolio.GetCodeOrDescriptionCUPSMegaRIPS; Contract.Codigo_ProceQ; Portfolio.GetCopayValueMegaRIPS', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateMegaRIPSBilling';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Security.Containers; Billing.Invoice; Billing.RevenueControlDetail; Billing.BillingAuthorization; Common.ThirdParty; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Billing.ServiceOrder; Billing.ServiceOrderDetailSurgical; Contract.CUPSEntity; Common.OperatingUnit; Contract.HealthAdministrator; Contract.CareGroup; dbo.ADINGRESO; dbo.INPACIENT; dbo.HCREGEGRE; Inventory.InventoryProduct; Contract.IPSService', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateMegaRIPSBilling';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateMegaRIPSBilling';
-- GO
