
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Control_Inacistencia]
	-- Add the parameters for the stored procedure here
	@FECHA_INICIAL AS DATETIME,
	@FECHA_FINAL AS DATETIME

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT        
dbo.TipDocR256(INP.IPTIPODOC) AS TipoDocumen, 
INP.IPCODPACI AS Identificacion, 
INP.IPNOMCOMP AS  Nombre,
AGA.FECHORAIN AS FechaConsulta, 
case AGA.CODESTCIT
when 0 then  'Asignada'  
when 1 then  'Cumplida' 
when 2 then  'Incumplida' 
when 3 then  'PreAsignada'
when 4 then  'Cita Cancelada'
end as 'ESTADO-CITA',
INE.NOMENTIDA AS Entidad,
CASE AGA.CODTIPCIT
WHEN 0 THEN 'Primera Vez' 
WHEN 1 THEN 'Control' 
WHEN 2 THEN 'PostOperatorio' 
END AS Tipo,
INPR.CODPROSAL AS CodMedico, 
INPR.NOMMEDICO AS NombreMedico,
INES.DESESPECI AS Especialidad, 
dbo.SexoR256(INP.IPSEXOPAC) AS Sexo, 
cast(INP.IPFECNACI as date)AS FNacimiento,
 dbo.Edad(CONVERT(varchar, INP.IPFECNACI, 105),CONVERT(varchar, AGA.FECHORAIN, 105))AS Edad,
dbo.Etarios(INP.IPFECNACI,AGA.FECHORAIN)  AS GrupoEtario1,
dbo.EtariosR248(INP.IPFECNACI,AGA.FECHORAIN)AS GrupoEtario2,
dbo.TipoPaciente(IPTIPOPAC)  AS TipoRegimen,
--INP.IPPRINOMB AS 'PRIMER NOMBRE',
--INP.IPSEGNOMB AS 'SEGUNDO NOMBRE',
--INP.IPPRIAPEL AS 'PRIMER APELLIDO',
--INP.IPSEGAPEL AS 'SEGUNDO APELLIDO',

INP.IPDIRECCI AS Direccion,
INMU.MUNNOMBRE AS Municipio,
AGAC.DESACTMED AS Acitvidad
FROM            dbo.INPACIENT AS INP INNER JOIN
                         dbo.INENTIDAD AS INE ON INP.CODENTIDA = INE.CODENTIDA INNER JOIN
                         dbo.AGASICITA AS AGA ON INP.IPCODPACI = AGA.IPCODPACI INNER JOIN
                         dbo.INPROFSAL AS INPR ON AGA.CODPROSAL = INPR.CODPROSAL INNER JOIN
                         dbo.INESPECIA AS INES ON AGA.CODESPECI= INES.CODESPECI INNER JOIN
                         dbo.INUBICACI AS INU ON INP.AUUBICACI = INU.AUUBICACI INNER JOIN
                         dbo.INMUNICIP AS INMU ON INU.DEPMUNCOD = INMU.DEPMUNCOD INNER JOIN
                         dbo.AGACTIMED AS AGAC ON AGA.CODACTMED = AGAC.CODACTMED LEFT OUTER JOIN
                         dbo.AGCITESPE AS AGCI ON AGA.CODESPECI = AGCI.CODESPECI AND AGA.IPCODPACI = AGCI.IPCODPACI
WHERE        (AGA.FECHORAIN BETWEEN @FECHA_INICIAL AND @FECHA_FINAL)
ORDER BY AGA.FECHORAIN ASC
END