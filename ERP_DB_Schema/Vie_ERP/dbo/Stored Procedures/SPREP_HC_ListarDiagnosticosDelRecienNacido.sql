
-- =============================================
-- Author:		<Juan Gilberto Montealegre>
-- Create date: <12/12/2013>
-- Description:	<Procedimiento para listar los diganosticos del recien nacido>
-- =============================================
CREATE PROCEDURE [dbo].[SPREP_HC_ListarDiagnosticosDelRecienNacido]
	-- Add the parameters for the stored procedure here
	@CodigoPaciente varchar(25),
	@NumeroIngreso char(10),
	@NumeroFolio char(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
			D.CONSECREC 'CONSECUTIVO',  ----- D.NUMCONSEC
			D.CODDIAGNO 'CODIGO DIAGNOSTICO',
			E.NOMDIAGNO 'NOMBRE DIAGNOSTICO' 
		FROM
			HCRECNADI D WITH(NOLOCK)
			INNER JOIN
			HCRECINAC  A WITH(NOLOCK)
			ON D.CONSECREC = A.NUMCONSEC   ---D.NUMCONSEC
			INNER JOIN 
			INDIAGNOS E WITH(NOLOCK)
			ON D.CODDIAGNO =  E.CODDIAGNO 

			WHERE A.IPCODPACI = @CodigoPaciente AND A.NUMEFOLIO =@NumeroFolio AND A.NUMINGRES=@NumeroIngreso
END