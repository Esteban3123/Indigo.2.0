-- =============================================
-- Modificated:     Miguel Angel Ruiz Vega - DBA
-- DateModificated: 2026-07-17
-- Description:     Optimización de SP_HC_ListarProductosInsumos
--   * Eliminado RTRIM() de las condiciones JOIN -> restaura Index Seek (fix raíz de sargabilidad)
--   * Eliminado WITH(NOLOCK) -> RCSI activo (redundante/riesgoso)
--   * UNION -> UNION ALL (ramas disjuntas, evita Distinct Sort)
--   * Filtro de almacén en el ON del LEFT JOIN -> CONSERVA productos con stock 0 (idéntico al original)
--   * PK en @Almacenes + dedupe de inserts
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarProductosInsumos]
    @Almacen        CHAR(4),
    @OidAlmacen     INT,
    @VersionERP     INT,
    @CentroAtencion CHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    -- Normalización de parámetros
    DECLARE @AlmacenAux VARCHAR(4) = RTRIM(@Almacen);
    IF @AlmacenAux IS NOT NULL AND LEN(@AlmacenAux) = 0
        SET @AlmacenAux = NULL;

    -- ============================ VERSION ERP 1 ============================
    IF @VersionERP = 1
    BEGIN
        SELECT
            RTRIM(A.CODPRODUC) AS Codigo,
            RTRIM(A.DESPRODUC) AS Descripcion,
            SUM(CAST(C.IFICANTID AS INT)) AS Disponibles,
            RTRIM(A.CODPRODUC) + ' - ' + RTRIM(A.DESPRODUC) AS CodigoDescripcion,
            A.JUSINMEDI AS ExigeJustificacion
        FROM dbo.IHLISTPRO A
        INNER JOIN dbo.IHRINDDGH B
            ON A.CODPRODUC = B.CODPRODUC AND A.PROESTADO = 1
        INNER JOIN dbo.INFISICO C
            ON B.IPRCODIGO = C.IPRCODIGO
        WHERE A.TIPPRODUC IN ('2','3')
            AND (@AlmacenAux IS NULL OR C.IALCODIGO = @AlmacenAux)
        GROUP BY RTRIM(A.CODPRODUC), RTRIM(A.DESPRODUC), A.JUSINMEDI;
    END

    -- ============================ VERSION ERP 2 ============================
    ELSE IF @VersionERP = 2
    BEGIN
        SELECT
            RTRIM(A.CODPRODUC) AS Codigo,
            RTRIM(A.DESPRODUC) AS Descripcion,
            SUM(CAST(C.IFICANTID AS INT)) AS Disponibles,
            RTRIM(A.CODPRODUC) + ' - ' + RTRIM(A.DESPRODUC) AS CodigoDescripcion,
            A.JUSINMEDI AS ExigeJustificacion
        FROM dbo.IHLISTPRO A
        INNER JOIN dbo.IHRINDDGH B
            ON A.CODPRODUC = B.CODPRODUC
        INNER JOIN dbo.INNPRODUC P
            ON P.IPRCODIGO = B.IPRCODIGO
        LEFT JOIN dbo.INNFISICO C
            ON P.OID = C.INNPRODUC
        LEFT JOIN dbo.INNALMACE D
            ON C.INNALMACE = D.OID
        WHERE A.PROESTADO = 1
            AND (A.TIPPRODUC = '2' OR A.ESPDILPRO = '1')
        GROUP BY RTRIM(A.CODPRODUC), RTRIM(A.DESPRODUC), A.JUSINMEDI;
    END

    -- ==================== VERSION ERP 3 (por defecto) =====================
    ELSE
    BEGIN
        -- Preparar tabla de almacenes una sola vez
        DECLARE @Almacenes AS TABLE (Id INT PRIMARY KEY);

        IF @CentroAtencion IS NOT NULL AND LEN(RTRIM(@CentroAtencion)) > 0
            INSERT INTO @Almacenes (Id)
            SELECT Id FROM Inventory.Warehouse
            WHERE CodeCenterAttention = @CentroAtencion;

        INSERT INTO @Almacenes (Id)
        SELECT Id FROM Inventory.Warehouse
        WHERE CodeCenterAttention IS NULL
            AND Id NOT IN (SELECT Id FROM @Almacenes);

        -- --------- PRODUCTOS (TIPPRODUC='3' o ESPDILPRO=1) ---------
        SELECT
            RTRIM(D.CODPRODUC) AS Codigo,
            RTRIM(D.DESPRODUC) AS Descripcion,
            RTRIM(D.CODPRODUC) + ' - ' + RTRIM(D.DESPRODUC) AS CodigoDescripcion,
            SUM(ISNULL(phy.Quantity, 0)) AS Disponibles,
            D.JUSINMEDI AS ExigeJustificacion
        FROM dbo.IHLISTPRO D
        INNER JOIN Inventory.ATC atc
            ON D.CODPRODUC = atc.Code                          -- sin RTRIM -> Index Seek
        LEFT JOIN Inventory.InventoryProduct invpro
            ON invpro.ATCId = atc.Id AND invpro.Status = 1
        LEFT JOIN Inventory.PhysicalInventory phy
            ON phy.ProductId = invpro.Id
           AND phy.WarehouseId IN (SELECT Id FROM @Almacenes)  -- filtro en el ON -> conserva stock 0
        WHERE D.PROESTADO = 1
            AND (D.TIPPRODUC = '3' OR D.ESPDILPRO = 1)
        GROUP BY RTRIM(D.CODPRODUC), RTRIM(D.DESPRODUC), D.JUSINMEDI

        UNION ALL

        -- --------- INSUMOS (TIPPRODUC='2') ---------
        SELECT
            RTRIM(D.CODPRODUC) AS Codigo,
            RTRIM(D.DESPRODUC) AS Descripcion,
            RTRIM(D.CODPRODUC) + ' - ' + RTRIM(D.DESPRODUC) AS CodigoDescripcion,
            SUM(ISNULL(phy.Quantity, 0)) AS Disponibles,
            D.JUSINMEDI AS ExigeJustificacion
        FROM dbo.IHLISTPRO D
        INNER JOIN Inventory.InventorySupplie s
            ON D.CODPRODUC = s.Code                            -- sin RTRIM -> Index Seek
        LEFT JOIN Inventory.InventoryProduct invpro
            ON invpro.SupplieId = s.Id AND invpro.Status = 1
        LEFT JOIN Inventory.PhysicalInventory phy
            ON phy.ProductId = invpro.Id
           AND phy.WarehouseId IN (SELECT Id FROM @Almacenes)  -- filtro en el ON -> conserva stock 0
        WHERE D.PROESTADO = 1
            AND D.TIPPRODUC = '2'
        GROUP BY RTRIM(D.CODPRODUC), RTRIM(D.DESPRODUC), D.JUSINMEDI;
    END
END
GO
