

-- =============================================
-- Author:		<Yezid Garcia Medina, Desarrollador Junior>
-- Create date: <17 Septiembre de 2019>
-- Description:	<Listar Reporte Consulta Citas>
-- =============================================
CREATE PROCEDURE [dbo].[SP_AGE_ListarReporteConsultaCitas]
(
    @Centro      varchar(50),
    @FechaInicio datetime,
    @FechaFin    datetime,
    @TipoFecha   varchar(5),
    @PageIndex   int = 0,    -- Página base 0 (retrocompatible: default 0)
    @PageSize    int = 15000,  -- Registros por página (retrocompatible: default 20000)
    @CitasXml    xml = NULL,  -- Filas de citas obtenidas por el servicio desde Scheduling (reemplaza lectura de AGASICITA)
    @CirugiasXml xml = NULL   -- Filas de cirugías obtenidas por el servicio desde Scheduling (reemplaza lectura de AGEPROGQX)
)
WITH RECOMPILE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @campo VARCHAR(10)
    DECLARE @sql   NVARCHAR(MAX)

    IF (@TipoFecha = 'False')
        SET @campo = 'FECREGSIS';
    ELSE
        SET @campo = 'FECHORAIN';

    SET @sql = N'
        WITH CTE_AGASICITA AS (
            SELECT * FROM (
                SELECT
                    T.c.value(''(CODAUTONU)[1]'', ''int'')                  AS CODAUTONU,
                    T.c.value(''(CODCENATE)[1]'', ''varchar(50)'')          AS CODCENATE,
                    T.c.value(''(FECREGSIS)[1]'', ''datetime'')             AS FECREGSIS,
                    T.c.value(''(FECHORAIN)[1]'', ''datetime'')             AS FECHORAIN,
                    T.c.value(''(FECHORAFI)[1]'', ''datetime'')             AS FECHORAFI,
                    T.c.value(''(CODESPECI)[1]'', ''varchar(50)'')          AS CODESPECI,
                    T.c.value(''(IPCODPACI)[1]'', ''varchar(50)'')          AS IPCODPACI,
                    T.c.value(''(GENCONENTITY)[1]'', ''int'')               AS GENCONENTITY,
                    T.c.value(''(CODPROSAL)[1]'', ''varchar(50)'')          AS CODPROSAL,
                    T.c.value(''(CODACTMED)[1]'', ''varchar(50)'')          AS CODACTMED,
                    T.c.value(''(CODUSUASI)[1]'', ''varchar(50)'')          AS CODUSUASI,
                    T.c.value(''(CANCELUSU)[1]'', ''varchar(50)'')          AS CANCELUSU,
                    T.c.value(''(CODUSUINA)[1]'', ''varchar(50)'')          AS CODUSUINA,
                    T.c.value(''(CODIGOCON)[1]'', ''varchar(50)'')          AS CODIGOCON,
                    T.c.value(''(IDSALA)[1]'', ''int'')                     AS IDSALA,
                    T.c.value(''(IDEQUIPOTRA)[1]'', ''int'')                AS IDEQUIPOTRA,
                    T.c.value(''(CODSERIPS)[1]'', ''varchar(50)'')          AS CODSERIPS,
                    T.c.value(''(CODDIAGNO)[1]'', ''varchar(50)'')          AS CODDIAGNO,
                    T.c.value(''(IDRIASCUPS)[1]'', ''int'')                 AS IDRIASCUPS,
                    T.c.value(''(CODCAUCAN)[1]'', ''varchar(50)'')          AS CODCAUCAN,
                    T.c.value(''(FECHCANCELA)[1]'', ''datetime'')          AS FECHCANCELA,
                    T.c.value(''(OBSCAUCAN)[1]'', ''nvarchar(1000)'')       AS OBSCAUCAN,
                    T.c.value(''(OBSCITPRE)[1]'', ''nvarchar(1000)'')       AS OBSCITPRE,
                    T.c.value(''(FECITADES)[1]'', ''datetime'')             AS FECITADES,
                    T.c.value(''(FECHAOFERTADA)[1]'', ''datetime'')         AS FECHAOFERTADA,
                    T.c.value(''(NUMINGRES)[1]'', ''varchar(50)'')          AS NUMINGRES,
                    T.c.value(''(CODTIPSOL)[1]'', ''varchar(5)'')           AS CODTIPSOL,
                    T.c.value(''(CODTIPCIT)[1]'', ''varchar(5)'')           AS CODTIPCIT,
                    T.c.value(''(CODESTCIT)[1]'', ''varchar(5)'')           AS CODESTCIT,
                    T.c.value(''(CITAEXTRA)[1]'', ''int'')                  AS CITAEXTRA,
                    T.c.value(''(OBSERVACI)[1]'', ''nvarchar(1000)'')       AS OBSERVACI,
                    T.c.value(''(TIPSOLICITU)[1]'', ''int'')                AS TIPSOLICITU,
                    T.c.value(''(TIPTRATAMIENTO)[1]'', ''int'')             AS TIPTRATAMIENTO,
                    T.c.value(''(CODCAUINA)[1]'', ''varchar(50)'')          AS CODCAUINA,
                    T.c.value(''(OBSCAUINA)[1]'', ''nvarchar(1000)'')       AS OBSCAUINA,
                    T.c.value(''(FECHAINA)[1]'', ''datetime'')              AS FECHAINA,
                    T.c.value(''(CONFASIST)[1]'', ''varchar(5)'')           AS CONFASIST,
                    T.c.value(''(MODALIDAD)[1]'', ''int'')                  AS MODALIDAD,
                    T.c.value(''(IDHCORDCICLOSD)[1]'', ''int'')             AS IDHCORDCICLOSD,
                    T.c.value(''(IDDESCRIPCIONRELACIONADA)[1]'', ''int'')   AS IDDESCRIPCIONRELACIONADA
                FROM @CitasXmlParam.nodes(''/Citas/Cita'') AS T(c)
            ) AS XmlCitas
            WHERE CODCENATE IN ('+ @Centro +')
              AND '+ QUOTENAME(@campo) +' BETWEEN @FechaInicioParam AND @FechaFinParam
        ),

        CTE_Tablas1 AS (
            SELECT
                A.CODAUTONU AS ''Autonumerico'',
                CASE WHEN B.CODESPECI IS NULL THEN '' - '' ELSE RTRIM(B.CODESPECI) END AS ''Cod. Especialidad'',
                CASE WHEN B.DESESPECI IS NULL THEN '' - '' ELSE RTRIM(B.DESESPECI) END AS ''Especialidad'',
                CONCAT(RTRIM(B.CODESPECI), '' - '', RTRIM(B.DESESPECI)) AS ''Concatenado- Especialidad'',
                CASE WHEN C.CODCENATE IS NULL THEN '' - '' ELSE RTRIM(C.CODCENATE) END AS ''Cod. Centro Atencion'',
                CASE WHEN C.NOMCENATE IS NULL THEN '' - '' ELSE RTRIM(C.NOMCENATE) END AS ''Centro Atencion'',
                CONCAT(RTRIM(C.CODCENATE), '' - '', RTRIM(C.NOMCENATE)) AS ''Concatenado- Centro Atencion'',
                CASE
                    WHEN UF1.UFUCODIGO IS NULL AND UF2.UFUCODIGO IS NULL THEN '' - ''
                    WHEN UF1.UFUCODIGO IS NOT NULL THEN RTRIM(UF1.UFUCODIGO)
                    ELSE RTRIM(UF2.UFUCODIGO)
                END AS ''Cod. Unidad Funcional'',
                CASE
                    WHEN UF1.UFUCODIGO IS NULL AND UF2.UFUCODIGO IS NULL THEN '' - ''
                    WHEN UF1.UFUCODIGO IS NOT NULL THEN RTRIM(UF1.UFUDESCRI)
                    ELSE RTRIM(UF2.UFUDESCRI)
                END AS ''Unidad Funcional'',
                CASE
                    WHEN UF1.UFUCODIGO IS NULL AND UF2.UFUCODIGO IS NULL THEN '' - ''
                    WHEN UF1.UFUCODIGO IS NOT NULL THEN CONCAT(RTRIM(UF1.UFUCODIGO), '' - '', RTRIM(UF1.UFUDESCRI))
                    ELSE CONCAT(RTRIM(UF2.UFUCODIGO), '' - '', RTRIM(UF2.UFUDESCRI))
                END AS ''Concatenado- Unidad Funcional'',
                CASE WHEN D.IPCODPACI IS NULL THEN '' - '' ELSE RTRIM(D.IPCODPACI) END AS ''Identificacion'',
                CASE WHEN D.IPNOMCOMP IS NULL THEN '' - '' ELSE RTRIM(D.IPNOMCOMP) END AS ''Nombre Paciente'',
                CASE WHEN D.IPTELMOVI IS NULL OR RTRIM(D.IPTELMOVI) = '''' THEN '' - '' ELSE RTRIM(D.IPTELMOVI) END AS ''CELULAR'',
                CASE WHEN D.IPTELEFON IS NULL OR RTRIM(D.IPTELEFON) = '''' THEN '' - '' ELSE RTRIM(D.IPTELEFON) END AS ''TELEFONO'',
                CASE
                    WHEN HEA.Id IS NULL AND ENT.CODENTIDA IS NULL THEN '' - ''
                    WHEN HEA.Id IS NOT NULL THEN RTRIM(HEA.Code)
                    ELSE RTRIM(ENT.CODENTIDA)
                END AS ''Cod. Entidad'',
                CASE
                    WHEN HEA.Id IS NULL AND ENT.CODENTIDA IS NULL THEN '' - ''
                    WHEN HEA.Id IS NOT NULL THEN RTRIM(HEA.Name)
                    ELSE RTRIM(ENT.NOMENTIDA)
                END AS ''Entidad'',
                CASE
                    WHEN HEA.Id IS NULL AND ENT.CODENTIDA IS NULL THEN '' - ''
                    WHEN HEA.Id IS NOT NULL THEN CONCAT(RTRIM(HEA.Code), '' - '', RTRIM(HEA.Name))
                    ELSE CONCAT(RTRIM(ENT.CODENTIDA), '' - '', RTRIM(ENT.NOMENTIDA))
                END AS ''Concatenado- Entidad'',
                CASE WHEN E.CODPROSAL IS NULL THEN '' - '' ELSE RTRIM(E.CODPROSAL) END AS ''Cod. Profesional'',
                CASE WHEN E.CODPROSAL IS NULL THEN '' - '' ELSE RTRIM(E.NOMMEDICO) END AS ''Profesional'',
                CASE WHEN E.CODPROSAL IS NULL THEN '' - '' ELSE CONCAT(RTRIM(E.CODPROSAL), '' - '', RTRIM(E.NOMMEDICO)) END AS ''Concatenado- Profesional'',
                A.FECHORAIN AS ''F. Inicial cita'',
                A.FECHORAFI AS ''F. Final cita'',
                CASE WHEN J.CODIGOCON IS NULL THEN '' - '' ELSE RTRIM(J.CODIGOCON) END AS ''Cod. Consultorio'',
                CASE WHEN J.CODIGOCON IS NULL THEN '' - '' ELSE RTRIM(J.DESCRICON) END AS ''Consultorio'',
                CASE WHEN J.CODIGOCON IS NULL THEN '' - '' ELSE CONCAT(RTRIM(J.CODIGOCON), '' - '', RTRIM(J.DESCRICON)) END AS ''Concatenado- Consultorio'',
                CASE WHEN F.CODACTMED IS NULL THEN '' - '' ELSE RTRIM(F.CODACTMED) END AS ''Cod. Actividad'',
                CASE WHEN F.CODACTMED IS NULL THEN '' - '' ELSE RTRIM(F.DESACTMED) END AS ''Nom. Actividad'',
                CASE WHEN F.CODACTMED IS NULL THEN '' - '' ELSE CONCAT(RTRIM(F.CODACTMED), '' - '', RTRIM(F.DESACTMED)) END AS ''Concatenado- Actividad'',
                CASE
                    WHEN A.CODTIPSOL IS NULL THEN '' - ''
                    WHEN A.CODTIPSOL = ''0'' THEN ''Presencial''
                    WHEN A.CODTIPSOL = ''1'' THEN ''Telefónica''
                END AS ''Forma de solicitud'',
                CASE
                    WHEN A.CODTIPCIT IS NULL THEN '' - ''
                    WHEN A.CODTIPCIT = ''0'' THEN ''Primera Vez''
                    WHEN A.CODTIPCIT = ''1'' THEN ''Control''
                    WHEN A.CODTIPCIT = ''2'' THEN ''Pos Operatorio''
                    WHEN A.CODTIPCIT = ''3'' THEN ''Cita Web''
                END AS ''Tipo cita'',
                CASE
                    WHEN A.CODESTCIT IS NULL THEN '' - ''
                    WHEN A.CODESTCIT = ''0'' THEN ''Asignada''
                    WHEN A.CODESTCIT = ''1'' THEN ''Cumplida''
                    WHEN A.CODESTCIT = ''2'' THEN ''Incumplida''
                    WHEN A.CODESTCIT = ''3'' THEN ''PreAsignada''
                    WHEN A.CODESTCIT = ''4'' THEN ''Cancelada''
                END AS ''Estado cita'',
                CASE
                    WHEN A.CITAEXTRA IS NULL THEN '' - ''
                    WHEN A.CITAEXTRA = 0 THEN ''No''
                    WHEN A.CITAEXTRA = 1 THEN ''Si''
                END AS ''Cita extra'',
                CASE WHEN A.OBSERVACI IS NULL OR RTRIM(A.OBSERVACI) = '''' THEN '' - '' ELSE RTRIM(A.OBSERVACI) END AS ''Observacion cita'',
                CASE WHEN G.CODUSUARI IS NULL THEN '' - '' ELSE RTRIM(G.CODUSUARI) END AS ''Cod. Usuario Registro'',
                CASE WHEN G.CODUSUARI IS NULL THEN '' - '' ELSE RTRIM(G.NOMUSUARI) END AS ''Nom. Usuario Registro'',
                CASE WHEN G.CODUSUARI IS NULL THEN '' - '' ELSE CONCAT(RTRIM(G.CODUSUARI), '' - '', RTRIM(G.NOMUSUARI)) END AS ''Concatenado- Usuario Registro'',
                A.FECREGSIS AS ''F. registro DB'',
                CASE WHEN A.OBSCITPRE IS NULL OR RTRIM(A.OBSCITPRE) = '''' THEN '' - '' ELSE RTRIM(A.OBSCITPRE) END AS ''Observacion cita preasignada'',
                A.FECITADES AS ''F. deseada cita'',
                CASE
                    WHEN A.TIPSOLICITU IS NULL THEN '' - ''
                    WHEN A.TIPSOLICITU = 1 THEN ''Cita Medica''
                    WHEN A.TIPSOLICITU = 2 THEN ''Cita Apoyo Diagnostico''
                    WHEN A.TIPSOLICITU = 3 THEN ''Cita Tratamiento Especiales''
                END AS ''Tipo de solicitud'',
                CASE WHEN K.CODIGSALA IS NULL THEN '' - '' ELSE RTRIM(K.CODIGSALA) END AS ''Cod. Sala'',
                CASE WHEN K.CODIGSALA IS NULL THEN '' - '' ELSE RTRIM(K.DESCRIPSAL) END AS ''Nombre Sala'',
                CASE WHEN K.CODIGSALA IS NULL THEN '' - '' ELSE CONCAT(RTRIM(K.CODIGSALA), '' - '', RTRIM(K.DESCRIPSAL)) END AS ''Concatenado- Sala'',
                CASE WHEN L.CODEQUIPO IS NULL THEN '' - '' ELSE RTRIM(L.CODEQUIPO) END AS ''Cod. Equipo Tratamiento'',
                CASE WHEN L.CODEQUIPO IS NULL THEN '' - '' ELSE RTRIM(L.DESCREQUI) END AS ''Equipo Tratamiento'',
                CASE WHEN L.CODEQUIPO IS NULL THEN '' - '' ELSE CONCAT(RTRIM(L.CODEQUIPO), '' - '', RTRIM(L.DESCREQUI)) END AS ''Concatenado- Equipo Tratamiento'',
                CASE WHEN H.CODUSUARI IS NULL THEN '' - '' ELSE RTRIM(H.CODUSUARI) END AS ''Cod. Usuario Cancela'',
                CASE WHEN H.CODUSUARI IS NULL THEN '' - '' ELSE RTRIM(H.NOMUSUARI) END AS ''Usuario Cancela'',
                CASE WHEN H.CODUSUARI IS NULL THEN '' - '' ELSE CONCAT(RTRIM(H.CODUSUARI), '' - '', RTRIM(H.NOMUSUARI)) END AS ''Concatenado- Usuario Cancela'',
                A.FECHCANCELA AS ''Fecha Cancelacion'',
                CASE WHEN P.DESCAUCAN IS NULL THEN '' - '' ELSE RTRIM(P.DESCAUCAN) END AS ''Causa de Cancelacion'',
                CASE WHEN A.OBSCAUCAN IS NULL OR RTRIM(A.OBSCAUCAN) = '''' THEN '' - '' ELSE RTRIM(A.OBSCAUCAN) END AS ''Observacion Cancelacion'',
                CASE WHEN M.CODSERIPS IS NULL THEN '' - '' ELSE RTRIM(M.CODSERIPS) END AS ''Cod. CUPS'',
                CASE WHEN M.CODSERIPS IS NULL THEN '' - '' ELSE RTRIM(M.DESSERIPS) END AS ''CUPS'',
                CASE WHEN M.CODSERIPS IS NULL THEN '' - '' ELSE CONCAT(RTRIM(M.CODSERIPS), '' - '', RTRIM(M.DESSERIPS)) END AS ''Concatenado- CUPS'',
                CASE WHEN T.Id IS NULL THEN '' - '' ELSE RTRIM(T.Code) END AS ''Cod. Descripcion relacionada'',
                CASE WHEN T.Id IS NULL THEN '' - '' ELSE RTRIM(T.Name) END AS ''Descripcion relacionada'',
                CASE WHEN T.Id IS NULL THEN '' - '' ELSE CONCAT(RTRIM(T.Code), '' - '', RTRIM(T.Name)) END AS ''Concatenado- Descripcion relacionada'',
                CASE WHEN A.NUMINGRES IS NULL OR RTRIM(A.NUMINGRES) = '''' THEN '' - '' ELSE RTRIM(A.NUMINGRES) END AS ''Ingreso'',
                CASE WHEN N.CODDIAGNO IS NULL THEN '' - '' ELSE RTRIM(N.CODDIAGNO) END AS ''Cod. Diagnostico'',
                CASE WHEN N.CODDIAGNO IS NULL THEN '' - '' ELSE RTRIM(N.NOMDIAGNO) END AS ''Diagnostico'',
                CASE WHEN N.CODDIAGNO IS NULL THEN '' - '' ELSE CONCAT(RTRIM(N.CODDIAGNO), '' - '', RTRIM(N.NOMDIAGNO)) END AS ''Concatenado- Diagnostico'',
                CASE
                    WHEN A.TIPTRATAMIENTO IS NULL THEN '' - ''
                    WHEN A.TIPTRATAMIENTO = 1 THEN ''Quimioterapia''
                    WHEN A.TIPTRATAMIENTO = 2 THEN ''RadioTerapia''
                    WHEN A.TIPTRATAMIENTO = 3 THEN ''Diálisis''
                    WHEN A.TIPTRATAMIENTO = 4 THEN ''Braquiterapia''
                END AS ''Tipo Tratamiento'',
                CASE WHEN Q.NOMBRE IS NULL THEN '' - '' ELSE RTRIM(Q.NOMBRE) END AS ''RIAS'',
                A.FECHAOFERTADA AS ''Fecha Ofertada'',
                CASE WHEN A.CODCAUINA IS NULL THEN '' - '' ELSE RTRIM(A.CODCAUINA) END AS ''Causa Inatencion'',
                CASE WHEN A.OBSCAUINA IS NULL OR RTRIM(A.OBSCAUINA) = '''' THEN '' - '' ELSE RTRIM(A.OBSCAUINA) END AS ''Observacion Inatencion'',
                CASE WHEN I.CODUSUARI IS NULL THEN '' - '' ELSE RTRIM(I.CODUSUARI) END AS ''Cod. Usuario Registra Inatencion'',
                CASE WHEN I.CODUSUARI IS NULL THEN '' - '' ELSE RTRIM(I.NOMUSUARI) END AS ''Usuario Registra Inatencion'',
                CASE WHEN I.CODUSUARI IS NULL THEN '' - '' ELSE CONCAT(RTRIM(I.CODUSUARI), '' - '', RTRIM(I.NOMUSUARI)) END AS ''Concatenado- Usuario Registra Inatencion'',
                A.FECHAINA AS ''Fecha Registro Inatencion'',
                CASE
                    WHEN A.CONFASIST IS NULL THEN '' - ''
                    WHEN A.CONFASIST = ''1'' THEN ''Confirmada''
                    WHEN A.CONFASIST = ''2'' THEN ''Cancelada''
                    WHEN A.CONFASIST = ''3'' THEN ''Sin Definir''
                END AS ''Confirmar Asistencia'',
                CASE
                    WHEN A.MODALIDAD IS NULL THEN '' - ''
                    WHEN A.MODALIDAD = 0 THEN ''Presencial''
                    WHEN A.MODALIDAD = 1 THEN ''Teleconsulta''
                END AS ''Modalidad'',
                CASE
                    WHEN R.Id IS NULL THEN '' - ''
                    ELSE CONCAT(
                        ''Ciclo: '', RTRIM(CONVERT(varchar(10), R.CICLO)), ''/'', QA.TOTAL_CICLOS,
                        ''  Día: '', RTRIM(CONVERT(varchar(10), R.DIA)), ''/'', CA.ULTIMO_DIA
                    )
                END AS ''Intervalo''
            FROM CTE_AGASICITA AS A WITH (NOLOCK)
                INNER JOIN ADCENATEN  AS C   WITH (NOLOCK) ON A.CODCENATE        = C.CODCENATE
                INNER JOIN INPACIENT  AS D   WITH (NOLOCK) ON A.IPCODPACI         = D.IPCODPACI
                LEFT  JOIN INESPECIA  AS B   WITH (NOLOCK) ON A.CODESPECI         = B.CODESPECI
                LEFT  JOIN INPROFSAL  AS E   WITH (NOLOCK) ON A.CODPROSAL         = E.CODPROSAL
                LEFT  JOIN AGACTIMED  AS F   WITH (NOLOCK) ON A.CODACTMED         = F.CODACTMED
                INNER JOIN SEGusuaru  AS G   WITH (NOLOCK) ON A.CODUSUASI         = G.CODUSUARI
                LEFT  JOIN SEGusuaru  AS H   WITH (NOLOCK) ON A.CANCELUSU         = H.CODUSUARI
                LEFT  JOIN SEGusuaru  AS I   WITH (NOLOCK) ON A.CODUSUINA         = I.CODUSUARI
                LEFT  JOIN AGCONSULT  AS J   WITH (NOLOCK) ON A.CODIGOCON         = J.CODIGOCON  AND A.CODCENATE = J.CODCENATE
                LEFT  JOIN AGENSALAC  AS K   WITH (NOLOCK) ON A.IDSALA            = K.CODCONCEC  AND A.CODCENATE = K.CODCENATE
                LEFT  JOIN AGEQUIPTRA AS L   WITH (NOLOCK) ON A.IDEQUIPOTRA       = L.ID
                LEFT  JOIN INCUPSIPS  AS M   WITH (NOLOCK) ON A.CODSERIPS         = M.CODSERIPS
                LEFT  JOIN INDIAGNOS  AS N   WITH (NOLOCK) ON A.CODDIAGNO         = N.CODDIAGNO
                LEFT  JOIN RIASCUPS   AS O   WITH (NOLOCK) ON A.IDRIASCUPS        = O.ID
                LEFT  JOIN RIAS       AS Q   WITH (NOLOCK) ON O.IDRIAS            = Q.ID
                LEFT  JOIN AGCAUCANC  AS P   WITH (NOLOCK) ON A.CODCAUCAN         = P.CODCAUCAN
                LEFT  JOIN INUNIFUNC  AS UF1 WITH (NOLOCK) ON J.UFUCODIGO         = UF1.UFUCODIGO
                LEFT  JOIN INUNIFUNC  AS UF2 WITH (NOLOCK) ON K.UFUCODIGO         = UF2.UFUCODIGO
                LEFT  JOIN INENTIDAD  AS ENT WITH (NOLOCK) ON D.CODENTIDA         = ENT.CODENTIDA
                LEFT  JOIN Contract.HealthAdministrator AS HEA WITH (NOLOCK) ON A.GENCONENTITY = HEA.Id
                LEFT  JOIN EHR.HCORDCICLOSD AS R WITH (NOLOCK) ON A.IDHCORDCICLOSD = R.ID
                OUTER APPLY (
                    SELECT TOP 1 RTRIM(QM.CICLOS) AS TOTAL_CICLOS
                    FROM EHR.HCORDQUIMIO QM
                    WHERE QM.ID = R.IDHCORDQUIMIO
                ) QA
                OUTER APPLY (
                    SELECT TOP 1 RTRIM(CD.DIA) AS ULTIMO_DIA
                    FROM EHR.HCORDCICLOSD CD
                    WHERE CD.IDHCORDQUIMIO = R.IDHCORDQUIMIO
                      AND CD.CICLO         = R.CICLO
                      AND CD.ADMISTRADIACASA = 0
                      AND CD.ESTADODIA    <> 3
                    ORDER BY CD.DIA DESC
                ) CA
                LEFT  JOIN Contract.CUPSEntityContractDescriptions AS S WITH (NOLOCK) ON A.IDDESCRIPCIONRELACIONADA = S.Id
                LEFT  JOIN Contract.ContractDescriptions            AS T WITH (NOLOCK) ON S.ContractDescriptionId   = T.Id
        ),

        CTE_AGEPROGQX AS (
            SELECT * FROM (
                SELECT
                    T.c.value(''(CODAUTONU)[1]'', ''int'')                  AS CODAUTONU,
                    T.c.value(''(CODCENATE)[1]'', ''varchar(50)'')          AS CODCENATE,
                    T.c.value(''(FECREGSIS)[1]'', ''datetime'')             AS FECREGSIS,
                    T.c.value(''(FECHORAIN)[1]'', ''datetime'')             AS FECHORAIN,
                    T.c.value(''(FECHORAFI)[1]'', ''datetime'')             AS FECHORAFI,
                    T.c.value(''(CODESPECI)[1]'', ''varchar(50)'')          AS CODESPECI,
                    T.c.value(''(IPCODPACI)[1]'', ''varchar(50)'')          AS IPCODPACI,
                    T.c.value(''(CODPROSAL)[1]'', ''varchar(50)'')          AS CODPROSAL,
                    T.c.value(''(CODUSUASI)[1]'', ''varchar(50)'')          AS CODUSUASI,
                    T.c.value(''(CODUSUCAN)[1]'', ''varchar(50)'')          AS CODUSUCAN,
                    T.c.value(''(CODUSUINA)[1]'', ''varchar(50)'')          AS CODUSUINA,
                    T.c.value(''(AGENSALAC)[1]'', ''int'')                  AS AGENSALAC,
                    T.c.value(''(CODSERIPS)[1]'', ''varchar(50)'')          AS CODSERIPS,
                    T.c.value(''(DiagnosisCode)[1]'', ''varchar(50)'')      AS DiagnosisCode,
                    T.c.value(''(CODCAUCAN)[1]'', ''varchar(50)'')          AS CODCAUCAN,
                    T.c.value(''(NUMINGRES)[1]'', ''varchar(50)'')          AS NUMINGRES,
                    T.c.value(''(ORIGENQX)[1]'', ''int'')                   AS ORIGENQX,
                    T.c.value(''(CODESTPQX)[1]'', ''varchar(5)'')           AS CODESTPQX,
                    T.c.value(''(OBSERVACION)[1]'', ''nvarchar(1000)'')     AS OBSERVACION,
                    T.c.value(''(FECHACAN)[1]'', ''datetime'')              AS FECHACAN,
                    T.c.value(''(OBSERCAN)[1]'', ''nvarchar(1000)'')        AS OBSERCAN,
                    T.c.value(''(CODCAUINA)[1]'', ''varchar(50)'')          AS CODCAUINA,
                    T.c.value(''(OBSCAUINA)[1]'', ''nvarchar(1000)'')       AS OBSCAUINA,
                    T.c.value(''(FECHAINA)[1]'', ''datetime'')              AS FECHAINA,
                    T.c.value(''(CONFASIST)[1]'', ''varchar(5)'')           AS CONFASIST,
                    T.c.value(''(IDDESCRIPCIONRELACIONADA)[1]'', ''int'')   AS IDDESCRIPCIONRELACIONADA
                FROM @CirugiasXmlParam.nodes(''/Cirugias/Cirugia'') AS T(c)
            ) AS XmlCirugias
            WHERE CODCENATE IN ('+ @Centro +')
              AND '+ QUOTENAME(@campo) +' BETWEEN @FechaInicioParam AND @FechaFinParam
        ),

        CTE_Tablas2 AS (
            SELECT
                A.CODAUTONU AS ''Autonumerico'',
                CASE WHEN B.CODESPECI IS NULL THEN '' - '' ELSE RTRIM(B.CODESPECI) END AS ''Cod. Especialidad'',
                CASE WHEN B.DESESPECI IS NULL THEN '' - '' ELSE RTRIM(B.DESESPECI) END AS ''Especialidad'',
                CONCAT(RTRIM(B.CODESPECI), '' - '', RTRIM(B.DESESPECI)) AS ''Concatenado- Especialidad'',
                CASE WHEN C.CODCENATE IS NULL THEN '' - '' ELSE RTRIM(C.CODCENATE) END AS ''Cod. Centro Atencion'',
                CASE WHEN C.NOMCENATE IS NULL THEN '' - '' ELSE RTRIM(C.NOMCENATE) END AS ''Centro Atencion'',
                CONCAT(RTRIM(C.CODCENATE), '' - '', RTRIM(C.NOMCENATE)) AS ''Concatenado- Centro Atencion'',
                CASE
                    WHEN UF1.UFUCODIGO IS NULL AND UF2.UFUCODIGO IS NULL THEN '' - ''
                    WHEN UF1.UFUCODIGO IS NOT NULL THEN RTRIM(UF1.UFUCODIGO)
                    ELSE RTRIM(UF2.UFUCODIGO)
                END AS ''Cod. Unidad Funcional'',
                CASE
                    WHEN UF1.UFUCODIGO IS NULL AND UF2.UFUCODIGO IS NULL THEN '' - ''
                    WHEN UF1.UFUCODIGO IS NOT NULL THEN RTRIM(UF1.UFUDESCRI)
                    ELSE RTRIM(UF2.UFUDESCRI)
                END AS ''Unidad Funcional'',
                CASE
                    WHEN UF1.UFUCODIGO IS NULL AND UF2.UFUCODIGO IS NULL THEN '' - ''
                    WHEN UF1.UFUCODIGO IS NOT NULL THEN CONCAT(RTRIM(UF1.UFUCODIGO), '' - '', RTRIM(UF1.UFUDESCRI))
                    ELSE CONCAT(RTRIM(UF2.UFUCODIGO), '' - '', RTRIM(UF2.UFUDESCRI))
                END AS ''Concatenado- Unidad Funcional'',
                CASE WHEN D.IPCODPACI IS NULL THEN '' - '' ELSE RTRIM(D.IPCODPACI) END AS ''Identificacion'',
                CASE WHEN D.IPNOMCOMP IS NULL THEN '' - '' ELSE RTRIM(D.IPNOMCOMP) END AS ''Nombre Paciente'',
                CASE WHEN D.IPTELMOVI IS NULL OR RTRIM(D.IPTELMOVI) = '''' THEN '' - '' ELSE RTRIM(D.IPTELMOVI) END AS ''CELULAR'',
                CASE WHEN D.IPTELEFON IS NULL OR RTRIM(D.IPTELEFON) = '''' THEN '' - '' ELSE RTRIM(D.IPTELEFON) END AS ''TELEFONO'',
                CASE
                    WHEN HEA.Id IS NULL AND HEAPAC.Code IS NULL THEN '' - ''
                    WHEN HEA.Id IS NOT NULL THEN RTRIM(HEA.Code)
                    ELSE RTRIM(HEAPAC.Code)
                END AS ''Cod. Entidad'',
                CASE
                    WHEN HEA.Id IS NULL AND HEAPAC.Code IS NULL THEN '' - ''
                    WHEN HEA.Id IS NOT NULL THEN RTRIM(HEA.Name)
                    ELSE RTRIM(HEAPAC.Name)
                END AS ''Entidad'',
                CASE
                    WHEN HEA.Id IS NULL AND HEAPAC.Code IS NULL THEN '' - ''
                    WHEN HEA.Id IS NOT NULL THEN CONCAT(RTRIM(HEA.Code), '' - '', RTRIM(HEA.Name))
                    ELSE CONCAT(RTRIM(HEAPAC.Code), '' - '', RTRIM(HEAPAC.Name))
                END AS ''Concatenado- Entidad'',
                CASE WHEN E.CODPROSAL IS NULL THEN '' - '' ELSE RTRIM(E.CODPROSAL) END AS ''Cod. Profesional'',
                CASE WHEN E.CODPROSAL IS NULL THEN '' - '' ELSE RTRIM(E.NOMMEDICO) END AS ''Profesional'',
                CASE WHEN E.CODPROSAL IS NULL THEN '' - '' ELSE CONCAT(RTRIM(E.CODPROSAL), '' - '', RTRIM(E.NOMMEDICO)) END AS ''Concatenado- Profesional'',
                A.FECHORAIN AS ''F. Inicial cita'',
                A.FECHORAFI AS ''F. Final cita'',
                '' - '' AS ''Cod. Consultorio'',
                '' - '' AS ''Consultorio'',
                '' - '' AS ''Concatenado- Consultorio'',
                '' - '' AS ''Cod. Actividad'',
                '' - '' AS ''Nom. Actividad'',
                '' - '' AS ''Concatenado- Actividad'',
                CASE
                    WHEN A.ORIGENQX IS NULL THEN '' - ''
                    WHEN A.ORIGENQX = 1    THEN ''Ambulatoria''
                    WHEN A.ORIGENQX = 2    THEN ''Hospitalaria''
                END AS ''Forma de solicitud'',
                ''-'' AS ''Tipo cita'',
                CASE
                    WHEN A.CODESTPQX IS NULL THEN '' - ''
                    WHEN A.CODESTPQX = ''0'' THEN ''Cirugia Programada''
                    WHEN A.CODESTPQX = ''1'' THEN ''Paciente admitido (Cirugia Origen Ambulatoria)''
                    WHEN A.CODESTPQX = ''2'' THEN ''Paciente en sala de espera''
                    WHEN A.CODESTPQX = ''3'' THEN ''Paciente en sala quirurgica''
                    WHEN A.CODESTPQX = ''4'' THEN ''Paciente en recuperación''
                    WHEN A.CODESTPQX = ''5'' THEN ''Paciente con alta''
                    WHEN A.CODESTPQX = ''6'' THEN ''Cancelada''
                END AS ''Estado cita'',
                '' - '' AS ''Cita extra'',
                CASE WHEN A.OBSERVACION IS NULL OR RTRIM(A.OBSERVACION) = '''' THEN '' - '' ELSE RTRIM(A.OBSERVACION) END AS ''Observacion cita'',
                CASE WHEN G.CODUSUARI IS NULL THEN '' - '' ELSE RTRIM(G.CODUSUARI) END AS ''Cod. Usuario Registro'',
                CASE WHEN G.CODUSUARI IS NULL THEN '' - '' ELSE RTRIM(G.NOMUSUARI) END AS ''Nom. Usuario Registro'',
                CASE WHEN G.CODUSUARI IS NULL THEN '' - '' ELSE CONCAT(RTRIM(G.CODUSUARI), '' - '', RTRIM(G.NOMUSUARI)) END AS ''Concatenado- Usuario Registro'',
                A.FECREGSIS AS ''F. registro DB'',
                '' - '' AS ''Observacion cita preasignada'',
                A.FECHORAIN AS ''F. deseada cita'',
                ''Cirugía'' AS ''Tipo de solicitud'',
                CASE WHEN K.CODIGSALA IS NULL THEN '' - '' ELSE RTRIM(K.CODIGSALA) END AS ''Cod. Sala'',
                CASE WHEN K.CODIGSALA IS NULL THEN '' - '' ELSE RTRIM(K.DESCRIPSAL) END AS ''Nombre Sala'',
                CASE WHEN K.CODIGSALA IS NULL THEN '' - '' ELSE CONCAT(RTRIM(K.CODIGSALA), '' - '', RTRIM(K.DESCRIPSAL)) END AS ''Concatenado- Sala'',
                '' - '' AS ''Cod. Equipo Tratamiento'',
                '' - '' AS ''Equipo Tratamiento'',
                '' - '' AS ''Concatenado- Equipo Tratamiento'',
                CASE WHEN H.CODUSUARI IS NULL THEN '' - '' ELSE RTRIM(H.CODUSUARI) END AS ''Cod. Usuario Cancela'',
                CASE WHEN H.CODUSUARI IS NULL THEN '' - '' ELSE RTRIM(H.NOMUSUARI) END AS ''Usuario Cancela'',
                CASE WHEN H.CODUSUARI IS NULL THEN '' - '' ELSE CONCAT(RTRIM(H.CODUSUARI), '' - '', RTRIM(H.NOMUSUARI)) END AS ''Concatenado- Usuario Cancela'',
                A.FECHACAN AS ''Fecha Cancelacion'',
                CASE WHEN P.DESCAUCAN IS NULL THEN '' - '' ELSE RTRIM(P.DESCAUCAN) END AS ''Causa de Cancelacion'',
                CASE WHEN A.OBSERCAN IS NULL OR RTRIM(A.OBSERCAN) = '''' THEN '' - '' ELSE RTRIM(A.OBSERCAN) END AS ''Observacion Cancelacion'',
                CASE WHEN M.CODSERIPS IS NULL THEN '' - '' ELSE RTRIM(M.CODSERIPS) END AS ''Cod. CUPS'',
                CASE WHEN M.CODSERIPS IS NULL THEN '' - '' ELSE RTRIM(M.DESSERIPS) END AS ''CUPS'',
                CASE WHEN M.CODSERIPS IS NULL THEN '' - '' ELSE CONCAT(RTRIM(M.CODSERIPS), '' - '', RTRIM(M.DESSERIPS)) END AS ''Concatenado- CUPS'',
                CASE WHEN T.Id IS NULL THEN '' - '' ELSE RTRIM(T.Code) END AS ''Cod. Descripcion relacionada'',
                CASE WHEN T.Id IS NULL THEN '' - '' ELSE RTRIM(T.Name) END AS ''Descripcion relacionada'',
                CASE WHEN T.Id IS NULL THEN '' - '' ELSE CONCAT(RTRIM(T.Code), '' - '', RTRIM(T.Name)) END AS ''Concatenado- Descripcion relacionada'',
                CASE WHEN A.NUMINGRES IS NULL OR RTRIM(A.NUMINGRES) = '''' THEN '' - '' ELSE RTRIM(A.NUMINGRES) END AS ''Ingreso'',
                CASE WHEN N.CODDIAGNO IS NULL THEN '' - '' ELSE RTRIM(N.CODDIAGNO) END AS ''Cod. Diagnostico'',
                CASE WHEN N.CODDIAGNO IS NULL THEN '' - '' ELSE RTRIM(N.NOMDIAGNO) END AS ''Diagnostico'',
                CASE WHEN N.CODDIAGNO IS NULL THEN '' - '' ELSE CONCAT(RTRIM(N.CODDIAGNO), '' - '', RTRIM(N.NOMDIAGNO)) END AS ''Concatenado- Diagnostico'',
                '' - '' AS ''Tipo Tratamiento'',
                '' - '' AS ''RIAS'',
                A.FECHORAIN AS ''Fecha Ofertada'',
                CASE WHEN A.CODCAUINA IS NULL THEN '' - '' ELSE RTRIM(A.CODCAUINA) END AS ''Causa Inatencion'',
                CASE WHEN A.OBSCAUINA IS NULL OR RTRIM(A.OBSCAUINA) = '''' THEN '' - '' ELSE RTRIM(A.OBSCAUINA) END AS ''Observacion Inatencion'',
                CASE WHEN I.CODUSUARI IS NULL THEN '' - '' ELSE RTRIM(I.CODUSUARI) END AS ''Cod. Usuario Registra Inatencion'',
                CASE WHEN I.CODUSUARI IS NULL THEN '' - '' ELSE RTRIM(I.NOMUSUARI) END AS ''Usuario Registra Inatencion'',
                CASE WHEN I.CODUSUARI IS NULL THEN '' - '' ELSE CONCAT(RTRIM(I.CODUSUARI), '' - '', RTRIM(I.NOMUSUARI)) END AS ''Concatenado- Usuario Registra Inatencion'',
                A.FECHAINA AS ''Fecha Registro Inatencion'',
                CASE
                    WHEN A.CONFASIST IS NULL THEN '' - ''
                    WHEN A.CONFASIST = ''1'' THEN ''Confirmada''
                    WHEN A.CONFASIST = ''2'' THEN ''Cancelada''
                    WHEN A.CONFASIST = ''3'' THEN ''Sin Definir''
                END AS ''Confirmar Asistencia'',
                ''Presencial'' AS ''Modalidad'',
                '' - ''        AS ''Intervalo''
            FROM CTE_AGEPROGQX AS A WITH (NOLOCK)
                INNER JOIN ADCENATEN AS C    WITH (NOLOCK) ON A.CODCENATE      = C.CODCENATE
                INNER JOIN INPACIENT AS D    WITH (NOLOCK) ON A.IPCODPACI       = D.IPCODPACI
                LEFT  JOIN INESPECIA AS B    WITH (NOLOCK) ON A.CODESPECI       = B.CODESPECI
                LEFT  JOIN INPROFSAL AS E    WITH (NOLOCK) ON A.CODPROSAL       = E.CODPROSAL
                INNER JOIN SEGusuaru AS G    WITH (NOLOCK) ON A.CODUSUASI       = G.CODUSUARI
                LEFT  JOIN SEGusuaru AS H    WITH (NOLOCK) ON A.CODUSUCAN       = H.CODUSUARI
                LEFT  JOIN SEGusuaru AS I    WITH (NOLOCK) ON A.CODUSUINA       = I.CODUSUARI
                LEFT  JOIN AGENSALAC AS K    WITH (NOLOCK) ON A.AGENSALAC       = K.CODCONCEC  AND A.CODCENATE = K.CODCENATE
                LEFT  JOIN INCUPSIPS AS M    WITH (NOLOCK) ON A.CODSERIPS       = M.CODSERIPS
                LEFT  JOIN INDIAGNOS AS N    WITH (NOLOCK) ON A.DiagnosisCode   = N.CODDIAGNO
                LEFT  JOIN AGCACANQX AS P    WITH (NOLOCK) ON A.CODCAUCAN       = P.CODCACANQ
                LEFT  JOIN INUNIFUNC AS UF1  WITH (NOLOCK) ON K.UFUCODIGO       = UF1.UFUCODIGO
                LEFT  JOIN INUNIFUNC AS UF2  WITH (NOLOCK) ON K.UFUCODIGO       = UF2.UFUCODIGO
                LEFT  JOIN ADINGRESO AS ING  WITH (NOLOCK) ON ING.NUMINGRES     = A.NUMINGRES
                LEFT  JOIN Contract.HealthAdministrator AS HEAPAC WITH (NOLOCK) ON D.GENCONENTITY   = HEAPAC.Id
                LEFT  JOIN Contract.HealthAdministrator AS HEA    WITH (NOLOCK) ON ING.GENCONENTITY = HEA.Id
                LEFT  JOIN Contract.CUPSEntityContractDescriptions AS S WITH (NOLOCK) ON A.IDDESCRIPCIONRELACIONADA = S.Id
                LEFT  JOIN Contract.ContractDescriptions            AS T WITH (NOLOCK) ON S.ContractDescriptionId   = T.Id
        )

        -- ↓ PAGINACIÓN: los 2 únicos cambios vs versión sin paginación
        SELECT Autonumerico, [Cod. Especialidad], Especialidad, [Concatenado- Especialidad],
               [Cod. Centro Atencion], [Centro Atencion], [Concatenado- Centro Atencion],
               [Cod. Unidad Funcional], [Unidad Funcional], [Concatenado- Unidad Funcional],
               Identificacion, [Nombre Paciente], CELULAR, TELEFONO,
               [Cod. Entidad], Entidad, [Concatenado- Entidad],
               [Cod. Profesional], Profesional, [Concatenado- Profesional],
               [F. Inicial cita], [F. Final cita],
               [Cod. Consultorio], Consultorio, [Concatenado- Consultorio],
               [Cod. Actividad], [Nom. Actividad], [Concatenado- Actividad],
               [Forma de solicitud], [Tipo cita], [Estado cita], [Cita extra],
               [Observacion cita], [Cod. Usuario Registro], [Nom. Usuario Registro],
               [Concatenado- Usuario Registro], [F. registro DB],
               [Observacion cita preasignada], [F. deseada cita], [Tipo de solicitud],
               [Cod. Sala], [Nombre Sala], [Concatenado- Sala],
               [Cod. Equipo Tratamiento], [Equipo Tratamiento], [Concatenado- Equipo Tratamiento],
               [Cod. Usuario Cancela], [Usuario Cancela], [Concatenado- Usuario Cancela],
               [Fecha Cancelacion], [Causa de Cancelacion], [Observacion Cancelacion],
               [Cod. CUPS], CUPS, [Concatenado- CUPS],
               [Cod. Descripcion relacionada], [Descripcion relacionada], [Concatenado- Descripcion relacionada],
               Ingreso, [Cod. Diagnostico], Diagnostico, [Concatenado- Diagnostico],
               [Tipo Tratamiento], RIAS, [Fecha Ofertada],
               [Causa Inatencion], [Observacion Inatencion],
               [Cod. Usuario Registra Inatencion], [Usuario Registra Inatencion],
               [Concatenado- Usuario Registra Inatencion], [Fecha Registro Inatencion],
               [Confirmar Asistencia], Modalidad, Intervalo
        FROM (
            SELECT * FROM CTE_Tablas1
            UNION
            SELECT * FROM CTE_Tablas2
        ) AS resultado
        ORDER BY Autonumerico ASC
        OFFSET (@PageIndexParam * @PageSizeParam) ROWS
        FETCH NEXT @PageSizeParam ROWS ONLY'

    -- Parámetros seguros vía sp_executesql (sin concatenación de fechas)
    EXEC sp_executesql @sql,
        N'@FechaInicioParam DATETIME, @FechaFinParam DATETIME, @PageIndexParam INT, @PageSizeParam INT, @CitasXmlParam XML, @CirugiasXmlParam XML',
        @FechaInicio, @FechaFin, @PageIndex, @PageSize, @CitasXml, @CirugiasXml

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte paginado de consulta de citas agendadas, permitiendo filtrar por centro de atención, rango de fechas (ya sea fecha de registro en el sistema o fecha/hora de inicio de la cita) y con soporte de paginación. Toca la entidad de agendamiento de citas (AGASICITA) y la enriquece con información del paciente (cédula, nombre, teléfono), especialidad, unidad funcional, centro de atención, profesional de salud, consultorio, actividad médica, entidad aseguradora y tipo/estado de la cita. Construye SQL dinámico para adaptar el campo de fecha utilizado en el filtro según el parámetro @TipoFecha (fecha de registro o fecha de inicio de cita). Es utilizado para reportería gerencial y operativa de citas médicas, agendamiento, productividad por profesional y seguimiento de asistencia a consultas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_ListarReporteConsultaCitas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_ListarReporteConsultaCitas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte paginado consolidado de citas médicas y cirugías programadas para uno o más centros de atención, en un rango de fechas (registro u hora de la cita), unificando ambas fuentes en un mismo conjunto de columnas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarReporteConsultaCitas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de centros debe venir como una lista válida lista para inyectar en una cláusula IN (e.g. valores entre comillas separados por comas).; Las fechas de inicio y fin no deben ser nulas para que el BETWEEN devuelva resultados.; El parámetro de tipo de fecha debe ser ''False'' para filtrar por FECREGSIS; cualquier otro valor filtra por FECHORAIN.; El tamaño de página debe ser > 0 para que FETCH NEXT devuelva filas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarReporteConsultaCitas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda columna nula o cadena vacía en el reporte se sustituye por el literal '' - '' para mantener una salida homogénea.; El reporte combina citas asistenciales (AGASICITA) con cirugías programadas (AGEPROGQX) mediante UNION, eliminando duplicados exactos.; Para cirugías, columnas no aplicables (consultorio, actividad, equipo de tratamiento, RIAS, tipo tratamiento, cita extra, observación pre-asignada) se devuelven siempre como '' - '' y la modalidad como ''Presencial''.; Para cirugías el ''Tipo de solicitud'' siempre se reporta como ''Cirugía''.; El filtrado de fechas se aplica al mismo campo (FECREGSIS o FECHORAIN según @TipoFecha) en ambas fuentes.; Las consultas se ejecutan con WITH (NOLOCK) en todas las tablas, asumiendo lectura sucia tolerada por ser un reporte.; El cálculo del último día del ciclo excluye días administrados en casa (ADMISTRADIACASA=0 obligatorio) y días con ESTADODIA = 3.; Solo se incluyen registros cuyo CODCENATE esté dentro de la lista de centros recibida.; La paginación se hace sobre el resultado unificado y ordenado por Autonumerico ascendente.; El SP se compila con WITH RECOMPILE para optimizar el plan según los parámetros de cada ejecución.; Las fechas y parámetros de paginación se pasan parametrizados a sp_executesql; solo la lista de centros y el nombre del campo de fecha se concatenan dinámicamente (este último protegido con QUOTENAME).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarReporteConsultaCitas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultado (UNION CTE_Tablas1 + CTE_Tablas2): Devuelve el conjunto unificado de citas (AGASICITA) y cirugías programadas (AGEPROGQX) ordenado por Autonumerico ASC, paginado con OFFSET (@PageIndex*@PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarReporteConsultaCitas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @TipoFecha = ''False'' → El filtro de rango de fechas se aplica sobre la columna FECREGSIS (fecha de registro en sistema). else El filtro de rango de fechas se aplica sobre la columna FECHORAIN (fecha/hora de inicio de la cita).; si CODTIPSOL en {0,1} (citas) → Se traduce a ''Presencial'' (0) o ''Telefónica'' (1); NULL se muestra como '' - ''.; si CODTIPCIT en {0,1,2,3} → Se traduce a Primera Vez / Control / Pos Operatorio / Cita Web.; si CODESTCIT en {0,1,2,3,4} → Estado de cita: Asignada / Cumplida / Incumplida / PreAsignada / Cancelada.; si CITAEXTRA = 1 → Se marca como ''Si''; CITAEXTRA = 0 → ''No''.; si TIPSOLICITU en {1,2,3} → Tipo de solicitud: Cita Medica / Cita Apoyo Diagnostico / Cita Tratamiento Especiales.; si TIPTRATAMIENTO en {1,2,3,4} → Tipo de tratamiento: Quimioterapia / RadioTerapia / Diálisis / Braquiterapia.; si CONFASIST en {1,2,3} → Confirmación de asistencia: Confirmada / Cancelada / Sin Definir.; si MODALIDAD = 0 / 1 (citas) → Modalidad Presencial (0) o Teleconsulta (1); en cirugías la modalidad se fuerza a ''Presencial''.; si ORIGENQX en {1,2} (cirugías) → Forma de solicitud: Ambulatoria (1) u Hospitalaria (2).; si CODESTPQX en {0..6} (cirugías) → Estado quirúrgico: Cirugia Programada / Paciente admitido / En sala de espera / En sala quirúrgica / En recuperación / Con alta / Cancelada.; si Existe orden de ciclo de quimioterapia (IDHCORDCICLOSD no nulo) → Se construye la columna ''Intervalo'' con formato ''Ciclo: x/total Día: y/ultimo'', donde ''ultimo_dia'' se calcula tomando el máximo DIA del mismo ciclo con ADMISTRADIACASA=0 y ESTADODIA<>3. else La columna Intervalo se muestra como '' - ''.; si HEA.Id no nulo (citas) → La entidad/aseguradora se toma de Contract.HealthAdministrator (código y nombre). else Se usa la entidad del paciente desde INENTIDAD (CODENTIDA/NOMENTIDA).; si ING.GENCONENTITY no nulo (cirugías) → La entidad se toma del ingreso (HealthAdministrator vinculado al ingreso). else Se usa la entidad asociada al paciente (HEAPAC) vía D.GENCONENTITY.; si UF1.UFUCODIGO no nulo → La unidad funcional reportada es la del consultorio (J.UFUCODIGO). else Se usa la unidad funcional de la sala (K.UFUCODIGO).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarReporteConsultaCitas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarReporteConsultaCitas';
-- GO
