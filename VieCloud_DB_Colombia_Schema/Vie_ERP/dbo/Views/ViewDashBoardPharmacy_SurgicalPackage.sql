CREATE VIEW [dbo].[ViewDashBoardPharmacy_SurgicalPackage]
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
    CC.CostCenterId,
    CASE
        WHEN CC.ObtainCostCenter = 3 THEN CC.CostCenterCode
        ELSE RTRIM(A.CODCENCOS)
    END AS CentroCostos,
    A.ORDTRANUE AS Tipo,
    A.NUMEFOLIO AS Folio,
    A.IDETIPHIS,
    CAST(0 AS bit) AS Impresion,
    CASE
        WHEN A.MEDICAMENTOVALIDADO IS NULL OR A.MEDICAMENTOVALIDADO = 0
            THEN CAST(0 AS bit)
        ELSE CAST(1 AS bit)
    END AS Validado,
    RTRIM(SER.CODSERIPS) + ' - ' + RTRIM(SER.DESSERIPS) AS Procedimiento,
    RTRIM(ESP.CODESPECI) + ' - ' + RTRIM(ESP.DESESPECI) AS Especialidad,
    CASE P.ORIGENQX
        WHEN 1 THEN 'Ambulatoria'
        ELSE 'Hospitalaria'
    END AS OrigenQXDescripcion,
    SALA.CODIGSALA + ' - ' + SALA.DESCRIPSAL AS Sala,
    '' AS Entidad,
    A.IDAGEPROGQX,
    P.FECHORAIN AS FechaCirugia,
    CASE A.TIPOSOLQX
        WHEN 1 THEN 'Paquete QX'
        WHEN 2 THEN 'Sol. Enfermería asociada a la cirugía'
    END AS TipoSolicitud,
    RTRIM(LTRIM(A.CODCENATE)) + ' - ' + RTRIM(LTRIM(CENT.NOMCENATE)) AS CareCenterDescription,
    PAC.IPFECNACI AS BirthDay,
    ISNULL(DF.MEDICAMENTO, 0) AS MEDICAMENTO,
    ISNULL(DF.INSUMOS, 0) AS INSUMOS,
    ISNULL(DF.MEDICAMENTO_INSUMO, 0) AS MEDICAMENTO_INSUMO
FROM dbo.HCFARMEPC AS A 
INNER JOIN dbo.IHBODEGAS AS BOD  ON A.CODBODEGA = BOD.CODBODEGA
INNER JOIN dbo.INPACIENT AS PAC  ON A.IPCODPACI = PAC.IPCODPACI
INNER JOIN dbo.INPROFSAL AS PROF  ON A.CODPROSAL = PROF.CODPROSAL
INNER JOIN dbo.AGEPROGQX AS P  ON A.IDAGEPROGQX = P.CODAUTONU
INNER JOIN dbo.INCUPSIPS AS SER  ON SER.CODSERIPS = P.CODSERIPS
INNER JOIN dbo.INESPECIA AS ESP  ON ESP.CODESPECI = P.CODESPECI
INNER JOIN dbo.AGENSALAC AS SALA  ON SALA.CODCONCEC = P.AGENSALAC
INNER JOIN dbo.ADCENATEN AS CENT  ON CENT.CODCENATE = A.CODCENATE
INNER JOIN dbo.INUNIFUNC AS UF  ON UF.UFUCODIGO = SALA.UFUCODIGO
LEFT JOIN DetailFlags AS DF ON DF.CODCONCEC = A.CODCONCEC
OUTER APPLY
(
    SELECT TOP (1)
        BC.ObtainCostCenter,
        COST.Id AS CostCenterId,
        ISNULL(COST.Code, '') AS CostCenterCode
    FROM Inventory.SettingInventory AS SI 
    INNER JOIN Contract.CUPSEntity AS CUPS  ON CUPS.Code = SER.CODSERIPS
    INNER JOIN Billing.BillingConcept AS BC  ON BC.Id = CUPS.BillingConceptId
    LEFT JOIN Billing.BillingConceptCostCenter AS BCCC  ON BCCC.BillingConceptId = BC.Id
    LEFT JOIN Payroll.FunctionalUnit AS FU  ON FU.Id = BCCC.FunctionalUnitId AND FU.Code = UF.UFUCODIGO
    LEFT JOIN Payroll.CostCenter AS COST  ON COST.Id = BCCC.CostCenterId AND FU.Id IS NOT NULL
    WHERE SI.PharmacySuppliesCostCenter = 2
    ORDER BY  BC.ObtainCostCenter DESC, COST.Code DESC
) AS CC
WHERE A.ORDESTADO = '1' AND A.IDAGEPROGQX IS NOT NULL;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista del dashboard de farmacia para paquetes quirúrgicos. Consolida las órdenes de medicamentos y/o insumos activas (estado 1) que están vinculadas a una programación quirúrgica, mostrando para cada orden: el paciente (cédula, nombre, fecha de nacimiento), el médico prescriptor y su profesión, la bodega de despacho, el ingreso hospitalario, la unidad funcional y sala de cirugía, el procedimiento quirúrgico (código CUPS) y la especialidad, el tipo de solicitud (paquete quirúrgico o solicitud de enfermería asociada a cirugía), el centro de atención, el centro de costos resuelto según configuración de inventario y facturación, y banderas que indican si la orden contiene medicamentos, insumos o ambos con cantidades pendientes por despachar. Se utiliza para la gestión y seguimiento operativo de farmacia en quirófano, permitiendo identificar rápidamente qué cirugías tienen materiales pendientes de entregar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewDashBoardPharmacy_SurgicalPackage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewDashBoardPharmacy_SurgicalPackage';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone para un dashboard de farmacia las órdenes activas de paquete quirúrgico vinculadas a una programación de cirugía, enriquecidas con datos del paciente, profesional, sala, especialidad, centro de costos y banderas de medicamentos/insumos pendientes.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacy_SurgicalPackage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las órdenes deben tener ORDESTADO=''1'' (activas); Las órdenes deben tener IDAGEPROGQX no nulo (asociadas a una programación quirúrgica); Debe existir correspondencia válida en bodega, paciente, profesional, programación quirúrgica, procedimiento CUPS, especialidad, sala, centro de atención y unidad funcional (joins internos)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacy_SurgicalPackage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan órdenes de farmacia activas asociadas a una cirugía programada; Las banderas MEDICAMENTO/INSUMOS/MEDICAMENTO_INSUMO solo se encienden cuando hay productos con cantidad pendiente (CANPENPRO>0) en HCFARMEPD; La selección del centro de costos parametrizado prioriza ObtainCostCenter más alto y código de centro de costos descendente, y solo aplica cuando la configuración de inventario tiene PharmacySuppliesCostCenter=2; La unidad funcional usada para resolver el centro de costos proviene de la sala de la programación quirúrgica; La validación del medicamento es booleana: cualquier valor distinto de 0/NULL se considera validado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacy_SurgicalPackage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Farmacia; Paquete quirúrgico; Programación de cirugía; Medicamentos; Insumos; Orden médica; Paciente; Profesional de la salud; Especialidad médica; Sala quirúrgica; Centro de atención; Unidad funcional; Centro de costos; Concepto de facturación; CUPS; Validación de medicamento; Origen quirúrgico (ambulatorio/hospitalario); Bodega de inventario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacy_SurgicalPackage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve únicamente órdenes farmacéuticas con ORDESTADO=''1'' y IDAGEPROGQX no nulo, una fila por orden (CODCONCEC)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacy_SurgicalPackage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si MEDICAMENTOVALIDADO IS NULL o = 0 → Validado = 0 (bit) else Validado = 1 (bit); si P.ORIGENQX = 1 → OrigenQXDescripcion = ''Ambulatoria'' else OrigenQXDescripcion = ''Hospitalaria''; si A.TIPOSOLQX = 1 → TipoSolicitud = ''Paquete QX'' else Si TIPOSOLQX = 2 entonces ''Sol. Enfermería asociada a la cirugía''; si cc.ObtainCostCenter = 3 → CentroCostos = código de centro de costos parametrizado (cc.CostCenterCode) else CentroCostos = A.CODCENCOS de la orden; si Existe producto pendiente (CANPENPRO>0) con TIPPRODUC=1 para la orden → MEDICAMENTO = 1 else MEDICAMENTO = 0; si Existe producto pendiente con TIPPRODUC=2 para la orden → INSUMOS = 1 else INSUMOS = 0; si Existe producto pendiente con TIPPRODUC=3 para la orden → MEDICAMENTO_INSUMO = 1 else MEDICAMENTO_INSUMO = 0; si Inventory.SettingInventory.PharmacySuppliesCostCenter = 2 → Se evalúa la obtención del centro de costos vía BillingConcept/BillingConceptCostCenter cruzando unidad funcional de la sala else No se obtiene CostCenterId/Code parametrizado (cc queda vacío)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacy_SurgicalPackage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.GetProfessionType', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacy_SurgicalPackage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFARMEPC; dbo.HCFARMEPD; dbo.IHLISTPRO; dbo.IHBODEGAS; dbo.INPACIENT; dbo.INPROFSAL; dbo.AGEPROGQX; dbo.INCUPSIPS; dbo.INESPECIA; dbo.AGENSALAC; dbo.ADCENATEN; dbo.INUNIFUNC; Inventory.SettingInventory; Contract.CUPSEntity; Billing.BillingConcept; Billing.BillingConceptCostCenter; Payroll.FunctionalUnit; Payroll.CostCenter', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacy_SurgicalPackage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacy_SurgicalPackage';
GO
