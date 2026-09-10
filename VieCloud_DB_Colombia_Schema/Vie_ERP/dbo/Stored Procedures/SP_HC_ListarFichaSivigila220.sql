

-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,24-01-2019,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila220]
(
  @IdFicha as Int
)

AS
BEGIN
  SET NOCOUNT ON;

			Select
				CASE DESPLAZAMIENTO     WHEN '1' THEN 'X' END AS 'DESPLAZAMIENTO_SI',CASE DESPLAZAMIENTO  WHEN '0' THEN 'X' END AS 'DESPLAZAMIENTO_NO'
				,CASE WHEN U.AUUBICACI IS NOT NULL THEN CONCAT( RTRIM(Q.StandardCode), '-', RTRIM(Q.Name), ' / ', RTRIM(O.depcodigo), '-', RTRIM(O.nomdepart), ' / ', RTRIM(R.MUNCODIGO), '-', RTRIM(R.MUNNOMBRE) ) END as 'Municipio'
				,CASE SINTODENGUE     WHEN '1' THEN 'X' END AS 'SINTODENGUE_SI',CASE SINTODENGUE WHEN '2' THEN 'X' END AS 'SINTODENGUE_NO',CASE SINTODENGUE WHEN '3' THEN 'X' END AS 'SINTODENGUE_desconocido'
				,ESTABLECIMIENTO as 'NombreEstablecimiento'
				,CASE FIEBRE     WHEN '1' THEN 'X' END AS 'FIEBRE'
				,CASE CAFALEA     WHEN '1' THEN 'X' END AS 'CEFALEA'
				,CASE DOLORRECTROO      WHEN '1' THEN 'X' END AS 'DOLORRECTROO'
				,CASE MIALGIAS     WHEN '1' THEN 'X' END AS 'MIALGIAS'
				,CASE ARTRALGIAS     WHEN '1' THEN 'X' END AS 'ARTRALGIAS'
				,CASE ERUPCIONRASH     WHEN '1' THEN 'X' END AS 'ERUPCIONRASH'
				,CASE DOLORABDOMINAL     WHEN '1' THEN 'X' END AS 'DengueSigno_DOLORABDOMINAL'
				,CASE VOMITO     WHEN '1' THEN 'X' END AS 'DengueSigno_VOMITO'
				,CASE DIARREA     WHEN '1' THEN 'X' END AS 'DengueSigno_DIARREA'
				,CASE SOMNOLENCIA     WHEN '1' THEN 'X' END AS 'DengueSigno_SOMNOLENCIA'
				,CASE HIPOTENSION     WHEN '1' THEN 'X' END AS 'DengueSigno_HIPOTENSION'
				,CASE HEPATOMEGALIA     WHEN '1' THEN 'X' END AS 'DengueSigno_HEPATOMEGALIA'
				,CASE HEMORRAGIAS     WHEN '1' THEN 'X' END AS 'DengueSigno_HEMORRAGIAS'
				,CASE HIPOTERMIA     WHEN '1' THEN 'X' END AS 'DengueSigno_HIPOTERMIA'
				,CASE AUMENTOHEMAT     WHEN '1' THEN 'X' END AS 'DengueSigno_AUMENTOHEMAT'
				,CASE CAIDAPLAQUETAS     WHEN '1' THEN 'X' END AS 'DengueSigno_CAIDAPLAQUETAS'
				,CASE ACOMULACIONLIQUID     WHEN '1' THEN 'X' END AS 'DengueSigno_ACOMULACIONLIQUID'
				,CASE EXTRAVASACION     WHEN '1' THEN 'X' END AS 'DengueGrave_EXTRAVASACION'
				,CASE HEMOCOMPROMISO     WHEN '1' THEN 'X' END AS 'DengueGrave_HEMOCOMPROMISO'
				,CASE SHOCKDENGUE     WHEN '1' THEN 'X' END AS 'DengueGrave_SHOCKDENGUE'
				,CASE DAÑOORGANOS     WHEN '1' THEN 'X' END AS 'DengueGrave_DAÑOORGANOS'
				,CASE CLASIFICACIONFINAL     WHEN '0' THEN 'X' END AS 'ClasiFinal_NoAplica',CASE CLASIFICACIONFINAL WHEN '1' THEN 'X' END AS 'ClasiFinal_DengueSinSignos',CASE CLASIFICACIONFINAL WHEN '2' THEN 'X' END AS 'ClasiFinal_DengueConSignos',CASE CLASIFICACIONFINAL WHEN '3' THEN 'X' END AS 'ClasiFinal_DengueGrave'
				,CASE CONDUCTA     WHEN '0' THEN 'X' END AS 'CONDUCTA_NoAplica',CASE CONDUCTA WHEN '1' THEN 'X' END AS 'CONDUCTA_Ambulatoria',CASE CONDUCTA WHEN '2' THEN 'X' END AS 'CONDUCTA_Hospitalizacion',CASE CONDUCTA WHEN '4' THEN 'X' END AS 'CONDUCTA_Observacion',CASE CONDUCTA WHEN '5' THEN 'X' END AS 'CONDUCTA_Remision',CASE WHEN CONDUCTA = '3' OR CONDUCTA = '6' THEN 'X' end as 'CONDUCTA_UCI' 
				,CASE TEJIDOS     WHEN '1' THEN 'X' END AS 'Muestra_TEJIDOS'
				,CASE HIGADO     WHEN '1' THEN 'X' END AS 'Muestra_HIGADO'
				,CASE BRAZO     WHEN '1' THEN 'X' END AS 'Muestra_BRAZO'
				,CASE PULMON     WHEN '1' THEN 'X' END AS 'Muestra_PULMON'
				,CASE CEREBRO     WHEN '1' THEN 'X' END AS 'Muestra_CEREBRO'
				,CASE MIOCARDIO     WHEN '1' THEN 'X' END AS 'Muestra_MIOCARDIO'
				,CASE MEDULA     WHEN '1' THEN 'X' END AS 'Muestra_MEDULA'
				,CASE RINON     WHEN '1' THEN 'X' END AS 'Muestra_RINON'
				,CONVERT(varchar(10),FECHATOMA,103) as 'FechaToma'
				,CONVERT(varchar(10),FECHARECEPCION,103) as 'FechaRecepcion'
				,MUESTRA
				,PRUEBA
				,AGENTE
				,RESULTADO
				,CONVERT(varchar(10),FECHARESULTADO,103) as 'FechaResultado'
				,VALORREGISTRADO 
				,VERSION AS 'VERSION' 
				,JSON AS 'JSON' 

			From 
				HCFICHA220 F 
				LEFT JOIN INUBICACI  U on F.UBICACION = U.AUUBICACI 
				Left Join INMUNICIP R on U.DEPMUNCOD = R.DEPMUNCOD 
				Left Join INDEPARTA O on R.DEPCODIGO = O.DEPCODIGO 
				Left Join Common.Country Q ON O.IDPAIS = Q.Id 
	
			Where 
				IDFICHANOTIFICACION  = @IdFicha
		
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera el detalle completo de una ficha de notificación SIVIGILA código 220 (dengue) a partir de su identificador único. Compone la información clínica y epidemiológica del caso: síntomas (fiebre, cefalea, mialgias, artralgias, erupción, dolor abdominal, entre otros), signos de alarma de dengue grave, clasificación final del caso, conducta médica adoptada (ambulatoria, hospitalización, UCI, remisión), muestras de tejidos tomadas y resultados de laboratorio. Además, resuelve la ubicación geográfica del paciente cruzando la tabla de ubicaciones con los catálogos maestros de municipios, departamentos y países, construyendo una cadena legible con país, departamento y municipio. Se usa para imprimir o consultar la ficha SIVIGILA de dengue en el módulo de historia clínica y vigilancia epidemiológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila220';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila220';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los datos de una ficha SIVIGILA de notificación de Dengue formateados para reporte, transformando códigos numéricos en marcas ''X'' y resolviendo la ubicación geográfica país/departamento/municipio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila220';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA220 con IDFICHANOTIFICACION igual al parámetro recibido; Las tablas de catálogo de ubicación (INUBICACI, INMUNICIP, INDEPARTA) y Common.Country deben estar disponibles para resolver la jerarquía geográfica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila220';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los valores binarios (1) se transforman a marca ''X'' para representación en formulario impreso/reporte; Las conductas con códigos 3 y 6 se unifican en una sola categoría UCI; Las fechas se devuelven en formato dd/mm/yyyy (estilo 103); Solo se devuelve la ficha cuyo IDFICHANOTIFICACION coincide con el parámetro; La información geográfica solo se construye si existe ubicación asociada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila220';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación SIVIGILA; Vigilancia epidemiológica de Dengue; Signos y síntomas de dengue; Dengue grave; Clasificación final del caso; Conducta clínica (ambulatoria, hospitalización, UCI, observación, remisión); Muestras de tejidos para análisis; Ubicación geográfica (país/departamento/municipio); Desplazamiento del paciente; Resultados de laboratorio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila220';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA220: Cuando IDFICHANOTIFICACION = @IdFicha, devuelve un resultset con los datos de la ficha de Dengue formateados para impresión (síntomas, signos de alarma, signos de dengue grave, clasificación final, conducta, muestras, resultados de laboratorio, ubicación)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila220';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CLASIFICACIONFINAL en {0,1,2,3} → Marca ''X'' en NoAplica, DengueSinSignos, DengueConSignos o DengueGrave respectivamente; si CONDUCTA = 3 o CONDUCTA = 6 → Marca ''X'' en CONDUCTA_UCI (ambos códigos se consolidan en UCI); si CONDUCTA en {0,1,2,4,5} → Marca ''X'' en NoAplica, Ambulatoria, Hospitalización, Observación o Remisión respectivamente; si SINTODENGUE en {1,2,3} → Marca ''X'' en SI, NO o desconocido respectivamente; si U.AUUBICACI IS NOT NULL → Construye string Municipio concatenando País, Departamento y Municipio else Municipio queda NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila220';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA220; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA; Common.Country', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila220';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila220';
-- GO
