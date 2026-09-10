-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,22-05-2018,>
-- Description:	<Description,Sp que me lista los pacientes que tienen ficha del Sivigila descartadas, esto para el Dashboard de Epidemiologia>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarPacientesFichaSivigilaDescartadas]
(
  @CentroAtencion as varchar(10),
  @FechaInicial datetime,
  @FechaFinal datetime
)

AS
BEGIN
  SET NOCOUNT ON;

select C.ID,FECHACREACION As 'Fecha notificacion',C.FECHADESCARTE As 'Fecha Descartada',Rtrim(Z.NOMBRE) AS 'Motivo descarte', (C.IPCODPACI) As 'Identificacion', Rtrim(B.IPNOMCOMP) As 'Nombre Paciente',cast(datediff(dd,B.IPFECNACI,[Common].[GETDATE]()) / 365.25 as int) As 'Edad',
Rtrim(E.NOMCENATE) As 'Centro Atencion',Rtrim(F.UFUDESCRI) As 'Unidad Funcional',Rtrim(C.NOMBEVENTO) As 'Nombre Evento', Rtrim(C.CODEVENTO) As 'Codigo Evento', CASE RTRIM(CODEVENTO) WHEN '356_D' THEN '356' WHEN '875_D' THEN '875'  WHEN '903_D' THEN '903' ELSE RTRIM(CODEVENTO) END AS CodigoVisible , 
CASE CLASIFICACIONCASO WHEN '1' THEN 'Sospechoso' WHEN '2' THEN 'Probable' WHEN '3' THEN 'Conf laboratorio'  WHEN '4' THEN 'Conf clinica' WHEN '5' THEN 'Conf epidemiológico' END AS 'Clasificacion',
Rtrim(G.NOMMEDICO) As 'Nombre Medico',Rtrim(C.CODCENATE) as 'Codigo CA',Rtrim(C.UFUCODIGO) As 'Codigo UF',Rtrim(C.NUMINGRES) As 'Ingreso',Rtrim(C.CODDIAGNO) As 'Codigo Diagnostico'
From HCFICHANOTIFICACION C
  Inner Join INPACIENT B ON C.IPCODPACI = B.IPCODPACI  
  Inner Join ADINGRESO D ON C.NUMINGRES = D.NUMINGRES
  Inner Join ADCENATEN E ON C.CODCENATE = E.CODCENATE
  Inner Join INUNIFUNC F ON C.UFUCODIGO = F.UFUCODIGO
  Inner Join INPROFSAL G ON C.CODUSUARIO = G.CODPROSAL
  Inner Join HCDESCARTEFICHA Z ON C.MOTIVODESCAR  = Z.ID where C.ESTADO = 3 AND FECHADESCARTE BETWEEN @FechaInicial AND @FechaFinal 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes cuyas fichas de notificación obligatoria SIVIGILA han sido descartadas, para uso en el Dashboard de Epidemiología. Filtra por centro de atención y rango de fechas de descarte, consultando la ficha de notificación (HCFICHANOTIFICACION) con estado descartado (estado = 3) y cruzando datos del paciente (nombre, identificación, edad calculada), el ingreso hospitalario, el centro de atención, la unidad funcional, el médico notificador y el motivo de descarte. Devuelve información clave del evento epidemiológico: nombre del evento, código de evento, clasificación del caso (sospechoso, probable, confirmado por laboratorio, clínica o epidemiología) y código de diagnóstico CIE-10, permitiendo al área de epidemiología hacer seguimiento y auditoría de fichas SIVIGILA rechazadas o invalidadas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarPacientesFichaSivigilaDescartadas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarPacientesFichaSivigilaDescartadas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las fichas de notificación Sivigila descartadas dentro de un rango de fechas para alimentar el dashboard de epidemiología.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaDescartadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas (inicial y final) debe estar definido para filtrar por FECHADESCARTE.; Las fichas deben tener paciente, ingreso, centro de atención, unidad funcional, profesional y motivo de descarte relacionados existentes para aparecer (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaDescartadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran fichas con ESTADO = 3 (descartadas).; La edad se calcula en años enteros como diferencia de días entre fecha de nacimiento y fecha actual del sistema dividida por 365.25.; Los códigos de evento con sufijo ''_D'' se normalizan al código base para presentación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaDescartadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación Sivigila; Descarte de ficha epidemiológica; Motivo de descarte; Evento epidemiológico; Clasificación de caso (sospechoso/probable/confirmado); Paciente; Ingreso; Centro de atención; Unidad funcional; Profesional de salud; Diagnóstico; Dashboard de epidemiología', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaDescartadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHANOTIFICACION: Retorna fichas con ESTADO = 3 (descartadas) cuya FECHADESCARTE esté entre el rango recibido, enriquecidas con datos de paciente, centro, unidad funcional, médico y motivo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaDescartadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CODEVENTO termina en ''_D'' (356_D, 875_D, 903_D) → Se expone el código visible sin sufijo (356, 875, 903) else Se expone el CODEVENTO original; si CLASIFICACIONCASO = 1..5 → Se traduce a etiqueta: 1=Sospechoso, 2=Probable, 3=Conf laboratorio, 4=Conf clínica, 5=Conf epidemiológico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaDescartadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaDescartadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHANOTIFICACION; dbo.INPACIENT; dbo.ADINGRESO; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.INPROFSAL; dbo.HCDESCARTEFICHA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaDescartadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaDescartadas';
-- GO
