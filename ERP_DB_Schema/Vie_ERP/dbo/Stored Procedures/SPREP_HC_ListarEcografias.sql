
CREATE PROCEDURE [dbo].[SPREP_HC_ListarEcografias]
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
SELECT  NUMEFOLIO AS 'NUMERO FOLIO',UltrasoundObservations,
        UltrasoundForEstimatedDueDate,Percentile, Size,
		FECULTECO AS 'FECHA ULTIMA ECOGRAFIA',
		UpcomingDateOfParturitionByUltrasound, 
		concat(NUMSEMULT,'  semanas')  AS 'SEMANAS EN ULTIMA ECOGRAFIA', 
		FECHISPAC AS 'FECHA DE HISTORIA', 
         CONCAT(NUMSEMHIS,'  semanas') AS 'SEMANAS ACTUALES',
		 Weight AS peso,AmnioticFluidIndex ,MaximumVerticalPocket
FROM HCANTECOG With(Nolock)
WHERE NUMEFOLIO = @NumeroFolio AND IPCODPACI = @CodigoPaciente AND NUMINGRES=@NumeroIngreso

END