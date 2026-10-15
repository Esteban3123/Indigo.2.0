
-- =============================================
-- Author:		Rafael Eduardo Patiño
-- Create date: 24/04/2014
-- Description:	Sp para listar los tipo de documentos version NET
-- =============================================
CREATE PROCEDURE [Glosas].[SP_TypeDocumentList]
@Empresa as varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	declare @sql nvarchar(MAX)

	Declare @tablaTmp table(
	Code  varchar(5),
	Name varchar(100),
	Consecutive integer,
	CodeName varchar(150)
	)

	SET NOCOUNT ON;
	set @sql = 'SELECT RTRIM(LTRIM(TCCODIGO )) as Code , RTRIM(LTRIM(TCNOMBRE )) as Name,GENCONSEC as Consecutive, RTRIM(LTRIM(TCCODIGO )) + '' - '' +  RTRIM(LTRIM(TCNOMBRE )) CodeName  FROM ' +  @Empresa + '..CTNTIPCOM '

	print @sql
	insert into @tablaTmp
	execute sp_executesql @sql


	select * from @tablaTmp
END