CREATE VIEW [dbo].[ViewDashBoardPharmacyMixingSation]
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
    PAC.IPFECNACI AS BirthDay
FROM dbo.ViewPharmacyMixingStationWithOncology AS A 
INNER JOIN dbo.IHBODEGAS AS BOD  ON BOD.CODBODEGA = A.CODBODEGA
INNER JOIN dbo.INPACIENT AS PAC  ON PAC.IPCODPACI = A.IPCODPACI
INNER JOIN dbo.INPROFSAL AS PROF  ON PROF.CODPROSAL = A.CODPROSAL
INNER JOIN dbo.INUNIFUNC AS UF  ON UF.UFUCODIGO = A.UFUCODIGO
INNER JOIN dbo.ADCENATEN AS CENT  ON CENT.CODCENATE = A.CODCENATE
LEFT JOIN dbo.CHCAMASHO AS CAMA  ON CAMA.CODICAMAS = CASE
                                                                        WHEN A.CODCAMACT = 0 THEN ''
                                                                        ELSE CAST(A.CODCAMACT AS varchar(15))
                                                                    END
WHERE A.RoutingMP = 1;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Panel de control (dashboard) para la estación de mezclas de farmacia, incluyendo oncología. Consolida las órdenes de preparación de medicamentos pendientes o en proceso, mostrando para cada orden: el paciente (cédula, nombre, fecha de nacimiento), el médico prescriptor y su tipo de profesión, la unidad funcional o servicio donde está hospitalizado el paciente, la cama asignada, la bodega de farmacia responsable, el centro de atención con su descripción, y el estado actual de la orden. Integra información de órdenes de mezclas (ViewPharmacyMixingStationWithOncology), datos del paciente (INPACIENT), profesional de la salud (INPROFSAL), unidad funcional (INUNIFUNC), centro de atención (ADCENATEN), bodega de inventario (IHBODEGAS) y cama hospitalaria (CHCAMASHO), filtrando únicamente las solicitudes enrutadas a la estación de mezclas (RoutingMP = 1). Sirve como fuente de datos para el tablero operativo de farmacia, permitiendo al personal visualizar y gestionar las preparaciones de mezclas y quimioterapias pendientes por paciente, ingreso y ubicación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewDashBoardPharmacyMixingSation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewDashBoardPharmacyMixingSation';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone, para un dashboard de la estación de mezclas de farmacia, las órdenes de medicamentos enrutadas a esa estación con datos del paciente, médico, unidad funcional, bodega, centro de atención y cama.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyMixingSation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La orden debe existir en ViewPharmacyMixingStationWithOncology con RoutingMP = 1 (enrutada a estación de mezclas).; Deben existir registros relacionados en IHBODEGAS, INPACIENT, INPROFSAL, INUNIFUNC y ADCENATEN para que la fila aparezca (joins internos).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyMixingSation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen órdenes con enrutamiento a estación de mezclas (RoutingMP = 1).; Toda fila resultante tiene paciente, profesional, unidad funcional, bodega y centro de atención existentes.; El indicador Validado siempre es bit 0/1; nunca nulo.; TIPOSOLICITUD nunca es nulo en la salida (default 0).; CodigoCama nunca es nulo (default '''').; La descripción del centro de atención siempre se entrega como ''CODCENATE - NOMCENATE''.; La bodega siempre se entrega como ''CODBODEGA - DESBODEGA''.; Impresion siempre se devuelve como bit vacío/0 por defecto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyMixingSation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estación de mezclas de farmacia; Oncología; Paciente; Profesional de la salud; Unidad funcional; Bodega de medicamentos; Centro de atención; Centro de costos; Cama hospitalaria; Ingreso/folio; Prescripción médica; Insumos; Validación de medicamento; Tipo de solicitud; Orden de medicamento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyMixingSation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ViewDashBoardPharmacyMixingSation: Devuelve solo órdenes con A.RoutingMP = 1, marcando Validado=1 cuando MEDICAMENTOVALIDADO no es nulo ni 0; en caso contrario Validado=0.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyMixingSation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.MEDICAMENTOVALIDADO IS NULL OR A.MEDICAMENTOVALIDADO = 0 → Validado = 0 (medicamento no validado) else Validado = 1 (medicamento validado); si A.CODCAMACT = 0 → Se trata como cadena vacía para buscar cama (no se asocia cama) else Se convierte CODCAMACT a varchar(15) y se busca en CHCAMASHO.CODICAMAS; si A.CODCONCEP es NULL o cadena vacía → ConsecutivoPrescripcion se devuelve como '''' else Se devuelve el valor de A.CODCONCEP; si A.CODCONCES es NULL o cadena vacía → ConsecutivoInsumos se devuelve como '''' else Se devuelve el valor de A.CODCONCES; si A.TIPOSOLICITUD es NULL → TIPOSOLICITUD se entrega como 0 else Se entrega el valor original', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyMixingSation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.GetProfessionType', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyMixingSation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ViewPharmacyMixingStationWithOncology; dbo.IHBODEGAS; dbo.INPACIENT; dbo.INPROFSAL; dbo.INUNIFUNC; dbo.ADCENATEN; dbo.CHCAMASHO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyMixingSation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyMixingSation';
GO
