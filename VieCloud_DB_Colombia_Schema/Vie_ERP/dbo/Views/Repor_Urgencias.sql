CREATE VIEW [dbo].[Repor_Urgencias]
AS
SELECT        TOP (100) 
                         PERCENT CASE WHEN U.IPTIPODOC = '1' THEN 'CC' WHEN U.IPTIPODOC = '2' THEN 'CE' WHEN U.IPTIPODOC = '3' THEN 'TI' WHEN U.IPTIPODOC = '4' THEN 'RC' WHEN U.IPTIPODOC = '5' THEN 'PA' WHEN U.IPTIPODOC = '6'
                          THEN ' AS' WHEN U.IPTIPODOC = '7' THEN 'MS' END AS [Tipo Identificación], C.IPCODPACI AS Identificación, C.IPNOMCOMP AS Paciente, 
                         CASE WHEN U.IPSEXOPAC = '1' THEN 'Masculino' WHEN U.IPSEXOPAC = '2' THEN 'Femenino' END AS Sexo, YEAR(GETDATE()) - YEAR(U.IPFECNACI) AS Edad, U.IPTELEFON AS Telefono, U.IPTELMOVI AS Movil, 
                         CASE C.CODTIPPAC WHEN 1 THEN 'Maternas' WHEN 2 THEN 'Menores de 5 Años' WHEN 3 THEN 'Adultos Mayores' WHEN 4 THEN 'Prepagadas o Particulares' WHEN 5 THEN 'Poblacion General' END AS TipoPaciente, 
                         C.IPFECLLEGA AS [Hora de ingreso a Admisiones], 
                         CASE WHEN C.CONESTADO = '1' THEN 'Sin Atender' WHEN C.CONESTADO = '2' THEN 'Ausente en Clasificacion TRIAGE' WHEN C.CONESTADO = '3' THEN 'Clasificado sin Ingreso' WHEN C.CONESTADO = '4' THEN 'Clasificado con Ingreso'
                          WHEN C.CONESTADO = '5' THEN 'Atendido' WHEN C.CONESTADO = '6' THEN 'Ausente en Atencion Inicial Urgencias' WHEN C.CONESTADO = '7' THEN 'Anulado por error de Parametrizacion' WHEN C.CONESTADO = '8' THEN 'No atendido por Clasificacion sin Autorizacion'
                          END AS Estado, ds.TRIANOMCA AS Causa_de_ingreso, C.OBVAUSENT AS Observacion_Ausencia_Triage, DATEDIFF(minute, C.IPFECLLEGA, A.TRIAFECHA) AS Minutos_Espera_Admisiones_a_Triage, 
                         A.TRIAFECHA AS FechaHoraTriage, A.TRIAGECLA AS ClasificacionTriage, P.NOMMEDICO AS Profesional_Realiza_Triage, ES.DESESPECI AS Especialidad, F.NOMENTIDA AS Entidad_del_Paciente, 
                         CASE e.EntityType WHEN '1' THEN 'EPS Contributivo' WHEN '2' THEN 'EPS Subsidiado' WHEN '3' THEN 'ET Vinculados Municipios' WHEN '4' THEN 'ET Vinculados Departamentos' WHEN '5' THEN 'ARL' WHEN '6' THEN 'Prepagada'
                          WHEN '7' THEN 'IPS' WHEN '8' THEN 'IPS' WHEN '9' THEN 'Regimen Especial' WHEN '10' THEN 'Accidentes Transito' WHEN '11' THEN 'Fosyga' WHEN '12' THEN 'Otros' WHEN '99' THEN 'Particulares' END AS Regimen, 
                         DATEDIFF(minute, A.TRIAFECHA, k.FECHINIHI) AS Minutos_Espera_Triage_A_Consulta, k.FECHINIHI AS [Fecha Hora Inicial consulta], PAIU.NOMMEDICO AS Profesional_que_atiende_consulta, A.NUMINGRES AS Ingreso, 
                         CASE I.IESTADOIN WHEN ' ' THEN 'Abierto' WHEN 'F' THEN 'Facturado' WHEN 'C' THEN 'Cerrado' WHEN 'A' THEN 'Anulado' END AS Estado_Ingreso, DX.NOMDIAGNO AS Diagnostico_Ingreso, 
                         DXA.NOMDIAGNO AS Diagnostico_Egreso, k.FECHFINH AS [Fecha Hora Final consulta], DATEDIFF(minute, k.FECHINIHI, k.FECHFINH) AS Minutos_de_la_Consulta
FROM            dbo.ADCONTURG AS C WITH (nolock) LEFT OUTER JOIN
                         dbo.INPACIENT AS U WITH (nolock) ON U.IPCODPACI = C.IPCODPACI LEFT OUTER JOIN
                         dbo.INENTIDAD AS F WITH (nolock) ON F.CODENTIDA = U.CODENTIDA LEFT OUTER JOIN
                         Contract.CareGroup AS E WITH (nolock) ON E.Id = U.GENCAREGROUP LEFT OUTER JOIN
                         dbo.ADTRIAGEU AS A WITH (nolock) ON A.TRIANUMER = C.CODCONCEC LEFT OUTER JOIN
                         dbo.INPROFSAL AS P WITH (nolock) ON P.CODPROSAL = A.CODPROSAL LEFT OUTER JOIN
                         dbo.INPROFSAL AS Pa WITH (nolock) ON Pa.CODPROSAL = C.PROAUSENT LEFT OUTER JOIN
                         dbo.INESPECIA AS ES WITH (nolock) ON ES.CODESPECI = P.CODESPEC1 LEFT OUTER JOIN
                         dbo.ADCATTRIU AS ds WITH (nolock) ON ds.TRIACATEG = A.TRIACATEG LEFT OUTER JOIN
                         dbo.HCURGING1 AS k WITH (nolock) ON k.IPCODPACI = A.IPCODPACI AND k.NUMINGRES = A.NUMINGRES AND k.UFUCODIGO = C.UFUCODIGO LEFT OUTER JOIN
                         dbo.INPROFSAL AS PAIU WITH (NOLOCK) ON PAIU.CODPROSAL = k.CODPROSAL LEFT OUTER JOIN
                         dbo.ADINGRESO AS I WITH (NOLOCK) ON I.NUMINGRES = A.NUMINGRES LEFT OUTER JOIN
                         dbo.INDIAGNOS AS DX WITH (NOLOCK) ON DX.CODDIAGNO = I.CODDIAING LEFT OUTER JOIN
                         dbo.INDIAGNOS AS DXA WITH (NOLOCK) ON DXA.CODDIAGNO = I.CODDIAEGR
WHERE        (C.IPFECLLEGA >= '01/09/2020 00:00:00')
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Panel de urgencias que consolida el recorrido completo de un paciente desde su llegada al servicio de urgencias hasta el cierre de la consulta. Integra datos de identificación y demografía del paciente (cédula, tipo de documento, sexo, edad, teléfono, tipo de paciente), la entidad aseguradora y régimen (EPS contributivo, subsidiado, ARL, prepagada, particular, etc.), el registro de admisión en urgencias con hora de llegada y estado del proceso (sin atender, ausente en triage, clasificado, atendido, anulado), la valoración de triage con clasificación de prioridad, causa de ingreso, profesional que realizó el triage y su especialidad, los tiempos de espera en minutos entre admisiones y triage y entre triage e inicio de consulta, la historia clínica de urgencias con fecha y hora de inicio y fin de la consulta y el profesional que atendió, y los diagnósticos de ingreso y egreso en codificación CIE-10. Existe para reportería operativa y gerencial del servicio de urgencias, permitiendo analizar flujos de atención, tiempos de espera, productividad de profesionales, distribución por entidad y régimen, y seguimiento de la calidad asistencial desde el 1 de septiembre de 2020 en adelante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Repor_Urgencias';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Repor_Urgencias';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporte consolidado de atenciones en urgencias desde 01/09/2020, integrando datos demográficos del paciente, triage, profesional, entidad/régimen, ingreso y tiempos entre etapas (admisión-triage-consulta).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Repor_Urgencias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de registros en ADCONTURG con IPFECLLEGA >= ''01/09/2020 00:00:00''; Las relaciones con paciente, entidad, triage, profesional, ingreso y diagnósticos pueden ser opcionales (LEFT JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Repor_Urgencias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Edad calculada como diferencia de años entre fecha actual y año de nacimiento (no exacta por mes/día); Tiempo de espera Admisiones→Triage = DATEDIFF en minutos entre IPFECLLEGA y TRIAFECHA; Tiempo de espera Triage→Consulta = DATEDIFF en minutos entre TRIAFECHA y FECHINIHI; Duración de consulta = DATEDIFF en minutos entre FECHINIHI y FECHFINH; El régimen se obtiene del CareGroup asociado al paciente, no del contacto de urgencias; La especialidad reportada corresponde al profesional que realiza el triage, no al que atiende la consulta; El cruce con historia clínica de urgencias requiere coincidencia simultánea de paciente, ingreso y unidad funcional (UFUCODIGO)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Repor_Urgencias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Urgencias; Triage; Clasificación TRIAGE; Ingreso/Admisión; Diagnóstico de ingreso; Diagnóstico de egreso; Entidad responsable de pago; Régimen de afiliación; Especialidad médica; Profesional de la salud; Tipo de paciente (maternas, menores, adultos mayores); Tiempos de espera asistenciales; Estado del contacto en urgencias; Ausentismo en triage/atención', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Repor_Urgencias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ADCONTURG: Devuelve filas únicamente cuando C.IPFECLLEGA >= ''01/09/2020 00:00:00''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Repor_Urgencias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si U.IPTIPODOC ∈ {1..7} → Mapea código a etiqueta de tipo de identificación: 1=CC, 2=CE, 3=TI, 4=RC, 5=PA, 6=AS, 7=MS; si U.IPSEXOPAC = 1 / 2 → Traduce a ''Masculino'' / ''Femenino''; si C.CODTIPPAC ∈ {1..5} → Clasifica paciente: 1=Maternas, 2=Menores de 5 Años, 3=Adultos Mayores, 4=Prepagadas o Particulares, 5=Población General; si C.CONESTADO ∈ {1..8} → Determina estado del contacto en urgencias: 1=Sin Atender, 2=Ausente en Triage, 3=Clasificado sin Ingreso, 4=Clasificado con Ingreso, 5=Atendido, 6=Ausente en Atención Inicial, 7=Anulado por error de Parametrización, 8=No atendido por Clasificación sin Autorización; si E.EntityType ∈ {1..12,99} → Asigna régimen: 1=EPS Contributivo, 2=EPS Subsidiado, 3/4=ET Vinculados, 5=ARL, 6=Prepagada, 7/8=IPS, 9=Régimen Especial, 10=Accidentes Tránsito, 11=Fosyga, 12=Otros, 99=Particulares; si I.IESTADOIN ∈ {'' '',''F'',''C'',''A''} → Estado del ingreso: '' ''=Abierto, F=Facturado, C=Cerrado, A=Anulado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Repor_Urgencias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADCONTURG; dbo.INPACIENT; dbo.INENTIDAD; Contract.CareGroup; dbo.ADTRIAGEU; dbo.INPROFSAL; dbo.INESPECIA; dbo.ADCATTRIU; dbo.HCURGING1; dbo.ADINGRESO; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Repor_Urgencias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Repor_Urgencias';
GO
