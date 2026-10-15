-- =============================================
-- Author:		Rafael Eduardo Patiño
-- Create date: 31/01/2014
-- Description:	Sp para validar que el contenedor exista
-- =============================================
CREATE PROCEDURE [Glosas].[SP_ValidateContainer]
@Empresa as varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	declare @sql nvarchar(MAX)

	Declare @tablaTmp table(
	Respuesta  varchar(100)
	)



	INSERT INTO @tablaTmp
	SELECT count(*) FROM sys.databases where UPPER(name) = UPPER(@Empresa)
	
	select * from @tablaTmp
END