
-- =============================================  
-- Author:  <Author,,Name>  
-- ALTER date: <ALTER Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[SP_CONT_ListarTercerosDGHFPpRUEBA]
AS
    BEGIN  
        -- SET NOCOUNT ON added to prevent extra result sets from  
        -- interfering with SELECT statements.  
        SET NOCOUNT ON;

        -- Insert statements for procedure here  
        SELECT TERCODTER AS Codigo,
               CASE TERTIPIDE
                   WHEN '1'
                   THEN 'Cedula Ciudadania'
                   WHEN '2'
                   THEN 'Cedula Extranjeria'
                   WHEN '3'
                   THEN 'Tarjeta Identidad'
                   WHEN '4'
                   THEN 'Registro Civil'
                   WHEN '5'
                   THEN 'Pasaporte'
                   WHEN '6'
                   THEN 'Adulto sin Identificacion'
                   WHEN '7'
                   THEN 'Menor sin Identificacion'
                   WHEN '8'
                   THEN 'Numero Unico de Identificacion'
                   WHEN '9'
                   THEN 'Nit'
               END AS TipoDocumento, 
               TERNOMTER AS Descripcion, 
               RTRIM(TERCODTER) + ' - ' + RTRIM(TERNOMTER) AS Nombre,
               CASE TERTIPCON
                   WHEN '1'
                   THEN 'Regimen Comun'
                   WHEN '2'
                   THEN 'Regimen Simplificado'
                   WHEN '3'
                   THEN 'Gran Contribuyente'
                   WHEN '4'
                   THEN 'Empresa Estatal'
               END AS TipoContribuyente,
               CASE TERTIPRET
                   WHEN '0'
                   THEN 'Ninguno'
                   WHEN '1'
                   THEN 'Exento Retencion'
                   WHEN '2'
                   THEN 'Hacer Retencion'
                   WHEN '3'
                   THEN 'Autoretenedor'
               END AS TipoRetencion, 
               TERTELEF2 AS Telefono
        FROM DBO.GETERCER;
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los terceros registrados en el sistema contable (tabla GETERCER), devolviendo su código, tipo y número de documento de identidad (cédula de ciudadanía, NIT, pasaporte, entre otros), nombre o razón social, tipo de contribuyente tributario (régimen común, simplificado, gran contribuyente, empresa estatal) y condición de retención en la fuente (exento, sujeto a retención, autoretenedor). Se usa para poblar selectores o reportes contables donde se necesita identificar proveedores, clientes o entidades relacionadas con la institución. Es una consulta de referencia para módulos de cuentas por pagar, cuentas por cobrar y parametrización tributaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_CONT_ListarTercerosDGHFPpRUEBA';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_CONT_ListarTercerosDGHFPpRUEBA';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los terceros con su identificación, tipo de documento, tipo de contribuyente y tipo de retención decodificados a texto legible.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CONT_ListarTercerosDGHFPpRUEBA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El nombre presentado concatena código y nombre del tercero separados por '' - '' aplicando RTRIM a ambos.; Solo se exponen valores tipificados conocidos; cualquier otro código de tipo se devuelve como NULL en la columna decodificada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CONT_ListarTercerosDGHFPpRUEBA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tercero; Tipo de identificación (Cédula, NIT, Pasaporte, Registro Civil, etc.); Tipo de contribuyente (Régimen Común/Simplificado, Gran Contribuyente, Empresa Estatal); Tipo de retención (Exento, Autoretenedor)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CONT_ListarTercerosDGHFPpRUEBA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] DBO.GETERCER: Devuelve un resultset de todos los terceros con descripciones traducidas para tipo de identificación, tipo de contribuyente y tipo de retención.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CONT_ListarTercerosDGHFPpRUEBA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Tipo de identificación del tercero (1-9) → Mapea a etiqueta: 1=Cédula Ciudadanía, 2=Cédula Extranjería, 3=Tarjeta Identidad, 4=Registro Civil, 5=Pasaporte, 6=Adulto sin Identificación, 7=Menor sin Identificación, 8=Número Único de Identificación, 9=NIT else NULL; si Tipo de contribuyente (1-4) → 1=Régimen Común, 2=Régimen Simplificado, 3=Gran Contribuyente, 4=Empresa Estatal else NULL; si Tipo de retención (0-3) → 0=Ninguno, 1=Exento Retención, 2=Hacer Retención, 3=Autoretenedor else NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CONT_ListarTercerosDGHFPpRUEBA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.GETERCER', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CONT_ListarTercerosDGHFPpRUEBA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CONT_ListarTercerosDGHFPpRUEBA';
-- GO
