
create FUNCTION [dbo].[TipoAfiliado] (@CodigoTipoAfiliado as int)
RETURNS nvarchar (60)
AS
BEGIN

declare @TipoAfiliado nvarchar (60)

SELECT @TipoAfiliado = CASE @CodigoTipoAfiliado WHEN 0 THEN 'NO APLICA' WHEN 1 THEN 'COTIZANTE' WHEN 2 THEN 'BENEFICIARIO' WHEN 3 THEN 'ADICIONAL' WHEN 4 THEN 'JUBILADO / RETIRADO' WHEN 5 THEN 'PENSIONADO' END

RETURN @TipoAfiliado

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que convierte un código numérico de tipo de afiliado en su descripción legible en texto. Recibe un número del 0 al 5 y devuelve la categoría de afiliación del paciente o beneficiario en el sistema de salud: NO APLICA, COTIZANTE, BENEFICIARIO, ADICIONAL, JUBILADO/RETIRADO o PENSIONADO. Se usa para mostrar en reportes, RIPS y documentos clínicos la condición de afiliación del paciente respecto a su aseguradora o EPS, en lugar del código interno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'TipoAfiliado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'TipoAfiliado';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce un código numérico de tipo de afiliado a su descripción textual estandarizada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoAfiliado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El código recibido debe estar en el rango 0-5 para obtener una descripción; otros valores devuelven NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoAfiliado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El catálogo de tipos de afiliado está fijo en seis valores (0..5); La descripción retornada nunca excede 60 caracteres (nvarchar(60)); Códigos fuera del catálogo siempre devuelven NULL, nunca un texto por defecto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoAfiliado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Afiliado; Cotizante; Beneficiario; Adicional; Jubilado/Retirado; Pensionado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoAfiliado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Cuando el código=0 retorna ''NO APLICA''; =1 ''COTIZANTE''; =2 ''BENEFICIARIO''; =3 ''ADICIONAL''; =4 ''JUBILADO / RETIRADO''; =5 ''PENSIONADO''; cualquier otro valor retorna NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoAfiliado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CodigoTipoAfiliado = 0 → Retorna ''NO APLICA''; si @CodigoTipoAfiliado = 1 → Retorna ''COTIZANTE''; si @CodigoTipoAfiliado = 2 → Retorna ''BENEFICIARIO''; si @CodigoTipoAfiliado = 3 → Retorna ''ADICIONAL''; si @CodigoTipoAfiliado = 4 → Retorna ''JUBILADO / RETIRADO''; si @CodigoTipoAfiliado = 5 → Retorna ''PENSIONADO'' else Retorna NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoAfiliado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoAfiliado';
GO
