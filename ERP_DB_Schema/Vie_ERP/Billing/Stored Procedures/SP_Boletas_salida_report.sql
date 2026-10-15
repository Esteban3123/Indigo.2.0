

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Billing].[SP_Boletas_salida_report] 
	-- Add the parameters for the stored procedure here
	@FECHA_INICIAL DATE, 
	@FECHA_FINAL DATE,
	@ID char
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select 
SLI.Code AS 'CODIGO',
SLI.AdmissionNumber AS 'NUMERO DE ADMISION',
SLI.DocumentDate AS 'FECHA',
INP.IPNOMCOMP AS 'NOMBRE DEL PACIENTE',
INP.IPCODPACI AS CEDULA
from Billing.SlipOut AS SLI INNER JOIN
                     dbo.ADINGRESO AS ADI ON ADI.NUMINGRES = SLI.AdmissionNumber INNER JOIN
					 dbo.INPACIENT AS INP ON ADI.IPCODPACI = INP.IPCODPACI
WHERE (SLI.DocumentDate BETWEEN @FECHA_INICIAL AND @FECHA_FINAL) OR  (INP.IPCODPACI = @ID)
ORDER BY SLI.Code	
END