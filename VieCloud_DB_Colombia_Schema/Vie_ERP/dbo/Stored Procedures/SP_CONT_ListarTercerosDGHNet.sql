
-- =============================================  
-- Author:  <Author,,Name>  
-- ALTER date: <ALTER Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[SP_CONT_ListarTercerosDGHNet](@Estado BIT)
AS
    BEGIN  
        -- SET NOCOUNT ON added to prevent extra result sets from  
        -- interfering with SELECT statements.  
        SET NOCOUNT ON;

        -- Insert statements for procedure here  
        --SELECT TERNUMDOC AS Codigo,TERPRINOM + ' ' + TERSEGNOM + ' ' + TERPRIAPE + ' ' + TERSEGAPE AS Descripcion,  
        --  TERNUMDOC + ' - ' + TERPRINOM + ' ' + TERSEGNOM + ' ' + TERPRIAPE + ' ' + TERSEGAPE AS Nombre,  
        --       CASE TERTIPDOC WHEN '1' THEN 'Cedula Ciudadania' WHEN '2' THEN 'Cedula Extranjeria'   
        --       WHEN '3' THEN 'Tarjeta Identidad' WHEN '4' THEN 'Registro Civil' WHEN '5' THEN 'Pasaporte' WHEN '6' THEN 'Adulto sin Identificacion' WHEN '7' THEN 'Menor sin Identificacion'   
        --       WHEN '8' THEN 'Numero Unico de Identificacion' WHEN '9' THEN 'Nit' END AS TipoDocumento,  
        --       CASE TERTIPCON WHEN '0' THEN 'Regimen Simplificado' WHEN '1' THEN 'Regimen Comun' WHEN '2' THEN 'Regimen Contributivo' WHEN '3' THEN 'Gran Contribuyente' WHEN '4' THEN 'Empresa Estatal' END AS TipoContribuyente,  
        --       CASE TERTIPRET WHEN '0' THEN 'Ninguno' WHEN '1' THEN 'Exento Retencion' WHEN '2' THEN 'Hacer Retencion' END AS TipoRetencion,TERTELEFONO AS  Telefono,TERACTINAC AS Estado  
        --FROM dbo.GENTERCER AS A LEFT OUTER JOIN dbo.GENTERCERT AS B ON A.OID=B.GENTERCER  
        --WHERE TERACTINAC=@Estado  
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que lista los terceros (personas naturales o jurídicas) registrados en el sistema contable, filtrando por estado activo o inactivo. Retorna información de identificación del tercero como número y tipo de documento (cédula, NIT, pasaporte, etc.), nombre completo, tipo de contribuyente (régimen simplificado, común, gran contribuyente, empresa estatal) y tipo de retención aplicable. Se utiliza para integración contable y tributaria, permitiendo consultar el maestro de terceros para procesos de facturación, retenciones y reportes fiscales. El parámetro @Estado permite filtrar entre terceros activos e inactivos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_CONT_ListarTercerosDGHNet';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_CONT_ListarTercerosDGHNet';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Procedimiento sin lógica activa; cuerpo completamente comentado, no ejecuta ninguna operación ni retorna resultados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CONT_ListarTercerosDGHNet';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'No produce conjunto de resultados ni modifica datos: el bloque SELECT está comentado.; Solo aplica SET NOCOUNT ON sin ejecutar consultas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CONT_ListarTercerosDGHNet';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tercero; Tipo de documento; Tipo de contribuyente; Tipo de retención; Régimen simplificado; Régimen común; Régimen contributivo; Gran contribuyente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CONT_ListarTercerosDGHNet';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CONT_ListarTercerosDGHNet';
-- GO
