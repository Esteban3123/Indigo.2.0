-- Stored Procedure
-- =============================================
-- Autor:		Juan David Patiño Cabrera
-- Fecha Creacion: 08-05-2018
-- Descripcion:	Sp que me lista la Información de las Fichas del Sivigila, Ficha 605
-- Modifico:    Yezid Garcia Medina
-- Fecha Modificación : 31-12-2021
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila605]
(
  @IdFicha as Int
)

AS
BEGIN
  SET NOCOUNT ON;
	
			Select 	
				CASE VACUNAROTAVIRUS WHEN '1' THEN 'X' END AS 'VACUNAROTAVIRUS_SI',CASE VACUNAROTAVIRUS  WHEN '2' THEN 'X' END AS 'VACUNAROTAVIRUS_NO',CASE VACUNAROTAVIRUS  WHEN '3' THEN 'X' END AS 'VACUNAROTAVIRUS_DESCONOCIDO'
				,convert(varchar(10),FECHAPLIDOSIS1,103) as 'FechaAplicacion1'
				,convert(varchar(10),FECHAPLIDOSIS2,103) as 'FechaAplicacion2'
				,CASE TIENECARNET WHEN '1' THEN 'X' END AS 'TIENECARNET_SI',CASE TIENECARNET  WHEN '0' THEN 'X' END AS 'TIENECARNET_NO'
				,PESONACER
				,CASE LECHEMATERNA  WHEN '1' THEN 'X' END AS 'LECHEMATERNA_SI',CASE LECHEMATERNA WHEN '2' THEN 'X' END AS 'LECHEMATERNA_NO',CASE LECHEMATERNA WHEN '3' THEN 'X' END AS 'LECHEMATERNA_DESCONOCIDO'
				,TIEMPOLECHE
				,CASE ALIMENACTUAL  WHEN '1' THEN 'X' END AS 'AlimentoActual_Materna',CASE ALIMENACTUAL WHEN '2' THEN 'X' END AS 'AlimentoActual_Artificial',CASE ALIMENACTUAL WHEN '3' THEN 'X' END AS 'AlimentoActual_Mixta',CASE ALIMENACTUAL WHEN '4' THEN 'X' END AS 'AlimentoActual_Variada'
				,CASE FIEBRE  WHEN '1' THEN 'X' END AS 'FIEBRE_SI',CASE FIEBRE WHEN '2' THEN 'X' END AS 'FIEBRE_NO',CASE FIEBRE WHEN '3' THEN 'X' END AS 'FIEBRE_DESCONOCIDO'
				,CASE VOMITO  WHEN '1' THEN 'X' END AS 'VOMITO_SI',CASE VOMITO WHEN '2' THEN 'X' END AS 'VOMITO_NO',CASE VOMITO WHEN '3' THEN 'X' END AS 'VOMITO_DESCONOCIDO'
				,NUMVOMITO
				,convert(varchar(10),FECHAINIDIA,103) as 'FechaInicioDiarrea'
				,NUMDEPOSIC as 'NumeroDeposiciones'
				,convert(varchar(10),FECTERMDIA,103) as 'FechaTerminacionDiarrea'
				,CASE HECES  WHEN '1' THEN 'X' END AS 'HECES_Liquidas',CASE HECES WHEN '2' THEN 'X' END AS 'HECES_SemiLiquidas',CASE HECES WHEN '3' THEN 'X' END AS 'HECES_SANGUIOLENTAS',CASE HECES WHEN '4' THEN 'X' END AS 'HECES_OTRA'
				,CUALHECES
				,CASE ESTADOINGRE  WHEN '1' THEN 'X' END AS 'EstadoIngreso_SinDesHidratacion',CASE ESTADOINGRE WHEN '2' THEN 'X' END AS 'EstadoIngreso_ConDesHidratacion'
				,CASE GRADODESHI  WHEN '1' THEN 'X' END AS 'Grado_Leve',CASE GRADODESHI WHEN '2' THEN 'X' END AS 'Grado_GraveDesconocido',CASE GRADODESHI WHEN '3' THEN 'X' END AS 'Grado_Grave',CASE GRADODESHI WHEN '4' THEN 'X' END AS 'Grado_Desconocido'
				,PESO
				,TALLA
				,CASE ANTIBIOTICOANTES  WHEN '1' THEN 'X' END AS 'ANTIBIOTICO_SI',CASE ANTIBIOTICOANTES WHEN '2' THEN 'X' END AS 'ANTIBIOTICO_NO',CASE ANTIBIOTICOANTES WHEN '3' THEN 'X' END AS 'ANTIBIOTICO_DESCONOCIDO'
				,CUALANTIBIO
				,CASE TIPOHIDRATA  WHEN '1' THEN 'X' END AS 'TIPOHIDRATA_ORAL',CASE TIPOHIDRATA WHEN '2' THEN 'X' END AS 'TIPOHIDRATA_INTRAVENOSA'
				,CASE COMPLICACIONEMB  WHEN '1' THEN 'X' END AS 'ComplicacionHospitalizacion_SI',CASE COMPLICACIONEMB WHEN '2' THEN 'X' END AS 'ComplicacionHospitalizacion_NO',CASE COMPLICACIONEMB WHEN '3' THEN 'X' END AS 'ComplicacionHospitalizacion_DESCONOCIDO'
				,CUALCOMPLICA as 'CUALComplicacionHospitalizacion'
				,CASE ANTIBIOHOSP  WHEN '1' THEN 'X' END AS 'AntibioticoHospitalizacion_SI',CASE ANTIBIOHOSP WHEN '2' THEN 'X' END AS 'AntibioticoHospitalizacion_NO',CASE ANTIBIOHOSP WHEN '3' THEN 'X' END AS 'AntibioticoHospitalizacion_DESCONOCIDO'
				,CUALANTIBIO2 as 'CUALAntibiotico'
				,DIASHOSPI as 'DiasHospitalizacion'
				,HOSPURGENCIA as 'HospitalizaiconUrgencia'
				,HOSPPEDIA as 'HospitalizacionPediatria'
				,HOSPUCI as 'HospitalizacionUCI'
				,convert(varchar(10),FECHAEGRESO,103) as 'FechaEgreso'
				,CASE MOTIVOEGRESO  WHEN '1' THEN 'X' END AS 'MOTIVOEGRESO_mejoria',CASE MOTIVOEGRESO WHEN '2' THEN 'X' END AS 'MOTIVOEGRESO_Salida',CASE MOTIVOEGRESO WHEN '3' THEN 'X' END AS 'MOTIVOEGRESO_Muerte' 
				,CASE SALIDADIARREA  WHEN '1' THEN 'X' END AS 'SALIDADIARREA_SI',CASE SALIDADIARREA WHEN '2' THEN 'X' END AS 'SALIDADIARREA_NO',CASE SALIDADIARREA WHEN '3' THEN 'X' END AS 'SALIDADIARREA_DESCONOCIDO'
				,DIAGNOEGRESO + ' - ' + rtrim(D.NOMDIAGNO)  as 'DiagnosticoEgreso' 
				,convert(varchar(10),FECHARECOL,103) as 'FechaRecoleccion'
				,convert(varchar(10),FECHARECEP,103) as 'FechaRecepcion'
				,convert(varchar(10),FECHARESULT,103) as 'FechaResultado'
				,CASE IDROTAVIRUS WHEN '1' THEN 'X' END AS 'ROTAVIRUS_SI',CASE IDROTAVIRUS  WHEN '0' THEN 'X' END AS 'ROTAVIRUS_NO'
				,SEROTIPOG
				,SEROTIPOP
				,CASE IDBACTERIAS WHEN '1' THEN 'X' END AS 'Bacterias_SI',CASE IDBACTERIAS  WHEN '0' THEN 'X' END AS 'Bacterias_NO'
				,CUALESBACTE as 'CualesBacterias'
				,CASE IDPARASITOS WHEN '1' THEN 'X' END AS 'PARASITOS_SI',CASE IDPARASITOS  WHEN '0' THEN 'X' END AS 'PARASITOS_NO'
				,CUALESPARA as 'CualesParasitos'
				,CASE NINOGUARDE WHEN '1' THEN 'X' END AS 'NINOGUARDE_SI',CASE NINOGUARDE  WHEN '0' THEN 'X' END AS 'NINOGUARDE_NO'
				,CUALGUARDE as 'CualGuarderia'
				,CASE DIARREAFAMI  WHEN '1' THEN 'X' END AS 'Diarreafamiliar_SI',CASE DIARREAFAMI WHEN '2' THEN 'X' END AS 'Diarreafamiliar_NO',CASE DIARREAFAMI WHEN '3' THEN 'X' END AS 'Diarreafamiliar_DESCONOCIDO', 
				VERSION AS 'VERSION', 
				JSON AS 'JSON' 
	
			From  
				HCFICHA605 As F 
				LEFT JOIN INDIAGNOS As D on F.DIAGNOEGRESO = D.CODDIAGNO 
			where 
				IDFICHANOTIFICACION  = @IdFicha	
	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera el detalle completo de la ficha epidemiológica SIVIGILA 605 correspondiente a Enfermedad Diarreica Aguda (EDA) en menores, identificada por un ID de ficha de notificación. Consolida los datos clínicos del episodio diarreico: vacunación contra rotavirus, alimentación, síntomas (fiebre, vómito, diarrea), grado de deshidratación, uso de antibióticos, tipo de hidratación, complicaciones durante la hospitalización, resultados de laboratorio (rotavirus, bacterias, parásitos) y datos de egreso. Enriquece el diagnóstico de egreso con el nombre oficial CIE-10 consultado en el catálogo maestro de diagnósticos (INDIAGNOS), y presenta cada campo categórico como marcas ''X'' listas para imprimir en el formulario oficial de notificación obligatoria del INS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila605';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila605';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera y formatea para presentación los datos de la ficha de notificación Sivigila 605 (vigilancia de enfermedad diarreica aguda por rotavirus) asociada a un identificador de ficha.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila605';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA605 cuyo IDFICHANOTIFICACION coincida con el identificador recibido; de lo contrario el resultado es vacío.; El código de diagnóstico de egreso debe existir en INDIAGNOS para obtener su descripción; si no existe, el nombre del diagnóstico vendrá NULL por el LEFT JOIN.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila605';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las fechas se entregan siempre en formato británico dd/mm/yyyy (style 103) como cadena de 10 caracteres.; Las variables categóricas se traducen a marcas ''X'' por opción excluyente; valores fuera del catálogo quedan como NULL en todas las marcas.; El diagnóstico de egreso se concatena como ''código - nombre'' usando descripción de INDIAGNOS.; La consulta es de solo lectura: no modifica datos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila605';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación Sivigila 605; Vigilancia epidemiológica; Enfermedad diarreica aguda; Rotavirus; Vacunación rotavirus; Lactancia materna; Deshidratación; Hospitalización (Urgencias, Pediatría, UCI); Diagnóstico de egreso; Resultados de laboratorio (rotavirus, bacterias, parásitos, serotipos G y P); Asistencia a guardería; Antecedente de diarrea familiar', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila605';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHA605: Cuando IDFICHANOTIFICACION = parámetro recibido, retorna las columnas de la ficha 605 transformadas (códigos numéricos a marcas ''X'' por opción y fechas a formato dd/mm/yyyy).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila605';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si VACUNAROTAVIRUS = ''1'' / ''2'' / ''3'' → Marca con ''X'' la opción SI / NO / DESCONOCIDO respectivamente; si TIENECARNET = ''1'' / ''0'' → Marca con ''X'' la opción SI / NO; si LECHEMATERNA / FIEBRE / VOMITO / ANTIBIOTICOANTES / COMPLICACIONEMB / ANTIBIOHOSP / SALIDADIARREA / DIARREAFAMI = ''1'' / ''2'' / ''3'' → Marca SI / NO / DESCONOCIDO; si ALIMENACTUAL = ''1'' / ''2'' / ''3'' / ''4'' → Marca alimentación actual: Materna / Artificial / Mixta / Variada; si HECES = ''1'' / ''2'' / ''3'' / ''4'' → Marca tipo de heces: Líquidas / Semilíquidas / Sanguinolentas / Otra; si ESTADOINGRE = ''1'' / ''2'' → Marca estado de ingreso: Sin Deshidratación / Con Deshidratación; si GRADODESHI = ''1'' / ''2'' / ''3'' / ''4'' → Marca grado: Leve / Grave Desconocido / Grave / Desconocido; si TIPOHIDRATA = ''1'' / ''2'' → Marca tipo de hidratación: Oral / Intravenosa; si MOTIVOEGRESO = ''1'' / ''2'' / ''3'' → Marca motivo de egreso: Mejoría / Salida / Muerte; si IDROTAVIRUS / IDBACTERIAS / IDPARASITOS / NINOGUARDE = ''1'' / ''0'' → Marca SI / NO en el resultado correspondiente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila605';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA605; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila605';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila605';
-- GO
