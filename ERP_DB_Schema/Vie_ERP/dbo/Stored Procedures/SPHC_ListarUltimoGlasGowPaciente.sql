
-- =============================================
-- Author:		<Juan Montealegre>
-- Create date: <28/03/2014>
-- Description:	<Valor GlasGow>
-- =============================================
CREATE PROCEDURE [dbo].[SPHC_ListarUltimoGlasGowPaciente]
(
@CodigoPaciente varchar(25),
@NumeroIngreso Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    SELECT TOP 1  VALGLASGO  FROM HCCTRNEUR WHERE IPCODPACI = @CodigoPaciente AND NUMINGRES = @NumeroIngreso ORDER BY FECREGIST DESC 
END