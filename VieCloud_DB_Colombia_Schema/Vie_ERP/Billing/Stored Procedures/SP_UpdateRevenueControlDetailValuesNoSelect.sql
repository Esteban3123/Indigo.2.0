
-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2015-09-18
-- Description:	Recalcula los valores de un folio
-- =============================================
CREATE PROCEDURE [Billing].[SP_UpdateRevenueControlDetailValuesNoSelect]
	-- Add the parameters for the stored procedure here
	@REVENUECONTROLDETAILID AS INT,
	@OperativeUnitId AS INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @StatusResult AS BIT, 
			@MessageResult AS VARCHAR(100)

	EXEC [Billing].[SP_UpdateRevenueControlDetailValues_Output] @REVENUECONTROLDETAILID, @OperativeUnitId, @StatusResult OUT, @MessageResult OUT

	--SELECT @StatusResult as StatusResult, @MessageResult as MessageResult
	RETURN
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recalcula y actualiza los valores monetarios de un detalle de folio de facturación (línea de control de ingresos), dado el identificador del detalle y la unidad operativa. Delega el procesamiento real al procedimiento SP_UpdateRevenueControlDetailValues_Output, capturando el resultado de éxito o error, pero sin retornarlo como conjunto de datos al llamador. Se utiliza cuando se necesita forzar el recálculo de valores de un folio sin necesidad de obtener una respuesta seleccionable, por ejemplo desde procesos batch o actualizaciones masivas de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_UpdateRevenueControlDetailValuesNoSelect';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_UpdateRevenueControlDetailValuesNoSelect';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'"Wrapper que dispara el recálculo de los valores de un folio delegando en el SP de salida, sin devolver result set al llamador.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateRevenueControlDetailValuesNoSelect';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un detalle de control de ingresos identificado por el ID provisto, asociado a la unidad operativa indicada, válido para el procedimiento interno de recálculo.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateRevenueControlDetailValuesNoSelect';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'No retorna result set al cliente: descarta StatusResult y MessageResult obtenidos del procedimiento interno (la línea SELECT está comentada y termina con RETURN).; SET NOCOUNT ON evita el envío de mensajes de filas afectadas.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateRevenueControlDetailValuesNoSelect';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'folio; recálculo de valores; control de ingresos (revenue control)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateRevenueControlDetailValuesNoSelect';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.SP_UpdateRevenueControlDetailValues_Output: Invoca SP_UpdateRevenueControlDetailValues_Output con los identificadores recibidos y descarta sus parámetros OUT (StatusResult, MessageResult), por lo que los efectos de datos sobre el folio quedan delegados a ese procedimiento.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateRevenueControlDetailValuesNoSelect';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.SP_UpdateRevenueControlDetailValues_Output', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateRevenueControlDetailValuesNoSelect';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateRevenueControlDetailValuesNoSelect';
-- GO
