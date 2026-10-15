

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