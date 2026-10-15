
CREATE FUNCTION [dbo].[MezclasLiquidosPorFolioConcatenados](@CodigoPaciente as varchar(25), @NumIngreso as char(10), @NumFolio as varchar(10))
RETURNS varchar(max)
AS
BEGIN
    DECLARE @MezclasLiquidos as varchar(max)

SELECT 
	@MezclasLiquidos = STRING_AGG(
							CONCAT(
							RTRIM(MEZLIQPAC), ' - ', RTRIM(ADMMEZLIQ)), ', ')
							FROM dbo.HCINFLIQD A With(Nolock) 
							INNER JOIN dbo.INUNIFUNC B With(Nolock) ON A.UFUCODIGO=B.UFUCODIGO AND (UFUTIPUNI<>'15' AND UFUTIPUNI<>'24') 
							WHERE A.IPCODPACI= @CodigoPaciente AND NUMINGRES = @NumIngreso AND A.NUMEFOLIO= @NumFolio 
    
    RETURN @MezclasLiquidos
END