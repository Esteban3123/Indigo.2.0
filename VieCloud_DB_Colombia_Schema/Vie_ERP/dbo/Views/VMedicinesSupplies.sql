

CREATE VIEW [dbo].[VMedicinesSupplies]
AS
WITH phy AS (
    -- Conjunto de procesos (TIPREGIST) por producto/ingreso/paciente
    SELECT DISTINCT
        p.CODPRODUC,
        p.IPCODPACI,
        p.NUMINGRES,
        p.TIPREGIST
    FROM (
        -- HCFISIPRO directo
        SELECT  phy.CODPRODUC, phy.IPCODPACI, phy.NUMINGRES, phy.TIPREGIST
        FROM dbo.HCFISIPRO phy WITH (NOLOCK)

        UNION ALL
        -- Recién nacido: NUMINGRES del hijo → mapeado al ingreso del padre
        SELECT  phy.CODPRODUC, phy.IPCODPACI, irn.NUMINGRES, phy.TIPREGIST
        FROM dbo.HCFISIPRO phy WITH (NOLOCK)
        JOIN dbo.HCINGRESORECNAC irn WITH (NOLOCK) ON phy.NUMINGRES = irn.NUMINGRESHIJO
        JOIN dbo.ADINGRESO ing                    ON phy.NUMINGRES = irn.NUMINGRESHIJO
        WHERE ing.IESTADOIN = 'C'

        UNION ALL
        -- Qx sin HCFISIPRO → usa TIPREGIST = 0
        SELECT hd.CODPRODUC, hc.IPCODPACI, hc.NUMINGRES, 0 AS TIPREGIST
        FROM dbo.HCHOJAGASTOQXD hd WITH (NOLOCK)
        JOIN dbo.HCHOJAGASTOQX  hc WITH (NOLOCK) ON hd.IDHCHOJAGASTOQX = hc.Id
    ) p
	WHERE p.TIPREGIST = 0 --Tipo de registro: Farmacia
),
kAgg AS (
    -- KARDEX agregado por producto/ingreso
    SELECT
        k.NUMINGRES,
        k.CODPRODUC,
        SUM(CASE WHEN k.TIPORIREG BETWEEN  1 AND  3 THEN k.CANPRODUCT ELSE 0 END) AS Entregada,
        SUM(CASE WHEN k.TIPORIREG BETWEEN 11 AND 15 THEN k.CANPRODUCT ELSE 0 END) AS Aplicada,
        SUM(CASE WHEN k.TIPORIREG = 18 THEN k.CANPRODUCT ELSE 0 END)              AS Devolutivo,
        SUM(CASE WHEN k.TIPORIREG BETWEEN  1 AND  3 THEN k.CANPRODUCT ELSE 0 END)
      - SUM(CASE WHEN k.TIPORIREG BETWEEN 11 AND 15 THEN k.CANPRODUCT ELSE 0 END)
      + SUM(CASE WHEN k.TIPORIREG = 5  THEN k.CANPRODUCT ELSE 0 END)
      - SUM(CASE WHEN k.TIPORIREG = 18 THEN k.CANPRODUCT ELSE 0 END)              AS Fisico
    FROM dbo.HCKARDPAC k WITH (NOLOCK)
	WHERE k.IdDetailPhysicalCUM IS NULL
    GROUP BY k.NUMINGRES, k.CODPRODUC
)

SELECT
    ip.CODPRODUC                                 AS Codigo,
    RTRIM(ip.DESPRODUC)                          AS Producto,
    p.IPCODPACI                                  AS CodigoPaciente,
    p.NUMINGRES                                  AS Ingreso,
    ISNULL(k.Entregada,  0)                      AS Entregada,
    ISNULL(k.Aplicada,   0)                      AS Aplicada,
    ISNULL(k.Devolutivo, 0)                      AS Devolutivo,
    ISNULL(k.Fisico,     0)                      AS Fisico,
    0                                            AS Alerta,
    CAST(0 AS BIT)								 AS ClaseItemProduccion
FROM dbo.IHLISTPRO ip       WITH (NOLOCK)
JOIN phy p                   ON p.CODPRODUC = ip.CODPRODUC
LEFT JOIN kAgg k             ON k.NUMINGRES = p.NUMINGRES
                             AND k.CODPRODUC = p.CODPRODUC;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el inventario de medicamentos e insumos dispensados por paciente e ingreso hospitalario, cruzando múltiples fuentes: los procesos físicos de farmacia (HCFISIPRO), la hoja de gastos quirúrgicos (HCHOJAGASTOQXD/HCHOJAGASTOQX) y el kárdex de movimientos farmacéuticos por paciente (HCKARDPAC). Incluye un tratamiento especial para recién nacidos, mapeando el ingreso del hijo al ingreso de la madre cuando el estado del ingreso es cerrado. Para cada combinación de producto e ingreso expone las cantidades entregadas desde farmacia, aplicadas al paciente, devueltas y el saldo físico disponible, permitiendo conciliar la dispensación versus la administración real de medicamentos e insumos durante la hospitalización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VMedicinesSupplies';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VMedicinesSupplies';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida por paciente/ingreso los productos farmacéuticos consumidos (vía historia clínica, recién nacido o quirófano) cruzándolos con el kárdex para reportar cantidades entregadas, aplicadas, devolutivos y saldo físico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VMedicinesSupplies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los movimientos del kárdex relevantes son los que no tienen IdDetailPhysicalCUM (NULL); Para mapear ingresos de recién nacido a ingreso del padre, el ingreso del hijo en ADINGRESO debe estar en estado ''C''; Solo se incluyen registros cuyo tipo de registro corresponde a Farmacia (TIPREGIST = 0)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VMedicinesSupplies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran consumos clasificados como Farmacia (TIPREGIST = 0); Solo se agregan movimientos de kárdex con IdDetailPhysicalCUM NULL; Físico = Entregada - Aplicada + (movimientos TIPORIREG=5) - Devolutivo; Para recién nacidos solo se reasigna el ingreso si el ingreso está confirmado/cerrado (IESTADOIN=''C''); Las cantidades nulas se reportan como cero; Alerta y ClaseItemProduccion siempre son 0/false en esta vista', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VMedicinesSupplies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Recién nacido; Hoja de gastos de quirófano; Kárdex de medicamentos; Farmacia; Producto/medicamento; Cantidad entregada; Cantidad aplicada; Devolutivo; Saldo físico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VMedicinesSupplies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] VMedicinesSupplies: Devuelve una fila por producto/paciente/ingreso con cantidades agregadas; si no hay movimientos en kárdex se retorna 0 vía ISNULL en Entregada/Aplicada/Devolutivo/Fisico; [RETURN_RESULT] VMedicinesSupplies: Las columnas Alerta y ClaseItemProduccion se retornan siempre como 0 / BIT 0 (constantes)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VMedicinesSupplies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen del consumo: HCFISIPRO directo → Toma CODPRODUC, IPCODPACI, NUMINGRES y TIPREGIST tal cual desde HCFISIPRO; si El ingreso pertenece a un recién nacido (existe en HCINGRESORECNAC.NUMINGRESHIJO) y el ingreso está en estado ''C'' → Sustituye el NUMINGRES por el del hijo proveniente de HCINGRESORECNAC; si El consumo proviene de hoja de gastos de quirófano (HCHOJAGASTOQXD/HCHOJAGASTOQX) sin registro en HCFISIPRO → Asigna TIPREGIST = 0 (Farmacia) por defecto; si TIPORIREG entre 1 y 3 → Suma la cantidad como Entregada y como componente positivo del Físico; si TIPORIREG entre 11 y 15 → Suma la cantidad como Aplicada y la resta del Físico; si TIPORIREG = 5 → Suma la cantidad al Físico (sin clasificarla en Entregada/Aplicada/Devolutivo); si TIPORIREG = 18 → Suma la cantidad como Devolutivo y la resta del Físico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VMedicinesSupplies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFISIPRO; dbo.HCINGRESORECNAC; dbo.ADINGRESO; dbo.HCHOJAGASTOQXD; dbo.HCHOJAGASTOQX; dbo.HCKARDPAC; dbo.IHLISTPRO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VMedicinesSupplies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VMedicinesSupplies';
GO
