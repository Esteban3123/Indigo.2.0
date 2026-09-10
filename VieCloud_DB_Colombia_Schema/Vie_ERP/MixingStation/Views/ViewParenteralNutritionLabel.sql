CREATE VIEW [MixingStation].[ViewParenteralNutritionLabel]
AS
	SELECT -- CABECERA: IDENTIFICACIÓN DEL PACIENTE
		npc.Id
		, cd.Id CampaignDetailId
		, rpds.Id RequestPackageDetailStatusId
		, pac.IPNOMCOMP AS PatientName
		, td.SIGLA AS PatientDocumentType
		, pac.IPCODPACI AS PatientCode
		, dbo.Edad(pac.IPFECNACI, Common.GETDATE()) AS PatientAge
		, CONCAT (npc.PESOPACIE, ' kg') AS PatientWeight
		, CONCAT(RTRIM(ca.NOMCENATE), ' - ', RTRIM(fu.Name)) AS PatientLocation
		, rmsdp.Bed PatientBed
		--Parámetros físico-químicos
		, rpds.BatchCode
		, CASE npc.VIADMIN 
			WHEN 1 THEN 'Línea central' 
			WHEN 2 THEN 'Línea periférica'
			ELSE '' 
		END AdministrationRouteDescription
		, npc.TEMPOADMIN InfusionTime
		, npc.VOLUTOTAL TotalVolume
		, npc.VELINFUSION InfusionVelocity
		--Datos de preparación
		, cd.ProcessingDate
		, rpds.BatchExpirationDate FinishDate
		, ISNULL(cduQC.Usuario, '') AS QualityChemical
		, ISNULL(cduPC.Usuario, '') AS ProductionChemical
		, '' Recommendations
		, pnc.[NAME] AS ParenteralNutritionName
		, case npc.TIPOINFUSION
			WHEN 1 THEN 'Continua'
			WHEN 2 THEN 'Ciclada'
			ELSE ''
		END InfusionType
		, CASE pp.Storage
			WHEN 1 THEN	'Ambiente: 20 ° C - 25 ° C (Permitida 15 ° C y 30 ° C)'
			WHEN 2 THEN 'Ambiente controlada: 20 ° C - 25 ° C'  
			WHEN 3 THEN 'En frío: 8 ° C - 15 ° C'  
			WHEN 4 THEN 'Refrigerador: 2 ° C - 8 ° C'  
			WHEN 5 THEN 'Congelador: -25 ° C - 10 ° C'
		END StorageName
		, CASE pp.PhotoProtection
			WHEN 1 THEN 'Si'
			WHEN 0 THEN 'No'
		END PhotoProtection
	FROM MixingStation.CampaignDetail cd
	JOIN MixingStation.RequestMixingStationDetailPatients rmsdp ON cd.Id = rmsdp.CampaignDetailId
	JOIN MixingStation.RequestMixingStationDetail rmsd ON rmsd.Id = rmsdp.RequestMixingStationDetailId
	JOIN MixingStation.PackagePersonalized pp ON pp.Id = rmsd.PackagePersonalizedId
	JOIN MedicalHistory.ProductSusceptibleMixingStation psms ON rmsdp.EntityId = psms.Id
	JOIN INPACIENT pac ON rmsdp.PatientCode = pac.IPCODPACI
	JOIN dbo.ADTIPOIDENTIFICA td ON pac.IPTIPODOC = td.CODIGO 
	JOIN HCNUTPAREC npc ON psms.IdOrigin = npc.ID
	JOIN HCPARNUTC pnc ON npc.IDHCPARNUTC = pnc.Id
	JOIN Payroll.FunctionalUnit fu ON psms.FunctionalUnitCode = fu.Code
	JOIN ADCENATEN ca ON psms.CenterAttentionCode = ca.CODCENATE
	-----Batchcode
	JOIN MixingStation.RequestPackageDetailStatus rpds ON rmsdp.RequestMixingStationDetailId = rpds.RequestMixingStationDetailId
	--- Datos de preparación
	LEFT JOIN [MixingStation].[ViewUsersRolsCM] cduQC (NOLOCK) ON cd.Id = cduQC.CampaignDetailId AND cduQC.UserRole = 1 --QF Calidad
	LEFT JOIN [MixingStation].[ViewUsersRolsCM] cduPC (NOLOCK) ON cd.Id = cduPC.CampaignDetailId AND cduPC.UserRole = 2 --QF Producción
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Etiqueta de nutrición parenteral para la estación de mezclas farmacéuticas. Consolida, para cada bolsa o preparado magistral de nutrición parenteral, los datos de identificación del paciente (nombre, tipo y número de documento, edad, peso, ubicación en unidad funcional y cama), los parámetros físico-químicos de la fórmula (vía de administración —línea central o periférica—, tiempo de infusión, volumen total, velocidad de infusión, tipo de infusión continua o ciclada) y los datos de trazabilidad del lote (código de lote, fecha de vencimiento, nombre del nutriente o componente parenteral, condiciones de almacenamiento y fotoproteción). También identifica al químico farmacéutico de calidad y al de producción responsables del lote. Cruza información del ciclo de campaña farmacéutica (CampaignDetail), la asignación del paciente a la mezcla (RequestMixingStationDetailPatients), el producto susceptible de mezcla con su origen en historia clínica (ProductSusceptibleMixingStation, HCNUTPAREC, HCPARNUTC), los datos maestros del paciente (INPACIENT, ADTIPOIDENTIFICA) y la ubicación asistencial (ADCENATEN, FunctionalUnit); se usa para imprimir o generar la etiqueta oficial que acompaña cada preparación de nutrición parenteral dispensada desde farmacia.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewParenteralNutritionLabel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewParenteralNutritionLabel';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola fila los datos clínicos, fisicoquímicos y de preparación necesarios para imprimir la etiqueta oficial de una nutrición parenteral elaborada en la estación de mezclas.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewParenteralNutritionLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La preparación debe corresponder a una nutrición parenteral cuyo origen (ProductSusceptibleMixingStation.IdOrigin) exista en HCNUTPAREC.; El paciente identificado en RequestMixingStationDetailPatients.PatientCode debe existir en INPACIENT.; El tipo de documento del paciente (IPTIPODOC) debe estar parametrizado en ADTIPOIDENTIFICA.; Debe existir un registro de estado de paquete (RequestPackageDetailStatus) asociado al RequestMixingStationDetailId para obtener BatchCode y BatchExpirationDate.; El producto susceptible debe tener un centro de atención (ADCENATEN) y unidad funcional (Payroll.FunctionalUnit) válidos.; La preparación personalizada (PackagePersonalized) debe estar vinculada al detalle de mezcla.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewParenteralNutritionLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La edad del paciente se calcula con dbo.Edad usando la fecha de nacimiento y la fecha actual del sistema (Common.GETDATE()).; El peso del paciente se expresa siempre en kilogramos (concatenación con '' kg'').; La ubicación del paciente se compone como ''Centro de atención - Unidad funcional'' a partir de ADCENATEN.NOMCENATE y Payroll.FunctionalUnit.Name.; El campo Recommendations se devuelve siempre como cadena vacía (no se calcula).; Solo se incluyen preparaciones cuyo origen esté tipificado como nutrición parenteral en HCNUTPAREC/HCPARNUTC.; Si no hay químico de calidad o producción asignado en la campaña, los campos correspondientes salen como cadena vacía, no NULL.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewParenteralNutritionLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nutrición parenteral; Etiqueta de preparación farmacéutica; Vía de administración (central/periférica); Tipo de infusión (continua/ciclada); Lote y fecha de vencimiento (BatchCode/BatchExpirationDate); Condiciones de almacenamiento y fotoprotección; Químico farmacéutico de calidad y de producción; Paciente, cama y ubicación asistencial; Velocidad de infusión, tiempo de administración y volumen total; Campaña de preparación en estación de mezclas', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewParenteralNutritionLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewParenteralNutritionLabel: Devuelve una fila por cada combinación CampaignDetail × paciente × estado de paquete con los datos de etiqueta de nutrición parenteral; los químicos de calidad y producción se devuelven como cadena vacía cuando no existen (ISNULL sobre LEFT JOIN ViewUsersRolsCM).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewParenteralNutritionLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si npc.VIADMIN = 1 → AdministrationRouteDescription = ''Línea central'' else Si VIADMIN = 2 → ''Línea periférica''; cualquier otro valor → cadena vacía; si npc.TIPOINFUSION = 1 → InfusionType = ''Continua'' else Si TIPOINFUSION = 2 → ''Ciclada''; otro valor → cadena vacía; si pp.Storage IN (1..5) → StorageName se traduce a rangos de temperatura: 1=Ambiente 20-25°C (15-30°C permitido), 2=Ambiente controlada 20-25°C, 3=En frío 8-15°C, 4=Refrigerador 2-8°C, 5=Congelador -25 a 10°C; si pp.PhotoProtection = 1 → PhotoProtection = ''Si'' else Si = 0 → ''No''; si ViewUsersRolsCM.UserRole = 1 → Se toma como Químico Farmacéutico de Calidad (QualityChemical) else UserRole = 2 → Químico Farmacéutico de Producción (ProductionChemical)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewParenteralNutritionLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Edad; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewParenteralNutritionLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.CampaignDetail; MixingStation.RequestMixingStationDetailPatients; MixingStation.RequestMixingStationDetail; MixingStation.PackagePersonalized; MedicalHistory.ProductSusceptibleMixingStation; dbo.INPACIENT; dbo.ADTIPOIDENTIFICA; dbo.HCNUTPAREC; dbo.HCPARNUTC; Payroll.FunctionalUnit; dbo.ADCENATEN; MixingStation.RequestPackageDetailStatus; MixingStation.ViewUsersRolsCM', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewParenteralNutritionLabel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewParenteralNutritionLabel';
GO
