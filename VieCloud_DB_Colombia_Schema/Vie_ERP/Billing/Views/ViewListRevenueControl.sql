CREATE VIEW [Billing].[ViewListRevenueControl]
AS

WITH SERVICE_ORDER_CTE AS (
	SELECT SODD.Id AS ServiceOrderDetailDistributionId
		, SODD.RevenueControlDetailId
		, SODD.Quantity
		, sodd.ServiceOrderDetailId
		, SOD.ServiceOrderId
		, SO.Code as ServiceOrderCode
		, SOD.PerformsFunctionalUnitId
		, FU.Code as FunctionalUnitCode
		, FU.Name as FunctionalUnitName
		, cc.Code as CostCenterCode
		, cc.Name as CostCenterName
		, sod.AuthorizationNumber
		, sod.SurgeryNumber
		, sod.PerformsHealthProfessionalCode
		, sod.PerformsProfessionalSpecialty
		, CONCAT(ISNULL(BG1.Code, BG2.Code), ' - ', ISNULL(BG1.Name, BG2.Name)) AS ServiceBillingGroupCodeName
		, BG1.Code AS ServiceBillingGroupCode
		, BG1.Name AS ServiceBillingGroupName
		, SOD.ProductId
		, case when sod.ProductId is null then '' else ip.Code end as ProductCode
		, case when sod.ProductId is null then '' else ip.Name end as ProductName
		, case when sod.IPSServiceId is not null then ipss.Code else isnull(ip.Code, '') end as ServiceCode
		, case when sod.IPSServiceId is not null then ipss.Name else isnull(ip.Name, '') end as ServiceName
		, case when sod.IPSServiceId is not null then ipss.Code else '' end as IpsServiceCode
		, case when sod.IPSServiceId is not null then ipss.Name else '' end as IpsServiceName
		, case when sod.IPSServiceId is not null then ipss.POS else null end as POSService
		, bi.Code as BillingConceptCode
		, bi.Name as BillingConceptName
		, case when sod.CUPSEntityId is not null then CUPSE.Code else null end as CupsCode
		, case when sod.CUPSEntityId is not null then CUPSE.Description else null end as CupsDescription
		, case when ccd.ContractDescriptionId is not null then cd.Code else null end as ContractDescriptionCode
		, case when ccd.ContractDescriptionId is not null then cd.Name else null end as ContractDescriptionName
		, bg2.Code as ProductBillingGroupCode
		, bg2.Name as ProductBillingGroupName
		, ISNULL(BG3.Code, BG2.Code) as ProductBillingGroupNoPosCode
		, ISNULL(BG3.Name, BG2.Name) as ProductBillingGroupNoPosName
		, ip.POSProduct
		, CONVERT(BIT, IIF(ISNULL(IP.AllPOSPathologies, 0) = 1 OR ISNULL(AT.AllPOSPathologies, 0) = 1, 1, 0)) AS AllPOSPathologies
		, AT.Id AS ATCId
		, AT.Code as AtcCode
		, sod.ServiceDate
		, sod.RecordType
		, sod.Presentation
		, sod.SettlementType
		, sodd.DistributionType
		, sod.SupplyQuantity
		, sod.DevolutionQuantity
		, sodd.ThirdPartyPercentage
		, sodd.ApplyRecoveryFee
		, sodd.RecoveryFeeType
		, sodd.PatientPercentage
		, sod.IsPackage
		, sod.CodeAssociateService
		, sod.ApplyRIAS
		, sod.RIASCupsId
		, sod.ProductLiquidationType
		, iif(GLI.Id is not null , CONCAT(GLI.Percentage, '%'), null) AS IvaPercentage
		, sodd.GrandTotalTaxes
		, sod.TaxValue
		, sod.GrossValue
		, case when sod.ProductId is not null then pt.Class else null end as ProductTypeClass
		, case when sod.DefinitionRateDetailId is not null then drd.AllowValueChange else null end as AllowValueChange
		, sodd.SubTotalPatientSalesPrice
		, sodd.ThirdPartySalesPrice
		, sod.TotalSalesPrice
		, sod.ThirdPartyDiscountPercentage
		, sodd.GrandTotalDiscount
		, sod.ThirdPartyDiscount
		, sodd.SubTotalSalesPrice
		, sodd.GrandTotalSalesPrice
		, sod.RateManualSalePrice
		, sod.CostValue
	FROM Billing.ServiceOrderDetailDistribution SODD
	JOIN Billing.ServiceOrderDetail SOD ON SODD.ServiceOrderDetailId = SOD.Id AND SOD.InvoicedQuantity > 0 AND SOD.IsDelete = 0
	JOIN Billing.ServiceOrder SO ON SOD.ServiceOrderId = SO.Id 
	JOIN Payroll.FunctionalUnit FU ON SOD.PerformsFunctionalUnitId = FU.Id 
	JOIN Payroll.CostCenter CC ON SOD.CostCenterId = CC.Id 
	LEFT JOIN Billing.BillingConcept BI ON SOD.BillingConceptId = BI.Id 
	LEFT JOIN Contract.DefinitionRateDetail DRD ON SOD.DefinitionRateDetailId = DRD.Id
	LEFT JOIN Contract.IPSService IPSS ON SOD.IPSServiceId = IPSS.Id 
	LEFT JOIN GeneralLedger.GeneralLedgerIVA GLI ON GLI.Id = sod.IVAId
	LEFT JOIN Contract.CUPSEntity CUPSE ON SOD.CUPSEntityId = CUPSE.Id 
	LEFT JOIN Contract.CUPSEntityContractDescriptions ccd ON SOD.CUPSEntityContractDescriptionId = ccd.Id
	LEFT JOIN Contract.ContractDescriptions cd ON ccd.ContractDescriptionId = cd.Id
	LEFT JOIN Billing.BillingGroup BG1  ON IIF(ISNULL(SOD.ApplyRIAS, 0) = 1, ISNULL(CUPSE.RIASBillingGroupId, CUPSE.BillingGroupId), IIF(ISNULL(ccd.Id, 0) > 0, ccd.BillingGroupId, CUPSE.BillingGroupId)) = BG1.Id 
	LEFT JOIN Inventory.InventoryProduct IP ON SOD.ProductId = IP.Id 
	LEFT JOIN Billing.BillingGroup BG2 ON IP.BillingGroupId = BG2.Id 
	LEFT JOIN [Inventory].[ProductType] pt on ip.ProductTypeId = pt.Id
	LEFT JOIN Inventory.ATC AT ON IP.ATCId = AT.Id 
	LEFT JOIN Billing.BillingGroup BG3 ON COALESCE(IP.BillingGroupNoPosId, AT.BillingGroupNoPosId, IP.BillingGroupId) = BG3.Id
), CTE_ApplyLogicThirdPartyBeneficiary AS (
		SELECT rcd.Id RevenueControlDetailId 
		FROM ADINGRESO a
		JOIN Contract.CareGroup cg ON cg.Id = a.GENCAREGROUP
		JOIN Billing.RevenueControl	 rc ON rc.AdmissionNumber = a.NUMINGRES
		JOIN Billing.RevenueControlDetail rcd ON rcd.RevenueControlId = rc.Id
		WHERE cg.LiquidationType NOT IN (2,5) AND cg.CareGroupType <> 3 AND rcd.IsMasterAccount IN (2,0))
SELECT
		CONCAT(I.Id, ' - ', ISNULL(SOCTE.ServiceOrderDetailDistributionId, - 1), ' - ', RCD.Id) AS IdKey, 		
		THP.Id AS ThirdPartyPatientId, 
		RC.PatientCode, 
		RC.AdmissionNumber, 
		CONCAT(LTRIM(RTRIM(RC.AdmissionNumber)), ' - Paciente: ', LTRIM(RTRIM(RC.PatientCode)), ' - ', THP.Name) AS AdmissionNumberPatient, 
		RCD.Id AS RevenueControlDetailId, 
		RCD.FolioOrder, 
		RCD.InvoiceCategoryId, 
		CASE WHEN RCD.InvoiceCategoryId IS NULL THEN ' - ' ELSE CONCAT(IC.Code, ' - ', IC.Name) END AS InvoiceCategoryCodeName,
		ISNULL(RCD.BillingAuthorizationId, - 1) AS BillingAuthorizationId, 
		TP.Id AS ThirdPartyId, 
		CONCAT(TP.Nit, ' - ', TP.Name) AS ThirdPartyNitName, 
		CG.Id AS CareGroupId, 
		CG.EntityType AS CaregroupEntityType, 
		CONCAT(CG.Code, ' - ', CG.Name) AS CareGroupCodeName, 
		CASE WHEN CG.ContractId IS NULL THEN ' - ' ELSE CONCAT(C.Code, ' - ', C.ContractName) END AS ContractCodeName,
		ISNULL(RCD.HealthAdministratorId, 0) AS HealthAdministratorId,
		CASE WHEN RCD.HealthAdministratorId IS NULL THEN ' - ' ELSE CONCAT(HA.Code, ' - ', HA.Name) END AS HealthAdministratorCodeName,
		CASE WHEN RCD.HealthAdministratorId IS NULL THEN NULL ELSE HA.ThirdPartyId END AS ThirdPartyHealthAdministrator,
		ISNULL(RCD.ContractEntityId, 0) AS ContractEntityId, 
		CASE WHEN RCD.ContractEntityId IS NULL THEN ' - ' ELSE CONCAT(CE.Code, ' - ', CE.Name) END AS ContractEntityCodeName,
		ISNULL(I.Id, - 1) AS InvoiceId, 
		ISNULL(I.InvoiceNumber, '') AS InvoiceNumber, 
		ISNULL(I.InvoiceDate, '') AS InvoiceDate, 
		ISNULL(I.InvoicedUser, '') AS InvoicedUser, 
		CG.CostCenterId CareGroupCostCenterId, 
		CG.LiquidationType,
		RCD.FolioType,
		RCD.Observation, 
		ROUND(Common.[CurrencyConverterByModule](rcd.TotalFolio,  cs.OfficialCurrencyId, ISNULL(i.CurrencyId,cs.OfficialCurrencyId),i.OperatingUnitId,'Invoice', i.InvoiceDate),2) AS TotalFolio, 
		RCD.ResponsibleRecoveryFee, 
		RCD.PatientDiscountPercentage, 
		RCD.PatientDiscount, 
		ISNULL(RCD.TotalPatientSalesPrice, 0) AS TotalPatientSalesPrice, 
		ISNULL(RCD.TotalPatientWithDiscount, 0) AS TotalPatientWithDiscount, 
		ISNULL(RCD.ValueVoucher, 0) AS VoucherValue, 
		RCD.Status,
		RCD.StatusFolioId,
		CONCAT(CSS.Code,'-',CSS.Name) AS StatusFolioName,
		RCD.IsMasterAccount As IsMasterAccount,
		SOCTE.ServiceOrderId AS ServiceOrderId, 
		SOCTE.ServiceOrderCode AS ServiceOrderCode, 
		ISNULL(SOCTE.ServiceOrderDetailDistributionId, - 1) AS Id, 
		SOCTE.ServiceOrderDetailId AS ServiceOrderDetailId, 
		CONCAT(SOCTE.FunctionalUnitCode, ' - ', SOCTE.FunctionalUnitName) AS PerformsFunctionalUnitCodeName, 		
		CONCAT(SOCTE.CostCenterCode, ' - ', SOCTE.CostCenterName) AS CostCenterCodeName, 
		ISNULL(SOCTE.AuthorizationNumber, '0') AS AuthorizationNumber, 
		SOCTE.SurgeryNumber,
		ISNULL(SOCTE.PerformsHealthProfessionalCode, '') AS PerformsHealthProfessionalCode, 
		ISNULL(SOCTE.PerformsProfessionalSpecialty, - 1) AS PerformsProfessionalSpecialty, 
		CONCAT(ISNULL(SOCTE.ServiceBillingGroupCode, IIF(ISNULL(POSP.HasPathologies, 0) = 1, SOCTE.ProductBillingGroupNoPosCode, SOCTE.ProductBillingGroupCode)), ' - ', ISNULL(SOCTE.ServiceBillingGroupName, IIF(ISNULL(POSP.HasPathologies, 0) = 1, SOCTE.ProductBillingGroupNoPosName, SOCTE.ProductBillingGroupName))) AS ServiceBillingGroupCodeName,
		SOCTE.ServiceCode AS ServiceCode, 
		SOCTE.ServiceName AS ServiceName, 
		CONCAT(SOCTE.BillingConceptCode, ' - ', SOCTE.BillingConceptName) AS IPSServiceGroupCodeName, 		
		SOCTE.IpsServiceCode AS IPSServiceCode, 
		SOCTE.IpsServiceName AS IPSServiceName, 
		SOCTE.POSService AS IsPOSService,
		CONCAT(SOCTE.CupsCode, ' - ', SOCTE.CupsDescription) AS CUPS, 
		CONCAT(SOCTE.ContractDescriptionCode, ' - ', SOCTE.ContractDescriptionName) AS ContractDescriptionCodeName, 
		CONCAT(IIF(ISNULL(POSP.HasPathologies, 0) = 1, SOCTE.ProductBillingGroupNoPosCode, SOCTE.ProductBillingGroupCode), ' - ', IIF(ISNULL(POSP.HasPathologies, 0) = 1, SOCTE.ProductBillingGroupNoPosName, SOCTE.ProductBillingGroupName)) AS ProductBillingGroupCodeName,
		ISNULL(SOCTE.ProductId, 0) AS ProductId,
		SOCTE.ProductCode AS ProductCode, 		
		SOCTE.ProductName AS ProductName, 
		SOCTE.POSProduct AS IsPOSProduct, 
		SOCTE.AtcCode AS ProductATCCode, 
		CAST(ISNULL(POSP.HasPathologies,0) AS BIT) AS HasPathologies,
		ISNULL(SOCTE.ServiceDate, Common.[GETDATE]()) AS ServiceDate, 
		ISNULL(SOCTE.RecordType, 1) AS RecordType, 
		SOCTE.Presentation, 
		SOCTE.SettlementType AS SettlementType, 
		ISNULL(SOCTE.DistributionType, 1) AS DistributionType, 
		Case ISNULL(SOCTE.DistributionType, 1) 
			When 1 Then 'Ninguno' 
			When 2 Then 'Distribución Normal' 
			When 3 Then 'Distribución por Corte de Cuentas' 
			When 4 Then 'Distribución por Unidad' 
			When 5 Then 'Ninguno NoPOS' 
			Else '' 
		End AS DistributionTypeName, 
		ISNULL(SOCTE.Quantity, 0) AS InvoicedQuantity, 
		ISNULL(SOCTE.SupplyQuantity, 0) AS SupplyQuantity, 
		ISNULL(SOCTE.DevolutionQuantity, 0) AS DevolutionQuantity, 
		ROUND(Common.[CurrencyConverterByModule](SOCTE.CostValue, cs.OfficialCurrencyId, ISNULL(i.CurrencyId,cs.OfficialCurrencyId),i.OperatingUnitId,'Invoice',i.InvoiceDate),2) AS CostValue,
		ROUND(Common.[CurrencyConverterByModule](SOCTE.RateManualSalePrice, cs.OfficialCurrencyId, ISNULL(i.CurrencyId,cs.OfficialCurrencyId),i.OperatingUnitId,'Invoice', i.InvoiceDate),2) AS RateManualSalePrice,
		ROUND(Common.[CurrencyConverterByModule](SOCTE.GrandTotalSalesPrice, cs.OfficialCurrencyId, ISNULL(i.CurrencyId,cs.OfficialCurrencyId),i.OperatingUnitId,'Invoice', i.InvoiceDate),2) AS GrandTotalSalesPrice,
		ROUND(Common.[CurrencyConverterByModule](SOCTE.SubTotalSalesPrice, cs.OfficialCurrencyId, ISNULL(i.CurrencyId,cs.OfficialCurrencyId),i.OperatingUnitId,'Invoice', i.InvoiceDate),2) AS SubTotalSalesPrice, 
		ROUND(Common.[CurrencyConverterByModule](SOCTE.ThirdPartyDiscount, cs.OfficialCurrencyId, ISNULL(i.CurrencyId,cs.OfficialCurrencyId),i.OperatingUnitId,'Invoice', i.InvoiceDate),2) AS ThirdPartyDiscount, 
		ROUND(Common.[CurrencyConverterByModule](SOCTE.GrandTotalDiscount, cs.OfficialCurrencyId, ISNULL(i.CurrencyId,cs.OfficialCurrencyId),i.OperatingUnitId,'Invoice', i.InvoiceDate),2) AS GrandTotalDiscount,
		ISNULL(SOCTE.ThirdPartyDiscountPercentage, 0) AS ThirdPartyDiscountPercentage, 
		ROUND(Common.[CurrencyConverterByModule](SOCTE.TotalSalesPrice, cs.OfficialCurrencyId, ISNULL(i.CurrencyId,cs.OfficialCurrencyId), i.OperatingUnitId,'Invoice',i.InvoiceDate),2) AS TotalSalesPrice,
		ROUND(Common.[CurrencyConverterByModule](SOCTE.ThirdPartySalesPrice, cs.OfficialCurrencyId, ISNULL(i.CurrencyId,cs.OfficialCurrencyId), i.OperatingUnitId,'Invoice',i.InvoiceDate),2) AS ThirdPartySalesPrice,
		ISNULL(SOCTE.ThirdPartyPercentage, 0) AS ThirdPartyPercentage, 
		IIF(SOCTE.SettlementType = 3, 'Si', 'No') AS SurchargeApply, 
		ISNULL(SOCTE.RecoveryFeeType, 0) AS RecoveryFeeType, 
		ISNULL(SOCTE.ApplyRecoveryFee, 1) AS ApplyRecoveryFee,  
		ROUND(Common.[CurrencyConverterByModule](SOCTE.SubTotalPatientSalesPrice, cs.OfficialCurrencyId, ISNULL(i.CurrencyId,cs.OfficialCurrencyId),i.OperatingUnitId,'Invoice', i.InvoiceDate),2) AS SubTotalPatientSalesPrice,
		ISNULL(SOCTE.PatientPercentage, 0) / 100 AS PatientPercentage, 		
		ISNULL(SOCTE.IsPackage, 0) AS IsPackage, 
		SOCTE.CodeAssociateService AS CodeAssociateService,
		ISNULL(SOCTE.CodeAssociateService, '') AS GuidHomologation, 
		ISNULL(SOCTE.ApplyRIAS, 0) ApplyRIAS, 
		ISNULL(SOCTE.RIASCupsId, 0) RIASCupsId,
		ISNULL(SOCTE.AllowValueChange, 0) AS AllowValueChange,
		SUBSTRING((SELECT ',' + code  AS [text()] FROM Billing.MipresCode mc WHERE mc.ServiceOrderDetailId = SOCTE.ServiceOrderDetailId FOR XML PATH ('')), 2, 1000) AS Mipres,
		SUBSTRING((SELECT ',' + IdMipres  AS [text()] FROM Billing.MipresCode mc WHERE mc.ServiceOrderDetailId = SOCTE.ServiceOrderDetailId FOR XML PATH ('')), 2, 1000) AS IdMipres,
		Case When SOCTE.ProductTypeClass = 5 Then 1 Else 0 End As IsItemProduction,
		Case When SOCTE.ProductTypeClass = 5 then 99 else ISNULL(SOCTE.DistributionType, 1) end as IconType,
		ISNULL((SELECT top 1 1 from Billing.ProductServiceDetail psd WHERE psd.ServiceOrderDetailId = SOCTE.ServiceOrderDetailId),0) FlagProductServiceDetail,
		SOCTE.ProductLiquidationType,
		CASE WHEN RCD.PatientQuotaResponsibleThirdPartyId IS NULL THEN ' - ' ELSE CONCAT(THPR.Nit, ' - ', THPR.Name) END AS ThirdPartyResponsibleQuotaNitName,
		RCD.PatientQuotaResponsibleThirdPartyId AS PatientQuotaResponsibleThirdPartyId,
		cg.MaximumTypeTop,isnull(cu.Abbreviation,'') CurrencyAbbreviation,
		ISNULL(cgm.[Value],0) AS MaximumTopValue,
		ISNULL(ai.ISOATVALO,0) AS InvoiceExternalValue,
		ISNULL(
			(
			SELECT SUM(A.TotalInvoice)
            FROM [Billing].[Invoice] A INNER JOIN dbo.ADINGRESO B On A.AdmissionNumber = B.NUMINGRES
            WHERE A.PatientCode = RC.PatientCode AND B.ITIPORIES = '2' AND CONVERT(date, B.FECACTRAN) = ai.FECACTRAN
			), 0) as InvoiceCurrentValue,
		SOCTE.IvaPercentage,
		ROUND(Common.[CurrencyConverterByModule](SOCTE.GrandTotalTaxes, cs.OfficialCurrencyId, ISNULL(i.CurrencyId,cs.OfficialCurrencyId), i.OperatingUnitId,'Invoice',i.InvoiceDate),2) AS IvaTotalValue,
		ROUND(Common.[CurrencyConverterByModule](SOCTE.GrossValue, cs.OfficialCurrencyId, ISNULL(i.CurrencyId,cs.OfficialCurrencyId),i.OperatingUnitId,'Invoice', i.InvoiceDate),2) AS GrossValue,
		SOCTE.GrandTotalTaxes,
		fnc.Id as FeeNotCollectedId,
		icy.BasicBillingId BasicbillingCopayId,
		IIF(Tmp.RevenueControlDetailId IS NOT NULL, 1, 0) ApplyLogicThirdPartyBeneficiary
FROM Billing.RevenueControl RC 
JOIN Billing.RevenueControlDetail RCD ON RC.Id = RCD.RevenueControlId
JOIN Common.ThirdParty TP ON RCD.ThirdPartyId = TP.Id 
JOIN Contract.CareGroup CG ON RCD.CareGroupId = CG.Id 
JOIN Common.ThirdParty THP ON RC.PatientCode = THP.Nit
LEFT JOIN dbo.INPACIENT PAC ON RC.PatientCode = PAC.IPCODPACI
LEFT JOIN [Contract].[Contract] C ON CG.ContractId = C.Id
LEFT JOIN Billing.InvoiceCategories IC ON RCD.InvoiceCategoryId = IC.Id 
LEFT JOIN Common.ThirdParty THPR  ON RCD.PatientQuotaResponsibleThirdPartyId = THPR.Id
LEFT JOIN Contract.HealthAdministrator HA ON RCD.HealthAdministratorId = HA.Id 
LEFT JOIN Contract.ContractEntity CE ON RCD.ContractEntityId = CE.Id 
LEFT JOIN Billing.RevenueControlDetailInvoice rcdi ON rcd.Id = rcdi.RevenueControlDetailId AND rcdi.[Status] = 1
LEFT JOIN Billing.Invoice I ON (RCD.Id = I.RevenueControlDetailId OR rcdi.InvoiceId = I.Id) AND I.Status = 1 
LEFT JOIN billing.InvoiceCopay icy ON icy.InvoiceId = I.Id
LEFT JOIN Billing.ConceptsCausesStatusFolio CSS ON RCD.StatusFolioId = CSS.Id
LEFT JOIN SERVICE_ORDER_CTE SOCTE ON RCD.Id = SOCTE.RevenueControlDetailId AND SOCTE.Quantity > 0
LEFT JOIN dbo.ADINGRESO ai ON ai.NUMINGRES=rc.AdmissionNumber
LEFT JOIN [Contract].CareGroupMaximumTop cgm ON cgm.CareGroupId = cg.Id and YEAR(ai.FECACTRAN) = cgm.[Year]
OUTER APPLY
(
	SELECT TOP 1 POSRuleRow.HasMatch
	FROM
	(
		SELECT CASE
			WHEN DP.CODDIAGNO IS NOT NULL
				AND ISNULL(SOCTE.ServiceDate, ISNULL(ai.IFECHAING, Common.[GETDATE]())) >=
					CASE ISNULL(PP.AgeMeasure, 1)
						WHEN 3 THEN DATEADD(DAY, ISNULL(PP.MinimumAge, 0), PAC.IPFECNACI)
						WHEN 2 THEN DATEADD(MONTH, ISNULL(PP.MinimumAge, 0), PAC.IPFECNACI)
						ELSE DATEADD(YEAR, ISNULL(PP.MinimumAge, 0), PAC.IPFECNACI)
					END
				AND ISNULL(SOCTE.ServiceDate, ISNULL(ai.IFECHAING, Common.[GETDATE]())) <
					CASE ISNULL(PP.AgeMeasure, 1)
						WHEN 3 THEN DATEADD(DAY, ISNULL(PP.MaximumAge, 255) + 1, PAC.IPFECNACI)
						WHEN 2 THEN DATEADD(MONTH, ISNULL(PP.MaximumAge, 255) + 1, PAC.IPFECNACI)
						ELSE DATEADD(YEAR, ISNULL(PP.MaximumAge, 255) + 1, PAC.IPFECNACI)
					END
			THEN 1 ELSE 0 END AS HasMatch
		FROM Inventory.POSPathologies PP
		LEFT JOIN Inventory.Diagnostic D ON PP.DiagnosticId = D.Id
		LEFT JOIN dbo.INDIAGNOP DP ON DP.NUMINGRES = RC.AdmissionNumber AND DP.IPCODPACI = RC.PatientCode AND RTRIM(DP.CODDIAGNO) = RTRIM(D.Code)
		WHERE PP.ProductId = SOCTE.ProductId OR PP.MedicamentId = SOCTE.ATCId
	) POSRuleRow
	ORDER BY POSRuleRow.HasMatch DESC
) POSRule
OUTER APPLY
(
	SELECT CAST(CASE
		WHEN POSRule.HasMatch = 1 THEN 1
		WHEN POSRule.HasMatch IS NULL AND ISNULL(SOCTE.AllPOSPathologies, 0) = 1 THEN 1
		ELSE 0
	END AS BIT) AS HasPathologies
) POSP
LEFT JOIN Common.Currency cu on i.CurrencyId = cu.Id
OUTER APPLY (
	SELECT TOP 1 fnc.Id
	FROM Billing.FeeNotCollected fnc WITH(NOLOCK)
	WHERE fnc.RevenueControlDetailId = RCD.Id
	ORDER BY fnc.Id DESC
) fnc
LEFT JOIN CTE_ApplyLogicThirdPartyBeneficiary Tmp ON Tmp.RevenueControlDetailId = rcd.Id
--------------------------------------------------------------------
OUTER APPLY GeneralLedger.CompanySettings cs
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de control de ingresos y facturación que consolida, para cada distribución financiera de una orden de servicio, toda la información necesaria para el análisis y seguimiento de los ingresos generados por la prestación de servicios de salud. Integra las órdenes de servicio y su detalle (procedimientos, medicamentos, insumos y estancias), la distribución del valor entre el tercero pagador (EPS, aseguradora) y el paciente, junto con datos del contrato, grupo de atención, concepto de facturación, unidad funcional, centro de costo, código CUPS, clasificación RIPS, porcentajes de IVA, descuentos, cuota moderadora y valores de venta. Está diseñada para reportería de control de ingresos, auditoría de facturación, conciliación financiera y análisis de rentabilidad por servicio, producto, unidad funcional o tercero pagador, incluyendo lógica especial para beneficiarios de terceros y cuentas maestras.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListRevenueControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListRevenueControl';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista consolidada para el control de ingresos por folio: une cada detalle de RevenueControl con su orden de servicio, distribución, factura, terceros, contrato y topes, exponiendo valores convertidos a la moneda de la factura para tableros de facturación.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRevenueControl';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El detalle de orden de servicio debe tener InvoicedQuantity > 0 e IsDelete = 0 para incluirse en el CTE SERVICE_ORDER_CTE; La distribución del servicio (Quantity) debe ser > 0 para enlazarse al folio; Existe un registro en GeneralLedger.CompanySettings (OUTER APPLY cs) con OfficialCurrencyId definido; El paciente está registrado como tercero en Common.ThirdParty (RC.PatientCode = THP.Nit); Solo se consideran facturas e InvoiceCopay/RevenueControlDetailInvoice con Status = 1', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRevenueControl';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todos los valores monetarios (TotalFolio, CostValue, GrandTotalSalesPrice, SubTotalSalesPrice, ThirdPartyDiscount, GrandTotalDiscount, TotalSalesPrice, ThirdPartySalesPrice, SubTotalPatientSalesPrice, RateManualSalePrice, GrossValue, GrandTotalTaxes) se convierten desde la moneda oficial de la compañía a la moneda de la factura usando Common.CurrencyConverterByModule con módulo ''Invoice'' y la fecha de la factura, redondeados a 2 decimales; PatientPercentage se expone dividido entre 100 (forma decimal); Cuando ProductId es NULL, ProductCode y ProductName se devuelven como cadena vacía; Solo se relacionan facturas con Status = 1, ya sea por enlace directo (Invoice.RevenueControlDetailId) o por la tabla puente RevenueControlDetailInvoice con Status = 1; HasPathologies se calcula tomando solo la primera partición (ROW_NUMBER=1) por ProductId en Inventory.POSPathologies; Si el folio no tiene orden de servicio asociada, ServiceOrderDetailDistributionId/Id e IdKey toman -1 como sentinel', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRevenueControl';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewListRevenueControl: Devuelve un IdKey compuesto por InvoiceId + ServiceOrderDetailDistributionId (-1 si no existe) + RevenueControlDetailId, garantizando unicidad por folio/distribución/factura', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRevenueControl';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si SOD.IPSServiceId IS NOT NULL → ServiceCode/ServiceName se toman del catálogo IPSService y se exponen IpsServiceCode/Name y POSService else ServiceCode/ServiceName se toman del producto (InventoryProduct) y los campos IpsService quedan vacíos; si ISNULL(SOD.ApplyRIAS,0) = 1 → BG1 (grupo de facturación del servicio) se resuelve con ISNULL(CUPSE.RIASBillingGroupId, CUPSE.BillingGroupId) else Si existe CUPSEntityContractDescription se usa ccd.BillingGroupId, de lo contrario CUPSE.BillingGroupId; si SOCTE.ProductTypeClass = 5 → IsItemProduction = 1 y IconType = 99 (ítem de producción) else IsItemProduction = 0 y IconType = DistributionType (default 1); si SOCTE.SettlementType = 3 → SurchargeApply = ''Si'' (aplica recargo) else SurchargeApply = ''No''; si DistributionType IN (1..5) → Mapea a nombre: 1=''Ninguno'', 2=''Distribución Normal'', 3=''Distribución por Corte de Cuentas'', 4=''Distribución por Unidad'', 5=''Ninguno NoPOS''; si CTE ApplyLogicThirdPartyBeneficiary: cg.LiquidationType NOT IN (2,5) AND cg.CareGroupType <> 3 AND rcd.IsMasterAccount IN (2,0) → ApplyLogicThirdPartyBeneficiary = 1 para ese RevenueControlDetail else ApplyLogicThirdPartyBeneficiary = 0; si RCD.HealthAdministratorId IS NULL → HealthAdministratorCodeName = '' - '' y ThirdPartyHealthAdministrator = NULL else Se concatena código y nombre del administrador y se expone su ThirdPartyId; si Subconsulta InvoiceCurrentValue: B.ITIPORIES = ''2'' AND CONVERT(date,B.FECACTRAN) = ai.FECACTRAN del ingreso actual → Suma TotalInvoice de facturas del mismo paciente cuyo ingreso es de tipo ''2'' y misma fecha de actuación, como valor facturado del día', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRevenueControl';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.CurrencyConverterByModule; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRevenueControl';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRevenueControl';
GO
