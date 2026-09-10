
-- =============================================
-- Author:		Juan F. Tamayo Puertas
-- Create date:	2016-08-23
-- Description:	Genera los datos para el archivo plano MegaRIPS por el Id de la RadicateInvoiceC
-- =============================================

CREATE PROCEDURE [Portfolio].[SP_GenerateMegaRIPS]
	@RadicateInvoiceCId AS integer, @companyCode as varchar(3)
AS
BEGIN
SET NOCOUNT ON;

	declare  @companyNit varchar(50)
SELECT TOP 1
	@companyNit = CompanyNit
FROM Security.ContainersInt
WHERE Code = @companyCode

SELECT
	*
FROM (SELECT
		au.InvoicePrefix AS Prefijo
	   ,REPLACE(F.InvoiceNumber, au.InvoicePrefix, '') AS Nume_Fac
	   ,'NI' AS Tipo_Doc_IPS
	   ,CAST(@companyNit AS INT) AS Num_Cod_IPS
	   ,dbo.CleanSpecialChars(U.IPSCode) AS Cod_Hab_IPS
	   ,E.HealthEntityCode AS Cod_Eps
	   ,
		/********CODIGO EPS CONTRI-SUB**************/
		--case E.Code
		--when 'EA0024' then 'EPS003'
		--when 'EA0025' then 'EPSS03' 
		--end as Cod_Eps,
		CASE
			WHEN G.LiquidationType = 1 THEN 2
			WHEN G.LiquidationType = 2 THEN 1
			WHEN G.LiquidationType = 3 THEN 6
			WHEN G.LiquidationType = 4 THEN 1
		END AS Cod_Cuenta
	   ,'' AS Cod_Contrato
	   ,F.InvoiceDate AS Fecha_Fact
	   ,F.TotalInvoice AS ValorBruto
	   ,CASE
			WHEN DF.RecoveryFeeType = 3 THEN F.TotalPatientWithDiscount
			WHEN DF.RecoveryFeeType = 1 THEN F.TotalPatientWithDiscount
			ELSE '0'
		END AS Copago
	   ,0 AS Valor_Copago_Compartido
	   ,0 AS Valor_Iva
	   ,0 AS Valor_Ico
	   ,CASE
			WHEN DF.RecoveryFeeType = 2 THEN F.TotalPatientWithDiscount
			ELSE 0
		END AS Valor_Moderadora
	   ,F.PatientDiscount AS Descuento
	   ,'0' AS Con_Des
	   ,F.ThirdPartySalesValue AS Valor_Neto
	   ,'' AS Periodo
	   ,7 AS Cod_Regional
	   ,CASE
			WHEN ing.ICAUSAING = '3' THEN 1
			WHEN ing.ICAUSAING = '10' THEN 2
			WHEN ing.ICAUSAING = '2' THEN 3
			WHEN ing.ICAUSAING = '6' THEN 4
			WHEN ing.ICAUSAING = '7' THEN 5
			ELSE 1
		END AS Clasificacion_Origen
	   ,CASE
			WHEN funct.UnitType IN (3, 4, 12, 13, 14, 15, 20, 21, 22, 24, 25) THEN '01' --'Ambulatorio' 
			WHEN funct.UnitType IN (2, 5, 6, 7, 8, 9, 10, 11, 16, 17, 18, 19) THEN '02' --'Hospitalización' 
			WHEN funct.UnitType IN (1, 23) THEN '03' --'Urgencias' 
			ELSE ''
		END AS Tipo_Servicio
	   ,'' AS Tipo_Paquete
	   ,'' AS Fin_Consulta
	   ,'' AS Dias_Trat
	   ,dbo.TipoDocumento(p.IPTIPODOC) AS Tdoc_Paciente
	   ,SUBSTRING(p.IPCODPACI, PATINDEX('%[^0]%', p.IPCODPACI + '.'), LEN(p.IPCODPACI)) AS Ndoc_Paciente
	   ,p.IPPRINOMB AS Nombre
	   ,p.IPSEGNOMB AS SegNombre
	   ,p.IPPRIAPEL AS Apellido
	   ,p.IPSEGAPEL AS SegApellido
	   ,(CAST(DATEDIFF(dd, p.IPFECNACI, [Common].[GETDATE]()) / 365.25 AS INT)) AS Edad
	   ,CASE
			WHEN p.IPSEXOPAC = '1' THEN 'M'
			WHEN p.IPSEXOPAC = '2' THEN 'F'
		END AS Sexo
	   ,CASE COALESCE(sal.ESTPACEGR, 1)
			WHEN '3' THEN 0
			WHEN '1' THEN 1
			WHEN '2' THEN 1
			WHEN '4' THEN 1
			ELSE 1
		END AS Estado_Paciente
	   ,'N' AS Discapacidad
	   ,CASE
			WHEN (CASE
					WHEN pr.Code IS NULL THEN cups.RIPSCode
					WHEN cups.RIPSCode IS NULL THEN pr.Code
				END) LIKE 'D%' THEN 'I'
			WHEN ISNUMERIC(CASE
					WHEN pr.Code IS NULL THEN cups.RIPSCode
					WHEN cups.RIPSCode IS NULL THEN pr.Code
				END) <> 0 THEN 'P'
			WHEN (CASE
					WHEN pr.Code IS NULL THEN cups.RIPSCode
					WHEN cups.RIPSCode IS NULL THEN pr.Code
				END) LIKE 'S%' THEN 'P'
			ELSE 'M'
		END AS Tipo_Prestacion
	   ,CASE
			WHEN pr.Code IS NULL THEN cups.RIPSCode
			WHEN cups.RIPSCode IS NULL THEN (CASE
					WHEN pr.Code LIKE 'M-%' THEN pr.CodeAlternativeTwo
					ELSE pr.Code
				END)

		END AS 'Codigo_facturacion_principal'
	   ,CASE
			WHEN Portfolio.GetCodeOrDescriptionCUPSMegaRIPS(dq.Id, 1) IS NULL THEN '0'
			ELSE [Contract].[Codigo_ProceQ](dq.Id)
		END
		AS Cod_procedi_Detalle
	   ,UPPER(
		CASE
			WHEN Portfolio.GetCodeOrDescriptionCUPSMegaRIPS(dq.Id, 0) IS NULL THEN (CASE
					WHEN pr.Name
						IS NULL THEN dbo.CleanSpecialChars(CUPS.RIPSDescription)
					ELSE CASE
							WHEN CUPS.RIPSDescription
								IS NULL THEN dbo.CleanSpecialChars(pr.Name)
						END
				END)
			ELSE Portfolio.GetCodeOrDescriptionCUPSMegaRIPS(dq.Id, 0)
		END) AS Descripcion_procedi
	   ,os.OrderDate AS FechaProcedi
	   ,'' AS HoraProcedi
	   ,dos.InvoicedQuantity AS CantidadProcedi
	   ,CASE
			WHEN dq.TotalSalesPrice IS NULL THEN dos.TotalSalesPrice
			ELSE dq.TotalSalesPrice
		END AS ValorUnitario
	   ,0 AS VALOR_COMPARTIDO_PACIENTE
	   ,CASE
			WHEN DF.RecoveryFeeType = 2 THEN CAST(F.TotalPatientWithDiscount AS INT)
			ELSE 0
		END AS VALOR_MODERADORA_PACIENTE
	   ,

		/**************************/

		CASE
			WHEN DF.RecoveryFeeType = 3 THEN (CASE
					WHEN dq.TotalSalesPrice IS NULL THEN (CASE
							WHEN portfolio.GetCopayValueMegaRIPS(F.InvoiceNumber, 1) = DF.GrandTotalSalesPrice THEN CAST(F.TotalPatientWithDiscount AS INT)
							ELSE 0
						END)
					ELSE (CASE
							WHEN portfolio.GetCopayValueMegaRIPS(F.InvoiceNumber, 0) = dq.TotalSalesPrice THEN CAST(F.TotalPatientWithDiscount AS INT)
							ELSE 0
						END)
				END)
			/*************************/
			WHEN DF.RecoveryFeeType = 1 THEN (CASE
					WHEN dq.TotalSalesPrice IS NULL THEN (CASE
							WHEN portfolio.GetCopayValueMegaRIPS(F.InvoiceNumber, 1) = DF.GrandTotalSalesPrice THEN CAST(F.TotalPatientWithDiscount AS INT)
							ELSE 0
						END)
					ELSE (CASE
							WHEN portfolio.GetCopayValueMegaRIPS(F.InvoiceNumber, 0) = dq.TotalSalesPrice THEN CAST(F.TotalPatientWithDiscount AS INT)
							ELSE 0
						END)
				END)
			ELSE 0
		END AS VALOR_COPAGO_PACIENTE
	   ,
		/***********/
		CASE
			WHEN DF.RecoveryFeeType = 3 THEN (
				CASE
					WHEN dq.TotalSalesPrice IS NULL THEN (CASE
							WHEN portfolio.GetCopayValueMegaRIPS(F.InvoiceNumber, 1) = DF.GrandTotalSalesPrice THEN CAST((DF.GrandTotalSalesPrice - F.TotalPatientWithDiscount) AS INT)
							ELSE CAST(DF.GrandTotalSalesPrice AS INT)
						END)
					ELSE (CASE
							WHEN portfolio.GetCopayValueMegaRIPS(F.InvoiceNumber, 0) = dq.TotalSalesPrice THEN CAST((dq.TotalSalesPrice - F.TotalPatientWithDiscount) AS INT)
							ELSE CAST(dq.TotalSalesPrice AS INT)
						END)
				END)
			/***********BONO PACIENTE********************/
			WHEN DF.RecoveryFeeType = 1 THEN (
				CASE
					WHEN dq.TotalSalesPrice IS NULL THEN (CASE
							WHEN portfolio.GetCopayValueMegaRIPS(F.InvoiceNumber, 1) = DF.GrandTotalSalesPrice THEN (DF.GrandTotalSalesPrice - F.TotalPatientWithDiscount)
							ELSE DF.GrandTotalSalesPrice
						END)
					ELSE (CASE
							WHEN portfolio.GetCopayValueMegaRIPS(F.InvoiceNumber, 0) = dq.TotalSalesPrice THEN (dq.TotalSalesPrice - F.TotalPatientWithDiscount)
							ELSE dq.TotalSalesPrice
						END)
				END)
			ELSE (CASE
					WHEN dq.TotalSalesPrice IS NULL THEN (CASE
							WHEN DF.RecoveryFeeType = 2 THEN (DF.GrandTotalSalesPrice - F.TotalPatientWithDiscount)
							ELSE DF.GrandTotalSalesPrice
						END)
					ELSE (CASE
							WHEN DF.RecoveryFeeType = 2 THEN (dq.TotalSalesPrice - F.TotalPatientWithDiscount)
							ELSE dq.TotalSalesPrice
						END)
				END)
		END AS ValorTotalServicio
	   ,
		/*************/
		dos.AuthorizationNumber AS CodAutorizacion
		--,ing.CODDIAEGR as DiagnosticoPrincipal,
	   ,F.OutputDiagnosis AS DiagnosticoPrincipal
	   ,'' AS TIPO_DIAG
	   ,'' AS DIAGNOSTICO_SECUNDARIO_1
	   ,'' AS DIAGNOSTICO_SECUNDARIO_2
	   ,ing.IFECHAING AS FECHA_ENTRADA
	   ,'' AS HORA_ENTRADA
	   ,CASE
			WHEN sal.FECALTPAC IS NULL THEN F.OutputDate
			ELSE sal.FECALTPAC
		END
		AS FECHA_SALIDA
	   ,'' AS HORA_SALIDA
	   ,dos.ServiceDate
	FROM Billing.Invoice AS F WITH (NOLOCK) 
	INNER JOIN Billing.RevenueControlDetail rcd ON rcd.Id = F.RevenueControlDetailId
	INNER JOIN Billing.BillingAuthorization au ON au.Id = rcd.BillingAuthorizationId
	INNER JOIN Common.ThirdParty AS T ON F.ThirdPartyId = T.Id
	INNER JOIN Billing.InvoiceDetail AS DF WITH (NOLOCK) ON DF.InvoiceId = F.Id
	INNER JOIN Billing.ServiceOrderDetail AS dos WITH (NOLOCK) ON dos.Id = DF.ServiceOrderDetailId
	INNER JOIN Payroll.FunctionalUnit AS funct WITH (NOLOCK) ON funct.Id = dos.PerformsFunctionalUnitId
	INNER JOIN Billing.ServiceOrder AS os WITH (NOLOCK) ON os.Id = dos.ServiceOrderId
	LEFT OUTER JOIN Billing.ServiceOrderDetailSurgical AS dq WITH (NOLOCK) ON dq.ServiceOrderDetailId = dos.Id AND dq.OnlyMedicalFees = '0'
	LEFT OUTER JOIN [Contract].CUPSEntity AS cups ON dos.CUPSEntityId = cups.Id
	INNER JOIN Common.OperatingUnit AS U WITH (NOLOCK) ON F.operatingUnitid = U.id
	INNER JOIN Contract.HealthAdministrator AS E WITH (NOLOCK) ON F.HealthAdministratorId = E.Id
	INNER JOIN Contract.CareGroup AS G WITH (NOLOCK) ON F.caregroupid = G.id
	INNER JOIN dbo.ADINGRESO AS ing WITH (NOLOCK) ON ing.NUMINGRES = F.AdmissionNumber
	INNER JOIN dbo.INPACIENT AS p WITH (NOLOCK) ON p.IPCODPACI = F.PatientCode
	LEFT OUTER JOIN dbo.HCREGEGRE AS sal WITH (NOLOCK) ON sal.NUMINGRES = F.AdmissionNumber
	LEFT OUTER JOIN Inventory.InventoryProduct AS pr WITH (NOLOCK) ON pr.Id = dos.ProductId
	LEFT OUTER JOIN Contract.IPSService AS ServiciosIPS WITH (NOLOCK) ON ServiciosIPS.Id = dos.IPSServiceId
	LEFT OUTER JOIN Portfolio.RadicateInvoiceD AS Dr ON F.InvoiceNumber = Dr.InvoiceNumber
	LEFT OUTER JOIN Portfolio.RadicateInvoiceC AS Cr ON Dr.RadicateInvoiceCId = Cr.Id
	WHERE (F.Status = '1') AND (dos.IsDelete = '0') AND Cr.Id = @RadicateInvoiceCId

	UNION ALL

	SELECT
		au.InvoicePrefix AS Prefijo
	   ,REPLACE(F.InvoiceNumber, au.InvoicePrefix, '') AS Nume_Fac
	   ,'NI' AS Tipo_Doc_IPS
	   ,CAST(@companyNit AS INT) AS Num_Cod_IPS
	   ,dbo.CleanSpecialChars(U.IPSCode) AS Cod_Hab_IPS
	   ,E.HealthEntityCode AS Cod_Eps
	   ,CASE
			WHEN G.LiquidationType = 1 THEN 2
			WHEN G.LiquidationType = 2 THEN 1
			WHEN G.LiquidationType = 3 THEN 6
			WHEN G.LiquidationType = 4 THEN 1
		END AS Cod_Cuenta
	   ,'' AS Cod_Contrato
	   ,F.InvoiceDate AS Fecha_Fact
	   ,F.TotalInvoice AS ValorBruto
	   ,CASE
			WHEN DF.RecoveryFeeType = 3 THEN F.TotalPatientWithDiscount
			WHEN DF.RecoveryFeeType = 1 THEN F.TotalPatientWithDiscount
			ELSE '0'
		END AS Copago
	   ,0 AS Valor_Copago_Compartido
	   ,0 AS Valor_Iva
	   ,0 AS Valor_Ico
	   ,CASE
			WHEN DF.RecoveryFeeType = 2 THEN F.TotalPatientWithDiscount
			ELSE 0
		END AS Valor_Moderadora
	   ,F.PatientDiscount AS Descuento
	   ,'0' AS Con_Des
	   ,F.ThirdPartySalesValue AS Valor_Neto
	   ,'' AS Periodo
	   ,7 AS Cod_Regional
	   ,CASE
			WHEN ing.ICAUSAING = '3' THEN 1
			WHEN ing.ICAUSAING = '10' THEN 2
			WHEN ing.ICAUSAING = '2' THEN 3
			WHEN ing.ICAUSAING = '6' THEN 4
			WHEN ing.ICAUSAING = '7' THEN 5
			ELSE 1
		END AS Clasificacion_Origen
	   ,CASE
			WHEN funct.UnitType IN (3, 4, 12, 13, 14, 15, 20, 21, 22, 24, 25) THEN '01' --'Ambulatorio' 
			WHEN funct.UnitType IN (2, 5, 6, 7, 8, 9, 10, 11, 16, 17, 18, 19) THEN '02' --'Hospitalización' 
			WHEN funct.UnitType IN (1, 23) THEN '03' --'Urgencias' 
			ELSE ''
		END AS Tipo_Servicio
	   ,'' AS Tipo_Paquete
	   ,'' AS Fin_Consulta
	   ,'' AS Dias_Trat
	   ,dbo.TipoDocumento(p.IPTIPODOC) AS Tdoc_Paciente
	   ,SUBSTRING(p.IPCODPACI, PATINDEX('%[^0]%', p.IPCODPACI + '.'), LEN(p.IPCODPACI)) AS Ndoc_Paciente
	   ,p.IPPRINOMB AS Nombre
	   ,p.IPSEGNOMB AS SegNombre
	   ,p.IPPRIAPEL AS Apellido
	   ,p.IPSEGAPEL AS SegApellido
	   ,(CAST(DATEDIFF(dd, p.IPFECNACI, [Common].[GETDATE]()) / 365.25 AS INT)) AS Edad
	   ,CASE
			WHEN p.IPSEXOPAC = '1' THEN 'M'
			WHEN p.IPSEXOPAC = '2' THEN 'F'
		END AS Sexo
	   ,CASE COALESCE(sal.ESTPACEGR, 1)
			WHEN '3' THEN 0
			WHEN '1' THEN 1
			WHEN '2' THEN 1
			WHEN '4' THEN 1
			ELSE 1
		END AS Estado_Paciente
	   ,'N' AS Discapacidad
	   ,CASE
			WHEN (CASE
					WHEN pr.Code IS NULL THEN cups.RIPSCode
					WHEN cups.RIPSCode IS NULL THEN pr.Code
				END) LIKE 'D%' THEN 'I'
			WHEN ISNUMERIC(CASE
					WHEN pr.Code IS NULL THEN cups.RIPSCode
					WHEN cups.RIPSCode IS NULL THEN pr.Code
				END) <> 0 THEN 'P'
			WHEN (CASE
					WHEN pr.Code IS NULL THEN cups.RIPSCode
					WHEN cups.RIPSCode IS NULL THEN pr.Code
				END) LIKE 'S%' THEN 'P'
			ELSE 'M'
		END AS Tipo_Prestacion
	   ,CASE
			WHEN pr.Code IS NULL THEN cups.RIPSCode
			WHEN cups.RIPSCode IS NULL THEN (CASE
					WHEN pr.Code LIKE 'M-%' THEN pr.CodeAlternativeTwo
					ELSE pr.Code
				END)
		END AS 'Codigo_facturacion_principal'
	   ,cups.Code AS Cod_procedi_Detalle
	   ,cups.Description AS Descripcion_procedi
	   ,os.OrderDate AS FechaProcedi
	   ,'' AS HoraProcedi
	   ,dos.InvoicedQuantity AS CantidadProcedi
	   ,0 AS ValorUnitario
	   ,0 AS VALOR_COMPARTIDO_PACIENTE
	   ,CASE
			WHEN DF.RecoveryFeeType = 2 THEN CAST(F.TotalPatientWithDiscount AS INT)
			ELSE 0
		END AS VALOR_MODERADORA_PACIENTE
	   ,0 AS VALOR_COPAGO_PACIENTE
	   ,0 AS ValorTotalServicio
	   ,dos.AuthorizationNumber AS CodAutorizacion
		--,ing.CODDIAEGR as DiagnosticoPrincipal,
	   ,F.OutputDiagnosis AS DiagnosticoPrincipal
	   ,'' AS TIPO_DIAG
	   ,'' AS DIAGNOSTICO_SECUNDARIO_1
	   ,'' AS DIAGNOSTICO_SECUNDARIO_2
	   ,ing.IFECHAING AS FECHA_ENTRADA
	   ,'' AS HORA_ENTRADA
	   ,CASE
			WHEN sal.FECALTPAC IS NULL THEN F.OutputDate
			ELSE sal.FECALTPAC
		END
		AS FECHA_SALIDA
	   ,'' AS HORA_SALIDA
	   ,dos.ServiceDate
	FROM Billing.Invoice AS F WITH (NOLOCK)
	INNER JOIN Billing.RevenueControlDetail rcd ON rcd.Id = F.RevenueControlDetailId
	INNER JOIN Billing.BillingAuthorization au ON au.Id = rcd.BillingAuthorizationId
	INNER JOIN Common.ThirdParty AS T ON F.ThirdPartyId = T.Id
	INNER JOIN Billing.InvoiceDetail AS DF WITH (NOLOCK) ON DF.InvoiceId = F.Id
	INNER JOIN Billing.ServiceOrderDetail AS dos WITH (NOLOCK) ON dos.Id = DF.ServiceOrderDetailId
	INNER JOIN Payroll.FunctionalUnit AS funct WITH (NOLOCK) ON funct.Id = dos.PerformsFunctionalUnitId
	INNER JOIN Billing.ServiceOrder AS os WITH (NOLOCK) ON os.Id = dos.ServiceOrderId
	LEFT OUTER JOIN [Contract].CUPSEntity AS cups ON dos.CUPSEntityId = cups.Id
	INNER JOIN Common.OperatingUnit AS U WITH (NOLOCK) ON F.operatingUnitid = U.id
	INNER JOIN Contract.HealthAdministrator AS E WITH (NOLOCK) ON F.HealthAdministratorId = E.Id
	INNER JOIN Contract.CareGroup AS G WITH (NOLOCK) ON F.caregroupid = G.id
	INNER JOIN dbo.ADINGRESO AS ing WITH (NOLOCK) ON ing.NUMINGRES = F.AdmissionNumber
	INNER JOIN dbo.INPACIENT AS p WITH (NOLOCK) ON p.IPCODPACI = F.PatientCode
	LEFT OUTER JOIN dbo.HCREGEGRE AS sal WITH (NOLOCK) ON sal.NUMINGRES = F.AdmissionNumber
	LEFT OUTER JOIN Inventory.InventoryProduct AS pr WITH (NOLOCK) ON pr.Id = dos.ProductId
	LEFT OUTER JOIN Contract.IPSService AS ServiciosIPS WITH (NOLOCK) ON ServiciosIPS.Id = dos.IPSServiceId
	LEFT OUTER JOIN Portfolio.RadicateInvoiceD AS Dr ON F.InvoiceNumber = Dr.InvoiceNumber
	LEFT OUTER JOIN Portfolio.RadicateInvoiceC AS Cr ON Dr.RadicateInvoiceCId = Cr.Id
	WHERE (F.Status = '1') AND (dos.IsDelete = '0')
						AND Cr.Id = @RadicateInvoiceCId
						AND dos.Presentation = 2) T
ORDER BY T.ServiceDate

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el archivo plano MegaRIPS requerido por la Superintendencia de Salud de Colombia, consolidando en una sola consulta todos los datos de facturación, servicios prestados y atención al paciente necesarios para el reporte. Toma como entrada el identificador de una radicación de facturas y el código de la empresa, y cruza información de facturas, detalles de facturación, órdenes de servicio, procedimientos quirúrgicos, unidades funcionales, terceros pagadores (EPS) y datos demográficos y clínicos del paciente (cédula, nombre, edad, sexo, diagnóstico, tipo de prestación, códigos CUPS/RIPS). Clasifica cada prestación según tipo de servicio (ambulatorio, hospitalización, urgencias), tipo de cuota de recuperación (copago, cuota moderadora) y causa de ingreso, produciendo las columnas exigidas por la estructura MegaRIPS para ser enviadas a la entidad reguladora o a la EPS contratante.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateMegaRIPS';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateMegaRIPS';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el conjunto de datos del archivo plano MegaRIPS para una radicación de facturas, consolidando información de facturación, paciente, ingreso/egreso, procedimientos y valores de cuotas moderadoras/copagos por cada factura asociada al radicado.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateMegaRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un Portfolio.RadicateInvoiceC con el Id recibido y sus RadicateInvoiceD vinculadas a las facturas a reportar.; Las facturas (Billing.Invoice) deben tener Status = ''1'' (activas).; El detalle de orden de servicio (ServiceOrderDetail) no debe estar marcado como eliminado (IsDelete = ''0'').; Debe existir un registro en Security.ContainersInt con Code = @companyCode para resolver el NIT de la compañía.; Cada factura debe tener ingreso (ADINGRESO) y paciente (INPACIENT) coincidentes por AdmissionNumber/PatientCode.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateMegaRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan facturas con Status=''1'' y detalles con IsDelete=''0''.; El NIT de la IPS (Num_Cod_IPS) siempre se toma de Security.ContainersInt según el código de compañía.; Cod_Regional siempre se emite con valor fijo 7.; Tipo_Doc_IPS siempre es ''NI'' (NIT) y Discapacidad siempre ''N''.; Valor_Iva, Valor_Ico, Valor_Copago_Compartido y VALOR_COMPARTIDO_PACIENTE siempre van en 0.; La edad se calcula en años usando 365.25 días contra Common.GETDATE().; El número de documento del paciente se reporta sin ceros a la izquierda (PATINDEX ''%[^0]%'').; El número de factura se reporta sin el prefijo de la resolución (REPLACE con au.InvoicePrefix).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateMegaRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset MegaRIPS: Devuelve un conjunto unificado (UNION ALL) ordenado por ServiceDate con los registros del archivo plano MegaRIPS para la radicación @RadicateInvoiceCId.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateMegaRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si G.LiquidationType = 1 / 2 / 3 / 4 → Cod_Cuenta se mapea a 2 / 1 / 6 / 1 respectivamente (tipo de cuenta MegaRIPS).; si DF.RecoveryFeeType = 3 o 1 → Copago = F.TotalPatientWithDiscount; se calculan VALOR_COPAGO_PACIENTE y ValorTotalServicio aplicando descuento al paciente cuando GetCopayValueMegaRIPS coincide con el valor total de venta. else Copago = 0; si DF.RecoveryFeeType = 2 → Valor_Moderadora y VALOR_MODERADORA_PACIENTE = F.TotalPatientWithDiscount; ValorTotalServicio se calcula descontando F.TotalPatientWithDiscount del precio total. else Valor_Moderadora = 0; si ing.ICAUSAING IN (''3'',''10'',''2'',''6'',''7'') → Clasificacion_Origen se mapea a 1/2/3/4/5 respectivamente. else Clasificacion_Origen = 1; si funct.UnitType en ciertos rangos → Tipo_Servicio = ''01'' Ambulatorio (3,4,12-15,20-22,24,25), ''02'' Hospitalización (2,5-11,16-19) o ''03'' Urgencias (1,23). else Tipo_Servicio = ''''; si Código de procedimiento (pr.Code o cups.RIPSCode) empieza por ''D'', es numérico, empieza por ''S'' u otro → Tipo_Prestacion = ''I'' (insumo/diagnóstico), ''P'' (procedimiento) o ''M'' (medicamento) según el patrón.; si pr.Code LIKE ''M-%'' → Codigo_facturacion_principal usa pr.CodeAlternativeTwo en vez de pr.Code (medicamentos).; si Existe ServiceOrderDetailSurgical (dq) con OnlyMedicalFees=''0'' → Usa dq.TotalSalesPrice y consulta GetCopayValueMegaRIPS con flag 0 (a nivel quirúrgico). else Usa DF.GrandTotalSalesPrice/dos.TotalSalesPrice y consulta con flag 1.; si sal.FECALTPAC IS NULL → FECHA_SALIDA = F.OutputDate. else FECHA_SALIDA = sal.FECALTPAC (fecha real del egreso hospitalario).; si p.IPSEXOPAC = ''1'' / ''2'' → Sexo = ''M'' / ''F'' respectivamente.; si COALESCE(sal.ESTPACEGR,1) = ''3'' → Estado_Paciente = 0 (fallecido). else Estado_Paciente = 1 (vivo) para los demás estados.; si Segunda consulta del UNION ALL: dos.Presentation = 2 → Se incluyen además los detalles con presentación 2 (paquetes/medicamentos) usando cups.Code y cups.Description directamente y ValorUnitario/ValorTotalServicio = 0.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateMegaRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.CleanSpecialChars; dbo.TipoDocumento; Common.GETDATE; Portfolio.GetCodeOrDescriptionCUPSMegaRIPS; Contract.Codigo_ProceQ; Portfolio.GetCopayValueMegaRIPS', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateMegaRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateMegaRIPS';
-- GO
