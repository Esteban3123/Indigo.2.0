

CREATE FUNCTION [dbo].[MedicosHosp] (@Paciente as varchar(25), @Ingreso as varchar(10))
RETURNS nvarchar (20)
AS
BEGIN

declare @Medico nvarchar(20)

SELECT TOP 1 @Medico=CODPROSAL FROM CHMEDITRA WHERE NUMINGRES=@Ingreso AND IPCODPACI=@Paciente

RETURN @Medico

END