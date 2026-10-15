
CREATE FUNCTION [dbo].[RiskFactorAlert] (@PatientCode as varchar(25), @AdmissionNumber as char(10), @Origin as int)
RETURNS bit
AS
BEGIN
--Origin -> 1 = Factores de riesgo 
--Origin -> 2 = Escalas

declare @RiskFactorAlert bit
declare @count as int
IF @Origin = 1 BEGIN
	SET @count = (select COUNT(*) FROM dbo.ReportRiskFactors WHERE IPCODPACI = @PatientCode AND Status = 1)
	IF @count > 0 BEGIN
		SET @RiskFactorAlert  = 1
	END
	ELSE BEGIN
		SET @RiskFactorAlert  = 0
	END
END
ELSE IF @Origin = 2 BEGIN
	set @count = (select COUNT(*) FROM dbo.HCESCALAS WHERE IPCODPACI = @PatientCode AND NUMINGRES = @AdmissionNumber)
	if @count > 0 begin
		SET @RiskFactorAlert  = 1
	end
	else begin
		SET @RiskFactorAlert  = 0
	end
end

/*
Función que me va a definir si un paciente tiene Factor de riesgo o Alerta
*/

RETURN @RiskFactorAlert

END