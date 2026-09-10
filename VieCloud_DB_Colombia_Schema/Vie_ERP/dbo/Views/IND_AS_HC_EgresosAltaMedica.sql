CREATE VIEW [dbo].[IND_AS_HC_EgresosAltaMedica]
AS
     SELECT A.IPCODPACI AS Identificacion, 
            H.NUMINGRES AS Ingreso,
            CASE
                WHEN F.IPTIPODOC = '1'
                THEN 'Cedula de Ciudadania'
                WHEN F.IPTIPODOC = '2'
                THEN 'Cedula de Extranjeria'
                WHEN F.IPTIPODOC = '3'
                THEN 'Tarjeta de Identidad'
                WHEN F.IPTIPODOC = '4'
                THEN 'Registro Civil'
                WHEN F.IPTIPODOC = '5'
                THEN 'Pasaporte'
                WHEN F.IPTIPODOC = '6'
                THEN 'Adulto sin Identificacion'
                WHEN F.IPTIPODOC = '7'
                THEN 'Menor sin Identificacion'
            END AS Tipo_Documento, 
            F.IPEXPEDIC AS Lugar_expedicion, 
            A.NUMINGRES, 
            F.IPNOMCOMP AS Paciente, 
            RTRIM(LTRIM(dbo.Edad(CONVERT(VARCHAR, F.IPFECNACI, 105), CONVERT(VARCHAR, GETDATE(), 105)))) AS Edad,
            CASE F.IPESTADOC
                WHEN '1'
                THEN 'Soltero'
                WHEN '2'
                THEN 'Casado'
                WHEN '3'
                THEN 'Viudo'
                WHEN '4'
                THEN 'Union libre'
                WHEN '5'
                THEN 'Separado_Div'
            END AS EstadoCivil,
            CASE
                WHEN F.IPSEXOPAC = '1'
                THEN 'Masculino'
                WHEN F.IPSEXOPAC = '2'
                THEN 'Femenino'
            END AS Sexo, 
            F.IPDIRECCI AS Direccion, 
            F.IPTELEFON AS Telefono, 
            F.IPTELMOVI AS Movil, 
            E.UFUDESCRI AS [Unidad Funcional], 
            H.IFECHAING AS [Fecha Ingreso], 
            A.FECALTPAC AS [Fecha Alta Medica], 
            MAX(EST.FECFINEST) AS Fecha_Egreso_Cama, 
            DATEDIFF(minute, A.FECALTPAC, MAX(EST.FECFINEST)) AS Diferencia_Minutos, 
            K.NOMENTIDA AS Entidad,
            CASE F.IPTIPOPAC
                WHEN '1'
                THEN 'Contributivo'
                WHEN '2'
                THEN 'Subsidiado'
                WHEN '3'
                THEN 'Vinculado'
                WHEN '4'
                THEN 'Particular'
                WHEN '5'
                THEN 'Desplazado Reg. Contributivo'
                WHEN '6'
                THEN 'Desplazado Reg. Subsidiado'
                WHEN '7'
                THEN 'Desplazado no Asegurado'
            END AS TipoPaciente, 
            J.CODDIAGNO AS CodDiag, 
            J.NOMDIAGNO AS DiagnosticoPpal, 
            H.IAUTORIZA AS [Nro. Autorizacion], 
            H.IOBSERVAC AS Observaciones,
            CASE ESTPACEGR
                WHEN 1
                THEN 'Mejor'
                WHEN 2
                THEN 'Igual o Peor'
                WHEN 3
                THEN 'Fallecido'
                WHEN 4
                THEN 'Remitido'
                WHEN 5
                THEN 'Hospitalizacion en Casa'
            END AS EstadoEgreso,
            CASE HC.INDICAPAC
                WHEN '1'
                THEN 'Trasladar a Urgencias Solo desde consulta externa'
                WHEN '2'
                THEN 'Trasladar a Observacion Urgencias  solo desde Urgencias'
                WHEN '3'
                THEN 'Trasladar a Hospitalizacion Dif Misma Unidad'
                WHEN '4'
                THEN 'Trasladar a  UCI Adulto Dif Misma Unidad'
                WHEN '5'
                THEN 'Trasladar a UCI Pediatrica Dif Misma Unidad'
                WHEN '6'
                THEN 'Trasladar a UCI Neonatal Dif Misma Unidad'
                WHEN '7'
                THEN 'Trasladar a Consulta Externa Dif Misma 
Unidad'
                WHEN '8'
                THEN 'Trasladar a  Cirugia Dif Misma Unidad'
                WHEN '9'
                THEN 'Hospitalizacion en Casa'
                WHEN '10'
                THEN 'Referencia'
                WHEN '11'
                THEN 'Morgue'
                WHEN '12'
                THEN 'Salida'
                WHEN '13'
                THEN 'Continua en la Unidad'
                WHEN '15'
                THEN 'Retiro Voluntario'
                WHEN '16'
                THEN 'Fuga'
            END AS DestinoPaciente, 
            SUBSTRING(F.AUUBICACI, 1, 5) AS CodMunicipio, 
            M.MUNNOMBRE AS Municipio, 
            SUBSTRING(F.AUUBICACI, 1, 2) AS CodDepto, 
            D.nomdepart AS Departamento
     FROM dbo.HCREGEGRE AS A WITH(NOLOCK)
          INNER JOIN dbo.HCHISPACA AS HC WITH(NOLOCK) ON HC.IPCODPACI = A.IPCODPACI
                                                         AND HC.NUMINGRES = A.NUMINGRES
                                                         AND HC.NUMEFOLIO = A.NUMEFOLIO
                                                         AND A.FECALTPAC BETWEEN '01/01/2019 00:00:00' AND '31/12/2019 23:59:59'
          INNER JOIN dbo.INUNIFUNC AS E WITH(NOLOCK) ON E.UFUCODIGO = A.UFUCODIGO
          INNER JOIN dbo.INPACIENT AS F WITH(NOLOCK) ON F.IPCODPACI = A.IPCODPACI
          INNER JOIN dbo.INMUNICIP AS M WITH(NOLOCK) ON M.DEPMUNCOD = SUBSTRING(F.AUUBICACI, 1, 5)
          INNER JOIN dbo.INDEPARTA AS D WITH(NOLOCK) ON D.depcodigo = SUBSTRING(F.AUUBICACI, 1, 2)
          INNER JOIN dbo.ADINGRESO AS H WITH(NOLOCK) ON H.IPCODPACI = F.IPCODPACI
                                                        AND A.NUMINGRES = H.NUMINGRES
          LEFT OUTER JOIN dbo.INENTIDAD AS AE WITH(NOLOCK) ON AE.CODENTIDA = H.CODENTIDA
          INNER JOIN dbo.INDIAGNOS AS J WITH(NOLOCK) ON J.CODDIAGNO = H.CODDIAING
          INNER JOIN dbo.INENTIDAD AS K WITH(NOLOCK) ON K.CODENTIDA = AE.CODENTIDA
          LEFT OUTER JOIN dbo.CHREGESTA AS EST WITH(NOLOCK) ON EST.NUMINGRES = H.NUMINGRES
     WHERE(A.FECALTPAC BETWEEN '01/01/2019 00:00:00' AND '31/12/2019 23:59:59')
     GROUP BY A.IPCODPACI, 
              H.NUMINGRES, 
              F.IPTIPODOC, 
              F.IPEXPEDIC, 
              A.NUMINGRES, 
              F.IPNOMCOMP, 
              F.IPFECNACI, 
              F.IPESTADOC, 
              F.IPSEXOPAC, 
              F.IPDIRECCI, 
              F.IPTELEFON, 
              F.IPTELMOVI, 
              E.UFUDESCRI, 
              H.IFECHAING, 
              HC.FECHISPAC, 
              A.FECALTPAC, 
              K.NOMENTIDA, 
              F.IPTIPOPAC, 
              J.CODDIAGNO, 
              J.NOMDIAGNO, 
              H.IOBSERVAC, 
              A.ESTPACEGR, 
              HC.INDICAPAC, 
              F.AUUBICACI, 
              M.MUNNOMBRE, 
              F.AUUBICACI, 
              D.nomdepart, 
              H.IAUTORIZA;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los egresos hospitalarios con alta médica del año 2019, integrando datos del registro de egreso (HCREGEGRE), la historia clínica (HCHISPACA), la admisión (ADINGRESO) y el maestro de pacientes (INPACIENT). Para cada paciente dado de alta muestra su identificación, tipo de documento, nombre completo, edad, sexo, estado civil, dirección, teléfono, unidad funcional de egreso, fecha de ingreso, fecha de alta médica, fecha real de egreso de cama, diferencia en minutos entre el alta y la salida física, entidad aseguradora o pagadora (EPS/ARS), tipo de régimen (contributivo, subsidiado, particular, etc.), diagnóstico principal CIE-10, estado de salud al egreso (mejorado, fallecido, remitido, etc.), destino del paciente (hospitalización en casa, morgue, referencia, fuga, etc.) y municipio o departamento de residencia. Sirve como fuente principal para indicadores y reportes de gestión de egresos hospitalarios, auditoría de tiempos de alta médica versus salida real de cama, y análisis de la condición de egreso de pacientes hospitalizados durante el período 2019.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'IND_AS_HC_EgresosAltaMedica';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'IND_AS_HC_EgresosAltaMedica';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta los egresos hospitalarios con alta médica del año 2019, consolidando datos demográficos del paciente, ingreso, diagnóstico principal, entidad responsable, destino, estado de egreso y diferencia de tiempo entre alta médica y liberación de cama.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_EgresosAltaMedica';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir en el maestro de pacientes y tener municipio/departamento válido derivado de los primeros 5 y 2 caracteres de su ubicación.; Debe existir un registro de ingreso (ADINGRESO) asociado al paciente y número de ingreso del egreso.; El diagnóstico de ingreso debe estar catalogado en INDIAGNOS.; La entidad del ingreso debe existir en INENTIDAD (se cruza dos veces vía AE→K).; La fecha de alta médica debe estar entre 01/01/2019 00:00:00 y 31/12/2019 23:59:59.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_EgresosAltaMedica';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen egresos cuya fecha de alta médica pertenece al año 2019 (filtro aplicado tanto en JOIN como en WHERE).; La edad se calcula dinámicamente al momento de la consulta usando la función dbo.Edad sobre la fecha de nacimiento y la fecha actual.; El código de municipio corresponde a los primeros 5 caracteres de AUUBICACI y el de departamento a los primeros 2.; La diferencia de minutos se calcula entre la fecha de alta médica y la última fecha fin de estancia (MAX FECFINEST).; La entidad mostrada proviene del segundo cruce con INENTIDAD (K) ligado a través del LEFT JOIN inicial (AE), lo que la condiciona a la existencia de la entidad del ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_EgresosAltaMedica';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Egreso hospitalario; Alta médica; Paciente; Ingreso/Admisión; Historia clínica; Unidad funcional; Diagnóstico principal (CIE); Entidad responsable de pago; Tipo de afiliación (régimen); Estado de egreso; Destino del paciente; Estancia / cama; Autorización; Municipio y departamento de residencia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_EgresosAltaMedica';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] IND_AS_HC_EgresosAltaMedica: Cuando A.FECALTPAC está dentro del año 2019, retorna una fila por egreso con el máximo FECFINEST de estancias asociadas al ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_EgresosAltaMedica';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPTIPODOC ∈ {1..7} → Traduce el código a etiqueta de tipo de documento (Cédula, Tarjeta de Identidad, Registro Civil, Pasaporte, etc.); si IPESTADOC ∈ {1..5} → Traduce a estado civil (Soltero, Casado, Viudo, Unión libre, Separado/Div); si IPSEXOPAC ∈ {1,2} → Traduce a Masculino/Femenino; si IPTIPOPAC ∈ {1..7} → Traduce a régimen de afiliación (Contributivo, Subsidiado, Vinculado, Particular, Desplazado en sus variantes); si ESTPACEGR ∈ {1..5} → Traduce a estado de egreso (Mejor, Igual o Peor, Fallecido, Remitido, Hospitalización en Casa); si HC.INDICAPAC ∈ {1..16} → Traduce al destino del paciente (traslados a urgencias/observación/UCI/cirugía/consulta, hospitalización en casa, referencia, morgue, salida, continúa, retiro voluntario, fuga)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_EgresosAltaMedica';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Edad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_EgresosAltaMedica';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCREGEGRE; dbo.HCHISPACA; dbo.INUNIFUNC; dbo.INPACIENT; dbo.INMUNICIP; dbo.INDEPARTA; dbo.ADINGRESO; dbo.INENTIDAD; dbo.INDIAGNOS; dbo.CHREGESTA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_EgresosAltaMedica';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_EgresosAltaMedica';
GO
