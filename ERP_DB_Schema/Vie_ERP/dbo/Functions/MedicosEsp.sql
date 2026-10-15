
CREATE FUNCTION [dbo].[MedicosEsp] (@Medico as nvarchar(20))
RETURNS nvarchar (60)
AS
BEGIN

declare @Especialidad nvarchar(60)

SELECT @Especialidad=CODESPEC1 FROM INPROFSAL WHERE CODPROSAL=@Medico

RETURN @Especialidad

END