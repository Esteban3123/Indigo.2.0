
CREATE PROCEDURE [dbo].[SPMOV_ListarCentrosDeAtencion]
(
	@Usuario nvarchar(50)
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT  RTRIM(A.codcenate) AS Codigo,
		RTRIM(A.nomcenate) as 'Centro Atencion',
		RTRIM(A.codcenate)+' - '+RTRIM(A.nomcenate) AS CodigoDescripcion
    FROM ADcenaten A
		INNER JOIN (SELECT PKVALORCO FROM INAUTORIU WHERE PKTIPOCON='1' AND CODUSUARI = @Usuario
					UNION 
					SELECT PKVALORCO FROM INAUTORIG WHERE PKTIPOCON='1' AND CODGRUPOU  in (select CODGRUPOU  from [dbo].[SEGusuaru] where CODUSUARI = @Usuario )) AS B ON A.codcenate=B.PKVALORCO
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los centros de atención (sedes, clínicas, hospitales) a los que tiene acceso un usuario específico del sistema. Combina las autorizaciones individuales del usuario y las autorizaciones heredadas por su grupo de seguridad para filtrar únicamente las sedes habilitadas para ese operador. Devuelve el código, el nombre y una descripción combinada de cada centro de atención autorizado, útil para poblar selectores o filtros en pantallas de registro de atención, admisión e ingreso de pacientes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarCentrosDeAtencion';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarCentrosDeAtencion';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar los centros de atención a los que un usuario tiene acceso, ya sea por permiso directo o a través de los grupos a los que pertenece.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarCentrosDeAtencion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El usuario debe existir en SEGusuaru para poder resolver sus grupos.; Deben existir registros de autorización con PKTIPOCON=''1'' asociados al usuario o a alguno de sus grupos para que el resultado no sea vacío.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarCentrosDeAtencion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven centros de atención sobre los cuales el usuario tiene autorización, ya sea directa (INAUTORIU) o heredada por su grupo (INAUTORIG).; El tipo de concepto autorizado se restringe siempre a PKTIPOCON=''1'' (centros de atención).; Los campos de salida se entregan sin espacios sobrantes (RTRIM) y se incluye una columna concatenada Código - Nombre.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarCentrosDeAtencion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de atención; Autorización por usuario; Autorización por grupo de usuario; Seguridad/Permisos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarCentrosDeAtencion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ADcenaten: Devuelve los centros de atención (ADcenaten) cuyo código coincide con un PKVALORCO autorizado para el usuario en INAUTORIU (PKTIPOCON=''1'') o para alguno de sus grupos en INAUTORIG (PKTIPOCON=''1'').', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarCentrosDeAtencion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADcenaten; dbo.INAUTORIU; dbo.INAUTORIG; dbo.SEGusuaru', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarCentrosDeAtencion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarCentrosDeAtencion';
-- GO
