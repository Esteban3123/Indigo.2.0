CREATE FUNCTION [dbo].[NivelAfiliado]
(
@CodigoNivelAfiliado as int
)
RETURNS nvarchar (60)
AS
BEGIN
 
    DECLARE  @CodigoNivel nvarchar (60)
   
    SELECT @CodigoNivel = CASE @CodigoNivel WHEN 01 THEN 'RANGO A 2020' WHEN 02 THEN 'RANGO B 2020' WHEN 03 THEN 'RANGO C 2020' WHEN 04 THEN 'OTRO' WHEN 08 THEN 'RANGO A 2011' END 

    RETURN @CodigoNivel
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Convierte el código numérico del nivel de afiliado en su descripción textual correspondiente, como ''RANGO A 2020'', ''RANGO B 2020'', ''RANGO C 2020'', ''OTRO'' o ''RANGO A 2011''. Se usa para mostrar en reportes y consultas el nombre legible del nivel o rango de afiliación del paciente o beneficiario, en lugar del código interno. Aplica a entidades relacionadas con afiliación, contratos y RIPS donde se requiere identificar el nivel socioeconómico o categoría del afiliado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'NivelAfiliado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'NivelAfiliado';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce un código numérico de nivel de afiliado a su descripción textual (rangos de afiliación por año).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'NivelAfiliado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proveer un código numérico de nivel de afiliado como entrada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'NivelAfiliado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La descripción retornada está limitada a un catálogo cerrado de 5 valores posibles (RANGO A 2020, RANGO B 2020, RANGO C 2020, OTRO, RANGO A 2011).; Cualquier código fuera de {01,02,03,04,08} produce NULL.; El CASE compara contra una variable no inicializada (@CodigoNivel), por lo que la comparación nunca coincide y la función siempre retorna NULL — posible defecto: debería compararse contra el parámetro de entrada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'NivelAfiliado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Afiliado; Nivel de afiliado; Rangos de afiliación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'NivelAfiliado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Retorna la descripción del nivel según el CASE; si no coincide ningún WHEN, retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'NivelAfiliado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si código de nivel = 01 → retorna ''RANGO A 2020''; si código de nivel = 02 → retorna ''RANGO B 2020''; si código de nivel = 03 → retorna ''RANGO C 2020''; si código de nivel = 04 → retorna ''OTRO''; si código de nivel = 08 → retorna ''RANGO A 2011'' else retorna NULL para cualquier otro código', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'NivelAfiliado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'NivelAfiliado';
GO
