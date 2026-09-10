

CREATE VIEW [dbo].[ATENCION_URGENCIAS]
AS
SELECT
   TOP (100) PERCENT C.IPCODPACI AS IDENTIFICACION,
   C.IPNOMCOMP AS NOMBRE_PACIENTE,
   CASE
      WHEN         C.CODTIPPAC LIKE '1'      THEN         'MATERNAS' 
      WHEN         C.CODTIPPAC LIKE '2'       THEN         'MENORES DE 5 AÑOS' 
      WHEN         C.CODTIPPAC LIKE '3'       THEN         'ADULTOS MAYORES' 
      WHEN         C.CODTIPPAC LIKE '4'       THEN         'DISCAPACITADOS' 
      WHEN         C.CODTIPPAC LIKE '5'       THEN         'POBLACIÓN GENERAL' 
      ELSE         'DESCONOCIDO' 
   END
   AS TIPO_PACIENTE, CAST(PA.IPFECNACI AS DATE) AS [FECHA NACIMIENTO], 
   DATEDIFF(YEAR, PA.IPFECNACI, C.IPFECLLEGA) AS EDAD, 
   C.IPFECLLEGA AS FECHA_LLEGADA, 
   C.PRIMERLLA AS LLAMADO_UNO, 
   C.SEGUNDLLA AS LLAMADO_DOS, 
   C.TERCERLLA AS LLAMADO_TRES, 
   C.UFUCODIGO AS UNIDADF, 
   UF.UFUDESCRI AS UNIDADFUNCIONAL, 
   CASE C.CONESTADO  
        WHEN 2 THEN 0
		ELSE DATEDIFF(MINUTE, C.IPFECLLEGA, GETDATE()) END AS EN_ESPERA_DESDE_LLEGADA, 
   T.TRIAFECHA AS FECHA_CLASIFICACION, 
   H1.FECHINIHI AS FECHA_INICIAL_ATENCION, 
   T.NUMINGRES AS NUMERO_INGRESO, 
   DATEDIFF(MINUTE, C.IPFECLLEGA, T.TRIAFECHA) AS DESDE_LLEGADA_HASTA_CLASIFICACION, 
   DATEDIFF(MINUTE, T.TRIAFECHA, ISNULL(H1.FECHINIHI, GETDATE()))  AS DESDE_CLASIFICACION_HASTA_INICIA_ATENCION, 
   DATEDIFF(MINUTE, T.TRIAFECHA, H1.FECINIATE) AS DESDE_CLASIFICACION_HASTA_TERMINAR_CONSULTA, 
   DATEDIFF(MINUTE, C.IPFECLLEGA, H1.FECINIATE) AS DESDE_LLEGADA_HASTA_TERMINAR, 
   DATEDIFF(MINUTE, C.IPFECLLEGA, H1.FECHFINH) AS DESDE_LLEGADA_HASTA_TERMINAR_ATENCION_CONSULTORIO, 
   CASE
      WHEN         C.CONESTADO = 1       THEN         'SIN ATENDER' 
      WHEN         C.CONESTADO = 2       THEN         'AUSENTE EN CLASIFICACION TRIAGE' 
      WHEN         C.CONESTADO = 3       THEN         'CLASIFICADO SIN INGRESO' 
      WHEN         C.CONESTADO = 4       THEN         'CLASIFICADO CON INGRESO' 
      WHEN         C.CONESTADO = 5       THEN         'ATENDIDO' 
      WHEN         C.CONESTADO = 6       THEN         'AUSENTE EN ATENCION INICIAL URGENCIAS' 
      WHEN         C.CONESTADO = 7       THEN         'ANULADO POR ERROR DE PARAMETRIZACION' 
      WHEN         C.CONESTADO = 8       THEN         'NO ATENDIDO POR CLASIFICACION III O IV SIN AUTORIZACION' 
   END
   AS ESTADO_ADMISION, 
   CASE
      WHEN         H1.FECHINIHI IS NULL      THEN         'SIN ATENDER' 
      ELSE         'ATENDIDO' 
   END
   AS ESTADO_CONSULTA, 
   CASE
      WHEN         T .TRIUNIEDA = 1       THEN         'AÑOS' 
      WHEN         T .TRIUNIEDA = 2       THEN         'MESES' 
      WHEN         T .TRIUNIEDA = 3       THEN         'DÍAS' 
   END
   AS UNIDADEDAD, T.TRIAGECLA AS CLASIFICACIÓN_TRIAGE, 
   CASE
      WHEN         T .TRIAGECLA = 1       THEN         'EMERGENCIA' 
      WHEN         T .TRIAGECLA = 2       THEN         'URGENCIA MÉDICA' 
      WHEN         T .TRIAGECLA = 3       THEN         'URGENCIA DIFERIDA' 
      WHEN         T .TRIAGECLA = 4       THEN         'NO URGENTE' 
   END
   AS DESCRIPCIÓN, T.CODPROSAL AS MÉDICO_CODIGO, P.NOMMEDICO AS MÉDICO_NOMBRE, T.TRIACAUIN AS PACIENTE_TIPO_CÓDIGO, 
   CASE
      WHEN         T .TRIACAUIN LIKE '1%'       THEN         'HERIDOS EN COMBATE' 
      WHEN         T .TRIACAUIN LIKE '2%'       THEN         'ENFERMEDAD PROFESIONAL' 
      WHEN         T .TRIACAUIN LIKE '3%'       THEN         'ENFERMEDAD GENERAL ADULTO' 
      WHEN         T .TRIACAUIN LIKE '4%'       THEN         'ENFERMEDAD GENERAL PEDIATRIA' 
      WHEN         T .TRIACAUIN LIKE '5%'       THEN         'ODONTOLOGÍA' 
      WHEN         T .TRIACAUIN LIKE '6%'       THEN         'ACCIDENTE DE TRANSITO' 
      WHEN         T .TRIACAUIN LIKE '7%'       THEN         'CATASTROFE/FISALUD' 
      WHEN         T .TRIACAUIN LIKE '8%'       THEN         'QUEMADOS' 
      WHEN         T .TRIACAUIN LIKE '9%'       THEN         'MATERNIDAD' 
      WHEN         T .TRIACAUIN LIKE '10%'       THEN         'ACCIDENTE LABORAL' 
      ELSE         'DESCONOCIDO' 
   END
   AS CAUSA_INGRESO, RTRIM(B.NOMENTIDA) AS ENTIDAD, --SUBSTRING(CAST(GETDATE() AS varchar), 0, 18) 
   [Common].[GETDATE] () AS HORA_ACTUAL,
   YEAR(C.IPFECLLEGA) AS  AÑO_FECHA_LLEGADA,
   MONTH(C.IPFECLLEGA) AS MES_FECHA_LLEGADA,
   DAY(C.IPFECLLEGA) AS  DIA_FECHA_LLEGADA,
   CASE MONTH(C.IPFECLLEGA)
      WHEN 1 THEN 'ENERO'
      WHEN 2 THEN 'FEBRERO'
      WHEN 3 THEN 'MARZO'
      WHEN 4 THEN 'ABRIL'
      WHEN 5 THEN 'MAYO'
      WHEN 6 THEN 'JUNIO'
      WHEN 7 THEN 'JULIO'
      WHEN 8 THEN 'AGOSTO'
      WHEN 9 THEN 'SEPTIEMBRE'
      WHEN 10 THEN 'OCTUBRE'
      WHEN 11 THEN 'NOVIEMBRE'
      WHEN 12 THEN 'DICIEMBRE'
      ELSE 'DESCONOCIDO'
   END MES_NOMBRE_FECHA_LLEGADA 
FROM
   dbo.ADCONTURG AS C WITH (NOLOCK) 
   LEFT OUTER JOIN
      dbo.ADTRIAGEU AS T WITH (NOLOCK) 
      ON C.IPCODPACI = T.IPCODPACI 
      AND C.CODCONCEC = T.TRIANUMER 
   LEFT OUTER JOIN
      dbo.HCURGING1 AS H1 WITH (NOLOCK) 
      ON T.IPCODPACI = H1.IPCODPACI 
      AND T.NUMINGRES = H1.NUMINGRES 
   LEFT OUTER JOIN
      dbo.INPROFSAL AS P WITH (NOLOCK) 
      ON T.CODPROSAL = P.CODPROSAL 
   INNER JOIN
      dbo.INENTIDAD AS B WITH (NOLOCK) 
      ON C.CODENTIDA = B.CODENTIDA 
   LEFT OUTER JOIN
      dbo.INUNIFUNC AS UF WITH (NOLOCK) 
      ON C.UFUCODIGO = UF.UFUCODIGO 
   LEFT OUTER JOIN
      dbo.INPACIENT AS PA WITH (NOLOCK) 
      ON C.IPCODPACI = PA.IPCODPACI 
WHERE
   (
      CAST(C.IPFECLLEGA AS date) = CAST(GETDATE() AS date)
   )
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting en tiempo real del flujo de atención de urgencias del día en curso. Consolida, para cada paciente presente hoy, sus datos demográficos, tipo de paciente (materna, pediátrico, adulto mayor, etc.), clasificación triage (1-4), causa de ingreso, entidad aseguradora y unidad funcional asignada. Calcula tiempos clave en minutos: llegada→clasificación, clasificación→inicio de atención y llegada→fin de consulta, junto con el estado actual de admisión y consulta.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ATENCION_URGENCIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ATENCION_URGENCIAS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Tablero operativo del día que consolida el flujo de pacientes en urgencias, mostrando tiempos entre llegada, clasificación triage y atención, junto con estados, causa de ingreso, entidad y datos demográficos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ATENCION_URGENCIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas de admisión de urgencias (ADCONTURG), triage (ADTRIAGEU) e historia clínica de urgencias (HCURGING1) deben mantener la trazabilidad por IPCODPACI, CODCONCEC/TRIANUMER y NUMINGRES.; Debe existir la entidad asociada en INENTIDAD (INNER JOIN obligatorio) para que el contacto aparezca en el resultado.; La función [Common].[GETDATE] debe estar disponible.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ATENCION_URGENCIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen registros del día en curso (filtro por fecha de llegada igual a hoy).; La edad se calcula en años como diferencia entre fecha de nacimiento del paciente y fecha de llegada al servicio.; Los tiempos de espera se expresan en minutos vía DATEDIFF(MINUTE,...).; Cuando el paciente está ausente en triage (CONESTADO=2), no se acumula tiempo de espera desde la llegada.; Cada contacto debe tener entidad pagadora asociada (INNER JOIN con INENTIDAD); sin ella no aparece.; Se utilizan lecturas sin bloqueo (WITH NOLOCK) en todas las tablas, permitiendo lecturas sucias.; Los códigos numéricos de tipo de paciente, estado de admisión, clasificación triage, unidad de edad y causa de ingreso se traducen siempre a etiquetas de negocio en español.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ATENCION_URGENCIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Urgencias; Triage; Clasificación triage (Emergencia/Urgencia/No urgente); Estado de admisión; Causa de ingreso; Tiempos de atención (llegada, clasificación, inicio y fin de atención); Tipo de paciente (maternas, menores, adultos mayores, discapacitados); Entidad pagadora; Unidad funcional; Profesional de la salud / médico tratante; Paciente; Historia clínica de urgencias; Ingreso hospitalario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ATENCION_URGENCIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ADCONTURG: Solo se devuelven contactos cuya fecha de llegada (IPFECLLEGA) corresponda al día actual: CAST(C.IPFECLLEGA AS date) = CAST(GETDATE() AS date).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ATENCION_URGENCIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si C.CODTIPPAC LIKE ''1''..''5'' → Clasifica al paciente como MATERNAS, MENORES DE 5 AÑOS, ADULTOS MAYORES, DISCAPACITADOS o POBLACIÓN GENERAL. else Se etiqueta como ''DESCONOCIDO''.; si C.CONESTADO = 2 (ausente en clasificación triage) → El tiempo EN_ESPERA_DESDE_LLEGADA se fuerza a 0, no se calcula contra GETDATE(). else Se calcula DATEDIFF(MINUTE, IPFECLLEGA, GETDATE()).; si C.CONESTADO entre 1 y 8 → Traduce el estado de admisión a etiquetas de negocio: SIN ATENDER, AUSENTE EN CLASIFICACION TRIAGE, CLASIFICADO SIN/CON INGRESO, ATENDIDO, AUSENTE EN ATENCION INICIAL, ANULADO POR ERROR DE PARAMETRIZACION, NO ATENDIDO POR CLASIFICACION III O IV SIN AUTORIZACION.; si H1.FECHINIHI IS NULL → El estado de consulta se reporta como ''SIN ATENDER''. else Se reporta como ''ATENDIDO''.; si T.TRIAGECLA en 1..4 → Describe la clasificación triage como EMERGENCIA, URGENCIA MÉDICA, URGENCIA DIFERIDA o NO URGENTE.; si T.TRIACAUIN LIKE ''1%''..''10%'' → Clasifica la causa de ingreso (HERIDOS EN COMBATE, ENFERMEDAD PROFESIONAL, ENFERMEDAD GENERAL ADULTO/PEDIATRIA, ODONTOLOGÍA, ACCIDENTE DE TRANSITO, CATASTROFE/FISALUD, QUEMADOS, MATERNIDAD, ACCIDENTE LABORAL). else Se reporta como ''DESCONOCIDO''.; si T.TRIUNIEDA en 1..3 → Interpreta la unidad de edad del triage como AÑOS, MESES o DÍAS.; si H1.FECHINIHI IS NULL al calcular DESDE_CLASIFICACION_HASTA_INICIA_ATENCION → Se sustituye por GETDATE() (ISNULL), midiendo el tiempo transcurrido hasta el momento actual. else Se usa la fecha real de inicio de atención.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ATENCION_URGENCIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ATENCION_URGENCIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADCONTURG; dbo.ADTRIAGEU; dbo.HCURGING1; dbo.INPROFSAL; dbo.INENTIDAD; dbo.INUNIFUNC; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ATENCION_URGENCIAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ATENCION_URGENCIAS';
GO
