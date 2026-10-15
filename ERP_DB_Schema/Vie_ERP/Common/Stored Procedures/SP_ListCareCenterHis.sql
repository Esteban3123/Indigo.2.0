
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