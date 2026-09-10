
-- =============================================  
-- Author:  Juan Patiño  
-- ALTER date: 08-05-2019  
-- Description: SP para traer la información de la Escala Bacteriana de Silness  
-- =============================================  

CREATE PROCEDURE [dbo].[ESE_SP_HC_odontologia_Indice_BacterianaSilness]
AS
     SELECT CASE C.IPTIPODOC
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
                THEN 'Menor Sin Identificación'
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
                THEN 'IPS Privada'
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
            A.FECHAREGISTRO AS 'Fecha Registro', 
            A.RESULTADO AS 'Resultado Escala',
            CASE
                WHEN RESULTADO >= 0
                     AND RESULTADO <= 15
                THEN CONVERT(VARCHAR, RESULTADO) + '-' + 'Higiene oral buena'
                WHEN RESULTADO >= 16
                     AND RESULTADO <= 33
                THEN CONVERT(VARCHAR, RESULTADO) + '-' + 'Higiene oral regular'
                WHEN RESULTADO >= 34
                     AND RESULTADO <= 100
                THEN CONVERT(VARCHAR, RESULTADO) + '-' + 'Higiene oral deficiente'
            END AS 'Interpretacion Escala',
            CASE B.IDMODELOHC
                WHEN 33
                THEN 'Historia clínica de salud oral - Higienista oral '
                WHEN 28
                THEN 'Odontología'
            END AS 'Origen del Registro'
     FROM.HCESCALAS A
         INNER JOIN.HCHISPACA B WITH(NOLOCK) ON A.NUMEFOLIO = B.NUMEFOLIO
                                                AND A.IPCODPACI = B.IPCODPACI
         INNER JOIN.INPACIENT C WITH(NOLOCK) ON A.IPCODPACI = C.IPCODPACI
         INNER JOIN.ADINGRESO D WITH(NOLOCK) ON A.NUMINGRES = D.NUMINGRES
         INNER JOIN Contract.CareGroup E WITH(NOLOCK) ON E.Id = D.GENCAREGROUP
         INNER JOIN.INENTIDAD F WITH(NOLOCK) ON F.CODENTIDA = D.CODENTIDA
         INNER JOIN.INPROFSAL G WITH(NOLOCK) ON G.CODPROSAL = A.CODPROSAL
     WHERE TIPOESCALA = '44'
           AND a.FECHAREGISTRO BETWEEN '01/06/2019' AND '01/07/2019';     
---Fecha Inicial del Folio Formato: (DD-MM-YYYY), Fecha Final del Folio Formato:(DD-MM-YYYY)  
---44 - Escala Bacteriana Silness
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el reporte del Índice Bacteriano de Silness (escala odontológica de placa bacteriana, código 44) aplicado a pacientes en atención de odontología e higiene oral. Consolida información del paciente (nombre completo, documento de identidad, fecha de nacimiento, edad calculada, sexo) tomada del maestro de pacientes, junto con datos del ingreso (grupo de atención, entidad aseguradora y su tipo) y del profesional de salud que registró la escala. Interpreta el resultado numérico de la escala en tres niveles de higiene oral: buena (0-15), regular (16-33) o deficiente (34-100), e indica si el registro proviene de una historia clínica de higienista oral o de odontología. Está orientado a reportería clínica y auditoría odontológica, permitiendo evaluar la condición de higiene bucal de los pacientes atendidos en un período determinado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_HC_odontologia_Indice_BacterianaSilness';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_HC_odontologia_Indice_BacterianaSilness';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta los registros de la Escala Bacteriana de Silness con datos demográficos del paciente, entidad, profesional y la interpretación cualitativa del resultado de higiene oral.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_Indice_BacterianaSilness';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen escalas registradas con TIPOESCALA = ''44'' (Escala Bacteriana Silness); Existe correspondencia entre folio/paciente/ingreso entre HCESCALAS, HCHISPACA, INPACIENT y ADINGRESO; El ingreso tiene grupo de atención (GENCAREGROUP) y entidad (CODENTIDA) válidos; Existe profesional asociado en INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_Indice_BacterianaSilness';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'TIPOESCALA ''44'' identifica la Escala Bacteriana de Silness; El rango de interpretación de higiene oral es: 0-15 buena, 16-33 regular, 34-100 deficiente; Solo se reconocen dos modelos de historia clínica como origen: 33 (Higienista oral) y 28 (Odontología); La edad se calcula en años con factor 365.25 (incluye bisiestos); El rango de fechas filtrado está hardcodeado a junio de 2019', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_Indice_BacterianaSilness';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Escala Bacteriana de Silness; Higiene oral; Historia clínica odontológica; Higienista oral; Tipo de documento de identidad; Tipo de entidad (EPS, ARL, IPS, Medicina Prepagada); Grupo de atención; Profesional de la salud; Folio de historia clínica; Ingreso del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_Indice_BacterianaSilness';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve registros con TIPOESCALA=''44'' cuya FECHAREGISTRO esté entre ''01/06/2019'' y ''01/07/2019''; [RETURN_RESULT] resultset: Calcula edad como DATEDIFF(dd, IPFECNACI, GETDATE())/365.25 truncado a entero', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_Indice_BacterianaSilness';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si RESULTADO entre 0 y 15 → Clasifica como ''Higiene oral buena''; si RESULTADO entre 16 y 33 → Clasifica como ''Higiene oral regular''; si RESULTADO entre 34 y 100 → Clasifica como ''Higiene oral deficiente''; si IDMODELOHC = 33 → Origen = ''Historia clínica de salud oral - Higienista oral'' else Si IDMODELOHC = 28 entonces Origen = ''Odontología''; si IPTIPODOC entre 1 y 12 → Mapea código de tipo documento a su descripción (CC, CE, TI, RC, Pasaporte, etc.); si EntityType entre 1 y 13 o 99 → Mapea a tipo de entidad (EPS Contributivo, Subsidiado, ARL, IPS, Particulares, etc.); si IPSEXOPAC = 1 o 2 → Mapea a ''Masculino'' o ''Femenino''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_Indice_BacterianaSilness';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'HCESCALAS; HCHISPACA; INPACIENT; ADINGRESO; Contract.CareGroup; INENTIDAD; INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_Indice_BacterianaSilness';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_Indice_BacterianaSilness';
-- GO
