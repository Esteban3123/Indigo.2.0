CREATE PROCEDURE [dbo].[SPCH_GestionHospitalariaPacientesHospitalizadosConResultados_EMR]
(
    @CentroAtencion Char(10),
    @UnidadFuncional Char(10),
    @AdmissionNumber varchar(20)
)
AS
BEGIN
	SET NOCOUNT ON;

    SELECT DISTINCT 
        CASE WHEN X.INDICAPAC='22' THEN 'presalida' 
        WHEN  I.NUMINGRES IS NULL THEN 'en_unidad'  
        ELSE 'salida' END AS Egreso
        , ISNULL(DESTINOPAC,0) as DestinoSalidaParcial
        , cast(' ' as char(100)) AS Origen
        , 'Normal' as Alerta
        , A.CODICAMAS AS 'Codigo Cama'
        , RTRIM(DESCCAMAS) AS Cama
        , dbo.ClaseHabitacion(A.CODCLAHAB) AS ClaseHabitacion
        , dbo.ClaseCama(A.CODCLACAM) AS 'Clase de Cama'
        , C.IPCODPACI AS Identificacion
        , H.IPTELEFON AS Telefono
        , H.IPDIRECCI AS Direccion
        , E.UFUDESCRI AS UnidadFuncional
        , C.NUMINGRES AS Ingreso
        , dbo.TipoAislamiento(A.CODAISLAM) AS Aislamiento
        , RTRIM(DESTIPEST) AS 'Tipo Estancia'
        , RTRIM(H.IPNOMCOMP) AS Paciente
        ,H.IPPRINOMB AS PrimerNombre
        ,H.IPSEGNOMB AS SegundoNombre
        ,H.IPPRIAPEL AS PrimerApellido
        ,H.IPSEGAPEL AS SegundoApellido
        , CAST(0 AS BIT) AS Resultado
        , A.CAMTRACIR AS TrasladoCirugia
        , A.CAMTRAMED AS TrasladoMedicamentos
        , A.CODCONCEC AS Consecutivo
        , CAST('' as bit) AS MuestraAlerta
        , K.CODESPECI AS CodigoEspecialidad
        , RTRIM(K.DESESPECI) AS DescripcionEspecialidad
        , IFECHAING
        , J.ESCADOWNT
        , J.ESCARASS
        , J.ESCNORPAC
        , J.ESCVASPAC
        , J.ESCAPAPAC
        , dbo.PuntajeEscalaDownTon(J.NUMINGRES, J.IPCODPACI) as PUNTAJEDOWN
        , dbo.PuntajeEscalaRass(J.NUMINGRES, J.IPCODPACI) as PUNTAJERASS
        , dbo.PuntajeEscalaNorton(J.NUMINGRES, J.IPCODPACI) as PUNTAJENORTON
        , dbo.PuntajeEscalaVas(J.NUMINGRES, J.IPCODPACI) as PUNTAJEVAS
        , dbo.PuntajeEscalaApache(J.NUMINGRES, J.IPCODPACI) as PUNTAJEAPACHE
        , (
            Select CAST(CASE WHEN COUNT(*) > 0 THEN 1 ELSE (SELECT CAST(CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS BIT) FROM HCESCDOWN AS HW where HW.IPCODPACI = J.IPCODPACI AND HW.NUMINGRES = J.NUMINGRES) END AS BIT) 
            from HCESCALAS HE 
            where HE.IPCODPACI = J.IPCODPACI 
                AND HE.NUMINGRES = J.NUMINGRES and HE.TIPOESCALA in (47,92,113)
        ) AS 'ESCALACAIDA'
        , (
            Select CAST(CASE WHEN COUNT(*) > 0 THEN 1 ELSE (
                Select CAST(CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS BIT) 
                from HCESCVASC VASC
                INNER JOIN HCESCVASD VASD ON VASC.CODCONSEC = VASD.CODCONSEC
                where VASC.IPCODPACI = J.IPCODPACI AND VASC.NUMINGRES = J.NUMINGRES) END AS BIT
            ) 
            from HCESCALAS HE 
            where HE.IPCODPACI = J.IPCODPACI 
                AND HE.NUMINGRES = J.NUMINGRES and HE.TIPOESCALA in (49,90,91,93)
        ) AS 'ESCALADOLOR'
        , (
            Select CAST(CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS BIT) 
            from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI 
                AND HE.NUMINGRES = J.NUMINGRES 
                and NOT(HE.TIPOESCALA in (47,92,49,90,91,93))
        ) AS 'ESCALASGENERAL'
        , (
            SELECT RESULTESCAIDA 
            FROM (
                Select TOP 1  HE.RESULTADO as RESULTESCAIDA, HE.FECHAREGISTRO AS FECHARESULTESCAIDA from HCESCALAS HE
                where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and HE.TIPOESCALA in (47,92,113)
                ORDER BY FECHARESULTESCAIDA desc
                UNION
                Select TOP 1 ISNULL(HW.RESULTADO, 0) as RESULTESCAIDA, HW.FECREGSIS AS FECHARESULTESCAIDA from HCESCDOWN HW 
                where HW.IPCODPACI = J.IPCODPACI AND HW.NUMINGRES = J.NUMINGRES and HW.TIPOESCALA in (47,92,113) 
                ORDER BY FECHARESULTESCAIDA DESC
            ) AS RESULTESCAIDA
            ORDER BY FECHARESULTESCAIDA DESC
            OFFSET 0 ROWS FETCH FIRST 1 ROWS ONLY
        ) AS 'RESULTESCAIDA'
        , (
            SELECT RESULTESDOLOR 
            FROM (
                Select TOP 1 VASD.CODTIPDOL AS RESULTESDOLOR, VASC.FECREGSIS AS FECHARESULTADODOLOR from HCESCVASC VASC
                INNER JOIN HCESCVASD VASD ON VASC.CODCONSEC = VASD.CODCONSEC
                where VASC.IPCODPACI = J.IPCODPACI AND VASC.NUMINGRES = J.NUMINGRES  
                ORDER BY FECHARESULTADODOLOR DESC
                
                UNION
                
                Select TOP 1  HE.RESULTADO AS RESULTESDOLOR, HE.FECHAREGISTRO AS FECHARESULTADODOLOR
                from HCESCALAS HE 
                where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and HE.TIPOESCALA in (49,90,91,93) 
                ORDER BY FECHARESULTADODOLOR DESC
            ) AS RESULTESDOLOR
            ORDER BY FECHARESULTADODOLOR DESC
            OFFSET 0 ROWS FETCH FIRST 1 ROWS ONLY
        ) AS 'RESULTESDOLOR'
        , (
            SELECT TIPOESCAIDA 
            FROM (
                Select TOP 1 HE.TIPOESCALA AS TIPOESCAIDA, HE.FECHAREGISTRO AS FECHAREGISTROTIPOCAIDA from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and HE.TIPOESCALA in (47,92,113) 
                ORDER BY HE.FECHAREGISTRO DESC -- AS 'TIPOESDOLOR',
                union 
                Select TOP 1 HW.TIPOESCALA AS TIPOESCAIDA, HW.FECREGSIS AS FECHAREGISTROTIPOCAIDA from HCESCDOWN HW 
                where HW.IPCODPACI = J.IPCODPACI AND HW.NUMINGRES = J.NUMINGRES 
                ORDER BY FECHAREGISTROTIPOCAIDA DESC) AS TIPOESCAIDA
                ORDER BY FECHAREGISTROTIPOCAIDA DESC
                OFFSET 0 ROWS FETCH FIRST 1 ROWS ONLY
        ) AS 'TIPOESCAIDA'
        , (
            SELECT TIPOESDOLOR 
            FROM (
                Select TOP 1 HE.TIPOESCALA AS TIPOESDOLOR, HE.FECHAREGISTRO AS FECHAREGISTROTIPODOLOR from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and HE.TIPOESCALA in (49,90,91,93) 
                ORDER BY HE.FECHAREGISTRO DESC -- AS 'TIPOESDOLOR',
                union 
                Select TOP (1) 49 AS TIPOESDOLOR, VASC.FECREGSIS AS FECHAREGISTROTIPODOLOR from HCESCVASC VASC
                INNER JOIN HCESCVASD VASD ON VASC.CODCONSEC = VASD.CODCONSEC
                where VASC.IPCODPACI = J.IPCODPACI AND VASC.NUMINGRES = J.NUMINGRES and VASD.CODTIPDOL IS NOT NULL 
                ORDER BY FECHAREGISTROTIPODOLOR DESC) AS TIPOESDOLOR
            ORDER BY FECHAREGISTROTIPODOLOR DESC
            OFFSET 0 ROWS FETCH FIRST 1 ROWS ONLY
        ) AS 'TIPOESDOLOR'
        , CASE WHEN H.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS
        , H.ZONAPARTADA
        , L.RIESGOAGRE
        , (
            SELECT count(*) 
            FROM dbo.ADPOBESPEPAC Z with(nolock) 
            INNER JOIN ADPOBESPE X with(nolock) ON X.ID = Z.IDADPOBESPE 
            WHERE IPCODPACI = H.IPCODPACI AND TIPOPOESPERIES = 1
        ) AS POBESPECIAL
        , J.VIVESOLO
        , (SELECT count(*) FROM ADACOMPAN with(nolock) WHERE NUMINGRES = J.NUMINGRES) AS ACOMPANANTES
        , (SELECT count(*) FROM HCORHEMBOL M with(nolock) INNER JOIN HCORHEMCO N with(nolock) ON  M.HCORHEMCOID = N.ID AND N.IPCODPACI = H.IPCODPACI WHERE M.CONFRECBOL = 1) AS BOLSAS
        , (SELECT count(*) FROM  HCORHEMBOL M with(nolock) INNER JOIN HCORHEMCO N with(nolock) ON  M.HCORHEMCOID = N.ID AND N.IPCODPACI = H.IPCODPACI WHERE M.ESTADO = 6 AND CONFRECBOL = 1 ) AS ESTADO
        , CONVERT(BIT,0) AS Riesgo
        , CONVERT(BIT,0) AS Hemocomponente
        , IPFECNACI AS FechaNacimiento
        , H.IPSEXOPAC AS Sexo
        , J.CODTIPPAC as TipoPaciente
        , RTRIM(P.CODENTIDA) + '-'+ RTRIM(P.NOMENTIDA) as EntidadPaciente
        , RTRIM(Q.CODDIAGNO) + '-' + RTRIM(Q.NOMDIAGNO) as Diagnostico
        , Z.Color
        , Prof.CODPROSAL AS CodigoMedicoTratante
        , RTRIM(Prof.NOMMEDICO) AS NombreMedicoTratante
        , iif((
            select top 1 COUNT(*) 
            from dbo.RecommendPatient 
            where IPCODPACI = C.IPCODPACI and NUMINGRES = C.NUMINGRES and Status = 1) > 0, Convert(Bit,1), Convert(Bit,0)
        ) as Recomendacion,
         -- Resultados pendientes (contadores)
        (
                (SELECT COUNT(*) FROM dbo.HCORDIMAG with(nolock) 
                 WHERE IPCODPACI = J.IPCODPACI 
                   AND NUMINGRES = J.NUMINGRES 
                   AND RTRIM(ESTSERIPS) IN ('1','2','3','5','8','9'))
                +
                (SELECT COUNT(*) FROM dbo.AMBORDIMA with(nolock) 
                 WHERE IPCODPACI = J.IPCODPACI 
                   AND NUMINGRES = J.NUMINGRES 
                   AND RTRIM(ESTSERIPS) IN ('1','2','3','5','8','9'))
            ) as ImagesCount,
            (
                (SELECT COUNT(*) FROM dbo.HCORDLABO with(nolock) 
                 WHERE IPCODPACI = J.IPCODPACI 
                   AND NUMINGRES = J.NUMINGRES 
                   AND RTRIM(ESTSERIPS) IN ('1','2'))
                +
                (SELECT COUNT(*) FROM dbo.AMBORDLAB with(nolock) 
                 WHERE IPCODPACI = J.IPCODPACI 
                   AND NUMINGRES = J.NUMINGRES 
                   AND RTRIM(ESTSERIPS) IN ('1','2'))
            ) as LabsCount,
            (SELECT COUNT(*) FROM dbo.HCORDINTE with(nolock) 
             WHERE IPCODPACI = J.IPCODPACI 
               AND NUMINGRES = J.NUMINGRES 
               AND RTRIM(ESTSERIPS) IN ('1','2','6')) as ConsultationsCount
        FROM dbo.CHCAMASHO A with (nolock)
        INNER JOIN dbo.ADcenaten D with (nolock) ON A.CODCENATE=D.CODCENATE 
        INNER JOIN dbo.INUNIFUNC E with (nolock) ON A.UFUCODIGO=E.UFUCODIGO 
        LEFT OUTER JOIN dbo.CHREGESTA C with (nolock) ON A.CODICAMAS=C.CODICAMAS AND C.REGESTADO = 1 
        LEFT OUTER JOIN dbo.CHTIPESTA G with (nolock) ON G.CODTIPEST=C.CODTIPEST 
        INNER JOIN dbo.INPacient H with (nolock) ON C.IPCODPACI=H.IPCODPACI 
        LEFT OUTER JOIN dbo.HCREGEGRE I with (nolock) ON C.NUMINGRES=I.NUMINGRES
        INNER JOIN dbo.ADINGRESO J with (nolock) ON C.NUMINGRES=J.NUMINGRES
        Outer apply (select TOP 1 INDICAPAC,IPCODPACI, NUMINGRES from HCHISPACA where IPCODPACI = J.IPCODPACI AND NUMINGRES = J.NUMINGRES order by FECHISPAC desc) as X 
        LEFT OUTER JOIN dbo.INESPECIA K with (nolock) ON C.CODESPECI = K.CODESPECI
        LEFT OUTER JOIN dbo.ADACTIVID L with (nolock) ON H.CODACTIVI = L.codactivi
        LEFT OUTER JOIN dbo.ADTRIAGEU M with (nolock) ON M.NUMINGRES = J.NUMINGRES
        LEFT OUTER JOIN dbo.ADCONTURG N with (nolock) ON N.CODCONCEC = M.CODCONCEC
        INNER JOIN	dbo.INENTIDAD P with (nolock) ON P.CODENTIDA = J.CODENTIDA
        LEFT OUTER JOIN dbo.INDIAGNOS Q with (nolock) ON Q.CODDIAGNO = (SELECT TOP 1 CODDIAGNO FROM INDIAGNOP WHERE IPCODPACI = H.IPCODPACI AND NUMINGRES = J.NUMINGRES AND CODDIAPRI = 1)  
        LEFT OUTER JOIN CHTIPOSAISLAMIENTOS Z with (nolock) ON A.CODAISLAM = Z.Id
        LEFT OUTER JOIN dbo.INPROFSAL Prof with (nolock) ON C.CODPROSAL = Prof.CODPROSAL
        WHERE 
            A.CODCENATE= @CentroAtencion
            AND (@AdmissionNumber IS NULL OR C.NUMINGRES = @AdmissionNumber)
            AND (@UnidadFuncional IS NULL OR A.UFUCODIGO = @UnidadFuncional)
            AND ESTADCAMA IN ('2','8')
        ORDER BY A.CODICAMAS

end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de gestión hospitalaria que obtiene el listado completo de pacientes actualmente hospitalizados en una unidad funcional y centro de atención específicos, o para un número de ingreso puntual. Integra información de camas (CHCAMASHO), estados de estancia (CHREGESTA), datos del paciente (INPACIENT), ingresos (ADINGRESO), egresos (HCREGEGRE) y notas clínicas (HCHISPACA) para construir una vista operativa de cada cama ocupada. Por cada paciente devuelve su estado de egreso (en unidad, presalida o salida), datos de identificación y contacto, tipo de estancia, clase de cama, aislamiento y, de forma destacada, el resultado y tipo de las escalas clínicas de riesgo de caída (Down-Ton, Morse), nivel de dolor (VAS y otras), sedación (RASS), úlceras por presión (Norton) y estado hemodinámico (APACHE), indicando si cada escala fue diligenciada durante el ingreso. Es el procedimiento central del módulo EMR de gestión hospitalaria para el seguimiento en tiempo real de pacientes hospitalizados por parte del equipo asistencial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_GestionHospitalariaPacientesHospitalizadosConResultados_EMR';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_GestionHospitalariaPacientesHospitalizadosConResultados_EMR';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes hospitalizados de un centro de atención (opcionalmente filtrados por unidad funcional o ingreso) con su ubicación, datos clínicos, escalas aplicadas, alertas, acompañantes, hemocomponentes y conteos de resultados/órdenes pendientes.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizadosConResultados_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@CentroAtencion debe corresponder a un centro de atención válido en CHCAMASHO/ADcenaten; Si se envía @UnidadFuncional debe coincidir con CHCAMASHO.UFUCODIGO; si se envía @AdmissionNumber debe coincidir con CHREGESTA.NUMINGRES; Las camas deben tener ESTADCAMA IN (''2'',''8'') para ser incluidas; El paciente debe tener un registro de estancia activo (CHREGESTA.REGESTADO = 1) asociado a la cama', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizadosConResultados_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven camas con ESTADCAMA IN (''2'',''8'') (camas ocupadas o en estado equivalente al filtro); Solo considera el registro de estancia activa: CHREGESTA.REGESTADO = 1; El diagnóstico mostrado es el principal (INDIAGNOP.CODDIAPRI = 1) más reciente del ingreso; El indicador de presalida proviene del registro más reciente de HCHISPACA (ORDER BY FECHISPAC DESC); Los resultados de escalas (caída/dolor) corresponden al registro más reciente combinando HCESCALAS, HCESCDOWN y HCESCVASC/HCESCVASD; ImagesCount cuenta órdenes de imágenes con ESTSERIPS IN (''1'',''2'',''3'',''5'',''8'',''9'') tanto hospitalarias como ambulatorias; LabsCount cuenta órdenes de laboratorio con ESTSERIPS IN (''1'',''2'') hospitalarias y ambulatorias; ConsultationsCount cuenta órdenes internas con ESTSERIPS IN (''1'',''2'',''6''); POBESPECIAL cuenta solo poblaciones especiales con TIPOPOESPERIES = 1 (riesgo); BOLSAS cuenta hemocomponentes con CONFRECBOL = 1 y ESTADO en bolsas activas; ''ESTADO'' cuenta bolsas con ESTADO = 6 y CONFRECBOL = 1; El procedimiento es de solo lectura (SET NOCOUNT ON, sin DML); Los parámetros @AdmissionNumber y @UnidadFuncional son opcionales: si son NULL no filtran', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizadosConResultados_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente hospitalizado; egreso/salida hospitalaria; cama hospitalaria; clase de habitación; clase de cama; unidad funcional; aislamiento; tipo de estancia; ingreso/admisión; especialidad médica; diagnóstico principal; entidad/aseguradora; médico tratante; escalas clínicas (Downton, Rass, Norton, VAS, Apache); escala de riesgo de caída; escala de dolor; población especial / riesgo; acompañantes; hemocomponentes/bolsas de sangre; triage de urgencias; riesgo de agresión; zona apartada; vive solo; recomendaciones de interconsulta; órdenes de imágenes diagnósticas; órdenes de laboratorio; órdenes de interconsulta; traslado a cirugía; traslado de medicamentos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizadosConResultados_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Última marca de historia del paciente (HCHISPACA.INDICAPAC) = ''22'' → Egreso se clasifica como ''presalida'' else Si no existe registro de egreso (HCREGEGRE.NUMINGRES IS NULL) se marca ''en_unidad''; en caso contrario ''salida''; si Tipo de documento del paciente IN (6,7) → Marca ASMS = 1 (paciente afiliado a régimen especial/ASMS) else ASMS = 0; si Existe al menos un registro en RecommendPatient con Status=1 para el paciente e ingreso → Recomendacion = 1 (tiene recomendaciones activas) else Recomendacion = 0; si Existen escalas registradas en HCESCALAS con TIPOESCALA IN (47,92,113) o registros en HCESCDOWN para el ingreso → Marca ESCALACAIDA = 1 indicando que se aplicó escala de riesgo de caída else ESCALACAIDA = 0; si Existen escalas en HCESCALAS con TIPOESCALA IN (49,90,91,93) o registros en HCESCVASC/HCESCVASD para el ingreso → Marca ESCALADOLOR = 1 indicando aplicación de escala de dolor else ESCALADOLOR = 0; si Existen escalas en HCESCALAS distintas de las de caída y dolor (NOT IN 47,92,49,90,91,93) → ESCALASGENERAL = 1 else ESCALASGENERAL = 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizadosConResultados_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHCAMASHO; dbo.ADcenaten; dbo.INUNIFUNC; dbo.CHREGESTA; dbo.CHTIPESTA; dbo.INPacient; dbo.HCREGEGRE; dbo.ADINGRESO; dbo.HCHISPACA; dbo.INESPECIA; dbo.ADACTIVID; dbo.ADTRIAGEU; dbo.ADCONTURG; dbo.INENTIDAD; dbo.INDIAGNOS; dbo.INDIAGNOP; dbo.CHTIPOSAISLAMIENTOS; dbo.INPROFSAL; dbo.HCESCALAS; dbo.HCESCDOWN; dbo.HCESCVASC; dbo.HCESCVASD; dbo.ADPOBESPEPAC; dbo.ADPOBESPE; dbo.ADACOMPAN; dbo.HCORHEMBOL; dbo.HCORHEMCO; dbo.RecommendPatient; dbo.HCORDIMAG; dbo.AMBORDIMA (+3 adicionales)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizadosConResultados_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizadosConResultados_EMR';
-- GO
