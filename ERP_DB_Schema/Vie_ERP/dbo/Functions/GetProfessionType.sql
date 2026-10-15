-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-09-03
-- Description:	Se retorna el tipo de profesión de acuerdo al id recibido
-- =============================================
CREATE FUNCTION [dbo].[GetProfessionType]
(	
	@ProfessionType INT
)
RETURNS VARCHAR(MAX) 
BEGIN

	RETURN CASE @ProfessionType
		WHEN 1 THEN 'Medico General' 
		WHEN 2 THEN 'Medico Especialista' 
		WHEN 3 THEN 'Enfermera' 
		WHEN 4 THEN 'Auxiliar Enfermeria' 
		WHEN 5 THEN 'Odontologo General' 
		WHEN 6 THEN 'Odontologo Especialista' 
		WHEN 7 THEN 'Nutricionista' 
		WHEN 8 THEN 'Higienista' 
		WHEN 9 THEN 'Psicologo' 
		WHEN 10 THEN 'Trabajadora Social' 
		WHEN 11 THEN 'Promotor de Saneamiento' 
		WHEN 12 THEN 'Ingeniero Sanitario' 
		WHEN 13 THEN 'Medico Veterinario' 
		WHEN 14 THEN 'Ingeniero Alimento' 
		WHEN 15 THEN 'Auxiliar Bacteriologo' 
		WHEN 16 THEN 'Terapeuta' 
		WHEN 17 THEN 'Optometra' 
		WHEN 18 THEN 'Quimico Farmaceutico' 
		WHEN 19 THEN 'Radiologo' 
		WHEN 20 THEN 'Tecnologo Radiologo' 
		WHEN 21 THEN 'Instrumentador Qx' 
		WHEN 22 THEN 'Auxiliar Patologia' 
		WHEN 23 THEN 'Otros' 
		WHEN 24 THEN 'Medico Interno' 
		WHEN 25 THEN 'Bacteriologo(a)' 
		WHEN 26 THEN 'Patólogo(a)'
		ELSE '-' 
	END
END