
CREATE PROCEDURE [dbo].[SP_HC_ListAdministrationRoute]
( @Medicamento VARCHAR(100)
) 
AS 
BEGIN
	SET NOCOUNT ON;

	SELECT RTRIM(ltrim(VA.CODVIAADM)) AS Codigo, RTRIM(VA.DESVIAADM) AS Via 
	FROM Inventory.ATC ATC WITH(NOLOCK)
	INNER JOIN Inventory.ATCAdministrationRoute ATCAR  WITH(NOLOCK)
	ON ATCAR.ATCId = ATC.Id
	INNER JOIN Inventory.AdministrationRoute AR  WITH(NOLOCK)
	ON AR.Id = ATCAR.AdministrationRouteId
	AND AR.[Status] = 1
	INNER JOIN dbo.HCVIAADMI VA  WITH(NOLOCK)
	ON VA.CODVIAADM = AR.CrystalAdministrationRoute
	WHERE ATC.Code in (@Medicamento)
END