

CREATE VIEW [MixingStation].[ViewDetailPharmaceuticalParametersNPT]
AS
SELECT
			  pdd.ID
			, pac.IPNOMCOMP PatientName
			, pac.IPCODPACI PatientCode
			, ISNULL(cma.NUMCAMHOS, '') AS Bed
			, CONCAT(psms.FunctionalUnitCode, ' - ', fu.Name) FunctionalUnitCodeName
			, pa.PESOPACIE PatientWeight
			, pa.VIADMIN AdministrationRoute
			, case pa.VIADMIN when 1 then 'Línea central' when 2 then 'Línea periférica' else '' end AdministrationRouteDescription
			, pa.TEMPOADMIN InfusionTime
			, pa.VOLUTOTAL TotalVolume
			, pa.VELINFUSION InfusionVelocity
			, pdd.NAMEFORM ParameterName
			, pdd.RESULT Result
			, pdd.COLOR Color
			, CAST(ph.GroupingCodeDose As Varchar(36)) GroupingCodeDose
	FROM HCNUTPAREFD pdd
	INNER JOIN HCNUTPAREC pa ON pa.ID = pdd.IDHCNUTPAREC
	INNER JOIN MedicalHistory.ProductSusceptibleMixingStation psms ON psms.Origin = 'HCNUTPAREC' AND psms.IdOrigin = pa.ID
	INNER JOIN INPACIENT pac on pa.IPCODPACI = pac.IPCODPACI
	INNER JOIN Payroll.FunctionalUnit fu on psms.FunctionalUnitCode = fu.Code
	LEFT JOIN ADINGRESO ing ON pa.NUMINGRES = ing.NUMINGRES
	LEFT JOIN CHCAMASHO cma WITH(NOLOCK) ON ISNULL(CASE WHEN ing.CODCAMACT = 0 THEN '' ELSE Cast(ing.CODCAMACT As VARCHAR(15)) END, '') = cma.CODICAMAS
	OUTER APPLY (	SELECT 
					TOP 1 ph.GroupingCodeDose 
					FROM MedicalHistory.PharmaDose ph
					WHERE ph.CodeSusceptibleMixingStation = psms.CodeSusceptibleMixingStation) ph
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el detalle de parámetros farmacéuticos de Nutrición Parenteral Total (NPT) para la estación de mezclas. Integra los resultados de cada parámetro nutricional calculado (nombre, valor numérico y color indicador) con los datos del paciente (nombre, cédula/identificación), la cama hospitalaria asignada, la unidad funcional de preparación y los datos clínicos de la fórmula nutricional como peso del paciente, vía de administración (línea central o periférica), tiempo de infusión, volumen total y velocidad de infusión. Relaciona la evaluación nutricional del paciente con el producto susceptible de mezcla registrado en la estación, el ingreso hospitalario activo y el código de agrupación de dosis farmacéutica. Se utiliza para la visualización y seguimiento de órdenes de NPT en la estación de mezclas, apoyando la preparación, trazabilidad y dispensación de nutrición parenteral.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewDetailPharmaceuticalParametersNPT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewDetailPharmaceuticalParametersNPT';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los parámetros farmacéuticos detallados de las prescripciones de Nutrición Parenteral Total (NPT) susceptibles de mezcla en central, con datos del paciente, ubicación, vía y características de infusión.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDetailPharmaceuticalParametersNPT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La prescripción NPT en HCNUTPAREC debe tener un registro asociado en MedicalHistory.ProductSusceptibleMixingStation con Origin=''HCNUTPAREC'' e IdOrigin igual al ID de la prescripción.; El paciente (IPCODPACI) debe existir en INPACIENT.; La unidad funcional referenciada en ProductSusceptibleMixingStation debe existir en Payroll.FunctionalUnit.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDetailPharmaceuticalParametersNPT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen prescripciones cuyo Origin en ProductSusceptibleMixingStation sea exactamente ''HCNUTPAREC'' (NPT).; GroupingCodeDose se obtiene mediante TOP 1 sobre PharmaDose filtrando por CodeSusceptibleMixingStation, garantizando a lo más un código de agrupación por fila.; Bed nunca es NULL: se reemplaza por cadena vacía vía ISNULL(NUMCAMHOS,'''').; FunctionalUnitCodeName concatena siempre código y nombre de la unidad funcional con separador '' - ''.; La ausencia de ingreso (ADINGRESO) o de cama no excluye la fila: se usan LEFT JOIN.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDetailPharmaceuticalParametersNPT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nutrición Parenteral Total (NPT); Estación de mezclas farmacéuticas; Paciente; Cama hospitalaria; Unidad funcional; Vía de administración (línea central/periférica); Tiempo y velocidad de infusión; Volumen total; Peso del paciente; Dosis farmacéutica; Código de agrupación de dosis', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDetailPharmaceuticalParametersNPT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewDetailPharmaceuticalParametersNPT: Devuelve una fila por cada parámetro farmacéutico (HCNUTPAREFD) ligado a una prescripción NPT (HCNUTPAREC) que esté marcada como susceptible de mezcla en estación.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDetailPharmaceuticalParametersNPT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pa.VIADMIN = 1 → AdministrationRouteDescription = ''Línea central'' else Si VIADMIN = 2 entonces ''Línea periférica''; cualquier otro valor produce cadena vacía.; si ing.CODCAMACT = 0 o NULL en ADINGRESO → Se usa cadena vacía para buscar la cama, resultando en Bed vacío vía ISNULL else Se castea CODCAMACT a VARCHAR(15) y se hace match con CHCAMASHO.CODICAMAS para obtener NUMCAMHOS.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDetailPharmaceuticalParametersNPT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCNUTPAREFD; dbo.HCNUTPAREC; MedicalHistory.ProductSusceptibleMixingStation; dbo.INPACIENT; Payroll.FunctionalUnit; dbo.ADINGRESO; dbo.CHCAMASHO; MedicalHistory.PharmaDose', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDetailPharmaceuticalParametersNPT';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDetailPharmaceuticalParametersNPT';
GO
