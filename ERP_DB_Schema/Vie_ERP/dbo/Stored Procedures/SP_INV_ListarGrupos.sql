
CREATE PROCEDURE [dbo].[SP_INV_ListarGrupos]
(
@VersionERP int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    
IF @VersionERP=1
    
    SELECT RTRIM(IGRCODIGO) AS 'CODIGO DEL GRUPO',IGRNOMBRE AS 'NOMBRE DEL GRUPO', RTRIM(IGRCODIGO) + ' - ' + RTRIM(IGRNOMBRE) AS 'GRUPO'
    FROM DBO.INGRUPOS
       
    ELSE
        
    SELECT OID ,RTRIM(IGRCODIGO) AS 'CODIGO DEL GRUPO',IGRNOMBRE AS 'NOMBRE DEL GRUPO', RTRIM(IGRCODIGO) + ' - ' + RTRIM(IGRNOMBRE) AS 'GRUPO'
    FROM DBO.INNGRUPO

END