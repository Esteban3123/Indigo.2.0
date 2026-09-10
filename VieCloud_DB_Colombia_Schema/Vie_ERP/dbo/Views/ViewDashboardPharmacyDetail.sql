
CREATE VIEW [dbo].[ViewDashboardPharmacyDetail]
AS


WITH BaseSFD AS
(
    SELECT
        SFD.*
    FROM dbo.HCFARMEPD SFD 
    WHERE SFD.CANPENPRO > 0 AND SFD.IDAGEPROGQX IS NULL
),
BaseProductoIngreso AS
(
    SELECT DISTINCT
        SFD.NUMINGRES,
        SFD.CODPRODUC
    FROM BaseSFD SFD
),
BaseProductoIngresoNoPBS AS
(
    SELECT DISTINCT
        SFD.NUMINGRES,
        SFD.CODPRODUC
    FROM BaseSFD SFD
    JOIN dbo.IHLISTPRO LP  ON LP.CODPRODUC = SFD.CODPRODUC
    WHERE LP.NOPOSPROD = 1
),
BasePaqueteEnfermeria AS
(
    SELECT DISTINCT
        SFD.CODCONCEC,
        SFD.IDAGPAQUETES,
        SFD.CODPRODUC
    FROM BaseSFD SFD
    WHERE SFD.IDAGPAQUETES IS NOT NULL
),
CantidadDispensada AS
(
    SELECT
        PH.AdmissionNumber AS NUMINGRES,
        A.Code AS CODPRODUC,
        SUM(ISNULL(PHD.Quantity, 0) - ISNULL(PHD.ReturnedQuantity, 0)) AS CantidadDispensada
    FROM Inventory.PharmaceuticalDispensing PH 
    JOIN Inventory.PharmaceuticalDispensingDetail PHD  ON PHD.PharmaceuticalDispensingId = PH.Id
    JOIN Inventory.InventoryProduct PR  ON PR.Id = PHD.ProductId
    JOIN Inventory.ATC A  ON A.Id = PR.ATCId
    JOIN BaseProductoIngreso B ON B.NUMINGRES = PH.AdmissionNumber AND B.CODPRODUC = A.Code
    WHERE PH.Status = 2
    GROUP BY PH.AdmissionNumber, A.Code
),
PM AS
(
    SELECT
        P.NUMINGRES,
        P.CODPRODUC,
        ISNULL(SUM(P.CANPEDPRO), 0) AS CANT_AUTORIZADA,
        STRING_AGG(P.CODMINSALUD, ', ') AS CODMINSALUD
    FROM dbo.HCJUNOPOM P 
    JOIN BaseProductoIngresoNoPBS B ON B.NUMINGRES = P.NUMINGRES AND B.CODPRODUC = P.CODPRODUC
    WHERE P.CODMINSALUD <> ''
    GROUP BY P.NUMINGRES, P.CODPRODUC
),
NursingPackages AS
(
    SELECT
        npo.IDHCFARMEPC,
        npo.IDAGPAQUETES,
        npod.CODPRODUC,
        SUM(npod.Quantity) AS Quantity
    FROM MedicalHistory.NursingPackagesOrder npo 
    JOIN MedicalHistory.NursingPackagesOrderDetail npod  ON npo.Id = npod.IdNursingPackagesOrder
    JOIN BasePaqueteEnfermeria B ON B.CODCONCEC = npo.IDHCFARMEPC AND B.IDAGPAQUETES = npo.IDAGPAQUETES AND B.CODPRODUC = npod.CODPRODUC
    GROUP BY npo.IDHCFARMEPC, npo.IDAGPAQUETES, npod.CODPRODUC
)
SELECT
    t.Id,
    t.FilaSeleccionada,
    t.Opcion,
    t.OpcionAnulado,
    t.Procedimiento,
    t.MarcarOpcion,
    t.Consecutivo,
    t.Ingreso,
    t.NUMEFOLIO,
    t.Entidad,
    t.CodigoEntidad,
    t.CodigoContrato,
    t.CodigoPlan,
    t.ContratoPlan,
    t.CodigoPaciente,
    t.Medico,
    t.NitMedico,
    t.Especialidad,
    t.Producto,
    t.CodProducto,
    t.CantidadPrescrita,
    t.DosisPrescrita,
    t.CantidadAutorizada,
    t.CantidadDispensada,
    t.NumeroNoPBS,
    t.AllowBillingWithoutAuthorization,
    Saldo.SaldoAutorizadoMipres,
    CASE
        WHEN t.NOPOSPROD = 1
            THEN ISNULL
            (
                PBSControl.Color,
                CASE
                    WHEN DATEADD
                    (
                        MINUTE,
                        [dbo].[fnConvertirAMinutos](t.ChangeAfter, CAST(t.ChangeAfterTimeUnit AS CHAR(1))),
                        t.FECINIDOS
                    ) > Common.GETDATE()
                        THEN t.WithoutCurrentAuthorizationColor
                    ELSE t.WithoutAuthorizationManagementColor
                END
            )
        ELSE ''
    END AS ColorAlertaPBS,
    t.NombrePaqueteEnf,
    CAST(t.Tipo AS VARCHAR(1)) AS Tipo,
    t.TipoAnterior,
    t.CantidadPaqueteEnf,
    t.CantidadSolicitada,
    t.CantidadEntregada,
    t.CantidadPendiente,
    t.Unico,
    t.NOPOS,
    t.PBS,
    t.UNIRS,
    t.UnidadMedida,
    t.IDETIPHIS,
    t.Estado,
    t.IDAGEPROGQX,
    t.EXTRAMURAL,
    t.Custodia,
    t.ADMINISTRACION,
    t.EntityId,
    t.EntityName,
    t.RoutedTo,
    t.SENDTO,
    t.CodeSusceptibleMixingStation,
    t.Note,
    t.TIPPRODUC,
    t.Observation,
    t.TotalDose,
    t.TotalDoseMeasurement,
    t.AllowPharmaceuticalCareRouting
FROM
(
    SELECT
        CAST(NEWID()as varchar(50)) as Id,
        CAST('' AS INT) AS FilaSeleccionada,
        '0' AS Opcion,
        '0' AS OpcionAnulado,
        '' AS Procedimiento,
        CAST('0' AS BIT) AS MarcarOpcion,
        SFD.CODCONCEC AS Consecutivo,
        RTRIM(SFD.NUMINGRES) AS Ingreso,
        SFD.NUMEFOLIO,
        RTRIM(HA.Name) AS Entidad,
        HA.Code AS CodigoEntidad,
        ING.CODCONTRA AS CodigoContrato,
        ING.CODPANATE AS CodigoPlan,
        RTRIM(CG.Name) AS ContratoPlan,
        RTRIM(SFD.IPCODPACI) AS CodigoPaciente,
        CONCAT(RTRIM(SFD.CODPROSAL), ' - ', RTRIM(PS.NOMMEDICO)) AS Medico,
        PS.CODIGONIT AS NitMedico,
        CONCAT(E.CODESPECI, ' - ', E.DESESPECI) AS Especialidad,
        CONCAT(RTRIM(SFD.CODPRODUC), ' - ', RTRIM(LP.DESPRODUC)) AS Producto,
        SFD.CODPRODUC AS CodProducto,
        SFD.CANPEDPRO AS CantidadPrescrita,
        CASE hc.DURACIDOS
            WHEN 'Tratamiento Continuo' THEN -1
            WHEN 'Dosis Unica         ' THEN 1
            ELSE PM.CANT_AUTORIZADA
        END AS DosisPrescrita,
        PM.CANT_AUTORIZADA AS CantidadAutorizada,
        ISNULL(CD.CantidadDispensada, 0) AS CantidadDispensada,
        PM.CODMINSALUD AS NumeroNoPBS,
        LP.NOPOSPROD,
        SIV.ChangeAfter,
        SIV.ChangeAfterTimeUnit,
        SFD.FECINIDOS,
        SIV.WithoutCurrentAuthorizationColor,
        SIV.WithoutAuthorizationManagementColor,
        SIV.DispensingWithoutAuthorization AS AllowBillingWithoutAuthorization,
        paq.NOMBRE AS NombrePaqueteEnf,
        IIF(NPOS.IDHCFARMEPC IS NULL, SFD.TIPOREGIS, '5') AS Tipo,
        CAST(SFD.TIPOREGIS AS VARCHAR(1)) AS TipoAnterior,
        ISNULL(NPOS.Quantity, 0) AS CantidadPaqueteEnf,
        SFD.CANPEDPRO AS CantidadSolicitada,
        CAST('' AS INT) AS CantidadEntregada,
        SFD.CANPENPRO AS CantidadPendiente,
        CAST(0 AS BIT) AS Unico,
        SFD.NOPOSPROD AS NOPOS,
        CASE
            WHEN SFD.Conditioned = 1 THEN 'Condicionado'
            WHEN LP.NOPOSPROD = 1 THEN 'No'
            ELSE 'Si'
        END AS PBS,
        CASE
            WHEN SFD.UNIRS = 1 THEN 'Si'
            ELSE 'No'
        END AS UNIRS,
        SFD.CODUNIMED AS UnidadMedida,
        SFD.IDETIPHIS,
        SFD.PROESTADO AS Estado,
        SFD.IDAGEPROGQX,
        CONVERT(BIT, ISNULL(SFD.EXTRAMURAL, 0)) AS EXTRAMURAL,
        ISNULL(SFD.MEDICACUSTODIA, CAST(0 AS BIT)) AS Custodia,
        RTRIM
        (
            CASE
                WHEN hc.FORMAPRESCRIBE IS NOT NULL THEN hc.DESADMINI
                ELSE
                    CASE
                        WHEN hc.DOSISPRFN IS NULL THEN
                            CASE
                                WHEN hc.DESADMINI IS NULL THEN
                                    RTRIM(CAST(ISNULL(SFD.DOSISPROD, 0) AS CHAR)) + ' '
                                    + RTRIM(CAST(ISNULL(UM2.ABRUNIMED, '') AS CHAR))
                                    +
                                    CASE
                                        WHEN SFD.FRECUENCI IS NULL THEN ''
                                        ELSE
                                            ' Cada ' + RTRIM(CAST(ISNULL(SFD.FRECUENCI, '') AS CHAR))
                                            + ISNULL
                                            (
                                                CASE SFD.UNIFRECUE
                                                    WHEN '1' THEN ' min(s) '
                                                    WHEN '2' THEN ' Hora(s) '
                                                    WHEN '3' THEN ' Dia(s) '
                                                END,
                                                ''
                                            )
                                    END
                                    + ' Vía: ' + ADM_VIA.ViaAdministracion
                                ELSE hc.DESADMINI
                            END
                        WHEN hc.DURACIDOS = 'Dosis Unica' THEN
                            RTRIM(CAST(hc.DOSISPRFN AS CHAR)) + ' '
                            + RTRIM(CAST(UM.ABRUNIMED AS CHAR))
                            + ' Dosis Única Via: ' + ADM_VIA.ViaAdministracion
                        ELSE
                            RTRIM(CAST(hc.DOSISPRFN AS CHAR)) + ' '
                            + RTRIM(CAST(UM.ABRUNIMED AS CHAR))
                            + ' Cada ' + RTRIM(CAST(hc.FRECUENCI AS CHAR))
                            + CASE hc.UNIFRECUE
                                WHEN '1' THEN ' min(s) '
                                WHEN '2' THEN ' Hora(s) '
                                WHEN '3' THEN ' Dia(s) '
                              END
                            + ' Vía: ' + ADM_VIA.ViaAdministracion
                    END
            END
        ) AS ADMINISTRACION,
        SFD.ID AS EntityId,
        'HCFARMEPD' AS EntityName,
        CASE ISNULL(SFD.SENDTO, 1)
            WHEN 0 THEN 'Pendiente'
            WHEN 1 THEN 'Servicio Farmaceutico'
            WHEN 2 THEN 'Central de Mezclas'
        END AS RoutedTo,
        ISNULL(SFD.SENDTO, 1) AS SENDTO,
        CAST(SFD.CodeSusceptibleMixingStation AS VARCHAR(36)) AS CodeSusceptibleMixingStation,
        PDN.Note,
        LP.TIPPRODUC,
        ISNULL(hc.INDAPLMED, mescla.INDAPLMED) AS Observation,
        CASE
            WHEN psm.Id IS NOT NULL THEN SFD.DOSISPROD * psm.ApplicationsNumber
            WHEN SFD.SourceTable = 'HCPRESCRA' THEN hcex.NUMEROAPLICACIONINICIAL * SFD.DOSISPROD
            ELSE SFD.DOSISPROD
        END AS TotalDose,
        UM3.ABRUNIMED AS TotalDoseMeasurement,
        ISNULL(PharmCareRoute.Flag, CAST(0 AS BIT)) AS AllowPharmaceuticalCareRouting
    FROM BaseSFD SFD
    JOIN dbo.ADINGRESO ING  ON SFD.NUMINGRES = ING.NUMINGRES
    JOIN Contract.CareGroup CG  ON CG.Id = ING.GENCAREGROUP
    JOIN Contract.HealthAdministrator HA  ON ING.GENCONENTITY = HA.Id
    JOIN dbo.IHLISTPRO LP  ON SFD.CODPRODUC = LP.CODPRODUC
    JOIN dbo.INPROFSAL PS  ON SFD.CODPROSAL = PS.CODPROSAL
    JOIN dbo.INESPECIA E  ON PS.CODESPEC1 = E.CODESPECI
    LEFT JOIN MedicalHistory.ProductSusceptibleMixingStation psm  ON psm.CodeSusceptibleMixingStation = SFD.CodeSusceptibleMixingStation
    LEFT JOIN dbo.INUNIMEDI UM3  ON UM3.CODUNIMED = SFD.CODUNIMED
    LEFT JOIN dbo.HCPRESCRA hc  ON SFD.SourceTable = 'HCPRESCRA' AND SFD.IdSourceTable = hc.ID
    LEFT JOIN HCPRESCRAEXT hcex  ON hcex.IDHCPRESCRA = hc.ID
    LEFT JOIN dbo.HCVIAADMI VIA  ON LP.CODVIAADM = VIA.CODVIAADM
    LEFT JOIN dbo.HCVIAADMI viaMed  ON viaMed.CODVIAADM = hc.CODVIAADM
    LEFT JOIN dbo.HCINFLIQA mescla  ON mescla.CONSECUTI = SFD.IdSourceTable AND SFD.SourceTable = 'HCINFLIQA'
    LEFT JOIN dbo.INUNIMEDI UM  ON hc.CODUNIMFN = UM.CODUNIMED
    LEFT JOIN dbo.INUNIMEDI UM2  ON SFD.CODUNIMED = UM2.CODUNIMED
    LEFT JOIN PM ON PM.NUMINGRES = SFD.NUMINGRES AND PM.CODPRODUC = SFD.CODPRODUC AND LP.NOPOSPROD = 1
    LEFT JOIN CantidadDispensada CD ON CD.NUMINGRES = SFD.NUMINGRES AND CD.CODPRODUC = SFD.CODPRODUC
    LEFT JOIN NursingPackages NPOS ON NPOS.IDHCFARMEPC = SFD.CODCONCEC AND NPOS.IDAGPAQUETES = SFD.IDAGPAQUETES AND NPOS.CODPRODUC = SFD.CODPRODUC
    LEFT JOIN dbo.AGPAQUETES paq  ON paq.ID = NPOS.IDAGPAQUETES
    CROSS APPLY
    (
        SELECT TOP (1)
            si.ChangeAfter,
            si.ChangeAfterTimeUnit,
            si.WithoutCurrentAuthorizationColor,
            si.WithoutAuthorizationManagementColor,
            si.DispensingWithoutAuthorization
        FROM Inventory.SettingInventory si 
        WHERE si.OperatingUnitId = CG.OperativeUnitId
    ) SIV
    OUTER APPLY
    (
        SELECT REPLACE(ISNULL(hc.DESADMINI, mescla.ADMMEZLIQ), 'continuamente', '') AS TextoAdministracion
    ) ADM_TXT
    OUTER APPLY
    (
        SELECT
            CASE
                WHEN CHARINDEX('vía', ADM_TXT.TextoAdministracion) > 0 THEN
                    SUBSTRING
                    (
                        ADM_TXT.TextoAdministracion,
                        CHARINDEX('vía', ADM_TXT.TextoAdministracion) + LEN('Via') + 1,
                        LEN(ADM_TXT.TextoAdministracion)
                    )
                ELSE ISNULL(viaMed.DESVIAADM, VIA.DESVIAADM)
            END AS ViaAdministracion
    ) ADM_VIA
    OUTER APPLY
    (
        SELECT TOP (1) pdn.Note
        FROM Inventory.ATC atc 
        JOIN Inventory.PharmaceuticalDispensingNotes pdn  ON pdn.ATCId = atc.ID
        WHERE atc.Code = SFD.CODPRODUC AND pdn.AdmissionNumber = SFD.NUMINGRES
        ORDER BY pdn.Id DESC
    ) PDN
    OUTER APPLY
    (
        SELECT TOP 1 CAST(1 AS BIT) AS Flag
        FROM MixingStation.MedicinesProduction  mpR
        JOIN Inventory.ATC                      atcR ON atcR.Id  = mpR.ATCId
        JOIN MixingStation.UnitDoseType         udtR ON udtR.Id  = mpR.UnitDoseTypeId
        WHERE atcR.Code             = SFD.CODPRODUC
          AND mpR.CenterAttentionId = SFD.CODCENATE
          AND udtR.MSClass          IN (3, 9, 10)
          AND SFD.IDETIPHIS         = 'ENFERMER1'
    ) PharmCareRoute
) AS t
CROSS APPLY
(
    SELECT t.CantidadAutorizada - t.CantidadDispensada AS SaldoAutorizadoMipres
) Saldo
OUTER APPLY
(
    SELECT TOP (1) SI.Color
    FROM Inventory.SettingInventoryPBSControl SI 
    WHERE Saldo.SaldoAutorizadoMipres BETWEEN SI.DoseFrom AND SI.DoseTo
    ORDER BY SI.DoseFrom DESC, SI.DoseTo DESC
) PBSControl
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Panel de detalle farmacéutico (dashboard de farmacia) que consolida toda la información operativa de cada ítem de dispensación o prescripción pendiente en la farmacia. Integra datos del ingreso del paciente (admisión/hospitalización/urgencias), la entidad pagadora (EPS/ARS) y su grupo de contratación, el profesional prescriptor y su especialidad, el producto farmacéutico o medicamento, las cantidades prescritas, autorizadas y dispensadas, el saldo pendiente de autorización MIPRES, el estado PBS/No PBS/Condicionado, la vía y frecuencia de administración, y alertas de color según control de inventario PBS. Combina información de prescripciones convencionales (HCFARMEPD, HCPRESCRA), órdenes de quimioterapia (HCORDMEDICAM), mezclas en estación de preparación (ProductSusceptibleMixingStation), catálogo de productos (IHLISTPRO), unidades de medida (INUNIMEDI), médico prescriptor (INPROFSAL), especialidades (INESPECIA), ingresos (ADINGRESO), entidades pagadoras (HealthAdministrator) y grupos de contrato (CareGroup). Se usa principalmente para el tablero operativo de farmacia, permitiendo al dispensador visualizar, gestionar y controlar en tiempo real cada solicitud de medicamento por paciente ingresado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewDashboardPharmacyDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewDashboardPharmacyDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de detalle para el dashboard de farmacia que consolida las solicitudes de dispensación pendientes con información del paciente, contrato, prescriptor, producto, dosis, autorizaciones PBS/No PBS y semáforo de alertas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existe configuración en Inventory.SettingInventory para la unidad operativa del CareGroup del ingreso (CROSS APPLY exige al menos un registro).; El ingreso tiene CareGroup y HealthAdministrator válidos (JOIN obligatorio).; El producto existe en IHLISTPRO y el profesional en INPROFSAL con su especialidad en INESPECIA.; Solo se consideran pedidos con cantidad pendiente mayor a cero y sin asociación a programación quirúrgica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen pedidos de farmacia con saldo pendiente (>0) y no asociados a agenda quirúrgica.; El saldo autorizado Mipres se calcula siempre como CantidadAutorizada - CantidadDispensada.; La cantidad autorizada y los códigos Mipres provienen exclusivamente de HCJUNOPOM filtrando CODMINSALUD no vacío y solo se aplican a productos No PBS (LP.NOPOSPROD=1).; El color de alerta PBS solo aplica a productos No PBS; productos PBS siempre devuelven cadena vacía.; La unidad de frecuencia se traduce a minutos/horas/días según códigos ''1'',''2'',''3''.; El destino de ruteo por defecto es ''Servicio Farmaceutico'' cuando SENDTO es nulo.; La nota mostrada es la última (Id DESC) registrada en PharmaceuticalDispensingNotes para el ingreso y ATC del producto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dispensación farmacéutica; Prescripción médica; Autorización Mipres; PBS / No PBS; Paquete de enfermería; Central de mezclas; Vía de administración; Dosis y frecuencia; Contrato y plan de salud; Especialidad médica; Medicación en custodia; Atención extramural', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ViewDashboardPharmacyDetail: Solo se retornan filas donde HCFARMEPD.CANPENPRO > 0 AND HCFARMEPD.IDAGEPROGQX IS NULL (pendientes y no ligadas a programación quirúrgica).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si NOPOSPROD = 1 (producto No PBS) y existe rango en SettingInventoryPBSControl que cubra (CantidadAutorizada - CantidadDispensada) → ColorAlertaPBS = color configurado por rango de dosis else Si no hay rango: si DATEADD sobre FECINIDOS con ChangeAfter aún es futuro respecto a Common.GETDATE() usa WithoutCurrentAuthorizationColor; en caso contrario WithoutAuthorizationManagementColor. Si NOPOSPROD <> 1 el color queda vacío.; si hc.DURACIDOS = ''Tratamiento Continuo'' → DosisPrescrita = -1 else Si ''Dosis Unica'' = 1; en otro caso = CANT_AUTORIZADA agregada de HCJUNOPOM.; si SFD.Conditioned = 1 → PBS = ''Condicionado'' else Si LP.NOPOSPROD = 1 → ''No''; en otro caso ''Si''.; si npos.IDHCFARMEPC IS NULL (sin paquete de enfermería asociado) → Tipo = SFD.TIPOREGIS else Tipo = ''5'' (marca como tipo paquete de enfermería).; si psm.Id IS NOT NULL (producto susceptible de mezcla) → TotalDose = dosisprod * ApplicationsNumber else Si SourceTable=''HCPRESCRA'' → NUMEROAPLICACIONINICIAL * DOSISPROD; en otro caso DOSISPROD.; si SFD.SENDTO IN (0,1,2) → RoutedTo = ''Pendiente'' / ''Servicio Farmaceutico'' / ''Central de Mezclas'' respectivamente; default 1 si NULL.; si FORMAPRESCRIBE IS NOT NULL → ADMINISTRACION = DESADMINI else Se construye descripción de administración según hc.DOSISPRFN, hc.DURACIDOS (''Dosis Unica'' o tratamiento con frecuencia) concatenando dosis, unidad, frecuencia (min/Hora/Día) y vía extraída del texto o de HCVIAADMI.; si SFD.UNIRS = 1 → UNIRS = ''Si'' else UNIRS = ''No''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.fnConvertirAMinutos; dbo.fnCalcularCantidadDispensada; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFARMEPD; dbo.ADINGRESO; Contract.CareGroup; Contract.HealthAdministrator; dbo.IHLISTPRO; dbo.INPROFSAL; dbo.INESPECIA; MedicalHistory.ProductSusceptibleMixingStation; dbo.INUNIMEDI; EHR.HCORDMEDICAM; dbo.HCPRESCRA; dbo.HCPRESCRAEXT; dbo.HCVIAADMI; dbo.HCINFLIQA; dbo.HCJUNOPOM; MedicalHistory.NursingPackagesOrder; MedicalHistory.NursingPackagesOrderDetail; dbo.AGPAQUETES; Inventory.SettingInventory; Inventory.SettingInventoryPBSControl; Inventory.PharmaceuticalDispensingNotes; Inventory.ATC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetail';
GO
