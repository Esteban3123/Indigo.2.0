
CREATE FUNCTION [dbo].[EstadoFolio] (@NUMINGRES as nvarchar(20),@UFUCODIGO as nvarchar(20))
RETURNS nvarchar (60)
AS
BEGIN

declare @INDICAPAC nvarchar(60); 
declare @NUMEFOLIO nvarchar(60); 

select @INDICAPAC=CASE INDICAPAC
WHEN 1 THEN 'Trasladar a Urgencias'
WHEN 2 THEN 'Trasladar a Observacion Urgencias'
WHEN 3 THEN 'Trasladar a Hospitalizacion'
WHEN 4 THEN 'Trasladar a  UCI Adulto'
WHEN 5 THEN 'Trasladar a UCI Pediatrica'
WHEN 6 THEN 'Trasladar a UCI Neonatal'
WHEN 7 THEN 'Trasladar a Consulta Externa'
WHEN 8 THEN 'Trasladar a  Cirugia'
WHEN 9 THEN 'Hospitalizacion en Casa'
WHEN 10 THEN 'Referencia'
WHEN 11 THEN 'Morgue'
WHEN 12 THEN 'Salida'
WHEN 13 THEN 'Continua en la Unidad'
WHEN 15 THEN 'Retiro Voluntario'
WHEN 16 THEN 'Fuga' END , @NUMEFOLIO=NUMEFOLIO
from .HCHISPACA AS A INNER JOIN
(SELECT UFUCODIGO, NUMINGRES,max(fechispac) AS FECHA
FROM .HCHISPACA
where INDICAPAC in ('3','9') and NUMINGRES =@NUMINGRES and UFUCODIGO= @UFUCODIGO
group by UFUCODIGO,NUMINGRES, UFUCODIGO) as B ON B.NUMINGRES = A.NUMINGRES AND B.UFUCODIGO = A.UFUCODIGO AND B.FECHA = A.FECHISPAC

RETURN @INDICAPAC +''+ @NUMEFOLIO

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que determina el estado o destino final del paciente en un ingreso hospitalario específico, buscando el folio de historia clínica más reciente que tenga indicación de traslado a hospitalización (código 3) o hospitalización en casa (código 9). Recibe el número de ingreso y la unidad funcional como parámetros, consulta la tabla de historias clínicas (HCHISPACA) para obtener la última nota clínica con esas indicaciones, y devuelve una cadena combinando la descripción legible del destino del paciente (por ejemplo ''Trasladar a Hospitalización'') junto con el número de folio correspondiente. Se utiliza para conocer rápidamente en qué estado de egreso o traslado quedó documentado el paciente dentro de su historia clínica, apoyando procesos de gestión de camas, admisiones y seguimiento del flujo del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'EstadoFolio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'EstadoFolio';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve una descripción textual del último estado de traslado/folio de un paciente en una unidad funcional, concatenando la acción de traslado y el número de folio asociado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoFolio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro histórico en HCHISPACA para el ingreso y unidad funcional indicados con INDICAPAC en (''3'',''9'').', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoFolio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se considera el evento más reciente (max FECHISPAC) por ingreso y unidad funcional.; El filtro restringe únicamente a estados de Hospitalización (3) u Hospitalización en Casa (9), ignorando otros tipos de traslado para la selección.; Si no hay registros que cumplan el filtro, la función retorna NULL.; El valor 14 no está mapeado y produciría descripción NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoFolio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Traslado de paciente; Hospitalización; Hospitalización en casa; UCI (Adulto, Pediátrica, Neonatal); Urgencias; Observación; Consulta externa; Cirugía; Referencia; Morgue; Retiro voluntario; Fuga; Folio de ingreso; Unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoFolio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Retorna la concatenación de la descripción de INDICAPAC (traducida según catálogo 1..16) más NUMEFOLIO del registro con FECHISPAC más reciente cuyo INDICAPAC esté en (''3'',''9'') para el ingreso y unidad dados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoFolio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si INDICAPAC = 1 → Etiqueta ''Trasladar a Urgencias''; si INDICAPAC = 2 → Etiqueta ''Trasladar a Observacion Urgencias''; si INDICAPAC = 3 → Etiqueta ''Trasladar a Hospitalizacion''; si INDICAPAC = 4 → Etiqueta ''Trasladar a UCI Adulto''; si INDICAPAC = 5 → Etiqueta ''Trasladar a UCI Pediatrica''; si INDICAPAC = 6 → Etiqueta ''Trasladar a UCI Neonatal''; si INDICAPAC = 7 → Etiqueta ''Trasladar a Consulta Externa''; si INDICAPAC = 8 → Etiqueta ''Trasladar a Cirugia''; si INDICAPAC = 9 → Etiqueta ''Hospitalizacion en Casa''; si INDICAPAC = 10 → Etiqueta ''Referencia''; si INDICAPAC = 11 → Etiqueta ''Morgue''; si INDICAPAC = 12 → Etiqueta ''Salida''; si INDICAPAC = 13 → Etiqueta ''Continua en la Unidad''; si INDICAPAC = 15 → Etiqueta ''Retiro Voluntario''; si INDICAPAC = 16 → Etiqueta ''Fuga'' else Si el código no corresponde a ninguno de los listados (incluido 14), la descripción es NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoFolio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'HCHISPACA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoFolio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoFolio';
GO
