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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera un código de secuencia alfanumérico formateado a partir de un patrón, un prefijo y un número correlativo. El patrón puede contener caracteres especiales: ''#'' para dígitos (0-9) y ''&'' para letras (A-Z), además de variables de fecha y hora como %d (día), %M (mes), %y (año), %h (hora), %m (minuto), %s (segundo) y %pfx (prefijo). Se utiliza para construir identificadores únicos de negocio como números de ingreso, órdenes médicas, facturas, recetas u otros consecutivos del sistema, garantizando que el número no supere el máximo permitido por el patrón definido; en caso contrario retorna ''__ERROR_MAXVALUE__''.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'GetSequence';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'GetSequence';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un código/secuencia formateado a partir de un patrón con marcadores numéricos (#), alfabéticos (&), prefijo y tokens de fecha/hora, validando que el número quepa en la capacidad del patrón.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetSequence';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El patrón no puede ser nulo ni cadena vacía/blancos; El número de secuencia debe ser >= 0; Debe existir la función Common.GETDATE para obtener fecha/hora', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetSequence';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El patrón debe ser no nulo y no vacío y el número no negativo para producir un valor válido; La capacidad máxima de la secuencia es 10^(#cantidad) * 26^(&cantidad); si el número la excede se retorna error; Cada ''#'' del patrón se sustituye por un dígito (0-9) y cada ''&'' por una letra (A-Z), preservando los caracteres literales; Las variables temporales en el patrón se reemplazan: %pfx por prefijo, %d día, %M mes, %y año, %h hora, %m minuto, %s segundo, con padding de ceros; La secuencia se calcula posicionalmente de derecha a izquierda usando bases mixtas (10 para #, 26 para &); La fecha/hora usada proviene siempre de Common.GETDATE() (hora del sistema centralizada)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetSequence';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'secuencia/correlativo; prefijo de código; patrón de formato; fecha y hora del sistema', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetSequence';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Cuando el patrón es nulo/vacío o el número es negativo, retorna la cadena ''__ERROR_MAXVALUE__''; [RETURN_RESULT] (scalar return): Cuando (ValorMax - 1) < Num (número fuera del rango representable por el patrón), retorna ''__ERROR_MAXVALUE__''; [RETURN_RESULT] (scalar return): En caso válido, retorna el patrón con %pfx/%d/%M/%y/%h/%m/%s reemplazados por prefijo y componentes de fecha/hora, y con cada # y & sustituidos por el dígito/letra calculado del número', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetSequence';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Patrón nulo/vacío o número negativo → Retorna ''__ERROR_MAXVALUE__''; si (ValorMax - 1) < Num, es decir, el número excede la capacidad del patrón → Retorna ''__ERROR_MAXVALUE__''; si Carácter del patrón es ''#'' → Aporta un dígito 0-9 (base 10) a la secuencia; si Carácter del patrón es ''&'' → Aporta una letra A-Z (base 26) a la secuencia; si Carácter del patrón distinto de ''#'' y ''&'' → Se conserva como literal en el resultado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetSequence';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetSequence';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetSequence';
GO
