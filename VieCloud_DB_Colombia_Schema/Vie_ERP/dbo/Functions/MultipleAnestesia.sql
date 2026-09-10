
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que recibe la técnica de anestesia utilizada en un procedimiento quirúrgico y devuelve su nombre legible en texto. Acepta dos modos: si se indica un código de técnica único (1=Raquídea, 2=Peridural, 3=Caudal, 4=Regional, 5=Local, 6=General, 7=Sedación), retorna el nombre correspondiente; si el código es nulo, evalúa indicadores individuales por tipo de anestesia y concatena todas las técnicas aplicadas en una sola cadena de texto. Se usa para mostrar en reportes clínicos, historia quirúrgica y documentos de anestesiología las técnicas anestésicas empleadas durante una cirugía o procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'MultipleAnestesia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'MultipleAnestesia';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve una cadena con la(s) técnica(s) anestésica(s) aplicada(s), ya sea traduciendo un código único o concatenando los indicadores booleanos de cada técnica cuando el código no se proporciona.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MultipleAnestesia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Si se usa el modo por código, este debe pertenecer al dominio 1..7 (Raquídea, Peridural, Caudal, Regional, Local, General, Sedación); valores fuera de rango producen NULL en la traducción.; Si el código es NULL, los indicadores booleanos de cada técnica determinan el resultado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MultipleAnestesia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El catálogo de técnicas anestésicas reconocido es fijo: Raquídea, Peridural, Caudal, Regional, Local, General, Sedación.; El código numérico tiene precedencia sobre los indicadores booleanos: si está presente, los flags individuales se ignoran.; La función nunca modifica datos; es puramente de presentación/formato.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MultipleAnestesia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Técnica de anestesia; Anestesia raquídea; Anestesia peridural; Anestesia caudal; Anestesia regional; Anestesia local; Anestesia general; Sedación; Procedimiento quirúrgico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MultipleAnestesia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Cuando el código de técnica no es NULL, retorna el nombre de la técnica correspondiente (1=RAQUÍDEA, 2=PERIDURAL, 3=CAUDAL, 4=REGIONAL, 5=LOCAL, 6=GENERAL, 7=SEDACIÓN) usando STRING_AGG con separador '', ''.; [RETURN_RESULT] (scalar return): Cuando el código de técnica es NULL, retorna la concatenación separada por espacios de los nombres de las técnicas cuyos indicadores booleanos están en 1; los indicadores en 0 aportan cadena vacía.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MultipleAnestesia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Código de técnica anestésica IS NOT NULL → Traduce el código numérico (1..7) al nombre textual de la técnica mediante CASE. else Construye la cadena resultante a partir de los siete indicadores booleanos individuales (raquídea, peridural, caudal, regional, local, general, sedación).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MultipleAnestesia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MultipleAnestesia';
GO
