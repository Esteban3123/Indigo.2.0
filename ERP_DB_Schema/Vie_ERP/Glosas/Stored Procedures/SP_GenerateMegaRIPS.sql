


-- =============================================
-- Author:		Juan F. Tamayo Puertas
-- Create date:	2016-08-23
-- Description:	Genera los datos para el archivo plano MegaRIPS por el Id de la RadicateInvoiceC
-- =============================================
CREATE PROCEDURE [Glosas].[SP_GenerateMegaRIPS]
	@RadicateInvoiceCId AS integer
AS
BEGIN
	SET NOCOUNT ON;

    select au.InvoicePrefix as Prefijo,F.InvoiceNumber as Nume_Fac,'NI' as Tipo_Doc_IPS,'813001952' as Num_Cod_IPS,U.IPSCode as Cod_Hab_IPS,E.HealthEntityCode as Cod_Eps,
    case when G.LiquidationType=1 then '2' when G.LiquidationType=2 then '1' when G.LiquidationType=3 then '6' when G.LiquidationType=4 then '1' end as Cod_Cuenta,
    '' as Cod_Contrato,F.InvoiceDate as Fecha_Fact,F.ThirdPartySalesValue as ValorBruto,CASE WHEN DF.RecoveryFeeType=3 THEN F.TotalPatientWithDiscount else '0' end as Copago,
    '0' as Valor_Copago_Compartido,'0'as Valor_Iva,'0' as Valor_Ico,CASE WHEN DF.RecoveryFeeType=2 THEN F.TotalPatientWithDiscount else '0' end as Valor_Moderadora,
    F.PatientDiscount as Descuento,'0' as Con_Des,F.TotalInvoice as Valor_Neto,'' as Periodo,'7' as Cod_Regional,case when ing.ICAUSAING='3' then '1' when ing.ICAUSAING=10 then '2' when ing.ICAUSAING='2' then '3' when ing.ICAUSAING='6' then '4'
    when ing.ICAUSAING='7' then '5' end as Clasificacion_Origen,'' as Tipo_Servicio,'' as Tipo_Paquete,'' as Fin_Consulta,'' as Dias_Trat,
    case when p.IPTIPODOC='1' then 'CC' when p.IPTIPODOC='2' then 'CE' when p.IPTIPODOC='3' then 'TI' when p.IPTIPODOC='4' then 'RC' 
    when p.IPTIPODOC='5' then 'PA' when p.IPTIPODOC='6' then 'AS' when p.IPTIPODOC='7' then 'MS'  end as Tdoc_Paciente,p.IPCODPACI as Ndoc_Paciente,
    p.IPPRINOMB as Nombre ,p.IPSEGNOMB as SegNombre ,p.IPPRIAPEL as Apellido,p.IPSEGAPEL as SegApellido, (cast(datediff(dd,p.IPFECNACI,[Common].[GETDATE]()) / 365.25 as int)) as Edad,
    case when p.IPSEXOPAC ='1' then 'M'  when p.IPSEXOPAC='2' then 'F' end as Sexo,case when sal.ESTPACEGR='3' then '0' when  sal.ESTPACEGR='1' then '1' 
    when sal.ESTPACEGR='2' then '1' when sal.ESTPACEGR='4' then '1' when sal.ESTPACEGR is NULL then '1' end as Estado_Paciente,'N' as Discapacidad,
    CASE dos.RecordType WHEN '1' THEN 'P' WHEN '2' THEN 'M' END AS Tipo_Prestacion,CASE WHEN pr.Code IS NULL THEN ServiciosIPS.Code ELSE pr.Code END AS Codigo_facturacion_principal,
    '' as Cod_procedi_Detalle,CASE WHEN pr.Name IS NULL THEN ServiciosIPS.Name ELSE pr.Name END AS Descripcion_procedi,os.OrderDate AS FechaProcedi,'' as HoraProcedi,
    dos.InvoicedQuantity AS CantidadProcedi,CASE WHEN dq.TotalSalesPrice IS NULL THEN dos.TotalSalesPrice ELSE dq.TotalSalesPrice END AS ValorUnitario,
    '0' as VALOR_COMPARTIDO_PACIENTE,'0' as VALOR_MODERADORA_PACIENTE,'0' as VALOR_COPAGO_PACIENTE,CASE WHEN dq.TotalSalesPrice IS NULL 
    THEN DF.GrandTotalSalesPrice ELSE dq.TotalSalesPrice END AS ValorTotalServicio,dos.AuthorizationNumber as CodAutorizacion,ing.CODDIAEGR as DiagnosticoPrincipal,'' as TIPO_DIAG,'' as DIAGNOSTICO_SECUNDARIO_1,
    '' as DIAGNOSTICO_SECUNDARIO_2,ing.IFECHAING as FECHA_ENTRADA,'' as HORA_ENTRADA,ing.FECREGCRE AS FECHA_SALIDA,'' as HORA_SALIDA,Cr.RadicatedConsecutive as Radicado,Cr.RadicatedDate as FechaRadicado
    FROM   Billing.Invoice AS F WITH (nolock) inner join
    Billing.RevenueControlDetail rcd on rcd.Id = F.Id inner join
    Billing.BillingAuthorization au on au.Id = rcd.BillingAuthorizationId inner join
    Common .ThirdParty as T on F.ThirdPartyId =T.Id inner join
    Billing.InvoiceDetail AS DF WITH (nolock) ON DF.InvoiceId = F.Id INNER JOIN
    Billing.ServiceOrderDetail AS dos WITH (nolock) ON dos.Id = DF.ServiceOrderDetailId INNER JOIN
    Billing.ServiceOrder AS os WITH (nolock) ON os.Id = dos.ServiceOrderId LEFT OUTER JOIN
    Billing.ServiceOrderDetailSurgical AS dq WITH (nolock) ON dq.ServiceOrderDetailId = dos.Id AND dq.OnlyMedicalFees = '0' INNER JOIN
    Common.OperatingUnit as U WITH (nolock) on F.operatingUnitid=U.id INNER JOIN
    Contract.HealthAdministrator as E WITH (nolock) on F.HealthAdministratorId =E.Id INNER JOIN
    Contract.CareGroup as G WITH (nolock) on F.caregroupid=G.id INNER JOIN
    dbo.ADINGRESO AS ing WITH (nolock) ON CAST(ing.NUMINGRES AS int) = F.AdmissionNumber INNER JOIN
    dbo.INPACIENT AS p WITH (nolock) ON p.IPCODPACI = F.PatientCode LEFT OUTER JOIN
    dbo.HCREGEGRE AS sal WITH (nolock) ON CAST(sal.NUMINGRES AS int) = F.AdmissionNumber LEFT OUTER JOIN
    Inventory.InventoryProduct AS pr WITH (nolock) ON pr.Id = dos.ProductId LEFT OUTER JOIN
    Contract.IPSService AS ServiciosIPS WITH (nolock) ON ServiciosIPS.Id = dos.IPSServiceId LEFT OUTER JOIN
    Portfolio.RadicateInvoiceD AS Dr on F.InvoiceNumber=Dr.InvoiceNumber LEFT OUTER JOIN
    Portfolio.RadicateInvoiceC AS Cr ON Dr.RadicateInvoiceCId=Cr.Id
    WHERE (F.Status = '1') AND (dos.IsDelete = '0') AND Cr.Id = @RadicateInvoiceCId ORDER BY dos.ServiceDate ASC
END