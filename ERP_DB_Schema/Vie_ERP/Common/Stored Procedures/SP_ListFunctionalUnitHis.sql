

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