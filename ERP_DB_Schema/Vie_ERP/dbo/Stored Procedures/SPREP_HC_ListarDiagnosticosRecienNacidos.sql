

CREATE PROCEDURE [dbo].[SPREP_HC_ListarDiagnosticosRecienNacidos]
(
@ConsecutivoRecienNacido int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT A.CODDIAGNO AS 'CODIGO DIAGNOSTICO', B.NOMDIAGNO AS DIAGNOSTICO
FROM HCRECNADI A WITH(NOLOCK)
INNER JOIN INDIAGNOS B ON A.CODDIAGNO=B.CODDIAGNO
WHERE A.CONSECREC = @ConsecutivoRecienNacido

END