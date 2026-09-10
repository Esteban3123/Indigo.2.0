

CREATE VIEW [dbo].[ViewDashboardPharmacyDetailMixingSation]
AS
WITH BaseSFD AS (
    SELECT
        SFD.ID,
        SFD.CODCONCEC,
        SFD.NUMINGRES,
        SFD.NUMEFOLIO,
        SFD.IPCODPACI,
        SFD.CODPROSAL,
        SFD.CODPRODUC,
        SFD.TIPOREGIS,
        SFD.CANPEDPRO,
        SFD.CANPENPRO,
        SFD.NOPOSPROD,
        SFD.Conditioned,
        SFD.UNIRS,
        SFD.CODUNIMED,
        SFD.IDETIPHIS,
        SFD.PROESTADO,
        SFD.IDAGEPROGQX,
        SFD.EXTRAMURAL,
        SFD.MEDICACUSTODIA,
        SFD.SENDTO,
        SFD.CodeSusceptibleMixingStation,
        SFD.IdSourceTable,
        SFD.SourceTable
    FROM dbo.HCFARMEPD AS SFD
    WHERE SFD.CANPENPRO > 0 AND SFD.IDAGEPROGQX IS NULL AND SFD.SENDTO IN (0, 2)
)
SELECT
    CAST(NEWID()as varchar(50)) as Id,
    CAST('' AS INT) AS FilaSeleccionada,
    '0' AS Opcion,
    '0' AS OpcionAnulado,
    '' AS Procedimiento,
    0 AS MarcarOpcion,
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
    RTRIM(LP.DESPRODUC) AS Producto,
    RTRIM(SFD.CODPRODUC) AS CodProducto,
    SFD.TIPOREGIS AS Tipo,
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
    CONVERT(bit, ISNULL(SFD.EXTRAMURAL, 0)) AS EXTRAMURAL,
    ISNULL(SFD.MEDICACUSTODIA, CAST(0 AS BIT)) AS Custodia,
    ADM.ADMINISTRACION,
    SFD.ID AS EntityId,
    'HCFARMEPD' AS EntityName,
    CASE SFD.SENDTO
        WHEN 0 THEN 'Pendiente'
        WHEN 1 THEN 'Servicio Farmaceutico'
        WHEN 2 THEN 'Central de Mezclas'
    END AS RoutedTo,
    SFD.SENDTO,
    CAST(SFD.CodeSusceptibleMixingStation AS VARCHAR(36)) AS CodeSusceptibleMixingStation,
    HCP.FinishedProductCode AS FinishedProductCodeNPT
FROM BaseSFD AS SFD
JOIN dbo.ADINGRESO AS ING ON ING.NUMINGRES = SFD.NUMINGRES
JOIN Contract.CareGroup AS CG ON CG.Id = ING.GENCAREGROUP
JOIN Contract.HealthAdministrator AS HA ON HA.Id = ING.GENCONENTITY
JOIN dbo.IHLISTPRO AS LP ON LP.CODPRODUC = SFD.CODPRODUC
JOIN dbo.INPROFSAL AS PS ON PS.CODPROSAL = SFD.CODPROSAL
JOIN dbo.INESPECIA AS E ON E.CODESPECI = PS.CODESPEC1
LEFT JOIN dbo.HCNUTPAREC AS HCT ON SFD.SourceTable = 'HCNUTPAREC' AND HCT.ID = SFD.IdSourceTable
LEFT JOIN dbo.HCPARNUTC AS HCP ON HCP.ID = HCT.IDHCPARNUTC
LEFT JOIN dbo.HCVIAADMI AS VIA ON VIA.CODVIAADM = LP.CODVIAADM
LEFT JOIN dbo.HCPRESCRA AS HC ON HC.NUMINGRES = SFD.NUMINGRES AND HC.NUMEFOLIO = SFD.NUMEFOLIO AND HC.IPCODPACI = SFD.IPCODPACI AND HC.CODPRODUC = SFD.CODPRODUC AND HC.MANEXTPRO = '0'
LEFT JOIN dbo.INUNIMEDI AS UM ON UM.CODUNIMED = HC.CODUNIMFN
OUTER APPLY (
    SELECT
        RTRIM(
            CASE
                WHEN HC.FORMAPRESCRIBE IS NOT NULL THEN HC.DESADMINI
                ELSE
                    CASE
                        WHEN HC.DOSISPRFN IS NULL THEN
                            CASE
                                WHEN HC.DESADMINI IS NULL THEN 'No aplica.'
                                ELSE HC.DESADMINI
                            END
                        WHEN HC.DURACIDOS = 'Dosis Unica' THEN
                            RTRIM(CAST(HC.DOSISPRFN AS CHAR)) + ' ' +
                            RTRIM(CAST(UM.ABRUNIMED AS CHAR)) +
                            ' Dosis Única Via: ' +
                            RTRIM(VIA.DESVIAADM)
                        ELSE
                            RTRIM(CAST(HC.DOSISPRFN AS CHAR)) + ' ' +
                            RTRIM(CAST(UM.ABRUNIMED AS CHAR)) +
                            ' Cada ' +
                            RTRIM(CAST(HC.FRECUENCI AS CHAR)) +
                            CASE HC.UNIFRECUE
                                WHEN '1' THEN ' min(s) '
                                WHEN '2' THEN ' Hora(s) '
                                WHEN '3' THEN ' Dia(s) '
                            END +
                            'Vía: ' +
                            RTRIM(VIA.DESVIAADM)
                    END
            END
        ) AS ADMINISTRACION
) AS ADM;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Panel de control (dashboard) de detalle de la estación de mezclas del servicio farmacéutico. Muestra los medicamentos prescritos con cantidad pendiente de entrega que están enrutados hacia la central de mezclas o en estado pendiente, excluyendo los asociados a programación quirúrgica. Integra información del episodio de ingreso del paciente (admisión, entidad pagadora, plan/contrato), el producto farmacéutico (nombre, código, si es PBS o No PBS, vía de administración), el médico prescriptor con su especialidad, y la pauta de administración calculada (dosis, frecuencia, vía). Adicionalmente vincula componentes de nutrición parenteral (NPT) cuando el medicamento proviene de una preparación nutricional, permitiendo identificar el código del producto terminado de la estación de mezclas; esta vista es la fuente principal para el seguimiento y despacho de preparaciones magistrales y mezclas intravenosas en el módulo de farmacia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewDashboardPharmacyDetailMixingSation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewDashboardPharmacyDetailMixingSation';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las prescripciones farmacéuticas pendientes de despacho que deben ser preparadas en la central de mezclas o servicio farmacéutico, consolidando datos clínicos, administrativos y de administración para su seguimiento.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetailMixingSation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de ingreso (ADINGRESO), entidad/contrato (HealthAdministrator/CareGroup), producto en catálogo (IHLISTPRO), profesional (INPROFSAL) y su especialidad (INESPECIA) referenciados por la prescripción farmacéutica.; El pedido farmacéutico debe tener cantidad pendiente mayor a cero.; El pedido no debe estar asociado a una programación quirúrgica (IDAGEPROGQX nulo).; El destino de envío (SENDTO) debe ser 0 (Pendiente) o 2 (Central de Mezclas).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetailMixingSation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca se exponen pedidos totalmente despachados (CANPENPRO=0).; Nunca se exponen pedidos vinculados a programación quirúrgica.; Sólo se incluyen ítems con destino pendiente o central de mezclas; los enviados a servicio farmacéutico quedan excluidos.; Cada fila se identifica con un GUID nuevo generado en tiempo de consulta.; El join con HCPRESCRA se restringe a prescripciones no extemporáneas (MANEXTPRO=''0'').', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetailMixingSation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Central de Mezclas; Servicio Farmacéutico; Prescripción de medicamentos; Pedido farmacéutico; Cantidad pendiente de despacho; Plan Básico de Salud (PBS/NO PBS); Producto condicionado; UNIRS; Vía de administración; Dosis y frecuencia; Nutrición parenteral total (NPT); Medicación en custodia; Atención extramural; Ingreso del paciente; Entidad administradora de salud / contrato / plan; Especialidad médica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetailMixingSation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFARMEPD: Devuelve sólo pedidos con CANPENPRO > 0, IDAGEPROGQX IS NULL y SENDTO IN (0,2), excluyendo los enviados al servicio farmacéutico (SENDTO=1) y los ya completamente despachados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetailMixingSation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si SFD.Conditioned = 1 → Clasifica el producto como ''Condicionado'' en el indicador PBS. else Si LP.NOPOSPROD=1 marca ''No'', en otro caso ''Si''.; si SFD.UNIRS = 1 → Marca el ítem como perteneciente a UNIRS (''Si''). else Marca ''No''.; si FORMAPRESCRIBE IS NOT NULL → Usa DESADMINI como texto de administración. else Construye la indicación a partir de la prescripción HCPRESCRA: si DOSISPRFN es nula muestra DESADMINI o ''No aplica.''; si DURACIDOS=''Dosis Unica'' arma ''dosis + unidad + Dosis Única Vía''; si no, arma ''dosis + unidad + Cada frecuencia + min/Hora/Día (según UNIFRECUE 1/2/3) + Vía''.; si SFD.SENDTO → Traduce 0→''Pendiente'', 1→''Servicio Farmaceutico'', 2→''Central de Mezclas'' en el campo RoutedTo.; si sfd.SourceTable = ''HCNUTPAREC'' → Vincula la prescripción con la receta de nutrición parenteral (HCNUTPAREC) y obtiene el código del producto terminado NPT desde HCPARNUTC.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetailMixingSation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFARMEPD; dbo.ADINGRESO; Contract.CareGroup; Contract.HealthAdministrator; dbo.IHLISTPRO; dbo.INPROFSAL; dbo.INESPECIA; dbo.HCNUTPAREC; dbo.HCPARNUTC; dbo.HCVIAADMI; dbo.HCPRESCRA; dbo.INUNIMEDI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetailMixingSation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetailMixingSation';
GO
