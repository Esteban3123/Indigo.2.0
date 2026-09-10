
CREATE PROCEDURE [dbo].[SPLIS_ListarLaboratoriosPacientesPendientes]
(
@Estado varchar(10),
@CentroAtencion varchar(100),
@ExamenEnSitio Bit
)
WITH RECOMPILE 
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @FechaActual datetime = common.GETDATE();
    DECLARE @FechaInicial datetime = DATEADD(MONTH, -4, @FechaActual);

    CREATE TABLE #Centros
    (
        CODCENATE varchar(20) NOT NULL PRIMARY KEY
    );

    INSERT INTO #Centros (CODCENATE)
    SELECT DISTINCT LTRIM(RTRIM(Value))
    FROM dbo.splitstring(@CentroAtencion)
    WHERE LTRIM(RTRIM(Value)) <> '';

    ;WITH Tmp_ConsultasOncologicas AS
    (
        SELECT
            A.AUTO,
            A.IDETIPHIS,
            A.FECORDMED,
            A.IPCODPACI,
            B.IPNOMCOMP,
            E.UFUACTPAC,
            C.UFUDESCRI AS UFUDESCRIC1,
            C2.UFUDESCRI AS UFUDESCRIC2,
            A.CODCENATE,
            A.ESTSERIPS,
            A.ESTALELAB,
            A.NUMINGRES,
            A.NUMEFOLIO,
            A.CODSERIPS,
            D.DESSERIPS,
            A.OBSSERIPS,
            B.IPRHSANGR,
            B.IPGRUPSAN,
            B.IPDIRECCI,
            B.IPTELEFON,
            B.IPSEXOPAC,
            B.CORELEPAC,
            B.IPTELMOVI,
            A.UFUCODIGO,
            A.CODDIAGNO,
            A.CODPROSAL,
            B.IPTIPODOC,
            B.IPPRINOMB,
            B.IPSEGNOMB,
            B.IPPRIAPEL,
            B.IPSEGAPEL,
            B.IPFECNACI,
            B.IPTIPOPAC,
            B.IPTIPOAFI,
            A.CANSERIPS,
            D.CODGOCUPS,
            A.CONCURRE,
            A.SERREAINT,
            ENT.CODENTIDA,
            ENT.NOMENTIDA,
            E.CODTIPPAC,
            A.PRISERIPS,
            A.FECHASUGE,
            E.CODCAMACT,
            B.AUUBICACI,
            A.IDDESCRIPCIONRELACIONADA,
            A.FECRECMUE,
            A.CODMOTIVOMUESTRANOCONFORME,
            MUN.DEPMUNCOD,
            MUN.MUNNOMBRE,
            CA.NOMCENATE,
            H.NOMMEDICO,
            D.SERIPSPOS,
            E.IESTADOIN,
            IDM.IDMuestra
        FROM dbo.HCORDLABO AS A WITH (NOLOCK)
        INNER JOIN #Centros CE
            ON CE.CODCENATE = A.CODCENATE
        INNER JOIN dbo.INPACIENT AS B WITH (NOLOCK)
            ON A.IPCODPACI = B.IPCODPACI
        INNER JOIN dbo.INCUPSIPS AS D WITH (NOLOCK)
            ON A.CODSERIPS = D.CODSERIPS
        INNER JOIN dbo.ADINGRESO AS E WITH (NOLOCK)
            ON A.IPCODPACI = E.IPCODPACI
           AND A.NUMINGRES = E.NUMINGRES
           AND E.IESTADOIN IN ('','P','C','B')
        INNER JOIN dbo.INENTIDAD AS ENT WITH (NOLOCK)
            ON ENT.CODENTIDA = E.CODENTIDA
        INNER JOIN dbo.INUNIFUNC AS C WITH (NOLOCK)
            ON E.UFUACTPAC = C.UFUCODIGO
        INNER JOIN dbo.INUNIFUNC AS C2 WITH (NOLOCK)
            ON A.UFUCODIGO = C2.UFUCODIGO
        INNER JOIN dbo.INUBICACI AS UBI WITH (NOLOCK)
            ON B.AUUBICACI = UBI.AUUBICACI
        INNER JOIN dbo.INMUNICIP AS MUN WITH (NOLOCK)
            ON UBI.DEPMUNCOD = MUN.DEPMUNCOD
        INNER JOIN dbo.ADCENATEN AS CA WITH (NOLOCK)
            ON CA.CODCENATE = A.CODCENATE
        INNER JOIN dbo.INPROFSAL AS H WITH (NOLOCK)
            ON A.CODPROSAL = H.CODPROSAL
        OUTER APPLY
        (
            SELECT TOP (1)
                CABE.AUTO AS IDMuestra
            FROM dbo.INTERCABE AS CABE WITH (NOLOCK)
            INNER JOIN dbo.INTERDETA AS DETA WITH (NOLOCK)
                ON CABE.AUTO = DETA.CODCONCEC
            WHERE CABE.IPCODPACI = A.IPCODPACI
              AND CABE.NUMINGRES = A.NUMINGRES
              AND DETA.CODSERIPS = A.CODSERIPS
              AND DETA.ORDTIP = 'INT'
              AND DETA.AUTOLABOR = A.AUTO
        ) IDM
        WHERE A.ESTSERIPS = '2'
          AND A.EXMREASIT = @ExamenEnSitio
          AND A.FECORDMED >= @FechaInicial
          AND A.FECORDMED <= @FechaActual
    )
    SELECT DISTINCT
        tmp.AUTO,
        tmp.IDETIPHIS,
        tmp.FECORDMED AS FechaSolicitud,
        RTRIM(tmp.IPCODPACI) AS CodigoPaciente,
        RTRIM(tmp.IPNOMCOMP) AS NombrePaciente,
        tmp.IPFECNACI AS FechaNacimiento,
        tmp.UFUACTPAC AS CodigoUnidad,
        RTRIM(tmp.UFUDESCRIC1) AS DescripcionUnidad,
        tmp.UFUDESCRIC2 AS UnidadSolicitante,
        tmp.CODCENATE AS CodigoCentro,
        CASE tmp.ESTSERIPS
            WHEN 1 THEN 'Solicitado'
            WHEN 2 THEN 'Muestra recolectada'
            WHEN 3 THEN 'PENDiente interpretación'
            WHEN 4 THEN 'Examen interpretado'
            WHEN 5 THEN 'Estudio remitido'
            WHEN 6 THEN 'Anulado'
            WHEN 7 THEN 'Extramural'
            WHEN 8 THEN 'Muestra recolectada parcialmente'
            WHEN 9 THEN 'Muestra no conforme'
        END AS Estado,
        tmp.ESTALELAB AS EstadoAlerta,
        tmp.NUMINGRES AS Ingreso,
        tmp.NUMEFOLIO AS Folio,
        RTRIM(tmp.CODSERIPS) + '-' + RTRIM(tmp.DESSERIPS) + '. ' + ISNULL(CD.name, '') AS DescripcionServicio,
        RTRIM(CD.name) AS DescripcionRelacionada,
        tmp.OBSSERIPS AS ObservacionServicio,
        RTRIM(tmp.CODSERIPS) AS CodigoServicio,
        RTRIM(I.NOMDIAGNO) AS NombreDiagnostico,
        RTRIM(G.DESCCAMAS) AS Cama,
        G.CODAISLAM AS TipoAislamiento,
        tmp.IPDIRECCI AS Direccion,
        tmp.IPTELEFON AS Telefono,
        tmp.IPSEXOPAC AS Sexo,
        tmp.IPRHSANGR AS Rh,
        tmp.IPGRUPSAN AS GrupoSanguineo,
        tmp.CORELEPAC,
        tmp.IPTELMOVI AS Movil,
        tmp.CORELEPAC AS Correo,
        tmp.NOMMEDICO AS Medico,
        0 AS Minutos,
        CAST('' AS CHAR) AS Barra,
        CAST('' AS bit) AS Marcar,
        tmp.CONCURRE AS Concurrencia,
        tmp.SERREAINT AS RealizaInterfaz,
        tmp.UFUCODIGO,
        tmp.CODDIAGNO,
        tmp.CODPROSAL,
        tmp.IPTIPODOC,
        tmp.IPPRINOMB,
        tmp.IPSEGNOMB,
        tmp.IPPRIAPEL,
        tmp.IPSEGAPEL,
        tmp.IPSEXOPAC,
        tmp.IPFECNACI,
        tmp.CODENTIDA,
        tmp.IPTIPOPAC,
        tmp.IPTIPOAFI,
        tmp.IPTELEFON,
        tmp.IPTELMOVI,
        tmp.IPDIRECCI,
        tmp.CANSERIPS,
        tmp.CODGOCUPS,
        CASE tmp.IDETIPHIS WHEN 'CODIGOAZU' THEN 'Emergencia' ELSE 'Historia' END AS TipoSolicitud,
        RTRIM(tmp.DEPMUNCOD) AS MunicipioCodigo,
        RTRIM(tmp.MUNNOMBRE) AS MunicipioNombre,
        RTRIM(tmp.CODENTIDA) AS entidadpagadoraCodigo,
        RTRIM(tmp.NOMENTIDA) AS entidadpagadoraNombre,
        tmp.CODTIPPAC,
        CASE WHEN tmp.PRISERIPS = '1' THEN 'Urgente' ELSE 'Rutinario' END AS Prioridad,
        RP.GESTACION,
        CASE WHEN EG.FECALTPAC IS NULL THEN 'Pacientes en la unidad' ELSE 'Pacientes cON salida' END AS PacienteSalida,
        'Autorización' AS Autorizacion,
        tmp.AUTO AS EntityId,
        tmp.FECHASUGE,
        RTRIM(tmp.NOMCENATE) AS CentroAtencion,
        Z.Color,
        dbo.Edad(tmp.IPFECNACI, @FechaActual) AS Edad,
        tmp.FECRECMUE AS FechaRecoleccion,
        CASE tmp.SERIPSPOS WHEN 0 THEN 'No' WHEN 1 THEN 'Si' END AS FinanciadoUPC,
        tmp.IDMuestra,
        dbo.RiskFactorAlert(tmp.IPCODPACI, '', 1) AS IconoRiesgos,
        dbo.RiskFactorAlert(tmp.IPCODPACI, tmp.NUMINGRES, 2) AS IconoEscalas,
        RTRIM(tmp.IESTADOIN) AS EstadoIngreso
    FROM Tmp_ConsultasOncologicas tmp
    LEFT JOIN dbo.CHCAMASHO AS G WITH (NOLOCK)
        ON tmp.CODCAMACT = G.CODICAMAS
    LEFT JOIN #Centros CE2
        ON CE2.CODCENATE = G.CODCENATE
    LEFT JOIN dbo.CHTIPOSAISLAMIENTOS AS Z WITH (NOLOCK)
        ON Z.Id = G.CODAISLAM
    LEFT JOIN dbo.INDIAGNOS AS I WITH (NOLOCK)
        ON tmp.CODDIAGNO = I.CODDIAGNO
    LEFT JOIN dbo.HCRIESGOSP AS RP WITH (NOLOCK)
        ON RP.NUMINGRCES = tmp.NUMINGRES
    LEFT JOIN dbo.HCREGEGRE AS EG WITH (NOLOCK)
        ON EG.NUMINGRES = tmp.NUMINGRES
    LEFT JOIN contract.CUPSEntityContractDescriptions AS CDD WITH (NOLOCK)
        ON CDD.Id = tmp.IDDESCRIPCIONRELACIONADA
    LEFT JOIN contract.ContractDescriptions AS CD WITH (NOLOCK)
        ON CD.Id = CDD.ContractDescriptionId
    WHERE G.CODICAMAS IS NULL OR CE2.CODCENATE IS NOT NULL
    ORDER BY tmp.FECORDMED DESC;
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los exámenes de laboratorio clínico pendientes (con muestra recolectada, estado ESTSERIPS=2) para uno o varios centros de atención, filtrando por estado de la orden, sede y si el examen se realiza en sitio. Cruza las órdenes de laboratorio (HCORDLABO) con los datos del paciente (INPACIENT), el ingreso activo (ADINGRESO, solo ingresos en curso: urgencias, hospitalización, consulta), la entidad pagadora (INENTIDAD), las unidades funcionales de ubicación actual y solicitante (INUNIFUNC), la nomenclatura del servicio CUPS (INCUPSIPS), el diagnóstico CIE-10 (INDIAGNOS), la cama asignada (CHCAMASHO) y el municipio de residencia del paciente (INMUNICIP, INUBICACI). Devuelve un listado de trabajo para el servicio de laboratorio, mostrando por cada orden pendiente: datos del paciente (cédula, nombre, edad, sexo, grupo sanguíneo), unidad donde está hospitalizado, médico solicitante, descripción del examen, prioridad (urgente o rutinario), estado de la muestra, folio, entidad pagadora, indicador de riesgo clínico e identificador de muestra asociado en el sistema de interconsultas; se usa principalmente en el módulo de recepción y toma de muestras de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPLIS_ListarLaboratoriosPacientesPendientes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPLIS_ListarLaboratoriosPacientesPendientes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes de laboratorio con muestra recolectada (pendientes de procesar) de los últimos 4 meses para los centros de atención indicados, enriqueciendo con datos del paciente, ingreso, ubicación, diagnóstico, cama y alertas de riesgo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesPendientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de centros de atención debe venir como cadena delimitada parseable por dbo.splitstring; Deben existir órdenes en HCORDLABO con ESTSERIPS=''2'' (muestra recolectada) en la ventana de los últimos 4 meses; El ingreso asociado en ADINGRESO debe estar en estado '''', ''P'', ''C'' o ''B''; El paciente debe tener ubicación válida vinculada a un municipio existente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesPendientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan órdenes de laboratorio en estado ''Muestra recolectada'' (ESTSERIPS=''2''); Ventana temporal fija de 4 meses hacia atrás desde la fecha actual del sistema (common.GETDATE); Se restringe a los centros de atención provistos, tanto a nivel de orden como de cama del paciente; Solo se incluyen pacientes con ingreso activo o en estados permitidos ('''',''P'',''C'',''B''); La muestra (IDMuestra) se obtiene como el primer registro de INTERCABE/INTERDETA con ORDTIP=''INT'' que coincida con paciente, ingreso, servicio y orden; La edad se calcula con dbo.Edad y los iconos de riesgo/escalas con dbo.RiskFactorAlert', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesPendientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de laboratorio; Muestra recolectada; Paciente; Ingreso/Admisión; Centro de atención; Unidad funcional; Cama hospitalaria; Tipo de aislamiento; Diagnóstico; Entidad pagadora; Prioridad de examen (urgente/rutinario); Gestación; Egreso del paciente; Financiación UPC; Factores de riesgo y escalas; Examen en sitio; Emergencia (código azul); Autorización', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesPendientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve únicamente órdenes con ESTSERIPS=''2'' (muestra recolectada), EXMREASIT igual al flag de examen en sitio recibido y FECORDMED dentro de los últimos 4 meses respecto a common.GETDATE(); [RETURN_RESULT] RESULTSET: Filtra al cruce con #Centros (lista de centros recibida) en HCORDLABO; además, si la cama existe, su centro también debe pertenecer a la lista (G.CODICAMAS IS NULL OR CE2.CODCENATE IS NOT NULL); [RETURN_RESULT] RESULTSET: Excluye ingresos cuyo IESTADOIN no esté en ('''',''P'',''C'',''B'')', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesPendientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTSERIPS de la orden → Mapea código numérico a etiqueta legible: 1 Solicitado, 2 Muestra recolectada, 3 Pendiente interpretación, 4 Examen interpretado, 5 Estudio remitido, 6 Anulado, 7 Extramural, 8 Muestra recolectada parcialmente, 9 Muestra no conforme; si IDETIPHIS = ''CODIGOAZU'' → TipoSolicitud = ''Emergencia'' else TipoSolicitud = ''Historia''; si PRISERIPS = ''1'' → Prioridad = ''Urgente'' else Prioridad = ''Rutinario''; si FECALTPAC del egreso es NULL → PacienteSalida = ''Pacientes en la unidad'' else PacienteSalida = ''Pacientes con salida''; si SERIPSPOS = 0/1 → FinanciadoUPC = ''No'' / ''Si''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesPendientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring; common.GETDATE; dbo.Edad; dbo.RiskFactorAlert', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesPendientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.INPACIENT; dbo.INCUPSIPS; dbo.ADINGRESO; dbo.INENTIDAD; dbo.INUNIFUNC; dbo.INUBICACI; dbo.INMUNICIP; dbo.ADCENATEN; dbo.INPROFSAL; dbo.INTERCABE; dbo.INTERDETA; dbo.CHCAMASHO; dbo.CHTIPOSAISLAMIENTOS; dbo.INDIAGNOS; dbo.HCRIESGOSP; dbo.HCREGEGRE; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesPendientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesPendientes';
-- GO
