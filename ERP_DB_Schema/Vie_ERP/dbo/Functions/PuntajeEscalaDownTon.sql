CREATE FUNCTION [dbo].[PuntajeEscalaDownTon]
(
	-- Add the parameters for the function here
	@NumeroIngreso char(10),
	@CodigoPaciente varchar(25)
)
RETURNS int
AS
BEGIN

DECLARE @Resultado int

set @Resultado = ( select top 1 RESULTADO from 
							(
									SELECT TOP 1  AUTO as 'Organizador', ( EDADPACIE + CAIDPREVI + TRANQUILI + DIURETICO + HIPOTENSO + ANTIPARKI +  ANTIDEPRE + ALTERAVIS +  ALTERAUDI + ICTUEXTRE +  ESTADOMEN + SEGURAYUD +  INSEGAYUD + IMPOSIBLE +  PATOLOGIA + NUTRICION + SELECIMED ) as RESULTADO 
									FROM  HCESCDOWN  a 
									WHERE  a.NUMINGRES = @NumeroIngreso AND a.IPCODPACI =@CodigoPaciente 
									order by AUTO DESC 
								union ALL
									select top 1 ID as 'Organizador', RESULTADO 
									FROM  HCESCALAS b 
									WHERE   b.NUMINGRES = @NumeroIngreso AND b.IPCODPACI =@CodigoPaciente and b.TIPOESCALA = '113'  
									order by ID DESC 
							) x order by Organizador DESC 
)

	RETURN @Resultado

END