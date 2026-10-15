

CREATE FUNCTION [dbo].[TipoProfesionMedico] (@Tipo as int)
RETURNS varchar (100)
AS
BEGIN

declare @Aislamiento varchar (100)


SELECT @Aislamiento = CASE @Tipo 
		WHEN '1' THEN 'Medico general' 
		WHEN '2' THEN 'Medico especialista' 
		WHEN '3' THEN 'Enfermera' 
		WHEN '4' THEN 'Auxiliar enfermeria' 
		WHEN '5' THEN 'Odontólogo General'
		WHEN '6' THEN 'Odontólogo especialista'
		WHEN '7' THEN 'Nutricionista'
		WHEN '8' THEN 'Higienista'
		WHEN '9' THEN 'Psicólogo'
		WHEN '10' THEN 'Trabajadora social'
		WHEN '11' THEN 'Promotor de saneamiento'
		WHEN '12' THEN 'Ingeniero sanitario'
		WHEN '13' THEN 'Medico veterinario'
		WHEN '14' THEN 'Ingeniero alimento'
		WHEN '15' THEN 'Auxiliar bacteriólogo'
		WHEN '16' THEN 'Terapeuta'
		WHEN '17' THEN 'Optómetra'
		WHEN '18' THEN 'Químico farmaceutico'
		WHEN '19' THEN 'Radiologo'
		WHEN '20' THEN 'Tecnólogo radiologo'
		WHEN '21' THEN 'Instrumentador Qx'
		WHEN '22' THEN 'Auxiliar patologia'
		WHEN '23' THEN 'Otros'
		WHEN '24' THEN 'Medico interno'
		WHEN '25' THEN 'Bacteriologo(a)'
		WHEN '26' THEN 'Patólogo(a)'
		WHEN '27' THEN 'Médico residente'
ELSE ' ' END

	

RETURN @Aislamiento

END