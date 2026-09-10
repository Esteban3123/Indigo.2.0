-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-09-02
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una distribución secundaria
-- =============================================
CREATE PROCEDURE [Cost].[SP_SaveDistributionSecondary]
    @DistributionSecondaryXml AS XML,
	@DistributionSecondaryDetailForDeleteXml AS XML,
	@CostLogisticsProductionCenterDetail AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeMessage INT,
			@Message VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC Cost.SP_SaveDistributionSecondary_Output 
		@DistributionSecondaryXml, 
		@DistributionSecondaryDetailForDeleteXml, 
		@CostLogisticsProductionCenterDetail,
		@CodeUser, 
		-----------------------------------------
		@CodeMessage OUTPUT, 
		@Message OUTPUT, 
		@Id OUTPUT, 
		@Code OUTPUT

	SELECT 
		@CodeMessage AS CodeMessage, 
		@Message AS Message, 
		@Id as Id, 
		@Code as Code
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que permite guardar, actualizar o confirmar una distribución secundaria de costos en el módulo de costos logísticos. Recibe como parámetros XMLs con el encabezado de la distribución secundaria, el detalle de registros a eliminar y el detalle del centro de producción logístico, junto con el código del usuario que ejecuta la operación. Delega la lógica principal al procedimiento Cost.SP_SaveDistributionSecondary_Output y retorna un código de mensaje, descripción del resultado, el identificador y el código del registro afectado. Se usa para gestionar la distribución secundaria de costos entre centros de producción o unidades funcionales dentro del proceso de costeo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_SaveDistributionSecondary';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_SaveDistributionSecondary';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que delega en otro procedimiento la persistencia (guardar/actualizar/confirmar) de una distribución secundaria de costos y devuelve el resultado como conjunto de filas.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDistributionSecondary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se deben proveer los XML de la distribución secundaria, su detalle a eliminar y el detalle de centro de producción logística.; Debe existir un usuario identificado que ejecuta la operación.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDistributionSecondary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La lógica real de validación y persistencia se delega íntegramente al procedimiento interno; este wrapper no aplica reglas adicionales.; Siempre retorna un único resultset con los cuatro campos de salida estandarizados (CodeMessage, Message, Id, Code).', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDistributionSecondary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Distribución secundaria de costos; Centro de producción logística', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDistributionSecondary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Tras ejecutar el procedimiento interno, devuelve un resultset con CodeMessage, Message, Id y Code obtenidos como parámetros OUTPUT.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDistributionSecondary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Cost.SP_SaveDistributionSecondary_Output', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDistributionSecondary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDistributionSecondary';
-- GO
