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