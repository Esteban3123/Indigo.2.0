CREATE FUNCTION [dbo].[ReplaceASCII](@inputString VARCHAR(8000))
RETURNS VARCHAR(55)
AS
     BEGIN
         DECLARE @badStrings VARCHAR(100);
         DECLARE @increment INT= 1;
         WHILE @increment <= DATALENGTH(@inputString)
             BEGIN
                 IF(ASCII(SUBSTRING(@inputString, @increment, 1)) < 33)
                     BEGIN
                         SET @badStrings = CHAR(ASCII(SUBSTRING(@inputString, @increment, 1)));
                         SET @inputString = REPLACE(@inputString, @badStrings, '');
                 END;
                 SET @increment = @increment + 1;
             END;
         RETURN @inputString;
     END;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de limpieza de texto que elimina caracteres especiales, espacios y símbolos no imprimibles (con código ASCII menor a 33) de una cadena de texto. Recorre carácter por carácter el texto ingresado y remueve cualquier carácter de control como saltos de línea, tabulaciones o espacios en blanco al inicio o fin. Se utiliza para sanear datos de texto libre antes de procesarlos o almacenarlos, por ejemplo al limpiar nombres de pacientes, descripciones de servicios o cualquier campo de texto que pueda contener caracteres no deseados provenientes de integraciones externas o digitación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'ReplaceASCII';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'ReplaceASCII';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Depura una cadena eliminando todos los caracteres ASCII de control (valor < 33), devolviendo la cadena saneada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ReplaceASCII';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La cadena de entrada no debe exceder 8000 caracteres (límite del parámetro); El resultado útil queda truncado a 55 caracteres por el tipo de retorno', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ReplaceASCII';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado nunca contiene caracteres ASCII con valor menor a 33 (espacios, tabs, saltos de línea y controles); El resultado se trunca implícitamente a 55 caracteres por el RETURNS VARCHAR(55); No realiza acceso a tablas ni efectos colaterales de datos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ReplaceASCII';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Cuando ASCII(carácter) < 33, se reemplaza ese carácter por cadena vacía en toda la entrada antes de retornar', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ReplaceASCII';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ASCII(SUBSTRING(@inputString,@increment,1)) < 33 → Elimina todas las ocurrencias de ese carácter en la cadena vía REPLACE else No modifica la cadena y avanza al siguiente carácter', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ReplaceASCII';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ReplaceASCII';
GO
