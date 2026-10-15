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