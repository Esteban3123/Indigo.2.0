

CREATE FUNCTION [dbo].[PatientDestiny] (@CodigoPaciente as varchar(25), @Ingreso as varchar(10), @FechaHistoria as Datetime )
RETURNS varchar (2)
AS
BEGIN


declare @PatientDestiny varchar(2)
	
 SET @PatientDestiny  = (select top 1 INDICAPAC from HCHISPACA where IPCODPACI = @CodigoPaciente and NUMINGRES = @Ingreso and FECHISPAC < @FechaHistoria and INDICAPAC <> 13 order by NUMEFOLIO desc)
/*
Función que me va a listar la causa de atención.
*/

RETURN @PatientDestiny

END