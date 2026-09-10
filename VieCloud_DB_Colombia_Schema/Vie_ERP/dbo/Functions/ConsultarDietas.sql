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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que consulta las dietas prescritas a un paciente durante un ingreso u hospitalización específico. Recibe como parámetros la cédula del paciente, el número de ingreso y un tipo de consulta: si el tipo es 1, devuelve las descripciones de todas las dietas asignadas concatenadas (ej: ''Dieta blanda, Dieta hiposódica''); si el tipo es 2, devuelve los códigos de las dietas separados por coma. Combina el registro de dietas del paciente (CHREGDIET) con el catálogo de tipos de dieta hospitalaria (CHTIPDIET) para traducir códigos en descripciones legibles. Se usa en visualización de información clínica nutricional del paciente durante su estancia hospitalaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'ConsultarDietas';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'ConsultarDietas';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve, para un paciente e ingreso dado, las dietas registradas concatenadas en una sola cadena: descripciones legibles o códigos según el modo solicitado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ConsultarDietas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el paciente y el ingreso indicados en CHREGDIET para obtener resultados; en caso contrario retorna NULL.; Para tipo=1 los códigos de dieta deben existir en CHTIPDIET (INNER JOIN) para aparecer en la descripción concatenada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ConsultarDietas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Filtra siempre por paciente (IPCODPACI) e ingreso (NUMINGRES) para acotar las dietas a una estancia específica.; El resultado se entrega como una única cadena (máx 2000 caracteres) sin filas múltiples.; El primer separador se elimina con STUFF, garantizando que la cadena no inicie con coma o salto de línea.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ConsultarDietas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ingreso hospitalario; dieta; tipo de dieta; registro de dietas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ConsultarDietas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Cuando @tipo=1, retorna las descripciones (DESTIPDIE) de las dietas del paciente y su ingreso concatenadas separadas por salto de línea (CHAR(10)).; [RETURN_RESULT] N/A: Cuando @tipo=2, retorna los códigos (CODTIPDIE) de las dietas del paciente y su ingreso concatenados separados por coma.; [RETURN_RESULT] N/A: Cuando @tipo no es 1 ni 2, retorna NULL al no asignarse valor a la variable de salida.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ConsultarDietas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @tipo = 1 → Consulta descripciones de dieta uniendo CHREGDIET con CHTIPDIET y las concatena con salto de línea. else Si @tipo = 2, consulta solo los códigos de dieta de CHREGDIET y los concatena con coma.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ConsultarDietas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHREGDIET; dbo.CHTIPDIET', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ConsultarDietas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ConsultarDietas';
GO
