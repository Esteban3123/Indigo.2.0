-- =============================================  
-- Author:  <Author,,yeny nuñez>  
-- ALTER date: <ALTER Date, 29/07/2019,>  
-- =============================================  
CREATE PROCEDURE [dbo].[ESE_SP_fichaepidemiologicas]
AS
    BEGIN
        SELECT F.CODEVENTO AS 'codigo evento', 
               F.NOMBEVENTO AS 'Nombre Evento', 
               F.FECHANOTIFI AS 'fecha notificacion', 
               F.RAZONSOCIAL AS 'centro salud', 
               P.IPPRIAPEL 'Apellido1', 
               P.IPSEGAPEL 'Apellido2', 
               P.IPPRINOMB 'Nombre1', 
               P.IPSEGNOMB 'Nombre2',
               CASE P.IPTIPODOC
                   WHEN '1'
                   THEN 'CC'
                   WHEN '2'
                   THEN 'CE'
                   WHEN '3'
                   THEN 'TI'
                   WHEN '4'
                   THEN 'RC'
                   WHEN '5'
                   THEN 'PS'
                   WHEN '6'
                   THEN 'AS'
                   WHEN '7'
                   THEN 'MS'
                   WHEN '8'
                   THEN 'NU'
               END 'Tipo Documento', 
               P.IPCODPACI, 
               F.EDAD AS 'Edad', 
               F.UNIDADMED AS 'Unidad Medida', 
               p.IDPAIS AS 'pais', 
               Z.Nationality AS 'Nacionalidad', 
               F.SEXO AS 'sexo', 
               F.CODPAIS AS 'codigo pais', 
               F.CODDEPARTAMENTO AS 'codigo dto', 
               F.AREA AS 'area', 
               F.LOCALIDAD AS 'Localidad', 
               F.BARRIO AS 'barrio', 
               f.VEREDA AS 'vereda', 
               f.DIRRESIDENCIA AS 'direccion', 
               F.OCUPACION, 
               F.TELNOTIFICA AS 'telefono',
               CASE P.IPTIPOPAC
                   WHEN '1'
                   THEN 'contributivo'
                   WHEN '2'
                   THEN 'Subsidiado'
                   WHEN '3'
                   THEN 'Vinculado'
                   WHEN '4'
                   THEN 'Particular'
                   WHEN '5'
                   THEN 'Otro'
                   WHEN '6'
                   THEN 'Despladado contributivo'
                   WHEN '7'
                   THEN 'Desplazado subisidiado'
                   WHEN '8'
                   THEN 'Desplazado no asegurado'
               END 'Regimen', 
               P.CODENTIDA AS 'Codigo Entidad', 
               en.Name AS 'Nombre Entidad', 
               F.ETNIA, 
               GR.DESGRUPET, 
               F.FECHACONSULTA AS 'fechaconsulta', 
               F.FECHAINICOSINTO AS 'fechainiciosintomas',
               CASE CLASIFICACIONCASO
                   WHEN '1'
                   THEN 'sospechoso'
                   WHEN '2'
                   THEN 'Probable'
                   WHEN '3'
                   THEN 'Conf.por laboratorio'
                   WHEN '4'
                   THEN 'Conf. clinica'
                   WHEN '5'
                   THEN 'Conf. nexo epedimiologico'
               END 'CLASIFICACIONCASO', 
               HOSPITALIZADO, 
               FECHAHOSP,
               CASE CONDICIONFINAL
                   WHEN '1'
                   THEN 'Vivo'
                   WHEN '2'
                   THEN 'Muerto'
                   WHEN '3'
                   THEN 'No sabe, no responde'
               END 'CONDICIONFINAL', 
               FECHADIFUNCION, 
               NUMCERTIFICADO, 
               CAUSAMUERTE, 
               TELNOTIFICA, 
               NOMPROFESIONAL, 
               MOTIVODESCAR, 
               OBSERVADESCARTE, 
               USUDESCARTA, 
               FECHADESCARTE, 
               USUREPORTASIVIGILA, 
               FECHAREPORSIVIGILA, 
               USUARIOVALIDO, 
               FECHAVALIDO, 
               ESTRATO, 
               FUENTE, 
               NACIONALIDAD
        FROM HCFICHANOTIFICACION F
             INNER JOIN INPACIENT P ON F.IPCODPACI = P.IPCODPACI
             LEFT JOIN Common.Country Z ON P.IDPAIS = Z.Id
             LEFT JOIN Contract.HealthAdministrator EN ON P.CODENTIDA = EN.Code
             LEFT JOIN ADGRUETNI GR ON F.ETNIA = GR.CODGRUPOE
        ORDER BY F.FECHACREACION;
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el listado completo de fichas de notificación epidemiológica obligatoria (SIVIGILA) registradas en la institución. Consolida, por cada evento notificable, los datos del evento (código, nombre, fecha de notificación, clasificación del caso, fecha de inicio de síntomas, hospitalización y condición final del paciente), los datos demográficos del paciente (nombre completo, tipo y número de documento/cédula, edad, sexo, nacionalidad, dirección, estrato, ocupación y régimen de salud) y la información de la entidad aseguradora (EPS/ARS). Cruza la ficha de notificación (HCFICHANOTIFICACION) con el maestro de pacientes (INPACIENT), el catálogo de países (Country), las administradoras de salud (HealthAdministrator) y el catálogo de grupos étnicos (ADGRUETNI) para enriquecer cada registro. Se usa para reportería epidemiológica, vigilancia en salud pública y generación de informes de eventos de notificación obligatoria ante entes de control.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_fichaepidemiologicas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_fichaepidemiologicas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las fichas de notificación epidemiológica con datos del paciente, evento, ubicación, entidad aseguradora y clasificación clínica, ordenadas por fecha de creación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_fichaepidemiologicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir relación entre HCFICHANOTIFICACION e INPACIENT por IPCODPACI; Los catálogos Common.Country, Contract.HealthAdministrator y ADGRUETNI deben existir para enriquecer datos (aunque LEFT JOIN tolera ausencias)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_fichaepidemiologicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El paciente (INPACIENT) es obligatorio para la ficha (INNER JOIN por IPCODPACI); País, entidad administradora y grupo étnico son opcionales (LEFT JOIN); El resultado siempre se ordena cronológicamente por FECHACREACION', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_fichaepidemiologicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación epidemiológica; Evento de salud pública; Paciente; Tipo de documento; Régimen de afiliación (contributivo/subsidiado/vinculado/desplazado); Clasificación de caso (sospechoso/probable/confirmado); Condición final (vivo/muerto); Hospitalización; Certificado de defunción; Causa de muerte; Grupo étnico; Nacionalidad; Entidad administradora de salud (EPS); Reporte SIVIGILA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_fichaepidemiologicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHANOTIFICACION: Devuelve todas las fichas epidemiológicas con datos decodificados (tipo documento, régimen, clasificación de caso, condición final) ordenadas por FECHACREACION ascendente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_fichaepidemiologicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si P.IPTIPODOC en {1..8} → Traduce código numérico a sigla de tipo de documento (CC, CE, TI, RC, PS, AS, MS, NU); si P.IPTIPOPAC en {1..8} → Traduce a régimen de afiliación: contributivo, Subsidiado, Vinculado, Particular, Otro, Desplazado contributivo, Desplazado subsidiado, Desplazado no asegurado; si CLASIFICACIONCASO en {1..5} → Traduce a: sospechoso, Probable, Conf. por laboratorio, Conf. clínica, Conf. nexo epidemiológico; si CONDICIONFINAL en {1,2,3} → Traduce a Vivo, Muerto o No sabe/no responde', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_fichaepidemiologicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHANOTIFICACION; dbo.INPACIENT; Common.Country; Contract.HealthAdministrator; dbo.ADGRUETNI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_fichaepidemiologicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_fichaepidemiologicas';
-- GO
