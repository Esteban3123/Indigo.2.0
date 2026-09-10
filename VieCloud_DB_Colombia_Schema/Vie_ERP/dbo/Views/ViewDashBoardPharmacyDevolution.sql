CREATE VIEW [dbo].[ViewDashBoardPharmacyDevolution]
AS
WITH DetailFlags AS
(
    SELECT
        D.CODCONCEC,
        MAX(CASE WHEN P.TIPPRODUC = 1 THEN 1 ELSE 0 END) AS MEDICAMENTO,
        MAX(CASE WHEN P.TIPPRODUC = 2 THEN 1 ELSE 0 END) AS INSUMOS,
        MAX(CASE WHEN P.TIPPRODUC = 3 THEN 1 ELSE 0 END) AS MEDICAMENTO_INSUMO
    FROM dbo.HCDEVMEDD AS D 
    INNER JOIN dbo.IHLISTPRO AS P  ON P.CODPRODUC = D.CODPRODUC
    WHERE D.CANPENDIE > 0
    GROUP BY D.CODCONCEC
)
SELECT
    A.CODCONCEC AS [Row],
    0 AS SelectOption,
    A.CODCENATE AS CodigoCentroAtencion,
    A.CODCONCEC AS ConsecutivoDevoluciones,
    A.FECHDEVOL AS FechaDevolucion,
    A.CODPROSAL AS CodigoProfesional,
    A.IPCODPACI AS CodigoPaciente,
    A.NUMINGRES AS Ingreso,
    A.CODCENATE AS CentroAtencion,
    A.UFUCODIGO,
    A.DEVESTADO AS Estado,
    RTRIM(A.CODCENCOS) AS CentroCostos,
    A.ORIDEVMED,
    A.FECREGISTR,
    A.CODUSUARI,
    RTRIM(PAC.IPNOMCOMP) AS NombrePaciente,
    RTRIM(UF.UFUDESCRI) AS UnidadFuncional,
    RTRIM(PROF.NOMMEDICO) AS NombreMedico,
    CONCAT(RTRIM(BOD.CODBODEGA), ' - ', RTRIM(BOD.DESBODEGA)) AS Bodega,
    CASE A.ORIDEVMED
        WHEN 1 THEN 'Traslado de Cama Hospitalizacion'
        WHEN 2 THEN 'Egreso de Cama Hospitalizacion'
        WHEN 5 THEN 'Hoja de Gasto QX'
        ELSE 'Enfermeria (Devolutivo Parcial)'
    END AS OrigenDevolutivo,
    CAST(0 AS bit) AS ImpresionDev,
    ISNULL(CAMA.NUMCAMHOS, '') AS CodigoCama,
    RTRIM(LTRIM(A.CODCENATE)) + ' - ' + RTRIM(LTRIM(CENT.NOMCENATE)) AS CareCenterDescription,
    dbo.GetProfessionType(PROF.TIPPROFES) AS Profesion,
    PAC.IPFECNACI AS BirthDay,
    ISNULL(DF.MEDICAMENTO, 0) AS MEDICAMENTO,
    ISNULL(DF.INSUMOS, 0) AS INSUMOS,
    ISNULL(DF.MEDICAMENTO_INSUMO, 0) AS MEDICAMENTO_INSUMO
FROM dbo.HCDEVMEDC AS A 
INNER JOIN dbo.IHBODEGAS AS BOD  ON A.CODBODEGA = BOD.CODBODEGA
INNER JOIN dbo.INPACIENT AS PAC  ON A.IPCODPACI = PAC.IPCODPACI
INNER JOIN dbo.INPROFSAL AS PROF  ON A.CODPROSAL = PROF.CODPROSAL
INNER JOIN dbo.INUNIFUNC AS UF  ON A.UFUCODIGO = UF.UFUCODIGO
INNER JOIN dbo.ADCENATEN AS CENT  ON CENT.CODCENATE = A.CODCENATE
INNER JOIN dbo.ADINGRESO AS ING  ON A.NUMINGRES = ING.NUMINGRES
LEFT JOIN dbo.CHCAMASHO AS CAMA  ON CAMA.CODICAMAS = CASE
                                                                        WHEN ING.CODCAMACT = 0 THEN ''
                                                                        ELSE CAST(ING.CODCAMACT AS VARCHAR(15))
                                                                    END
LEFT JOIN DetailFlags AS DF ON DF.CODCONCEC = A.CODCONCEC
WHERE A.DEVESTADO = '1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Panel de control (dashboard) de devoluciones de medicamentos e insumos pendientes en farmacia hospitalaria. Consolida en una sola consulta cada devolución activa (estado 1) registrada en HCDEVMEDC, enriquecida con datos del paciente (cédula, nombre, fecha de nacimiento), del profesional de salud, de la unidad funcional, del centro de atención, de la bodega de destino y de la cama hospitalaria asignada al ingreso. Clasifica cada devolución según si contiene medicamentos (tipo 1), insumos (tipo 2) o medicamentos-insumos combinados (tipo 3), identificando productos con cantidad pendiente mayor a cero desde HCDEVMEDD cruzado con el catálogo IHLISTPRO. Sirve como fuente de datos para el módulo de farmacia que monitorea devoluciones en curso, permitiendo filtrar por origen del devolutivo (traslado de cama, egreso, hoja de gasto quirúrgico o devolución parcial de enfermería).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewDashBoardPharmacyDevolution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewDashBoardPharmacyDevolution';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone para un dashboard las devoluciones de farmacia activas con datos del paciente, médico, bodega, cama y unidad funcional, indicando si la devolución contiene medicamentos, insumos o ambos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de devoluciones en estado activo (''1'') en HCDEVMEDC.; Cada devolución debe tener bodega, paciente, profesional, unidad funcional, centro de atención e ingreso válidos (joins internos).; Para clasificar el tipo de producto deben existir detalles en HCDEVMEDD con cantidad pendiente mayor a cero y referencia en IHLISTPRO.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen devoluciones activas (DEVESTADO=''1'').; Cada fila representa una devolución (CODCONCEC) y su clasificación de productos se basa únicamente en ítems con cantidad pendiente > 0.; El campo ImpresionDev siempre se devuelve vacío/false (placeholder).; SelectOption siempre es 0.; La descripción del centro de atención se concatena como ''CODCENATE - NOMCENATE''.; La bodega se presenta como ''CODBODEGA - DESBODEGA''.; Si el ingreso no tiene cama válida, CodigoCama se devuelve como cadena vacía.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Devolución de farmacia; Medicamentos; Insumos; Paciente; Profesional de la salud; Centro de atención; Unidad funcional; Bodega; Cama hospitalaria; Ingreso hospitalario; Origen de devolutivo (traslado, egreso, hoja de gasto QX, enfermería)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCDEVMEDC: Solo retorna devoluciones cuyo DEVESTADO = ''1'' (devoluciones activas/vigentes).; [RETURN_RESULT] dbo.HCDEVMEDD: Solo se consideran detalles de devolución con CANPENDIE > 0 para determinar tipo de producto (medicamento/insumo).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ORIDEVMED = 1 → Se etiqueta el origen como ''Traslado de Cama Hospitalizacion''.; si ORIDEVMED = 2 → Se etiqueta como ''Egreso de Cama Hospitalizacion''.; si ORIDEVMED = 5 → Se etiqueta como ''Hoja de Gasto QX''.; si ORIDEVMED distinto de 1, 2 o 5 → Se etiqueta como ''Enfermeria (Devolutivo Parcial)''.; si IHLISTPRO.TIPPRODUC = 1 para algún ítem pendiente de la devolución → Se marca la devolución como MEDICAMENTO = 1. else MEDICAMENTO = 0.; si IHLISTPRO.TIPPRODUC = 2 para algún ítem pendiente → Se marca INSUMOS = 1. else INSUMOS = 0.; si IHLISTPRO.TIPPRODUC = 3 para algún ítem pendiente → Se marca MEDICAMENTO_INSUMO = 1. else MEDICAMENTO_INSUMO = 0.; si ADINGRESO.CODCAMACT = 0 o nulo → Se trata como sin cama asignada (cadena vacía) y no se enlaza con CHCAMASHO. else Se enlaza CODCAMACT con CHCAMASHO.CODICAMAS para obtener NUMCAMHOS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.GetProfessionType', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCDEVMEDC; dbo.HCDEVMEDD; dbo.IHLISTPRO; dbo.IHBODEGAS; dbo.INPACIENT; dbo.INPROFSAL; dbo.INUNIFUNC; dbo.ADCENATEN; dbo.ADINGRESO; dbo.CHCAMASHO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyDevolution';
GO
