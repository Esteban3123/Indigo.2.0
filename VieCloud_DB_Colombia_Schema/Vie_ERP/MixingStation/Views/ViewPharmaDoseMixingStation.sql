

CREATE VIEW [MixingStation].[ViewPharmaDoseMixingStation]
AS
	WITH Cte_DispensingDose AS(
								SELECT
									pd.Id PharmaceuticalDispensingId,
									pdd.ProductId,
									ip.Code,
									ip.Name,
									a.Code AtcCode,
									SUM(pddb.OutstandingQuantity) QuantityDelivered,
									rpds.GroupingCodeDose,
									pd.AdmissionNumber,
									bs.BatchCode,
									rpds.PackageId,
									pdd.FunctionalUnitId,
									pdd.SurchargeApply,
									pdd.OrderedHealthProfessionalCode,
									pdd.OrderedHealthProfessionalThirdPartyId,
									pdd.HealthAdministratorId,
									pdd.OrderedProfessionalSpecialty,
									pdd.WarehouseId
								FROM Inventory.PharmaceuticalDispensingDetailBatchSerial pddb 
								JOIN Inventory.PharmaceuticalDispensingDetail pdd WITH(NOLOCK) ON pdd.Id = pddb.PharmaceuticalDispensingDetailId
								JOIN Inventory.PharmaceuticalDispensing pd WITH(NOLOCK) ON pd.Id = pdd.PharmaceuticalDispensingId
								JOIN Inventory.InventoryProduct ip WITH(NOLOCK) on pdd.ProductId= ip.Id
								JOIN Inventory.ATC a WITH(NOLOCK) ON a.Id = ip.ATCId
								JOIN Inventory.ProductType pt WITH(NOLOCK) on ip.ProductTypeId=pt.Id and pt.Class = 5
								JOIN Inventory.PhysicalInventory pi WITH(NOLOCK) on pddb.PhysicalInventoryId= pi.Id
								JOIN Inventory.BatchSerial bs WITH(NOLOCK) on pi.BatchSerialId=bs.Id
								JOIN MixingStation.RequestPackageDetailStatus rpds WITH(NOLOCK) on rpds.BatchCode= bs.BatchCode
								WHERE pddb.OutstandingQuantity > 0 
								GROUP BY pddb.Id, pdd.ProductId, pdd.FunctionalUnitId, pdd.SurchargeApply, pdd.OrderedHealthProfessionalCode, pdd.OrderedHealthProfessionalThirdPartyId,
								pdd.HealthAdministratorId, pdd.OrderedProfessionalSpecialty, pdd.WarehouseId, pd.Id, ip.Code, ip.Name, rpds.GroupingCodeDose, pd.AdmissionNumber, bs.BatchCode,
								rpds.PackageId, a.Code),
	Cte_PharmaDose AS (
								SELECT	
										ph.CodeSusceptibleMixingStation,
										ph.GroupingCodeDose,
										ph.AppliedDose,
										ph.QuantityReceivable,
										ph.ProductCode,
										hcf.NUMINGRES
								FROM MedicalHistory.PharmaDose ph WITH(NOLOCK)
								JOIN HCFARMEPC hcf ON hcf.CODCONCEC = ph.IDHCFARMEPC
								GROUP BY ph.GroupingCodeDose, ph.CodeSusceptibleMixingStation ,ph.AppliedDose,ph.QuantityReceivable, 
								ph.ProductCode, hcf.NUMINGRES),
	Cte_PharmaDoseDetail AS	(
								SELECT	ph.GroupingCodeDose,
										ph.DeliveryStatus,
										ph.AppliedDose,
										ph.QuantityReceivable,
										ph.CodeSusceptibleMixingStation,
										ph.AppliedDateDose,
										ph.ProductCode,
										hcf.NUMINGRES
								FROM MedicalHistory.PharmaDose ph WITH(NOLOCK)
								JOIN HCFARMEPC hcf ON hcf.CODCONCEC = ph.IDHCFARMEPC
								GROUP BY ph.GroupingCodeDose,ph.DeliveryStatus,ph.AppliedDose,ph.QuantityReceivable,
											ph.CodeSusceptibleMixingStation,ph.AppliedDateDose, ph.ProductCode, hcf.NUMINGRES),
	Cte_WithoutDispensation AS (
								SELECT 
								rpds.ProductId,
								ip.Code,
								ip.Name,
								SUM(rmd.Quantity) QuantityDelivered,
								rpds.GroupingCodeDose,
								phd.NUMINGRES AdmissionNumber,
								bs.BatchCode,
								rpds.PackageId,
								phd.WarehouseId,
								phd.FunctionalUnitId,
								phd.HealthAdministratorId,
								phd.OrderedHealthProfessionalCode,
								phd.OrderedHealthProfessionalThirdPartyId,
								phd.OrderedProfessionalSpecialty,
								phd.TypePayment,
								rmd.RequestMixingStationId RequestMixingStationId
								FROM MixingStation.RequestPackageDetailStatus rpds
								JOIN MixingStation.RequestMixingStationDetail rmd ON rmd.Id = rpds.RequestMixingStationDetailId
								JOIN Inventory.PhysicalInventory pi ON pi.Id = rpds.PhysicalInventoryId
								JOIN Inventory.BatchSerial bs on pi.BatchSerialId=bs.Id
								JOIN MixingStation.CampaignDetail cd ON cd.Id = rmd.CampaignDetailId
								JOIN Inventory.InventoryProduct ip on rpds.ProductId = ip.Id
								JOIN (SELECT 
											phd.GroupingCodeDose,
											fpc.NUMINGRES,
											w.Id WarehouseId,
											fu.Id FunctionalUnitId,
											c.HealthAdministratorId,
											fpc.CODPROSAL OrderedHealthProfessionalCode,
											i.CODESPEC1 OrderedProfessionalSpecialty,
											t.Id OrderedHealthProfessionalThirdPartyId,
											cgm.TypePayment
										FROM MedicalHistory.PharmaDose phd WITH(NOLOCK)
										JOIN HCFARMEPC fpc WITH(NOLOCK) ON fpc.CODCONCEC = phd.IDHCFARMEPC
										JOIN ADINGRESO a WITH(NOLOCK) ON a.NUMINGRES = fpc.NUMINGRES
										JOIN Inventory.Warehouse w ON w.Code = fpc.CODBODEGA
										JOIN Payroll.FunctionalUnit fu ON fu.Code = a.UFUCODIGO
										JOIN Contract.CareGroup cg ON cg.Id = a.GENCAREGROUP
										JOIN Contract.Contract c ON c.Id = cg.ContractId
										JOIN INPROFSAL i WITH(NOLOCK) ON i.CODPROSAL = fpc.CODPROSAL
										JOIN Common.ThirdParty t WITH(NOLOCK) ON t.Nit = i.CODIGONIT
										LEFT JOIN Contract.CareGroupMixLiquidation cgm WITH(NOLOCK) ON cgm.CareGroupId = cg.Id) phd ON phd.GroupingCodeDose = rpds.GroupingCodeDose
								LEFT JOIN Cte_DispensingDose cdd ON cdd.GroupingCodeDose = rpds.GroupingCodeDose
								WHERE cd.CampaignStatus = 6 AND phd.TypePayment = 2 AND rmd.Source = 1 AND cdd.PharmaceuticalDispensingId IS NULL AND rpds.Status = 3
								GROUP BY	rpds.ProductId, ip.Code, ip.Name, rpds.GroupingCodeDose, bs.BatchCode, rpds.PackageId, phd.NUMINGRES, phd.WarehouseId, phd.FunctionalUnitId, 
											phd.HealthAdministratorId, phd.OrderedHealthProfessionalCode, phd.OrderedHealthProfessionalThirdPartyId, phd.OrderedProfessionalSpecialty, 
											phd.TypePayment,rmd.RequestMixingStationId)

		SELECT	NEWID() Id,
				psms.FullProductName,
				psms.ApplicationsNumber DoseNumber,
				ProductId,
				Code,
				CONCAT(Code,'-',Name) ProductCodName,
				SUM(QuantityDelivered)QuantityDelivered,
				SUM(cp.AppliedDose) AppliedDose,
				SUM(cp.QuantityReceivable) QuantityReceivable,
				NULL DeliveryStatusName,
				NULL DeliveryStatus,
				NULL Child,
				cdd.AdmissionNumber,
				NULL BatchCode,
				CAST(0 AS BIT) Selected,				
				null PackageId,
				cdd.FunctionalUnitId,
				null SurchargeApply,
				null OrderedHealthProfessionalCode,
				NULL OrderedHealthProfessionalThirdPartyId,
				NULL HealthAdministratorId,
				NULL OrderedProfessionalSpecialty,
				NULL WarehouseId,
				NULL AppliedDateDose,
				cdd.PharmaceuticalDispensingId Father,
				NULL PaidToEndCampaign
		FROM Cte_DispensingDose cdd
		JOIN Cte_PharmaDose cp ON cdd.GroupingCodeDose= cp.GroupingCodeDose AND cdd.AtcCode = cp.ProductCode  AND cdd.AdmissionNumber = cp.NUMINGRES
		JOIN MedicalHistory.ProductSusceptibleMixingStation psms ON psms.CodeSusceptibleMixingStation = cp.CodeSusceptibleMixingStation AND psms.MainDrugCode = cp.ProductCode
		GROUP BY psms.CodeSusceptibleMixingStation, psms.FullProductName, psms.ApplicationsNumber, cdd.ProductId, cdd.Code, cdd.Name, cdd.AdmissionNumber,
		cdd.FunctionalUnitId, cdd.PharmaceuticalDispensingId
				
		UNION ALL
	
		SELECT	cp2.GroupingCodeDose Id,
				psms.FullProductName,
				psms.ApplicationsNumber DoseNumber,
				ProductId,
				NULL,
				CONCAT(Code,'-',Name) ProductCodName,
				QuantityDelivered,
				SUM(cp2.AppliedDose) AppliedDose,
				SUM(cp2.QuantityReceivable) QuantityReceivable,
				CASE cp2.DeliveryStatus
					WHEN 0 THEN 'Sin Entregar'
					WHEN 1 THEN 'Entregado'
					WHEN 2 THEN 'Generado'
				END DeliveryStatusName,
				cp2.DeliveryStatus,
				cdd.PharmaceuticalDispensingId Child,
				cdd.AdmissionNumber,
				cdd.BatchCode,
				IIF(cp2.DeliveryStatus = 2, 1, 0) Seleccione,
				cdd.PackageId,
				cdd.FunctionalUnitId,
				cdd.SurchargeApply,
				cdd.OrderedHealthProfessionalCode,
				cdd.OrderedHealthProfessionalThirdPartyId,
				cdd.HealthAdministratorId,
				cdd.OrderedProfessionalSpecialty,
				cdd.WarehouseId,
				cp2.AppliedDateDose,
				NULL Father,
				NULL PaidToEndCampaign
		FROM Cte_DispensingDose cdd
		JOIN Cte_PharmaDoseDetail cp2 ON cdd.GroupingCodeDose= cp2.GroupingCodeDose AND cdd.AtcCode = cp2.ProductCode AND cdd.AdmissionNumber = cp2.NUMINGRES
		JOIN MedicalHistory.ProductSusceptibleMixingStation psms ON psms.CodeSusceptibleMixingStation = cp2.CodeSusceptibleMixingStation AND psms.MainDrugCode = cp2.ProductCode
		GROUP BY cp2.GroupingCodeDose, psms.FullProductName, psms.ApplicationsNumber, cdd.ProductId, cdd.Code, cdd.Name, cdd.QuantityDelivered, cp2.DeliveryStatus,
		cdd.PharmaceuticalDispensingId, cdd.AdmissionNumber, cdd.BatchCode, cdd.PackageId, cdd.FunctionalUnitId, cdd.SurchargeApply, cdd.OrderedHealthProfessionalCode,
		cdd.OrderedHealthProfessionalThirdPartyId, cdd.HealthAdministratorId, cdd.OrderedProfessionalSpecialty, cdd.WarehouseId, cp2.AppliedDateDose

		UNION ALL

		SELECT	NEWID() Id,
				psms.FullProductName,
				psms.ApplicationsNumber DoseNumber,
				ProductId,
				Code,
				Concat(Code,'-',Name) ProductCodName,
				sum(QuantityDelivered)QuantityDelivered,
				sum(cp.AppliedDose) AppliedDose,
				sum(cp.QuantityReceivable) QuantityReceivable,
				NULL DeliveryStatusName,
				NULL DeliveryStatus,
				NULL Child,
				cdd.AdmissionNumber,
				NULL BatchCode,
				cast(0 as bit) Selected,				
				NULL PackageId,
				NULL FunctionalUnitId,
				NULL SurchargeApply,
				NULL OrderedHealthProfessionalCode,
				NULL OrderedHealthProfessionalThirdPartyId,
				NULL HealthAdministratorId,
				NULL OrderedProfessionalSpecialty,
				NULL WarehouseId,
				NULL AppliedDateDose,
				cdd.RequestMixingStationId Father,
				NULL PaidToEndCampaign
		FROM Cte_WithoutDispensation cdd
		JOIN Cte_PharmaDose cp ON cdd.GroupingCodeDose= cp.GroupingCodeDose
		JOIN MedicalHistory.ProductSusceptibleMixingStation psms ON psms.CodeSusceptibleMixingStation = cp.CodeSusceptibleMixingStation
		GROUP BY	ProductId,Code,Name,cdd.AdmissionNumber,psms.FullProductName,psms.ApplicationsNumber,psms.CodeSusceptibleMixingStation,cdd.RequestMixingStationId
				
		UNION ALL
	
		SELECT		cp2.GroupingCodeDose Id,
					psms.FullProductName,
					psms.ApplicationsNumber DoseNumber,
					ProductId,
					NULL,
					Concat(Code,'-',Name) ProductCodName,
					QuantityDelivered,
					sum(cp2.AppliedDose) AppliedDose,
					sum(cp2.QuantityReceivable) QuantityReceivable,
					'Elaborado' DeliveryStatusName,
					3 DeliveryStatus,
					cdd.RequestMixingStationId Child,
					cdd.AdmissionNumber,
					cdd.BatchCode,
					IIF(cp2.DeliveryStatus = 2, 1, 0) AS Seleccione,
					cdd.PackageId,
					cdd.FunctionalUnitId,
					0 SurchargeApply,
					cdd.OrderedHealthProfessionalCode,
					cdd.OrderedHealthProfessionalThirdPartyId,
					cdd.HealthAdministratorId,
					cdd.OrderedProfessionalSpecialty,
					cdd.WarehouseId,
					cp2.AppliedDateDose,
					NULL Father,
					IIF(cdd.TypePayment = 2, 1,0) PaidToEndCampaign
			FROM Cte_WithoutDispensation cdd
			JOIN Cte_PharmaDoseDetail cp2 ON cdd.GroupingCodeDose= cp2.GroupingCodeDose
			JOIN MedicalHistory.ProductSusceptibleMixingStation psms ON psms.CodeSusceptibleMixingStation = cp2.CodeSusceptibleMixingStation
			GROUP BY	ProductId,Code,Name,cdd.AdmissionNumber,psms.FullProductName,psms.ApplicationsNumber,QuantityDelivered,cp2.DeliveryStatus,
						cdd.BatchCode,cp2.GroupingCodeDose,cdd.PackageId,cdd.WarehouseId,cdd.FunctionalUnitId,cp2.AppliedDateDose, cdd.HealthAdministratorId,
						cdd.OrderedHealthProfessionalCode, cdd.OrderedHealthProfessionalThirdPartyId, cdd.OrderedProfessionalSpecialty,
						cdd.TypePayment, cdd.RequestMixingStationId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de la estación de mezclas farmacéuticas que consolida la información de dosis preparadas, dispensadas y pendientes de dispensación para mezclas intravenosas u otras preparaciones especiales. Integra los registros de dispensación farmacéutica (detalle de ítems entregados, lotes y seriales usados) con las dosis registradas en la historia clínica (PharmaDose) y el estado de los paquetes generados en la estación de mezclas, identificando qué productos de tipo mezcla (clase 5) fueron despachados a cada ingreso del paciente. Diferencia dos escenarios clave: dosis que ya tienen una dispensación formal registrada en inventario, y dosis aprobadas por la estación de mezclas que aún no cuentan con documento de dispensación (pago tipo campaña sin dispensación asociada). El resultado final expone, por número de ingreso y código de agrupación de dosis, el producto mezclado con su código ATC, las cantidades entregadas, las dosis aplicadas y las cantidades pendientes de recibir, sirviendo de insumo para el control operativo y la trazabilidad de preparaciones farmacéuticas en la unidad de mezclas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewPharmaDoseMixingStation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewPharmaDoseMixingStation';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola vista las dosis farmacéuticas susceptibles de mezcla por paciente/admisión, combinando dispensaciones existentes y solicitudes a la estación de mezclas aún no dispensadas, con su estado de entrega para la operación de la mixing station.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewPharmaDoseMixingStation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en Inventory.PharmaceuticalDispensingDetailBatchSerial con OutstandingQuantity > 0 para considerar dispensaciones; Los productos deben pertenecer a ProductType con Class = 5 (clase de medicamentos susceptibles de mezcla); Las dosis en MedicalHistory.PharmaDose deben enlazar con HCFARMEPC vía CODCONCEC = IDHCFARMEPC; Para el ramo ''sin dispensación'' la campaña debe estar en estado 6 (CampaignDetail.CampaignStatus), el RequestMixingStationDetail.Source = 1, RequestPackageDetailStatus.Status = 3 y el TypePayment del CareGroupMixLiquidation = 2', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewPharmaDoseMixingStation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las filas de tipo ''cabecera'' (UNION 1 y 3) llevan Father con el Id de la dispensación o de la solicitud y Child en NULL; las filas de ''detalle'' (UNION 2 y 4) invierten la relación: Child con el Id padre y Father en NULL, modelando una jerarquía padre/hijo en la misma vista; El emparejamiento entre dispensación y dosis exige coincidencia simultánea de GroupingCodeDose, AtcCode/ProductCode y AdmissionNumber/NUMINGRES; Solo se consideran productos con clasificación ATC y de tipo Class=5; La cantidad entregada (QuantityDelivered) proviene de OutstandingQuantity en el ramo dispensado y de RequestMixingStationDetail.Quantity en el ramo sin dispensación; Las filas del ramo sin dispensación no exponen BatchCode/Pkg en la cabecera y siempre marcan SurchargeApply = 0 en el detalle', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewPharmaDoseMixingStation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dispensación farmacéutica; Dosis farmacéutica; Estación de mezclas (Mixing Station); Producto susceptible de mezcla; Clasificación ATC; Lote/Serial de inventario; Admisión/ingreso del paciente; Campaña de preparación; Grupo de atención y contrato; Tipo de pago (pago al cierre de campaña); Profesional ordenante y especialidad; Unidad funcional / bodega; Estado de entrega de dosis (Sin Entregar/Entregado/Generado/Elaborado)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewPharmaDoseMixingStation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewPharmaDoseMixingStation: Devuelve cuatro conjuntos UNION ALL: (1) cabecera por dispensación farmacéutica existente, (2) detalle por dosis con estado de entrega 0/1/2, (3) cabecera para solicitudes sin dispensación todavía, (4) detalle de esas solicitudes marcado como ''Elaborado'' (DeliveryStatus=3).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewPharmaDoseMixingStation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ProductType.Class = 5 → Solo se incluyen productos clasificados como susceptibles de mezcla en el CTE de dispensación; si pddb.OutstandingQuantity > 0 → Solo se suman como QuantityDelivered las unidades con saldo pendiente entregable; si CampaignDetail.CampaignStatus = 6 AND TypePayment = 2 AND RequestMixingStationDetail.Source = 1 AND RequestPackageDetailStatus.Status = 3 AND no existe dispensación (cdd.PharmaceuticalDispensingId IS NULL) → Se incluye la solicitud en el CTE Cte_WithoutDispensation (preparaciones sin dispensación asociada); si DeliveryStatus = 0/1/2 en PharmaDose → Se traduce a ''Sin Entregar'' / ''Entregado'' / ''Generado'' respectivamente en DeliveryStatusName else Para el ramo de Cte_WithoutDispensation se fija literalmente ''Elaborado'' con DeliveryStatus = 3; si DeliveryStatus = 2 (Generado) → Selected/Seleccione = 1 (la fila viene preseleccionada) else Selected = 0; si TypePayment = 2 (en ramo sin dispensación) → PaidToEndCampaign = 1 (pago al cierre de campaña) else PaidToEndCampaign = 0', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewPharmaDoseMixingStation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalDispensingDetailBatchSerial; Inventory.PharmaceuticalDispensingDetail; Inventory.PharmaceuticalDispensing; Inventory.InventoryProduct; Inventory.ATC; Inventory.ProductType; Inventory.PhysicalInventory; Inventory.BatchSerial; MixingStation.RequestPackageDetailStatus; MedicalHistory.PharmaDose; HCFARMEPC; MixingStation.RequestMixingStationDetail; MixingStation.CampaignDetail; ADINGRESO; Inventory.Warehouse; Payroll.FunctionalUnit; Contract.CareGroup; Contract.Contract; INPROFSAL; Common.ThirdParty; Contract.CareGroupMixLiquidation; MedicalHistory.ProductSusceptibleMixingStation', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewPharmaDoseMixingStation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewPharmaDoseMixingStation';
GO
