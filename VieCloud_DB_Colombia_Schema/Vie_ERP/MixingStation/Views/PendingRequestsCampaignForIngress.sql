

CREATE VIEW [MixingStation].[PendingRequestsCampaignForIngress]
AS

SELECT	ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) Id
		,cd.CampaignNumber 
		,cd.CampaignStatus
		,CASE cd.CampaignStatus
			WHEN 1 THEN 'Abierta'
			WHEN 2 THEN 'Cerrada'
			WHEN 5 THEN 'Procesada'
		END CampaignStatusName
		,a.NUMINGRES 
		,a.IPCODPACI PatientCode
		FROM MixingStation.RequestPackageDetailStatus rpds
		JOIN MixingStation.RequestMixingStationDetail rmd ON rmd.Id = rpds.RequestMixingStationDetailId
		JOIN MixingStation.CampaignDetail cd ON cd.Id = rmd.CampaignDetailId
		JOIN MedicalHistory.PharmaDose phd ON phd.GroupingCodeDose = rpds.GroupingCodeDose
		JOIN MedicalHistory.ProductSusceptibleMixingStation psms on psms.CodeSusceptibleMixingStation = phd.CodeSusceptibleMixingStation and phd.ProductCode = psms.MainDrugCode
		JOIN HCFARMEPD hcf on psms.CodeSusceptibleMixingStation = hcf.CodeSusceptibleMixingStation and hcf.CODPRODUC = psms.MainDrugCode
		JOIN HCFARMEPC fpc on hcf.CODCONCEC = fpc.CODCONCEC
		JOIN ADINGRESO a ON a.NUMINGRES = fpc.NUMINGRES
		JOIN Contract.CareGroupMixLiquidation cgml ON cgml.CareGroupId = a.GENCAREGROUP AND cd.UnitDoseTypeId = cgml.UnitDoseTypeId
		WHERE cd.CampaignStatus IN (1,2,5) AND rmd.Source = 1 AND cgml.TypePayment = 2
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las solicitudes de preparación farmacéutica en estación de mezclas que están pendientes o activas por ingreso de paciente, mostrando el número y estado de campaña (Abierta, Cerrada o Procesada), el número de ingreso hospitalario y el código del paciente. Integra el ciclo completo desde el estado de los paquetes de mezcla (RequestPackageDetailStatus), el detalle de la solicitud (RequestMixingStationDetail) y la campaña farmacéutica (CampaignDetail), cruzando con las dosis prescritas en historia clínica (PharmaDose), los productos susceptibles de mezcla (ProductSusceptibleMixingStation), las órdenes de medicamentos de farmacia (HCFARMEPD, HCFARMEPC) y el episodio de ingreso del paciente (ADINGRESO). Filtra únicamente campañas en estado Abierta, Cerrada o Procesada, cuyo origen sea estación de mezclas y cuyo tipo de pago contractual por grupo de atención (CareGroupMixLiquidation) corresponda a liquidación por mezcla, permitiendo así identificar qué ingresos tienen preparaciones farmacéuticas pendientes de gestión en el proceso de mezclas y facturación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'PendingRequestsCampaignForIngress';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'PendingRequestsCampaignForIngress';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los ingresos hospitalarios con campañas de preparación farmacéutica (mezclas) pendientes o procesadas que deben gestionarse bajo liquidación por mezcla, traduciendo el estado numérico de la campaña a su nombre.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'PendingRequestsCampaignForIngress';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir relación entre la dosis farmacéutica (PharmaDose) y un producto susceptible de mezcla (ProductSusceptibleMixingStation) por GroupingCodeDose, CodeSusceptibleMixingStation y MainDrugCode/ProductCode.; El producto debe estar enlazado con HCFARMEPD/HCFARMEPC para resolver el ingreso (NUMINGRES) en ADINGRESO.; El grupo de atención del ingreso (GENCAREGROUP) y el tipo de dosis unitaria de la campaña deben tener configurada una liquidación por mezcla en Contract.CareGroupMixLiquidation.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'PendingRequestsCampaignForIngress';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen campañas en estados 1, 2 o 5 (Abierta, Cerrada o Procesada); cualquier otro estado se excluye.; Solo se incluyen detalles de solicitud cuyo Source = 1.; Solo se consideran combinaciones de grupo de atención y tipo de dosis unitaria con TypePayment = 2 (liquidación por mezcla).; Cada fila corresponde a un ingreso (NUMINGRES) cuyo grupo de atención tiene configurada liquidación por mezcla compatible con el tipo de dosis unitaria de la campaña.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'PendingRequestsCampaignForIngress';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Campaña de preparación farmacéutica; Estación de mezclas; Ingreso hospitalario; Paciente; Dosis farmacéutica; Producto susceptible de mezcla; Grupo de atención; Tipo de dosis unitaria; Liquidación por mezcla / tipo de pago', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'PendingRequestsCampaignForIngress';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.PendingRequestsCampaignForIngress: Devuelve filas con Id secuencial, número y estado de campaña, nombre legible del estado (1=Abierta, 2=Cerrada, 5=Procesada), número de ingreso y código de paciente.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'PendingRequestsCampaignForIngress';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si cd.CampaignStatus = 1 → Etiqueta el estado como ''Abierta''.; si cd.CampaignStatus = 2 → Etiqueta el estado como ''Cerrada''.; si cd.CampaignStatus = 5 → Etiqueta el estado como ''Procesada''.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'PendingRequestsCampaignForIngress';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.RequestPackageDetailStatus; MixingStation.RequestMixingStationDetail; MixingStation.CampaignDetail; MedicalHistory.PharmaDose; MedicalHistory.ProductSusceptibleMixingStation; dbo.HCFARMEPD; dbo.HCFARMEPC; dbo.ADINGRESO; Contract.CareGroupMixLiquidation', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'PendingRequestsCampaignForIngress';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'PendingRequestsCampaignForIngress';
GO
