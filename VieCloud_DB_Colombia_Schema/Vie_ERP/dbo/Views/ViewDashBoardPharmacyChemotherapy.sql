CREATE VIEW [dbo].[ViewDashBoardPharmacyChemotherapy]
AS
WITH DetailFlags AS
(
    SELECT
        D.CODCONCEC,
        MAX(CASE WHEN P.TIPPRODUC = 1 THEN 1 ELSE 0 END) AS MEDICAMENTO,
        MAX(CASE WHEN P.TIPPRODUC = 2 THEN 1 ELSE 0 END) AS INSUMOS,
        MAX(CASE WHEN P.TIPPRODUC = 3 THEN 1 ELSE 0 END) AS MEDICAMENTO_INSUMO
    FROM dbo.HCFARMEPD AS D 
    INNER JOIN dbo.IHLISTPRO AS P  ON P.CODPRODUC = D.CODPRODUC
    WHERE D.CANPENPRO > 0
    GROUP BY D.CODCONCEC
)
SELECT
    A.CODCONCEC AS [Row],
    0 AS SelectOption,
    A.CODCENATE AS CodigoCentroAtencion,
    A.CODCONCEC AS ConsecutivoFarmacia,
    A.FECHAORDE AS FechaOrden,
    A.IPCODPACI AS CodigoPaciente,
    RTRIM(PAC.IPNOMCOMP) AS NombrePaciente,
    RTRIM(UF.UFUDESCRI) AS UnidadFuncional,
    A.ORDESTADO AS Estado,
    RTRIM(PROF.NOMMEDICO) AS NombreMedico,
    dbo.GetProfessionType(PROF.TIPPROFES) AS Profesion,
    CONCAT(RTRIM(BOD.CODBODEGA), ' - ', RTRIM(BOD.DESBODEGA)) AS Bodega,
    A.NUMINGRES AS Ingreso,
    A.UFUCODIGO,
    COALESCE(NULLIF(A.CODCONCEP, ''), '') AS ConsecutivoPrescripcion,
    COALESCE(NULLIF(A.CODCONCES, ''), '') AS ConsecutivoInsumos,
    RTRIM(A.CODCENCOS) AS CentroCostos,
    A.ORDTRANUE AS Tipo,
    A.NUMEFOLIO AS Folio,
    A.IDETIPHIS,
    CAST(0 AS bit) AS Impresion,
    CASE
        WHEN A.MEDICAMENTOVALIDADO IS NULL OR A.MEDICAMENTOVALIDADO = 0
            THEN CAST(0 AS bit)
        ELSE CAST(1 AS bit)
    END AS Validado,
    ISNULL(A.TIPOSOLICITUD, 0) AS TIPOSOLICITUD,
    ISNULL(CG.ExtramuralPharmaceuticalDispensing, 0) AS TIPOSOLICITUD1,
    CG.ExtramuralPharmaceuticalDispensing AS PERMITEEXTRA,
    RTRIM(LTRIM(A.CODCENATE)) + ' - ' + RTRIM(LTRIM(CENT.NOMCENATE)) AS CareCenterDescription,
    'Ciclo ' + CAST(CICLO.CICLO AS varchar(2)) + ', día ' + CAST(CICLO.DIA AS varchar(2)) AS DIA,
    CONVERT(varchar(10), CITA.FECHORAIN, 103) + ' ' + CONVERT(varchar(8), CITA.FECHORAIN, 114) AS FechaCita,
    CASE CITA.CODESTCIT
        WHEN 0 THEN 'Asignada'
        WHEN 1 THEN 'Cumplida'
        WHEN 2 THEN 'Incumplida'
        WHEN 3 THEN 'Pre Asignada'
        WHEN 4 THEN 'Cita Cancelada'
        ELSE ''
    END AS EstadoCita,
    A.IDCITA,
    A.IDHCORDPRON,
    ING.IAUTORIZA AS AuthorizationNumber,
    PAC.IPFECNACI AS BirthDay,
    ISNULL(DF.MEDICAMENTO, 0) AS MEDICAMENTO,
    ISNULL(DF.INSUMOS, 0) AS INSUMOS,
    ISNULL(DF.MEDICAMENTO_INSUMO, 0) AS MEDICAMENTO_INSUMO
FROM dbo.HCFARMEPC AS A 
INNER JOIN dbo.IHBODEGAS AS BOD  ON A.CODBODEGA = BOD.CODBODEGA
INNER JOIN dbo.INPACIENT AS PAC  ON A.IPCODPACI = PAC.IPCODPACI
INNER JOIN dbo.INPROFSAL AS PROF  ON A.CODPROSAL = PROF.CODPROSAL
INNER JOIN dbo.INUNIFUNC AS UF  ON A.UFUCODIGO = UF.UFUCODIGO
INNER JOIN dbo.ADINGRESO AS ING  ON A.NUMINGRES = ING.NUMINGRES
INNER JOIN dbo.ADCENATEN AS CENT  ON CENT.CODCENATE = A.CODCENATE
LEFT JOIN Contract.CareGroup AS CG  ON ING.GENCAREGROUP = CG.Id
LEFT JOIN dbo.AGASICITA AS CITA  ON CITA.CODAUTONU = A.IDCITA
LEFT JOIN EHR.HCORDCICLOSD AS CICLO  ON CICLO.ID = CITA.IDHCORDCICLOSD
LEFT JOIN DetailFlags AS DF ON DF.CODCONCEC = A.CODCONCEC
WHERE A.ORDESTADO = '1' AND A.ORDENQUIMIO = 1 AND ING.IESTADOIN IN (' ', 'P');
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Panel de control (dashboard) de órdenes de farmacia para quimioterapia activas y pendientes de despacho. Consolida, para cada orden de quimioterapia en estado activo (ORDESTADO=1, ORDENQUIMIO=1) cuyo ingreso esté vigente, los datos del paciente (cédula, nombre, fecha de nacimiento), el médico tratante y su profesión, la unidad funcional, la bodega asignada, el centro de atención, el número de ingreso y la autorización. Complementa cada orden con información del ciclo y día del protocolo de quimioterapia, la fecha y estado de la cita agendada, el consecutivo de prescripción e insumos, y si la orden ya fue validada por farmacia. Indica además, a través de banderas (flags), si la orden contiene medicamentos, insumos o una combinación de ambos con cantidad pendiente por despachar, y si el grupo de atención del contrato permite dispensación extramural (farmacia externa). Sirve como fuente principal para la pantalla de gestión y despacho farmacéutico de quimioterapia en el módulo de farmacia del EHR.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewDashBoardPharmacyChemotherapy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewDashBoardPharmacyChemotherapy';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las órdenes de quimioterapia activas de farmacia con datos de paciente, médico, cita, ciclo/día y clasificación de productos pendientes (medicamento, insumo o ambos) para alimentar un tablero de control farmacéutico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyChemotherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La orden debe existir en HCFARMEPC con estado activo (ORDESTADO=''1'') y marcada como orden de quimioterapia (ORDENQUIMIO=1).; El ingreso asociado debe estar en estado vacío ('' '') o pendiente (''P'') en ADINGRESO.IESTADOIN.; Deben existir registros relacionados de bodega, paciente, profesional, unidad funcional, ingreso y centro de atención para que la orden aparezca.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyChemotherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran productos cuya cantidad pendiente por procesar sea mayor a cero (CANPENPRO>0) para clasificar banderas de medicamento/insumo.; La clasificación de tipo de producto se hace por TIPPRODUC (1=Medicamento, 2=Insumo, 3=Medicamento+Insumo).; Las órdenes con ingreso en estados distintos a vacío o ''P'' nunca aparecen en el dashboard.; Las órdenes que no son de quimioterapia (ORDENQUIMIO≠1) o que no están activas nunca se muestran.; Los códigos de prescripción e insumos vacíos se normalizan a cadena vacía mediante COALESCE/NULLIF.; El TIPOSOLICITUD se entrega siempre con valor (0 si es nulo).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyChemotherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de quimioterapia; Dispensación farmacéutica; Ciclo y día de tratamiento oncológico; Cita médica y su estado; Ingreso/admisión del paciente; Validación de medicamento; Dispensación farmacéutica extramural; Autorización de ingreso; Centro de atención; Unidad funcional; Bodega de farmacia; Clasificación de productos: medicamento, insumo, medicamento+insumo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyChemotherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFARMEPC: Devuelve solo órdenes de quimioterapia activas: WHERE ORDESTADO=''1'' AND ORDENQUIMIO=1 y con ingreso en estado '' '' o ''P''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyChemotherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.MEDICAMENTOVALIDADO IS NULL o = 0 → Marca Validado = 0 (no validado) else Marca Validado = 1 (validado); si G.CODESTCIT (estado de la cita) → Traduce a etiqueta: 0=Asignada, 1=Cumplida, 2=Incumplida, 3=Pre Asignada, 4=Cita Cancelada; otro valor => cadena vacía; si El consecutivo de la orden tiene productos pendientes (HCFARMEPD.CANPENPRO>0) clasificados en IHLISTPRO con TIPPRODUC=1 → Marca la bandera MEDICAMENTO=1 else MEDICAMENTO=0; si Existen productos pendientes con TIPPRODUC=2 → Marca la bandera INSUMOS=1 else INSUMOS=0; si Existen productos pendientes con TIPPRODUC=3 → Marca la bandera MEDICAMENTO_INSUMO=1 else MEDICAMENTO_INSUMO=0; si ING.GENCAREGROUP coincide con Contract.CareGroup.Id → Toma ExtramuralPharmaceuticalDispensing como TIPOSOLICITUD1 y PERMITEEXTRA; si no hay match, TIPOSOLICITUD1=0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyChemotherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFARMEPC; dbo.HCFARMEPD; dbo.IHLISTPRO; dbo.IHBODEGAS; dbo.INPACIENT; dbo.INPROFSAL; dbo.INUNIFUNC; dbo.ADINGRESO; dbo.ADCENATEN; Contract.CareGroup; dbo.AGASICITA; EHR.HCORDCICLOSD; dbo.GetProfessionType', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyChemotherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyChemotherapy';
GO
