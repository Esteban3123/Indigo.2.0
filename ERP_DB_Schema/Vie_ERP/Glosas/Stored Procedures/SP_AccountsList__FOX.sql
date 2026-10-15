-- =============================================
-- Author:		Rafael Eduardo Patiño
-- Create date: 14/09/2013
-- Description:	Sp para listar las cuentas contables de DGH
-- =============================================
CREATE PROCEDURE [Glosas].[SP_AccountsList__FOX]
@Empresa as varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	declare @sql nvarchar(MAX)

	Declare @tablaTmp table(
	AccountCode  varchar(100),
	AccountName varchar(100),
	AccountNameCode varchar(200)
	)

	SET NOCOUNT ON;
	set @sql = 'SELECT RTRIM(LTRIM(cpccodcue)) as AccountCode, RTRIM(LTRIM(cpcnomcue)) AS AccountName,
	RTRIM(LTRIM(cpccodcue)) + '' - '' +  RTRIM(LTRIM(cpcnomcue)) AS AccountNameCode
	FROM ' + @Empresa + '..ctPlaCue  WHERE /* cpcmancen=''2'' and cpcmanter=''2'' and */  cpctipcue = ''5''  '

	print @sql
	insert into @tablaTmp
	execute sp_executesql @sql


	select * from @tablaTmp
END