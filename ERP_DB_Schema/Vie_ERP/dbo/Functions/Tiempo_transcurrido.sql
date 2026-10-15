-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Tiempo_transcurrido] (
@FECHA_INICIO as DATETIME,
@FECHA_FIN AS DATETIME

)
RETURNS varchar (100)
AS
BEGIN

declare @CODIGO varchar(100)


SET @CODIGO=(CAST(DATEPART(hh, (@FECHA_INICIO- @FECHA_FIN)) AS NVARCHAR(2)) + ':' + CAST(DATEPART(mi, (@FECHA_INICIO- @FECHA_FIN)) AS NVARCHAR(2)) + ':' + CAST(DATEPART(s, (@FECHA_INICIO- @FECHA_FIN)) AS NVARCHAR(2)))


RETURN (@CODIGO)
END