
-- =============================================  
-- Author:  <Author,,Name>  
-- ALTER date: <ALTER Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[SP_CONT_ListarTercerosDGHFP]
AS
    BEGIN  
        -- SET NOCOUNT ON added to prevent extra result sets from  
        -- interfering with SELECT statements.  
        SET NOCOUNT ON;

        -- Insert statements for procedure here  
        --SELECT TERCODTER AS Codigo,  
        --       CASE TERTIPIDE WHEN '1' THEN 'Cedula Ciudadania' WHEN '2' THEN 'Cedula Extranjeria'   
        --       WHEN '3' THEN 'Tarjeta Identidad' WHEN '4' THEN 'Registro Civil' WHEN '5' THEN 'Pasaporte' WHEN '6' THEN 'Adulto sin Identificacion' WHEN '7' THEN 'Menor sin Identificacion'   
        --       WHEN '8' THEN 'Numero Unico de Identificacion' WHEN '9' THEN 'Nit' END AS TipoDocumento,TERNOMTER AS Descripcion,RTRIM(TERCODTER)+' - '+RTRIM(TERNOMTER) AS Nombre,  
        --       CASE TERTIPCON WHEN  '1' THEN 'Regimen Comun' WHEN '2' THEN 'Regimen Simplificado' WHEN '3' THEN 'Gran Contribuyente' WHEN '4' THEN 'Empresa Estatal' END AS TipoContribuyente,  
        --       CASE TERTIPRET WHEN '0' THEN 'Ninguno' WHEN '1' THEN 'Exento Retencion' WHEN '2' THEN 'Hacer Retencion' WHEN '3' THEN 'Autoretenedor' END AS TipoRetencion,TERTELEF2 AS  Telefono  
        --FROM DBO.GETERCER   
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento destinado a listar terceros registrados en el sistema contable (tabla GETERCER), incluyendo información como código del tercero, tipo y número de documento de identidad (cédula de ciudadanía, NIT, pasaporte, entre otros), nombre o razón social, tipo de contribuyente (régimen común, simplificado, gran contribuyente, empresa estatal) y tipo de retención aplicable. Actualmente el cuerpo principal del procedimiento está comentado y no retorna datos, por lo que se encuentra inactivo o en proceso de revisión. Su propósito de negocio es servir como fuente de consulta de terceros para procesos contables, de facturación, retenciones y reportes financieros.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_CONT_ListarTercerosDGHFP';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_CONT_ListarTercerosDGHFP';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Procedimiento sin lógica activa; su cuerpo está completamente comentado y no produce resultados ni efectos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CONT_ListarTercerosDGHFP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'No retorna resultset ni modifica datos; solo aplica SET NOCOUNT ON.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CONT_ListarTercerosDGHFP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CONT_ListarTercerosDGHFP';
-- GO
