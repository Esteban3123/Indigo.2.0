-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-07-08
-- Description:	Reversion de la estimacion de costos
-- =============================================
CREATE PROCEDURE [Cost].[SP_ReverseEstimateCostNative]
	@Year INT,
	@Month INT,
	@UserCode VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON

	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY
		/********************************************  DISTRIBUCION FINAL ********************************************/
		
		DELETE cmce
		FROM [Cost].[ClosedMonth] cm 
		JOIN [Cost].[ClosedMonthCUPSEntity] cmce ON cm.Id = cmce.ClosedMonthId
		WHERE cm.Year = @Year AND cm.Month = @Month

		DELETE cmca
		FROM [Cost].[ClosedMonth] cm 
		JOIN [Cost].[ClosedMonthCostActivity] cmca ON cm.Id = cmca.ClosedMonthId
		WHERE cm.Year = @Year AND cm.Month = @Month

		DELETE cmi
		FROM [Cost].[ClosedMonth] cm 
		JOIN [Cost].[ClosedMonthInventory] cmi ON cm.Id = cmi.ClosedMonthId
		WHERE cm.Year = @Year AND cm.Month = @Month

		DELETE cmp
		FROM [Cost].[ClosedMonth] cm 
		JOIN [Cost].[ClosedMonthPayroll] cmp ON cm.Id = cmp.ClosedMonthId
		WHERE cm.Year = @Year AND cm.Month = @Month

		DELETE cmfa
		FROM [Cost].[ClosedMonth] cm 
		JOIN [Cost].[ClosedMonthFixedAsset] cmfa ON cm.Id = cmfa.ClosedMonthId
		WHERE cm.Year = @Year AND cm.Month = @Month

		DELETE cmcebcc
		FROM [Cost].[ClosedMonth] cm 
		JOIN [Cost].[ClosedMonthCUPSEntityByCostCenter] cmcebcc ON cm.Id = cmcebcc.ClosedMonthId
		WHERE cm.Year = @Year AND cm.Month = @Month

		DELETE cm
		FROM [Cost].[ClosedMonth] cm 
		WHERE cm.Year = @Year AND cm.Month = @Month

		/****************************************** DISTRIBUCION SECUNDARIA ******************************************/

		UPDATE cen
			SET cen.SecondaryDirectCostDistribution = 0,
				cen.SecondaryAutoCostDistribution = 0,
				cen.SecondaryManPowerDistributionDirect = 0,
				cen.SecondaryManPowerDistributionInDirect = 0,
				cen.SecondaryFixedAssetDistribution = 0,
				cen.SecondaryDispensingDistribution = 0,
				cen.SecondaryTransferDistribution = 0,
				cen.SecondaryDistribution = 0
		FROM Cost.CostEstimationNative cen
		WHERE cen.Year = @Year AND cen.Month = @Month

		UPDATE cdds
			SET cdds.Status = 1
		FROM Cost.CostDirectDistributionSecondary cdds
		JOIN Cost.CostDirectDistributionSecondaryDetail cddsd ON cdds.Id = cddsd.DirectDistributionSecondaryId
		LEFT JOIN Cost.CostDirectDistributionSecondaryDetailRedistribution cddsdr ON cddsd.Id = cddsdr.DirectDistributionSecondaryDetailId
		WHERE cdds.Year = @Year AND cdds.Month = @Month AND cdds.Status = 2

		/****************************************** DISTRIBUCION PRIMARIA ******************************************/

		DELETE cdmi 
		FROM [Cost].[CostDistributionManpowerInitial] cdmi
		WHERE cdmi.Year = @Year AND cdmi.Month = @Month

		DELETE cdfai
		FROM [Cost].[CostDistributionFixedAssetInitial] cdfai
		WHERE cdfai.Year = @Year AND cdfai.Month = @Month

		UPDATE cen
			SET cen.DirectCostDistribution = 0,
				cen.AutoCostDistribution = 0,
				cen.ManPowerDistributionDirect = 0,
				cen.ManPowerDistributionInDirect = 0,
				cen.FixedAssetDistribution = 0,
				cen.DispensingDistribution = 0,
				cen.TransferDistribution = 0,
				cen.InitialDistribution = 0,
				cen.CostAccountingAdjustment = 0,
				cen.ManPowerAccountingAdjustment = 0,
				cen.FixedAssetAccountingAdjustment = 0,
				cen.DispensingAccountingAdjustment = 0,
				cen.TransferAccountingAdjustment = 0,
				cen.TotalSales = 0,
				cen.ModificationDate = Common.GETDATE(),
				cen.ModificationUser = @UserCode
		FROM Cost.CostEstimationNative cen
		WHERE cen.Year = @Year AND cen.Month = @Month

		SELECT	0 AS CodeMessage, 'Reversión realizada con éxito' AS Message
	END TRY
	BEGIN CATCH
		SELECT	999 AS CodeMessage, CONCAT('Error realizando la reversión: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE()) AS Message
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Revierte o anula la estimación de costos de un mes y año específicos en el módulo de costos nativos. Elimina todos los registros de cierre mensual asociados al período indicado: distribución de costos por CUPS/entidad, actividades, inventario, nómina, activos fijos y centros de costos. Adicionalmente, resetea a cero todos los campos de distribución primaria y secundaria en la estimación de costos nativa (CostEstimationNative), devolviendo el período a su estado inicial antes del proceso de estimación. Se utiliza cuando se necesita corregir o rehacer el proceso de costeo de un período contable cerrado.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ReverseEstimateCostNative';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ReverseEstimateCostNative';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Revierte el proceso de estimación y cierre de costos de un período (año/mes), eliminando los registros del cierre mensual y reseteando las distribuciones primaria y secundaria a su estado inicial.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseEstimateCostNative';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un cierre de costos previamente generado para el año/mes indicado (registros en Cost.ClosedMonth y/o valores distribuidos en CostEstimationNative).; Las distribuciones secundarias a revertir deben encontrarse en Status=2 para poder ser regresadas a Status=1.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseEstimateCostNative';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La reversión se ejecuta en un orden que primero elimina los detalles del cierre mensual y luego el encabezado de ClosedMonth, evitando registros huérfanos.; Tras ejecutar exitosamente, el período (año/mes) queda sin registros de cierre y con todos los valores de distribución (primaria, secundaria y ajustes contables) en cero en CostEstimationNative.; Solo se afectan registros del año y mes recibidos como parámetros; otros períodos no son tocados.; Toda modificación queda auditada con el usuario y fecha actual en CostEstimationNative.; El procedimiento nunca propaga excepciones: siempre devuelve un resultset con código 0 o 999.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseEstimateCostNative';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reversión de estimación de costos; Cierre mensual de costos; Distribución primaria de costos; Distribución secundaria de costos; Mano de obra (manpower); Activos fijos; Inventario; Nómina; Servicios CUPS por entidad; Centros de costo; Ajustes contables de costos; Dispensación y transferencia de costos', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseEstimateCostNative';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] Cost.ClosedMonthCUPSEntity: Elimina registros de CUPS por entidad ligados al ClosedMonth cuyo Year/Month coincide con el período recibido.; [DELETE] Cost.ClosedMonthCostActivity: Elimina los costos por actividad asociados al cierre del período indicado.; [DELETE] Cost.ClosedMonthInventory: Elimina el costo promedio de inventario del cierre del período indicado.; [DELETE] Cost.ClosedMonthPayroll: Elimina el cierre de nómina del período indicado.; [DELETE] Cost.ClosedMonthFixedAsset: Elimina los activos fijos del cierre del período indicado.; [DELETE] Cost.ClosedMonthCUPSEntityByCostCenter: Elimina los registros de CUPS por entidad y centro de costo del cierre del período indicado.; [DELETE] Cost.ClosedMonth: Tras eliminar todos sus dependientes, elimina el propio registro de cierre del año/mes indicado.; [UPDATE] Cost.CostEstimationNative: Para el período indicado, pone en 0 todos los valores de distribución secundaria (SecondaryDirectCostDistribution, SecondaryAutoCostDistribution, SecondaryManPowerDistributionDirect/InDirect, SecondaryFixedAssetDistribution, SecondaryDispensingDistribution, SecondaryTransferDistribution, SecondaryDistribution).; [UPDATE] Cost.CostDirectDistributionSecondary: Cuando una distribución secundaria del período tiene Status=2 y posee detalle, su Status se regresa a 1 (estado previo a la aplicación).; [DELETE] Cost.CostDistributionManpowerInitial: Elimina la distribución inicial de mano de obra del período indicado.; [DELETE] Cost.CostDistributionFixedAssetInitial: Elimina la distribución inicial de activos fijos del período indicado.; [UPDATE] Cost.CostEstimationNative: Para el período indicado, resetea a 0 las distribuciones primarias y ajustes contables (DirectCostDistribution, AutoCostDistribution, ManPower/FixedAsset/Dispensing/Transfer Distribution, InitialDistribution, *AccountingAdjustment, TotalSales) y registra ModificationDate=Common.GETDATE() y ModificationUser con el usuario recibido.; [RETURN_RESULT] (resultset): Si todo concluye sin error, retorna CodeMessage=0 y mensaje ''Reversión realizada con éxito''; si hay excepción, retorna CodeMessage=999 con el mensaje de error y línea.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseEstimateCostNative';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Distribución secundaria con Year/Month del período y Status=2 con detalle existente → Se actualiza su Status a 1 (revertir aplicación) else No se modifica; si Ocurre una excepción en cualquier paso (BEGIN CATCH) → Se devuelve un resultset con CodeMessage=999 y mensaje de error concatenado else Se devuelve CodeMessage=0 con mensaje de éxito', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseEstimateCostNative';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseEstimateCostNative';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.ClosedMonth; Cost.ClosedMonthCUPSEntity; Cost.ClosedMonthCostActivity; Cost.ClosedMonthInventory; Cost.ClosedMonthPayroll; Cost.ClosedMonthFixedAsset; Cost.ClosedMonthCUPSEntityByCostCenter; Cost.CostEstimationNative; Cost.CostDirectDistributionSecondary; Cost.CostDirectDistributionSecondaryDetail; Cost.CostDirectDistributionSecondaryDetailRedistribution; Cost.CostDistributionManpowerInitial; Cost.CostDistributionFixedAssetInitial', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseEstimateCostNative';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseEstimateCostNative';
-- GO
