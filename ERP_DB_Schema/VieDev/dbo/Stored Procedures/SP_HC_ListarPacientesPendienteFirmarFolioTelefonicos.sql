

-- =============================================
-- Author:		
-- Create date: 14/12/2022
-- Description:	SP que lista los pacientes Que Tiene folios pendientes de firmar.
-- =============================================

CREATE PROCEDURE [dbo].[SP_HC_ListarPacientesPendienteFirmarFolioTelefonicos]
(
@CentroAtencion Char(10) ,
@UFuncional char(20),
@Profesional as char(20),
@DashboardEnfermeria as bit
)
AS
BEGIN
	SET NOCOUNT ON;

if @DashboardEnfermeria = 1 begin

	Select distinct N.CODTIPPAC as TipoPaciente, I.IPCODPACI AS Identificacion, RTRIM(I.IPNOMCOMP) AS NombreCompleto, IPFECNACI AS FechaNacimiento , 
			RTRIM(O.NOMENTIDA) as EntidadPaciente, RTRIM(Q.CODDIAGNO) + '-' + RTRIM(Q.NOMDIAGNO) as Diagnostico, N.NUMINGRES AS Ingreso
	from HCHISPACA HC with(nolock)
					INNER JOIN INPACIENT I with(nolock) on hc.IPCODPACI = I.IPCODPACI 
					INNER JOIN ADINGRESO N with(nolock) ON N.NUMINGRES = hc.NUMINGRES
					INNER JOIN INUNIFUNC Z with(nolock) ON  Z.UFUCODIGO  = hc.UFUCODIGO
					INNER JOIN INENTIDAD O with(nolock) ON O.CODENTIDA = N.CODENTIDA 
					LEFT OUTER JOIN INDIAGNOS Q with (nolock) ON Q.CODDIAGNO = (SELECT TOP 1 CODDIAGNO FROM INDIAGNOP WHERE IPCODPACI = I.IPCODPACI AND NUMINGRES = N.NUMINGRES AND CODDIAPRI = 1)  
   WHERE HC.HCTELEFONICA = 1 and HC.PendingValidationHCTelephone = 0 and hc.CODCENATE = @CentroAtencion and hc.UFUCODIGO = @UFuncional --and  hc.CODPROSAL = @Profesional

end else begin

	Select distinct N.CODTIPPAC as TipoPaciente, I.IPCODPACI AS Identificacion, RTRIM(I.IPNOMCOMP) AS NombreCompleto, IPFECNACI AS FechaNacimiento , 
			RTRIM(O.NOMENTIDA) as EntidadPaciente, RTRIM(Q.CODDIAGNO) + '-' + RTRIM(Q.NOMDIAGNO) as Diagnostico, N.NUMINGRES AS Ingreso
	from HCHISPACA HC with(nolock)
					INNER JOIN INPACIENT I with(nolock) on hc.IPCODPACI = I.IPCODPACI 
					INNER JOIN ADINGRESO N with(nolock) ON N.NUMINGRES = hc.NUMINGRES
					INNER JOIN INUNIFUNC Z with(nolock) ON  Z.UFUCODIGO  = hc.UFUCODIGO
					INNER JOIN INENTIDAD O with(nolock) ON O.CODENTIDA = N.CODENTIDA 
					LEFT OUTER JOIN INDIAGNOS Q with (nolock) ON Q.CODDIAGNO = (SELECT TOP 1 CODDIAGNO FROM INDIAGNOP WHERE IPCODPACI = I.IPCODPACI AND NUMINGRES = N.NUMINGRES AND CODDIAPRI = 1)  
   WHERE HC.HCTELEFONICA = 1 and HC.PendingValidationHCTelephone = 0 and hc.CODCENATE = @CentroAtencion and hc.UFUCODIGO = @UFuncional and  hc.MEDSOLHCTELEF = @Profesional

	end
END