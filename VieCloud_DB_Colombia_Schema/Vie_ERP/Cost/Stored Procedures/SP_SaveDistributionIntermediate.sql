-- =============================================
-- Author:		Diego A. Roldan Lozano
-- Create date: 2025-07-23
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una distribución intermedia
-- =============================================
CREATE PROCEDURE [Cost].[SP_SaveDistributionIntermediate]
    @DistributionIntermediateXml AS XML,
	@DistributionIntermediateDetailForDeleteXml AS XML,
	@CostLogisticsProductionCenterDetail AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeMessage INT,
			@Message VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC Cost.SP_SaveDistributionIntermediate_Output 
		@DistributionIntermediateXml, 
		@DistributionIntermediateDetailForDeleteXml, 
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que gestiona el ciclo de vida de una distribución intermedia de costos: permite crearla, actualizarla o confirmarla según los datos recibidos. Recibe como parámetros la cabecera y el detalle de la distribución intermedia en formato XML, el detalle de registros a eliminar, la información del centro de producción logístico de costos y el código del usuario que realiza la operación. Delega el procesamiento real al procedimiento interno Cost.SP_SaveDistributionIntermediate_Output, actuando como fachada que expone el resultado de la operación (código de mensaje, mensaje descriptivo, identificador y código del registro afectado). Se utiliza en el módulo de costos para registrar cómo se distribuyen los costos entre centros intermedios de la organización.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_SaveDistributionIntermediate';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_SaveDistributionIntermediate';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'"Wrapper que delega en un procedimiento interno la operación de guardar/actualizar/confirmar una distribución intermedia y devuelve el resultado (código, mensaje, id y código generado) como result set.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDistributionIntermediate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el procedimiento Cost.SP_SaveDistributionIntermediate_Output con la firma esperada (cuatro parámetros de entrada y cuatro OUTPUT).', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDistributionIntermediate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Actúa como wrapper: delega toda la lógica al procedimiento _Output y expone el resultado como conjunto de filas en lugar de parámetros OUTPUT.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDistributionIntermediate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Distribución intermedia de costos', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDistributionIntermediate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (result set): Tras invocar SP_SaveDistributionIntermediate_Output, se retorna un SELECT con CodeMessage, Message, Id y Code obtenidos de los parámetros OUTPUT.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDistributionIntermediate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Cost.SP_SaveDistributionIntermediate_Output', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDistributionIntermediate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDistributionIntermediate';
-- GO
