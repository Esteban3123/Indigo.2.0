

CREATE FUNCTION [dbo].[Medicos] (@Paciente as varchar(25), @Ingreso as varchar(10))
RETURNS varchar (20)
AS
BEGIN

declare @Medico varchar(20)
declare @prueba varchar(25)
SELECT @prueba=IPCODPACI FROM INPACIENT
set @Medico = 'A01'

RETURN @Medico

END