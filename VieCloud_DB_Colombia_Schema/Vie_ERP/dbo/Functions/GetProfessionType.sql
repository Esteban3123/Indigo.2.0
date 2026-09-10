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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que traduce un código numérico de tipo de profesión al nombre descriptivo del cargo o especialidad del profesional de salud. Recibe un identificador entero y devuelve la denominación correspondiente, como Médico General, Enfermera, Odontólogo Especialista, Bacteriólogo, Psicólogo, entre otros. Se utiliza para mostrar en reportes, interfaces y documentos clínicos el tipo de profesional que atendió al paciente o ejecutó un procedimiento, evitando trabajar con códigos numéricos. Cubre 27 tipos de profesión del sector salud, incluyendo médicos, auxiliares, terapeutas, radiólogos e ingenieros sanitarios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'GetProfessionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'GetProfessionType';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce un identificador numérico de tipo de profesión sanitaria a su descripción textual estandarizada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetProfessionType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El catálogo de profesiones está cableado en el código (26 valores fijos).; Siempre retorna un VARCHAR no nulo; ante un código desconocido devuelve ''-''.; Los nombres de profesión se devuelven sin tildes salvo en los códigos 25 y 26 (''Bacteriologo(a)'' y ''Patólogo(a)'').', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetProfessionType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tipo de profesión sanitaria; Personal asistencial (médico, enfermería, odontología, terapia, laboratorio, radiología, etc.)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetProfessionType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (retorno escalar): Para cada valor entre 1 y 26 retorna la etiqueta correspondiente (p.ej. 1→''Medico General'', 2→''Medico Especialista'', 3→''Enfermera'', …, 26→''Patólogo(a)''); cualquier otro valor retorna ''-''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetProfessionType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Identificador entre 1 y 26 → Devuelve la descripción textual del tipo de profesión asociado else Devuelve el carácter ''-'' como valor por defecto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetProfessionType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetProfessionType';
GO
