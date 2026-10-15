-- =============================================
-- Author:		Rafael Patiño
-- Create date: 31/08/2013
-- Description:	Fucion para validar mes cerrado
-- =============================================
CREATE FUNCTION [dbo].[GloValidarMes] 
(
		@Fecha date,
		@Empresa varchar(30)
)
RETURNS bit
AS
BEGIN
	-- Declare the return variable here
	declare @GloValidarMes as bit
	declare @sql nvarchar(1000)
	declare @tableTmp table(CPSFECINI  date, cpscierre  varchar(17) )
	declare @count  Integer

	declare @Mes  Integer = Month(@Fecha)
	declare @Ano  Integer = year(@Fecha)
	declare @MesCerrado  Integer

	-- Add the T-SQL statements to compute the return value here
	set @sql =  'SELECT CPSFECINI, rtrim(cpscierre) as cpscierre FROM DGEMPRES' + @Empresa + '..ctparsis'
	
	
	insert into @tableTmp
	select '13/08/2013' ,2 
	--execute sp_executesql @sql

	select @count = count(*) from @tableTmp

	IF @count > 0 BEGIN
		declare @CPSFECINI as date = (select CPSFECINI from @tableTmp)
		declare @cpscierre varchar(17) = (select cpscierre from @tableTmp)

		declare @MesIni as integer =  Month(@CPSFECINI)
		declare @anoini as integer =  year(@CPSFECINI)
		set @MesCerrado = len(@cpscierre)

		/*If MesCerrado <= 13 And MesCerrado <= Mes Then
       GloValidarMes = True
    Else
       If MesCerrado > 13 Then
          MesCerrado = MesCerrado - 12
          If MesCerrado <= Mes Then
            GloValidarMes = True
          Else
            GloValidarMes = False
          End If
       Else
          difAno = Ano - anoini
          If difAno > 1 Then
             GloValidarMes = True
          Else
             GloValidarMes = False
          End If
       End If
    End If
	end*/

		if @MesCerrado <=13 and @MesCerrado <= @Mes begin
			set @GloValidarMes = 1
		end
	END
	--SELECT CPSFECINI,rtrim(cpscierre) as cpscierre FROM .ctparsis


	-- Return the result of the function
	RETURN @GloValidarMes 

END