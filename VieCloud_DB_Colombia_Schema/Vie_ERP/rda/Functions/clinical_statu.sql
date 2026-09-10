
CREATE FUNCTION [rda].[clinical_statu] (@Estado as int)
RETURNS varchar (15)
AS
BEGIN

declare @grupo varchar(15)
SET @grupo=
     CASE @Estado 
		 WHEN 1 THEN 'remission'
		 WHEN 2 THEN 'remission'
		 WHEN 3 THEN 'resolved'
		 WHEN 4 THEN 'active'
		 WHEN 5 THEN 'active'
		 WHEN 6 THEN 'resolved'
		 ELSE 'unknown'
	 END 
RETURN @grupo
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que traduce un código numérico de estado clínico a su etiqueta en inglés, agrupando los valores 1 y 2 como `remission`, 3 y 6 como `resolved`, 4 y 5 como `active`, y cualquier otro como `unknown`. Se utiliza para normalizar o categorizar el estado de condiciones o diagnósticos clínicos en reportes o consultas del esquema `rda`.', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'clinical_statu';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'clinical_statu';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Mapea un código numérico de estado clínico interno a su categoría FHIR-like (''active'', ''remission'', ''resolved'' o ''unknown'').', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'clinical_statu';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor retornado siempre pertenece al conjunto cerrado {''remission'',''resolved'',''active'',''unknown''}.; La longitud del resultado nunca excede 15 caracteres (tipo de retorno varchar(15)).; Códigos no contemplados nunca producen NULL: caen al ELSE ''unknown''.', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'clinical_statu';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'estado clínico; remisión; resolución; condición activa', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'clinical_statu';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Estados 1 y 2 → ''remission''; estados 4 y 5 → ''active''; estados 3 y 6 → ''resolved''; cualquier otro valor → ''unknown''.', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'clinical_statu';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Estado IN (1,2) → Devuelve ''remission''; si @Estado IN (4,5) → Devuelve ''active''; si @Estado IN (3,6) → Devuelve ''resolved''; si @Estado fuera de {1..6} → Devuelve ''unknown''', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'clinical_statu';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'clinical_statu';
GO
