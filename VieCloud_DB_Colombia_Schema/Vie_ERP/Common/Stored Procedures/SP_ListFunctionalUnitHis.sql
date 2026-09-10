

-- =======================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 18/05/2018
-- Description:	Consulta las unidades funcionales del HIS
-- =======================================================

CREATE Procedure [Common].[SP_ListFunctionalUnitHis]
	@CareCenterCode varchar(20),
	@UserCode varchar(20),
	@GroupCode varchar(20),
	@Container varchar(100)
AS
Begin
	Set Nocount On;
		
	declare @TableReturn table(Codigo varchar(20), UnidadFuncional varchar(100), TipoUnidadFuncional int, CodigoDescripcion varchar(100), [State] bit)

	declare @sql nvarchar(MAX)

	Begin Try

		declare @GroupId int = (select GroupCode from Security.[User] where UserCode = @UserCode)
		set @GroupCode = (select Code from Security.[Group] where Id = @GroupId)
		
		SELECT RTRIM(A.UFUCODIGO) as Codigo,
		RTRIM(A.UFUDESCRI) as UnidadFuncional,
		UFUTIPUNI as TipoUnidadFuncional,
		CONCAT(RTRIM(A.UFUCODIGO),'-',RTRIM(A.UFUDESCRI)) AS CodigoDescripcion,
		Cast(0 as bit) As State
		FROM dbo.INUNIFUNC A 
		INNER JOIN (
			SELECT PKVALORCO, cast(1 as bit) as Valor,  'AutUsuario' as Permiso 
			FROM dbo.INAUTORIU 
			WHERE PKTIPOCON='2' AND CODUSUARI = @UserCode
			UNION 
			SELECT PKVALORCO, cast(1 as bit) as Valor , 'AutUsuario' as Permiso 
			FROM dbo.INAUTORIG 
			WHERE PKTIPOCON='2' AND CODGRUPOU =  @GroupCode
		) AS B ON A.UFUCODIGO=B.PKVALORCO 
		INNER JOIN dbo.INCENUNFU C ON A.UFUCODIGO = C.UFUCODIGO AND C.CODCENATE =@CareCenterCode

	End Try
	Begin Catch
		select * from @TableReturn
	End Catch

End
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las unidades funcionales (servicios, salas o áreas de atención) del sistema HIS disponibles para un usuario específico en un centro de atención determinado. Combina el catálogo maestro de unidades funcionales (INUNIFUNC) con la relación de unidades por centro de atención (INCENUNFU), filtrando únicamente las unidades que el usuario tiene autorizadas, ya sea por permiso individual (INAUTORIU) o por permiso heredado del grupo al que pertenece (INAUTORIG). El grupo del usuario se resuelve automáticamente a partir del código de usuario consultando el módulo de seguridad, ignorando el valor de grupo enviado como parámetro. Se usa típicamente para poblar listas desplegables de selección de unidad funcional en formularios de ingreso, atención u hospitalización.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_ListFunctionalUnitHis';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_ListFunctionalUnitHis';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las unidades funcionales del HIS habilitadas para un centro de atención, accesibles por el usuario directamente o a través de las autorizaciones de su grupo.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_ListFunctionalUnitHis';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El usuario debe existir en Security.User para resolver su grupo.; El grupo asociado al usuario debe existir en Security.Group.; Debe existir el centro de atención indicado para hacer match en INCENUNFU.; Las autorizaciones consultadas se filtran por tipo de control PKTIPOCON=''2''.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_ListFunctionalUnitHis';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan unidades funcionales sobre las que el usuario tenga autorización directa (INAUTORIU) o heredada por su grupo (INAUTORIG) con tipo de control ''2''.; Las unidades retornadas deben pertenecer al centro de atención indicado (INCENUNFU).; El estado (State) de cada unidad retornada se inicializa siempre en 0 (false).; Si ocurre cualquier error durante la consulta, se retorna un conjunto vacío en lugar de propagar la excepción.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_ListFunctionalUnitHis';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Unidad funcional; Centro de atención; Usuario; Grupo de usuarios; Autorización por usuario; Autorización por grupo', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_ListFunctionalUnitHis';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.INUNIFUNC: Devuelve las unidades funcionales (código, descripción, tipo, código-descripción y State=0) que cumplan: tener autorización en INAUTORIU para el usuario o en INAUTORIG para su grupo con PKTIPOCON=''2'', y estar asociadas al centro de atención en INCENUNFU.; [RETURN_RESULT] @TableReturn: Ante cualquier excepción capturada, retorna el contenido (vacío) de la tabla temporal @TableReturn.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_ListFunctionalUnitHis';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Security.User; Security.Group; dbo.INUNIFUNC; dbo.INAUTORIU; dbo.INAUTORIG; dbo.INCENUNFU', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_ListFunctionalUnitHis';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_ListFunctionalUnitHis';
-- GO
