-- ===============================================================================================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-04-06
-- Description:	Procedimiento que se encarga de traer información adicional de la factura
-- ==============================================================================================================
CREATE PROCEDURE [Billing].[SP_GetInvoiceMoreInformationByInvoiceId]
	@InvoiceId INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @IsAllPackage BIT =0
	DECLARE @IFECHAING DATETIME = NULL

	DECLARE @Invoice as Table (	Id int,
								DocumentType TINYINT,
								PatientAffiliatedType INT,
								PatientType INT,
								AdmissionNumber CHAR(10),
								CareGroupId INT,
								OperatingUnitId INT,
								InvoiceDate DATETIME,
								PatientCode VARCHAR(25),
								ContractId INT,
								RevenueControlDetailId INT
								)

	declare @DetailsTable as Table (InvoiceId int,
									RecoveryFeeType tinyint,
									SubTotalPatientSalesPrice numeric(20,2),
									GrandTotalSalesPrice numeric(20,2),
									ThirdPartySalesPrice numeric(20,2),
									IsPackage bit,
									DistributionType tinyint,
									ServiceDate DateTime,
									InvoiceIdCP int,
									AdmissionNumber char(10)
									)
	DECLARE @DetailsGroup as Table (	InvoiceId INT,
										CopayValue NUMERIC(20,2),
										FeeModeratorValue NUMERIC(20,2),
										FeeRecoveryValue NUMERIC(20,2),
										DistributedValue NUMERIC(20,2),
										FirstServiceDate DATETIME
										)

	INSERT INTO @Invoice (	Id,
							DocumentType,
							PatientAffiliatedType,
							PatientType,
							AdmissionNumber,
							CareGroupId,
							OperatingUnitId,
							InvoiceDate,
							PatientCode,
							ContractId,
							RevenueControlDetailId)

					SELECT	i.Id,
							i.DocumentType,
							i.PatientAffiliatedType,
							i.PatientType,
							i.AdmissionNumber,
							i.CareGroupId,
							i.OperatingUnitId,
							i.InvoiceDate,
							i.PatientCode,
							i.ContractId,
							i.RevenueControlDetailId
					FROM Billing.Invoice i WITH(NOLOCK)
					where i.Id =@InvoiceId

		IF EXISTS( SELECT 1 FROM @Invoice WHERE DocumentType =4) BEGIN

			WITH Cte_InvoiceEntityCapitated as (SELECT	i.Id,
														COALESCE(iecP.InitialDate,iec.InitialDate) as InitialDate,
														COALESCE(iecP.EndDate, iec.EndDate) as EndDate,
														i.CareGroupId,
														iec.InvoiceCategoryId
												FROM @Invoice i
												join Billing.InvoiceEntityCapitated iec WITH(NOLOCK) on i.Id = iec.InvoiceId
												LEFT JOIN Billing.InvoiceEntityCapitated iecP WITH(NOLOCK) on iec.PreviousRIPSInvoice = iecp.Id
												where (iec.InvoicePeriod is null OR iec.InvoicePeriod = 2)
												)

			INSERT INTO @DetailsTable (		InvoiceId,--1
											RecoveryFeeType,--2
											SubTotalPatientSalesPrice,--3
											GrandTotalSalesPrice,--4
											ThirdPartySalesPrice,--5
											IsPackage,--6
											DistributionType,--7
											ServiceDate,--8
											InvoiceIdCP , --9
											AdmissionNumber --10
											)
				
				SELECT	cte.Id,--1
						id.RecoveryFeeType,--2
						id.SubTotalPatientSalesPrice,--3
						id.GrandTotalSalesPrice,--4
						id.ThirdPartySalesPrice,--5
						sod.IsPackage,--6
						id.DistributionType,--7
						id.ServiceDate,--8
						id.InvoiceId, --9
						i.AdmissionNumber --10
				from  Billing.InvoiceDetail id WITH(NOLOCK)
				join Billing.Invoice i WITH(NOLOCK) on id.InvoiceId =i.Id
				join Billing.ServiceOrderDetail sod WITH(NOLOCK) on id.ServiceOrderDetailId = sod.Id
				join Cte_InvoiceEntityCapitated cte 
						on i.CareGroupId = cte.CareGroupId and i.InvoiceCategoryId = cte.InvoiceCategoryId
							and cast(i.InvoiceDate as DATE) BETWEEN cast(cte.InitialDate as DATE) and CAST( cte.EndDate as DATE) 
				WHERE i.DocumentType = 5 AND i.Status = 1

				SELECT top 1 @IFECHAING = ad.IFECHAING
				from ADINGRESO ad WITH(NOLOCK)
				JOIN (	SELECT AdmissionNumber
						FROM @DetailsTable icd
						GROUP by icd.AdmissionNumber) tmp on ad.NUMINGRES = tmp.AdmissionNumber
				ORDER by ad.IFECHAING ASC

		END
		ELSE BEGIN

				SELECT top 1 @IFECHAING = ad.IFECHAING
				FROM @Invoice i
				JOIN ADINGRESO ad WITH(NOLOCK) on i.AdmissionNumber = ad.NUMINGRES
			
				INSERT INTO @DetailsTable (	InvoiceId,--1
											RecoveryFeeType,--2
											SubTotalPatientSalesPrice,--3
											GrandTotalSalesPrice,--4
											ThirdPartySalesPrice,--5
											IsPackage,--6
											DistributionType,--7
											ServiceDate--8
											)
				SELECT	id.InvoiceId,--1
						id.RecoveryFeeType,--2
						id.SubTotalPatientSalesPrice,--3
						id.GrandTotalSalesPrice,--4
						id.ThirdPartySalesPrice,--5
						sod.IsPackage,--6
						id.DistributionType,--7
						id.ServiceDate--8
				from  Billing.InvoiceDetail id WITH(NOLOCK)
				join Billing.ServiceOrderDetail sod WITH(NOLOCK) on id.ServiceOrderDetailId = sod.Id
				WHERE id.InvoiceId = @InvoiceId 

				IF(1 = 	ALL(select COALESCE(tmp.IsPackage,0)
							from  @DetailsTable tmp)) BEGIN
							SET @IsAllPackage =1
				END
		END

		INSERT INTO @DetailsGroup (	InvoiceId,
									CopayValue,
									FeeModeratorValue,
									FeeRecoveryValue,
									DistributedValue,
									FirstServiceDate)

		SELECT	tmp.InvoiceId,
				IIF(cg.MethodFixedAmountCollectionReport = 1, SUM(IIF(tmp.RecoveryFeeType = 3, tmp.SubTotalPatientSalesPrice, 0)), 0) CopayValue,
				IIF(cg.MethodFixedAmountCollectionReport = 1, SUM(IIF(tmp.RecoveryFeeType = 2, tmp.SubTotalPatientSalesPrice, 0)), 0) FeeModeratorValue,
				IIF(cg.MethodFixedAmountCollectionReport = 1, SUM(IIF(tmp.RecoveryFeeType = 5, tmp.SubTotalPatientSalesPrice, 0)), 0) FeeRecoveryValue,
				SUM
				(
					IIF
					(
						tmp.DistributionType > 1,
						(tmp.GrandTotalSalesPrice - tmp.ThirdPartySalesPrice - tmp.SubTotalPatientSalesPrice),
						0
					)
				) DistributedValue,
				MIN(tmp.ServiceDate) FirstServiceDate
		FROM @DetailsTable tmp
		INNER JOIN @Invoice inv ON tmp.InvoiceId = inv.Id
		INNER JOIN Contract.CareGroup cg WITH (NOLOCK) ON inv.CareGroupId = cg.Id
		GROUP BY tmp.InvoiceId, cg.MethodFixedAmountCollectionReport

	SELECT	ou.IPSCode,
			CASE pacient.IPTIPODOC
				WHEN  1 THEN 'CC'
				WHEN  2 THEN 'CE'
				WHEN  3 THEN 'TI'
				WHEN  4 THEN 'RC'
				WHEN  5 THEN 'PA'
				WHEN  6 THEN 'AS'
				WHEN  7 THEN 'MS'
				WHEN  8 THEN 'NU'
				WHEN  9 THEN 'CN'
				WHEN 10 THEN 'CD'
				WHEN 11 THEN 'SC'
				WHEN 12 THEN 'PE'

			END AS IdentificationTypeCode,
			CASE pacient.IPTIPODOC
				WHEN  1 THEN 'Cédula de ciudadanía'
				WHEN  2 THEN 'Cédula de extranjería'
				WHEN  3 THEN 'Carné diplomático'
				WHEN  4 THEN 'Pasaporte'
				WHEN  5 THEN 'Salvoconducto'
				WHEN  6 THEN 'Permiso especial de permanencia'
				WHEN  7 THEN 'Registro civil de nacimiento'
				WHEN  8 THEN 'Tarjeta de identidad'
				WHEN  9 THEN 'Certificado de nacido vivo'
				WHEN 10 THEN 'Adulto sin identificar'
				WHEN 11 THEN 'Menor sin identificar'
				WHEN 12 THEN 'Documento extranjero'

			END AS IdentificationTypeDescription,
			LTRIM(RTRIM(pacient.IPCODPACI)) AS IdentificationNumber,
			LTRIM(RTRIM(pacient.IPPRIAPEL)) AS FirstLastName, 
			LTRIM(RTRIM(pacient.IPSEGAPEL)) AS SecondLastName, 
			LTRIM(RTRIM(pacient.IPPRINOMB)) AS FirstName, 
			LTRIM(RTRIM(pacient.IPSEGNOMB)) AS SecondName,
			CASE i.DocumentType
				WHEN 3 THEN '08'
				ELSE
					CASE 
						WHEN cg.EntityType = 9 THEN
							CASE i.PatientAffiliatedType											
								WHEN 2 THEN '06'
								ELSE '07'
							END
						WHEN cg.EntityType = 5 THEN '09'
						WHEN cg.EntityType = 13 AND cg.ConceptToBill = 13 THEN '10'
						ELSE
							CASE i.PatientType
								WHEN 1 THEN
									CASE i.PatientAffiliatedType
										WHEN 2 THEN '02'
										WHEN 3 THEN '03'
										ELSE '01'
									END
								WHEN 2 THEN '04'
								WHEN 4 THEN '08'
								ELSE '05'
							END 
					END
			END PatientTypeCode,
			CASE i.DocumentType
				WHEN 3 THEN 'Particular'
				ELSE
					CASE 
						WHEN cg.EntityType = 9 THEN 
							CONCAT
							(
								'Especiales o de Excepción',
								CASE i.PatientAffiliatedType											
									WHEN 2 THEN ' beneficiario'
									ELSE ' cotizante'
								END
							)
						WHEN cg.EntityType = 5 THEN 'Tomador/Amparado ARL'
						WHEN cg.EntityType = 13 AND cg.ConceptToBill = 13 THEN 'Tomador/Amparado SOAT'
						ELSE
							CASE i.PatientType
								WHEN 1 THEN
									CONCAT
									(
										'Contributivo',
										CASE i.PatientAffiliatedType									
											WHEN 2 THEN ' beneficiario'
											WHEN 3 THEN ' adicional'
											ELSE ' cotizante'
										END
									)
								WHEN 2 THEN 'Subsidiado'
								WHEN 4 THEN 'Particular'
								ELSE 'Sin régimen'
							END 
					END
			END PatientTypeDescription,
			CASE 
				WHEN @IsAllPackage =1 THEN '01'
				ELSE CASE cg.LiquidationType
						WHEN 1 THEN '04'
						WHEN 2 THEN '03'
						WHEN 5 THEN '02'
				END
			END LiquidationTypeCode,
			CASE 
				WHEN @IsAllPackage =1 THEN 'Pago individual por caso / Conjunto integral de atenciones / Paquete / Canasta'
				ELSE CASE cg.LiquidationType
						WHEN 1 THEN 'Pago por evento'
						WHEN 2 THEN 'Pago por capitación'
						WHEN 5 THEN 'Pago global prospectivo'
				END
			END LiquidationTypeDescription,
			CASE 
				-- WHEN cg.ConceptToBill in (1,3,4,17) then '01'
				WHEN cg.ConceptToBill = 18 THEN '02'
				WHEN cg.ConceptToBill = 20 THEN '03'
				WHEN cg.ConceptToBill = 13 THEN '04'
				WHEN cg.ConceptToBill = 11 THEN '05'
				WHEN cg.ConceptToBill in (14,15) THEN '06'
				WHEN cg.ConceptToBill = 8 THEN '07'
				WHEN cg.ConceptToBill in (10,12,16) THEN '08'
				WHEN cg.ConceptToBill = 19 THEN '09'
				WHEN cg.ConceptToBill = 2 THEN '10'
				WHEN cg.ConceptToBill = 5 THEN '11'
				WHEN cg.ConceptToBill in (6,17) THEN '12'
				WHEN cg.ConceptToBill = 9 THEN '13'
				WHEN cg.ConceptToBill = 21 THEN '14'
				WHEN cg.ConceptToBill in (7,4) THEN '15'
				WHEN cg.ConceptToBill = '1' THEN '16'
				WHEN cg.ConceptToBill = '3' THEN '17'
			END CoverageCode,
			CASE 
				-- WHEN cg.ConceptToBill in (1,3,4,17) then 'Plan de beneficios en salud financiado con UPC'
				WHEN cg.ConceptToBill = 18 THEN 'Presupuesto máximo'
				WHEN cg.ConceptToBill = 20 THEN 'Prima EPS / EOC, no asegurados SOAT'
				WHEN cg.ConceptToBill = 13 THEN 'Cobertura Póliza SOAT'
				WHEN cg.ConceptToBill = 11 THEN 'Cobertura ARL'
				WHEN cg.ConceptToBill in (14,15) THEN 'Cobertura ADRES'
				WHEN cg.ConceptToBill = 8 THEN 'Cobertura Salud Pública'
				WHEN cg.ConceptToBill in (10,12,16) THEN 'Cobertura entidad territorial, recursos de oferta'
				WHEN cg.ConceptToBill = 19 THEN 'Urgencias población migrante'
				WHEN cg.ConceptToBill = 2 THEN 'Plan complementario en Salud'
				WHEN cg.ConceptToBill = 5 THEN 'Plan medicina prepagada'
				WHEN cg.ConceptToBill in (6,17) THEN 'Otras Pólizas en salud'
				WHEN cg.ConceptToBill = 9 THEN 'Cobertura Régimen Especial o Excepción'
				WHEN cg.ConceptToBill = 21 THEN 'Cobertura Fondo Nacional de Salud de las Personas Privadas de la Libertad'
				WHEN cg.ConceptToBill in (7,4) THEN 'Particular'
				WHEN cg.ConceptToBill = '1' THEN 'Plan de beneficios en salud financiado con UPC Regimen Contributivo'
				WHEN cg.ConceptToBill = '3' THEN 'Plan de beneficios en salud financiado con UPC Regimen Subsidiado'
			END CoverageDescription,
			(
				SELECT STUFF((
					SELECT DISTINCT ',' + a.AuthorizationNumber
					FROM
					(
						SELECT RTRIM(LTRIM(admission.IAUTORIZA)) AuthorizationNumber
						FROM dbo.ADINGRESO admission WITH (NOLOCK)
						WHERE i.AdmissionNumber = admission.NUMINGRES
					UNION ALL
						SELECT RTRIM(LTRIM(sod.AuthorizationNumber)) AuthorizationNumber
						FROM Billing.InvoiceDetail id WITH (NOLOCK)
						JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON id.ServiceOrderDetailId = sod.Id 
						WHERE I.Id = ID.InvoiceId
					) a
				FOR XML PATH ('')), 1, 2, '')
			) AuthorizationNumber,
			/*----------------------- Identificación MIPRES -----------------------*/
			(
				SELECT STUFF((
					SELECT DISTINCT ',' + Mipres.Code
					FROM
					(
						SELECT pres.Code
						FROM Billing.MipresCode pres WITH (NOLOCK)
						JOIN  Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sod.Id = pres.ServiceOrderDetailId
						JOIN Billing.InvoiceDetail id WITH (NOLOCK) ON sod.Id = id.ServiceOrderDetailId
						JOIN Billing.Invoice i WITH (NOLOCK) ON i.Id = id.InvoiceId
						WHERE i.Id = @InvoiceId
					) Mipres
				FOR XML PATH ('')), 1, 2, '')
			) MIPRESNumber,
			(
				SELECT STUFF((
					SELECT DISTINCT ',' + Mipres.IdMipres
					FROM
					(
						SELECT IdMipres
						FROM Billing.MipresCode pres WITH (NOLOCK)
						JOIN  Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sod.Id = pres.ServiceOrderDetailId
						JOIN Billing.InvoiceDetail id WITH (NOLOCK) ON sod.Id = id.ServiceOrderDetailId
						JOIN Billing.Invoice i WITH (NOLOCK) ON i.Id = id.InvoiceId
						WHERE i.Id = @InvoiceId
					) Mipres
				FOR XML PATH ('')), 1, 2, '')
			) MIPRESId,
			/*-------------------------------------------------------------------*/
			ISNULL(c.ContractNumber, '') ContractNumber,
			CAST('01' AS VARCHAR(2)) NoContractReason, -- Forzado urgencias mientras validamos aplicabilidad
			CASE 
				WHEN cg.CareGroupType = 4 and et.Type = 3 THEN adf.NUMSOA
				WHEN et.Code IN ('13') THEN ad.IAUTORIZA
				else NULL
			END	PolicyNumber, --SOAT y Planes voluntarios,
			cd.PermanentObservationOfTheInvoice,
			ISNULL(cd.BillingInitialDate, i.InvoiceDate) BillingInitialDate,
			ISNULL(cd.BillingEndDate, i.InvoiceDate) BillingEndDate,
			ISNULL(id.CopayValue, 0) CopayValue,
			ISNULL(id.FeeModeratorValue, 0) FeeModeratorValue,
			ISNULL(id.FeeRecoveryValue, 0) FeeRecoveryValue,
			ISNULL(id.DistributedValue, 0) DistributedValue,
			ISNULL((select SUM(K.Value) FROM (SELECT pa.Value
				FROM Billing.InvoicePortfolioAdvance ipa WITH(NOLOCK)
				JOIN Portfolio.PortfolioAdvance pa WITH(NOLOCK) ON pa.Id = ipa.PortfolioAdvanceId
				WHERE ipa.InvoiceId = i.Id AND pa.ThirdPartyBeneficiaryId IS NOT NULL
				
				UNION ALL
				
				SELECT ipa.Value				
				FROM Billing.InvoicePortfolioAdvance ipa
				JOIN Portfolio.PortfolioAdvance pa ON pa.Id = ipa.PortfolioAdvanceId
				JOIN Treasury.CashReceiptDetails crd ON crd.Id = pa.CashReceiptDetailId
				JOIN Treasury.CashReceiptConcepts crc ON crc.Id = crd.IdCashReceiptConcept
				WHERE ipa.InvoiceId = i.Id AND crc.IsFixedAmountInvoiceAdvance = 1 AND i.DocumentType = 4 ) K),0) PortfolioAdvanceEntityValue,
			(CASE
				WHEN  fnc.ReportType =1 THEN 'SS-Reporte'
				WHEN fnc.ReportType =2 THEN 'SS-SinAporte'
				ELSE 'SS-CUFE' END) OperationMode,
			/*---------------------------------------------------------*/
			CASE
				WHEN EXISTS(SELECT 1 FROM Billing.InvoiceEntityCapitated iec_ini WHERE iec_ini.InvoiceId = i.Id AND iec_ini.InvoicePeriod = 1) THEN i.CapitationInitialDate
				WHEN id.FirstServiceDate > @IFECHAING THEN @IFECHAING
				ELSE id.FirstServiceDate
			END AS FirstServiceDate
	FROM Common.OperatingUnit ou WITH (NOLOCK)
	JOIN Billing.Invoice i WITH (NOLOCK) ON ou.Id = i.OperatingUnitId
	LEFT JOIN ADINGRESO ad WITH(NOLOCK) on i.AdmissionNumber = ad.NUMINGRES
	LEFT JOIN Contract.CareGroup cg WITH (NOLOCK) ON i.CareGroupId = cg.Id
	LEFT JOIN Contract.CompanyType et WITH(NOLOCK) ON et.Id = cg.EntityType
	LEFT JOIN Contract.Contract c WITH (NOLOCK) ON i.ContractId = c.Id
	LEFT JOIN Contract.ContractDetail cd WITH (NOLOCK) ON c.Id = cd.ContractId AND cd.ValidRecord = 1
	-------------------------------------------------------------------------------------------------------------------
	LEFT JOIN dbo.INPACIENT pacient WITH (NOLOCK) ON i.PatientCode = pacient.IPCODPACI
	-------------------------------------------------------------------------------------------------------------------
	LEFT JOIN @DetailsGroup id ON i.Id = id.InvoiceId
	LEFT JOIN Billing.FeeNotCollected fnc WITH (NOLOCK) on fnc.RevenueControlDetailId=i.RevenueControlDetailId
	-------------------------------------------------------------------------------------------------------------------
	OUTER APPLY (
    SELECT TOP (1) a.*
    FROM ADFURIPSU a WITH (NOLOCK)
    WHERE RTRIM(a.NUMINGRES) = RTRIM(i.AdmissionNumber)
	     OR RTRIM(a.NUMINGRES) = RTRIM(ad.IINGRESOA)
    ORDER BY CASE WHEN RTRIM(a.NUMINGRES) = RTRIM(i.AdmissionNumber) THEN 0 ELSE 1 END
	) adf
	WHERE i.Id = @InvoiceId AND i.DocumentType IN (1, 2, 3, 4, 5)
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene información adicional y complementaria de una factura de venta a partir de su identificador, consolidando datos del encabezado de la factura (fecha, tipo de documento, tipo de paciente, contrato, unidad operativa), el detalle de servicios facturados (copagos, cuotas moderadoras, cuotas de recuperación, valores distribuidos y fecha de primer servicio) y datos demográficos del paciente desde el ingreso. Maneja dos flujos diferenciados: facturas de capitación (tipo 4), donde agrupa los detalles de facturas de venta asociadas al período de capitación cruzando con InvoiceEntityCapitated, y facturas ordinarias (otros tipos), donde toma el detalle directo de la factura. El resultado final reúne información del IPS, tipo y número de documento del paciente, fecha de ingreso, valores desglosados por tipo de cuota y datos necesarios para la presentación, impresión y reporte de la factura en el proceso de facturación y cobro a pagadores.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetInvoiceMoreInformationByInvoiceId';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetInvoiceMoreInformationByInvoiceId';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida información complementaria de una factura (identificación del paciente, tipo de usuario y cobertura según normativa colombiana, autorizaciones, MIPRES, valores de copago/cuota moderadora/recuperación/distribuidos, anticipos y fecha del primer servicio) para reportes regulatorios de facturación electrónica en salud.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInvoiceMoreInformationByInvoiceId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La factura debe existir en Billing.Invoice con el Id recibido y tener DocumentType entre 1 y 5 para producir resultado.; Para facturas DocumentType=4 (RIPS de capitación) deben existir registros relacionados en Billing.InvoiceEntityCapitated con InvoicePeriod NULL o 2 y facturas asociadas DocumentType=5 con Status=1.; Los códigos de IPTIPODOC, ConceptToBill, EntityType, LiquidationType, PatientType y PatientAffiliatedType deben estar dentro de los valores mapeados; otros valores producen NULL en los campos derivados.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInvoiceMoreInformationByInvoiceId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los valores CopayValue, FeeModeratorValue, FeeRecoveryValue, DistributedValue y PortfolioAdvanceEntityValue se devuelven siempre como 0 cuando no hay registros (vía ISNULL).; BillingInitialDate y BillingEndDate caen en la fecha de la factura cuando ContractDetail no provee fechas específicas (ContractDetail.ValidRecord=1 requerido).; El cálculo DistributedValue solo aplica cuando DistributionType>1; con DistributionType ≤ 1 aporta 0.; Los códigos PatientTypeCode y CoverageCode siguen la codificación oficial colombiana de facturación electrónica en salud (Resolución 1383/2025 y anteriores).; Para DocumentType=4, sólo se consideran capitaciones cuyo InvoicePeriod sea NULL o 2 (mes vencido).; Sólo se procesan facturas con DocumentType IN (1,2,3,4,5); otros tipos no producen filas.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInvoiceMoreInformationByInvoiceId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve una fila por factura con datos demográficos del paciente, tipo de usuario, modalidad de pago (LiquidationType), cobertura (ConceptToBill), autorizaciones concatenadas, MIPRES, número de póliza, observaciones contractuales, valores agregados y fecha del primer servicio.; [INSERT] @DetailsTable: Si DocumentType=4: inserta detalles de facturas DocumentType=5 con Status=1 cuyo CareGroupId+InvoiceCategoryId coincidan con InvoiceEntityCapitated y InvoiceDate caiga entre InitialDate y EndDate del período de capitación (usando PreviousRIPSInvoice si existe). En caso contrario, inserta los InvoiceDetail directos de la factura.; [INSERT] @DetailsGroup: Agrupa por InvoiceId sumando: SubTotalPatientSalesPrice cuando RecoveryFeeType=3 (CopayValue), =2 (FeeModeratorValue), =5 (FeeRecoveryValue); y (GrandTotal-ThirdParty-SubTotalPatient) cuando DistributionType>1 (DistributedValue); MIN(ServiceDate) como FirstServiceDate.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInvoiceMoreInformationByInvoiceId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe registro en @Invoice con DocumentType=4 (factura de capitación / RIPS) → Resuelve detalles vía CTE de InvoiceEntityCapitated cruzando facturas DocumentType=5 Status=1 dentro del período, y obtiene IFECHAING desde ADINGRESO usando los AdmissionNumber agrupados. else Obtiene IFECHAING por el AdmissionNumber directo de la factura, inserta los InvoiceDetail propios y evalúa @IsAllPackage=1 si TODOS los detalles tienen IsPackage=1.; si @IsAllPackage = 1 → LiquidationTypeCode=''01'' y descripción ''Pago individual por caso / Conjunto integral de atenciones / Paquete / Canasta''. else Mapea LiquidationType de CareGroup: 1→''04'' Pago por evento, 2→''03'' Pago por capitación, 5→''02'' Pago global prospectivo.; si DocumentType=3 → PatientTypeCode=''08'' y descripción ''Particular'' (no se evalúan EntityType ni PatientType). else Aplica jerarquía: EntityType=9→Especial/Excepción (cotizante/beneficiario según PatientAffiliatedType); EntityType=5→ARL ''09''; EntityType=13 y ConceptToBill=13→SOAT ''10''; en caso contrario clasifica por PatientType (Contributivo/Subsidiado/Particular/Sin régimen).; si FirstServiceDate del agregado > IFECHAING de ADINGRESO → Devuelve IFECHAING como FirstServiceDate (la fecha real de ingreso prevalece sobre la del primer servicio facturado). else Devuelve la FirstServiceDate calculada de los detalles.; si CareGroup.CareGroupType=4 y CompanyType.Type=3 → PolicyNumber = ADFURIPSU.NUMSOA (póliza SOAT desde FURIPS). else Si CompanyType.Code=''13'' usa ADINGRESO.IAUTORIZA; en otro caso PolicyNumber NULL.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInvoiceMoreInformationByInvoiceId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceEntityCapitated; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Billing.MipresCode; Billing.InvoicePortfolioAdvance; Billing.FeeNotCollected; Portfolio.PortfolioAdvance; Treasury.CashReceiptDetails; Treasury.CashReceiptConcepts; Common.OperatingUnit; Contract.CareGroup; Contract.CompanyType; Contract.Contract; Contract.ContractDetail; dbo.ADINGRESO; dbo.ADFURIPSU; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInvoiceMoreInformationByInvoiceId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInvoiceMoreInformationByInvoiceId';
-- GO
