
CREATE PROCEDURE [dbo].[SP_HC_ListarFoliosPendientesValidarHCTelefonicas]
(
@INDPacient as varchar(25),
@CentroAtencion Char(10) ,
@UFuncional char(20)  ,
@Profesional as char(20),
@DashboardEnfermeria as bit
)
AS
BEGIN
	SET NOCOUNT ON;

if @DashboardEnfermeria = 1 begin

Select     cast(0 as bit) as Seleccionar,
			   HC.ID
			  ,	HC.FECHISPAC as FechaHistoria
			  , RTRIM(Q.CODDIAGNO) + '-' + RTRIM(Q.NOMDIAGNO) as Diagnostico
			  , case hc.INDICATION when 1 then 'Telefónica' when 2 then 'Verbal' when 3 then 'Escrita' when 4 then 'Resultado crítico' end as TipoIndicacion
			  , Rtrim(t.NOMMEDICO) as NombreProfesionalAsisteshc
			  , G.DESESPECI AS EspecialidadProfesionalAsistehc
			  , X.UFUDESCRI AS UnidadFuncionadelFolio
			  , HC.NUMEFOLIO as Folio			  
			  , HC.IPCODPACI as Paciente				  
			  , HC.NUMINGRES as Ingreso
			  , case PendingValidationHCTelephone when 0 then 'Sin firmar' when 1 then 'Firmados' end as Estado, HC.PendingValidationHCTelephone
	from HCHISPACA HC with(nolock)
					INNER JOIN INPACIENT I with(nolock) on hc.IPCODPACI = I.IPCODPACI 
					INNER JOIN ADINGRESO N with(nolock) ON N.NUMINGRES = hc.NUMINGRES
					INNER JOIN INUNIFUNC Z with(nolock) ON  Z.UFUCODIGO  = hc.UFUCODIGO
					INNER JOIN INUNIFUNC X ON X.UFUCODIGO = HC.UFUCODIGO 
					INNER JOIN INPROFSAL T ON T.CODPROSAL = HC.MEDSOLHCTELEF 				
					INNER JOIN INESPECIA G ON G.CODESPECI = HC.ESPMEDSOLHCTELEF 
					LEFT OUTER JOIN INDIAGNOS Q with (nolock) ON Q.CODDIAGNO = (SELECT TOP 1 CODDIAGNO FROM INDIAGNOP WHERE IPCODPACI = I.IPCODPACI AND NUMINGRES = N.NUMINGRES AND CODDIAPRI = 1)  
   WHERE hc.IPCODPACI = @INDPacient and HC.HCTELEFONICA = 1 and HC.PendingValidationHCTelephone in(0,1) --and hc.CODPROSAL = @Profesional

end else begin

Select     cast(0 as bit) as Seleccionar,
			   HC.ID
			  ,	HC.FECHISPAC as FechaHistoria
			  , RTRIM(Q.CODDIAGNO) + '-' + RTRIM(Q.NOMDIAGNO) as Diagnostico
			  , case hc.INDICATION when 1 then 'Telefonica' when 2 then 'Verbal' when 3 then 'Escrita' when 4 then 'Resultado crítico' end as TipoIndicacion
			  , Rtrim(t.NOMMEDICO) as NombreProfesionalAsisteshc
			  , G.DESESPECI AS EspecialidadProfesionalAsistehc
			  , X.UFUDESCRI AS UnidadFuncionadelFolio
			  , HC.NUMEFOLIO as Folio			  
			  , HC.IPCODPACI as Paciente				  
			  , HC.NUMINGRES as Ingreso
			  , case PendingValidationHCTelephone when 0 then 'Sin firmar' when 1 then 'Firmados' end as Estado, HC.PendingValidationHCTelephone
	from HCHISPACA HC with(nolock)
					INNER JOIN INPACIENT I with(nolock) on hc.IPCODPACI = I.IPCODPACI 
					INNER JOIN ADINGRESO N with(nolock) ON N.NUMINGRES = hc.NUMINGRES
					INNER JOIN INUNIFUNC Z with(nolock) ON  Z.UFUCODIGO  = hc.UFUCODIGO
					INNER JOIN INUNIFUNC X ON X.UFUCODIGO = HC.UFUCODIGO 
					INNER JOIN INPROFSAL T ON T.CODPROSAL = HC.CODPROSAL 				
					INNER JOIN INESPECIA G ON G.CODESPECI = HC.CODESPTRA
					LEFT OUTER JOIN INDIAGNOS Q with (nolock) ON Q.CODDIAGNO = (SELECT TOP 1 CODDIAGNO FROM INDIAGNOP WHERE IPCODPACI = I.IPCODPACI AND NUMINGRES = N.NUMINGRES AND CODDIAPRI = 1)  
   WHERE hc.IPCODPACI = @INDPacient and HC.HCTELEFONICA = 1 and HC.PendingValidationHCTelephone in(0,1) and hc.MEDSOLHCTELEF = @Profesional
   end
end