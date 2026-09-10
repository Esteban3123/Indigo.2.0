

CREATE FUNCTION [dbo].[DestinoFolio] (@Paciente as varchar(25))
RETURNS nvarchar (80)
AS
BEGIN

declare @DescipcionDestino nvarchar(80)

SELECT TOP 1 @DescipcionDestino= ' Destino: ' + CASE INDICAPAC WHEN '1' THEN 'Trasladar a Urgencias' WHEN '2'
THEN 'Trasladar a Observacion Urgencias' WHEN '3' THEN 'Trasladar a Hospitalizacion' WHEN '4' THEN 'Trasladar a  UCI Adulto'
WHEN '5' THEN 'Trasladar a UCI Pediatrica' WHEN '6' THEN 'Trasladar a UCI Neonatal' WHEN '7' THEN 'Trasladar a Consulta Externa'
WHEN '8' THEN 'Trasladar a  Cirugia' WHEN '9' THEN 'Hospitalizacion en Casa' WHEN '10' THEN 'Referencia' WHEN '11' THEN 'Morgue'
WHEN '12' THEN 'Salida' WHEN '13' THEN 'Continua en la Unidad' END  FROM HCHISPACA WHERE IPCODPACI = @Paciente
ORDER BY FECHISPAC DESC

RETURN @DescipcionDestino

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que, dado el código o cédula de un paciente, retorna el destino de traslado o egreso registrado en su folio clínico más reciente. Consulta la historia clínica del paciente (HCHISPACA) ordenando por fecha descendente para obtener el último registro, y traduce el código de indicación (INDICAPAC) a una descripción legible como ''Trasladar a Urgencias'', ''Hospitalización'', ''UCI Adulto'', ''Referencia'', ''Salida'', entre otros. Se usa para mostrar en pantalla o reportes el destino final del paciente según la última nota clínica, apoyando la gestión de flujo hospitalario y continuidad de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'DestinoFolio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'DestinoFolio';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la descripción textual del último destino registrado para un paciente, traduciendo el código de indicador de destino en su nombre legible.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DestinoFolio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en HCHISPACA para el paciente recibido.; El campo INDICAPAC debe contener uno de los valores ''1''..''13'' para producir descripción no nula.; El campo FECHISPAC debe estar poblado para definir el registro más reciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DestinoFolio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se considera el registro más reciente del paciente según FECHISPAC (orden descendente, TOP 1).; El texto retornado siempre lleva el prefijo '' Destino: '' cuando hay coincidencia y el indicador está dentro del catálogo 1..13.; El catálogo de destinos está embebido en código (no parametrizado en tabla).; Si el paciente no tiene registros en HCHISPACA o el indicador no está en 1..13, retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DestinoFolio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Historia clínica; Destino del paciente; Urgencias; Observación; Hospitalización; UCI Adulto; UCI Pediátrica; UCI Neonatal; Consulta Externa; Cirugía; Hospitalización en Casa; Referencia; Morgue; Salida', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DestinoFolio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCHISPACA: Selecciona el registro más reciente del paciente (ORDER BY FECHISPAC DESC, TOP 1) en HCHISPACA y retorna '' Destino: '' concatenado con la descripción correspondiente al valor de INDICAPAC.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DestinoFolio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si INDICAPAC = ''1'' → Destino: Trasladar a Urgencias; si INDICAPAC = ''2'' → Destino: Trasladar a Observacion Urgencias; si INDICAPAC = ''3'' → Destino: Trasladar a Hospitalizacion; si INDICAPAC = ''4'' → Destino: Trasladar a UCI Adulto; si INDICAPAC = ''5'' → Destino: Trasladar a UCI Pediatrica; si INDICAPAC = ''6'' → Destino: Trasladar a UCI Neonatal; si INDICAPAC = ''7'' → Destino: Trasladar a Consulta Externa; si INDICAPAC = ''8'' → Destino: Trasladar a Cirugia; si INDICAPAC = ''9'' → Hospitalizacion en Casa; si INDICAPAC = ''10'' → Referencia; si INDICAPAC = ''11'' → Morgue; si INDICAPAC = ''12'' → Salida; si INDICAPAC = ''13'' → Continua en la Unidad; si INDICAPAC fuera del rango 1..13 → Retorna NULL (CASE sin ELSE)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DestinoFolio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DestinoFolio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DestinoFolio';
GO
