-- Stored Procedure
-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,19-11-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila228]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON; 

	Select
		Rtrim(D16) As 'D16', Rtrim(D15) As 'D15', Rtrim(D13) As 'D13', Rtrim(D12) As 'D12',
		Rtrim(D11) As 'D11', Rtrim(D21) As 'D21', Rtrim(D22) As 'D22', Rtrim(D23) As 'D23',
		Rtrim(D25) As 'D25', Rtrim(D26) As 'D26', Rtrim(D36) As 'D36', Rtrim(D46) As 'D46',
		CASE CLASIFCLIN WHEN '1' THEN 'X' END AS 'Normal', CASE CLASIFCLIN WHEN '2' THEN 'X' END AS 'Dudoso', CASE CLASIFCLIN WHEN '3' THEN 'X' END AS 'Muy Leve',
		CASE CLASIFCLIN WHEN '4' THEN 'X' END AS 'Leve', CASE CLASIFCLIN WHEN '5' THEN 'X' END AS 'Moderada', CASE CLASIFCLIN WHEN '6' THEN 'X' END AS 'Severa',
		CASE PRESECARI WHEN '1' THEN 'X' END AS 'PRESECARI Si',CASE PRESECARI WHEN '0' THEN 'X' END AS 'PRESECARI No',
		CASE TIPCARIE WHEN '1' THEN 'X' END AS 'TIPCARIE Si',CASE TIPCARIE WHEN '0' THEN 'X' END AS 'TIPCARIE No',
		CASE FUENTECONS WHEN '1' THEN 'X' END AS 'Acueducto', CASE FUENTECONS WHEN '2' THEN 'X' END AS 'Pozo', CASE FUENTECONS WHEN '3' THEN 'X' END AS 'Quebrada',
		CASE FUENTECONS WHEN '4' THEN 'X' END AS 'Agua Embotellada', CASE FUENTECONS WHEN '5' THEN 'X' END AS 'Otro',
		CASE INGESTCREM WHEN '1' THEN 'X' END AS 'INGESTCREM Si',CASE INGESTCREM WHEN '0' THEN 'X' END AS 'INGESTCREM No',
		CASE INGESTENJUA WHEN '1' THEN 'X' END AS 'INGESTENJUA Si',CASE INGESTENJUA WHEN '0' THEN 'X' END AS 'INGESTENJUA No',
		CASE APLITOP WHEN '1' THEN 'X' END AS 'APLITOP Si',CASE APLITOP WHEN '0' THEN 'X' END AS 'APLITOP No',
		CASE PERSORECLAC WHEN '1' THEN 'X' END AS 'SI', CASE PERSORECLAC WHEN '2' THEN 'X' END AS 'NO', CASE PERSORECLAC WHEN '3' THEN 'X' END AS 'DESCONOCIDO', 
		VERSION AS 'VERSION', 
		JSON AS 'JSON' 

	From 
		HCFICHA228 
	
	Where 
		IDFICHANOTIFICACION  = @IdFicha

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera el detalle completo de la Ficha SIVIGILA 228 (vigilancia epidemiológica odontológica) a partir de un identificador de ficha. Consulta la tabla HCFICHA228 para obtener el índice COP/CEO por superficies dentales (D11 a D46), la clasificación clínica de fluorosis (Normal, Dudoso, Muy Leve, Leve, Moderada, Severa), la presencia y tipo de caries, la fuente de agua de consumo (acueducto, pozo, quebrada, embotellada u otra), los hábitos de higiene oral como ingesta de crema dental y enjuague bucal, la aplicación tópica de flúor, y si la persona recibe lactancia. Presenta cada campo codificado de forma legible con etiquetas Sí/No o la opción seleccionada, e incluye además la versión y representación JSON de la ficha. Se usa para imprimir o visualizar la ficha de reporte de caries dental al sistema de vigilancia en salud pública SIVIGILA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila228';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila228';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera y formatea los datos de una ficha de notificación Sivigila (formato 228) traduciendo códigos numéricos a marcas tipo ''X'' para impresión/visualización del formulario.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila228';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA228 con IDFICHANOTIFICACION igual al identificador recibido', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila228';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los códigos numéricos de los campos categóricos se transforman a marcadores ''X'' para presentación tipo formulario impreso; Los campos de texto se devuelven sin espacios en blanco a la derecha (RTRIM); Solo se retorna información de una única ficha identificada por su ID de notificación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila228';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación Sivigila; Caries dental; Clasificación clínica de fluorosis; Fuente de consumo de agua; Aplicación tópica de flúor; Ingesta de crema dental/enjuague; Lactancia / persona que recibe lactancia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila228';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA228: Devuelve un único registro de HCFICHA228 filtrado por IDFICHANOTIFICACION, con campos D## en RTRIM y campos categóricos (CLASIFCLIN, PRESECARI, TIPCARIE, FUENTECONS, INGESTCREM, INGESTENJUA, APLITOP, PERSORECLAC) traducidos a columnas con marcador ''X''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila228';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CLASIFCLIN entre ''1''..''6'' → Marca con ''X'' la columna correspondiente: 1=Normal, 2=Dudoso, 3=Muy Leve, 4=Leve, 5=Moderada, 6=Severa; si FUENTECONS entre ''1''..''5'' → Marca ''X'' según fuente de consumo de agua: 1=Acueducto, 2=Pozo, 3=Quebrada, 4=Agua Embotellada, 5=Otro; si PERSORECLAC en ''1'',''2'',''3'' → Marca ''X'' en SI, NO o DESCONOCIDO según valor; si Campos PRESECARI/TIPCARIE/INGESTCREM/INGESTENJUA/APLITOP = ''1'' o ''0'' → Se marca ''X'' en columna Si o No correspondiente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila228';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA228', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila228';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila228';
-- GO
