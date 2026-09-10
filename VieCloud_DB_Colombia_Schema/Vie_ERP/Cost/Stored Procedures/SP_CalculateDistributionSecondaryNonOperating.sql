-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date:	2020-05-14
-- Description:	Calcula la distribución secundaria de otros centros de produccion no operativos
-- =============================================
CREATE PROCEDURE [Cost].[SP_CalculateDistributionSecondaryNonOperating]
	@CostDirectDistributionSecondaryId INT,
	------------------------------------------------------
	@CodeResult Int OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	/****************************************************  VARIABLES ****************************************************/

	DECLARE @Year INT,
			@Month INT,
			--------------------------------------------
			@ProductionCenterRows INT = 1,
			@ProductionCenterId INT = 0,
			@ProductionCenterCodeName VARCHAR(200), 
			@CostDistributionSecondaryId INT,
			--------------------------------------------
			@CostDirectDistributionSecondaryDetailRows INT,
			@CostDirectDistributionSecondaryDetailId INT,
			@ValueToDistribute DECIMAL(20, 4),
			@PercentageToDistribute DECIMAL(5, 2),
			--------------------------------------------
			@Message VARCHAR(MAX)

	/**************************************************** ASIGNACIONES **************************************************/

	SELECT	@Year = cdds.Year,
			@Month = cdds.Month
	FROM Cost.CostDirectDistributionSecondary cdds
	WHERE cdds.Id = @CostDirectDistributionSecondaryId

	/**************************************************** VALIDACIONES **************************************************/
	
	-- Valido que existan centros de producción no operativos por redistribuir
	IF NOT EXISTS
	(
		SELECT 1
		FROM Cost.CostDirectDistributionSecondary cdds
		JOIN Cost.CostDirectDistributionSecondaryDetail cddsd ON cdds.Id = cddsd.DirectDistributionSecondaryId
		JOIN Cost.CostProductionCenter cpc ON cddsd.ProductionCenterId = cpc.Id
		WHERE cdds.Id = @CostDirectDistributionSecondaryId AND cddsd.Value <> 0 AND cpc.CenterType <> 1
	)
	BEGIN
		SELECT	@CodeResult = 0, 
				@MessageResult = ''
		RETURN
	END

	-- Valido que los centros de producción tengan un elemento de distribución secundaria
	IF EXISTS 
	(
		SELECT 1
		FROM Cost.CostDirectDistributionSecondary cdds
		JOIN Cost.CostDirectDistributionSecondaryDetail cddsd ON cdds.Id = cddsd.DirectDistributionSecondaryId
		JOIN Cost.CostProductionCenter cpc ON cddsd.ProductionCenterId = cpc.Id
		LEFT JOIN 
		(
			SELECT ProductionCenterId, COUNT(1) Quantity
			FROM Cost.CostDistributionSecondary 
			WHERE Status = 1
			GROUP BY ProductionCenterId
		) cds ON cpc.Id = cds.ProductionCenterId
		WHERE cdds.Id = @CostDirectDistributionSecondaryId AND cddsd.Value <> 0 AND cpc.CenterType <> 1
			AND ISNULL(cds.Quantity, 0) <> 1
	)
	BEGIN
		SELECT @Message = STUFF((
			SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + cpc.Code
			FROM Cost.CostDirectDistributionSecondary cdds
			JOIN Cost.CostDirectDistributionSecondaryDetail cddsd ON cdds.Id = cddsd.DirectDistributionSecondaryId
			JOIN Cost.CostProductionCenter cpc ON cddsd.ProductionCenterId = cpc.Id
			LEFT JOIN 
			(
				SELECT ProductionCenterId, COUNT(1) Quantity
				FROM Cost.CostDistributionSecondary 
				WHERE Status = 1
				GROUP BY ProductionCenterId
			) cds ON cpc.Id = cds.ProductionCenterId
			WHERE cdds.Id = @CostDirectDistributionSecondaryId AND cddsd.Value <> 0 AND cpc.CenterType <> 1
				AND ISNULL(cds.Quantity, 0) <> 1
			FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

		SELECT	@CodeResult = 999, 
				@MessageResult = 'Los siguientes centros de producción no tienen o tienen mas de un elemento de distribución secundaria activo: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
		RETURN
	END

	-- Valido, si existen centros de producción no administrativos directos, estos ya deben estar distribuidos
	IF EXISTS
	(
		SELECT 1 
		FROM Cost.CostDirectDistributionSecondaryDetail cddsd
		JOIN Cost.CostProductionCenter cpc ON cddsd.ProductionCenterId = cpc.Id
		JOIN Cost.CostDistributionSecondary cds ON cpc.Id = cds.ProductionCenterId AND cds.Status = 1
		LEFT JOIN Cost.CostDistributionSecondaryBase cdsb ON cds.Id = cdsb.DistributionSecondaryId
		LEFT JOIN Cost.CostDirectDistributionSecondary cddst 
			ON cds.Id = cddst.DistributionSecondaryId AND cddst.Year = @Year AND cddst.Month = @Month AND cddst.Status = 2
		WHERE cddsd.DirectDistributionSecondaryId = @CostDirectDistributionSecondaryId AND cddsd.Value <> 0 
			AND cpc.CenterType <> 1 AND ISNULL(cdsb.DistributionType, 1) = 1
			AND cddst.Id IS NULL
	)
	BEGIN
		SELECT @Message = STUFF((
			SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + cpc.Code
			FROM Cost.CostDirectDistributionSecondaryDetail cddsd
			JOIN Cost.CostProductionCenter cpc ON cddsd.ProductionCenterId = cpc.Id
			JOIN Cost.CostDistributionSecondary cds ON cpc.Id = cds.ProductionCenterId AND cds.Status = 1
			LEFT JOIN Cost.CostDistributionSecondaryBase cdsb ON cds.Id = cdsb.DistributionSecondaryId
			LEFT JOIN Cost.CostDirectDistributionSecondary cddst 
				ON cds.Id = cddst.DistributionSecondaryId AND cddst.Year = @Year AND cddst.Month = @Month AND cddst.Status = 2
			WHERE cddsd.DirectDistributionSecondaryId = @CostDirectDistributionSecondaryId AND cddsd.Value <> 0 
				AND cpc.CenterType <> 1 AND ISNULL(cdsb.DistributionType, 1) = 1
				AND cddst.Id IS NULL
			FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

		SELECT	@CodeResult = 999, 
				@MessageResult = 'Se debe distribuir primero los siguientes centros de producción: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
		RETURN
	END

	/**************************************************** PROCESO **************************************************/

	WHILE @ProductionCenterRows > 0
	BEGIN
		SELECT TOP 1 
			@ProductionCenterId = cpc.Id,
			@ProductionCenterCodeName = CONCAT(cpc.Code, ' - ', cpc.Name),
			@CostDistributionSecondaryId = cds.Id,
			--------------------------------------------
			@CostDirectDistributionSecondaryDetailRows = 1,
			@CostDirectDistributionSecondaryDetailId = 0
		FROM Cost.CostDirectDistributionSecondary cdds
		JOIN Cost.CostDirectDistributionSecondaryDetail cddsd ON cdds.Id = cddsd.DirectDistributionSecondaryId
		JOIN Cost.CostProductionCenter cpc ON cddsd.ProductionCenterId = cpc.Id
		JOIN Cost.CostDistributionSecondary cds ON cpc.Id = cds.ProductionCenterId AND cds.Status = 1
		WHERE cdds.Id = @CostDirectDistributionSecondaryId AND cddsd.Value <> 0 AND cpc.CenterType <> 1
			AND cpc.Id > @ProductionCenterId
		ORDER BY cpc.Id

		SET @ProductionCenterRows = @@ROWCOUNT
		IF @ProductionCenterRows = 0 
		BEGIN
			BREAK
		END

		-----------------------------------------------------------------------------

		WHILE @CostDirectDistributionSecondaryDetailRows > 0
		BEGIN
			SELECT TOP 1 
				@CostDirectDistributionSecondaryDetailId = cddsd.Id,
				@ValueToDistribute = cddsd.Value,
				@PercentageToDistribute = cddsd.Percentage
			FROM Cost.CostDirectDistributionSecondaryDetail cddsd
			WHERE cddsd.DirectDistributionSecondaryId = @CostDirectDistributionSecondaryId
				AND cddsd.ProductionCenterId = @ProductionCenterId
				 AND cddsd.Value <> 0
				AND cddsd.Id > @CostDirectDistributionSecondaryDetailId
			ORDER BY cddsd.Id

			SET @CostDirectDistributionSecondaryDetailRows = @@ROWCOUNT
			IF @CostDirectDistributionSecondaryDetailRows = 0 
			BEGIN
				BREAK
			END

			-----------------------------------------------------------------------------

			INSERT INTO Cost.CostDirectDistributionSecondaryDetailRedistribution
			(
				DirectDistributionSecondaryDetailId, ProductionCenterId, Percentage, Value
			)
			SELECT @CostDirectDistributionSecondaryDetailId, ProductionCenterId, SUM(Percentage), SUM(Value)
			FROM [Cost].[GetCalculateDistributionSecondary](@Year, @Month, @CostDistributionSecondaryId, @ValueToDistribute, @PercentageToDistribute)
			WHERE Value <> 0
			GROUP BY ProductionCenterId
		END

		SET @Message = ISNULL(@Message, '') + CHAR(13) + CHAR(10) + @ProductionCenterCodeName
	END

	/**************************************************** VALIDACIONES **************************************************/

	-- Valido que los centros de producción se hayan distribuido completamente
	IF EXISTS 
	(
		SELECT 1
		FROM Cost.CostDirectDistributionSecondary cdds
		JOIN Cost.CostDirectDistributionSecondaryDetail cddsd ON cdds.Id = cddsd.DirectDistributionSecondaryId
		JOIN Cost.CostProductionCenter cpc ON cddsd.ProductionCenterId = cpc.Id
		LEFT JOIN 
		(
			SELECT DirectDistributionSecondaryDetailId, SUM(Value) Value
			FROM Cost.CostDirectDistributionSecondaryDetailRedistribution
			GROUP BY DirectDistributionSecondaryDetailId
		) cddsdr ON cddsd.Id = cddsdr.DirectDistributionSecondaryDetailId
		WHERE cdds.Id = @CostDirectDistributionSecondaryId AND cddsd.Value <> 0 AND cpc.CenterType <> 1
			AND cddsd.Value <> ISNULL(cddsdr.Value, 0)
	)
	BEGIN
		SELECT @Message = STUFF((
			SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + cpc.Code
			FROM Cost.CostDirectDistributionSecondary cdds
			JOIN Cost.CostDirectDistributionSecondaryDetail cddsd ON cdds.Id = cddsd.DirectDistributionSecondaryId
			JOIN Cost.CostProductionCenter cpc ON cddsd.ProductionCenterId = cpc.Id
			LEFT JOIN 
			(
				SELECT DirectDistributionSecondaryDetailId, SUM(Value) Value
				FROM Cost.CostDirectDistributionSecondaryDetailRedistribution
				GROUP BY DirectDistributionSecondaryDetailId
			) cddsdr ON cddsd.Id = cddsdr.DirectDistributionSecondaryDetailId
			WHERE cdds.Id = @CostDirectDistributionSecondaryId AND cddsd.Value <> 0 AND cpc.CenterType <> 1
				AND cddsd.Value <> ISNULL(cddsdr.Value, 0)
			FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

		SELECT	@CodeResult = 999, 
				@MessageResult = 'No se pudo realizar la redistribución completa de los siguientes centros de producción: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
		RETURN
	END
	
	/***************************************************** RESULTADO *****************************************************/

	SELECT	@CodeResult = 0, 
			@MessageResult = 'Se redistribuyeron los siguientes centros de producción no operativos: ' + ISNULL(@Message, '')

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula y ejecuta la distribución secundaria de costos para los centros de producción no operativos (administrativos o de apoyo), redistribuyendo los valores acumulados en la distribución directa secundaria hacia otros centros de producción del período indicado (mes/año). Valida que existan centros no operativos con valores pendientes de redistribuir, que cada centro tenga exactamente un elemento de distribución secundaria activo, y que los centros que distribuyen por base directa ya hayan sido procesados previamente. Utiliza las tablas de distribución directa secundaria (CostDirectDistributionSecondary y su detalle), los centros de producción (CostProductionCenter) y las bases de distribución secundaria (CostDistributionSecondary y CostDistributionSecondaryBase) para calcular los porcentajes e importes a repartir. Retorna un código y mensaje de resultado indicando éxito o el listado de centros de producción con inconsistencias que impiden completar el proceso de costeo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CalculateDistributionSecondaryNonOperating';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CalculateDistributionSecondaryNonOperating';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Redistribuye los costos de centros de producción no operativos hacia otros centros aplicando bases de distribución secundaria activas, generando los registros de redistribución por detalle.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateDistributionSecondaryNonOperating';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro padre de distribución secundaria directa con año y mes asociados al Id recibido.; Deben existir detalles con Value distinto de 0 cuyo centro de producción no sea de tipo 1 (no administrativo/operativo).; Cada centro de producción no operativo a redistribuir debe tener exactamente un elemento de distribución secundaria activo (Status=1).; Los centros de producción referenciados con DistributionType=1 deben estar previamente distribuidos (existir un CostDirectDistributionSecondary con Status=2 para el mismo año y mes).', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateDistributionSecondaryNonOperating';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan centros de producción cuyo CenterType es distinto de 1 (no operativos/no administrativos).; Solo se consideran detalles con Value<>0.; Cada centro debe tener exactamente una base de distribución secundaria activa (Status=1) para ser redistribuido.; La redistribución debe ser exhaustiva: la suma de los valores redistribuidos por detalle debe ser igual al Value original del detalle.; Los centros con DistributionType=1 dependientes deben estar previamente distribuidos (Status=2) para el mismo año/mes antes de redistribuirse.; Las filas insertadas en la tabla de redistribución se agregan por ProductionCenterId (SUM de Percentage y Value) y omiten valores 0.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateDistributionSecondaryNonOperating';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Distribución secundaria de costos; Centros de producción no operativos; Redistribución de costos; Bases de distribución secundaria; Periodo contable (año/mes); Tipo de centro (CenterType); Tipo de distribución (DistributionType); Porcentaje y valor a distribuir', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateDistributionSecondaryNonOperating';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Cost.CostDirectDistributionSecondaryDetailRedistribution: Por cada detalle con Value<>0 de un centro con CenterType<>1, inserta filas agregadas (SUM Percentage, SUM Value) por ProductionCenterId obtenidas de Cost.GetCalculateDistributionSecondary(Year, Month, CostDistributionSecondaryId, ValueToDistribute, PercentageToDistribute) donde Value<>0.; [RETURN_RESULT] (resultset): Si no existen detalles a redistribuir (no hay filas con Value<>0 y CenterType<>1), retorna CodeResult=0 y MessageResult vacío.; [RETURN_RESULT] (resultset): Si algún centro no tiene exactamente un elemento de distribución secundaria activo, retorna CodeResult=999 con mensaje listando los códigos de centro afectados.; [RETURN_RESULT] (resultset): Si existen centros con DistributionType=1 sin distribución previa (Status=2) en el mismo año/mes, retorna CodeResult=999 con mensaje ''Se debe distribuir primero los siguientes centros de producción''.; [RETURN_RESULT] (resultset): Tras el proceso, si la suma redistribuida no coincide con el Value original de algún detalle, retorna CodeResult=999 con mensaje ''No se pudo realizar la redistribución completa de los siguientes centros de producción''.; [RETURN_RESULT] (resultset): Si todo finaliza correctamente, retorna CodeResult=0 con mensaje listando los centros de producción no operativos redistribuidos (código y nombre).', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateDistributionSecondaryNonOperating';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existen detalles con Value<>0 y CenterType<>1 para el Id de distribución directa secundaria. → Termina sin error retornando CodeResult=0 y mensaje vacío. else Continúa con las validaciones posteriores.; si Algún centro de producción no operativo no tiene exactamente un (1) elemento de distribución secundaria activo (Status=1). → Aborta con CodeResult=999 listando los códigos de centro problemáticos.; si Existe un centro con DistributionType=1 (o NULL) cuya distribución previa con Status=2 para el mismo año/mes no existe. → Aborta con CodeResult=999 indicando que primero deben distribuirse esos centros.; si Para algún detalle, el Value original difiere de la suma de Value redistribuidos. → Aborta con CodeResult=999 indicando redistribución incompleta. else Retorna CodeResult=0 con la lista de centros redistribuidos.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateDistributionSecondaryNonOperating';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Cost.GetCalculateDistributionSecondary', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateDistributionSecondaryNonOperating';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostDirectDistributionSecondary; Cost.CostDirectDistributionSecondaryDetail; Cost.CostProductionCenter; Cost.CostDistributionSecondary; Cost.CostDistributionSecondaryBase; Cost.CostDirectDistributionSecondaryDetailRedistribution; Cost.GetCalculateDistributionSecondary', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateDistributionSecondaryNonOperating';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CalculateDistributionSecondaryNonOperating';
-- GO
