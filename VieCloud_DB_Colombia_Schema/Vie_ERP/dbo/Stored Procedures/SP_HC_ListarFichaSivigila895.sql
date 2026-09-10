-- Stored Procedure
-- =============================================
-- Autor:		Jean Carlos Roldan Lozano
-- Fecha Creación: 04-12-2018
-- Descripción:	Sp que me lista la Información de las Fichas del Sivigila, Ficha 895
-- Modificó:    Yezid Garcia Medina
-- Fecha Modificación : 20-01-2022
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila895]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
 
			Select
				CASE EXANTEMA WHEN '1' THEN 'X' END AS 'EXANTEMA',CASE FIEBRE WHEN '1' THEN 'X' END AS 'FIEBRE', CASE HIPEREMIA WHEN '1' THEN 'X' END AS 'HIPEREMIA',
				CASE ARTRALGIAS WHEN '1' THEN 'X' END AS 'ARTRALGIAS',CASE MIALGIAS WHEN '1' THEN 'X' END AS 'MIALGIAS', CASE CEFALEA WHEN '1' THEN 'X' END AS 'CEFALEA',
				CASE COMPLITIPONEUR  WHEN '1' THEN 'X' END AS 'COMPLITIPONEUR si',CASE COMPLITIPONEUR WHEN '0' THEN 'X' END AS 'COMPLITIPONEUR no',
				CASE DESPLAULTI  WHEN '1' THEN 'X' END AS 'DESPLAULTI si',CASE DESPLAULTI WHEN '0' THEN 'X' END AS 'DESPLAULTI no',
				CASE REALPRIMECOGES  WHEN '1' THEN 'X' END AS 'REALPRIMECOGES si',CASE REALPRIMECOGES WHEN '0' THEN 'X' END AS 'REALPRIMECOGES no',
				CASE REALNECROCLI  WHEN '1' THEN 'X' END AS 'REALNECROCLI si',CASE REALNECROCLI WHEN '0' THEN 'X' END AS 'REALNECROCLI no',
				CASE GESTTERMEMB  WHEN '1' THEN 'X' END AS 'GESTTERMEMB si',CASE GESTTERMEMB WHEN '0' THEN 'X' END AS 'GESTTERMEMB no',
				CASE DEFECONGE  WHEN '1' THEN 'X' END AS 'DEFECONGE si',CASE DEFECONGE WHEN '0' THEN 'X' END AS 'DEFECONGE no',
				CASE TOMOMUESSUER  WHEN '1' THEN 'X' END AS 'TOMOMUESSUER si',CASE TOMOMUESSUER WHEN '0' THEN 'X' END AS 'TOMOMUESSUER no',
				CASE TOMOMUESCORD  WHEN '1' THEN 'X' END AS 'TOMOMUESCORD si',CASE TOMOMUESCORD WHEN '0' THEN 'X' END AS 'TOMOMUESCORD no',
				convert(varchar(10),FECHAINICIOSIND,103) As 'FECHAINICIOSIND', convert(varchar(10),FECHAULTMENS,103) As 'FECHAULTMENS', convert(varchar(10),FECHAPRIMECOG,103) As 'FECHAPRIMECOG',
				convert(varchar(10),FECHTERMEMB,103) As 'FECHTERMEMB',
				Rtrim(TIPOCOMPLNEUR) As 'TIPOCOMPLNEUR', Rtrim(MUNIDEPA) As 'MUNIDEPA', Rtrim(EDADGESTPRIMEC) As 'EDADGESTPRIMEC', Rtrim(PERIMCEFA) As 'PERIMCEFA',
				CASE ENCUSEGUEAPB WHEN '1' THEN 'X' END AS 'ENCUSEGUEAPB SI',CASE ENCUSEGUEAPB WHEN '2' THEN 'X' END AS 'ENCUSEGUEAPB NO', CASE ENCUSEGUEAPB WHEN '3' THEN 'X' END AS 'Pendiente',
				CASE CONDIFINAL WHEN '1' THEN 'X' END AS 'Aborto',CASE CONDIFINAL WHEN '2' THEN 'X' END AS 'Muerte Perinatal', CASE CONDIFINAL WHEN '3' THEN 'X' END AS 'NacidoVivo', 
				VERSION AS 'VERSION', 
				JSON AS 'JSON',
				RTRIM( JSON_VALUE(JSON,'$.CODIGOCIE10') ) As 'CODIGOCIE10', 
				RTRIM( JSON_VALUE(JSON,'$.CODPAIS_DESPLAZAMIENTO') ) As 'CODPAIS_DESPLAZAMIENTO', 
				RTRIM( JSON_VALUE(JSON,'$.CODDEPARTAMENTO_DESPLAZAMIENTO') ) As 'CODDEPARTAMENTO_DESPLAZAMIENTO', 
				RTRIM( JSON_VALUE(JSON,'$.CODMUNICIPIO_DESPLAZAMIENTO') ) As 'CODMUNICIPIO_DESPLAZAMIENTO'					
			From 
				HCFICHA895 
			Where 
				IDFICHANOTIFICACION  = @IdFicha			
			
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera el detalle completo de una ficha de notificación epidemiológica SIVIGILA código 895, correspondiente a casos de Zika y arbovirus con complicaciones neurológicas y obstétricas. Consulta la tabla HCFICHA895 filtrando por el identificador de la ficha y devuelve los síntomas del paciente (exantema, fiebre, hiperemia, artralgias, mialgias, cefalea), las complicaciones neurológicas, el seguimiento gestacional (primera consulta, edad gestacional, perímetro cefálico, terminación del embarazo), la toma de muestras (suero y cordón), las fechas clave (inicio de síntomas, última menstruación, primera consulta, terminación del embarazo) y la condición final del caso (aborto, muerte perinatal o nacido vivo). Además extrae del campo JSON adicional el código CIE-10 del diagnóstico y los datos de desplazamiento geográfico (país, departamento y municipio), presentando los valores binarios como marcas visuales para el formulario impreso del reporte epidemiológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila895';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila895';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera y formatea los datos de una ficha de notificación Sivigila 895 (Zika en gestantes) para visualización, transformando flags y fechas en marcas legibles.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila895';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA895 con IDFICHANOTIFICACION igual al identificador suministrado; La columna JSON debe contener un documento JSON válido con las claves CODIGOCIE10, CODPAIS_DESPLAZAMIENTO, CODDEPARTAMENTO_DESPLAZAMIENTO y CODMUNICIPIO_DESPLAZAMIENTO para extraerse correctamente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila895';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las fechas se entregan siempre con formato dd/mm/yyyy (estilo 103); Los campos de texto se devuelven sin espacios a la derecha (RTRIM); Los flags se traducen a ''X'' o NULL, nunca al valor original; La consulta solo filtra por IDFICHANOTIFICACION; el parámetro NumFicha no se utiliza', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila895';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Sivigila; Ficha 895; Notificación epidemiológica; Síntomas (exantema, fiebre, hiperemia, artralgias, mialgias, cefalea); Complicación neurológica; Gestación / embarazo; Defectos congénitos; Muestra de suero; Muestra de cordón; Aborto; Muerte perinatal; Nacido vivo; Edad gestacional; Perímetro cefálico; CIE-10; Desplazamiento (país/departamento/municipio)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila895';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA895: Cuando IDFICHANOTIFICACION = @IdFicha, retorna las columnas clínicas y epidemiológicas de la ficha 895 con transformaciones de presentación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila895';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Campo de síntoma (EXANTEMA, FIEBRE, HIPEREMIA, ARTRALGIAS, MIALGIAS, CEFALEA) = ''1'' → Se muestra ''X'' indicando presencia del síntoma else Se muestra NULL; si Campos binarios SI/NO (COMPLITIPONEUR, DESPLAULTI, REALPRIMECOGES, REALNECROCLI, GESTTERMEMB, DEFECONGE, TOMOMUESSUER, TOMOMUESCORD) = ''1'' → Marca ''X'' en la columna ''si'' else Si = ''0'' marca ''X'' en la columna ''no''; si ENCUSEGUEAPB = ''1'' → Marca ''X'' en SI else Si = ''2'' marca NO; si = ''3'' marca Pendiente; si CONDIFINAL = ''1'' → Marca ''X'' en Aborto else Si = ''2'' marca Muerte Perinatal; si = ''3'' marca NacidoVivo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila895';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA895', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila895';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila895';
-- GO
