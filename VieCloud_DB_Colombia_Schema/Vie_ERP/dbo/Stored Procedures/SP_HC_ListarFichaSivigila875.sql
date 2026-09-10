-- Stored Procedure
-- =============================================
-- Autor:		Juan David Patiño Cabrera
-- Fecha Creacion: 08-05-2018
-- Descripcion:	Sp que me lista la Información de las Fichas del Sivigila, Ficha 875
-- Modifico:    Yezid Garcia Medina
-- Fecha Modificación : 29-03-2022
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila875]
(
  @IdFicha as Int
)

AS
BEGIN
  SET NOCOUNT ON;

			Select
				 CASE CUALVIONOSEXUAL WHEN '1' THEN 'X' END AS 'ViolenciaNoSexual_Fisica',CASE CUALVIONOSEXUAL WHEN '2' THEN 'X' END AS 'ViolenciaNoSexual_Psicologica',CASE CUALVIONOSEXUAL WHEN '3' THEN 'X' END AS 'ViolenciaNoSexual_Negligencia', 
				 CASE CUALVIOSEXUAL WHEN 1 THEN 'X' END AS 'ViolenciaSexual_Abuso', 
				 CASE CUALVIOSEXUAL WHEN 2 THEN 'X' END AS 'ViolenciaSexual_Acoso', 
				 CASE CUALVIOSEXUAL WHEN 3 THEN 'X' END AS 'ViolenciaSexual_Violacion', 
				 CASE CUALVIOSEXUAL WHEN 4 THEN 'X' END AS 'ViolenciaSexual_ExplotacionNinos', 
				 CASE CUALVIOSEXUAL WHEN 5 THEN 'X' END AS 'ViolenciaSexual_TrataPersonas', 
				 CASE CUALVIOSEXUAL WHEN 6 THEN 'X' END AS 'ViolenciaSexual_ActosSexuales', 
				 CASE CUALVIOSEXUAL WHEN 7 THEN 'X' END AS 'ViolenciaSexual_Otros', 
				 CASE CUALVIOSEXUAL WHEN 8 THEN 'X' END AS 'ViolenciaSexual_Mutilacion', 
				 CASE ACTIVIDAD      WHEN '1' THEN 'X' END AS 'ACTIVIDAD_Liderez',CASE ACTIVIDAD WHEN '2' THEN 'X' END AS 'ACTIVIDAD_Estudiante',CASE ACTIVIDAD WHEN '3' THEN 'X' END AS 'ACTIVIDAD_Otro',CASE ACTIVIDAD    WHEN '4' THEN 'X' END AS 'ACTIVIDAD_TrabajadraDomes',CASE ACTIVIDAD WHEN '5' THEN 'X' END AS 'ACTIVIDAD_TrabajoSexual',CASE ACTIVIDAD WHEN '6' THEN 'X' END AS 'ACTIVIDAD_Campesino',CASE ACTIVIDAD WHEN '7' THEN 'X' END AS 'ACTIVIDAD_AmaCasa',CASE ACTIVIDAD WHEN '8' THEN 'X' END AS 'ACTIVIDAD_PersonaCuidaOtros',CASE ACTIVIDAD WHEN '9' THEN 'X' END AS 'ACTIVIDAD_Ninguna'
				,CASE ORIENTASEXUAL       WHEN '1' THEN 'X' END AS 'ORI_Homosexual',CASE ORIENTASEXUAL WHEN '2' THEN 'X' END AS 'ORI_Bisexual',CASE ORIENTASEXUAL WHEN '3' THEN 'X' END AS 'ORI_Heterosexual',CASE ORIENTASEXUAL    WHEN '4' THEN 'X' END AS 'ORI_Asexual'
				,CASE IDENTGENERO        WHEN '1' THEN 'X' END AS 'IDE_Masculino',CASE IDENTGENERO WHEN '2' THEN 'X' END AS 'IDE_Femenino',CASE IDENTGENERO WHEN '3' THEN 'X' END AS 'IDE_Transgenero',CASE IDENTGENERO    WHEN '4' THEN 'X' END AS 'IDE_Intergenero',CASE IDENTGENERO    WHEN '5' THEN 'X' END AS 'IDE_Otro'
				,CASE CONSUMOSPA   WHEN '1' THEN 'X' END AS 'CONSUMOSPA_SI',CASE CONSUMOSPA WHEN '0' THEN 'X' END AS 'CONSUMOSPA_NO'
				,CASE MUJERCABFAMI    WHEN '1' THEN 'X' END AS 'MUJERCABFAMI_SI',CASE MUJERCABFAMI WHEN '0' THEN 'X' END AS 'MUJERCABFAMI_NO'
				,CASE ANTEVIOLENCIA    WHEN '1' THEN 'X' END AS 'ANTEVIOLENCIA_SI',CASE ANTEVIOLENCIA WHEN '0' THEN 'X' END AS 'ANTEVIOLENCIA_NO'
				,CASE ALCOHOLVIC    WHEN '1' THEN 'X' END AS 'ALCOHOLVIC_SI',CASE ALCOHOLVIC WHEN '0' THEN 'X' END AS 'ALCOHOLVIC_NO'
				,EDAD
				,CASE SEXO WHEN 1 THEN 'X' END AS 'SEXO_Masculino',CASE SEXO WHEN 2 THEN 'X' END AS 'SEXO_Femenino',CASE WHEN (SEXO IS NULL) OR (SEXO IS NOT NULL AND SEXO = 3) THEN 'X' END AS 'SEXO_SinInformacion',CASE SEXO WHEN 4 THEN 'X' END AS 'SEXO_Intersexual'
				,CASE PARENTVICTIMA  WHEN '1' THEN 'X' END AS 'ParentesctoVict_Padre',CASE PARENTVICTIMA WHEN '2' THEN 'X' END AS 'ParentesctoVict_Madre',CASE PARENTVICTIMA WHEN '3' THEN 'X' END AS 'ParentesctoVict_Pareja',CASE PARENTVICTIMA    WHEN '4' THEN 'X' END AS 'ParentesctoVict_Expareja',CASE PARENTVICTIMA    WHEN '5' THEN 'X' END AS 'ParentesctoVict_Familiar',CASE PARENTVICTIMA    WHEN '6' THEN 'X' END AS 'ParentesctoVict_Ninguno'
				,CASE CONVIVEAGRE     WHEN '1' THEN 'X' END AS 'CONVIVEAGRE_SI',CASE CONVIVEAGRE  WHEN '0' THEN 'X' END AS 'CONVIVEAGRE_NO'
				,CASE AGRESORNOFA   WHEN '1' THEN 'X' END AS 'AgreNoFamiliar_Profesor',CASE AGRESORNOFA WHEN '2' THEN 'X' END AS 'AgreNoFamiliar_Amigo',CASE AGRESORNOFA WHEN '3' THEN 'X' END AS 'AgreNoFamiliar_CompaneroTrabajo',CASE AGRESORNOFA    WHEN '4' THEN 'X' END AS 'AgreNoFamiliar_CompaneroEstudio',CASE AGRESORNOFA    WHEN '5' THEN 'X' END AS 'AgreNoFamiliar_Desconocido',CASE AGRESORNOFA    WHEN '6' THEN 'X' END AS 'AgreNoFamiliar_Vecino',CASE AGRESORNOFA  WHEN '7' THEN 'X' END AS 'AgreNoFamiliar_Conocido',CASE AGRESORNOFA WHEN '8' THEN 'X' END AS 'AgreNoFamiliar_SinInformacion',CASE AGRESORNOFA WHEN '9' THEN 'X' END AS 'AgreNoFamiliar_Otro',CASE AGRESORNOFA    WHEN '10' THEN 'X' END AS 'AgreNoFamiliar_Jefe',CASE AGRESORNOFA    WHEN '11' THEN 'X' END AS 'AgreNoFamiliar_Sacerdote',CASE AGRESORNOFA    WHEN '12' THEN 'X' END AS 'AgreNoFamiliar_ServidorPublico'
				,CASE CONFICTOARM      WHEN '1' THEN 'X' END AS 'CONFICTOARM_SI',CASE CONFICTOARM   WHEN '0' THEN 'X' END AS 'CONFICTOARM_NO'
				,CASE MECANISMOAGRE    WHEN '1' THEN 'X' END AS 'MecanismoAgre_Ahorcamiento',CASE MECANISMOAGRE WHEN '2' THEN 'X' END AS 'MecanismoAgre_Caidas',CASE MECANISMOAGRE WHEN '3' THEN 'X' END AS 'MecanismoAgre_Contundente',CASE MECANISMOAGRE    WHEN '4' THEN 'X' END AS 'MecanismoAgre_Cortante',CASE MECANISMOAGRE    WHEN '5' THEN 'X' END AS 'MecanismoAgre_Proyectil',CASE MECANISMOAGRE    WHEN '6' THEN 'X' END AS 'MecanismoAgre_QuemaduraFuego',CASE MECANISMOAGRE  WHEN '7' THEN 'X' END AS 'MecanismoAgre_QuemaduraAcido',CASE MECANISMOAGRE WHEN '8' THEN 'X' END AS 'MecanismoAgre_QuemaduraLiquido',CASE MECANISMOAGRE WHEN '9' THEN 'X' END AS 'MecanismoAgre_Otrosmecanismos',CASE MECANISMOAGRE    WHEN '10' THEN 'X' END AS 'MecanismoAgre_sustancias'
				,CASE SITIOANATOMICO     WHEN '1' THEN 'X' END AS 'Sitio_Cara',CASE SITIOANATOMICO WHEN '2' THEN 'X' END AS 'Sitio_Cuello',CASE SITIOANATOMICO WHEN '3' THEN 'X' END AS 'Sitio_Mano',CASE SITIOANATOMICO    WHEN '4' THEN 'X' END AS 'Sitio_Pies',CASE SITIOANATOMICO    WHEN '5' THEN 'X' END AS 'Sitio_Pliegues',CASE SITIOANATOMICO    WHEN '6' THEN 'X' END AS 'Sitio_Genitales',CASE SITIOANATOMICO  WHEN '7' THEN 'X' END AS 'Sitio_Tronco',CASE SITIOANATOMICO WHEN '8' THEN 'X' END AS 'Sitio_MiembroSupe',CASE SITIOANATOMICO WHEN '9' THEN 'X' END AS 'Sitio_MiembroInfe'
				,CASE GRADO      WHEN '1' THEN 'X' END AS 'GRADO_Primero',CASE GRADO  WHEN '2' THEN 'X' END AS 'GRADO_Segundo',CASE GRADO  WHEN '3' THEN 'X' END AS 'GRADO_Tercero'
				,CASE EXTENSION       WHEN '1' THEN 'X' END AS 'EXTENSION_Menor5',CASE EXTENSION  WHEN '2' THEN 'X' END AS 'EXTENSION_6_14',CASE EXTENSION  WHEN '3' THEN 'X' END AS 'EXTENSION_Mayor15'
				,convert(varchar(10),FECHAHECHO ,103) as 'FechaHecho'
				,datepart(HOUR,FECHAHECHO) as 'FechaHora',
				 CASE ESCENARIO WHEN 1 THEN 'X' END AS 'Escenario_ViaPublica', 
				 CASE ESCENARIO WHEN 2 THEN 'X' END AS 'Escenario_Vivienda', 
				 CASE ESCENARIO WHEN 3 THEN 'X' END AS 'Escenario_CentroEducativos', 
				 CASE ESCENARIO WHEN 4 THEN 'X' END AS 'Escenario_Oficinas', 
				 CASE ESCENARIO WHEN 5 THEN 'X' END AS 'Escenario_Otro', 
				 CASE ESCENARIO WHEN 6 THEN 'X' END AS 'Escenario_Comercial',
				 CASE ESCENARIO WHEN 7 THEN 'X' END AS 'Escenario_EspaciosTerrestre', 
				 CASE ESCENARIO WHEN 8 THEN 'X' END AS 'Escenario_LugaresEsparcimiento',
				 CASE ESCENARIO WHEN 9 THEN 'X' END AS 'Escenario_Salud',
				 CASE ESCENARIO WHEN 10 THEN 'X' END AS 'Escenario_Deporte', 
				 CASE VIOLENLUGAR    WHEN '1' THEN 'X' END AS 'Ambito_Escolar',CASE VIOLENLUGAR WHEN '2' THEN 'X' END AS 'Ambito_Laboral',CASE VIOLENLUGAR WHEN '3' THEN 'X' END AS 'Ambito_Institucional',CASE VIOLENLUGAR    WHEN '4' THEN 'X' END AS 'Ambito_Virtual', CASE VIOLENLUGAR    WHEN '6' THEN 'X' END AS 'Ambito_Comunitario',CASE VIOLENLUGAR  WHEN '7' THEN 'X' END AS 'Ambito_Hogar',CASE VIOLENLUGAR WHEN '8' THEN 'X' END AS 'Ambito_OtrosAmbito'
				,CASE PROFIVIH     WHEN '1' THEN 'X' END AS 'PROFIVIH_SI',CASE PROFIVIH WHEN '0' THEN 'X' END AS 'PROFIVIH_NO'
				,CASE PROFIHEPB     WHEN '1' THEN 'X' END AS 'PROFIHEPB_SI',CASE PROFIHEPB WHEN '0' THEN 'X' END AS 'PROFIHEPB_NO'
				,CASE OTRASPROFI     WHEN '1' THEN 'X' END AS 'OTRASPROFI_SI',CASE OTRASPROFI WHEN '0' THEN 'X' END AS 'OTRASPROFI_NO'
				,CASE ANTICONEMER     WHEN '1' THEN 'X' END AS 'Anticoncepcion_SI',CASE ANTICONEMER WHEN '0' THEN 'X' END AS 'Anticoncepcion_NO'
				,CASE ORIENTAIVE     WHEN '1' THEN 'X' END AS 'ORIENTAIVE_SI',CASE ORIENTAIVE WHEN '0' THEN 'X' END AS 'ORIENTAIVE_NO'
				,CASE SALUDMENTAL     WHEN '1' THEN 'X' END AS 'SALUDMENTAL_SI',CASE SALUDMENTAL WHEN '0' THEN 'X' END AS 'SALUDMENTAL_NO'
				,CASE REMIPROTEC     WHEN '1' THEN 'X' END AS 'remisionProteccion_SI',CASE REMIPROTEC  WHEN '0' THEN 'X' END AS 'remisionProteccion_NO'
				,CASE INFORMEAUTO      WHEN '1' THEN 'X' END AS 'InformeAutoridades_SI',CASE INFORMEAUTO   WHEN '0' THEN 'X' END AS 'InformeAutoridades_NO'
				,CASE EVIDENCIAMED      WHEN '1' THEN 'X' END AS 'RecoleccionEveidencia_SI',CASE EVIDENCIAMED  WHEN '0' THEN 'X' END AS 'RecoleccionEveidencia_NO', 
				VERSION AS 'VERSION', 
				JSON AS 'JSON' 
			From 
				HCFICHA875 
			Where 
				IDFICHANOTIFICACION  = @IdFicha			
		
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera los datos detallados de una ficha 875 del SIVIGILA para su impresión o visualización en la historia clínica. Dado un identificador de ficha, consulta la tabla HCFICHA875 y transforma todos los campos codificados (tipo de violencia sexual y no sexual, actividad de la víctima, orientación sexual, identidad de género, datos del agresor, mecanismo de agresión, sitio anatómico de las lesiones, escenario del hecho y medidas de atención tomadas como profilaxis VIH, hepatitis B, anticoncepción de emergencia, salud mental y remisión a protección) en columnas tipo ''X'' listas para marcar casillas en el formulario oficial de notificación obligatoria de violencia sexual y de género. Existe para generar el documento de reporte SIVIGILA Ficha 875 directamente desde la historia clínica del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila875';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila875';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera y traduce a banderas tipo casilla (''X'') la información de una Ficha Sivigila 875 (violencia de género e intrafamiliar) para visualización/impresión del formato.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila875';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un identificador de ficha de notificación válido para retornar datos de la ficha 875', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila875';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La consulta retorna a lo más una fila correspondiente a la ficha de notificación solicitada, identificada por IDFICHANOTIFICACION; Los valores codificados se traducen a banderas ''X'' por categoría (formato tipo casillas de verificación de la ficha epidemiológica); Si el sexo no está informado o es código 3, siempre se marca como ''Sin información''; La fecha del hecho se presenta en formato dd/mm/yyyy (estilo 103) y se expone separadamente la hora del hecho', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila875';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha Sivigila 875; Violencia sexual; Violencia no sexual (física, psicológica, negligencia); Orientación sexual; Identidad de género; Consumo de SPA; Mujer cabeza de familia; Antecedentes de violencia; Conflicto armado; Mecanismo de agresión; Sitio anatómico de lesión; Escenario del hecho; Ámbito de la violencia; Profilaxis VIH/Hepatitis B; Anticoncepción de emergencia; Orientación IVE; Salud mental; Remisión a protección; Informe a autoridades; Recolección de evidencia médico-legal; Parentesco con víctima; Agresor no familiar', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila875';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA875: Cuando IDFICHANOTIFICACION coincide con el parámetro recibido, devuelve la ficha decodificada en banderas ''X'' por categoría (violencia, actividad, orientación, identidad, escenario, ámbito, mecanismo, sitio anatómico, profilaxis, etc.) más EDAD, FECHAHECHO, hora, VERSION y JSON', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila875';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si SEXO IS NULL o SEXO = 3 → Marca ''X'' en SEXO_SinInformacion', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila875';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA875', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila875';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila875';
-- GO
