
-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2015-09-18
-- Description:	Recalcula los valores de un folio
-- =============================================
CREATE PROCEDURE [Billing].[SP_UpdateRevenueControlDetailValues]
	-- Add the parameters for the stored procedure here
	@REVENUECONTROLDETAILID AS INT,
	@OperativeUnitId AS INT = NULL
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @StatusResult AS BIT, 
			@MessageResult AS VARCHAR(MAX)

	EXEC [Billing].[SP_UpdateRevenueControlDetailValues_Output] @REVENUECONTROLDETAILID, @OperativeUnitId, @StatusResult OUT, @MessageResult OUT

	SELECT @StatusResult as StatusResult, @MessageResult as MessageResult
	RETURN
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recalcula y actualiza los valores monetarios de un detalle de control de ingresos (folio de facturación), identificado por su ID interno. Delega el procesamiento real al procedimiento SP_UpdateRevenueControlDetailValues_Output, pasando el identificador del folio y la unidad operativa, y retorna el resultado de la operación con un indicador de éxito o error y su mensaje correspondiente. Se utiliza cuando es necesario refrescar o corregir los valores económicos asociados a un folio de control de ingresos en el módulo de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_UpdateRevenueControlDetailValues';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_UpdateRevenueControlDetailValues';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'"Wrapper que dispara el recálculo de los valores de un folio delegando en el procedimiento *_Output y expone el estado y mensaje del resultado como result set.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateRevenueControlDetailValues';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un identificador de detalle de control de ingresos válido para que el proceso interno opere sobre él.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateRevenueControlDetailValues';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El recálculo se delega íntegramente al procedimiento *_Output; este wrapper no aplica lógica adicional sobre los datos.; Siempre devuelve un result set con dos columnas: StatusResult (BIT) y MessageResult (VARCHAR(MAX)).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateRevenueControlDetailValues';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'folio; recálculo de valores', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateRevenueControlDetailValues';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (result set): Tras ejecutar SP_UpdateRevenueControlDetailValues_Output, retorna un SELECT con @StatusResult y @MessageResult capturados de los parámetros OUTPUT.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateRevenueControlDetailValues';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.SP_UpdateRevenueControlDetailValues_Output', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateRevenueControlDetailValues';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateRevenueControlDetailValues';
-- GO
