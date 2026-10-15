-- =============================================
-- Author:		Diego Andrés Roldán Lozano
-- Create date: 27-12-2014
-- Description:	
-- =============================================
CREATE PROCEDURE [InteropCost].[SP_MainAccountValue]
	@Container varchar(15), 
    @MainAccountNumber nvarchar(30),
	@Year varchar(4),
	@Month int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	/****** Script for SelectTopNRows command from SSMS  ******/
	declare @sql nvarchar(MAX)
	declare @sqlCuenta nvarchar(MAX)
	declare @MainAccountId int
	declare @tablaTmp table(
		Diference numeric(18,2)
	)

	--set @sql = N'SELECT @MainAccountId = OID FROM [' + @Container + '].[dbo].[CTNCUENTA] Where CUECODIGO = @NumCuenta '
	--exec sp_executesql @sql,N'@NumCuenta nvarchar(30), @MainAccountId int OUTPUT',@NumCuenta= @MainAccountNumber, @MainAccountId = @MainAccountId OUTPUT
	
	set @sqlCuenta = 'SELECT (ABS(SUM([CSCDEBITO]) - SUM([CSCCREDITO]))) as diference
						FROM [' + @Container + '].[dbo].[CTNSAL' + @Year + ']
						where CTNCUENTA = @MainAccountId
						and CSCMES = @Month'
	INSERT INTO @tablaTmp
	exec sp_executesql @sqlCuenta, N'@MainAccountId int, @Month int',@MainAccountId= @MainAccountNumber, @Month = @Month




	SELECT Diference FROM @tablaTmp
	--select SCOPE_IDENTITY() as id;
END