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