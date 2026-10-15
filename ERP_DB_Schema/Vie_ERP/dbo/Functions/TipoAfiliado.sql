
create FUNCTION [dbo].[TipoAfiliado] (@CodigoTipoAfiliado as int)
RETURNS nvarchar (60)
AS
BEGIN

declare @TipoAfiliado nvarchar (60)

SELECT @TipoAfiliado = CASE @CodigoTipoAfiliado WHEN 0 THEN 'NO APLICA' WHEN 1 THEN 'COTIZANTE' WHEN 2 THEN 'BENEFICIARIO' WHEN 3 THEN 'ADICIONAL' WHEN 4 THEN 'JUBILADO / RETIRADO' WHEN 5 THEN 'PENSIONADO' END

RETURN @TipoAfiliado

END