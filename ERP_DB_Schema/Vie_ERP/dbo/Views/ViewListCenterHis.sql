create VIEW [dbo].[ViewListCenterHis]
AS
		SELECT DISTINCT RTRIM(A.CODCENATE) AS Codigo
		, RTRIM(A.NOMCENATE) as CentroAtencion
		, CONCAT(RTRIM(A.CODCENATE), ' - ', RTRIM(A.NOMCENATE)) AS CodigoDescripcion
		, B.CODUSUARI AS CodigoUsuario
		FROM ADcenaten A 
		INNER JOIN (
			SELECT PKVALORCO, cast(1 as bit) as Valor, 'AutUsuario' as Permiso, CODUSUARI
			FROM INAUTORIU WHERE PKTIPOCON='1'
			UNION 
			SELECT PKVALORCO, cast(1 as bit) as Valor, 'AutGrupo' as Permiso , CODGRUPOU
			FROM INAUTORIG WHERE PKTIPOCON='1'
		) AS B ON A.codcenate=B.PKVALORCO