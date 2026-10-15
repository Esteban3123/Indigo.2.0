
CREATE FUNCTION [dbo].[MultipleAnestesia] (@TECNANESTE as int , @x1_raquidea as bit, @x2_peridual as bit, @x3_caudal as bit, @x4_regional as bit, @x5_local as bit, @x6_general as bit, @x7_sedacion as bit)
RETURNS nvarchar (max)
AS
BEGIN

 -- Tecnica de Anestesia = 1-Raquídea 2-Peridural 3-Caudal 4-Regional 5-LocaL 6-General 7-Sedación
declare @MultipleAnestesia nvarchar (max)
declare @Separador nvarchar(3) = ', '

declare @tecnica_1 nvarchar(10) = 'RAQUÎDEA'
declare @tecnica_2 nvarchar(10) = 'PERIDURAL'
declare @tecnica_3 nvarchar(10) = 'CAUDAL'
declare @tecnica_4 nvarchar(10) = 'REGIONAL'
declare @tecnica_5 nvarchar(10) = 'LOCAL'
declare @tecnica_6 nvarchar(10) = 'GENERAL'
declare @tecnica_7 nvarchar(10) = 'SEDACIÓN'
 
declare @texto_1 nvarchar(10)
declare @texto_2 nvarchar(10)
declare @texto_3 nvarchar(10)
declare @texto_4 nvarchar(10)
declare @texto_5 nvarchar(10)
declare @texto_6 nvarchar(10)
declare @texto_7 nvarchar(10)

--declare @table as table(strtexto varchar(500))

IF @TECNANESTE IS NOT NULL 
	Begin
		Set @MultipleAnestesia = STRING_AGG((CASE @TECNANESTE  WHEN 1 THEN @tecnica_1 WHEN 2 THEN @tecnica_2 WHEN 3 THEN @tecnica_3 WHEN 4 THEN @tecnica_4 WHEN 5 THEN @tecnica_5 WHEN 6 THEN @tecnica_6 WHEN 7 THEN @tecnica_7 END), ', ') 
	End

IF @TECNANESTE IS NULL 
	Begin
	 	 
		set @texto_1 = CASE WHEN @x1_raquidea = 1 THEN @tecnica_1 ELSE '' END
		set @texto_2 = CASE WHEN @x2_peridual = 1 THEN @tecnica_2 ELSE '' END
		set @texto_3 = CASE WHEN @x3_caudal = 1 THEN @tecnica_3 ELSE '' END
		set @texto_4 = CASE WHEN @x4_regional = 1 THEN @tecnica_4 ELSE '' END
		set @texto_5 = CASE WHEN @x5_local = 1 THEN @tecnica_5 ELSE '' END
		set @texto_6 = CASE WHEN @x6_general = 1 THEN @tecnica_6 ELSE '' END
		set @texto_7 = CASE WHEN @x7_sedacion = 1 THEN @tecnica_7 ELSE '' END

		Set @MultipleAnestesia =  @texto_1 +' '+ @texto_2 +' '+ @texto_3 +' '+ @texto_4 +' '+ @texto_5 +' '+ @texto_6 +' '+ @texto_7
		
		--insert into @table
		--select @texto_1  
		--union all
		--select @texto_2
		--union all
		--select @texto_3  
		--union all
		--select @texto_4
		--union all
		--select @texto_5  
		--union all
		--select @texto_6 
 	--    union all
		--select @texto_7 

		--DECLARE @columnas Nvarchar(max)='';

		--select @columnas =coalesce(@columnas + cast(strtexto as varchar(100)) + ', ', ' ')  from @table 

		--set @MultipleAnestesia  = left(@columnas,LEN(@columnas)-1)

	End

RETURN @MultipleAnestesia
END