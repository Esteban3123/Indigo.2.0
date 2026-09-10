-- =============================================
-- Author:		Andrea Pahola Coqueco Cuellar
-- Create date: 2023-08-17
-- Description:	Procedimiento que se encarga de obtener el periodo liquidado usando el Id de la liquidación
-- =============================================

CREATE PROCEDURE [Payroll].[SP_GetPeriodLiquidatedByLiquidationId]
(
	@LiquidationId INT,
	@Period VARCHAR(7) OUTPUT,
	@DayInitial INT OUTPUT,
	@DayEnd INT OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON

	SELECT
		@Period = CONCAT(RIGHT(CONCAT('00', MONTH(l.PayrollDateLiquidated)), 2),'/', YEAR(l.PayrollDateLiquidated)),
		@DayInitial = IIF(l.PayrollDays = 15, IIF(DAY(l.PayrollDateLiquidated) = 15, 1, 16), 1),
		@DayEnd = IIF(l.PayrollDays = 15, IIF(DAY(l.PayrollDateLiquidated) = 15, 15, 31), 31)
	FROM Payroll.Liquidation l
	WHERE l.Id = @LiquidationId
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dado el identificador de una liquidación de nómina, este procedimiento recupera el período de pago correspondiente (mes y año en formato MM/AAAA) y los días de inicio y fin del período (quincena o mes completo). Consulta la tabla de liquidaciones de nómina para determinar, según la fecha de liquidación y la cantidad de días de pago (15 o 30/31 días), si el período liquidado corresponde a la primera o segunda quincena del mes. Se utiliza para identificar con precisión el rango de fechas de un período de nómina ya liquidado a partir de su ID de liquidación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_GetPeriodLiquidatedByLiquidationId';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_GetPeriodLiquidatedByLiquidationId';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Deriva el período (MM/AAAA) y el rango de días (inicial/final) cubiertos por una liquidación de nómina, según su fecha y la modalidad de pago (quincenal o mensual).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetPeriodLiquidatedByLiquidationId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Payroll.Liquidation con Id igual al identificador recibido; en caso contrario los parámetros OUTPUT quedan sin valor asignado.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetPeriodLiquidatedByLiquidationId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El mes en @Period siempre se expresa con dos dígitos (padding con ''00'').; El día final de la segunda quincena y del mes se fija en 31 sin importar la duración real del mes.; Sólo se reconocen dos modalidades de período: quincenal (PayrollDays=15) o mensual (cualquier otro valor).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetPeriodLiquidatedByLiquidationId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'liquidación de nómina; período de nómina; quincena; fecha de liquidación', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetPeriodLiquidatedByLiquidationId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (OUTPUT): Devuelve @Period como ''MM/AAAA'' a partir de MONTH/YEAR de Liquidation.PayrollDateLiquidated, con mes a 2 dígitos.; [RETURN_RESULT] (OUTPUT): Si PayrollDays = 15 y DAY(PayrollDateLiquidated) = 15 → @DayInitial=1, @DayEnd=15 (primera quincena).; [RETURN_RESULT] (OUTPUT): Si PayrollDays = 15 y DAY(PayrollDateLiquidated) ≠ 15 → @DayInitial=16, @DayEnd=31 (segunda quincena).; [RETURN_RESULT] (OUTPUT): Si PayrollDays ≠ 15 → @DayInitial=1, @DayEnd=31 (período mensual completo).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetPeriodLiquidatedByLiquidationId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Liquidation.PayrollDays = 15 (liquidación quincenal) → Determina la quincena según DAY(PayrollDateLiquidated): día 15 = primera quincena (1-15); cualquier otro día = segunda quincena (16-31). else Se asume período mensual: días 1 a 31.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetPeriodLiquidatedByLiquidationId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Liquidation', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetPeriodLiquidatedByLiquidationId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetPeriodLiquidatedByLiquidationId';
-- GO
