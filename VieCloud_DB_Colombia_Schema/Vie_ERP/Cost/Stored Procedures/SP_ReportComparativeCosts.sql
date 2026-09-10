-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-24
-- Description:	Procedimiento para el reporte del comparativo de costos por tipo
-- =============================================
CREATE PROCEDURE [Cost].[SP_ReportComparativeCosts]
	@xmlCriterias AS XML,
	@xmlFilters AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @TypeReport INT

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT 
			@TypeReport = t.x.value('TypeReport[1]','int')
		FROM @xmlFilters.nodes('/Data') t(x)

		/********************************** OBTENCION DE DATOS **********************************/

		IF @TypeReport = 1
		BEGIN
			EXEC [Cost].[SP_ReportComparativeManPowerDistribution] @xmlCriterias, @xmlFilters
		END
		ELSE IF @TypeReport = 2
		BEGIN
			EXEC [Cost].[SP_ReportComparativeDispensingDistribution] @xmlCriterias, @xmlFilters
		END
		ELSE IF @TypeReport = 3
		BEGIN
			EXEC [Cost].[SP_ReportComparativeTransferDistribution] @xmlCriterias, @xmlFilters
		END
		ELSE IF @TypeReport = 4
		BEGIN
			EXEC [Cost].[SP_ReportComparativeCostDistribution] @xmlCriterias, @xmlFilters
		END
		ELSE IF @TypeReport = 5
		BEGIN
			EXEC [Cost].[SP_ReportComparativeFixedAssetDistribution] @xmlCriterias, @xmlFilters
		END
		ELSE IF @TypeReport = 6
		BEGIN
			EXEC [Cost].[SP_ReportComparativeSales] @xmlCriterias, @xmlFilters
		END

	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) AS Message
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento orquestador del reporte comparativo de costos por tipo. Recibe criterios y filtros en formato XML, determina el tipo de reporte solicitado y delega la ejecución al subprocedimiento correspondiente según la categoría: mano de obra (tipo 1), dispensación de medicamentos (tipo 2), traslados o transferencias (tipo 3), distribución general de costos (tipo 4), activos fijos (tipo 5) o ventas (tipo 6). Permite a los usuarios de costos comparar la distribución de gastos por diferentes conceptos a través de un único punto de entrada, facilitando el análisis financiero y de gestión de costos institucionales.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportComparativeCosts';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportComparativeCosts';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Despachador que enruta la generación del reporte comparativo de costos al procedimiento especializado según el tipo de reporte solicitado.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeCosts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de filtros debe contener el nodo /Data/TypeReport con un entero válido entre 1 y 6 para que se ejecute algún reporte.; Los XML de criterios y filtros deben tener la estructura esperada por los procedimientos invocados.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeCosts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo ejecuta uno de los seis procedimientos especializados, mutuamente excluyentes según TypeReport.; Si TypeReport no está entre 1 y 6, no se ejecuta ningún reporte y no se retorna resultado (salvo error capturado).; Cualquier excepción se captura y se transforma en un resultset estándar con Code=''999'', evitando propagar el error.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeCosts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Costos comparativos; Mano de obra; Dispensación; Transferencias; Distribución de costos; Activos fijos; Ventas', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeCosts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Si ocurre cualquier error en TRY, devuelve un resultset con Code=''999'' y Message con ERROR_MESSAGE() y línea del error.; [RETURN_RESULT] (resultset): Devuelve el resultset producido por el SP delegado correspondiente al TypeReport recibido.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeCosts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TypeReport = 1 → Ejecuta SP_ReportComparativeManPowerDistribution (mano de obra).; si TypeReport = 2 → Ejecuta SP_ReportComparativeDispensingDistribution (dispensación).; si TypeReport = 3 → Ejecuta SP_ReportComparativeTransferDistribution (transferencias).; si TypeReport = 4 → Ejecuta SP_ReportComparativeCostDistribution (distribución de costos).; si TypeReport = 5 → Ejecuta SP_ReportComparativeFixedAssetDistribution (activos fijos).; si TypeReport = 6 → Ejecuta SP_ReportComparativeSales (ventas).', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeCosts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Cost.SP_ReportComparativeManPowerDistribution; Cost.SP_ReportComparativeDispensingDistribution; Cost.SP_ReportComparativeTransferDistribution; Cost.SP_ReportComparativeCostDistribution; Cost.SP_ReportComparativeFixedAssetDistribution; Cost.SP_ReportComparativeSales', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeCosts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportComparativeCosts';
-- GO
