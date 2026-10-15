
CREATE FUNCTION [dbo].[Patient_Stage] (@IPCODPACI as varchar(25), @CODDIAGNO as varchar(25), @NUMEFOLIO as varchar(25))
RETURNS varchar (300)
AS
BEGIN

declare @Stage varchar(300)
	
 SET @Stage  = (select Top 1
					Case ESTADIO 
					When 0 then 'Estadio clínico (ec) 0 (tumor in situ)' 
					when 1 Then 'ec I o 1' 
					when 2 then 'ec IA o 1A' 
					when 3 Then 'ec IA1' 
					when 4 Then 'ec IA2' 
					when 5 Then 'ec IB o 1b' 
					when 6 Then 'ec IB1' 
					when 7 Then 'ec IB2' 
					when 8 Then 'ec IC o 1c' 
					when 9 Then 'ec IS o 1s' 
					when 10 Then 'ec II o 2' 
					when 11 Then 'ec IIA o 2a' 
					when 12 Then 'ec IIA1' 
					when 13 Then 'ec IIA2' 
					when 14 Then 'ec IIB o 2b' 
					when 15 Then 'ec IIC o 2c'
					when 16 Then 'ec III o 3' 
					when 17 Then 'ec IIIA o 3a' 
					when 18 Then 'ec IIIB o 3b' 
					when 19 Then 'ec IIIC o 3c' 
					when 20 Then 'ec IV o 4' 
					when 21 Then 'ec IVA o 4a' 
					when 22 Then 'ec IVB o 4b' 
					when 23 Then 'ec IVC o 4c' 
					when 24 Then 'ec 4S' 
					when 25 Then 'ec V o 5' 
					when 26 Then 'Estadio IAB' 
					when 27 Then 'ec IIIC1 o 3c1'
					when 28 Then 'ec IIIC2 o 3c2'
					when 29 Then 'ec IIID o 3D'
					when 30 Then 'ec IB3'
					when 31 Then 'ec IC1'
					when 32 Then 'ec IC2'
					when 33 Then 'ec IC3'
					when 34 Then 'ec IIIA1'
					when 35 Then 'ec IIIA2'
					when 55 Then 'Persona con aseguramiento' 
					when 93 Then 'Sin información de estadificación en historia clínica' 
					when 98 Then 'No Aplica' 
					when 99 Then 'Desconocido, el dato de esta variable no se encuentra descrito en los soportes clínicos' 
					end AS 'ESTADIO'
					from dbo.INDIAGNOH A WITH(NOLOCK) 
					where A.IPCODPACI = @IPCODPACI And A.CODDIAGNO = @CODDIAGNO ANd NUMEFOLIO = @NUMEFOLIO
				  )

/*
Función que me va a listar el estadio del paciente.
*/

RETURN @Stage

END