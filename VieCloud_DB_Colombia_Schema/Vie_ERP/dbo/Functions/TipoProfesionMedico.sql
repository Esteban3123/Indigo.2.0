

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que convierte un código numérico en el nombre legible del tipo de profesión o especialidad del profesional de la salud. Recibe un número entero (por ejemplo 1 para Médico general, 2 para Médico especialista, 3 para Enfermera, 9 para Psicólogo, 25 para Bacteriólogo, etc.) y devuelve la descripción textual correspondiente. Se usa para mostrar en informes, pantallas y reportes RIPS el cargo o perfil asistencial del profesional que brinda la atención, en lugar del código interno. Cubre 27 tipos de profesionales incluyendo médicos, enfermeras, odontólogos, terapeutas, tecnólogos y otros roles del equipo de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'TipoProfesionMedico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'TipoProfesionMedico';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce un código numérico de tipo de profesional de salud a su descripción textual estandarizada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoProfesionMedico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre retorna una cadena no nula (espacio si el código es desconocido); El catálogo de tipos de profesional está embebido en el código (no se consulta tabla); Existen 27 tipos de profesional reconocidos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoProfesionMedico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Profesional de salud; Médico general; Médico especialista; Médico interno; Médico residente; Enfermería; Odontología; Nutrición; Psicología; Trabajo social; Saneamiento; Veterinaria; Bacteriología; Terapia; Optometría; Química farmacéutica; Radiología; Instrumentación quirúrgica; Patología', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoProfesionMedico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve la descripción del profesional según el código (1=Medico general, 2=Medico especialista, ... 27=Médico residente); si el código no coincide con ninguno, retorna un espacio en blanco.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoProfesionMedico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Tipo entre 1 y 27 → Retorna la descripción del tipo de profesional correspondiente else Retorna cadena con un espacio '' ''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoProfesionMedico';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoProfesionMedico';
GO
