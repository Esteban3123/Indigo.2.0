
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_DiagnosticosHistorico]
(
@CodigoPaciente Varchar(25),
@NumeroFolio nChar(10),
@NumeroIngreso Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
          
SELECT A.CODDIAGNO AS 'CODIGO DEL DIAGNOSTICO', NOMDIAGNO AS 'NOMBRE DEL DIAGNOSTICO', 
	   CAST(CODDIAPRI AS BIT) AS 'CODIGO DEL DIAGNOSTICO PRINCIPAL',RTRIM(OBSDIAGNO) AS 'OBSERVACIONES DEL DIAGNOSTICO',
	   CASE A.TIPDIAGNO WHEN 'I' THEN 'Impresión diagnóstica' WHEN 'C' THEN 'Confirmado nuevo' WHEN 'R' THEN 'Confirmado repetido'  END AS 'TIPO DIAGNOSTICO'
FROM INDIAGNOH A With(Nolock)
INNER JOIN INDIAGNOS B With(Nolock) ON A.CODDIAGNO=B.CODDIAGNO 
WHERE IPCODPACI=@CodigoPaciente AND NUMINGRES=@NumeroIngreso AND NUMEFOLIO=@NumeroFolio

END