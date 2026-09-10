

CREATE VIEW [MixingStation].[ViewDilutionFactorsDetailByIdFactor] 
AS
		Select CONCAT(DFD.Id, ' ') Id
		,DF.Code as CodeFactor
		, DF.Id as DilutionFactorId
		, dfd.Id as DilutionFactorDetailId
		, df.ATCId as ATCDilutionFactor
		, DFD.AtcId as ATCDilutionFactorDetail
		,dfd.Volume
		,dfd.Dilution
		,DFD.Concentration
		,DFD.AmountTime
		,DFD.TimeUnit
		, a.Code
		, a.Name
		,df.CreationDate
		,df.ModificationDate
		From MixingStation.DilutionFactorsDetail DFD with(nolock)
		Inner Join MixingStation.DilutionFactors DF with(nolock) On DFD.DilutionFactorsId = DF.Id
		INNER join Inventory.ATC A WITH (NOLOCK) on DFD.AtcId = A.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el detalle de los factores de dilución utilizados en la estación de mezclas (preparación de medicamentos). Combina el encabezado del factor de dilución (código, medicamento ATC asociado, fechas de creación y modificación) con cada línea de detalle que especifica el medicamento diluente (ATC del detalle), el volumen, el porcentaje de dilución, la concentración, y el tiempo de administración con su unidad. Se apoya en el catálogo maestro de medicamentos ATC para obtener el código y nombre legible del insumo o medicamento involucrado en la dilución. Sirve para consultar, por factor de dilución, todos los componentes y parámetros técnicos de preparación de mezclas farmacéuticas, facilitando la trazabilidad y validación de recetas de mezclas en farmacia hospitalaria.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewDilutionFactorsDetailByIdFactor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewDilutionFactorsDetailByIdFactor';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el detalle de factores de dilución junto con el factor padre y datos del medicamento (ATC), para consulta por identificador del factor de dilución.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDilutionFactorsDetailByIdFactor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada detalle debe tener un factor de dilución padre válido en MixingStation.DilutionFactors (INNER JOIN por DilutionFactorsId).; Cada detalle debe referenciar un ATC existente en Inventory.ATC (INNER JOIN por AtcId).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDilutionFactorsDetailByIdFactor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El identificador expuesto del detalle se entrega como cadena con un espacio al final (CONCAT(DFD.Id, '' '')), forzando tipo texto.; Las fechas de creación y modificación expuestas corresponden al factor padre (DilutionFactors), no al detalle.; Se exponen tanto el ATC del factor padre (df.ATCId) como el ATC del detalle (DFD.AtcId), permitiendo distinguir cuando difieren.; Todas las lecturas se realizan con WITH(NOLOCK), por lo que pueden incluirse lecturas sucias.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDilutionFactorsDetailByIdFactor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factor de dilución; Detalle de factor de dilución; Medicamento ATC; Concentración; Volumen; Tiempo de administración; Estación de mezclas', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDilutionFactorsDetailByIdFactor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve una fila por cada registro de DilutionFactorsDetail combinado con su factor padre y su ATC; los detalles sin factor padre o sin ATC asociado quedan excluidos por los INNER JOIN.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDilutionFactorsDetailByIdFactor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.DilutionFactorsDetail; MixingStation.DilutionFactors; Inventory.ATC', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDilutionFactorsDetailByIdFactor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDilutionFactorsDetailByIdFactor';
GO
