

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el archivo plano MegaRIPS para un radicado de facturas (RadicateInvoiceCId), consolidando en una sola consulta toda la información requerida para la presentación de cuentas médicas ante las EPS y administradoras de salud. Integra datos de encabezado y detalle de facturas (Billing.Invoice, InvoiceDetail), órdenes de servicio y procedimientos facturados (ServiceOrder, ServiceOrderDetail, ServiceOrderDetailSurgical), autorizaciones DIAN (BillingAuthorization), datos del paciente como cédula, nombre, edad y sexo (INPACIENT), información del ingreso hospitalario con diagnóstico de egreso y causa de ingreso (ADINGRESO), registro de egreso (HCREGEGRE), sede de atención (OperatingUnit), EPS o pagador (HealthAdministrator), grupo de atención y tipo de liquidación (CareGroup), y el consecutivo y fecha del radicado (RadicateInvoiceC/D). El resultado incluye campos obligatorios del estándar RIPS como prefijo y número de factura, tipo y documento del paciente, clasificación del origen de atención, códigos de procedimientos o medicamentos, diagnóstico principal, fechas de entrada y salida, valores brutos, copagos, cuotas moderadoras y valor neto, ordenados por fecha de servicio para ser consumidos en el proceso de radicación y glosas ante terceros pagadores.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateMegaRIPS';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateMegaRIPS';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el conjunto de datos plano (formato MegaRIPS) con encabezado de factura, paciente, ingreso/egreso y detalle de servicios facturados, para todas las facturas asociadas a un radicado de cuenta de cobro.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateMegaRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un radicado de cuenta de cobro (Portfolio.RadicateInvoiceC) con el Id solicitado y facturas asociadas en Portfolio.RadicateInvoiceD.; Las facturas deben tener Status=''1'' y estar enlazadas a un control de ingresos con autorización de facturación vigente.; Debe existir el ingreso del paciente (ADINGRESO) con NUMINGRES coincidente con AdmissionNumber de la factura.; Debe existir el paciente en INPACIENT con IPCODPACI igual a PatientCode de la factura.; Las órdenes de servicio referenciadas deben estar activas (IsDelete=''0'').', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateMegaRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen facturas con Status=''1'' (activas/vigentes).; Solo se incluyen ítems de orden de servicio no eliminados (IsDelete=''0'').; Solo se consideran detalles quirúrgicos con OnlyMedicalFees=''0'' (excluye honorarios médicos puros).; El proceso filtra exclusivamente por un radicado (RadicateInvoiceC) específico.; Los resultados se ordenan por fecha de servicio ascendente.; Campos como Valor_Copago_Compartido, Valor_Iva, Valor_Ico, Cod_Contrato, Periodo, Tipo_Servicio, Tipo_Paquete, Fin_Consulta, Dias_Trat, HoraProcedi, Discapacidad, TIPO_DIAG, DIAGNOSTICO_SECUNDARIO_1/2, HORA_ENTRADA, HORA_SALIDA se entregan vacíos o ''0'' por diseño.; Discapacidad siempre se reporta como ''N''.; Cod_Regional siempre es ''7''.; Num_Cod_IPS está fijo a ''813001952'' y Tipo_Doc_IPS a ''NI'' (NIT institucional).; La edad se calcula en años enteros usando Common.GETDATE() y 365.25 días/año.; Si existe precio en detalle quirúrgico (dq) tiene prioridad sobre el precio del detalle de orden o de la factura.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateMegaRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'MegaRIPS; Factura; Radicación de factura; Autorización de facturación (DIAN); EPS / Administradora de salud; Paciente; Ingreso/Egreso hospitalario; Diagnóstico (CIE); Copago; Cuota moderadora; Procedimiento; Medicamento/Insumo; Orden de servicio; Servicio quirúrgico; Causa de ingreso; Tipo de documento; Estado del paciente al egreso; IPS; Contrato/Grupo de atención', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateMegaRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset MegaRIPS): Devuelve un resultset con datos de factura, paciente, ingreso, egreso y detalle de servicios para todas las facturas del radicado solicitado, filtrando por Invoice.Status=''1'', ServiceOrderDetail.IsDelete=''0'' y RadicateInvoiceC.Id = parámetro.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateMegaRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CareGroup.LiquidationType en {1,2,3,4} → Mapea Cod_Cuenta a {1→''2'', 2→''1'', 3→''6'', 4→''1''} (tipo de cuenta MegaRIPS); si InvoiceDetail.RecoveryFeeType = 3 → Copago = TotalPatientWithDiscount; en otro caso Copago = ''0'' else Copago=''0''; si InvoiceDetail.RecoveryFeeType = 2 → Valor_Moderadora = TotalPatientWithDiscount; en otro caso ''0'' else Valor_Moderadora=''0''; si ADINGRESO.ICAUSAING en {3,10,2,6,7} → Clasificacion_Origen mapeada a {3→1, 10→2, 2→3, 6→4, 7→5}; si INPACIENT.IPTIPODOC en {1..7} → Tdoc_Paciente mapeado a {CC,CE,TI,RC,PA,AS,MS}; si INPACIENT.IPSEXOPAC → Sexo: 1→''M'', 2→''F''; si HCREGEGRE.ESTPACEGR → Estado_Paciente: 3→''0''; 1,2,4 o NULL → ''1'' (vivo/fallecido); si ServiceOrderDetail.RecordType → Tipo_Prestacion: 1→''P'' (procedimiento), 2→''M'' (medicamento/insumo); si InventoryProduct.Code IS NULL → Usa IPSService.Code/Name como código y descripción de facturación else Usa InventoryProduct.Code/Name; si ServiceOrderDetailSurgical.TotalSalesPrice IS NULL → ValorUnitario y ValorTotalServicio toman valores de ServiceOrderDetail/InvoiceDetail (no quirúrgico) else Toma TotalSalesPrice del detalle quirúrgico', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateMegaRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.RevenueControlDetail; Billing.BillingAuthorization; Common.ThirdParty; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Billing.ServiceOrder; Billing.ServiceOrderDetailSurgical; Common.OperatingUnit; Contract.HealthAdministrator; Contract.CareGroup; dbo.ADINGRESO; dbo.INPACIENT; dbo.HCREGEGRE; Inventory.InventoryProduct; Contract.IPSService; Portfolio.RadicateInvoiceD; Portfolio.RadicateInvoiceC', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateMegaRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateMegaRIPS';
-- GO
