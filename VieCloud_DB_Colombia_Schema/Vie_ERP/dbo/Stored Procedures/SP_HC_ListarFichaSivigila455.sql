-- Stored Procedure

-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,28-11-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila455]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
 

	--Select 	*	From HCFICHA455 where IDFICHANOTIFICACION  = 13

	Select
	Rtrim(CUALOTRO) As 'CUALOTRO',
	CASE VISTORATAS  WHEN '1' THEN 'X' END AS 'VISTORATAS si',CASE VISTORATAS WHEN '0' THEN 'X' END AS 'VISTORATAS no',
	CASE SISTALCANT  WHEN '1' THEN 'X' END AS 'SISTALCANT si',CASE SISTALCANT WHEN '0' THEN 'X' END AS 'SISTALCANT no',
	CASE AGUASESTAN  WHEN '1' THEN 'X' END AS 'AGUASESTAN si',CASE AGUASESTAN WHEN '0' THEN 'X' END AS 'AGUASESTAN no',
	CASE DISPORESISOL  WHEN '1' THEN 'X' END AS 'Recolección',CASE DISPORESISOL WHEN '0' THEN 'X' END AS 'DispoPerido',
	CASE FIEBRE WHEN '1' THEN 'X' END AS 'FIEBRE',CASE CEFALEA WHEN '1' THEN 'X' END AS 'CEFALEA', CASE MIALGIAS WHEN '1' THEN 'X' END AS 'MIALGIAS',
	CASE HEPATOMEGALIA WHEN '1' THEN 'X' END AS 'HEPATOMEGALIA', CASE ICTERICIA WHEN '1' THEN 'X' END AS 'ICTERICIA', CASE PERROS WHEN '1' THEN 'X' END AS 'PERROS',
	CASE GATOS WHEN '1' THEN 'X' END AS 'GATOS',CASE BOVINOS WHEN '1' THEN 'X' END AS 'BOVINOS', CASE EQUINOS WHEN '1' THEN 'X' END AS 'EQUINOS',
	CASE PORCINOS WHEN '1' THEN 'X' END AS 'PORCINOS', CASE NINGUNO WHEN '1' THEN 'X' END AS 'NINGUNO', CASE OTROS WHEN '1' THEN 'X' END AS 'OTROS',
	CASE ACUEDUCTO WHEN '1' THEN 'X' END AS 'ACUEDUCTO',CASE POZOCOMUN WHEN '1' THEN 'X' END AS 'POZOCOMUN', CASE RIO WHEN '1' THEN 'X' END AS 'RIO',
	CASE TANQUE WHEN '1' THEN 'X' END AS 'TANQUE', CASE REPRESA WHEN '1' THEN 'X' END AS 'REPRESA', CASE RIO2 WHEN '1' THEN 'X' END AS 'RIO2',
	CASE ARROYO WHEN '1' THEN 'X' END AS 'ARROYO',CASE LAGO WHEN '1' THEN 'X' END AS 'LAGO', CASE SINATENCE WHEN '1' THEN 'X' END AS 'SINATENCE'
		
	From HCFICHA455 where IDFICHANOTIFICACION  = @IdFicha

END

--select * from hcfichanotificacion where id = '8'
--select * from HCFICHA455
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera el detalle completo de la ficha epidemiológica SIVIGILA código 455, utilizada para la notificación de Leptospirosis y otras enfermedades de vigilancia en salud pública. Dado el identificador de la ficha, consulta la tabla HCFICHA455 y retorna los síntomas clínicos del paciente (fiebre, cefalea, mialgias, hepatomegalia, ictericia), los animales con los que tuvo contacto (perros, gatos, bovinos, equinos, porcinos), las fuentes de agua de exposición (acueducto, pozo, río, tanque, represa, arroyo, lago) y las condiciones ambientales del entorno (avistamiento de ratas, sistema de alcantarillado, aguas estancadas, disposición de residuos sólidos). Los campos booleanos se convierten a marcas visuales (''X'') para facilitar la impresión o visualización del formulario oficial de notificación epidemiológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila455';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila455';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los datos de la ficha epidemiológica SIVIGILA 455 (leptospirosis) traduciendo banderas binarias a marcas ''X'' para impresión/visualización.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila455';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA455 cuyo IDFICHANOTIFICACION coincida con el identificador recibido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila455';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen columnas con valores ''X'' o NULL, nunca el valor crudo 0/1.; Las variables binarias con dualidad si/no solo se marcan en una de las dos columnas (excluyentes según el valor almacenado).; La consulta filtra siempre por IDFICHANOTIFICACION, devolviendo a lo sumo los registros que cumplan ese identificador.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila455';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación SIVIGILA; Vigilancia epidemiológica; Factores ambientales (alcantarillado, aguas estancadas, residuos sólidos, vista de ratas); Síntomas clínicos (fiebre, cefalea, mialgias, hepatomegalia, ictericia); Exposición a animales (perros, gatos, bovinos, equinos, porcinos); Fuente de abastecimiento de agua (acueducto, pozo, río, tanque, represa, arroyo, lago)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila455';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHA455: Cuando IDFICHANOTIFICACION coincide con el parámetro de entrada, retorna las columnas de la ficha transformando 1→''X'' y 0→''X'' en columnas separadas (si/no) para cada variable binaria.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila455';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Valor de la bandera = ''1'' → Marca con ''X'' la columna afirmativa (si) correspondiente else Si vale ''0'' marca con ''X'' la columna negativa (no); cualquier otro valor queda NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila455';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA455', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila455';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila455';
-- GO
