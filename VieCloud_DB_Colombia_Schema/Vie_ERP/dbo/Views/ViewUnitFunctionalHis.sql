

CREATE VIEW [dbo].[ViewUnitFunctionalHis]
AS

SELECT RTRIM(A.UFUCODIGO) AS Codigo,RTRIM(A.UFUDESCRI) AS 'UnidadFuncional',
UFUTIPUNI AS TipoUnidadFuncional,
CONCAT(RTRIM(A.UFUCODIGO),' - ',RTRIM(A.UFUDESCRI)) AS CodigoDescripcion ,
B.CODUSUARI AS CodigoUsuario ,
CAST(0 AS BIT) AS State ,
C.CODCENATE AS CenterCode
FROM INUNIFUNC A 
INNER JOIN (SELECT PKVALORCO, CAST(1 AS BIT) AS Valor, 'AutUsuario' AS Permiso ,CODUSUARI 
FROM INAUTORIU WHERE PKTIPOCON='2'
UNION 
SELECT PKVALORCO, CAST(1 AS BIT) AS Valor, 'AutGrupo' AS Permiso ,CODGRUPOU 
FROM INAUTORIG 
WHERE PKTIPOCON='2') AS B ON A.UFUCODIGO=B.PKVALORCO 
INNER JOIN INCENUNFU C ON A.UFUCODIGO = C.UFUCODIGO --AND C.CODCENATE = '01' 
--group by A.UFUCODIGO,A.UFUDESCRI,UFUTIPUNI,A.UFUCODIGO,A.UFUDESCRI,B.CODUSUARI,C.CODCENATE

--SELECT RTRIM(A.UFUCODIGO) as Codigo,RTRIM(A.UFUDESCRI) as 'UnidadFuncional',
--UFUTIPUNI as TipoUnidadFuncional,
--CONCAT(RTRIM(A.UFUCODIGO),' - ',RTRIM(A.UFUDESCRI)) AS CodigoDescripcion ,
--B.CODUSUARI AS CodigoUsuario ,
--Cast(0 as bit) As State ,
--C.CODCENATE AS CenterCode
--FROM dbo.INUNIFUNC A 
--INNER JOIN (SELECT PKVALORCO, cast(1 as bit) as Valor, 'AutUsuario' as Permiso , CODUSUARI
--FROM dbo.INAUTORIU 
--WHERE PKTIPOCON='2' AND CODUSUARI = '999' 
--UNION 
--SELECT PKVALORCO, cast(1 as bit) as Valor, 'AutGrupo' as Permiso 
--FROM dbo.INAUTORIG 
--WHERE PKTIPOCON='2' AND CODGRUPOU = '999') AS B ON A.UFUCODIGO=B.PKVALORCO 
--INNER JOIN dbo.INCENUNFU C ON A.UFUCODIGO = C.UFUCODIGO AND C.CODCENATE = '001'
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que lista las unidades funcionales (servicios, salas o áreas de atención) a las que un usuario o grupo tiene acceso autorizado, cruzando el catálogo maestro de unidades funcionales (INUNIFUNC) con las autorizaciones por usuario (INAUTORIU) y por grupo de usuario (INAUTORIG), filtradas por tipo de contrato ''2''. También vincula cada unidad funcional con el centro de atención al que pertenece (INCENUNFU), devolviendo el código, nombre descriptivo, tipo de unidad, código concatenado con descripción, el usuario o grupo autorizado y el código del centro de atención. Se utiliza para controlar qué unidades funcionales puede ver o gestionar cada usuario o grupo en el módulo de historia clínica, según los permisos configurados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewUnitFunctionalHis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewUnitFunctionalHis';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las unidades funcionales autorizadas (por usuario o por grupo) junto con el centro de atención al que pertenecen, para alimentar selectores filtrados por permisos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewUnitFunctionalHis';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las autorizaciones deben existir con PKTIPOCON=''2'' (tipo de control específico para unidades funcionales).; La unidad funcional debe estar asociada a al menos un centro de atención en INCENUNFU.; El código de la unidad funcional debe coincidir con PKVALORCO de la tabla de autorizaciones.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewUnitFunctionalHis';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran autorizaciones cuyo tipo de control sea ''2''.; Una unidad funcional sin autorización (ni de usuario ni de grupo) no aparece en el resultado.; Una unidad funcional sin asociación a algún centro en INCENUNFU no aparece en el resultado.; El campo State siempre se devuelve como 0 (bit).; El campo Valor de la autorización siempre se devuelve como 1 (bit) (aunque no se proyecta en la salida final).; CodigoDescripcion se construye siempre como ''codigo - descripcion'' con espacios eliminados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewUnitFunctionalHis';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Unidad funcional; Tipo de unidad funcional; Centro de atención; Autorización por usuario; Autorización por grupo; Permisos de acceso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewUnitFunctionalHis';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve una fila por cada combinación (unidad funcional × usuario/grupo autorizado × centro de atención) cuando existe autorización con PKTIPOCON=''2''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewUnitFunctionalHis';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Autorización con PKTIPOCON=''2'' proviene de INAUTORIU → Se marca Permiso=''AutUsuario'' y se toma CODUSUARI como código del autorizado.; si Autorización con PKTIPOCON=''2'' proviene de INAUTORIG → Se marca Permiso=''AutGrupo'' y se toma CODGRUPOU como código del autorizado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewUnitFunctionalHis';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INUNIFUNC; dbo.INAUTORIU; dbo.INAUTORIG; dbo.INCENUNFU', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewUnitFunctionalHis';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewUnitFunctionalHis';
GO
