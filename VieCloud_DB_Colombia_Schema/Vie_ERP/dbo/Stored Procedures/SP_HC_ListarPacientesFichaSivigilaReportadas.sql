-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,21-05-2018,>
-- Description:	<Description,Sp que me lista los pacientes que tienen ficha del Sivigila Reportadas, esto para el Dashboard de Epidemiologia>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarPacientesFichaSivigilaReportadas]
(
  @CentroAtencion as varchar(max),
  @VersionERP int
)

AS
  SET NOCOUNT ON;

   declare @Sql as nvarchar(max)

IF @VersionERP = 0  BEGIN ---Vie

  set 	@Sql = ' select C.ID,INGRECEXT as ''Remitido'',isnull(DESTINOPAC,0) as ''Agrupacion'',cast('' '' as char(100)) AS ''Origen'',FECHACREACION As ''Fecha notificacion'',FECHAREPORSIVIGILA As ''Fecha Reportada'',Rtrim(C.IPCODPACI) As ''Identificacion'', Rtrim(B.IPNOMCOMP) As ''Nombre Paciente'',cast(datediff(dd,B.IPFECNACI,[Common].[GETDATE]()) / 365.25 as int) As ''Edad'',
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
	   where C.ESTADO = 2 AND convert(varchar(MAX),C.CODCENATE) IN (' + @CentroAtencion + ') order by c.IPCODPACI  '

  exec sp_executesql @Sql 
  
END ELSE  BEGIN --Otro ERP

  set 	@Sql = ' select C.ID,INGRECEXT as ''Remitido'',isnull(DESTINOPAC,0) as ''Agrupacion'',cast('' '' as char(100)) AS ''Origen'',FECHACREACION As ''Fecha notificacion'',FECHAREPORSIVIGILA As ''Fecha Reportada'',Rtrim(C.IPCODPACI) As ''Identificacion'', Rtrim(B.IPNOMCOMP) As ''Nombre Paciente'',cast(datediff(dd,B.IPFECNACI,[Common].[GETDATE]()) / 365.25 as int) As ''Edad'',
					Rtrim(NOMENTADM) As ''Entidad'',Rtrim(E.NOMCENATE) As ''Centro Atencion'',Rtrim(F.UFUDESCRI) As ''Unidad Funcional'',Rtrim(C.NOMBEVENTO) As ''Nombre Evento'', Rtrim(C.CODEVENTO) As ''Codigo Evento'',CASE RTRIM(CODEVENTO) WHEN ''356_D'' THEN ''356'' WHEN ''875_D'' THEN ''875''  WHEN ''903_D'' THEN ''903'' ELSE RTRIM(CODEVENTO) END AS CodigoVisible , 
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
	    where C.ESTADO = 2 AND convert(varchar(MAX),C.CODCENATE) IN (' + @CentroAtencion + ') order by c.IPCODPACI  '

  exec sp_executesql @Sql 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes que tienen fichas de notificación SIVIGILA en estado Reportado (estado=2), para uso en el Dashboard de Epidemiología. Por cada paciente muestra su identificación/cédula, nombre, edad, ingreso hospitalario, cama asignada, entidad administradora de salud, centro de atención, unidad funcional, médico tratante, nombre y código del evento epidemiológico, clasificación del caso (sospechoso, probable, confirmado por laboratorio, clínica o epidemiología), fecha de notificación y fecha de reporte al SIVIGILA. Recibe un centro de atención y una versión del ERP como parámetros: si la versión es 0 (Indigo Vie) obtiene la entidad desde el módulo Contract, mientras que para otras versiones la toma de la tabla de entidades administradoras INENTADM. Construye SQL dinámico filtrando por uno o varios centros de atención, por lo que se usa principalmente para reportería y vigilancia epidemiológica institucional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarPacientesFichaSivigilaReportadas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarPacientesFichaSivigilaReportadas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las fichas de notificación Sivigila ya reportadas (estado reportada) de los pacientes atendidos en los centros de atención indicados, con datos clínicos y administrativos para el dashboard de epidemiología.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaReportadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un código de centro de atención válido y correctamente formateado para inyección en cláusula IN; La ficha debe estar asociada a un ingreso, paciente, centro de atención, unidad funcional y profesional existentes (joins internos); Para versión ERP=0 debe existir el contrato de administradora en Contract.HealthAdministrator vinculado al ingreso; para otras versiones debe existir la entidad en INENTADM', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaReportadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan fichas de notificación con ESTADO = 2 (interpretado como reportadas a Sivigila); El listado se restringe a los centros de atención indicados en el filtro recibido; La edad se calcula en años cumplidos a partir de la fecha de nacimiento dividiendo días entre 365.25; Los códigos de evento con sufijo ''_D'' se normalizan al código base para visualización; El resultado se ordena por identificación del paciente; Si el paciente no tiene cama asignada (LEFT JOIN), igualmente aparece en el reporte', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaReportadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación Sivigila; Vigilancia epidemiológica; Evento de salud pública; Clasificación de caso (sospechoso/probable/confirmado); Paciente; Ingreso hospitalario; Centro de atención; Unidad funcional; Entidad administradora de salud (EPS); Cama hospitalaria; Profesional de la salud / Médico notificador', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaReportadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHANOTIFICACION: Cuando ESTADO = 2 y CODCENATE está en la lista de centros recibidos, devuelve la ficha enriquecida con datos de paciente, ingreso, entidad, centro, unidad funcional, médico y cama', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaReportadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Versión de ERP igual a 0 (Vie) → Resuelve la entidad administradora desde Contract.HealthAdministrator usando el GENCONENTITY del ingreso else Resuelve la entidad administradora desde dbo.INENTADM usando CODENTIDA de la ficha de notificación (otro ERP); si CODEVENTO termina en ''_D'' (356_D, 875_D, 903_D) → Se muestra el código sin el sufijo ''_D'' como código visible else Se muestra el CODEVENTO original como código visible; si CLASIFICACIONCASO en (1..5) → Se traduce a etiqueta: 1=Sospechoso, 2=Probable, 3=Conf laboratorio, 4=Conf clínica, 5=Conf epidemiológico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaReportadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHANOTIFICACION; dbo.INPACIENT; dbo.ADINGRESO; Contract.HealthAdministrator; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.CHCAMASHO; dbo.INPROFSAL; dbo.INENTADM', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaReportadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaReportadas';
-- GO
