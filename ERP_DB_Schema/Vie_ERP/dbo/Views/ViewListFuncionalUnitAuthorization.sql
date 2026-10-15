
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