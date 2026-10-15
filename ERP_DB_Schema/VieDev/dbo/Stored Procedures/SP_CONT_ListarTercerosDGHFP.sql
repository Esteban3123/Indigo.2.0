
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