

CREATE VIEW [Inventory].[ViewLASAMedication]
AS
	SELECT *
	FROM (
			SELECT	CONCAT(atc.Code, '-', dci.Code, '-', sdci.CODDCIMED) UUID,
					atc.Code,
					CONCAT(atc.Code, ' - ', atc.Name) RequestedMedication,
					CASE p.TIPO
						WHEN 1 THEN 'Suena Igual'
						WHEN 2 THEN 'Se parece'
						WHEN 3 THEN 'Suena Igual y se Parece'
					END Likeness,
					CONCAT(sdci.CODDCIMED, ' - ', sdci.DESDCIMED) SimilarMedicine,
					dci.TypeWarning as IsRestrictive
			FROM Inventory.ATC atc
			JOIN Inventory.DCI dci ON atc.DCIId = dci.Id
			JOIN dbo.IHPARAMDCI p ON dci.Code = p.CODDCIMEDPADRE OR dci.Code = p.CODDCIMED
			JOIN dbo.IHDCIMEDI sdci ON IIF(dci.Code = p.CODDCIMEDPADRE, p.CODDCIMED, p.CODDCIMEDPADRE) = sdci.CODDCIMED AND dci.Code <> p.CODDCIMED) sub
			GROUP by sub.UUID,sub.RequestedMedication,sub.Likeness,sub.SimilarMedicine,sub.IsRestrictive, sub.Code
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista de medicamentos LASA (Look-Alike, Sound-Alike) que identifica pares de medicamentos que suenan igual, se parecen visualmente, o ambas condiciones, para prevenir errores de dispensación y prescripción. Cruza el catálogo de medicamentos ATC con sus principios activos (DCI) y la tabla de parámetros de similitud (IHPARAMDCI), para exponer por cada medicamento consultado cuál es su medicamento similar o confundible, el tipo de parecido (fonético, visual o ambos) y si el medicamento tiene restricción especial de alerta. Sirve como base para alertas clínicas y de farmacia al momento de formular o dispensar medicamentos con alto riesgo de confusión, apoyando la seguridad del paciente.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewLASAMedication';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewLASAMedication';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los pares de medicamentos con riesgo de confusión (LASA: Look-Alike/Sound-Alike) clasificando su tipo de similitud fonética y/o visual, junto con el indicador restrictivo del DCI.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewLASAMedication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir correspondencia entre Inventory.ATC y Inventory.DCI mediante DCIId; Los códigos DCI deben estar registrados en dbo.IHPARAMDCI como padre o hijo en una relación de similitud; El código del medicamento similar debe existir en dbo.IHDCIMEDI', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewLASAMedication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Un medicamento nunca se compara consigo mismo (dci.Code <> p.CODDCIMED); La relación de similitud es bidireccional: se evalúa tanto cuando el DCI es padre como cuando es hijo en IHPARAMDCI; Solo existen tres tipos de similitud reconocidos: 1=Suena Igual, 2=Se parece, 3=Suena Igual y se Parece; El UUID identifica unívocamente la tripleta ATC-DCI-Medicamento similar', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewLASAMedication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Medicamentos LASA (Look-Alike Sound-Alike); Clasificación ATC; Denominación Común Internacional (DCI); Medicamentos restrictivos; Similitud fonética y visual de medicamentos', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewLASAMedication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve pares de medicamentos LASA con su tipo de parecido (''Suena Igual'', ''Se parece'', ''Suena Igual y se Parece'') derivado de IHPARAMDCI.TIPO y marca de restricción tomada de DCI.TypeWarning', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewLASAMedication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si p.TIPO = 1 → Clasifica la similitud como ''Suena Igual''; si p.TIPO = 2 → Clasifica la similitud como ''Se parece''; si p.TIPO = 3 → Clasifica la similitud como ''Suena Igual y se Parece''; si dci.Code = p.CODDCIMEDPADRE → El medicamento similar se toma de p.CODDCIMED else El medicamento similar se toma de p.CODDCIMEDPADRE', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewLASAMedication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.ATC; Inventory.DCI; dbo.IHPARAMDCI; dbo.IHDCIMEDI', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewLASAMedication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewLASAMedication';
GO
