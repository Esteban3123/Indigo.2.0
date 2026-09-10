CREATE VIEW [dbo].[ViewDashBoardPharmacy]
AS
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
    ISNULL(CAMA.NUMCAMHOS, '') AS CodigoCama,
    A.TIPOSOLICITUD1,
    A.PERMITEEXTRA,
    RTRIM(LTRIM(A.CODCENATE)) + ' - ' + RTRIM(LTRIM(CENT.NOMCENATE)) AS CareCenterDescription,
    A.PatientDischarge,
    PAC.IPFECNACI AS BirthDay,
    CASE WHEN ISNULL(A.MEDICAMENTO, 0) > 0 THEN 1 ELSE 0 END AS MEDICAMENTO,
    CASE WHEN ISNULL(A.INSUMOS, 0) > 0 THEN 1 ELSE 0 END AS INSUMOS,
    CASE WHEN ISNULL(A.MEDICAMENTO_INSUMO, 0) > 0 THEN 1 ELSE 0 END AS MEDICAMENTO_INSUMO
FROM dbo.ViewPharmacy AS A
INNER JOIN dbo.IHBODEGAS AS BOD ON BOD.CODBODEGA = A.CODBODEGA
INNER JOIN dbo.INPACIENT AS PAC ON PAC.IPCODPACI = A.IPCODPACI
INNER JOIN dbo.INPROFSAL AS PROF ON PROF.CODPROSAL = A.CODPROSAL
INNER JOIN dbo.INUNIFUNC AS UF ON UF.UFUCODIGO = A.UFUCODIGO
INNER JOIN dbo.ADCENATEN AS CENT ON CENT.CODCENATE = A.CODCENATE
LEFT JOIN dbo.CHCAMASHO AS CAMA ON CAMA.CODICAMAS = CASE
                                                                    WHEN A.CODCAMACT = 0 THEN ''
                                                                    ELSE CAST(A.CODCAMACT AS varchar(15))
                                                                END
WHERE A.HasQuantityRemaining = 1;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Panel de control (dashboard) de farmacia que consolida las órdenes de medicamentos e insumos pendientes de dispensar, es decir, aquellas que aún tienen cantidad por entregar. Integra información de la orden farmacéutica (consecutivo, fecha, estado, tipo y folio), datos del paciente (cédula, nombre completo, fecha de nacimiento), unidad funcional o servicio donde está atendido, médico prescriptor y su tipo de profesión, bodega asignada para la dispensación, centro de atención con su descripción, número de ingreso, cama hospitalaria, y consecutivos de prescripción de medicamentos e insumos. Se usa para que el personal de farmacia visualice en tiempo real las órdenes activas que requieren atención, filtrando solo las que tienen saldo pendiente por entregar, e indica si la orden contiene medicamentos, insumos o ambos, y si los medicamentos han sido validados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewDashBoardPharmacy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewDashBoardPharmacy';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida información de órdenes de farmacia con saldo pendiente, enriqueciendo datos del paciente, profesional, bodega, unidad funcional, centro de atención y cama para alimentar el dashboard de farmacia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las órdenes deben existir en ViewPharmacy con HasQuantityRemaining = 1 (cantidad pendiente); La orden debe tener bodega, paciente, profesional, unidad funcional y centro de atención válidos (JOIN obligatorio)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen órdenes de farmacia con cantidad pendiente por entregar; ConsecutivoPrescripcion y ConsecutivoInsumos nunca son NULL: se normalizan a cadena vacía; TIPOSOLICITUD nunca es NULL: se normaliza a 0; CodigoCama nunca es NULL: se normaliza a cadena vacía; Los campos Validado, MEDICAMENTO, INSUMOS y MEDICAMENTO_INSUMO siempre son booleanos/binarios (0 o 1); Impresion siempre se entrega como bit con valor falso (no se persiste estado de impresión en la vista); La descripción del centro de atención se compone como ''CODCENATE - NOMCENATE''; La descripción de la bodega se compone como ''CODBODEGA - DESBODEGA''; Una orden sin cama asociada o con cama 0 se reporta sin código de cama', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Profesional de la salud; Médico; Tipo de profesión; Bodega/almacén; Centro de atención; Unidad funcional; Centro de costos; Orden de farmacia; Prescripción; Insumos; Medicamento; Cama hospitalaria; Ingreso/hospitalización; Folio; Tipo de solicitud; Validación de medicamento; Alta del paciente (PatientDischarge); Fecha de nacimiento; Saldo/cantidad remanente de dispensación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ViewPharmacy: Solo se retornan registros donde HasQuantityRemaining = 1 (existe cantidad remanente por dispensar)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si MEDICAMENTOVALIDADO IS NULL OR MEDICAMENTOVALIDADO = 0 → Validado se reporta como 0 (no validado) else Validado se reporta como 1 (validado); si CODCAMACT = 0 o NULL → Se intenta empate con cadena vacía contra CODICAMAS, dejando CodigoCama vacío else Se castea CODCAMACT a VARCHAR(15) para emparejar con CHCAMASHO.CODICAMAS; si MEDICAMENTO > 0 → Indicador MEDICAMENTO = 1 else Indicador MEDICAMENTO = 0; si INSUMOS > 0 → Indicador INSUMOS = 1 else Indicador INSUMOS = 0; si MEDICAMENTO_INSUMO > 0 → Indicador MEDICAMENTO_INSUMO = 1 else Indicador MEDICAMENTO_INSUMO = 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.GetProfessionType', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ViewPharmacy; dbo.IHBODEGAS; dbo.INPACIENT; dbo.INPROFSAL; dbo.INUNIFUNC; dbo.ADCENATEN; dbo.CHCAMASHO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacy';
GO
