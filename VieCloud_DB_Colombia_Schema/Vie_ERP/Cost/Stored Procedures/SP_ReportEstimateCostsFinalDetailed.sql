-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-24
-- Description:	Procedimiento para el reporte de estimacion de costos tipo final detallado
-- =============================================
CREATE PROCEDURE [Cost].[SP_ReportEstimateCostsFinalDetailed]
	@xmlCriterias AS XML,
	@xmlFilters AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE -- CRITERIOS --
			@YearStart INT,
			@MonthStart INT,
			@YearEnd INT,
			@MonthEnd INT,
			@TypeReport INT,
			@GroupBy INT,
			@Level INT,
			-- FILTROS --
			@CenterType VARCHAR(MAX),
			@Categories VARCHAR(MAX),
			@ProductionCenters VARCHAR(MAX),
			@DetailType VARCHAR(MAX),
			-------------
			@FilterByCategories BIT = 0,
			@FilterByProductionCenters BIT = 0

	DECLARE @Table_CenterType AS TABLE(CenterType INT)
	DECLARE @Table_Categories AS TABLE(Id INT)
	DECLARE @Table_ProductionCenters AS TABLE(Id INT)
	DECLARE @Table_DetailType AS TABLE(DetailType INT)

	DECLARE @Table_Result AS TABLE
	(
		OrganizationalStructureParentId INT,
		OrganizationalStructureParentCode VARCHAR(20), 
		OrganizationalStructureParentName VARCHAR(500),
		OrganizationalStructureCode VARCHAR(20), 
		OrganizationalStructureName VARCHAR(200),
		OrganizationalStructureLevel INT,
		CategoryCode VARCHAR(20),
		CategoryName VARCHAR(200),
		ProductionCenterId INT,
		ProductionCenterCode VARCHAR(20),
		ProductionCenterName VARCHAR(200),
		CenterType TINYINT,
		AccountId INT,
		AccountNumber VARCHAR(50),
		AccountName VARCHAR(200),
		ThirdPartyId INT,
		ThirdPartyNit VARCHAR(20),
		ThirdPartyName VARCHAR(500),
		HomologationType TINYINT,
		Value DECIMAL(20,4)
	)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT	@YearStart = t.x.value('YearStart[1]','int'),
				@MonthStart = t.x.value('MonthStart[1]','int'),
				@YearEnd = t.x.value('YearEnd[1]','int'),
				@MonthEnd = t.x.value('MonthEnd[1]','int'),
				@TypeReport = t.x.value('TypeReport[1]','int'),
				@GroupBy = t.x.value('GroupBy[1]','int'),
				@Level = t.x.value('Level[1]','int')
		FROM @xmlCriterias.nodes('/Data') t(x)

		--Se obtienen los datos de los filtros
		SELECT	@CenterType = t.x.value('CenterType[1]','varchar(max)'),
				@Categories = t.x.value('Categories[1]','varchar(max)'),
				@ProductionCenters = t.x.value('ProductionCenters[1]','varchar(max)'),
				@DetailType = t.x.value('DetailType[1]','varchar(max)')
		FROM @xmlFilters.nodes('/Data') t(x)

		--Se Cargan las listas enviadas

		INSERT INTO @Table_CenterType
			SELECT CAST(Data AS INT) Data 
			FROM dbo.Split(@CenterType, ',')

		IF @Categories <> ''
		BEGIN
			SET @FilterByCategories = 1

			INSERT INTO @Table_Categories
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@Categories, ',')
		END

		IF @ProductionCenters <> ''
		BEGIN
			SET @FilterByProductionCenters = 1

			INSERT INTO @Table_ProductionCenters
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@ProductionCenters, ',')
		END

		INSERT INTO @Table_DetailType
			SELECT CAST(Data AS INT) Data 
			FROM dbo.Split(@DetailType, ',')

		/********************************** OBTENCION DE DATOS **********************************/

		IF @TypeReport = 6
		BEGIN
			INSERT INTO @Table_Result
				SELECT	cosc.ParentId OrganizationalStructureParentId, NULL, NULL,
						cosc.Code OrganizationalStructureCode, 
						cosc.Name OrganizationalStructureName,
						cosc.Level OrganizationalStructureLevel,
						cpcc.Code CategoryCode,
						cpcc.Name CategoryName,
						cpc.Id ProductionCenterId,
						cpc.Code ProductionCenterCode,
						cpc.Name ProductionCenterName,
						cpc.CenterType,
						ma.Id AccountId,
						ma.Number AccountNumber,
						ma.Name AccountName,
						tp.Id ThirdPartyId,
						tp.Nit ThirdPartyNit,
						tp.Name ThirdPartyName,
						dt.DetailType,
						SUM(IIF(mac.Nature = 1, (glb.DebitValue - glb.CreditValue), (glb.CreditValue - glb.DebitValue))) Value
				FROM Cost.CostProductionCenter cpc WITH (NOLOCK)
				JOIN @Table_CenterType ct ON cpc.CenterType = ct.CenterType
				JOIN Cost.CostOrganizationalStructureOfCosts cosc WITH (NOLOCK) ON cpc.OrganizationalStructureOfCostId = cosc.Id
				JOIN Cost.CostProductionCenterHomologation cpch WITH (NOLOCK) ON cpc.Id = cpch.ProductionCenterId
				JOIN @Table_DetailType dt ON cpch.HomologationType = dt.DetailType
				JOIN Cost.CostProductionCenterCostCenter cpccc ON cpch.ProductionCenterId = cpccc.ProductionCenterId
				JOIN GeneralLedger.GeneralLedgerBalance glb WITH (NOLOCK) ON cpch.AccountOriginId = glb.IdMainAccount AND cpccc.CostCenterId = glb.IdCostCenter				
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON glb.IdMainAccount = ma.Id
				JOIN GeneralLedger.MainAccountClasses mac WITH (NOLOCK) ON ma.IdAccountClass = mac.Id
				LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON glb.IdThirdParty = tp.Id
				LEFT JOIN Cost.CostProductionCenterCategory cpcc WITH (NOLOCK) ON cpc.CategoryId = cpcc.Id
				LEFT JOIN @Table_Categories c ON cpcc.Id = c.Id
				LEFT JOIN @Table_ProductionCenters pc ON cpc.Id = pc.Id
				WHERE (
						(glb.Year > @YearStart OR (glb.Year = @YearStart AND glb.Month >= @MonthStart))
						AND
						(glb.Year < @YearEnd OR (glb.Year = @YearEnd AND glb.Month <= @MonthEnd))
					)
					AND (@FilterByCategories = 0 OR c.Id IS NOT NULL)
					AND (@FilterByProductionCenters = 0 OR pc.Id IS NOT NULL)
					AND (@GroupBy <> 3 OR cosc.Level >= @Level)
				GROUP BY	cosc.ParentId, cosc.Code, cosc.Name, cosc.Level,
							cpcc.Code, cpcc.Name, 
							cpc.Id, cpc.Code, cpc.Name, cpc.CenterType,
							ma.Id, ma.Number, ma.Name,
							tp.Id, tp.Nit, tp.Name,
							dt.DetailType

			/*********************************************  DISTRIBUCIÓN SECUNDARIA ORIGEN *********************************************/

			INSERT INTO @Table_Result
				SELECT	scosc.ParentId OrganizationalStructureParentId, NULL, NULL,
						scosc.Code OrganizationalStructureCode, 
						scosc.Name OrganizationalStructureName,
						scosc.Level OrganizationalStructureLevel,
						scpcc.Code CategoryCode,
						scpcc.Name CategoryName,
						scpc.Id ProductionCenterId,
						scpc.Code ProductionCenterCode,
						scpc.Name ProductionCenterName,
						scpc.CenterType,
						tcpc.Id AccountId,
						tcpc.Code AccountNumber,
						tcpc.Name AccountName,
						tcpc.CenterType ThirdPartyId,
						NULL ThirdPartyNit,
						CASE tcpc.CenterType
							WHEN 1 THEN 'Operativo'
							WHEN 2 THEN 'Administrativo'
							WHEN 3 THEN 'Logístico'
							ELSE 'N/A'
						END ThirdPartyName,
						dt.DetailType HomologationType,
						ISNULL(cddsdr.Value, cddsd.Value) * -1 Value
				FROM Cost.CostProductionCenter scpc WITH (NOLOCK)
				JOIN @Table_CenterType sct ON scpc.CenterType = sct.CenterType
				JOIN Cost.CostOrganizationalStructureOfCosts scosc WITH (NOLOCK) ON scpc.OrganizationalStructureOfCostId = scosc.Id
				--------------------------------------------------------------------------------------------------------------------
				JOIN Cost.CostDistributionSecondary cds WITH (NOLOCK) ON scpc.Id = cds.ProductionCenterId
				JOIN Cost.CostDirectDistributionSecondary cdds WITH (NOLOCK) ON cds.Id = cdds.DistributionSecondaryId
				JOIN Cost.CostDirectDistributionSecondaryDetail cddsd WITH (NOLOCK) ON cdds.Id = cddsd.DirectDistributionSecondaryId
				LEFT JOIN Cost.CostDirectDistributionSecondaryDetailRedistribution cddsdr WITH (NOLOCK) ON cddsd.Id = cddsdr.DirectDistributionSecondaryDetailId
				--------------------------------------------------------------------------------------------------------------------
				LEFT JOIN Cost.CostProductionCenter tcpc WITH (NOLOCK) ON ISNULL(cddsdr.ProductionCenterId, cddsd.ProductionCenterId) = tcpc.Id
				--------------------------------------------------------------------------------------------------------------------
				LEFT JOIN Cost.CostProductionCenterCategory scpcc WITH (NOLOCK) ON scpc.CategoryId = scpcc.Id
				LEFT JOIN @Table_DetailType dt ON 9 = dt.DetailType
				LEFT JOIN @Table_Categories sc ON scpcc.Id = sc.Id
				LEFT JOIN @Table_ProductionCenters spc ON scpc.Id = spc.Id
				WHERE cdds.Status = 2 AND
					(
						(cdds.Year > @YearStart OR (cdds.Year = @YearStart AND cdds.Month >= @MonthStart))
						AND
						(cdds.Year < @YearEnd OR (cdds.Year = @YearEnd AND cdds.Month <= @MonthEnd))
					)
					AND dt.DetailType IS NOT NULL
					AND (@FilterByCategories = 0 OR sc.Id IS NOT NULL)
					AND (@FilterByProductionCenters = 0 OR spc.Id IS NOT NULL)
					AND (@GroupBy <> 3 OR scosc.Level >= @Level)

			/********************************************* DISTRIBUCIÓN SECUNDARIA DESTINO *********************************************/

			INSERT INTO @Table_Result
				SELECT	tcosc.ParentId OrganizationalStructureParentId, NULL, NULL,
						tcosc.Code OrganizationalStructureCode, 
						tcosc.Name OrganizationalStructureName,
						tcosc.Level OrganizationalStructureLevel,
						tcpcc.Code CategoryCode,
						tcpcc.Name CategoryName,
						tcpc.Id ProductionCenterId,
						tcpc.Code ProductionCenterCode,
						tcpc.Name ProductionCenterName,
						tcpc.CenterType,
						scpc.Id AccountId,
						scpc.Code AccountNumber,
						scpc.Name AccountName,
						scpc.CenterType ThirdPartyId,
						NULL ThirdPartyNit,
						CASE scpc.CenterType
							WHEN 1 THEN 'Operativo'
							WHEN 2 THEN 'Administrativo'
							WHEN 3 THEN 'Logístico'
							ELSE 'N/A'
						END ThirdPartyName,
						dt.DetailType HomologationType,
						ISNULL(cddsdr.Value, cddsd.Value) Value
				FROM Cost.CostProductionCenter scpc WITH (NOLOCK)
				--------------------------------------------------------------------------------------------------------------------
				JOIN Cost.CostDistributionSecondary cds WITH (NOLOCK) ON scpc.Id = cds.ProductionCenterId
				JOIN Cost.CostDirectDistributionSecondary cdds WITH (NOLOCK) ON cds.Id = cdds.DistributionSecondaryId
				JOIN Cost.CostDirectDistributionSecondaryDetail cddsd WITH (NOLOCK) ON cdds.Id = cddsd.DirectDistributionSecondaryId
				LEFT JOIN Cost.CostDirectDistributionSecondaryDetailRedistribution cddsdr WITH (NOLOCK) ON cddsd.Id = cddsdr.DirectDistributionSecondaryDetailId
				--------------------------------------------------------------------------------------------------------------------
				LEFT JOIN Cost.CostProductionCenter tcpc WITH (NOLOCK) ON ISNULL(cddsdr.ProductionCenterId, cddsd.ProductionCenterId) = tcpc.Id
				LEFT JOIN @Table_CenterType tct ON tcpc.CenterType = tct.CenterType
				LEFT JOIN Cost.CostOrganizationalStructureOfCosts tcosc WITH (NOLOCK) ON tcpc.OrganizationalStructureOfCostId = tcosc.Id
				--------------------------------------------------------------------------------------------------------------------
				LEFT JOIN Cost.CostProductionCenterCategory tcpcc WITH (NOLOCK) ON tcpc.CategoryId = tcpcc.Id
				LEFT JOIN @Table_DetailType dt ON 9 = dt.DetailType
				LEFT JOIN @Table_Categories tc ON tcpcc.Id = tc.Id
				LEFT JOIN @Table_ProductionCenters tpc ON tcpc.Id = tpc.Id
				WHERE cdds.Status = 2 AND
					(
						(cdds.Year > @YearStart OR (cdds.Year = @YearStart AND cdds.Month >= @MonthStart))
						AND
						(cdds.Year < @YearEnd OR (cdds.Year = @YearEnd AND cdds.Month <= @MonthEnd))
					)
					AND dt.DetailType IS NOT NULL
					AND (@FilterByCategories = 0 OR tc.Id IS NOT NULL)
					AND (@FilterByProductionCenters = 0 OR tpc.Id IS NOT NULL)
					AND (@GroupBy <> 3 OR tcosc.Level >= @Level)

			/******************************************  ASIGNACIÓN ESTRUCTURA ORGANIZACIONAL ******************************************/

			IF @GroupBy = 3
			BEGIN
				DECLARE @LevelRows INT = 1,
						@CurrentLevel INT,
						@LevelMax INT = @Level

				WHILE @LevelRows > 0
				BEGIN
					SELECT TOP 1
						@CurrentLevel = OrganizationalStructureLevel
					FROM @Table_Result
					WHERE OrganizationalStructureLevel > @Level
					ORDER BY OrganizationalStructureLevel DESC

					SET @LevelRows = @@ROWCOUNT
					IF @LevelRows = 0 OR @CurrentLevel = @LevelMax
					BEGIN
						BREAK
					END

					UPDATE r
						SET r.OrganizationalStructureParentId = cosc.ParentId,
							r.OrganizationalStructureCode = cosc.Code,
							r.OrganizationalStructureName = cosc.Name,
							r.OrganizationalStructureLevel = cosc.Level
					FROM @Table_Result r
					JOIN Cost.CostOrganizationalStructureOfCosts cosc WITH (NOLOCK) ON r.OrganizationalStructureParentId = cosc.Id
					WHERE r.OrganizationalStructureLevel = @CurrentLevel

					SET @LevelMax = @CurrentLevel
				END

				UPDATE r
					SET r.OrganizationalStructureParentCode = cosc.Code,
						r.OrganizationalStructureParentName = cosc.Name
				FROM @Table_Result r
				JOIN Cost.CostOrganizationalStructureOfCosts cosc WITH (NOLOCK) ON r.OrganizationalStructureParentId = cosc.Id
			END
		END
	END TRY
	BEGIN CATCH	
		INSERT INTO @Table_Result 
		(
			ProductionCenterCode, ProductionCenterName, 
			CategoryCode, CategoryName,
			OrganizationalStructureCode, OrganizationalStructureName
		)
		SELECT	'999', ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)),
				'999', ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)),
				'999', ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20))
	END CATCH

	SELECT 
		r.OrganizationalStructureParentCode ParentCode, 
		r.OrganizationalStructureParentName ParentName,
		CASE @GroupBy
			WHEN 1 THEN r.ProductionCenterCode
			WHEN 2 THEN r.CategoryCode
			WHEN 3 THEN r.OrganizationalStructureCode
		END Code, 
		CASE @GroupBy
			WHEN 1 THEN r.ProductionCenterName
			WHEN 2 THEN r.CategoryName
			WHEN 3 THEN r.OrganizationalStructureName
		END Name,
		r.CenterType,
		CASE r.CenterType
			WHEN 1 THEN 'Operativo'
			WHEN 2 THEN 'Administrativo'
			WHEN 3 THEN 'Logístico'
			ELSE 'N/A'
		END CenterTypeName,
		r.AccountNumber, r.AccountName,
		r.ThirdPartyNit, r.ThirdPartyName,
		CASE r.HomologationType
			WHEN 4 THEN 1
			WHEN 1 THEN 2
			WHEN 5 THEN 3
			WHEN 2 THEN 4
			WHEN 3 THEN 5
			WHEN 9 THEN 6
			WHEN 6 THEN 7
			ELSE 0
		END HomologationType,
		CASE r.HomologationType
			WHEN 1 THEN 'Mano de Obra'
			WHEN 2 THEN 'Dispensación'
			WHEN 3 THEN 'Consumo'
			WHEN 4 THEN 'Gastos Generales'
			WHEN 5 THEN 'Activos Fijos'
			WHEN 6 THEN 'Ventas'
			WHEN 9 THEN 'Distribución Secundaria'
			ELSE 'N/A'
		END HomologationTypeName,
		SUM(r.Value) Value
	FROM @Table_Result r
	GROUP BY
		r.OrganizationalStructureParentCode, r.OrganizationalStructureParentName,
		CASE @GroupBy
			WHEN 1 THEN r.ProductionCenterCode
			WHEN 2 THEN r.CategoryCode
			WHEN 3 THEN r.OrganizationalStructureCode
		END, 
		CASE @GroupBy
			WHEN 1 THEN r.ProductionCenterName
			WHEN 2 THEN r.CategoryName
			WHEN 3 THEN r.OrganizationalStructureName
		END,
		r.CenterType,
		r.AccountNumber, r.AccountName,
		r.ThirdPartyNit, r.ThirdPartyName,
		r.HomologationType
	HAVING SUM(r.Value) <> 0
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el reporte detallado de costos estimados finales por centro de producción, integrando saldos contables del libro mayor con la estructura organizacional de costos, homologaciones de cuentas y centros de costo. Consolida múltiples tipos de distribución de costos (primaria, secundaria e intermedia, incluyendo origen y destino) según el tipo de informe solicitado, el rango de período (año/mes inicio y fin), el tipo de centro, categoría y nivel jerárquico. Utiliza criterios y filtros enviados en formato XML para parametrizar la consulta, y combina datos de terceros (proveedores, aseguradoras) asociados a los movimientos contables para ofrecer un desglose completo por cuenta, centro de costo y tipo de detalle de homologación. Este procedimiento es la base del módulo de costos para la toma de decisiones financieras y de gestión hospitalaria.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportEstimateCostsFinalDetailed';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportEstimateCostsFinalDetailed';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte detallado final de estimación de costos, consolidando saldos contables por centro de producción, categoría y estructura organizacional, incluyendo movimientos de distribución secundaria (origen y destino).', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsFinalDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los XML de criterios y filtros deben contener los nodos esperados (YearStart, MonthStart, YearEnd, MonthEnd, TypeReport, GroupBy, Level / CenterType, Categories, ProductionCenters, DetailType).; El filtro CenterType y DetailType deben venir con al menos un valor entero separado por comas para que existan resultados.; Solo se procesa el reporte cuando TypeReport = 6.; El rango de período (Año/Mes inicio – Año/Mes fin) debe ser coherente para acotar GeneralLedgerBalance y CostDirectDistributionSecondary.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsFinalDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El reporte solo opera para TypeReport = 6; otros valores no producen datos.; La distribución secundaria se considera únicamente cuando su Status = 2 (estado válido/aprobado).; El valor en distribución secundaria origen siempre se registra negativo y en destino siempre positivo, manteniendo neto cero entre ambos.; Si existe redistribución (CostDirectDistributionSecondaryDetailRedistribution), prevalece su Value y ProductionCenterId sobre el detalle base.; El signo del saldo contable depende de la naturaleza de la clase de la cuenta (1=débito, otros=crédito).; El resultado final excluye agrupaciones cuyo total sea 0.; Los errores no abortan el procedimiento: se devuelven como una fila marcadora con código ''999''.; Las consultas usan WITH (NOLOCK) en todas las tablas físicas, asumiendo lecturas sucias aceptables para el reporte.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsFinalDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @Table_Result: Cuando TypeReport = 6, inserta los saldos contables agregados desde GeneralLedgerBalance, calculando Value = SUM(IIF(MainAccountClasses.Nature = 1, Debito-Credito, Credito-Debito)), filtrando por período, CenterType, DetailType y opcionalmente Categorías y Centros de Producción.; [INSERT] @Table_Result: Cuando TypeReport = 6 y existe DetailType=9, inserta filas de distribución secundaria ORIGEN (valor negativo: ISNULL(Redistribution.Value, Detail.Value) * -1) solo cuando CostDirectDistributionSecondary.Status = 2 y dentro del período.; [INSERT] @Table_Result: Cuando TypeReport = 6 y existe DetailType=9, inserta filas de distribución secundaria DESTINO (valor positivo) solo cuando CostDirectDistributionSecondary.Status = 2 y dentro del período.; [UPDATE] @Table_Result: Cuando GroupBy = 3, escala iterativamente la estructura organizacional reasignando ParentId/Code/Name/Level subiendo niveles hasta alcanzar el Level solicitado, y luego asigna ParentCode y ParentName desde CostOrganizationalStructureOfCosts.; [INSERT] @Table_Result: En caso de error capturado por CATCH, inserta una fila con código ''999'' y el mensaje y línea del error en los campos descriptivos.; [RETURN_RESULT] RESULT_SET: Devuelve el resultado agrupado según GroupBy (1=ProductionCenter, 2=Category, 3=OrganizationalStructure), traduciendo CenterType y HomologationType a nombres de dominio, y excluye filas cuya SUMA de Value sea 0 (HAVING SUM(Value) <> 0).', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsFinalDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Categories <> '''' → Activa FilterByCategories=1 y carga la lista de categorías para filtrar resultados. else No se aplica filtro por categorías.; si @ProductionCenters <> '''' → Activa FilterByProductionCenters=1 y carga la lista de centros de producción para filtrar resultados. else No se aplica filtro por centros de producción.; si @TypeReport = 6 → Ejecuta la carga de saldos contables y de distribución secundaria origen/destino. else No produce datos de costos.; si @GroupBy = 3 → Ejecuta el bucle de escalamiento jerárquico de la estructura organizacional hasta el Level solicitado. else No reasigna jerarquía organizacional.; si @GroupBy = 3 (en WHERE de cargas) → Filtra los registros donde el nivel de la estructura organizacional sea >= @Level.; si MainAccountClasses.Nature = 1 → Calcula el Valor como Débito - Crédito (cuentas de naturaleza débito). else Calcula el Valor como Crédito - Débito (cuentas de naturaleza crédito).; si CenterType IN (1,2,3) → Traduce a ''Operativo'' (1), ''Administrativo'' (2) o ''Logístico'' (3). else Etiqueta como ''N/A''.; si HomologationType original (4,1,5,2,3,9,6) → Reordena a códigos de salida (1=Mano de Obra, 2=Dispensación, 3=Consumo, 4=Gastos Generales, 5=Activos Fijos, 6=Ventas, 7=Distribución Secundaria → equivalencia interna).', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsFinalDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsFinalDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostProductionCenter; Cost.CostOrganizationalStructureOfCosts; Cost.CostProductionCenterHomologation; Cost.CostProductionCenterCostCenter; GeneralLedger.GeneralLedgerBalance; GeneralLedger.MainAccounts; GeneralLedger.MainAccountClasses; Common.ThirdParty; Cost.CostProductionCenterCategory; Cost.CostDistributionSecondary; Cost.CostDirectDistributionSecondary; Cost.CostDirectDistributionSecondaryDetail; Cost.CostDirectDistributionSecondaryDetailRedistribution', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsFinalDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCostsFinalDetailed';
-- GO
