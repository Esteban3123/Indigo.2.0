CREATE PROCEDURE [dbo].[SPREP_HC_Generales_CabeceraReportes_EMR]
(
    @Paciente varchar(25),
    @NumeroIngreso VARCHAR(MAX)
)
WITH RECOMPILE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        A.IPCODPACI AS CodigoPaciente,
        A.IPNOMCOMP AS Nombres,
        A.IPPRINOMB AS PrimerNombre,
        A.IPSEGNOMB AS SegundoNombre,
        A.IPPRIAPEL AS PrimerApellido,
        A.IPSEGAPEL AS SegundoApellido,
        A.IPFECNACI AS FechaNacimiento,
        RTRIM(K.NUMEFOLIO) AS Folio,
        EV.Name AS NombreEntidad,
        A.IPSEXOPAC AS Sexo,
        Q.CODDIAGNO + ' - ' + Q.NOMDIAGNO AS Diagnostico,
        RTRIM(L.DESESPECI) AS Especialidad,
        RTRIM(D.DESACTIVI) AS Profesion,
        S.DESCRIPCION AS GrupoPoblacional,
        O.CREDDESCRI AS Religion,
        RTRIM(A.IPGRUPSAN) AS GrupoSanguineo,
        CASE A.IPRHSANGR WHEN '+' THEN 'Positivo' WHEN '-' THEN 'Negativo' ELSE '!!' END AS RHSangre,
        RTRIM(A.IPTELEFON) + ' - ' + RTRIM(A.IPTELMOVI) AS Telefono,
        IIF(H.Address IS NULL,
            (RTRIM(A.IPDIRECCI) + ' - ' + RTRIM(E.UBINOMBRE) + ' - ' + RTRIM(F.MUNNOMBRE) 
            + ' - ' + RTRIM(Z.NOMDEPART)), dbo.Patient_Address(A.IPCODPACI)
        ) AS Direccion,
        RTRIM(J.UFUDESCRI) AS UnidadFuncional,
        RTRIM(cama.DESCCAMAS) AS Cama,
        T.TALLAPACI AS Talla,
        (T.PESOPACIE / 1000.0) AS Peso,
        CASE
            WHEN T.TALLAPACI IS NULL OR T.TALLAPACI = 0 THEN NULL
            WHEN T.PESOPACIE IS NULL OR T.PESOPACIE = 0 THEN NULL
            ELSE (T.PESOPACIE / 1000.0) / POWER(T.TALLAPACI / 100.0, 2)
        END AS IMC,
        dbo.PuntajeEscalaRass(B.NUMINGRES, B.IPCODPACI) AS PUNTAJERASS,
        dbo.PuntajeEscalaNorton(B.NUMINGRES, B.IPCODPACI) AS PUNTAJENORTON,
        dbo.PuntajeEscalaVas(B.NUMINGRES, B.IPCODPACI) AS PUNTAJEVAS,
        dbo.PuntajeEscalaApache(B.NUMINGRES, B.IPCODPACI) AS PUNTAJEAPACHE
    FROM dbo.INPACIENT A WITH (NOLOCK)
    INNER JOIN dbo.ADINGRESO B WITH (NOLOCK) ON A.IPCODPACI = B.IPCODPACI
    -- INNER JOIN dbo.HCHISPACA K WITH (NOLOCK) ON B.NUMINGRES = K.NUMINGRES
    OUTER APPLY (SELECT TOP 1 INDICAPAC,IPCODPACI, NUMINGRES, NUMEFOLIO FROM HCHISPACA WHERE IPCODPACI = B.IPCODPACI AND NUMINGRES = B.NUMINGRES order by FECHISPAC DESC) AS K 
    LEFT JOIN Contract.healthadministrator EV WITH (NOLOCK) ON EV.Id = B.GENCONENTITY
    LEFT JOIN dbo.CHCAMASHO cama WITH (NOLOCK) ON B.CODCAMACT = cama.CODICAMAS
    LEFT JOIN dbo.INUNIFUNC J WITH (NOLOCK) ON cama.UFUCODIGO = J.UFUCODIGO
    LEFT JOIN Admissions.PatientAddress H WITH (NOLOCK) ON A.IPCODPACI = H.IPCODPACI AND IsMain = 1
    LEFT JOIN dbo.INUBICACI E WITH (NOLOCK) ON A.AUUBICACI = E.AUUBICACI
    LEFT JOIN dbo.INMUNICIP F WITH (NOLOCK) ON E.DEPMUNCOD = F.DEPMUNCOD
    LEFT JOIN dbo.INDEPARTA Z WITH (NOLOCK) ON Z.DEPCODIGO = F.DEPCODIGO
    LEFT JOIN dbo.ADACTIVID D WITH (NOLOCK) ON A.CODACTIVI = D.CODACTIVI
    LEFT JOIN dbo.ADPOBESPEPAC R WITH (NOLOCK) ON A.IPCODPACI = R.IPCODPACI
    LEFT JOIN dbo.ADPOBESPE S WITH (NOLOCK) ON R.IDADPOBESPE = S.ID
    LEFT JOIN dbo.ADCREDO O WITH (NOLOCK) ON A.CREDCODIGO = O.CREDCODIGO
    LEFT JOIN dbo.INDIAGNOS Q WITH (NOLOCK) 
        ON Q.CODDIAGNO = (
            SELECT TOP 1 CODDIAGNO 
            FROM INDIAGNOP 
            WHERE IPCODPACI = A.IPCODPACI 
              AND NUMINGRES = B.NUMINGRES 
              AND CODDIAPRI = 1
        )
    LEFT JOIN dbo.CHREGESTA M WITH (NOLOCK) 
        ON cama.CODICAMAS = M.CODICAMAS AND M.REGESTADO = 1
    LEFT JOIN dbo.INESPECIA L WITH (NOLOCK) ON M.CODESPECI = L.CODESPECI
    OUTER APPLY (
        SELECT TOP 1 PESOPACIE, TALLAPACI 
        FROM HCEXFISIC HE 
        WHERE HE.IPCODPACI = A.IPCODPACI 
          AND PESOPACIE IS NOT NULL 
          AND LEN(PESOPACIE) > 0
        ORDER BY FECREGITE DESC
    ) T
    WHERE B.NUMINGRES IN (SELECT Value FROM dbo.splitstring(@NumeroIngreso))
    AND B.IPCODPACI = @Paciente
    ;
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera la cabecera o encabezado de los reportes de Historia Clínica Electrónica (EMR) para uno o varios ingresos hospitalarios. Consolida en una sola consulta los datos demográficos del paciente (nombre, fecha de nacimiento, grupo sanguíneo, RH, teléfono, dirección), su episodio de ingreso (número de ingreso, unidad funcional, cama asignada), la entidad pagadora o aseguradora (EPS/ARS), el diagnóstico principal CIE-10, la especialidad médica, la profesión del responsable, el grupo poblacional especial, la religión o credo, y medidas antropométricas como talla, peso e IMC. Adicionalmente calcula y expone los puntajes de escalas clínicas clave: RASS (sedación-agitación), Norton (riesgo de úlceras por presión), VAS (dolor) y APACHE (gravedad). Se usa como fuente estándar de encabezado en los documentos e informes clínicos impresos o exportados del módulo de historia clínica, recibiendo como parámetro uno o varios números de ingreso separados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_CabeceraReportes_EMR';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_CabeceraReportes_EMR';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene los datos de cabecera consolidados de un paciente y su(s) ingreso(s) para encabezar reportes clínicos en la historia electrónica (datos demográficos, ubicación, diagnóstico principal, signos antropométricos y puntajes de escalas).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CabeceraReportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir en INPACIENT y tener al menos un ingreso en ADINGRESO con el o los NUMINGRES indicados.; El parámetro de números de ingreso debe ser parseable por dbo.splitstring (lista delimitada).; Para que se muestre especialidad, la cama actual del ingreso debe tener un registro activo en CHREGESTA con REGESTADO = 1.; Para que se muestre diagnóstico, debe existir en INDIAGNOP un registro con CODDIAPRI = 1 (diagnóstico principal) para el paciente e ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CabeceraReportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera el folio de historia clínica más reciente del ingreso (ORDER BY FECHISPAC DESC, TOP 1).; Solo toma el último registro de examen físico con peso no nulo para calcular peso, talla e IMC (ORDER BY FECREGITE DESC).; El peso se almacena en gramos y se convierte a kilogramos dividiendo entre 1000.; La talla se asume en centímetros y se convierte a metros dividiendo entre 100 para el cálculo del IMC.; Solo se considera diagnóstico principal aquel con CODDIAPRI = 1.; La especialidad reportada corresponde al estado de cama activo (REGESTADO = 1).; La dirección principal del paciente es la marcada con IsMain = 1 en PatientAddress.; Todas las consultas usan WITH (NOLOCK), aceptando lecturas sucias para fines de reporte.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CabeceraReportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/admisión; Historia clínica (folio); Entidad administradora de salud; Diagnóstico principal; Especialidad médica; Unidad funcional; Cama hospitalaria; Grupo poblacional; Credo/religión; Grupo sanguíneo y RH; Dirección de residencia; Examen físico (peso/talla); IMC (Índice de Masa Corporal); Escalas clínicas: RASS, Norton, VAS, APACHE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CabeceraReportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Retorna una sola fila (TOP 1) con la cabecera del paciente filtrando por IPCODPACI = @Paciente y NUMINGRES dentro de la lista @NumeroIngreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CabeceraReportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.IPRHSANGR = ''+'' → Reporta RH como ''Positivo'' else Si ''-'' reporta ''Negativo''; cualquier otro valor se reporta como ''!!''; si Admissions.PatientAddress.Address IS NULL para el paciente con IsMain=1 → Construye la dirección concatenando IPDIRECCI + ubicación + municipio + departamento else Usa la función dbo.Patient_Address(IPCODPACI) como dirección; si TALLAPACI es NULL/0 o PESOPACIE es NULL/0 → IMC se reporta como NULL else Calcula IMC = (PESOPACIE/1000) / (TALLAPACI/100)^2', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CabeceraReportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring; dbo.Patient_Address; dbo.PuntajeEscalaRass; dbo.PuntajeEscalaNorton; dbo.PuntajeEscalaVas; dbo.PuntajeEscalaApache', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CabeceraReportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPACIENT; dbo.ADINGRESO; dbo.HCHISPACA; Contract.healthadministrator; dbo.CHCAMASHO; dbo.INUNIFUNC; Admissions.PatientAddress; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA; dbo.ADACTIVID; dbo.ADPOBESPEPAC; dbo.ADPOBESPE; dbo.ADCREDO; dbo.INDIAGNOS; dbo.INDIAGNOP; dbo.CHREGESTA; dbo.INESPECIA; dbo.HCEXFISIC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CabeceraReportes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CabeceraReportes_EMR';
-- GO
