
-- =======================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 18/05/2018
-- Description:	Consulta los centros de atención del HIS
-- =======================================================

CREATE PROCEDURE [Common].[SP_ListCareCenterHis] @UserCode  VARCHAR(20), 
                                                @GroupCode VARCHAR(20), 
                                                @Container VARCHAR(100)
AS
    BEGIN
        SET NOCOUNT ON;
        DECLARE @TableReturn TABLE
        (Codigo            VARCHAR(20), 
         CentroAtencion    VARCHAR(100), 
         CodigoDescripcion VARCHAR(100)
        );
        DECLARE @sql NVARCHAR(MAX);
        BEGIN TRY
            SET @GroupCode =
            (
                SELECT CODGRUPOU
                FROM.SEGusuaru
                WHERE CODUSUARI = @UserCode
            );

            --SET @GroupCode =
            --(
            --    SELECT Code FROM .SEGgruusu WHERE codgrupou = @GroupId
            --);

            SET @sql = N'SELECT DISTINCT RTRIM(A.codcenate) AS Codigo,
		RTRIM(A.nomcenate) as CentroAtencion,
		CONCAT(RTRIM(A.CODCENATE),' + N'''-''' + N', RTRIM(A.NOMCENATE)) AS CodigoDescripcion 
		FROM ' + @Container + N'.dbo.ADcenaten A 
		INNER JOIN (
			SELECT PKVALORCO, cast(1 as bit) as Valor, ''AutUsuario'' as Permiso 
			FROM ' + @Container + N'.dbo.INAUTORIU 
			WHERE PKTIPOCON=''1'' AND CODUSUARI = ''' + @UserCode + N''' 
			UNION 
			SELECT PKVALORCO, cast(1 as bit) as Valor, ''AutGrupo'' as Permiso 
			FROM ' + @Container + N'.dbo.INAUTORIG 
			WHERE PKTIPOCON=''1'' AND CODGRUPOU = ''' + @GroupCode + N'''
		) AS B ON A.codcenate=B.PKVALORCO';
            PRINT @sql;
            INSERT INTO @TableReturn
            EXECUTE sp_executesql 
                    @sql;
            SELECT *
            FROM @TableReturn;
        END TRY
        BEGIN CATCH
            SELECT *
            FROM @TableReturn;
        END CATCH;
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los centros de atención habilitados para un usuario específico dentro del HIS (Historia Clínica Hospitalaria), filtrando únicamente aquellos centros sobre los que el usuario o su grupo tienen autorización. Primero obtiene el código de grupo del usuario consultando la tabla de usuarios del sistema de seguridad, luego cruza los centros de atención con las autorizaciones por usuario y por grupo registradas en el contenedor de base de datos indicado. Se utiliza para poblar selectores o filtros de centros de atención en la interfaz, garantizando que cada usuario solo vea los centros a los que tiene acceso permitido.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_ListCareCenterHis';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_ListCareCenterHis';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los centros de atención del HIS autorizados para un usuario, considerando permisos directos y los heredados por su grupo.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_ListCareCenterHis';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El usuario debe existir en SEGusuaru para resolver su grupo (CODGRUPOU).; Debe proveerse el nombre de la base/contenedor del HIS donde residen ADcenaten, INAUTORIU e INAUTORIG.; Las tablas de autorización usan PKTIPOCON=''1'' para identificar autorizaciones de centros de atención.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_ListCareCenterHis';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El grupo usado para evaluar permisos siempre se recalcula desde SEGusuaru a partir del usuario, ignorando el grupo recibido como parámetro.; Solo se consideran autorizaciones de tipo PKTIPOCON=''1'' (centros de atención).; El resultado entrega códigos distintos (DISTINCT) y una descripción concatenada ''codigo-nombre''.; Si ocurre cualquier error, nunca se lanza excepción al cliente; se retorna el conjunto acumulado.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_ListCareCenterHis';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de atención; Usuario; Grupo de usuarios; Autorización por usuario; Autorización por grupo; HIS', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_ListCareCenterHis';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableReturn: Inserta los centros de atención (codcenate, nomcenate) cuyo código tenga autorización vigente en INAUTORIU para el usuario o en INAUTORIG para su grupo, con PKTIPOCON=''1''.; [RETURN_RESULT] @TableReturn: Devuelve el conjunto resultante; ante error en la ejecución dinámica retorna la tabla vacía (CATCH silencioso).', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_ListCareCenterHis';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Permiso directo del usuario: INAUTORIU.CODUSUARI = usuario y PKTIPOCON=''1'' → Marca el centro como autorizado por ''AutUsuario''; si Permiso por grupo: INAUTORIG.CODGRUPOU = grupo del usuario y PKTIPOCON=''1'' → Marca el centro como autorizado por ''AutGrupo'' (UNION con el caso anterior); si Falla la ejecución del SQL dinámico → Se captura la excepción y se devuelve la tabla de retorno tal como esté (posiblemente vacía), sin propagar el error', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_ListCareCenterHis';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'SEGusuaru; ADcenaten; INAUTORIU; INAUTORIG', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_ListCareCenterHis';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_ListCareCenterHis';
-- GO
