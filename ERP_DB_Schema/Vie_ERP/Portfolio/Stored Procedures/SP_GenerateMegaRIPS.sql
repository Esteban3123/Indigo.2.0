
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