
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que devuelve la descripción textual del estadio clínico oncológico de un paciente para un diagnóstico y folio de historia clínica específicos. A partir del código de paciente (cédula), el código de diagnóstico CIE-10 y el número de folio, consulta la tabla de diagnósticos de historia clínica (INDIAGNOH) y traduce el valor numérico del campo ESTADIO a su denominación clínica estándar (por ejemplo: ''ec IIB o 2b'', ''ec IV o 4'', ''No Aplica''). Cubre todos los estadios reconocidos internacionalmente para clasificación oncológica (0 a IV con subestadios A, B, C) y valores especiales como ''Sin información'' o ''Desconocido''. Se utiliza principalmente en reportes oncológicos, historia clínica y RIPS para expresar de forma legible el estadio tumoral registrado en la atención del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Patient_Stage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Patient_Stage';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce el código numérico de estadificación clínica oncológica del diagnóstico de un paciente a su descripción textual estándar (TNM/AJCC).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Stage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un diagnóstico en INDIAGNOH que coincida con paciente, código de diagnóstico y folio; de lo contrario el resultado es NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Stage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se considera el primer registro encontrado (TOP 1) sin ORDER BY explícito; El resultado se restringe a la combinación exacta paciente + diagnóstico + folio; Los códigos de estadio reconocidos están limitados al catálogo fijo: 0-35, 55, 93, 98 y 99; Códigos 93/98/99 representan ausencia de dato (sin información, no aplica, desconocido) y 55 representa estado de aseguramiento, no un estadio clínico real; La consulta usa NOLOCK, por lo que admite lecturas sucias', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Stage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Diagnóstico; Estadio clínico oncológico; Clasificación TNM/AJCC; Tumor in situ; Folio de atención; Aseguramiento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Stage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.INDIAGNOH: Cuando existe diagnóstico que coincide con paciente, código de diagnóstico y folio, devuelve la descripción textual del valor ESTADIO según mapeo fijo; si no hay coincidencia o el valor no está en el catálogo, devuelve NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Stage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Valor numérico de ESTADIO en INDIAGNOH (0-35, 55, 93, 98, 99) → Devuelve la descripción textual correspondiente del estadio clínico oncológico (TNM/AJCC) else Si el código no coincide con ninguno de los valores mapeados, devuelve NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Stage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INDIAGNOH', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Stage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Stage';
GO
