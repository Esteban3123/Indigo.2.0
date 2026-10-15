
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Citas_en_Espera] 
	-- Add the parameters for the stored procedure here
	@Fecha_Inic Date,
	@Fecha_Fin Date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT        
cast (AGC.FECREGSIS as date) AS Fecha_solicitud,
AGC.FECHACITA AS Fecha_Deseada,  
INPA.IPNOMCOMP AS Paciente, 
AGC.IPCODPACI AS Identificacion, 
INEN.NOMENTIDA AS Entidad, 
INPA.IPTELEFON AS Tel1, INPA.IPTELMOVI AS Tel2, INPA.IPDIRECCI AS Direccion, 
INES.DESESPECI AS Especialidad, 
AGAC.DESACTMED AS Acitividad,
INP.NOMMEDICO AS Médico, 
AGC.OBSERVACI AS Observaciones, 
AGC.CITAASIGN AS Se_asignó, '1' as cont,
CASE AGC.ESTADO 
WHEN 1 THEN 'En Espera'
WHEN 2 THEN 'Asignada'
WHEN 3 THEN 'Cancelada'
END AS 'ESTADO-CITA'
FROM            dbo.AGCITAESP AS AGC INNER JOIN
                         dbo.INPACIENT AS INPA ON AGC.IPCODPACI = INPA.IPCODPACI INNER JOIN
                         dbo.INESPECIA AS INES ON AGC.CODESPECI = INES.CODESPECI INNER JOIN
                         dbo.INENTIDAD AS INEN ON INPA.CODENTIDA = INEN.CODENTIDA INNER JOIN
                         dbo.AGACTIMED AS AGAC ON AGC.CODACTMED = AGAC.CODACTMED LEFT OUTER JOIN
                         dbo.INPROFSAL AS INP ON AGC.CODPROSAL = INP.CODPROSAL
WHERE (AGC.FECREGSIS BETWEEN @Fecha_Inic AND @Fecha_Fin) ORDER BY AGC.FECREGSIS asc 
END