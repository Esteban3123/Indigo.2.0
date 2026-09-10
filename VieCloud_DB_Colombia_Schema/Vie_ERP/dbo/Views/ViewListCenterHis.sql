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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los centros de atención (sedes, clínicas u hospitales) que tienen acceso autorizado según la configuración de permisos por usuario o grupo de usuario. Combina la tabla de centros de atención con las autorizaciones definidas en INAUTORIU (permisos por usuario individual) e INAUTORIG (permisos por grupo de usuario), filtrando únicamente las autorizaciones de tipo contrato ''1''. Expone el código del centro, el nombre, una descripción combinada código-nombre y el código del usuario o grupo autorizado. Se utiliza para controlar y presentar en pantalla qué centros de atención puede ver o gestionar cada usuario según su perfil de acceso en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewListCenterHis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewListCenterHis';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los centros de atención visibles para cada usuario o grupo según las autorizaciones configuradas para el tipo de control ''1''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListCenterHis';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros de autorización (usuario o grupo) con PKTIPOCON=''1'' cuyo PKVALORCO coincida con el código del centro de atención', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListCenterHis';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen autorizaciones cuyo tipo de control es ''1''; El campo Valor siempre se devuelve como bit 1 (autorización afirmativa); Solo se listan centros de atención que tengan al menos una autorización vigente vinculada; Resultados sin duplicados por uso de DISTINCT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListCenterHis';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de atención; Autorización por usuario; Autorización por grupo; Permisos de acceso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListCenterHis';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve centros de atención unidos con autorizaciones de usuario o grupo (PKTIPOCON=''1'') marcando el origen como ''AutUsuario'' o ''AutGrupo''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListCenterHis';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Autorización proviene de INAUTORIU con PKTIPOCON=''1'' → Se etiqueta el permiso como ''AutUsuario'' y se toma CODUSUARI else Si proviene de INAUTORIG con PKTIPOCON=''1'' se etiqueta como ''AutGrupo'' y se toma CODGRUPOU', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListCenterHis';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADcenaten; dbo.INAUTORIU; dbo.INAUTORIG', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListCenterHis';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListCenterHis';
GO
