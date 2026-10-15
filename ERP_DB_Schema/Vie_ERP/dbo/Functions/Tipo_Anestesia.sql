-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	Tipo de Anestecia que se aplicara al paciente
-- =============================================
CREATE FUNCTION [dbo].[Tipo_Anestesia] (
@TIPO_ANEST as int

)
RETURNS varchar (100)
AS
BEGIN

declare @TPANEST varchar(100)
SET @TPANEST=(CASE 
WHEN @TIPO_ANEST = 1 THEN 'LOCAL' 
WHEN @TIPO_ANEST = 2 THEN 'REGIONAL' 
WHEN @TIPO_ANEST = 3 THEN 'GENERAL' 
WHEN @TIPO_ANEST = 4 THEN 'COMBINADA'
END)

RETURN (@TPANEST)
END