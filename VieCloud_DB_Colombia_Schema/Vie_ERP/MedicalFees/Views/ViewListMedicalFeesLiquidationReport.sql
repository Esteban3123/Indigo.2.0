CREATE VIEW [MedicalFees].[ViewListMedicalFeesLiquidationReport]
AS
SELECT	mfld.Id AS Row
	   ,mfl.Id AS Id
	   ,mfl.Code AS Code
	   ,mfl.HealthProfessionalCode
	   ,CONCAT(RTRIM(t.Nit), ' - ', RTRIM(t.Name)) AS Tercero
	   ,mfl.InitialDate
	   ,mfl.EndDate
	   ,mfld.LiquidationType
	   ,pfu.code AS UnitCode
	   ,pfu.Name AS UnitName
	   ,pcc.Code AS CostCode
	   ,pcc.Name AS CostName
	   ,mfc.AdmissionNumber
	   ,CONCAT(RTRIM(mfc.PatientCode), ' - ', RTRIM(p.IPNOMCOMP)) AS PatientDescription
	   ,cis.Code AS ServiceCode
	   ,cis.Name AS ServiceName
	   ,cce.Code AS CupsCode
	   ,cce.Description AS CupsDescripcion
	   ,mfc.InvoiceQuantity AS Quantity
	   ,mfc.TotalAmountPayable AS CausaTotal
	   ,mfl.CreationUser AS UserPrint
	   ,gm.Number AS NumberAccount
	   ,gm.Name AS NameAccount
	   ,CONCAT(RTRIM(tc.Nit), ' - ', RTRIM(tc.Name)) AS TerceroCausation
	   ,CONCAT(RTRIM(mfcc.Code), ' - ', RTRIM(mfcc.ContractName)) AS ContratoAgremiacion
	   ,mfl.LiquidationType AS TipoLiquidation
FROM MedicalFees.MedicalFeesLiquidationDetail mfld WITH (NOLOCK)
JOIN MedicalFees.MedicalFeesLiquidation mfl WITH (NOLOCK) ON mfl.Id = mfld.MedicalFeesLiquidacionId
JOIN MedicalFees.MedicalFeesCausation mfc WITH (NOLOCK) ON mfc.id = mfld.MedicalFeesCausationId
JOIN MedicalFees.MedicalFeesContract mfcc WITH (NOLOCK) ON mfcc.id = mfc.MedicalFeesContractId
JOIN Common.Supplier cs WITH (NOLOCK) ON cs.Id = mfl.SupplierId
JOIN [Billing].ServiceOrderDetail sod WITH (NOLOCK) ON sod.id = mfc.ServiceOrderDetailId
JOIN [Contract].IPSService cis WITH (NOLOCK) ON cis.Id = sod.IPSServiceId
JOIN [Contract].CUPSEntity cce WITH (NOLOCK) ON cce.Id = sod.CUPSEntityId
LEFT JOIN [Common].ThirdParty tc WITH (NOLOCK) ON tc.id = mfc.ThirdPartyId
LEFT JOIN [Common].ThirdParty t WITH (NOLOCK) ON t.id = cs.IdThirdParty
LEFT JOIN [Payroll].FunctionalUnit pfu WITH (NOLOCK) ON pfu.id = sod.PerformsFunctionalUnitId
LEFT JOIN [Payroll].CostCenter pcc WITH (NOLOCK) ON pcc.Id = sod.CostCenterId
LEFT JOIN [Billing].BillingConcept bc WITH (NOLOCK) ON bc.id = cce.BillingConceptId
LEFT JOIN Billing.BillingConceptAccount bca WITH (NOLOCK) ON bc.Id = bca.BillingConceptId AND bc.AccountingType = 2 AND bca.UnitType = Billing.fnGetUnitType(pfu.UnitType)
LEFT JOIN [GeneralLedger].MainAccounts gm WITH (NOLOCK) ON gm.id = ISNULL(bca.FeesExpensesAccountId, bc.FeesExpensesAccountId)
LEFT JOIN dbo.INPACIENT p WITH (NOLOCK) ON p.IPCODPACI = mfc.PatientCode
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista para el reporte de liquidaciones de honorarios médicos. Consolida información de cada liquidación (cabecera y detalle) junto con los datos del profesional de salud o proveedor liquidado, el paciente atendido, el número de ingreso, los servicios y procedimientos CUPS facturados, la unidad funcional y centro de costo donde se realizó la prestación, y las cuentas contables de gasto asociadas. También incluye el tercero de la causación y el contrato de agremiación vinculado. Sirve como fuente principal para imprimir o exportar el reporte de liquidación de honorarios médicos, mostrando cantidades, valores causados y datos de trazabilidad del proceso de pago a profesionales de salud.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'VIEW', @level1name = N'ViewListMedicalFeesLiquidationReport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'VIEW', @level1name = N'ViewListMedicalFeesLiquidationReport';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la información detallada de liquidaciones de honorarios médicos con sus causaciones, contratos, servicios prestados, paciente, proveedor, unidad funcional, centro de costo y cuenta contable asociada para reporte de liquidación.', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListMedicalFeesLiquidationReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en MedicalFeesLiquidationDetail vinculados a una liquidación (MedicalFeesLiquidation) y a una causación (MedicalFeesCausation).; La causación está asociada a un contrato de honorarios médicos (MedicalFeesContract).; La liquidación tiene un proveedor (Supplier) registrado en Common.Supplier.; La causación referencia un detalle de orden de servicio (ServiceOrderDetail) con servicio IPS y código CUPS asociados.', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListMedicalFeesLiquidationReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La cuenta contable de honorarios (FeesExpensesAccountId) se resuelve priorizando la cuenta específica por unidad (BillingConceptAccount) y, en su ausencia, la cuenta general del concepto de facturación.; El tercero del proveedor y el tercero de causación se muestran concatenando NIT y nombre.; El paciente y el contrato se presentan concatenando código y descripción/nombre.; La vinculación con paciente, terceros, unidad funcional, centro de costo y cuenta contable es opcional (LEFT JOIN), por lo que pueden venir nulos.; Solo se incluyen registros que tengan detalle de liquidación, liquidación, causación, contrato, proveedor, orden de servicio, servicio IPS y CUPS (INNER JOIN obligatorios).', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListMedicalFeesLiquidationReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Liquidación de honorarios médicos; Causación de honorarios; Contrato de agremiación/honorarios; Profesional de la salud; Proveedor/Tercero; Paciente; Admisión; Servicio IPS; CUPS; Unidad funcional; Centro de costo; Concepto de facturación; Cuenta contable de gastos por honorarios; Orden de servicio', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListMedicalFeesLiquidationReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MedicalFees.ViewListMedicalFeesLiquidationReport: Devuelve una fila por cada detalle de liquidación cruzado con su liquidación, causación, contrato, proveedor, servicio IPS, CUPS, terceros y cuenta contable.', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListMedicalFeesLiquidationReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si bc.AccountingType = 2 y bca.UnitType = Billing.fnGetUnitType(pfu.UnitType) → Se enlaza la cuenta contable específica desde BillingConceptAccount (bca.FeesExpensesAccountId) else Se utiliza la cuenta contable por defecto del concepto de facturación (bc.FeesExpensesAccountId) vía ISNULL', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListMedicalFeesLiquidationReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.fnGetUnitType', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListMedicalFeesLiquidationReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MedicalFees.MedicalFeesLiquidationDetail; MedicalFees.MedicalFeesLiquidation; MedicalFees.MedicalFeesCausation; MedicalFees.MedicalFeesContract; Common.Supplier; Billing.ServiceOrderDetail; Contract.IPSService; Contract.CUPSEntity; Common.ThirdParty; Payroll.FunctionalUnit; Payroll.CostCenter; Billing.BillingConcept; Billing.BillingConceptAccount; GeneralLedger.MainAccounts; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListMedicalFeesLiquidationReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'VIEW', @level1name=N'ViewListMedicalFeesLiquidationReport';
GO
