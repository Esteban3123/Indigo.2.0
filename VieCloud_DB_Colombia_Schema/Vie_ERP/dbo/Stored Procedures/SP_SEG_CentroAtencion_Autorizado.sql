-- =============================================
-- Author:      Hector Rodriguez Rubiano
-- Create Date: 27/01/2021
-- Description: Centros de atención autorizados por usuario o grupo
-- =============================================
CREATE PROCEDURE [dbo].[SP_SEG_CentroAtencion_Autorizado]
(
    -- Add the parameters for the stored procedure here
    @Usuario CHAR(20),
    @Grupo CHAR(3)
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
	SELECT DISTINCT
		RTRIM(A.codcenate) AS Codigo
	   ,RTRIM(A.nomcenate) AS CentroAtencion
	   ,RTRIM(A.codcenate) + ' - ' + RTRIM(A.nomcenate) AS CodigoDescripcion
	FROM dbo.ADcenaten A
	INNER JOIN (SELECT
			PKVALORCO
		   ,CAST(1 AS BIT) AS Valor
		   ,'AutUsuario' AS Permiso
		FROM dbo.INAUTORIU
		WHERE PKTIPOCON = '1'
		AND CODUSUARI = @Usuario
		UNION SELECT
			PKVALORCO
		   ,CAST(1 AS BIT) AS Valor
		   ,'AutGrupo' AS Permiso
		FROM dbo.INAUTORIG
		WHERE PKTIPOCON = '1'
		AND CODGRUPOU = @Grupo) AS B
		ON A.codcenate = B.PKVALORCO
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Devuelve la lista de centros de atención (sedes, clínicas, hospitales) a los que tiene acceso un usuario o grupo determinado, según las autorizaciones de seguridad configuradas en el sistema. Recibe como parámetros el código de usuario y el código de grupo, y consulta las tablas de autorizaciones individuales (INAUTORIU) y de grupo (INAUTORIG) para identificar qué centros están permitidos, cruzándolos con el catálogo de centros de atención (ADCENATEN) para obtener el código y nombre de cada sede. Se utiliza para controlar el acceso por sede en interfaces y reportes, garantizando que cada usuario solo vea los centros de atención que tiene habilitados según su perfil de seguridad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_SEG_CentroAtencion_Autorizado';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_SEG_CentroAtencion_Autorizado';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la lista de centros de atención que tiene autorizados un usuario o un grupo, combinando permisos individuales y grupales sobre el tipo de contrato ''1''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_CentroAtencion_Autorizado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en INAUTORIU o INAUTORIG con PKTIPOCON=''1'' para el usuario o grupo indicado.; Los códigos de centro de atención en ADcenaten deben corresponder con los valores PKVALORCO autorizados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_CentroAtencion_Autorizado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna centros de atención cuyo código exista como valor autorizado (PKVALORCO) en autorizaciones de tipo de contrato ''1''.; Considera autorizado tanto si el permiso está concedido al usuario como si está concedido a su grupo (unión de ambas fuentes).; El resultado es distinto (sin duplicados) aunque el centro esté autorizado por usuario y grupo simultáneamente.; El tipo de control/contrato considerado siempre es ''1'' (centros de atención).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_CentroAtencion_Autorizado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de atención; Autorización por usuario; Autorización por grupo; Permisos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_CentroAtencion_Autorizado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ADcenaten: Retorna centros de atención (código, nombre y código-descripción) cuando ADcenaten.codcenate coincide con un PKVALORCO autorizado en INAUTORIU (por usuario) o en INAUTORIG (por grupo) con PKTIPOCON=''1''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_CentroAtencion_Autorizado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADcenaten; dbo.INAUTORIU; dbo.INAUTORIG', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_CentroAtencion_Autorizado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_CentroAtencion_Autorizado';
-- GO
