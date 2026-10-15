

CREATE PROCEDURE [dbo].[SP_AD_ListarIngresos]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
--SELECT CAST(0 AS BIT) AS 'CHECK',A.NUMINGRES AS 'Numero del Ingreso', A.IPCODPACI AS 'Identificacion', B.IPPRINOMB AS 'Primer Nombre',B.IPSEGNOMB AS 'Segundo Nombre',B.IPPRIAPEL AS 'Primer Apellido',B.IPSEGAPEL AS 'Segundo Apellido', C.NOMENTIDA AS 'Entidad'
SELECT  A.IPCODPACI AS 'Identificacion'
FROM ADINGRESO A
INNER JOIN INPACIENT B ON B.IPCODPACI=A.IPCODPACI
INNER JOIN INENTIDAD C ON C.CODENTIDA=B.CODENTIDA


END