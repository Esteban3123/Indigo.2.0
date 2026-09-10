-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[LiquidationDetail]
	-- Add the parameters for the stored procedure here
	@PayrollId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Payroll].[LiquidationDetail].[PayrollDate], [Payroll].[LiquidationDetail].[ConceptTotalValue],
	[Payroll].[LiquidationDetail].[AccruedValue], [Payroll].[LiquidationDetail].[DeductedValue] 
	FROM [Payroll].[LiquidationDetail] WHERE [Payroll].[LiquidationDetail].[PayrollId] = @PayrollId;
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que consulta el detalle de liquidación de una nómina específica, identificada por su ID de nómina. Retorna la fecha de pago, el valor total por concepto, el valor devengado y el valor deducido de cada concepto liquidado para el período indicado. Se usa para visualizar el desglose de devengados y deducciones de una liquidación de nómina de empleados, permitiendo revisar cómo quedó compuesta cada nómina procesada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'LiquidationDetail';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'LiquidationDetail';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta el detalle de la liquidación de nómina (fecha, valor del concepto, devengado y deducido) asociado a una nómina específica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'LiquidationDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el identificador de nómina en el detalle de liquidación para obtener resultados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'LiquidationDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan registros del detalle de liquidación correspondientes a la nómina solicitada; No modifica datos: operación de solo lectura', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'LiquidationDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nómina; Liquidación de nómina; Devengados; Deducciones; Conceptos de pago', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'LiquidationDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.LiquidationDetail: Cuando el identificador de nómina coincide, retorna fecha, valor total del concepto, valor devengado y valor deducido del detalle de liquidación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'LiquidationDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.LiquidationDetail', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'LiquidationDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'LiquidationDetail';
-- GO
