-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,21-05-2018,>
-- Description:	<Description,Sp que me lista los pacientes que tienen ficha del Sivigila diligenciadas, esto para el Dashboard de Epidemiologia>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarPacientesFichaSivigilaDiligenciadas]
(
  @CentroAtencion as varchar(MAX),
  @VersionERP int
)

AS
  SET NOCOUNT ON;

   declare @Sql as nvarchar(max)

IF @VersionERP = 0  BEGIN ---Vie

  set 	@Sql = ' select C.ID,INGRECEXT as ''Remitido'',isnull(DESTINOPAC,0) as ''Agrupacion'',cast('' '' as char(100)) AS ''Origen'',FECHACREACION As ''Fecha notificacion'',Rtrim(C.IPCODPACI) As ''Identificacion'', Rtrim(B.IPNOMCOMP) As ''Nombre Paciente'',cast(datediff(dd,B.IPFECNACI,[Common].[GETDATE]()) / 365.25 as int) As ''Edad'',
			Rtrim(heal.Name) As ''Entidad'',Rtrim(E.NOMCENATE) As ''Centro Atencion'',Rtrim(F.UFUDESCRI) As ''Unidad Funcional'',Rtrim(C.NOMBEVENTO) As ''Nombre Evento'', Rtrim(C.CODEVENTO) As ''Codigo Evento'', CASE RTRIM(CODEVENTO) WHEN ''356_D'' THEN ''356'' WHEN ''875_D'' THEN ''875''  WHEN ''903_D'' THEN ''903'' ELSE RTRIM(CODEVENTO) END AS CodigoVisible , 
			CASE CLASIFICACIONCASO WHEN 1 THEN ''Sospechoso'' WHEN 2 THEN ''Probable'' WHEN 3 THEN ''Conf laboratorio''  WHEN 4 THEN ''Conf clinica'' WHEN 5 THEN ''Conf epidemiológico'' END AS ''Clasificacion'',
			Rtrim(G.NOMMEDICO) As ''Nombre Medico'',Rtrim(C.CODCENATE) as ''Codigo CA'',Rtrim(C.UFUCODIGO) As ''Codigo UF'',Rtrim(C.NUMINGRES) As ''Ingreso'',Rtrim(C.CODDIAGNO) As ''Codigo Diagnostico'',Rtrim(Z.DESCCAMAS) as ''Cama'',UFUACTPAC
			From HCFICHANOTIFICACION C
			  Inner Join INPACIENT B ON C.IPCODPACI = B.IPCODPACI  
			  Inner Join ADINGRESO D ON C.NUMINGRES = D.NUMINGRES
			  Inner join Contract.HealthAdministrator heal on heal.Id = D.GENCONENTITY
			  Inner Join ADCENATEN E ON C.CODCENATE = E.CODCENATE
			  Inner Join INUNIFUNC F ON C.UFUCODIGO = F.UFUCODIGO
			  Left Join CHCAMASHO z on z.codicamas = D.CODCAMACT
			  Inner Join INPROFSAL G ON C.CODUSUARIO = G.CODPROSAL 
	   where C.ESTADO = 1 AND convert(varchar(MAX),C.CODCENATE) IN (' + @CentroAtencion + ') order by c.IPCODPACI  '

  exec sp_executesql @Sql 
  
END ELSE  BEGIN --Otro ERP

  set 	@Sql = ' select C.ID,INGRECEXT as ''Remitido'',isnull(DESTINOPAC,0) as ''Agrupacion'',cast('' '' as char(100)) AS ''Origen'',FECHACREACION As ''Fecha notificacion'',Rtrim(C.IPCODPACI) As ''Identificacion'', Rtrim(B.IPNOMCOMP) As ''Nombre Paciente'',cast(datediff(dd,B.IPFECNACI,[Common].[GETDATE]()) / 365.25 as int) As ''Edad'',
					Rtrim(NOMENTADM) As ''Entidad'',Rtrim(E.NOMCENATE) As ''Centro Atencion'',Rtrim(F.UFUDESCRI) As ''Unidad Funcional'',Rtrim(C.NOMBEVENTO) As ''Nombre Evento'', Rtrim(C.CODEVENTO) As ''Codigo Evento'', CASE RTRIM(CODEVENTO) WHEN ''356_D'' THEN ''356'' WHEN ''875_D'' THEN ''875''  WHEN ''903_D'' THEN ''903'' ELSE RTRIM(CODEVENTO) END AS CodigoVisible , 
					CASE CLASIFICACIONCASO WHEN 1 THEN ''Sospechoso'' WHEN 2 THEN ''Probable'' WHEN 3 THEN ''Conf laboratorio''  WHEN 4 THEN ''Conf clinica'' WHEN 5 THEN ''Conf epidemiológico'' END AS ''Clasificacion'',
					Rtrim(G.NOMMEDICO) As ''Nombre Medico'',Rtrim(C.CODCENATE) as ''Codigo CA'',Rtrim(C.UFUCODIGO) As ''Codigo UF'',Rtrim(C.NUMINGRES) As ''Ingreso'',Rtrim(C.CODDIAGNO) As ''Codigo Diagnostico'',Rtrim(Z.DESCCAMAS) as ''Cama'',UFUACTPAC
					From HCFICHANOTIFICACION C
					   Inner Join INPACIENT B ON C.IPCODPACI = B.IPCODPACI  
					  Inner Join ADINGRESO D ON C.NUMINGRES = D.NUMINGRES
					  INNER join dbo.INENTADM X on X.CODENTADM = C.CODENTIDA
					  Inner Join ADCENATEN E ON C.CODCENATE = E.CODCENATE
					  Inner Join INUNIFUNC F ON C.UFUCODIGO = F.UFUCODIGO
					  Left Join CHCAMASHO z on z.codicamas = D.CODCAMACT
					  Inner Join INPROFSAL G ON C.CODUSUARIO = G.CODPROSAL 
	    where C.ESTADO = 1 AND convert(varchar(MAX),C.CODCENATE) IN (' + @CentroAtencion + ') order by c.IPCODPACI  '


  exec sp_executesql @Sql 

END