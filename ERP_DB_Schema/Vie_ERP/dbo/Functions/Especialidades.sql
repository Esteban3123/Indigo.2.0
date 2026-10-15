

CREATE FUNCTION [dbo].[Especialidades] (@Especialidad as nvarchar(3))
RETURNS nvarchar (60)
AS
BEGIN

declare @NomEspecialidad nvarchar(60)

SELECT @NomEspecialidad=DESESPECI FROM INESPECIA WHERE CODESPECI=@Especialidad

RETURN @NomEspecialidad

END