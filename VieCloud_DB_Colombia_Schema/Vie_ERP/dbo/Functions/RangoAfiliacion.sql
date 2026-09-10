
CREATE FUNCTION [dbo].[RangoAfiliacion](@CodigoNivelAfiliado as char(2))
RETURNS nvarchar (60)
AS
BEGIN
    DECLARE @CodigoNivel nvarchar (60)

    SELECT @CodigoNivel = CASE @CodigoNivelAfiliado WHEN '01' THEN 'RANGO A 2020' WHEN '02' THEN 'RANGO B 2020' 
	WHEN '03' THEN 'RANGO C 2020' WHEN '04' THEN 'OTRO' WHEN '08' THEN 'RANGO A 2011' ELSE 'NO APLICA' END 

    RETURN @CodigoNivel
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que convierte el código numérico del nivel de afiliación de un paciente o afiliado en su descripción legible. Recibe un código de dos caracteres (por ejemplo ''01'', ''02'', ''03'') y devuelve el nombre del rango tarifario correspondiente, como ''RANGO A 2020'', ''RANGO B 2020'', ''RANGO C 2020'' o ''RANGO A 2011''. Se utiliza para mostrar en reportes y documentos el rango de afiliación o nivel socioeconómico del afiliado en términos comprensibles, evitando mostrar códigos internos. Es relevante en procesos de facturación, liquidación de copagos, cuotas moderadoras y clasificación de pacientes según nivel de afiliación al sistema de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'RangoAfiliacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'RangoAfiliacion';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce un código de nivel de afiliación a una etiqueta descriptiva del rango de afiliación correspondiente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'RangoAfiliacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre devuelve un valor no nulo (mínimo ''NO APLICA'').; Solo reconoce los códigos ''01'',''02'',''03'',''04'',''08''; el resto se considera no aplicable.; Distingue dos vigencias de rangos: 2020 (códigos 01-03) y 2011 (código 08).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'RangoAfiliacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'afiliación; nivel de afiliado; rango de afiliación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'RangoAfiliacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Devuelve ''RANGO A 2020'' si código=''01''; ''RANGO B 2020'' si ''02''; ''RANGO C 2020'' si ''03''; ''OTRO'' si ''04''; ''RANGO A 2011'' si ''08''; en cualquier otro caso devuelve ''NO APLICA''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'RangoAfiliacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si código de nivel = ''01'' → retorna ''RANGO A 2020''; si código de nivel = ''02'' → retorna ''RANGO B 2020''; si código de nivel = ''03'' → retorna ''RANGO C 2020''; si código de nivel = ''04'' → retorna ''OTRO''; si código de nivel = ''08'' → retorna ''RANGO A 2011''; si código de nivel no coincide con los valores definidos → retorna ''NO APLICA''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'RangoAfiliacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'RangoAfiliacion';
GO
