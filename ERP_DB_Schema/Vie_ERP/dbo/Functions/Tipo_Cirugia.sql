-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Tipo_Cirugia] (
@TIPO_CIRUG as bit

)
RETURNS varchar (100)
AS
BEGIN

declare @TPCIRU varchar(100)
SET @TPCIRU=(CASE 
WHEN @TIPO_CIRUG = 'TRUE' THEN 'URGENCIA' 
WHEN @TIPO_CIRUG = 'FALSE' THEN 'PROGRAMADA' END)

RETURN (@TPCIRU)
END