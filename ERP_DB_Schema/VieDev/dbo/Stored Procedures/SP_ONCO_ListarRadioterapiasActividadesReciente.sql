
CREATE PROCEDURE [dbo].[SP_ONCO_ListarRadioterapiasActividadesReciente]
(
@Identificacion Varchar(25)
)
AS
BEGIN
	SET NOCOUNT ON;

select 
			
			A.ID, ES.ID AS 'IDESQUEMA',A.CODCENATE, ORD.NUMINGRES ,
			rtrim(F.CODPROSAL) as CODPROSAL,
			rtrim(A.CODSERIPS) as CODSERIPS,
			Rtrim(A.IPCODPACI) As Identificacion, A.FECHAORDEN,
			Rtrim(IPNOMCOMP) As NombrePaciente,
			Rtrim(E.CODSERIPS) +' - '+ Rtrim(DESSERIPS) As Procedimiento,
			Rtrim(CE.NOMCENATE ) as CentroAtencion,
			Rtrim(F.NOMMEDICO) as Profesional,
			Rtrim(D.CODDIAGNO) + ' - ' + Rtrim(D.NOMDIAGNO ) as Diagnostico,
			Rtrim(D.CODDIAGNO) as CodigoDiagnostico,
			Rtrim(ESP.DESESPECI) as Especialidad,
			CASE ORD.MANEXTPRO when 1 then 'Ambulatoria' else 'Hospitalario' END as OrigenOrden,
			E.SERIPSDASH as 'TipoServicio',
			Case 
			WHEN  A.ESTADO = 3 Then 'Simulación Realizada' 
			when (A.ESTADO = 4 and ES.ESTADO = 1)  Then 'Planeación Por Realizar' 
			when (A.ESTADO = 4 and ES.ESTADO = 2)  Then 'Planeación Confirmada' 
			when (A.ESTADO = 4 and ES.ESTADO = 3)  Then 'Esquema Iniciado' 
			when (A.ESTADO = 5)  Then 'Finalizado' 
			when (A.ESTADO = 6 or ES.ESTADO = 5 )  Then 'Anulado' 
			when (A.ESTADO = 7)  Then 'Completado' 
			End as 'Estado Esquema',
			ES.DOSTOTAL,ES.NUMSESION, ES.DOSISPORSESION,ORD.NUMINGRES
		from HCRADORDEN A 
		   left JOIN HCRADESQUEMAS ES with(nolock) ON A.ID = ES.IDHCRADORDEN
			INNER JOIN HCORDPRON ORD with(nolock) ON A.IDHCORDPRON = ORD.AUTO
			INNER JOIN INCUPSIPS  E with(nolock) ON A.CODSERIPS = E.CODSERIPS AND E.SERIPSDASH  = 6 --Radioterapia Externa
			INNER JOIN INPACIENT B ON A.IPCODPACI = B.IPCODPACI 
			INNER JOIN ADCENATEN CE with(nolock) ON CE.CODCENATE = A.CODCENATE
			INNER JOIN INPROFSAL   F with(nolock) ON A.CODPROSAL  = F.CODPROSAL
			INNER JOIN INDIAGNOS  D with(nolock) ON A.CODDIAGNO = D.CODDIAGNO
			INNER JOIN INESPECIA ESP with(nolock) ON ESP.CODESPECI  = A.CODESPECI
		WHERE A.IPCODPACI = @Identificacion and A.ESTADO IN (3,4,5,6,7)  
     
end