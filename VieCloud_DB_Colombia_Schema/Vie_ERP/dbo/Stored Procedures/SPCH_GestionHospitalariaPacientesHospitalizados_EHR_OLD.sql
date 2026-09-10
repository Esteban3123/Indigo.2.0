CREATE PROCEDURE [dbo].[SPCH_GestionHospitalariaPacientesHospitalizados_EHR_OLD]
(
    @CentroAtencion Char(10),
    @UnidadFuncional Char(10)
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
        , CAST('' AS CHAR(50)) AS Edad
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
        ) as Recomendacion
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
            AND (@UnidadFuncional IS NULL OR A.UFUCODIGO = @UnidadFuncional)
            AND ESTADCAMA IN ('2','8')
        ORDER BY A.CODICAMAS

end
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento que retorna el censo de pacientes hospitalizados activos (camas en estados ''2'' y ''8'') filtrado por centro de atención y unidad funcional. Para cada paciente consolida datos de cama, identificación, entidad aseguradora, diagnóstico principal, médico tratante, escalas clínicas de riesgo (caídas Down-Ton, dolor VAS, sedación RASS, úlceras Norton, Apache), hemocomponentes, acompañantes y estado de egreso (presalida/en_unidad/salida). Es la fuente principal del tablero de gestión hospitalaria del EHR.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizados_EHR_OLD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizados_EHR_OLD';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes hospitalizados de un centro de atención (y opcionalmente una unidad funcional) con datos de cama, ingreso, escalas clínicas, riesgos y estado de egreso para gestión hospitalaria.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizados_EHR_OLD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención debe existir en CHCAMASHO/ADCENATEN.; Las camas consideradas deben tener ESTADCAMA en (''2'',''8'') (ocupadas/asignadas).; El paciente debe tener un registro activo en CHREGESTA (REGESTADO = 1) ligado a la cama.; Debe existir un ingreso (ADINGRESO) asociado al registro de estancia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizados_EHR_OLD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran camas con estado ''2'' u ''8''.; Solo se incluye el registro de estancia activo (REGESTADO = 1).; El diagnóstico mostrado es el principal del ingreso (CODDIAPRI = 1).; Las escalas de caída se identifican por TIPOESCALA en (47,92,113); las de dolor por (49,90,91,93).; El indicador de pre-salida se determina por el último registro de HCHISPACA (FECHISPAC máxima) con INDICAPAC=''22''.; Pacientes con tipo de documento 6 o 7 se marcan como ASMS.; Los conteos de bolsas de hemocomponentes solo cuentan registros con CONFRECBOL = 1, y el estado cuenta adicionalmente ESTADO = 6.; Población especial considera solo TIPOPOESPERIES = 1.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizados_EHR_OLD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente hospitalizado; Cama hospitalaria; Unidad funcional; Centro de atención; Ingreso/admisión; Egreso (presalida, en unidad, salida); Clase de habitación y cama; Aislamiento; Especialidad médica; Médico tratante; Diagnóstico principal; Entidad responsable de pago; Escalas clínicas (Downton, Rass, Norton, VAS, Apache); Riesgo de caída; Escala de dolor; Triage de urgencias; Población especial; Acompañantes; Hemocomponentes/bolsas de sangre; Recomendación/interconsulta; Riesgo de agresión; Tipo de documento ASMS; Vive solo; Zona apartada; Traslado a cirugía / medicamentos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizados_EHR_OLD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve una fila por cama ocupada/asignada (ESTADCAMA IN (''2'',''8'')) del centro indicado, filtrando opcionalmente por unidad funcional cuando @UnidadFuncional no es NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizados_EHR_OLD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Último HCHISPACA.INDICAPAC del paciente = ''22'' → Egreso = ''presalida'' else Si HCREGEGRE.NUMINGRES IS NULL → ''en_unidad''; en otro caso → ''salida''; si IPTIPODOC IN (6,7) → ASMS = 1 (paciente marcado como ASMS) else ASMS = 0; si Existe HCESCALAS con TIPOESCALA IN (47,92,113) para el ingreso → ESCALACAIDA = 1 else Si existe HCESCDOWN para el ingreso → 1; en otro caso → 0; si Existe HCESCALAS con TIPOESCALA IN (49,90,91,93) para el ingreso → ESCALADOLOR = 1 else Si existe HCESCVASC/HCESCVASD para el ingreso → 1; en otro caso → 0; si Existe HCESCALAS con TIPOESCALA NOT IN (47,92,49,90,91,93) → ESCALASGENERAL = 1 else 0; si Existe RecommendPatient con Status = 1 para el paciente y el ingreso → Recomendacion = 1 else Recomendacion = 0; si @UnidadFuncional IS NULL → No se filtra por unidad funcional else Solo camas con A.UFUCODIGO = @UnidadFuncional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizados_EHR_OLD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.ClaseHabitacion; dbo.ClaseCama; dbo.TipoAislamiento; dbo.PuntajeEscalaDownTon; dbo.PuntajeEscalaRass; dbo.PuntajeEscalaNorton; dbo.PuntajeEscalaVas; dbo.PuntajeEscalaApache', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizados_EHR_OLD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHCAMASHO; dbo.ADcenaten; dbo.INUNIFUNC; dbo.CHREGESTA; dbo.CHTIPESTA; dbo.INPacient; dbo.HCREGEGRE; dbo.ADINGRESO; dbo.HCHISPACA; dbo.INESPECIA; dbo.ADACTIVID; dbo.ADTRIAGEU; dbo.ADCONTURG; dbo.INENTIDAD; dbo.INDIAGNOS; dbo.INDIAGNOP; dbo.CHTIPOSAISLAMIENTOS; dbo.INPROFSAL; dbo.HCESCALAS; dbo.HCESCDOWN; dbo.HCESCVASC; dbo.HCESCVASD; dbo.ADPOBESPEPAC; dbo.ADPOBESPE; dbo.ADACOMPAN; dbo.HCORHEMBOL; dbo.HCORHEMCO; dbo.RecommendPatient', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizados_EHR_OLD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizados_EHR_OLD';
-- GO
