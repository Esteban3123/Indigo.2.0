CREATE PROCEDURE [dbo].[ESE_SP_HC_odontologia_Indice_CEO_COP]
AS
     SELECT D.NUMINGRES,
            CASE C.IPTIPODOC
                WHEN 1
                THEN 'Cédula de Ciudadanía'
                WHEN 2
                THEN 'Cédula de Extranjería'
                WHEN 3
                THEN 'Tarjeta de Identidad'
                WHEN 4
                THEN 'Registro Civil'
                WHEN 5
                THEN 'Pasaporte'
                WHEN 6
                THEN 'Adulto Sin Identificación'
                WHEN 7
                THEN 'Menor 
Sin Identificación'
                WHEN 8
                THEN 'Número único de identificación personal'
                WHEN 9
                THEN 'Certificado Nacido Vivo'
                WHEN 10
                THEN 'Carnet Diplomático'
                WHEN 11
                THEN 'Salvoconducto'
                WHEN 12
                THEN 'Permiso especial de Permanencia'
            END AS 'Tipo de ID del paciente', 
            C.IPCODPACI AS 'Numero de id del paciente', 
            C.IPPRIAPEL AS 'Apellido 1', 
            C.IPSEGAPEL AS 'Apellido 2', 
            C.IPPRINOMB AS 'Nombre 1', 
            C.IPSEGNOMB AS 'Nombre 2', 
            C.IPFECNACI AS 'Fecha de nacieminto', 
            (CAST(DATEDIFF(dd, IPFECNACI, [Common].[GETDATE]()) / 365.25 AS INT)) AS 'Edad en años',
            CASE IPSEXOPAC
                WHEN 1
                THEN 'Masculino'
                WHEN 2
                THEN 'Femenino'
            END AS 'Sexo', 
            E.Code AS 'Código Grupo de atención', 
            E.Name AS 'Nombre Grupo de atención', 
            F.CODENTIDA AS 'Código Entidad', 
            F.NOMENTIDA AS 'Nombre entidad',
            CASE EntityType
                WHEN 1
                THEN 'EPS Contributivo'
                WHEN 2
                THEN 'EPS Subsidiado'
                WHEN 3
                THEN 'ET Vinculados Municipios'
                WHEN 4
                THEN 'ET Vinculados Departamentos'
                WHEN 5
                THEN 'ARL Riesgos Laborales'
                WHEN 6
                THEN 'MP Medicina Prepagada'
                WHEN 7
                THEN 'IPS Priva
da'
                WHEN 8
                THEN 'IPS Publica'
                WHEN 9
                THEN 'Regimen Especial'
                WHEN 10
                THEN 'Accidentes de transito'
                WHEN 11
                THEN 'Fosyga'
                WHEN 12
                THEN 'Otros'
                WHEN 13
                THEN 'Aseguradoras'
                WHEN 99
                THEN 'Particulares'
            END AS 'Tipo de entidad', 
            G.CODPROSAL AS 'Código del profesional', 
            G.NOMMEDICO AS 'Nombre del profesional', 
            D.NUMINGRES AS 'ingreso', 
            (H.CEO_C + H.CPO_C) AS 'Total dientes cariados', 
            (H.CEO_E + H.CPO_P) AS 'Total dientes perdidos-Extraidos', 
            (H.CEO_O + H.CPO_O) AS 'Total dientes Obturados', 
            (CEO_C + CEO_E + CEO_O) AS 'Suma de CEO', 
            (CPO_C + CPO_P + CPO_O) AS 'Suma de CPO', 
            H.CEO_C, 
            H.CEO_E, 
            H.CEO_O, 
            H.CPO_C, 
            H.CPO_O, 
            H.CPO_P, 
            B.IDMODELOHC, 
            B.FECHISPAC AS 'Fecha del folio', 
            odo.[Tipo de Atencion]
     FROM.ODONTOCONTROL A
         INNER JOIN.HCHISPACA B WITH(NOLOCK) ON A.NUMEFOLIO = B.NUMEFOLIO
                                                AND A.IPCODPACI = B.IPCODPACI
                                                AND B.IDMODELOHC = 28
         INNER JOIN.INPACIENT C WITH(NOLOCK) ON A.IPCODPACI = C.IPCODPACI
         INNER JOIN.ADINGRESO D WITH(NOLOCK) ON A.NUMINGRES = D.NUMINGRES
         INNER JOIN Contract.CareGroup E WITH(NOLOCK) ON E.Id = D.GENCAREGROUP
         INNER JOIN.INENTIDAD F WITH(NOLOCK) ON F.CODENTIDA = D.CODENTIDA
         INNER JOIN.INPROFSAL G WITH(NOLOCK) ON G.CODPROSAL = A.CODPROSAL
         INNER JOIN.ODONTOCONTROLVALO H WITH(NOLOCK) ON H.IDODONTOCONTROL = A.ID
         INNER JOIN.ESE_HC_HISTORICO_ODONTOLOGIA ODO ON A.NUMINGRES = ODO.Ingreso
     WHERE B.FECHISPAC BETWEEN '01/08/2019' AND '01/09/2019';   ---Fecha Inicial del Folio Formato: (DD-MM-YYYY), Fecha Final del Folio Formato:(DD-MM-YYYY)
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de índices odontológicos CEO (dientes de leche: Cariados, con Extracción indicada, Obturados) y CPO (dientes permanentes: Cariados, Perdidos, Obturados) por paciente y por ingreso, cruzando los controles odontológicos con la historia clínica, los datos del paciente, el ingreso, la entidad aseguradora, el grupo de atención y el profesional tratante. Consolida los totales de dientes cariados, perdidos y obturados tanto en dentición temporal como permanente, junto con la suma total de cada índice (CEO y CPO), permitiendo medir la salud bucal de la población atendida. Es utilizado para reportería y análisis epidemiológico odontológico, filtrando por un rango de fechas de folio de historia clínica (modelo HC 28) y el tipo de atención registrado en el histórico odontológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_HC_odontologia_Indice_CEO_COP';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_HC_odontologia_Indice_CEO_COP';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta el índice odontológico CEO/COP (cariados, extraídos/perdidos, obturados) por paciente y folio, con datos demográficos, entidad y profesional, en un rango de fechas de folio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_Indice_CEO_COP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen folios de historia clínica con IDMODELOHC = 28 (modelo odontológico).; Existen registros de control odontológico con valoración CEO/COP asociada.; Cada ingreso debe tener grupo de atención, entidad y profesional válidos para los JOIN INNER.; Debe existir registro histórico odontológico asociado al ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_Indice_CEO_COP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera folios del modelo odontológico (IDMODELOHC = 28).; La edad se calcula como DATEDIFF(días, fecha_nac, hoy)/365.25 truncado a entero.; Total cariados = CEO_C + CPO_C; perdidos/extraídos = CEO_E + CPO_P; obturados = CEO_O + CPO_O.; Suma CEO = CEO_C+CEO_E+CEO_O; Suma CPO = CPO_C+CPO_P+CPO_O.; El rango de fechas del folio está hardcodeado (01/08/2019 a 01/09/2019), no parametrizado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_Indice_CEO_COP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Tipo de documento de identidad; Sexo; Edad; Ingreso asistencial; Folio de historia clínica; Modelo de historia clínica odontológica; Grupo de atención; Entidad responsable de pago; Tipo de entidad (EPS, ARL, IPS, Fosyga, etc.); Profesional de la salud; Índice CEO (dentición temporal); Índice COP/CPO (dentición permanente); Dientes cariados, extraídos/perdidos y obturados; Tipo de atención odontológica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_Indice_CEO_COP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve filas con índice CEO/COP por folio cuando B.FECHISPAC está entre 01/08/2019 y 01/09/2019 y B.IDMODELOHC = 28.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_Indice_CEO_COP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si C.IPTIPODOC entre 1..12 → Traduce el código a la descripción del tipo de documento (CC, CE, TI, RC, Pasaporte, etc.).; si IPSEXOPAC = 1 / 2 → Etiqueta el sexo como ''Masculino'' o ''Femenino''.; si EntityType entre 1..13 o 99 → Traduce el tipo de entidad (EPS Contributivo, Subsidiado, ARL, IPS, Fosyga, Particulares, etc.).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_Indice_CEO_COP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_Indice_CEO_COP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ODONTOCONTROL; dbo.HCHISPACA; dbo.INPACIENT; dbo.ADINGRESO; Contract.CareGroup; dbo.INENTIDAD; dbo.INPROFSAL; dbo.ODONTOCONTROLVALO; dbo.ESE_HC_HISTORICO_ODONTOLOGIA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_Indice_CEO_COP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_Indice_CEO_COP';
-- GO
