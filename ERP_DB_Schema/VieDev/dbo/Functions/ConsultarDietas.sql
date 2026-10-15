-- =============================================
-- Author:      Felipe Ortiz
-- Create Date: 8/03/2022
-- Description: Función para obtener las dieta
--				de los pacientes.
-- =============================================
CREATE FUNCTION [dbo].[ConsultarDietas]
(
 @paciente varchar(25),
 @ingreso varchar(20),
 @tipo tinyint
)
returns varchar(2000)
AS
BEGIN
	declare @valor varchar(2000)

	--Consultamos las descripción de las dietas, es decir todas las las descripciones concatenadas
	if @tipo = 1 
		begin
		SET @valor = (select STUFF((SELECT CHAR(10) + x.DESTIPDIE from dbo.CHREGDIET AS DIET INNER JOIN CHTIPDIET X ON DIET.CODTIPDIE = X.CODTIPDIE WHERE DIET.IPCODPACI = @paciente AND NUMINGRES = @ingreso FOR XML PATH('')),1,1,'') as tipoDieta) 
		end
	-- Consultamos lo codigo de las dietas, concatenados
	else if @tipo =2 
		begin 
		SET @valor = (select STUFF((SELECT ',' + DIET.CODTIPDIE from dbo.CHREGDIET AS DIET WHERE DIET.IPCODPACI = @paciente AND NUMINGRES = @ingreso FOR XML PATH('')),1,1,'') as CodigoDieta)
		end
return @valor
END