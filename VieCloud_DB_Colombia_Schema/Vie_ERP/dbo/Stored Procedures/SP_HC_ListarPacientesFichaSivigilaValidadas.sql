-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,22-05-2018,>
-- Description:	<Description,Sp que me lista los pacientes que tienen ficha del Sivigila Validadas, esto para el Dashboard de Epidemiologia>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarPacientesFichaSivigilaValidadas]
(
  @CentroAtencion as varchar(10),
  @FechaInicial datetime,
  @FechaFinal datetime
)

AS
BEGIN
  SET NOCOUNT ON;

select C.ID,FECHACREACION As 'Fecha notificacion',C.FECHAREPORSIVIGILA As 'Fecha Reportada',C.FECHAVALIDO As 'Fecha Validada',Rtrim(C.IPCODPACI) As 'Identificacion', Rtrim(B.IPNOMCOMP) As 'Nombre Paciente',cast(datediff(dd,B.IPFECNACI,[Common].[GETDATE]()) / 365.25 as int) As 'Edad',
Rtrim(E.NOMCENATE) As 'Centro Atencion',Rtrim(F.UFUDESCRI) As 'Unidad Funcional',Rtrim(C.NOMBEVENTO) As 'Nombre Evento', Rtrim(C.CODEVENTO) As 'Codigo Evento',CASE RTRIM(CODEVENTO) WHEN '356_D' THEN '356' WHEN '875_D' THEN '875'  WHEN '903_D' THEN '903' ELSE RTRIM(CODEVENTO) END AS CodigoVisible , 
CASE CLASIFICACIONCASO WHEN '1' THEN 'Sospechoso' WHEN '2' THEN 'Probable' WHEN '3' THEN 'Conf laboratorio'  WHEN '4' THEN 'Conf clinica' WHEN '5' THEN 'Conf epidemiológico' END AS 'Clasificacion',
Rtrim(G.NOMMEDICO) As 'Nombre Medico',Rtrim(C.CODCENATE) as 'Codigo CA',Rtrim(C.UFUCODIGO) As 'Codigo UF',Rtrim(C.NUMINGRES) As 'Ingreso',Rtrim(C.CODDIAGNO) As 'Codigo Diagnostico'
From HCFICHANOTIFICACION C
  Inner Join INPACIENT B ON C.IPCODPACI = B.IPCODPACI  
  Inner Join ADINGRESO D ON C.NUMINGRES = D.NUMINGRES
  Inner Join ADCENATEN E ON C.CODCENATE = E.CODCENATE
  Inner Join INUNIFUNC F ON C.UFUCODIGO = F.UFUCODIGO
  Inner Join INPROFSAL G ON C.CODUSUARIO = G.CODPROSAL where C.ESTADO = 4 AND FECHAVALIDO BETWEEN @FechaInicial AND @FechaFinal 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes cuyas fichas de notificación obligatoria SIVIGILA han sido validadas, para alimentar el Dashboard de Epidemiología. Filtra por centro de atención y rango de fechas de validación, retornando datos del paciente (cédula, nombre, edad calculada), del evento notificable (nombre, código, clasificación del caso: sospechoso, probable, confirmado por laboratorio, clínica o epidemiología), del ingreso, del centro de atención, la unidad funcional y el médico notificador. Integra las tablas de fichas SIVIGILA, pacientes, ingresos, centros de atención, unidades funcionales y profesionales de la salud para producir el consolidado de eventos en salud pública ya validados por el área de epidemiología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarPacientesFichaSivigilaValidadas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarPacientesFichaSivigilaValidadas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes con fichas de notificación SIVIGILA en estado validado dentro de un rango de fechas, para alimentar el dashboard de epidemiología.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaValidadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las fichas deben tener un ingreso, centro de atención, unidad funcional, paciente y profesional de salud existentes (joins INNER).; Debe existir el rango de fechas de validación para filtrar.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaValidadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran fichas con ESTADO = 4 (validadas).; La edad se calcula como diferencia en días entre la fecha de nacimiento y la fecha actual del sistema, dividida por 365.25 y truncada a entero.; Solo se incluyen fichas con todas las relaciones obligatorias (paciente, ingreso, centro, unidad funcional, profesional).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaValidadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación SIVIGILA; Paciente; Evento epidemiológico; Clasificación de caso (sospechoso/probable/confirmado); Centro de atención; Unidad funcional; Ingreso hospitalario; Diagnóstico; Profesional de salud (médico); Dashboard de epidemiología', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaValidadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHANOTIFICACION: Cuando ESTADO = 4 y FECHAVALIDO está entre el rango recibido, se devuelven las fichas SIVIGILA validadas con datos del paciente, evento, centro, unidad funcional y médico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaValidadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CODEVENTO termina en ''_D'' (356_D, 875_D, 903_D) → Se expone el código visible sin el sufijo (356, 875, 903) else Se expone el CODEVENTO tal cual; si CLASIFICACIONCASO = 1..5 → Se traduce a etiqueta: 1=Sospechoso, 2=Probable, 3=Conf laboratorio, 4=Conf clínica, 5=Conf epidemiológico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaValidadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaValidadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHANOTIFICACION; dbo.INPACIENT; dbo.ADINGRESO; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaValidadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesFichaSivigilaValidadas';
-- GO
