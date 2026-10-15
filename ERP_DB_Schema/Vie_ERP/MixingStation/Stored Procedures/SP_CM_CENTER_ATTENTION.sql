-- =============================================
-- Author:		HECTOR RODRIGUEZ
-- Create date: 25-09-2019
-- Description:	Consulta centros de atencion de una central de mezcla
-- =============================================
CREATE PROCEDURE [MixingStation].[SP_CM_CENTER_ATTENTION]
	@IdMixingStation	int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT DISTINCT
	CodeCenterAttention = RTRIM(CMCA.CodeCenterAttention)
	, NameCenterAttention = RTRIM(CA.NOMCENATE)
	, Ubicacion = CASE WHEN C.Code IS NULL THEN CA.DEPMUNCOD ELSE C.Code + ' - ' + C.Name END
	FROM MixingStation.CMCenterAttention CMCA
	INNER JOIN dbo.ADCENATEN CA
	ON CA.CODCENATE = CMCA.CodeCenterAttention
	LEFT OUTER JOIN Common.City C
	ON C.Code = CA.DEPMUNCOD
	WHERE CMCA.IdMixingStation = @IdMixingStation

END