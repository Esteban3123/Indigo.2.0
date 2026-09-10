
CREATE FUNCTION dbo.EstadificacionLinfomas(@ESTADIO as INT)
RETURNS varchar (300)
AS
BEGIN

declare @Stage varchar(300)
	
 SET @Stage  = (SELECT
				CASE @ESTADIO 
				 WHEN 1  THEN 'Estado (etapa) I'
				 WHEN 2  THEN 'Estado (etapa) II'
				 WHEN 3  THEN 'Estado (etapa) III'
				 WHEN 4  THEN 'Estado (etapa) IV'
				 WHEN 5  THEN 'Estado IA'
				 WHEN 6  THEN 'Estado IB'
				 WHEN 7  THEN 'Estado IIA'
				 WHEN 8  THEN 'Estado IIB'
				 WHEN 9  THEN 'Estado IIIA'
				 WHEN 10 THEN 'Estado IIIB'
				 WHEN 11 THEN 'Estado IVA'
				 WHEN 12 THEN 'Estado IVB'
				 WHEN 13 THEN 'Extranodal cualquier estadio'
				 WHEN 14 THEN 'Primario SNC'
				 WHEN 15 THEN 'Primario Mediastinal'
				 WHEN 16 THEN 'Primario de otros órganos'
				 WHEN 98 THEN 'No Aplica (tumor diferente a los enunciados)'
				 WHEN 99 THEN 'Desconocido, el dato de esta variable no se encuentra descrito en los soportes clínicos'
				END AS 'ESTADIO'					
			   )
RETURN @Stage

END

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce un código numérico de estadificación de linfomas a su descripción textual estandarizada (etapas I-IV, subetapas A/B, presentaciones extranodales y valores especiales).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadificacionLinfomas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El código de estadio debe corresponder a uno de los valores catalogados (1-16, 98, 99); cualquier otro valor produce resultado NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadificacionLinfomas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El catálogo de estadios de linfoma se mantiene centralizado en esta función; Los códigos 1-16 representan estadificaciones clínicas reales; 98 y 99 son valores administrativos (no aplica/desconocido); La función no consulta tablas: el mapeo es estático en código', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadificacionLinfomas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Linfoma; Estadificación oncológica; Etapas clínicas I-IV; Subetapas A/B; Enfermedad extranodal; Primario SNC; Primario mediastinal', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadificacionLinfomas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (retorno escalar): Devuelve la descripción textual del estadio según el mapeo CASE; si el código no coincide con ninguno de los WHEN definidos, retorna NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadificacionLinfomas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si código entre 1 y 4 → Retorna ''Estado (etapa) I'' a ''Estado (etapa) IV''; si código entre 5 y 12 → Retorna subetapas con sufijo A/B (IA, IB, IIA, IIB, IIIA, IIIB, IVA, IVB); si código entre 13 y 16 → Retorna presentaciones especiales: Extranodal, Primario SNC, Primario Mediastinal o Primario de otros órganos; si código = 98 → Retorna ''No Aplica (tumor diferente a los enunciados)''; si código = 99 → Retorna ''Desconocido, el dato de esta variable no se encuentra descrito en los soportes clínicos''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadificacionLinfomas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadificacionLinfomas';
GO
