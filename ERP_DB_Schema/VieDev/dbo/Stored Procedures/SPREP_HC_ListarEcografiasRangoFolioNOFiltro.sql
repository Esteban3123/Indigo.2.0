
CREATE PROCEDURE [dbo].[SPREP_HC_ListarEcografiasRangoFolioNOFiltro]
(
@CodigoPaciente Varchar(25),
@NumeroFolioInicial int,
@NumeroFolioFinal int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT  
	NUMEFOLIO AS 'NUMERO FOLIO',FECULTECO AS 'FECHA ULTIMA ECOGRAFIA', 
	concat(NUMSEMULT,'  Semanas') AS 'SEMANAS EN ULTIMA ECOGRAFIA', 
	FECHISPAC AS 'FECHA DE HISTORIA', CONCAT(NUMSEMHIS,'  Semanas') AS 'SEMANAS ACTUALES',
    UltrasoundObservations,UltrasoundForEstimatedDueDate,Percentile, Size,UpcomingDateOfParturitionByUltrasound,
	Weight AS peso,AmnioticFluidIndex ,MaximumVerticalPocket
FROM HCANTECOG WITH(NOLOCK)

WHERE IPCODPACI = @CodigoPaciente AND NUMEFOLIO BETWEEN @NumeroFolioInicial AND @NumeroFolioFinal

END