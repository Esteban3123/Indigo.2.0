
create VIEW [dbo].[ViewListFuncionalUnitAuthorization]
AS
SELECT      distinct isnull(B.Codeusers,' ') as Codeusers, isnull(B.CodeGroup,' ') as CodeGroup, RTRIM(A.UFUCODIGO) AS Codigo, RTRIM(A.UFUDESCRI) AS UnidadFuncional, A.UFUTIPUNI AS TipoUnidadFuncional, RTRIM(A.UFUCODIGO) + ' - ' + RTRIM(A.UFUDESCRI) 
                         AS CodigoDescripcion
FROM            dbo.INUNIFUNC AS A INNER JOIN
                             (SELECT        PKVALORCO, CAST(1 AS bit) AS Valor, 'AutUsuario' AS Permiso, NULL AS CodeGroup, CODUSUARI AS Codeusers
                               FROM            dbo.INAUTORIU
                               WHERE        (PKTIPOCON = '2')
                               UNION
                               SELECT        PKVALORCO, CAST(1 AS bit) AS Valor, 'AutGrupo' AS Permiso, CODGRUPOU AS CodeGroup, NULL AS Codeusers
                               FROM            dbo.INAUTORIG
                               WHERE        (PKTIPOCON = '2') AND (CODGRUPOU IN
                                                            (SELECT        CODGRUPOU
                                                              FROM            dbo.SEGusuaru))) AS B ON A.UFUCODIGO = B.PKVALORCO INNER JOIN
                         dbo.INCENUNFU AS C ON A.UFUCODIGO = C.UFUCODIGO
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las unidades funcionales (servicios, salas o áreas de atención) que tienen autorización de acceso configurada, ya sea por usuario individual o por grupo de usuarios, para el tipo de contrato de unidades funcionales (PKTIPOCON = ''2''). Combina el catálogo maestro de unidades funcionales (INUNIFUNC) con las autorizaciones por usuario (INAUTORIU) y por grupo (INAUTORIG), filtrando además que la unidad funcional pertenezca a algún centro de atención (INCENUNFU). Se usa para controlar qué unidades funcionales puede ver o gestionar cada usuario o grupo dentro del sistema, sirviendo como base para filtros de seguridad y listas desplegables de selección de área o servicio en la interfaz clínica y administrativa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewListFuncionalUnitAuthorization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewListFuncionalUnitAuthorization';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las unidades funcionales sobre las cuales un usuario o un grupo del usuario actual tiene autorización configurada (tipo de control ''2''), incluyendo solo aquellas asociadas a algún centro.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListFuncionalUnitAuthorization';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros de autorización con PKTIPOCON=''2'' en INAUTORIU (por usuario) o INAUTORIG (por grupo).; El grupo autorizado en INAUTORIG debe existir en SEGusuaru para considerarse válido.; La unidad funcional debe estar relacionada con al menos un centro en INCENUNFU.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListFuncionalUnitAuthorization';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran autorizaciones con tipo de control igual a ''2''.; Una unidad funcional sin relación en INCENUNFU nunca se retorna (INNER JOIN).; Los grupos autorizados deben estar registrados en SEGusuaru para ser incluidos.; Los valores nulos de Codeusers y CodeGroup se sustituyen por espacio en blanco en la salida.; Se eliminan duplicados mediante DISTINCT y UNION.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListFuncionalUnitAuthorization';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Unidad Funcional; Autorización por usuario; Autorización por grupo; Tipo de unidad funcional; Centro de atención; Permisos de seguridad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListFuncionalUnitAuthorization';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve unidades funcionales (código, descripción, tipo) cuando UFUCODIGO coincide con PKVALORCO de autorizaciones (usuario o grupo) con PKTIPOCON=''2'' y existe vínculo en INCENUNFU.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListFuncionalUnitAuthorization';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Autorización proviene de INAUTORIU con PKTIPOCON=''2'' → Se marca como permiso ''AutUsuario'' y se expone Codeusers (CODUSUARI), dejando CodeGroup nulo; si Autorización proviene de INAUTORIG con PKTIPOCON=''2'' y CODGRUPOU está en SEGusuaru → Se marca como permiso ''AutGrupo'' y se expone CodeGroup (CODGRUPOU), dejando Codeusers nulo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListFuncionalUnitAuthorization';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INUNIFUNC; dbo.INAUTORIU; dbo.INAUTORIG; dbo.SEGusuaru; dbo.INCENUNFU', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListFuncionalUnitAuthorization';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListFuncionalUnitAuthorization';
GO
