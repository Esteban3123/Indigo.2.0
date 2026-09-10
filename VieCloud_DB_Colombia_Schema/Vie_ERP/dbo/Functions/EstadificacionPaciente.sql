
CREATE FUNCTION dbo.EstadificacionPaciente(@ESTADIO as INT)
RETURNS varchar (300)
AS
BEGIN

declare @Stage varchar(300)
	
 SET @Stage  = (SELECT
				CASE @ESTADIO 
				WHEN 0 then 'Estadio clínico (ec) 0 (tumor in situ)' 
				WHEN 1 Then 'ec I o 1' 
				WHEN 2 then 'ec IA o 1A' 
				WHEN 3 Then 'ec IA1' 
				WHEN 4 Then 'ec IA2' 
				WHEN 5 Then 'ec IB o 1b' 
				WHEN 6 Then 'ec IB1' 
				WHEN 7 Then 'ec IB2' 
				WHEN 8 Then 'ec IC o 1c' 
				WHEN 9 Then 'ec IS o 1s' 
				WHEN 10 Then 'ec II o 2' 
				WHEN 11 Then 'ec IIA o 2a' 
				WHEN 12 Then 'ec IIA1' 
				WHEN 13 Then 'ec IIA2' 
				WHEN 14 Then 'ec IIB o 2b' 
				WHEN 15 Then 'ec IIC o 2c'
				WHEN 16 Then 'ec III o 3' 
				WHEN 17 Then 'ec IIIA o 3a' 
				WHEN 18 Then 'ec IIIB o 3b' 
				WHEN 19 Then 'ec IIIC o 3c' 
				WHEN 20 Then 'ec IV o 4' 
				WHEN 21 Then 'ec IVA o 4a' 
				WHEN 22 Then 'ec IVB o 4b' 
				WHEN 23 Then 'ec IVC o 4c' 
				WHEN 24 Then 'ec 4S' 
				WHEN 25 Then 'ec V o 5' 
				WHEN 26 Then 'Estadio IAB' 
				WHEN 27 Then 'ec IIIC1 o 3c1'
				WHEN 28 Then 'ec IIIC2 o 3c2'
				WHEN 29 Then 'ec IIID o 3D'
				WHEN 30 Then 'ec IB3'
				WHEN 31 Then 'ec IC1'
				WHEN 32 Then 'ec IC2'
				WHEN 33 Then 'ec IC3'
				WHEN 34 Then 'ec IIIA1'
				WHEN 35 Then 'ec IIIA2'
				WHEN 55 Then 'Persona con aseguramiento' 
				WHEN 93 Then 'Sin información de estadificación en historia clínica' 
				WHEN 98 Then 'No Aplica' 
				WHEN 99 Then 'Desconocido, el dato de esta variable no se encuentra descrito en los soportes clínicos' 
				END AS 'ESTADIO'					
			   )
RETURN @Stage

END

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce un código numérico de estadificación clínica oncológica a su descripción textual estándar (estadios TNM y valores administrativos especiales).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadificacionPaciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El código de estadio debe corresponder a uno de los valores catalogados (0-35, 55, 93, 98, 99); cualquier otro valor produce NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadificacionPaciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado siempre es un varchar(300) o NULL.; El catálogo de estadios cubre rangos clínicos 0-V incluyendo subclasificaciones TNM (A, B, C, S, 1, 2, 3) y valores administrativos (55, 93, 98, 99).; No realiza acceso a tablas ni modifica datos (función pura determinística sobre el parámetro).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadificacionPaciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estadificación clínica oncológica; Estadio tumoral (TNM); Tumor in situ; Historia clínica; Aseguramiento del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadificacionPaciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve la descripción textual del estadio clínico según el código numérico de entrada vía CASE; si el código no está mapeado retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadificacionPaciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si código entre 0 y 35 → Retorna la denominación del estadio clínico oncológico correspondiente (ec 0 a ec V, con subdivisiones A/B/C y numéricas como IA1, IIIC2, IB3, etc.); si código = 55 → Retorna ''Persona con aseguramiento''; si código = 93 → Retorna ''Sin información de estadificación en historia clínica''; si código = 98 → Retorna ''No Aplica''; si código = 99 → Retorna ''Desconocido, el dato de esta variable no se encuentra descrito en los soportes clínicos'' else Para cualquier otro código retorna NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadificacionPaciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadificacionPaciente';
GO
