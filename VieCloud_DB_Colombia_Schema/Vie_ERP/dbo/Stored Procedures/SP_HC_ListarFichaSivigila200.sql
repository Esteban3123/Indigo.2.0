-- Stored Procedure
-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,29-11-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila200]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
 
	Select
		CASE DESPLAZAMIENTO  WHEN '1' THEN 'X' END AS 'DESPLAZAMIENTO si',CASE DESPLAZAMIENTO WHEN '0' THEN 'X' END AS 'DESPLAZAMIENTO no',
		CASE HECHOCONT  WHEN '1' THEN 'X' END AS 'HECHOCONT si',CASE HECHOCONT WHEN '0' THEN 'X' END AS 'HECHOCONT no',
		CASE CASOASOC  WHEN '1' THEN 'X' END AS 'CASOASOC si',CASE CASOASOC WHEN '0' THEN 'X' END AS 'CASOASOC no',
		CASE CASOCAPT WHEN '1' THEN 'X' END AS 'UPGD',CASE CASOCAPT WHEN '2' THEN 'X' END AS 'BusqueInst', CASE CASOCAPT WHEN '3' THEN 'X' END AS 'VigIntes',
		CASE CASOCAPT WHEN '4' THEN 'X' END AS 'BusqueComun',
		CASE AGENTIDENT WHEN 1 THEN 'X' END AS 'VibrioChole', CASE AGENTIDENT WHEN 2 THEN 'X' END AS 'VibrioSPP', CASE AGENTIDENT WHEN 3 THEN 'X' END AS 'Otro',
		CASE AGENTIDENT WHEN 4 THEN 'X' END AS 'Pendiente', CASE AGENTIDENT WHEN 5 THEN 'X' END AS 'No Detectado', CASE AGENTIDENT WHEN 6 THEN 'X' END AS 'VibChoO1Toxi',
		CASE AGENTIDENT WHEN 7 THEN 'X' END AS 'VibChoO1noToxi', CASE AGENTIDENT WHEN 8 THEN 'X' END AS 'VibChonoO1noO139', CASE AGENTIDENT WHEN 9 THEN 'X' END AS 'VibChono139notoxi',
		CASE AGENTIDENT WHEN 10 THEN 'X' END AS 'VibChoO139',
		Rtrim(DEPART1) As 'DEPART1', Rtrim(DEPART2) As 'DEPART2', Rtrim(DEPART3) As 'DEPART3', Rtrim(DEPART4) As 'DEPART4', Rtrim(CIUDAD1) As 'CIUDAD1', Rtrim(CIUDAD2) As 'CIUDAD2',
		Rtrim(CIUDAD3) As 'CIUDAD3', Rtrim(CIUDAD4) As 'CIUDAD4', Rtrim(PAIS1) As 'PAIS1', Rtrim(PAIS2) As 'PAIS2', Rtrim(PAIS3) As 'PAIS3', Rtrim(PAIS4) As 'PAIS4',  Rtrim(CONQUIEN) As 'CONQUIEN',
		convert(varchar(10),FECHDESP1,103) As 'FECHDESP1', convert(varchar(10),FECHDESP2,103) As 'FECHDESP2',
		convert(varchar(10),FECHDESP3,103) As 'FECHDESP3', convert(varchar(10),FECHDESP4,103) As 'FECHDESP4',
		convert(varchar(10),FECHLLEG1,103) As 'FECHLLEG1', convert(varchar(10),FECHLLEG2,103) As 'FECHLLEG2',
		convert(varchar(10),FECHLLEG3,103) As 'FECHLLEG3', convert(varchar(10),FECHLLEG4,103) As 'FECHLLEG4',
		VERSION AS 'VERSION', 
		JSON AS 'JSON' 

	From 
		HCFICHA200 
	
	Where 
		IDFICHANOTIFICACION  = @IdFicha 
		
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta y devuelve los datos epidemiológicos de desplazamientos, contactos y agente identificado registrados en una ficha de notificación SIVIGILA (específicamente la ficha 200, correspondiente a cólera u otras enfermedades de vigilancia epidemiológica obligatoria). Recibe el identificador interno de la ficha y su número, y retorna los indicadores de desplazamiento del paciente (hasta cuatro trayectos con departamento, ciudad, país, fecha de salida y fecha de llegada), si hubo hecho de contacto, si existe caso asociado, el mecanismo de captación del caso (UPGD, búsqueda institucional, vigilancia intensificada o búsqueda comunitaria) y el agente etiológico identificado (Vibrio cholerae y variantes). Se utiliza para imprimir o visualizar la sección epidemiológica de la ficha de notificación obligatoria en la historia clínica del paciente dentro del módulo de salud pública del sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila200';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila200';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los datos de la ficha de notificación Sivigila 200 (cólera) transformando códigos numéricos en marcas ''X'' por categoría para presentación en formulario.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila200';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA200 cuyo IDFICHANOTIFICACION coincida con el identificador recibido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila200';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna información de una única ficha filtrada por IDFICHANOTIFICACION.; Las columnas categóricas se devuelven como ''X'' o NULL (no como valores numéricos crudos).; Las fechas se entregan en formato dd/mm/yyyy (CONVERT estilo 103).; Los campos de localización (DEPART, CIUDAD, PAIS) y CONQUIEN se entregan sin espacios a la derecha.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila200';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación Sivigila 200; Cólera (Vibrio cholerae O1, O139, toxigénico/no toxigénico); Vigilancia epidemiológica; Tipo de captación del caso (UPGD, búsqueda institucional, vigilancia intensificada, búsqueda comunitaria); Desplazamiento del paciente (departamento, ciudad, país, fechas de desplazamiento y llegada); Caso asociado / hecho contagiante', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila200';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA200: Cuando IDFICHANOTIFICACION = @IdFicha, se retorna la ficha con campos categóricos (DESPLAZAMIENTO, HECHOCONT, CASOASOC, CASOCAPT, AGENTIDENT) traducidos a ''X'' por opción, fechas formateadas a dd/mm/yyyy (estilo 103) y campos de texto sin espacios finales (RTRIM).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila200';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si DESPLAZAMIENTO = ''1'' / ''0'' → Marca ''X'' en columna ''DESPLAZAMIENTO si'' o ''DESPLAZAMIENTO no'' respectivamente; si HECHOCONT = ''1'' / ''0'' → Marca ''X'' en ''HECHOCONT si'' o ''HECHOCONT no''; si CASOASOC = ''1'' / ''0'' → Marca ''X'' en ''CASOASOC si'' o ''CASOASOC no''; si CASOCAPT en {1,2,3,4} → Marca ''X'' en una de las categorías de captación: UPGD (1), BusqueInst (2), VigIntes (3), BusqueComun (4); si AGENTIDENT en {1..10} → Marca ''X'' en la categoría correspondiente del agente identificado: VibrioChole, VibrioSPP, Otro, Pendiente, No Detectado, VibChoO1Toxi, VibChoO1noToxi, VibChonoO1noO139, VibChono139notoxi, VibChoO139', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila200';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA200', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila200';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila200';
-- GO
