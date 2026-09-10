

-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,08-05-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila580]
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
				HCFICHA580 F 
				LEFT JOIN INUBICACI  U on F.UBICACION = U.AUUBICACI 
				Left Join INMUNICIP R on U.DEPMUNCOD = R.DEPMUNCOD 
				Left Join INDEPARTA O on R.DEPCODIGO = O.DEPCODIGO 
				Left Join Common.Country Q ON O.IDPAIS = Q.Id 

			Where 
				IDFICHANOTIFICACION  = @idficha
		
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera el detalle completo de una ficha de notificación obligatoria de dengue (SIVIGILA ficha 580) a partir de su identificador único. Compone la información clínica del caso —síntomas generales (fiebre, cefalea, mialgias, artralgias, erupción), signos de alarma (vómito, diarrea, hipotensión, hepatomegalia, hemorragias, caída de plaquetas, acumulación de líquidos) y criterios de dengue grave (extravasación, shock, daño de órganos)— junto con la clasificación clínica final y la conducta médica adoptada (ambulatoria, hospitalización, UCI, remisión, observación). Integra además la ubicación geográfica del caso cruzando las tablas de ubicación, municipio, departamento y país (incluyendo código ISO del país), los órganos con muestras tomadas para laboratorio y los resultados de las pruebas diagnósticas con sus fechas de toma, recepción y resultado. Se usa para imprimir o consultar la ficha epidemiológica de un caso de dengue notificado al sistema SIVIGILA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila580';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila580';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera y formatea, para una ficha de notificación dada, los datos epidemiológicos de la ficha 580 de Sivigila (dengue): ubicación geográfica, signos/síntomas, clasificación final, conducta, muestras y resultados de laboratorio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila580';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA580 cuyo IDFICHANOTIFICACION coincida con el parámetro de entrada; Para resolver la ubicación geográfica completa (País/Departamento/Municipio) la ficha debe tener UBICACION asociada en INUBICACI con relaciones válidas a INMUNICIP, INDEPARTA y Common.Country', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila580';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada síntoma/signo/muestra solo se marca con ''X'' cuando su campo vale ''1''; cualquier otro valor produce NULL en la columna correspondiente; Las fechas FECHATOMA, FECHARECEPCION y FECHARESULTADO se devuelven siempre formateadas como dd/mm/yyyy (estilo 103); Los códigos de conducta 3 y 6 se tratan como equivalentes a UCI; El procedimiento es de solo lectura: no realiza cambios sobre los datos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila580';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha Sivigila 580; Notificación epidemiológica; Dengue; Signos de alarma del dengue; Dengue grave; Clasificación final del caso; Conducta clínica (ambulatoria, hospitalización, observación, remisión, UCI); Muestras de tejidos para vigilancia (hígado, pulmón, cerebro, miocardio, médula, riñón, brazo); Desplazamiento del paciente; Ubicación geográfica País/Departamento/Municipio; Resultado de laboratorio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila580';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHA580: Devuelve un único conjunto de resultados con los datos de la ficha Sivigila 580 filtrada por IDFICHANOTIFICACION = @IdFicha, transformando códigos numéricos en marcas ''X'' por casilla.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila580';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si DESPLAZAMIENTO = ''1'' / ''0'' → Marca ''X'' en DESPLAZAMIENTO_SI o DESPLAZAMIENTO_NO respectivamente; si U.AUUBICACI IS NOT NULL → Concatena País-Departamento-Municipio como cadena ''Municipio'' else Devuelve NULL en Municipio; si SINTODENGUE = ''1'' / ''2'' / ''3'' → Marca ''X'' en SINTODENGUE_SI, SINTODENGUE_NO o SINTODENGUE_desconocido respectivamente; si CLASIFICACIONFINAL = ''0'',''1'',''2'',''3'' → Marca la clasificación final como NoAplica, DengueSinSignos, DengueConSignos o DengueGrave; si CONDUCTA = ''0'',''1'',''2'',''4'',''5'' → Marca NoAplica, Ambulatoria, Hospitalización, Observación o Remisión; si CONDUCTA = ''3'' OR CONDUCTA = ''6'' → Marca ''X'' en CONDUCTA_UCI (ambos códigos se consolidan como UCI)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila580';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA580; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA; Common.Country', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila580';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila580';
-- GO
