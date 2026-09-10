
-- =============================================
-- Author:		Juan Fernando Tamayo
-- Create date:	2016-10-24
-- Description:	Lista los detalles para el reporte de Distribución de Elementos del Costo
-- =============================================
CREATE PROCEDURE [InteropCost].[SP_ReportDistributionDirectCost]
	@ContainerCost NVARCHAR(20),
	@DistributionDirectCostId INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @ResultTable TABLE(MainAccount VARCHAR(50), CostCenter VARCHAR(50), [Value] DECIMAL(18,2), CenterType int)
	DECLARE @StatementSql NVARCHAR(MAX) = N'SELECT CAST((COALESCE(cta.CUECODIGO, '''') + '' - '' + COALESCE(cta.CUENOMBRE, '''')) AS VARCHAR(50)) AS MainAccount, CAST((COALESCE(ccos.CCCODIGO, '''') + '' - '' + COALESCE(ccos.CCNOMBRE, '''')) AS VARCHAR(50)) AS CostCenter, CAST(SUM(ddcd.[Value]) AS DECIMAL(18,2)) AS [Value], pc.CenterType as CenterType FROM InteropCost.DistributionDirectCostDetail ddcd 
     INNER JOIN InteropCost.DistributionDirectCost ddc ON ddc.Id = ddcd.DistributionDirectCostId
     LEFT OUTER JOIN ' + @ContainerCost + '.dbo.CTNCUENTA cta ON ddcd.MainAccountId = cta.OID
     LEFT OUTER JOIN ' + @ContainerCost + '.dbo.CTNCENCOS ccos ON ddcd.CostCenterId = ccos.OID
	 inner join InteropCost.ProductionCenter pc on pc.Id = ddcd.ProductionCenterId
     WHERE ddc.Id = ' + CAST(@DistributionDirectCostId AS VARCHAR(50)) + '
     GROUP BY pc.CenterType, cta.CUECODIGO, ccos.CCCODIGO, cta.CUENOMBRE, ccos.CCNOMBRE'

	INSERT INTO @ResultTable 
	EXEC sp_executesql @StatementSql

	SELECT MainAccount, CostCenter, [Value], CenterType FROM @ResultTable order by CenterType asc
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el detalle del reporte de Distribución de Costos Directos por elementos del costo, consolidando la información de cuentas contables principales y centros de costo asociados a una distribución específica. Recibe como parámetros el identificador del contenedor de costos (base de datos de la empresa) y el identificador de la distribución de costos directos a consultar. Cruza los detalles de distribución con las tablas maestras de cuentas contables (CTNCUENTA) y centros de costo (CTNCENCOS) del contenedor indicado, agrupando y sumando los valores por tipo de centro de producción. Retorna un listado ordenado por tipo de centro con la cuenta principal, el centro de costo, el valor monetario acumulado y el tipo de centro de producción, útil para reportería contable y control de costos hospitalarios.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportDistributionDirectCost';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportDistributionDirectCost';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte detallado de Distribución de Elementos del Costo Directo, agrupando valores por cuenta contable principal, centro de costo y tipo de centro de producción para una distribución específica.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDistributionDirectCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La base de datos cuyo nombre se recibe debe existir y contener las tablas dbo.CTNCUENTA y dbo.CTNCENCOS accesibles para el ejecutor.; Debe existir el registro de DistributionDirectCost identificado por el Id recibido (de lo contrario el reporte vendrá vacío).; Los detalles en DistributionDirectCostDetail deben referenciar ProductionCenter válidos (INNER JOIN obligatorio).', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDistributionDirectCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La cuenta contable y el centro de costo se concatenan como ''código - nombre'', usando cadena vacía cuando alguno es NULL (COALESCE).; El cruce con cuentas (CTNCUENTA) y centros de costo (CTNCENCOS) es LEFT JOIN: se conservan detalles aunque no exista la cuenta o el centro de costo en la BD contable externa.; El cruce con ProductionCenter es INNER JOIN: solo se incluyen detalles cuyo centro de producción exista.; El filtro por DistributionDirectCostId garantiza que el reporte corresponde a una sola distribución directa.; Los valores numéricos se truncan/redondean a DECIMAL(18,2).', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDistributionDirectCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Distribución de costos directos; Cuenta contable principal; Centro de costo; Centro de producción; Tipo de centro (CenterType); Reporte de elementos del costo', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDistributionDirectCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve filas (MainAccount, CostCenter, Value, CenterType) ordenadas ascendentemente por CenterType, agrupando SUM(Value) por cuenta, centro de costo y tipo de centro de producción.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDistributionDirectCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'sys.sp_executesql', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDistributionDirectCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'InteropCost.DistributionDirectCostDetail; InteropCost.DistributionDirectCost; InteropCost.ProductionCenter; dbo.CTNCUENTA; dbo.CTNCENCOS', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDistributionDirectCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDistributionDirectCost';
-- GO
