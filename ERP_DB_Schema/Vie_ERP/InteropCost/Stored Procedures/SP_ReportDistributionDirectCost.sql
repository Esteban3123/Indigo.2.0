
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