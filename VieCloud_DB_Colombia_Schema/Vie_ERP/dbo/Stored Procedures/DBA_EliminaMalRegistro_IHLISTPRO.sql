CREATE PROCEDURE [dbo].[DBA_EliminaMalRegistro_IHLISTPRO] @strCodProduc_OLD VARCHAR(20), 
                                                         @strCodProduc_NEW VARCHAR(20)
AS
    BEGIN
        SET NOCOUNT ON;
        UPDATE dbo.HCFARMEPD
          SET 
              CODPRODUC = @strCodProduc_NEW
        WHERE CODPRODUC = @strCodProduc_OLD;
        -----
        UPDATE dbo.HCPRESCRA
          SET 
              CODPRODUC = @strCodProduc_NEW
        WHERE CODPRODUC = @strCodProduc_OLD;
        -----
        UPDATE dbo.HCPRESCRD
          SET 
              CODPRODUC = @strCodProduc_NEW
        WHERE CODPRODUC = @strCodProduc_OLD;
        -----
        UPDATE dbo.HCSOLINSD
          SET 
              CODPRODUC = @strCodProduc_NEW
        WHERE CODPRODUC = @strCodProduc_OLD;
        -----
        UPDATE dbo.HCFISIPRO
          SET 
              CODPRODUC = @strCodProduc_NEW
        WHERE CODPRODUC = @strCodProduc_OLD;
        DELETE FROM dbo.IHLISTPRO
        WHERE CODPRODUC = @strCodProduc_OLD;
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de corrección y fusión de productos farmacéuticos duplicados o mal registrados en el catálogo maestro (IHLISTPRO). Reasigna el código de producto incorrecto (OLD) por el código correcto (NEW) en todas las tablas clínicas donde ese producto pueda estar referenciado: dispensación de farmacia (HCFARMEPD), prescripciones de medicamentos cabecera y detalle (HCPRESCRA, HCPRESCRD), solicitudes de insumos (HCSOLINSD) y fisioterapia/procedimientos (HCFISIPRO). Una vez actualizadas todas las referencias, elimina el registro duplicado o erróneo del catálogo maestro de productos. Se usa cuando se detecta un medicamento o insumo cargado con un código equivocado que ya tiene movimientos clínicos asociados, permitiendo consolidar todo el historial bajo el código correcto sin perder trazabilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'DBA_EliminaMalRegistro_IHLISTPRO';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'DBA_EliminaMalRegistro_IHLISTPRO';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'"Reasigna en tablas operativas las referencias de un código de producto antiguo (mal registrado) a uno nuevo y elimina el registro erróneo del catálogo maestro de productos."', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'DBA_EliminaMalRegistro_IHLISTPRO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El código de producto nuevo debe existir o ser válido en el catálogo destino para no romper integridad referencial.; El código de producto antiguo debe corresponder a un registro mal creado en el catálogo maestro de productos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'DBA_EliminaMalRegistro_IHLISTPRO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todas las referencias al código de producto antiguo se reemplazan por el nuevo en las tablas dependientes antes de eliminar el registro maestro.; Ninguna tabla dependiente queda con el código antiguo tras la ejecución.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'DBA_EliminaMalRegistro_IHLISTPRO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Producto farmacéutico; Prescripción; Farmacia; Fisiatría; Solicitud de insumos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'DBA_EliminaMalRegistro_IHLISTPRO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] dbo.HCFARMEPD: Donde CODPRODUC coincide con el código antiguo, se reemplaza por el código nuevo.; [UPDATE] dbo.HCPRESCRA: Donde CODPRODUC coincide con el código antiguo, se reemplaza por el código nuevo en prescripciones (cabecera).; [UPDATE] dbo.HCPRESCRD: Donde CODPRODUC coincide con el código antiguo, se reemplaza por el código nuevo en prescripciones (detalle).; [UPDATE] dbo.HCSOLINSD: Donde CODPRODUC coincide con el código antiguo, se reemplaza por el código nuevo en el detalle de solicitudes de insumos.; [UPDATE] dbo.HCFISIPRO: Donde CODPRODUC coincide con el código antiguo, se reemplaza por el código nuevo en productos de fisiatría.; [DELETE] dbo.IHLISTPRO: Tras reasignar las referencias, se elimina del catálogo maestro el registro cuyo CODPRODUC equivale al código antiguo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'DBA_EliminaMalRegistro_IHLISTPRO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'DBA_EliminaMalRegistro_IHLISTPRO';
-- GO
