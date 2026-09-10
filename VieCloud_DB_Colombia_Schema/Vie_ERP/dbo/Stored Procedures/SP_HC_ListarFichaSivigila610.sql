-- Stored Procedure
-- =============================================
-- Autor:		Juan David Patiño Cabrera
-- Fecha Creacion: 08-05-2018
-- Descripcion:	Sp que me lista la Información de las Fichas del Sivigila, Ficha 610
-- Modifico:    Yezid Garcia Medina
-- Fecha Modificación : 19-01-2022
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila610]
(
  @IdFicha as Int
)

AS
BEGIN
  SET NOCOUNT ON;
  
			Select 	
				 NOMMADREPAC as 'NombreMadre'	
				,NOMPADREPAC as 'NombrePadre'
				,convert(varchar(10),FECHAINVES,103) as 'FechaInvestigacion'
				,DOSISVOP as 'NumeroDosisVOP'
				,DOSISCIP as 'NumeroDosisVIP'
				,convert(varchar(10),FECHAINVES,103) as 'FechaUltimaDosis'
				,CASE CARNET  WHEN '1' THEN 'X' END AS 'Carnet_SI',CASE CARNET WHEN '2' THEN 'X' END AS 'Carnet_NO',CASE CARNET WHEN '3' THEN 'X' END AS 'Carnet_Desconocido'
				,CASE FIEBRE  WHEN '1' THEN 'X' END AS 'FIEBRE_SI',CASE FIEBRE WHEN '2' THEN 'X' END AS 'FIEBRE_NO',CASE FIEBRE WHEN '3' THEN 'X' END AS 'FIEBRE_Desconocido'
				,CASE RESPIRATORIO WHEN '1' THEN 'X' END AS 'RESPIRATORIO_SI',CASE RESPIRATORIO WHEN '2' THEN 'X' END AS 'RESPIRATORIO_NO',CASE RESPIRATORIO WHEN '3' THEN 'X' END AS 'RESPIRATORIO_Desconocido'
				,CASE DIGESTIVOS  WHEN '1' THEN 'X' END AS 'DIGESTIVOS_SI',CASE DIGESTIVOS WHEN '2' THEN 'X' END AS 'DIGESTIVOS_NO',CASE DIGESTIVOS WHEN '3' THEN 'X' END AS 'DIGESTIVOS_Desconocido'
				,CASE DOLORMUSCULAR   WHEN '1' THEN 'X' END AS 'DOLORMUSCULAR_SI',CASE DOLORMUSCULAR WHEN '2' THEN 'X' END AS 'DOLORMUSCULAR_NO',CASE DOLORMUSCULAR WHEN '3' THEN 'X' END AS 'DOLORMUSCULAR_Desconocido'
				,CASE SIGNOSMEN    WHEN '1' THEN 'X' END AS 'SIGNOSMEN_SI',CASE SIGNOSMEN WHEN '2' THEN 'X' END AS 'SIGNOSMEN_NO',CASE SIGNOSMEN WHEN '3' THEN 'X' END AS 'SIGNOSMEN_Desconocido'
				,CASE FIEBREPARALI    WHEN '1' THEN 'X' END AS 'FIEBREPARALI_SI',CASE FIEBREPARALI WHEN '2' THEN 'X' END AS 'FIEBREPARALI_NO',CASE FIEBREPARALI WHEN '3' THEN 'X' END AS 'FIEBREPARALI_Desconocido'
				,INSTALACION 
				,CASE PROGRESION     WHEN '1' THEN 'X' END AS 'PROGRESION_Ascendente',CASE PROGRESION WHEN '2' THEN 'X' END AS 'PROGRESION_Desce',CASE PROGRESION WHEN '3' THEN 'X' END AS 'PROGRESION_Indeter'
				,convert(varchar(10),FECHAINVES,103) as 'FechaInicioParalisis'
				,CASE MSDPARESIA   WHEN '1' THEN 'X' END AS 'SUPERIORDERECHO_PARESIA _SI',CASE MSDPARESIA   WHEN '0' THEN 'X' END AS 'SUPERIORDERECHO_PARESIA _NO'
				,CASE MSDPARALISIS    WHEN '1' THEN 'X' END AS 'SUPERIORDERECHO_PARALISIS_SI',CASE MSDPARALISIS  WHEN '0' THEN 'X' END AS 'SUPERIORDERECHO_PARALISIS_NO'
				,CASE MSDFLACIDA    WHEN '1' THEN 'X' END AS 'SUPERIORDERECHO_FLACIDA_SI',CASE MSDFLACIDA  WHEN '0' THEN 'X' END AS 'SUPERIORDERECHO_FLACIDA_NO'
				,CASE MSDLOCALIZA    WHEN '1' THEN 'X' END AS 'SUPERIORDERECHO_lOCALIZACION_PROXIMAL',CASE MSDLOCALIZA  WHEN '2' THEN 'X' END AS 'SUPERIORDERECHO_lOCALIZACION_DISTAL'
				,CASE MSDSENSIBILIDAD     WHEN '1' THEN 'X' END AS 'SUPERIORDERECHO_SENSIBILIDAD_N',CASE MSDSENSIBILIDAD   WHEN '2' THEN 'X' END AS 'SUPERIORDERECHO_SENSIBILIDAD_A',CASE MSDSENSIBILIDAD   WHEN '3' THEN 'X' END AS 'SUPERIORDERECHO_SENSIBILIDAD_D'
				,CASE MSDROT      WHEN '1' THEN 'X' END AS 'SUPERIORDERECHO_ROT_N',CASE MSDROT   WHEN '2' THEN 'X' END AS 'SUPERIORDERECHO_ROT_A',CASE MSDROT   WHEN '3' THEN 'X' END AS 'SUPERIORDERECHO_ROT_D'
				,CASE MSIPARESIA   WHEN '1' THEN 'X' END AS 'SUPERIORIZQUIERDO_MSDPARESIA _SI',CASE MSIPARESIA   WHEN '0' THEN 'X' END AS 'SUPERIORIZQUIERDO_MSDPARESIA _NO'
				,CASE MSIPARALISIS    WHEN '1' THEN 'X' END AS 'SUPERIORIZQUIERDO_PARALISIS_SI',CASE MSIPARALISIS  WHEN '0' THEN 'X' END AS 'SUPERIORIZQUIERDO_PARALISIS_NO'
				,CASE MSIFLACIDA    WHEN '1' THEN 'X' END AS 'SUPERIORIZQUIERDO_FLACIDA_SI',CASE MSIFLACIDA  WHEN '0' THEN 'X' END AS 'SUPERIORIZQUIERDO_FLACIDA_NO'
				,CASE MSILOCALIZA    WHEN '1' THEN 'X' END AS 'SUPERIORIZQUIERDO_lOCALIZACION_PROXIMAL',CASE MSILOCALIZA  WHEN '2' THEN 'X' END AS 'SUPERIORIZQUIERDO_lOCALIZACION_DISTAL'
				,CASE MSISENSIBILIDAD     WHEN '1' THEN 'X' END AS 'SUPERIORIZQUIERDO_SENSIBILIDAD_N',CASE MSISENSIBILIDAD   WHEN '2' THEN 'X' END AS 'SUPERIORIZQUIERDO_SENSIBILIDAD_A',CASE MSISENSIBILIDAD   WHEN '3' THEN 'X' END AS 'SUPERIORIZQUIERDO_SENSIBILIDAD_D'
				,CASE MSIROT      WHEN '1' THEN 'X' END AS 'SUPERIORIZQUIERDO_ROT_N',CASE MSIROT   WHEN '2' THEN 'X' END AS 'SUPERIORIZQUIERDO_ROT_A',CASE MSIROT   WHEN '3' THEN 'X' END AS 'SUPERIORIZQUIERDO_ROT_D'
				,CASE MIDPARECIA   WHEN '1' THEN 'X' END AS 'INFERIORDERECHO_PARESIA _SI',CASE MIDPARECIA   WHEN '0' THEN 'X' END AS 'INFERIORDERECHO_PARESIA _NO'
				,CASE MIDPARALISIS    WHEN '1' THEN 'X' END AS 'INFERIORDERECHO_PARALISIS_SI',CASE MIDPARALISIS  WHEN '0' THEN 'X' END AS 'INFERIORDERECHO_PARALISIS_NO'
				,CASE MIDFLACIDA    WHEN '1' THEN 'X' END AS 'INFERIORDERECHO_FLACIDA_SI',CASE MIDFLACIDA  WHEN '0' THEN 'X' END AS 'INFERIORDERECHO_FLACIDA_NO'
				,CASE MIDLOCALIZA    WHEN '1' THEN 'X' END AS 'INFERIORDERECHO_lOCALIZACION_PROXIMAL',CASE MIDLOCALIZA  WHEN '2' THEN 'X' END AS 'INFERIORDERECHO_lOCALIZACION_DISTAL'
				,CASE MIDSENSIBILIDAD     WHEN '1' THEN 'X' END AS 'INFERIORDERECHO_SENSIBILIDAD_N',CASE MIDSENSIBILIDAD   WHEN '2' THEN 'X' END AS 'INFERIORDERECHO_SENSIBILIDAD_A',CASE MIDSENSIBILIDAD   WHEN '3' THEN 'X' END AS 'INFERIORDERECHO_SENSIBILIDAD_D'
				,CASE MIDROT      WHEN '1' THEN 'X' END AS 'INFERIORDERECHO_ROT_N',CASE MIDROT   WHEN '2' THEN 'X' END AS 'INFERIORDERECHO_ROT_A',CASE MIDROT   WHEN '3' THEN 'X' END AS 'INFERIORDERECHO_ROT_D'
				,CASE MIZPARESIA   WHEN '1' THEN 'X' END AS 'INFERIORIZQUIERDO_MSDPARESIA _SI',CASE MIZPARESIA   WHEN '0' THEN 'X' END AS 'INFERIORIZQUIERDO_MSDPARESIA _NO'
				,CASE MIZPARALISIS    WHEN '1' THEN 'X' END AS 'INFERIORIZQUIERDO_PARALISIS_SI',CASE MIZPARALISIS  WHEN '0' THEN 'X' END AS 'INFERIORIZQUIERDO_PARALISIS_NO'
				,CASE MIZFLACIDA    WHEN '1' THEN 'X' END AS 'INFERIORIZQUIERDO_FLACIDA_SI',CASE MIZFLACIDA  WHEN '0' THEN 'X' END AS 'INFERIORIZQUIERDO_FLACIDA_NO'
				,CASE MIZLOCALIZA    WHEN '1' THEN 'X' END AS 'INFERIORIZQUIERDO_lOCALIZACION_PROXIMAL',CASE MIZLOCALIZA  WHEN '2' THEN 'X' END AS 'INFERIORIZQUIERDO_lOCALIZACION_DISTAL'
				,CASE MIZSENSIBILIDAD     WHEN '1' THEN 'X' END AS 'INFERIORIZQUIERDO_SENSIBILIDAD_N',CASE MIZSENSIBILIDAD   WHEN '2' THEN 'X' END AS 'INFERIORIZQUIERDO_SENSIBILIDAD_A',CASE MIZSENSIBILIDAD   WHEN '3' THEN 'X' END AS 'INFERIORIZQUIERDO_SENSIBILIDAD_D'
				,CASE MIZROT      WHEN '1' THEN 'X' END AS 'INFERIORIZQUIERDO_ROT_N',CASE MIZROT   WHEN '2' THEN 'X' END AS 'INFERIORIZQUIERDO_ROT_A',CASE MIZROT   WHEN '3' THEN 'X' END AS 'INFERIORIZQUIERDO_ROT_D'
				,CASE MUSCULOSRESPI    WHEN '1' THEN 'X' END AS 'MUSCULORESPI_SI',CASE MUSCULOSRESPI WHEN '2' THEN 'X' END AS 'MUSCULORESPI_NO',CASE MUSCULOSRESPI WHEN '3' THEN 'X' END AS 'MUSCULORESPI_Desconocido'
				,CASE SIGNOSMENI2    WHEN '1' THEN 'X' END AS 'SIGNOSMENI2_SI',CASE SIGNOSMENI2 WHEN '2' THEN 'X' END AS 'SIGNOSMENI2_NO',CASE SIGNOSMENI2 WHEN '3' THEN 'X' END AS 'SIGNOSMENI2_Desconocido'
				,CASE BABINSKY    WHEN '1' THEN 'X' END AS 'BABINSKY_SI',CASE BABINSKY WHEN '2' THEN 'X' END AS 'BABINSKY_NO',CASE BABINSKY WHEN '3' THEN 'X' END AS 'BABINSKY_Desconocido'
				,CASE BRUDZINSKY    WHEN '1' THEN 'X' END AS 'BRUDZINSKY_SI',CASE BRUDZINSKY WHEN '2' THEN 'X' END AS 'BRUDZINSKY_NO',CASE BRUDZINSKY WHEN '3' THEN 'X' END AS 'BRUDZINSKY_Desconocido'
				,CASE PARESCRANEA    WHEN '1' THEN 'X' END AS 'PARESCRANEA_SI',CASE PARESCRANEA WHEN '2' THEN 'X' END AS 'PARESCRANEA_NO',CASE PARESCRANEA WHEN '3' THEN 'X' END AS 'PARESCRANEA_Desconocido'
				,CASE LIQUIDOCEFA    WHEN '1' THEN 'X' END AS 'LIQUIDOCEFA_SI',CASE LIQUIDOCEFA WHEN '2' THEN 'X' END AS 'LIQUIDOCEFA_NO',CASE LIQUIDOCEFA WHEN '3' THEN 'X' END AS 'LIQUIDOCEFA_Desconocido'
				,CASE ELECTROMIO    WHEN '1' THEN 'X' END AS 'ELECTROMIO_SI',CASE ELECTROMIO WHEN '2' THEN 'X' END AS 'ELECTROMIO_NO',CASE ELECTROMIO WHEN '3' THEN 'X' END AS 'ELECTROMIO_Desconocido'
				,CASE VELOCIDADCOND    WHEN '1' THEN 'X' END AS 'VELOCIDADCOND_SI',CASE VELOCIDADCOND WHEN '2' THEN 'X' END AS 'VELOCIDADCOND_NO',CASE VELOCIDADCOND WHEN '3' THEN 'X' END AS 'VELOCIDADCOND_Desconocido'
				,DIAGNOINICIAL + ' - ' + rtrim(D.NOMDIAGNO)  as 'DiagnosticoInicial' 
				,CASE TOMAMUESTRA    WHEN '1' THEN 'X' END AS 'TOMAMUESTRA_SI',CASE TOMAMUESTRA WHEN '2' THEN 'X' END AS 'TOMAMUESTRA_NO',CASE TOMAMUESTRA WHEN '3' THEN 'X' END AS 'TOMAMUESTRA_Desconocido'
				,convert(varchar(10),FECHATOMA,103) as 'FechaToma'
				,convert(varchar(10),FECHAENVIO,103) as 'FechaEnvio'
				,convert(varchar(10),FECHARECEP,103) as 'FechaRecepcion'
				,convert(varchar(10),FECHARESULT,103) as 'FechaResultado'
				,CASE RESULTADO    WHEN '1' THEN 'X' END AS 'Resul_Negativo',CASE RESULTADO WHEN '2' THEN 'X' END AS 'Resul_EnteroVirus',CASE RESULTADO WHEN '3' THEN 'X' END AS 'Resul_Polo_Salvaje_1',CASE RESULTADO    WHEN '4' THEN 'X' END AS 'Resul_Polo_Salvaje_2',CASE RESULTADO WHEN '5' THEN 'X' END AS 'Resul_Polo_Salvaje_3',CASE RESULTADO WHEN '6' THEN 'X' END AS 'Resul_Polo_vacunal_1',CASE RESULTADO    WHEN '7' THEN 'X' END AS 'Resul_Polo_vacunal_2',CASE RESULTADO WHEN '8' THEN 'X' END AS 'Resul_Polo_vacunal_3',CASE RESULTADO WHEN '9' THEN 'X' END AS 'Resul_DerivadoVacuna1'
				,convert(varchar(10),FECHAVACUBLO ,103) as 'FechaVacunacionBloqueo'
				,CASE CASODETEC  WHEN '1' THEN 'X' END AS 'Caso_Consulta',CASE CASODETEC WHEN '2' THEN 'X' END AS 'Caso_Laboratorio',CASE CASODETEC WHEN '3' THEN 'X' END AS 'Caso_Institucional',CASE CASODETEC    WHEN '4' THEN 'X' END AS 'Caso_comunitaria',CASE CASODETEC WHEN '5' THEN 'X' END AS 'Caso_Contactos',CASE CASODETEC WHEN '6' THEN 'X' END AS 'Caso_Comunidad',CASE CASODETEC    WHEN '7' THEN 'X' END AS 'Caso_Otros',CASE CASODETEC    WHEN '8' THEN 'X' END AS 'Caso_Desconocido'
				,convert(varchar(10),FECHASEG60  ,103) as 'FechaSeguimineto60dias'
				,CASE PARALISIS60   WHEN '1' THEN 'X' END AS 'PARALISIS60_SI',CASE PARALISIS60  WHEN '2' THEN 'X' END AS 'PARALISIS60_NO',CASE PARALISIS60  WHEN '3' THEN 'X' END AS 'PARALISIS60_Desconocido'
				,CASE ARTROFIA60    WHEN '1' THEN 'X' END AS 'ARTROFIA60_SI',CASE ARTROFIA60  WHEN '2' THEN 'X' END AS 'ARTROFIA60_NO',CASE ARTROFIA60  WHEN '3' THEN 'X' END AS 'ARTROFIA60_Desconocido'
				,CASE CLASIFINAL   WHEN '1' THEN 'X' END AS 'ClasificaFinal_Salvaje',CASE CLASIFINAL WHEN '2' THEN 'X' END AS 'ClasificaFinal_Derivado',CASE CLASIFINAL WHEN '3' THEN 'X' END AS 'ClasificaFinal_Asociado',CASE CLASIFINAL    WHEN '4' THEN 'X' END AS 'ClasificaFinal_Compatible',CASE CLASIFINAL WHEN '5' THEN 'X' END AS 'ClasificaFinal_Descartado'
				,convert(varchar(10),FECHACLASI  ,103) as 'Fechaclasificacion'
				,CASE CRITERIOCLASI    WHEN '1' THEN 'X' END AS 'CriterioClasifi_Laboratorio',CASE CRITERIOCLASI WHEN '2' THEN 'X' END AS 'CriterioClasifi_Perdido',CASE CRITERIOCLASI WHEN '3' THEN 'X' END AS 'CriterioClasifi_Defuncion',CASE CRITERIOCLASI    WHEN '4' THEN 'X' END AS 'CriterioClasifi_ParalisisResidual',CASE CRITERIOCLASI WHEN '5' THEN 'X' END AS 'CriterioClasifi_SinParalisisResidual',CASE CRITERIOCLASI WHEN '6' THEN 'X' END AS 'CriterioClasifi_OtroDiagnosticoClinico'
				,DIAGNOFINAL + ' - ' + rtrim(D.NOMDIAGNO)  as 'DiagnosticoFinal', 
				VERSION AS 'VERSION', 
				JSON AS 'JSON' 
			From 
				HCFICHA610 F
				LEFT JOIN INDIAGNOS D on F.DIAGNOINICIAL = D.CODDIAGNO 
				LEFT JOIN INDIAGNOS D2 on F.DIAGNOFINAL = D2.CODDIAGNO 
			where 
				IDFICHANOTIFICACION  = @IdFicha	
		
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera el detalle completo de la ficha epidemiológica SIVIGILA 610, correspondiente a la notificación de Parálisis Flácida Aguda (polio y eventos neurológicos similares). A partir del identificador de la ficha, obtiene datos del paciente (nombre del padre y la madre), información de vacunación (número de dosis VOP y VIP, estado del carnet), síntomas clínicos presentes (fiebre, síntomas respiratorios, digestivos, dolor muscular, signos meníngeos, fiebre con parálisis), características de la parálisis (instalación, progresión ascendente/descendente/indeterminada) y el examen neurológico detallado de los cuatro miembros (superior derecho e izquierdo, inferior derecho e izquierdo), evaluando paresia, parálisis, flacidez, localización proximal/distal, sensibilidad y reflejos osteotendinosos. Cruza con el catálogo de diagnósticos CIE-10 (INDIAGNOS) para obtener los nombres del diagnóstico inicial y final del caso. Se usa para imprimir o consultar la ficha oficial de vigilancia epidemiológica obligatoria exigida por el SIVIGILA ante casos de parálisis flácida en menores.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila610';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila610';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera y formatea para presentación los datos clínicos y epidemiológicos de la ficha 610 del Sivigila (parálisis flácida aguda/poliomielitis) asociada a una notificación específica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila610';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA610 con el identificador de ficha de notificación recibido; Los códigos de diagnóstico inicial y final deben corresponder con CODDIAGNO de INDIAGNOS para resolver su descripción', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila610';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las fechas siempre se devuelven en formato dd/mm/yyyy (estilo 103); Para cada campo categórico solo una de las columnas SI/NO/Desconocido (o equivalentes) puede contener ''X''; las demás quedan NULL; El diagnóstico inicial y final se devuelven concatenados como ''CODIGO - NOMBRE''; si no hay coincidencia en INDIAGNOS, el nombre queda nulo y la concatenación produce NULL; Los nombres de diagnóstico se entregan sin espacios finales (RTRIM); El procedimiento es de solo lectura: no realiza modificaciones de datos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila610';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Sivigila; Ficha 610; Parálisis flácida aguda; Poliomielitis; Vacuna VOP; Vacuna VIP; Signos meníngeos; Babinsky; Brudzinsky; Electromiografía; Velocidad de conducción; Líquido cefalorraquídeo; Diagnóstico inicial; Diagnóstico final; Clasificación final del caso; Criterio de clasificación; Seguimiento a 60 días; Vacunación de bloqueo; Toma de muestra; Polio salvaje; Polio vacunal; Derivado de vacuna', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila610';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHA610: Devuelve una sola fila con los datos clínicos de la ficha 610 filtrando por IDFICHANOTIFICACION = parámetro de entrada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila610';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Valor de campo categórico (CARNET, FIEBRE, RESPIRATORIO, DIGESTIVOS, DOLORMUSCULAR, SIGNOSMEN, FIEBREPARALI, MUSCULOSRESPI, SIGNOSMENI2, BABINSKY, BRUDZINSKY, PARESCRANEA, LIQUIDOCEFA, ELECTROMIO, VELOCIDADCOND, TOMAMUESTRA, PARALISIS60, ARTROFIA60) = ''1'' / ''2'' / ''3'' → Marca con ''X'' la columna SI / NO / Desconocido respectivamente; si PROGRESION = ''1'' / ''2'' / ''3'' → Marca progresión Ascendente / Descendente / Indeterminada; si Campos de exploración por miembro (PARESIA, PARALISIS, FLACIDA) = ''1'' o ''0'' → Marca SI cuando es 1 y NO cuando es 0; si Campos de localización por miembro (LOCALIZA) = ''1'' / ''2'' → Marca Proximal / Distal; si Campos de SENSIBILIDAD y ROT por miembro = ''1'' / ''2'' / ''3'' → Marca Normal / Anormal / Disminuido (N/A/D); si RESULTADO entre ''1'' y ''9'' → Marca el tipo de resultado virológico: Negativo, EnteroVirus, Polio salvaje 1/2/3, Polio vacunal 1/2/3, Derivado de vacuna; si CASODETEC entre ''1'' y ''8'' → Marca la forma de detección del caso: Consulta, Laboratorio, Institucional, Comunitaria, Contactos, Comunidad, Otros, Desconocido; si CLASIFINAL entre ''1'' y ''5'' → Marca clasificación final: Salvaje / Derivado / Asociado / Compatible / Descartado; si CRITERIOCLASI entre ''1'' y ''6'' → Marca criterio de clasificación: Laboratorio, Perdido, Defunción, Parálisis residual, Sin parálisis residual, Otro diagnóstico clínico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila610';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA610; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila610';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila610';
-- GO
