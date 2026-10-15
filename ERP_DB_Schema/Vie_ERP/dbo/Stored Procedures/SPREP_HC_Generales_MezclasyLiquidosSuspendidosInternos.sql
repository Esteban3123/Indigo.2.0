
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_MezclasyLiquidosSuspendidosInternos]
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
          
SELECT RTRIM(MEZLIQPAC) AS 'MEZCLAS Y LIQUIDOS',RTRIM(ADMMEZLIQ) AS 'ADMINISTRACION MEZCLAS Y LIQUIDOS',RTRIM(MOTSUSMED) AS 'MOTIVO DE SUSPENSION',CODCENATE,UFUCODIGO,IPCODPACI
                   
FROM HCINFLIQI WITH(NOLOCK)
WHERE IPCODPACI=@CodigoPaciente AND NUMINGRES=@NumeroIngreso AND NUMFOLSUS=@NumeroFolio AND PREESTADO='4' AND IDETIPHIS<>'CODIGOAZU'

END