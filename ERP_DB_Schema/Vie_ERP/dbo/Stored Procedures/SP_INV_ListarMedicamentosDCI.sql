

CREATE PROCEDURE [dbo].[SP_INV_ListarMedicamentosDCI]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    SELECT CODDCIMED AS 'CODIGO DCI', RTRIM(DESDCIMED) AS DESCRIPCION,RTRIM(CODDCIMED) +' - '+ RTRIM(DESDCIMED) AS DCI
       FROM IHDCIMEDI

END