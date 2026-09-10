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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes que tienen fichas de notificación SIVIGILA diligenciadas, para alimentar el Dashboard de Epidemiología. Retorna por cada paciente su identificación (cédula), nombre, edad, ingreso hospitalario, centro de atención, unidad funcional, cama asignada, entidad administradora de salud (EPS), médico notificador, código y nombre del evento epidemiológico, y la clasificación del caso (sospechoso, probable, confirmado por laboratorio, clínica o epidemiología). Recibe como parámetros el centro de atención y la versión del ERP para adaptar dinámicamente la consulta a la fuente correcta de la entidad administradora (Indigo Vie o ERP externo). Es usado por el área de Epidemiología y Salud Pública para el seguimiento y reporte de eventos de interés en salud pública notificados al SIVIGILA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarPacientesFichaSivigilaDiligenciadas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarPacientesFichaSivigilaDiligenciadas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar las fichas de notificación SIVIGILA diligenciadas y activas de pacientes, con datos clínicos y administrativos, para alimentar el dashboard de epidemiología, adaptando la fuente de la entidad aseguradora según la versión de ERP.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaDiligenciadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de centros de atención debe venir como lista CSV válida e inyectable en la cláusula IN (riesgo de SQL dinámico); Debe existir la función Common.GETDATE() para el cálculo de edad; Las fichas deben tener relación íntegra con paciente, ingreso, centro, unidad funcional y profesional (INNER JOIN); Para VersionERP=0 debe existir la relación ADINGRESO.GENCONENTITY con Contract.HealthAdministrator; en caso contrario, debe existir CODENTIDA en INENTADM', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaDiligenciadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan fichas de notificación SIVIGILA con ESTADO = 1 (activas/diligenciadas); El resultado se filtra por los centros de atención indicados en el parámetro de lista; La edad se calcula en años a partir de la fecha de nacimiento usando Common.GETDATE() y división por 365.25; Los códigos de evento con sufijo ''_D'' se normalizan al código base para presentación; El listado se ordena por identificación del paciente; Solo se incluyen fichas con ingreso (NUMINGRES), centro, unidad funcional y profesional válidos por el uso de INNER JOIN; La fuente de la entidad aseguradora depende de la versión de ERP configurada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaDiligenciadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación SIVIGILA; Vigilancia epidemiológica; Evento de notificación obligatoria; Clasificación del caso (sospechoso/probable/confirmado); Paciente; Ingreso hospitalario; Centro de atención; Unidad funcional; Entidad administradora de salud (EPS); Profesional de salud / médico tratante; Cama hospitalaria; Dashboard de Epidemiología', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaDiligenciadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHANOTIFICACION: Cuando ESTADO=1 y CODCENATE está en la lista recibida, retorna el conjunto de fichas SIVIGILA con datos del paciente, ingreso, entidad, centro, unidad funcional, evento, clasificación, médico y cama', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaDiligenciadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si VersionERP = 0 (ERP Vie) → Construye consulta tomando la entidad desde Contract.HealthAdministrator (heal.Name) vía D.GENCONENTITY else Para otros ERP, toma la entidad desde INENTADM (NOMENTADM) vía C.CODENTIDA; si CODEVENTO termina en ''_D'' (356_D, 875_D, 903_D) → Se expone el código visible sin sufijo (356, 875, 903); en caso contrario se muestra el CODEVENTO original; si CLASIFICACIONCASO ∈ {1,2,3,4,5} → Se traduce a etiqueta clínica: 1=Sospechoso, 2=Probable, 3=Conf laboratorio, 4=Conf clínica, 5=Conf epidemiológico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaDiligenciadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaDiligenciadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHANOTIFICACION; dbo.INPACIENT; dbo.ADINGRESO; Contract.HealthAdministrator; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.CHCAMASHO; dbo.INPROFSAL; dbo.INENTADM', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaDiligenciadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaDiligenciadas';
-- GO
