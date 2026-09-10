

-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,08-05-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila210]
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

			from 
				HCFICHA210 F 
				LEFT JOIN INUBICACI  U on F.UBICACION = U.AUUBICACI				
				Left Join INMUNICIP R on U.DEPMUNCOD = R.DEPMUNCOD 
				Left Join INDEPARTA O on R.DEPCODIGO = O.DEPCODIGO 
				Left Join Common.Country Q ON O.IDPAIS = Q.Id 
				
			where 
				IDFICHANOTIFICACION  = @IdFicha
		
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento almacenado que recupera el detalle completo de una ficha de notificación SIVIGILA formulario 210 (dengue) dado su identificador. Consolida la información clínica del caso: síntomas iniciales (fiebre, cefalea, mialgias, artralgias, erupción, dolor retroocular), signos de alarma (vómito, diarrea, somnolencia, hipotensión, hepatomegalia, hemorragias, caída de plaquetas, acumulación de líquidos), criterios de dengue grave (extravasación, compromiso hemodinámico, shock, daño de órganos), clasificación final del caso y conducta médica adoptada (ambulatoria, hospitalización, UCI, remisión u observación). Integra la ubicación geográfica completa del paciente cruzando las tablas de ubicaciones, municipios, departamentos y países para construir la cadena país-departamento-municipio. Incluye además los datos de muestras de laboratorio tomadas por órgano (hígado, pulmón, cerebro, riñón, miocardio, médula, tejidos), fechas de toma, recepción y resultado, así como la prueba realizada, el agente identificado y el resultado obtenido. Se usa para imprimir o visualizar la ficha epidemiológica oficial de dengue en el módulo de historia clínica y vigilancia en salud pública.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila210';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila210';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera y formatea los datos de una ficha Sivigila (caso de dengue) transformando códigos numéricos en marcas tipo ''X'' para impresión/visualización del formato oficial.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila210';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA210 con el identificador de ficha de notificación solicitado; Las tablas de catálogo geográfico (INUBICACI, INMUNICIP, INDEPARTA, Common.Country) deben estar pobladas para resolver el municipio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila210';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las casillas tipo checklist se representan con ''X'' cuando la condición se cumple y NULL en caso contrario; Los códigos de conducta ''3'' y ''6'' se consolidan ambos en la categoría UCI; Las fechas (toma, recepción, resultado) se entregan en formato dd/mm/yyyy (estilo 103); La ubicación geográfica solo se arma cuando existe ubicación asociada en INUBICACI; Solo se retornan datos de la ficha cuyo identificador coincide con el parámetro recibido', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila210';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación Sivigila; Dengue (signos, signos de alarma, dengue grave); Clasificación final de caso; Conducta clínica (ambulatoria, hospitalización, observación, remisión, UCI); Muestras de tejidos para laboratorio; Ubicación geográfica (país/departamento/municipio); Desplazamiento del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila210';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ?: Devuelve un único resultado con los datos de la ficha cuando IDFICHANOTIFICACION = @IdFicha', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila210';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si DESPLAZAMIENTO = ''1'' / ''0'' → Marca con ''X'' la casilla SI o NO de desplazamiento; si SINTODENGUE = ''1'' / ''2'' / ''3'' → Marca SI / NO / Desconocido respectivamente para síntomas de dengue; si CLASIFICACIONFINAL = ''0'',''1'',''2'',''3'' → Mapea a NoAplica, Dengue sin signos, Dengue con signos, Dengue grave; si CONDUCTA = ''0'',''1'',''2'',''4'',''5'' → Mapea a NoAplica, Ambulatoria, Hospitalización, Observación, Remisión; si CONDUCTA = ''3'' OR CONDUCTA = ''6'' → Marca casilla ''UCI'' (ambos códigos consolidan en UCI); si U.AUUBICACI IS NOT NULL → Construye cadena ''Municipio'' concatenando país, departamento y municipio; en caso contrario queda NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila210';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA210; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA; Common.Country', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila210';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila210';
-- GO
