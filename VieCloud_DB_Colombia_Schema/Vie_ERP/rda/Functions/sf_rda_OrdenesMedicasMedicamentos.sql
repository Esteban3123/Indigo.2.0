
CREATE FUNCTION [rda].[sf_rda_OrdenesMedicasMedicamentos]
(
	@NumeroIngreso    char(10),
    @CodigoPaciente   varchar(25),
    @FolioInicio      nchar(10),
    @FolioFin         nchar(10),  -- NULL = folio único (igual a @FolioInicio)
    @ManExtPro        bit         -- NULL = ignorar filtro, 0 o 1 = filtrar explícitamente
)
RETURNS varchar(MAX)
AS
BEGIN
    
	DECLARE @json NVARCHAR(MAX)
	SET @json = (
    SELECT
        TipoMedicamento,
        AlternativeCode,
        CodigoATC,
        NombreDCI,
        FechaPrescripcion,
        DosisOrdenada,
        UnidadMedidaDosis,
        ViaAdministracion,
        Frecuencia,
        UnidadFrecuencia,
        DosisAplicadas
    FROM (
        -- =============================================
        -- 1. MEDICAMENTOS DE QUIMIOTERAPIA
        -- =============================================
        SELECT
            IIF(ATC_INFO.UNIRS = 1, '05. Medicamento UNIRS', MT.Code + '. ' + MT.Name)  AS TipoMedicamento,
            TRY_CAST(DCI.AlternativeCode AS INT) AS AlternativeCode,
            ATC_INFO.Code AS CodigoATC,
            DCI.Name AS NombreDCI,
            QC.FECHAREGISTRO AS FechaPrescripcion,
            QM.DOSISPROD AS DosisOrdenada,
            RTRIM(UN.ABRUNIMED) AS UnidadMedidaDosis,
            RTRIM(VIA.DESVIAADM) AS ViaAdministracion,
            FREQ.FRECUENCI AS Frecuencia,
            CASE FREQ.UNIFRECUE
                WHEN '1' THEN 'Minutos'
                WHEN '2' THEN 'Horas'
                WHEN '3' THEN 'Días'
            END AS UnidadFrecuencia,
            QC.ID AS QuimioID,
            QM.CODPRODUC AS CodProducto,
            NULL AS CodConcec
        FROM EHR.HCORDQUIMIO               QC
        INNER JOIN EHR.HCORDMEDICAM          QM  ON QM.IDHCORDQUIMIO  = QC.ID
        INNER JOIN dbo.IHLISTPRO             P   ON P.CODPRODUC        = QM.CODPRODUC
        INNER JOIN dbo.INUNIMEDI             UN  ON UN.CODUNIMED        = QM.CODUNIMED
        INNER JOIN dbo.HCVIAADMI             VIA ON VIA.CODVIAADM       = QM.CODVIAADM
        OUTER APPLY (
            SELECT TOP 1 ATC.Id, ATC.UNIRS, ATC.DCIId, ATC.Code
            FROM Inventory.ATC ATC
            WHERE ATC.ATCEntityId = QM.ATCEntityId
            ORDER BY ATC.Id ASC
        ) ATC_INFO
        LEFT JOIN Inventory.DCI              DCI ON DCI.Id              = ATC_INFO.DCIId
        OUTER APPLY (
            SELECT TOP 1 INV.MedicationTypeId
            FROM Inventory.InventoryProduct INV
            WHERE INV.ATCId = ATC_INFO.Id
            ORDER BY INV.Id ASC
        ) INV_INFO
        LEFT JOIN Inventory.MedicationType   MT  ON MT.Id               = INV_INFO.MedicationTypeId
        OUTER APPLY (
            SELECT TOP 1 FRECUENCI, UNIFRECUE
            FROM dbo.HCHOJAMED
            WHERE IDHCORDQUIMIO = QC.ID
              AND CODPRODUC     = QM.CODPRODUC
            ORDER BY FECPROAPL ASC
        ) FREQ
        WHERE QC.IPCODPACI        = @CodigoPaciente
          AND RTRIM(QC.NUMINGRES)  = @NumeroIngreso
          AND RTRIM(QC.NUMEFOLIO)  = @FolioInicio

        UNION ALL

        -- =============================================
        -- 2. MEDICAMENTOS DE CONTROL
        -- =============================================
        SELECT
            IIF(ATC_INFO.UNIRS = 1, '05. Medicamento UNIRS', MT.Code + '. ' + MT.Name)  AS TipoMedicamento,
            TRY_CAST(DCI.AlternativeCode AS INT)                                          AS AlternativeCode,
            ATC_INFO.Code                                                                  AS CodigoATC,
            DCI.Name                                                                       AS NombreDCI,
            A.FECINIDOS                                                                    AS FechaPrescripcion,
            A.DOSISPROD                                                                    AS DosisOrdenada,
            RTRIM(UN.ABRUNIMED)                                                            AS UnidadMedidaDosis,
            RTRIM(VIA.DESVIAADM)                                                           AS ViaAdministracion,
            A.FRECUENCI                                                                    AS Frecuencia,
            CASE A.UNIFRECUE
                WHEN '1' THEN 'Minutos'
                WHEN '2' THEN 'Horas'
                WHEN '3' THEN 'Días'
            END                                                                            AS UnidadFrecuencia,
            NULL         AS QuimioID,
            A.CODPRODUC  AS CodProducto,
            A.CODCONCEC  AS CodConcec
        FROM dbo.HCPRESCRD               A
        INNER JOIN dbo.HCPRODUCTOSCONTROL  E   ON E.CODPRODUC  = A.CODPRODUC
                                               AND E.NUMEFOLIO  = A.NUMEFOLIO
                                               AND E.NUMINGRES  = A.NUMINGRES
        INNER JOIN dbo.IHLISTPRO           B   ON B.CODPRODUC   = A.CODPRODUC
        INNER JOIN dbo.INUNIMEDI           UN  ON UN.CODUNIMED   = A.CODUNIMED
        INNER JOIN dbo.HCVIAADMI           VIA ON VIA.CODVIAADM  = A.CODVIAADM
        OUTER APPLY (
            SELECT TOP 1 ATC.Id, ATC.UNIRS, ATC.DCIId, ATC.Code
            FROM Inventory.InventoryProduct INV
            INNER JOIN Inventory.ATC ATC ON ATC.Id = INV.ATCId
            WHERE INV.Code = A.CODPRODUC
            ORDER BY INV.Id ASC
        ) ATC_INFO
        LEFT JOIN Inventory.DCI            DCI ON DCI.Id              = ATC_INFO.DCIId
        OUTER APPLY (
            SELECT TOP 1 INV2.MedicationTypeId
            FROM Inventory.InventoryProduct INV2
            WHERE INV2.ATCId = ATC_INFO.Id
            ORDER BY INV2.Id ASC
        ) INV_INFO
        LEFT JOIN Inventory.MedicationType MT  ON MT.Id               = INV_INFO.MedicationTypeId
        WHERE A.MANEXTPRO      = 1
          AND A.IPCODPACI      = @CodigoPaciente
          AND A.NUMINGRES      = @NumeroIngreso
          AND A.NUMEFOLIO      = @FolioInicio
          AND A.IDESQUEMAONC  IS NULL

        UNION ALL

        -- =============================================
        -- 3. MEDICAMENTOS NORMALES
        -- =============================================
        SELECT
            IIF(ATC_INFO.UNIRS = 1, '05. Medicamento UNIRS', MT.Code + '. ' + MT.Name)  AS TipoMedicamento,
            TRY_CAST(DCI.AlternativeCode AS INT)                                          AS AlternativeCode,
            ATC_INFO.Code                                                                  AS CodigoATC,
            DCI.Name                                                                       AS NombreDCI,
            A.FECINIDOS                                                                    AS FechaPrescripcion,
            A.DOSISPROD                                                                    AS DosisOrdenada,
            RTRIM(UN.ABRUNIMED)                                                            AS UnidadMedidaDosis,
            RTRIM(VIA.DESVIAADM)                                                           AS ViaAdministracion,
            A.FRECUENCI                                                                    AS Frecuencia,
            CASE A.UNIFRECUE
                WHEN '1' THEN 'Minutos'
                WHEN '2' THEN 'Horas'
                WHEN '3' THEN 'Días'
            END                                                                            AS UnidadFrecuencia,
            NULL         AS QuimioID,
            A.CODPRODUC  AS CodProducto,
            A.CODCONCEC  AS CodConcec
        FROM dbo.HCPRESCRD               A
        INNER JOIN dbo.IHLISTPRO           B   ON B.CODPRODUC   = A.CODPRODUC
        INNER JOIN dbo.INUNIMEDI           UN  ON UN.CODUNIMED   = A.CODUNIMED
        INNER JOIN dbo.HCVIAADMI           VIA ON VIA.CODVIAADM  = A.CODVIAADM
        OUTER APPLY (
            SELECT TOP 1 ATC.Id, ATC.UNIRS, ATC.DCIId, ATC.Code
            FROM Inventory.InventoryProduct INV
            INNER JOIN Inventory.ATC ATC ON ATC.Id = INV.ATCId
            WHERE INV.Code = A.CODPRODUC
            ORDER BY INV.Id ASC
        ) ATC_INFO
        LEFT JOIN Inventory.DCI            DCI ON DCI.Id              = ATC_INFO.DCIId
        OUTER APPLY (
            SELECT TOP 1 INV2.MedicationTypeId
            FROM Inventory.InventoryProduct INV2
            WHERE INV2.ATCId = ATC_INFO.Id
            ORDER BY INV2.Id ASC
        ) INV_INFO
        LEFT JOIN Inventory.MedicationType MT  ON MT.Id               = INV_INFO.MedicationTypeId
        WHERE A.MANEXTPRO      = 1
          AND A.IPCODPACI      = @CodigoPaciente
          AND A.NUMINGRES      = @NumeroIngreso
          AND A.NUMEFOLIO      = @FolioInicio
          AND A.IDESQUEMAONC  IS NULL
          -- Excluir los que ya son de control
          AND NOT EXISTS (
              SELECT 1 FROM dbo.HCPRODUCTOSCONTROL E
              WHERE E.CODPRODUC = A.CODPRODUC
                AND E.NUMEFOLIO = A.NUMEFOLIO
                AND E.NUMINGRES = A.NUMINGRES
          )
    ) BASE
    OUTER APPLY (
        SELECT
            HM.DOSISPROD               AS Dosis,
            RTRIM(UN_D.ABRUNIMED)      AS UnidadMedida,
            HM.FECAPLMED               AS FechaAdministracion,
            RTRIM(TI.NOMBRE)           AS TipoIdentificacion,
            RTRIM(PROF.CODIGONIT)      AS NumeroDocumento
        FROM dbo.HCHOJAMED             HM
        LEFT JOIN dbo.INUNIMEDI        UN_D ON UN_D.CODUNIMED  = HM.CODUNIMED
        LEFT JOIN dbo.INPROFSAL        PROF ON PROF.CODPROSAL  = HM.CODPROAPL
        LEFT JOIN dbo.ADTIPOIDENTIFICA TI   ON TI.ID           = PROF.IDADTIPOIDENTIFICA
        WHERE HM.MEDESTADO = '2'
          AND (
              (BASE.QuimioID  IS NOT NULL AND HM.IDHCORDQUIMIO = BASE.QuimioID AND HM.CODPRODUC = BASE.CodProducto)
              OR
              (BASE.CodConcec IS NOT NULL AND HM.CONSECPRESCRA = BASE.CodConcec)
          )
        FOR JSON PATH
    ) DOSIS(DosisAplicadas)
    FOR JSON PATH, ROOT('Medicamentos')
)

RETURN @json

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que consolida en un JSON (raíz `Medicamentos`) las órdenes médicas de medicamentos de un paciente para un ingreso y folio específicos, agrupando tres fuentes: medicamentos de quimioterapia (`HCORDQUIMIO`/`HCORDMEDICAM`), medicamentos de control (`HCPRODUCTOSCONTROL`) y prescripciones normales (`HCPRESCRD`). Para cada medicamento retorna clasificación ATC, DCI, dosis, vía, frecuencia y el detalle de dosis efectivamente administradas (estado `2` en `HCHOJAMED`) con identificación del profesional aplicador.', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'sf_rda_OrdenesMedicasMedicamentos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'sf_rda_OrdenesMedicasMedicamentos';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve un JSON consolidado con los medicamentos prescritos a un paciente en un folio/ingreso (quimioterapia, control y normales), incluyendo su clasificación ATC, tipo, frecuencia y dosis efectivamente administradas.', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'sf_rda_OrdenesMedicasMedicamentos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente (IPCODPACI), número de ingreso (NUMINGRES) y folio (NUMEFOLIO) deben existir y coincidir entre las tablas de prescripción/órdenes.; Los productos referenciados deben existir en dbo.IHLISTPRO, dbo.INUNIMEDI y dbo.HCVIAADMI (joins INNER).', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'sf_rda_OrdenesMedicasMedicamentos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran dosis aplicadas con MEDESTADO=''2'' en dbo.HCHOJAMED.; Las prescripciones de control y normales se filtran exclusivamente con MANEXTPRO=1 e IDESQUEMAONC IS NULL (excluye esquemas oncológicos en esos bloques).; Un mismo medicamento no se duplica entre el bloque de Control y el de Normales gracias al NOT EXISTS contra HCPRODUCTOSCONTROL.; La selección de ATC, MedicationType y frecuencia siempre toma TOP 1 con orden ASC por Id/FECPROAPL, garantizando un único registro por medicamento.; AlternativeCode se devuelve como entero (TRY_CAST a INT); valores no numéricos resultan en NULL.; Las unidades y vías de administración se devuelven sin espacios finales (RTRIM).', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'sf_rda_OrdenesMedicasMedicamentos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Folio de atención; Prescripción de medicamentos; Quimioterapia / esquema oncológico; Medicamento de control; Medicamento UNIRS; Clasificación ATC; DCI (Denominación Común Internacional); Vía de administración; Frecuencia de administración; Dosis ordenada y dosis aplicada; Hoja de medicación; Profesional de la salud que aplica', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'sf_rda_OrdenesMedicasMedicamentos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve varchar(MAX) con JSON serializado bajo el root ''Medicamentos'' (FOR JSON PATH, ROOT(''Medicamentos'')).', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'sf_rda_OrdenesMedicasMedicamentos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ATC_INFO.UNIRS = 1 → Etiqueta TipoMedicamento como ''05. Medicamento UNIRS'' else Concatena MT.Code + ''. '' + MT.Name desde Inventory.MedicationType; si FREQ.UNIFRECUE / A.UNIFRECUE = ''1'',''2'',''3'' → Traduce a ''Minutos'',''Horas'',''Días'' respectivamente else NULL; si Origen del medicamento: orden de quimioterapia (EHR.HCORDQUIMIO) → Se incluye en bloque 1 filtrando por paciente, ingreso y folio; se enlaza la frecuencia desde HCHOJAMED por IDHCORDQUIMIO+CODPRODUC (TOP 1 por FECPROAPL ASC); si Existe registro en HCPRODUCTOSCONTROL para (CODPRODUC, NUMEFOLIO, NUMINGRES) y A.MANEXTPRO=1 y A.IDESQUEMAONC IS NULL → Se clasifica como ''Medicamento de Control'' (bloque 2) else Si no existe en HCPRODUCTOSCONTROL, se clasifica como ''Medicamento Normal'' (bloque 3); si BASE.QuimioID IS NOT NULL → Las dosis aplicadas se buscan por HM.IDHCORDQUIMIO=QuimioID AND HM.CODPRODUC=CodProducto else Si BASE.CodConcec IS NOT NULL, se buscan por HM.CONSECPRESCRA = CodConcec', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'sf_rda_OrdenesMedicasMedicamentos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'EHR.HCORDQUIMIO; EHR.HCORDMEDICAM; dbo.IHLISTPRO; dbo.INUNIMEDI; dbo.HCVIAADMI; Inventory.ATC; Inventory.DCI; Inventory.InventoryProduct; Inventory.MedicationType; dbo.HCHOJAMED; dbo.HCPRESCRD; dbo.HCPRODUCTOSCONTROL; dbo.INPROFSAL; dbo.ADTIPOIDENTIFICA', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'sf_rda_OrdenesMedicasMedicamentos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'sf_rda_OrdenesMedicasMedicamentos';
GO
