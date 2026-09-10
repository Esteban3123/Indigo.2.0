

CREATE FUNCTION [dbo].[PrecedentDescription] (@CodigoAntecedente as int)
RETURNS nvarchar (50)
AS
BEGIN

declare @DescripcionAntecedente nvarchar(50)

SELECT @DescripcionAntecedente = CASE @CodigoAntecedente WHEN 1 THEN 'Médicos' WHEN 2 THEN 'Quirúrgico' WHEN 3 THEN 'Anestésico' WHEN 4 THEN 'Transfusionales' WHEN 5 THEN 'Inmunológicos' WHEN 6 THEN 'Alérgicos' WHEN 7 THEN 'Traumáticos' WHEN 8 THEN 'Psicológicos' WHEN 9 THEN 'Farmacológicos'  WHEN 10 THEN 'Familiares' WHEN 11 THEN 'Ginecológicos / obstétricos' WHEN 12 THEN 'Urología sexual' WHEN 13 THEN 'Perinatales' WHEN 14 THEN 'Tóxicos' WHEN 15 THEN 'Hábitos de vida' WHEN 16 THEN 'Esquema de vacunación' WHEN 17 THEN 'Escolares' WHEN 18 THEN 'Laborales' WHEN 19 THEN 'Nutricionales' WHEN 20 THEN 'Odontológicos' WHEN 21 THEN 'Socioeconómicos' WHEN 22 THEN 'Otros' WHEN 23 THEN 'Oftalmológicos' END

RETURN @DescripcionAntecedente

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que convierte un código numérico de antecedente clínico en su descripción textual correspondiente. Dado un número entero entre 1 y 23, retorna el nombre del tipo de antecedente del paciente, por ejemplo: Médicos, Quirúrgico, Alérgicos, Familiares, Ginecológicos, entre otros. Se utiliza en la historia clínica para mostrar de forma legible la categoría de antecedente registrada, evitando mostrar solo el código. Cubre los principales grupos de antecedentes usados en la anamnesis clínica, incluyendo antecedentes médicos, quirúrgicos, farmacológicos, laborales, socioeconómicos y de hábitos de vida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'PrecedentDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'PrecedentDescription';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce un código numérico de antecedente clínico a su descripción textual estandarizada en español.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PrecedentDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El código debe estar en el rango 1-23 para obtener una descripción; cualquier otro valor produce NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PrecedentDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El catálogo de antecedentes está embebido en código (no parametrizable vía tabla); Existen exactamente 23 tipos de antecedentes soportados; La descripción retornada nunca excede 50 caracteres', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PrecedentDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Antecedentes médicos; Antecedentes quirúrgicos; Antecedentes anestésicos; Antecedentes transfusionales; Antecedentes inmunológicos; Antecedentes alérgicos; Antecedentes traumáticos; Antecedentes psicológicos; Antecedentes farmacológicos; Antecedentes familiares; Antecedentes ginecológicos/obstétricos; Urología sexual; Antecedentes perinatales; Antecedentes tóxicos; Hábitos de vida; Esquema de vacunación; Antecedentes escolares; Antecedentes laborales; Antecedentes nutricionales; Antecedentes odontológicos; Antecedentes socioeconómicos; Antecedentes oftalmológicos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PrecedentDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Devuelve la descripción textual del antecedente según el mapeo fijo de códigos 1-23; si el código no coincide, retorna NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PrecedentDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CodigoAntecedente entre 1 y 23 → Retorna la etiqueta correspondiente al tipo de antecedente clínico (Médicos, Quirúrgico, Anestésico, etc.) else Retorna NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PrecedentDescription';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PrecedentDescription';
GO
