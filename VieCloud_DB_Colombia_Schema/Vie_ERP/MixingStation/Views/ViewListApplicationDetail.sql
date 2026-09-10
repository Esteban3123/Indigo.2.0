

CREATE view [MixingStation].[ViewListApplicationDetail] 
as 

--select concat('HCFARMEPD', '-', d.ID) Row, d.ID Id,
--LTRIM(RTRIM(d.IPCODPACI)) PatientCode, LTRIM(RTRIM(pat.IPNOMCOMP)) PatientName, LTRIM(RTRIM(d.IPCODPACI)) + ' - ' + LTRIM(RTRIM(pat.IPNOMCOMP)) PatientCodeName,
--RTRIM(LTRIM(d.CODCENATE)) CareCenterCode, RTRIM(LTRIM(cc.NOMCENATE)) CareCenterName, RTRIM(LTRIM(d.CODCENATE)) + ' - ' + RTRIM(LTRIM(cc.NOMCENATE)) CareCenterCodeName,
--RTRIM(LTRIM(d.UFUCODIGO)) FunctionalUnitCode, RTRIM(LTRIM(fu.UFUDESCRI)) FunctionalUnitName, RTRIM(LTRIM(d.UFUCODIGO)) + ' - ' + RTRIM(LTRIM(fu.UFUDESCRI)) FunctionalUnitCodeName,
--d.CODPRODUC ProductCode, d.CANPEDPRO Quantity, ISNULL(d.DOSISPROD, 0) Dosage, d.CODUNIMED MeasurementUnitCode, d.OBSERVATIONSCM Observations,
--1 SourceType, ISNULL(cama.NUMCAMHOS, '') Bed, 0 AdministrationRouteId, 'INTRAVENOSA' AdministrationRouteCodeName,
--ISNULL(h.PREESTADO, 0) StatusHCPRESCRA,
--	case ISNULL(h.PREESTADO, 0)
--		when 3 then 'Tratamiento Descontinuado'
--		when 4 then 'Tratamiento Suspendido'
--		when 7 then 'Tratamiento Terminado por Salida del Paciente'
--		else ''
--	end StatusNameHCPRESCRA
--from dbo.HCFARMEPD d
--inner join dbo.HCFARMEPC c on c.CODCONCEC = d.CODCONCEC
--inner join dbo.ADINGRESO ing on c.NUMINGRES = ing.NUMINGRES
--inner join dbo.INPACIENT pat on pat.IPCODPACI = d.IPCODPACI
--inner join dbo.ADCENATEN cc on cc.CODCENATE = d.CODCENATE
--inner join dbo.INUNIFUNC fu on fu.UFUCODIGO = d.UFUCODIGO
--left join CHCAMASHO cama ON ISNULL(CASE WHEN ing.CODCAMACT = 0 THEN '' ELSE CAST(ing.CODCAMACT AS VARCHAR(15)) END, '') = cama.CODICAMAS
--left join (
--	select a.IPCODPACI, a.NUMINGRES, a.CODPRODUC, MAX(a.ID) HCPRESCRAId
--	from .HCPRESCRA a 
--	inner join .HCPRESCRD d on d.IPCODPACI = a.IPCODPACI and d.CODPRODUC = a.CODPRODUC and d.NUMINGRES = a.NUMINGRES
--	where d.TRATMODIF = 1
--	group by a.IPCODPACI, a.NUMINGRES, a.CODPRODUC
--) res on res.IPCODPACI = d.IPCODPACI and d.NUMINGRES = d.NUMINGRES and res.CODPRODUC = d.CODPRODUC
--left join .HCPRESCRA h WITH(NOLOCK) on h.ID = res.HCPRESCRAId
--where d.SENDTO in (1,3) and d.VIEPROCESSED = 0

select concat('PharmaDose', '-', phd.Id) Row, phd.GroupingCodeDose Id,
	LTRIM(RTRIM(pat.IPCODPACI)) PatientCode, LTRIM(RTRIM(pat.IPNOMCOMP)) PatientName, LTRIM(RTRIM(pat.IPCODPACI)) + ' - ' + LTRIM(RTRIM(pat.IPNOMCOMP)) PatientCodeName,
	RTRIM(LTRIM(hcp.CODCENATE)) CareCenterCode, RTRIM(LTRIM(cc.NOMCENATE)) CareCenterName, RTRIM(LTRIM(hcp.CODCENATE)) + ' - ' + RTRIM(LTRIM(cc.NOMCENATE)) CareCenterCodeName,
	RTRIM(LTRIM(hcp.UFUCODIGO)) FunctionalUnitCode, RTRIM(LTRIM(fu.UFUDESCRI)) FunctionalUnitName, RTRIM(LTRIM(hcp.UFUCODIGO)) + ' - ' + RTRIM(LTRIM(fu.UFUDESCRI)) FunctionalUnitCodeName,
	phd.ProductCode ProductCode, 1 Quantity, ISNULL(phd.Dose, 0) Dosage, phd.MeasurementUnitCode MeasurementUnitCode, '' Observations,
	1 SourceType, ISNULL(cama.NUMCAMHOS, '') Bed, 0 AdministrationRouteId, 'INTRAVENOSA' AdministrationRouteCodeName,
	ISNULL(h.PREESTADO, 0) StatusHCPRESCRA,
	case ISNULL(h.PREESTADO, 0)
		when 3 then 'Tratamiento Descontinuado'
		when 4 then 'Tratamiento Suspendido'
		when 7 then 'Tratamiento Terminado por Salida del Paciente'
		else ''
	end StatusNameHCPRESCRA
from MedicalHistory.PharmaDose phd
Inner Join MedicalHistory.ProductSusceptibleMixingStation psms with(nolock) on psms.CodeSusceptibleMixingStation = phd.CodeSusceptibleMixingStation
Left Join HCFARMEPC hcp with(nolock) on phd.IDHCFARMEPC = hcp.CODCONCEC
Left join dbo.ADINGRESO ing on hcp.NUMINGRES = ing.NUMINGRES
Left join dbo.INPACIENT pat on pat.IPCODPACI = hcp.IPCODPACI
Left join dbo.ADCENATEN cc on cc.CODCENATE = hcp.CODCENATE
Left join dbo.INUNIFUNC fu on fu.UFUCODIGO = hcp.UFUCODIGO
left join CHCAMASHO cama ON ISNULL(CASE WHEN ing.CODCAMACT = 0 THEN '' ELSE CAST(ing.CODCAMACT AS VARCHAR(15)) END, '') = cama.CODICAMAS
left join HCPRESCRA h on psms.Origin = 'HCPRESCRA' And psms.IdOrigin = h.ID

union all

select concat('RequestUnitDoseExternalCareCenterPatientDetails', '-', rpd.Id) Row, Cast(rpd.Id as Varchar(11)) Id,
pecc.IdentificationNumber PatientCode, pecc.Name + ' ' + pecc.LastName PatientName, pecc.IdentificationNumber + ' - ' + pecc.Name + ' ' + pecc.LastName PatientCodeName,
ecc.Code CareCenterCode, ecc.Description CareCenterName, ecc.Code + ' - ' + ecc.Description CareCenterCodeName,
rp.ExternalFunctionalUnitCode FunctionalUnitCode, '' FunctionalUnitName, rp.ExternalFunctionalUnitCode FunctionalUnitCodeName,
a.Code ProductCode, rpd.Quantity Quantity, rpd.Dosage Dosage, mu.Code MeasurementUnitCode, rpd.Observations,
2 SourceType,
rp.Bed,
rpd.AdministrationRouteId, ar.Code + ' - ' + ar.Name AdministrationRouteCodeName, '' StatusHCPRESCRA, '' StatusNameHCPRESCRA
from MixingStation.RequestUnitDoseExternalCareCenterPatientDetails rpd
inner join MixingStation.RequestUnitDoseExternalCareCenterPatient rp on rp.Id = rpd.RequestUnitDoseExternalCareCenterPatientId
inner join MixingStation.RequestUnitDoseExternalCareCenter rcc on rcc.Id = rp.RequestUnitDoseExternalCareCenterId
inner join MixingStation.PatientExternalCareCenter pecc on pecc.Id = rp.PatientExternalCareCenterId
inner join MixingStation.ExternalCareCenter ecc on ecc.Id = rcc.ExternalCareCenterId
inner join Inventory.ATC a on a.Id = rpd.ATCId
inner join Inventory.InventoryMeasurementUnit mu on mu.Id = rpd.MeasurementUnitId
inner join Inventory.AdministrationRoute ar on ar.Id = rpd.AdministrationRouteId
where rpd.SendTo is null
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el detalle de aplicaciones (dosis) pendientes de preparación en la estación de mezclas, integrando dos fuentes: (1) dosis farmacéuticas del módulo de historia clínica (PharmaDose y ProductSusceptibleMixingStation) vinculadas a órdenes médicas de farmacia, ingreso del paciente, centro de atención, unidad funcional y cama hospitalaria; y (2) solicitudes de dosis unitarias para pacientes de centros de atención externos (RequestUnitDoseExternalCareCenter). Por cada registro expone la identificación y nombre del paciente, sede y unidad funcional, medicamento, cantidad, dosis, unidad de medida, cama asignada, vía de administración intravenosa y el estado del tratamiento prescrito (activo, suspendido, descontinuado o terminado por salida). Sirve como fuente principal de reportería y gestión operativa de la estación de preparación de mezclas farmacéuticas, permitiendo identificar qué dosis deben prepararse, para qué paciente y en qué servicio, tanto para pacientes internos como externos.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListApplicationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListApplicationDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Unifica en un solo listado las dosis farmacéuticas pendientes de preparar en la estación de mezclas, provenientes tanto de pacientes internos (PharmaDose) como de solicitudes externas de dosis unitaria, con datos de paciente, servicio, producto y estado del tratamiento.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListApplicationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los productos internos deben estar registrados como susceptibles de mezcla (ProductSusceptibleMixingStation) y vinculados a una PharmaDose vía CodeSusceptibleMixingStation.; Para pacientes externos, el detalle (RequestUnitDoseExternalCareCenterPatientDetails) solo se incluye si SendTo IS NULL (aún no enviado).; Las claves de catálogo (ATC, InventoryMeasurementUnit, AdministrationRoute, ExternalCareCenter, PatientExternalCareCenter) deben existir para que la rama externa retorne filas (joins INNER).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListApplicationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Quantity siempre = 1 para registros provenientes de PharmaDose.; AdministrationRouteId siempre = 0 y AdministrationRouteCodeName siempre ''INTRAVENOSA'' para origen interno (PharmaDose).; SourceType = 1 identifica origen interno (PharmaDose) y SourceType = 2 identifica origen externo (solicitud de centro externo).; El campo Row se construye con prefijo identificador del origen (''PharmaDose-{Id}'' o ''RequestUnitDoseExternalCareCenterPatientDetails-{Id}''), garantizando unicidad entre ramas.; Dosage nunca es NULL en la rama interna: se aplica ISNULL(phd.Dose,0).; Los códigos textuales (PatientCode, CareCenterCode, FunctionalUnitCode) se devuelven recortados con LTRIM/RTRIM.; StatusHCPRESCRA y StatusNameHCPRESCRA solo aplican a la rama interna; en la rama externa son cadena vacía.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListApplicationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estación de mezclas farmacéuticas; Dosis unitaria; Paciente interno hospitalizado; Paciente de centro de atención externo; Orden médica / prescripción farmacéutica (HCPRESCRA); Estado de tratamiento (activo, descontinuado, suspendido, terminado por salida); Vía de administración intravenosa; Centro de atención y unidad funcional; Cama hospitalaria; Clasificación ATC de medicamentos', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListApplicationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewListApplicationDetail: Combina con UNION ALL dos orígenes: (1) PharmaDose con SourceType=1, Quantity fija=1 y vía ''INTRAVENOSA''; (2) RequestUnitDoseExternalCareCenterPatientDetails con SourceType=2 filtrado por SendTo IS NULL.; [RETURN_RESULT] MixingStation.ViewListApplicationDetail: Cuando psms.Origin=''HCPRESCRA'' y psms.IdOrigin=h.ID, traduce HCPRESCRA.PREESTADO a texto: 3=''Tratamiento Descontinuado'', 4=''Tratamiento Suspendido'', 7=''Tratamiento Terminado por Salida del Paciente'', otro=''''.; [RETURN_RESULT] MixingStation.ViewListApplicationDetail: Cuando ADINGRESO.CODCAMACT=0 expone Bed como cadena vacía; en caso contrario lo convierte a varchar(15) y lo cruza con CHCAMASHO.CODICAMAS para obtener NUMCAMHOS.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListApplicationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si psms.Origin = ''HCPRESCRA'' AND psms.IdOrigin = h.ID → Une con HCPRESCRA para obtener PREESTADO y traducirlo a etiqueta de estado del tratamiento. else StatusHCPRESCRA queda en 0 y StatusNameHCPRESCRA en cadena vacía.; si ISNULL(h.PREESTADO,0) IN (3,4,7) → Asigna etiqueta legible: Descontinuado / Suspendido / Terminado por Salida del Paciente. else StatusNameHCPRESCRA = '''' (sin etiqueta).; si ing.CODCAMACT = 0 → No intenta resolver cama (Bed=''''). else Castea CODCAMACT a varchar(15) y busca el número de cama en CHCAMASHO.; si rpd.SendTo IS NULL → Incluye el detalle de la solicitud externa en el resultado. else Excluye el detalle (ya fue enviado/procesado).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListApplicationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MedicalHistory.PharmaDose; MedicalHistory.ProductSusceptibleMixingStation; dbo.HCFARMEPC; dbo.ADINGRESO; dbo.INPACIENT; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.CHCAMASHO; dbo.HCPRESCRA; MixingStation.RequestUnitDoseExternalCareCenterPatientDetails; MixingStation.RequestUnitDoseExternalCareCenterPatient; MixingStation.RequestUnitDoseExternalCareCenter; MixingStation.PatientExternalCareCenter; MixingStation.ExternalCareCenter; Inventory.ATC; Inventory.InventoryMeasurementUnit; Inventory.AdministrationRoute', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListApplicationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListApplicationDetail';
GO
