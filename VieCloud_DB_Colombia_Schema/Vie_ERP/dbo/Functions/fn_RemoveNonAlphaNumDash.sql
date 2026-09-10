CREATE FUNCTION dbo.fn_RemoveNonAlphaNumDash
(
   @input varchar(max)
)
RETURNS varchar(max)
AS
BEGIN
   declare @output varchar(max) = '';
   declare @i int = 1;
   declare @len int = LEN(@input);
   while @i <= @len
   begin
       declare @char char(1) = SUBSTRING(@input, @i, 1);
       if @char like '[A-Za-z0-9-]'
           set @output = @output + @char;
       set @i = @i + 1;
   end
   return @output;
END

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Sanitiza una cadena conservando únicamente caracteres alfanuméricos y guiones, eliminando cualquier otro carácter.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fn_RemoveNonAlphaNumDash';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La salida solo contiene letras A-Z/a-z, dígitos 0-9 y guiones ''-''.; Preserva el orden original de los caracteres válidos.; Nunca retorna NULL si la entrada no es NULL (inicia como cadena vacía).; La longitud de la salida es menor o igual a la longitud de la entrada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fn_RemoveNonAlphaNumDash';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Recorre carácter a carácter y, cuando coincide con el patrón ''[A-Za-z0-9-]'', lo concatena al resultado; en caso contrario lo descarta.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fn_RemoveNonAlphaNumDash';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El carácter actual cumple LIKE ''[A-Za-z0-9-]'' → Se agrega al string de salida else Se omite (no se agrega nada)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fn_RemoveNonAlphaNumDash';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fn_RemoveNonAlphaNumDash';
GO
