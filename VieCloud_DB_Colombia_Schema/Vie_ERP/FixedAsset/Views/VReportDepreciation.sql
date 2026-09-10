CREATE VIEW [FixedAsset].[VReportDepreciation]
AS
SELECT dd.Id,
		fi.code AS CodeItem,
		fi.[Description],
		ma.Number,
		ma.[Name] AS NameAccount,
		pa.Serie,
		pa.Plate,
		pa.Model,
		ft.Code,
		ft.[Name],
		(pa.HistoricalValue - pa.FinancialDiscount) HistoricalValue,
		dd.ResidualValue,
		(dd.DepreciationValue - dd.FinantialDiscountAdjusment) DepreciationValue,
		dd.DepreciatedDays,
		d.ClosingYear,
		d.ClosingMonth,
		gb.Code AS CodeLegalBook,
		gb.Name AS NameLegalBook
FROM FixedAsset.FixedAssetDepreciationDetail dd WITH(NOLOCK)
JOIN FixedAsset.FixedAssetDepreciation d WITH(NOLOCK) ON d.id = dd.FixedAssetDepreciationId
JOIN GeneralLedger.LegalBook gb WITH(NOLOCK) ON gb.Id = dd.LegalBookId
JOIN FixedAsset.FixedAssetPhysicalAsset pa WITH(NOLOCK) ON pa.Id = dd.FixedAssetPhysicalAssetId
JOIN FixedAsset.FixedAssetItem fi WITH(NOLOCK) ON fi.Id = pa.ItemId
JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON ma.Id = pa.MainAccountId
JOIN FixedAsset.FixedAssetTrademark ft WITH(NOLOCK) ON ft.Id = pa.TrademarkId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de depreciación de activos fijos que consolida el detalle de cada cálculo de depreciación por activo físico, mostrando el ítem del activo (código y descripción), la cuenta contable principal asociada, datos de identificación del bien (serie, placa, modelo y marca), así como los valores financieros clave: valor histórico neto de descuento financiero, valor residual, valor depreciado ajustado y días depreciados. Integra también el período de cierre (año y mes) y el libro legal contable al que pertenece cada movimiento, permitiendo generar informes contables y fiscales de depreciación de activos fijos por período y libro legal.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'VIEW', @level1name = N'VReportDepreciation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'VIEW', @level1name = N'VReportDepreciation';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida el detalle de depreciación de activos fijos por período y libro contable, exponiendo valores netos (histórico y depreciación ajustados por descuentos financieros) junto con datos del bien, marca, cuenta contable y libro legal, para fines de reporte.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportDepreciation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada detalle de depreciación debe tener su cabecera de depreciación, libro legal, activo físico, ítem, cuenta contable principal y marca asociados (joins internos requieren existencia).', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportDepreciation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor histórico reportado siempre se presenta neto del descuento financiero del activo físico.; El valor de depreciación reportado siempre se presenta neto del ajuste por descuento financiero.; Solo se incluyen detalles con relaciones completas a cabecera, libro legal, activo físico, ítem, cuenta y marca (INNER JOIN).; Se usa NOLOCK en todas las tablas: lectura sin bloqueo, puede haber lecturas sucias, consistente con uso de reporte.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportDepreciation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Depreciación de activos fijos; Valor histórico; Valor residual; Descuento financiero; Ajuste por descuento financiero; Días depreciados; Período de cierre contable; Libro legal contable; Cuenta contable; Activo físico (placa, serie, modelo); Marca de activo fijo', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportDepreciation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] FixedAsset.VReportDepreciation: Devuelve el valor histórico neto como (HistoricalValue - FinancialDiscount) del activo físico.; [RETURN_RESULT] FixedAsset.VReportDepreciation: Devuelve el valor de depreciación neto como (DepreciationValue - FinantialDiscountAdjusment) del detalle.; [RETURN_RESULT] FixedAsset.VReportDepreciation: Expone el período contable (ClosingYear, ClosingMonth) tomado de la cabecera de depreciación.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportDepreciation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetDepreciationDetail; FixedAsset.FixedAssetDepreciation; GeneralLedger.LegalBook; FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetItem; GeneralLedger.MainAccounts; FixedAsset.FixedAssetTrademark', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportDepreciation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportDepreciation';
GO
