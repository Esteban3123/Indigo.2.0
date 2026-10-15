--=============================================
--Author:		Alvaro Heliud Herrera Novoa
--Create date: 2020-02-12
--Description:	Sequense in T-SQL
--=============================================

CREATE FUNCTION [dbo].[GetSequence]
(@Prefix  VARCHAR(10), 
 @Pattern VARCHAR(256), 
 @Num     INT
)
RETURNS VARCHAR(256)
AS
     BEGIN
         -- Variables optimizadas
         DECLARE @ResultVar VARCHAR(256) = '';
         DECLARE @Error VARCHAR(256) = '__ERROR_MAXVALUE__';
         
         -- Validar variables de entrada
         IF @Pattern IS NULL OR LTRIM(RTRIM(@Pattern)) = '' OR @Num < 0
         BEGIN
             RETURN @Error;
         END;
         
         -- Variables para fechas (calculadas una sola vez)
         DECLARE @CurrentDate DATETIME = [Common].[GETDATE]();
         DECLARE @DayStr VARCHAR(2) = RIGHT('00' + CAST(DAY(@CurrentDate) AS VARCHAR(2)), 2);
         DECLARE @MonthStr VARCHAR(2) = RIGHT('00' + CAST(MONTH(@CurrentDate) AS VARCHAR(2)), 2);
         DECLARE @YearStr VARCHAR(4) = RIGHT('0000' + CAST(YEAR(@CurrentDate) AS VARCHAR(4)), 4);
         DECLARE @HourStr VARCHAR(2) = RIGHT('00' + CAST(DATEPART(HOUR, @CurrentDate) AS VARCHAR(2)), 2);
         DECLARE @MinuteStr VARCHAR(2) = RIGHT('00' + CAST(DATEPART(MINUTE, @CurrentDate) AS VARCHAR(2)), 2);
         DECLARE @SecondStr VARCHAR(2) = RIGHT('00' + CAST(DATEPART(SECOND, @CurrentDate) AS VARCHAR(2)), 2);
         
         -- Variables para el procesamiento
         DECLARE @LenPattern INT = LEN(LTRIM(RTRIM(@Pattern)));
         DECLARE @Numero INT = 1;
         DECLARE @ValorMax NUMERIC(38, 0) = 1;
         DECLARE @Letra VARCHAR(1);
         DECLARE @PatternClean VARCHAR(256) = '';
         
         -- Generar pattern limpio y calcular valor máximo
         WHILE @LenPattern >= @Numero
         BEGIN
             SET @Letra = SUBSTRING(@Pattern, @Numero, 1);
             
             IF @Letra = '#' OR @Letra = '&'
             BEGIN
                 SET @PatternClean = @PatternClean + @Letra;
                 
                 IF @Letra = '#'
                     SET @ValorMax = @ValorMax * 10;
                 ELSE
                     SET @ValorMax = @ValorMax * 26;
             END;
             
             SET @Numero = @Numero + 1;
         END;
         
         -- Validar máximo de la secuencia
         IF (@ValorMax - 1) < @Num
         BEGIN
             RETURN @Error;
         END;
         
         -- Algoritmo matemático simple para generar la secuencia numérica
         DECLARE @PatternLen INT = LEN(@PatternClean);
         DECLARE @RemainingNum INT = @Num;
         DECLARE @BaseValue INT;
         DECLARE @CurrentValue INT;
         DECLARE @Position INT = 1;
         DECLARE @CurrentChar CHAR(1);
         DECLARE @SequenceResult VARCHAR(256) = '';
         
         -- Procesar cada posición de derecha a izquierda usando matemáticas puras
         WHILE @Position <= @PatternLen
         BEGIN
             SET @CurrentChar = SUBSTRING(@PatternClean, @PatternLen - @Position + 1, 1);
             
             IF @CurrentChar = '#'
                 SET @BaseValue = 10;
             ELSE IF @CurrentChar = '&'
                 SET @BaseValue = 26;
             ELSE
                 SET @BaseValue = 1;
                 
             -- Calcular el valor para esta posición
             SET @CurrentValue = (@RemainingNum % @BaseValue) + 1;
             
             -- Construir la secuencia de derecha a izquierda
             SET @SequenceResult = 
                 CASE @CurrentChar
                     WHEN '#' THEN CHAR(48 + @CurrentValue - 1) -- '0' a '9'
                     WHEN '&' THEN CHAR(64 + @CurrentValue)     -- 'A' a 'Z'
                     ELSE @CurrentChar
                 END + @SequenceResult;
             
             -- Reducir el número restante
             SET @RemainingNum = @RemainingNum / @BaseValue;
             SET @Position = @Position + 1;
         END;
         
         -- Ahora construir el resultado final reemplazando las variables en el patrón original
         SET @ResultVar = @Pattern;
         
         -- Reemplazar variables especiales
         SET @ResultVar = REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(
             @ResultVar, 
             '%pfx', @Prefix),
             '%d', @DayStr),
             '%M', @MonthStr),
             '%y', @YearStr),
             '%h', @HourStr),
             '%m', @MinuteStr),
             '%s', @SecondStr);
         
         -- Reemplazar los caracteres # y & con la secuencia calculada
         DECLARE @SeqPos INT = 1;
         DECLARE @ResultPos INT = 1;
         DECLARE @FinalResult VARCHAR(256) = '';
         DECLARE @PatternChar VARCHAR(1);
         
         WHILE @ResultPos <= LEN(@ResultVar)
         BEGIN
             SET @PatternChar = SUBSTRING(@ResultVar, @ResultPos, 1);
             
             IF @PatternChar = '#' OR @PatternChar = '&'
             BEGIN
                 -- Reemplazar con el carácter correspondiente de la secuencia
                 SET @FinalResult = @FinalResult + SUBSTRING(@SequenceResult, @SeqPos, 1);
                 SET @SeqPos = @SeqPos + 1;
             END
             ELSE
             BEGIN
                 -- Mantener el carácter literal
                 SET @FinalResult = @FinalResult + @PatternChar;
             END;
             
             SET @ResultPos = @ResultPos + 1;
         END;
         
         -- Retornar secuencia final
         RETURN @FinalResult;
     END;
