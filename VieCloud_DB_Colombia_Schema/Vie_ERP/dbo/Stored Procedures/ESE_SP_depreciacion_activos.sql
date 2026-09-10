CREATE PROCEDURE [dbo].[ESE_SP_depreciacion_activos] @depre INT, 
                                                    @libro INT
AS
     SELECT padb.legalbookid, 
            pa.ItemId, 
            AR.Code, 
            AR.Description, 
            pa.Plate, 
            pa.HistoricalValue, 
            pa.FairValue, 
            RES.Code, 
            TER.Name, 
            LO.Code, 
            LO.Name, 
            padb.DepreciatedValue, 
            padb.Id, 
            dd.AccumulatedDepreciation, 
            dd.AdjustedValue, 
            dd.DepreciatedDays, 
            dd.DepreciationValue, 
            dd.DevaluationValue, 
            dd.LifeTime, 
            dd.TransactionValue, 
            dd.ValorizationValue, 
            padb.HistoricalValue AS 'vhistorico niif'
     FROM FixedAsset.FixedAssetPhysicalAssetDetailBook Padb
          LEFT JOIN FixedAsset.FixedAssetDepreciationDetail dd ON dd.FixedAssetPhysicalAssetDetailBookId = padb.Id
                                                                  AND dd.FixedAssetDepreciationId = @depre
                                                                  AND dd.LegalBookId = @libro
          INNER JOIN FixedAsset.FixedAssetPhysicalAsset pa ON padb.PhysicalAssetId = pa.Id
          INNER JOIN FixedAsset.FixedAssetItem AR ON pa.itemid = AR.Id
          INNER JOIN FixedAsset.FixedAssetResponsible RES ON pa.ResponsibleId = RES.Id
          INNER JOIN Common.ThirdParty TER ON RES.ThirdPartyId = TER.Id
          INNER JOIN FixedAsset.FixedAssetLocation LO ON pa.LocationId = LO.Id
     WHERE padb.LegalBookId = @libro;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta el detalle de depreciación de activos fijos para un proceso de depreciación y libro contable específicos (NIIF u otro libro legal). Para cada activo físico registrado, retorna su identificación (código, descripción, placa), valores (histórico, razonable, depreciado, NIIF), datos del responsable y tercero asociado, ubicación física, y los cálculos contables del período: depreciación acumulada, días depreciados, valor ajustado, valorización, desvalorización y vida útil. Se usa para generar informes y reportes de depreciación de activos fijos por libro contable, apoyando la gestión contable y patrimonial de la organización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_depreciacion_activos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_depreciacion_activos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta el detalle de activos fijos físicos junto con sus valores de depreciación calculados para un libro contable y un proceso de depreciación específicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_depreciacion_activos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un libro contable identificado por el parámetro de libro en FixedAssetPhysicalAssetDetailBook.; El proceso de depreciación referenciado debe existir para que el LEFT JOIN retorne valores; si no existe, las columnas de depreciación quedarán nulas.; Los activos físicos deben tener responsable, tercero asociado, ítem catalogado y ubicación registrados (INNER JOIN obligatorio).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_depreciacion_activos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen registros de detalle pertenecientes al libro contable solicitado.; El filtro de proceso de depreciación se aplica únicamente sobre el detalle de depreciación, no excluye activos sin depreciación calculada.; Cada activo retornado tiene obligatoriamente responsable, tercero, ítem y ubicación asociados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_depreciacion_activos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Activo fijo físico; Depreciación; Libro contable (legal/NIIF); Valor histórico; Valor razonable (fair value); Valor depreciado; Depreciación acumulada; Valorización; Desvalorización; Vida útil; Responsable de activo; Ubicación de activo; Placa de activo; Tercero', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_depreciacion_activos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando padb.LegalBookId coincide con el libro indicado, retorna una fila por activo físico con sus datos contables, responsable, ubicación y valores de depreciación del proceso indicado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_depreciacion_activos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe registro en FixedAssetDepreciationDetail con el FixedAssetDepreciationId y LegalBookId solicitados para el detalle del activo → Se retornan los valores calculados de depreciación (acumulada, ajustada, días, valorización, desvalorización, vida útil, etc.) else Se retorna el activo con columnas de depreciación en NULL (LEFT JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_depreciacion_activos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetPhysicalAssetDetailBook; FixedAsset.FixedAssetDepreciationDetail; FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetResponsible; Common.ThirdParty; FixedAsset.FixedAssetLocation', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_depreciacion_activos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_depreciacion_activos';
-- GO
