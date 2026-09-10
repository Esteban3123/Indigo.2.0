-- Stored Procedure
-- =============================================
-- Autor:		Jean Carlos Roldan Lozano
-- Fecha Creacion: 22-11-2018
-- Descripcion:	Sp que me lista la Información de las Fichas del Sivigila, Ficha 591
-- Modifico:    Yezid Garcia Medina
-- Fecha Modificación : 30-12-2021
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila591]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;

			Select
				CASE SITIODEF WHEN '1' THEN 'X' END AS 'Hospital', CASE SITIODEF WHEN '2' THEN 'X' END AS 'Centro', CASE SITIODEF WHEN '3' THEN 'X' END AS 'CasaDom',
				CASE SITIODEF WHEN '4' THEN 'X' END AS 'LugTrab', CASE SITIODEF WHEN '5' THEN 'X' END AS 'ViaPub', CASE SITIODEF WHEN '6' THEN 'X' END AS 'OtroSit',
				CASE SITIODEF WHEN '7' THEN 'X' END AS 'SinInformacion', Rtrim(NOMAPE) As 'NOMAPE', 				
				CASE Rtrim(TIPOID) 
					WHEN '1' THEN 'RC - Registro Civil' WHEN '2' THEN 'TI - Tarjeta de Identidad' WHEN '3' THEN 'CC - Cédula de Ciudadanía' WHEN '4' THEN 'CE - Cédula de Extranjería' 
					WHEN '5' THEN 'PA - Pasaporte' WHEN '6' THEN 'MS - Menor Sin Identificación' WHEN '7' THEN 'AS - Adulto Sin Identificación' WHEN '8' THEN 'PE - Permiso Especial de Permanencia' 
					WHEN '9' THEN 'CN - Certificado de Nacido Vivo' WHEN '10' THEN 'CD - Carnet Diplomático' WHEN '11' THEN 'SC - Salvoconducto' WHEN '13' THEN 'DE - Documento Extranjero' 
					WHEN '14' THEN 'PT - Permiso Temporal de Permanencia'
				END AS 'TIPOID', 
				Rtrim(NUMIDEN) As 'NUMIDEN',
				Rtrim(EDAD) As 'EDAD', Rtrim(NUMHIJVIVOS) As 'NUMHIJVIVOS', Rtrim(NUMHIJMUERT) As 'NUMHIJMUERT', Rtrim(ULTIMOANIO) As 'ULTIMOANIO',
				CASE ESTADCONYU WHEN '1' THEN 'X' END AS 'Nocasamas', CASE ESTADCONYU WHEN '2' THEN 'X' END AS 'Nocasamenos', CASE ESTADCONYU WHEN '3' THEN 'X' END AS 'Divorciada',
				CASE ESTADCONYU WHEN '4' THEN 'X' END AS 'Viuda', CASE ESTADCONYU WHEN '5' THEN 'X' END AS 'Soltera', CASE ESTADCONYU WHEN '6' THEN 'X' END AS 'Casada',
				CASE ESTADCONYU WHEN '7' THEN 'X' END AS 'SinInfo',
				CASE NECROPSIA WHEN '1' THEN 'X' END AS 'NECROPSIA', CASE HISTCLINICA WHEN '1' THEN 'X' END AS 'HISTCLINICA', CASE PRUEBLAB WHEN '1' THEN 'X' END AS 'PRUEBLAB',
				CASE INTERROG WHEN '1' THEN 'X' END AS 'INTERROG', CASE ASISMED WHEN '1' THEN 'X' END AS 'ASISMED',
				Rtrim(CAUSAA) As 'CAUSAA', Rtrim(CAUSAB) As 'CAUSAB', Rtrim(CAUSAC) As 'CAUSAC',
				Rtrim(CAUSAD) As 'CAUSAD', Rtrim(OTROESTAD) As 'OTROESTAD', Rtrim(CAUSAPROB) As 'CAUSAPROB',
				CASE CLASIFIN WHEN '1' THEN 'X' END AS 'DNT', CASE CLASIFIN WHEN '2' THEN 'X' END AS 'IRA', CASE CLASIFIN WHEN '3' THEN 'X' END AS 'EDA', 
				VERSION AS 'VERSION', 
				JSON AS 'JSON' 
	
			From
				HCFICHA591

			Where
				IDFICHANOTIFICACION  = @IdFicha
		
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta y retorna el detalle completo de una ficha de notificación de muerte o defunción SIVIGILA (Ficha 591) identificada por su ID de ficha. Recupera información del fallecido como nombre, tipo y número de identificación, edad, número de hijos vivos y fallecidos, estado conyugal y lugar de defunción (hospital, domicilio, vía pública, entre otros). También expone las fuentes de confirmación diagnóstica (necropsia, historia clínica, pruebas de laboratorio, interrogatorio, asistencia médica), las causas de muerte en cadena (causa A, B, C, D), otros estados patológicos y la clasificación final del evento (desnutrición, IRA, EDA). Se usa para imprimir o visualizar la Ficha 591 del SIVIGILA en el contexto de vigilancia epidemiológica de muertes, apoyando el reporte obligatorio a las autoridades de salud pública.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila591';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila591';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera y formatea los datos de una ficha SIVIGILA 591 (mortalidad) para su despliegue/impresión, traduciendo códigos a marcas ''X'' y descripciones legibles.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila591';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA591 con IDFICHANOTIFICACION igual al identificador suministrado, de lo contrario el resultado será vacío', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila591';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retorna información de la ficha cuyo identificador coincide exactamente con el parámetro de entrada; Los campos categóricos se presentan como columnas booleanas marcadas con ''X'' (formato de impresión de ficha); Los valores de texto se devuelven sin espacios finales (RTRIM); No modifica datos: es una consulta de solo lectura', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila591';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación SIVIGILA 591; Mortalidad materna; Sitio de defunción; Estado conyugal; Tipo de identificación; Causas de muerte (CAUSAA-D); Clasificación final (DNT/IRA/EDA); Necropsia; Historia clínica; Hijos vivos / muertos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila591';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA591: Cuando IDFICHANOTIFICACION coincide con el identificador recibido, retorna un único conjunto con los datos de la ficha 591 transformados (sitio de defunción, tipo de identificación, estado conyugal, fuentes diagnósticas, causas y clasificación final)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila591';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Valor de SITIODEF (1..7) → Marca con ''X'' la columna correspondiente al sitio de defunción (Hospital, Centro, CasaDom, LugTrab, ViaPub, OtroSit, SinInformación); si Valor de TIPOID (1..14) → Traduce el código del tipo de identificación a su descripción estándar (RC, TI, CC, CE, PA, MS, AS, PE, CN, CD, SC, DE, PT); si Valor de ESTADCONYU (1..7) → Marca con ''X'' el estado conyugal (No casada hace más/menos, Divorciada, Viuda, Soltera, Casada, Sin información); si NECROPSIA / HISTCLINICA / PRUEBLAB / INTERROG / ASISMED = ''1'' → Marca con ''X'' la fuente de información usada para el diagnóstico de la defunción; si Valor de CLASIFIN (1..3) → Marca con ''X'' la clasificación final como DNT, IRA o EDA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila591';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA591', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila591';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila591';
-- GO
