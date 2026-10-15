
CREATE PROCEDURE [dbo].[SPHC_ListarLaboratoriosApache]
(
@CodigoPaciente varchar(25),
@NumeroIngreso char(10)
)
AS
BEGIN
	SET NOCOUNT ON;

	---Consecutivo orden de laboratorio con resultado, estado 3 
	DECLARE @CODIGOORDEN AS INT
	SELECT
			TOP 1
			@CODIGOORDEN = AUTO 
		FROM
			HCORDLABO
		WHERE 
			IPCODPACI =@CodigoPaciente AND NUMINGRES = @NumeroIngreso
		ORDER BY
			AUTO DESC

	--Obtener los resultados de laboratorio por el consecutivo
	SELECT ANALITO, VALOR FROM INTERLABD WHERE AUTOLABOR =3837
END