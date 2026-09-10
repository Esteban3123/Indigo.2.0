

-- =============================================
-- Author:		<Author,,Juan Pablo Robayo>
-- Create date: <03-05-2022,,>
-- Description:	<Cirugias_Realizadas,,>
-- =============================================
CREATE PROCEDURE [dbo].[SPHC_Cirugias_Realizadas]
@Fechainicial datetime,
@Fechafinal datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT DISTINCT CASE WHEN INP.IPTIPODOC='1' THEN 'CC' WHEN INP.IPTIPODOC='2' THEN 'CE' WHEN INP.IPTIPODOC='3' THEN 'TI' WHEN INP.IPTIPODOC='4' THEN 'RC' 
WHEN INP.IPTIPODOC='5' THEN 'PA' WHEN INP.IPTIPODOC='6' THEN 'AS' WHEN INP.IPTIPODOC='7' THEN 'MS' WHEN INP.IPTIPODOC='8' THEN 'NU' WHEN INP.IPTIPODOC='9' THEN 'CN' WHEN INP.IPTIPODOC='10' THEN 'CD'
WHEN INP.IPTIPODOC='11' THEN 'SC' WHEN INP.IPTIPODOC='12' THEN 'PE' END 'TIPO DOCUMENTO', INFQ.IPCODPACI 'ID PACIENTE',INP.IPNOMCOMP 'NOMBRE PACIENTE',CASE WHEN INP.IPSEXOPAC=1 THEN 'M' ELSE 'F' END 'SEXO',
FLOOR((CAST(CONVERT(VARCHAR(8), INFQ.FECHORFIN , 112) AS INT)-CAST(CONVERT(VARCHAR(8), INP.IPFECNACI, 112) AS INT)) / 10000) AS 'EDAD'
,AGEN.NUMINGRES 'INGRESO',INFQ.FECHORINI 'INICIO CX',INFQ.FECHORFIN 'FIN CX',ANES.HORFINCIRU 'INICIO ANESTESIA', ANES.HORFINCIRU 'FIN ANESTESIA', INFQ.SALACIRUG 'SALA', INFQ.CODPROSAL 'COD. CIRUJANO', PRO.NOMMEDICO 'NOMBRE CIRUJANO',
ANES.CODPROSAL 'COD. ANESTESIOLOGO' , PRO2.NOMMEDICO 'ANESTESIOLOGO',
INFQ.CODDIAPRE 'DIAG. PREOPERATORIO', INFQ.CODDIAPOS 'DIAG. POSTOPERATORIO', ISNULL(ORD.FECORDMED,' ') 'FECHA ORDEN', RAD.FECHARADIC 'FECHA RADICACION', CASE WHEN DATEPART(weekday,INFQ.FECHORFIN )=1 THEN 'Lunes' 
WHEN DATEPART(weekday,INFQ.FECHORFIN )=2 THEN 'Martes' WHEN DATEPART(weekday,INFQ.FECHORFIN )=3 THEN 'Miercoles' WHEN DATEPART(weekday,INFQ.FECHORFIN )=4 THEN 'Jueves'
WHEN DATEPART(weekday,INFQ.FECHORFIN )=5 THEN 'Viernes' WHEN DATEPART(weekday,INFQ.FECHORFIN )=6 THEN 'Sabado' WHEN DATEPART(weekday,INFQ.FECHORFIN )=7 THEN 'Domingo' END'DIA',
INFQ.CODSERIPS 'COD. CUPS', CUPS.DESCODCUPS 'NOMBRE CUPS', CASE WHEN AGEN.ORIGENQX=1 THEN 'Cirugia de Origen Ambulatorio' ELSE 'Cirugia de Origen Hospitalario' END 'ORIGEN', ING.CODENTIDA, ENT.NOMENTIDA,
case when convert(varchar,INFQ.FECHORINI,108) BETWEEN '07:00:00' AND '11:59:00' THEN 'Mañana' WHEN convert(varchar,INFQ.FECHORINI,108) BETWEEN '12:00:00' AND '18:59:00' THEN 'Tarde'
WHEN convert(varchar,INFQ.FECHORINI,108) BETWEEN '19:00:00' AND '06:59:00' THEN 'Noche' END 'JORNADA', CASE WHEN ORD.PRISERIPS= 1 THEN 'Urgencia' when ORD.PRISERIPS=2 THEN 'Urgencia' ELSE 'Normal'
END 'PRIORIDAD'

FROM DBO.AGEPROGQX AGEN
INNER JOIN DBO.HCQXINFOR INFQ ON INFQ.IPCODPACI=AGEN.IPCODPACI AND AGEN.NUMINGRES=INFQ.NUMINGRES 
LEFT JOIN DBO.INPROFSAL PRO ON PRO.CODPROSAL=INFQ.CODPROSAL
LEFT JOIN DBO.INPACIENT INP ON INP.IPCODPACI=INFQ.IPCODPACI
LEFT JOIN DBO.HCORDPROQ ORD ON ORD.IPCODPACI=INFQ.IPCODPACI 
LEFT JOIN DBO.ADRADICACIONQX RAD ON RAD.IPCODPACI=INFQ.IPCODPACI 
LEFT JOIN dbo.INCUPSIPS CUPS ON CUPS.CODSERIPS=INFQ.CODSERIPS
LEFT JOIN DBO.HCREGANES ANES ON ANES.IPCODPACI=INFQ.IPCODPACI AND ANES.NUMINGRES=INFQ.NUMINGRES
LEFT JOIN DBO.INPROFSAL PRO2 ON PRO2.CODPROSAL=ANES.CODPROSAL
LEFT JOIN DBO.ADINGRESO ING ON ING.NUMINGRES=INFQ.NUMINGRES
LEFT JOIN DBO.INENTIDAD ENT ON ENT.CODENTIDA=ING.CODENTIDA

WHERE AGEN.PRINCIPAL=1 AND INFQ.FECHORINI BETWEEN @Fechainicial AND @Fechafinal
  
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de cirugías realizadas en un rango de fechas, consolidando información clínica, administrativa y logística de cada acto quirúrgico. Para cada cirugía, combina el informe quirúrgico (HCQXINFOR) con la programación de sala (AGEPROGQX), el registro anestésico (HCREGANES), las órdenes de procedimientos (HCORDPROQ), la radicación quirúrgica (ADRADICACIONQX) y el ingreso del paciente (ADINGRESO), enriqueciendo con datos del paciente (INPACIENT), los profesionales intervinientes como cirujano y anestesiólogo (INPROFSAL), el procedimiento CUPS (INCUPSIPS) y la entidad o aseguradora. El resultado incluye identificación y datos demográficos del paciente (tipo de documento, cédula, nombre, sexo, edad calculada), número de ingreso, sala, jornada, día de la semana, horarios de inicio y fin de la cirugía y la anestesia, diagnósticos preoperatorio y postoperatorio, código y nombre del cirujano y anestesiólogo, código CUPS del procedimiento, origen de la cirugía (ambulatoria u hospitalaria), prioridad (urgencia o normal), fecha de la orden médica y fecha de radicación. Se utiliza para informes de producción quirúrgica, auditoría clínica y gestión de salas de cirugía.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_Cirugias_Realizadas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_Cirugias_Realizadas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de cirugías realizadas en un rango de fechas, consolidando datos del paciente, procedimiento, cirujano, anestesiólogo, diagnósticos, entidad responsable y clasificación de jornada/prioridad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Cirugias_Realizadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se requiere un rango de fechas (inicial y final) para filtrar la fecha/hora de inicio de la cirugía.; Deben existir cirugías marcadas como principales en la programación quirúrgica (PRINCIPAL=1).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Cirugias_Realizadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan cirugías marcadas como principales en la agenda quirúrgica.; La edad se calcula a partir de la diferencia entre la fecha de fin de cirugía (FECHORFIN) y la fecha de nacimiento del paciente.; El reporte considera la cirugía como filtro temporal por su hora de inicio (FECHORINI), no por su fin.; Los registros del reporte son únicos (DISTINCT).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Cirugias_Realizadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Cirugía; Programación quirúrgica; Cirujano; Anestesiólogo; Anestesia; Diagnóstico preoperatorio; Diagnóstico postoperatorio; Orden médica; Radicación; CUPS; Ingreso hospitalario; Entidad responsable de pago; Sala de cirugía; Jornada (mañana/tarde/noche); Prioridad (urgencia/normal); Origen ambulatorio/hospitalario; Tipo de documento de identidad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Cirugias_Realizadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve listado DISTINCT de cirugías cuya FECHORINI está entre @Fechainicial y @Fechafinal y que están marcadas como principales (AGEN.PRINCIPAL=1).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Cirugias_Realizadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPTIPODOC entre ''1'' y ''12'' → Mapea el código a abreviatura de tipo de documento (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE) else NULL; si IPSEXOPAC = 1 → Sexo = ''M'' else Sexo = ''F''; si AGEN.ORIGENQX = 1 → Origen = ''Cirugia de Origen Ambulatorio'' else Origen = ''Cirugia de Origen Hospitalario''; si Hora de FECHORINI entre 07:00 y 11:59 → Jornada = ''Mañana'' else Si entre 12:00 y 18:59 => ''Tarde''; si entre 19:00 y 06:59 => ''Noche''; si ORD.PRISERIPS = 1 o 2 → Prioridad = ''Urgencia'' else Prioridad = ''Normal''; si DATEPART(weekday, FECHORFIN) entre 1 y 7 → Mapea día numérico a nombre del día en español (Lunes..Domingo) else NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Cirugias_Realizadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.AGEPROGQX; DBO.HCQXINFOR; DBO.INPROFSAL; DBO.INPACIENT; DBO.HCORDPROQ; DBO.ADRADICACIONQX; dbo.INCUPSIPS; DBO.HCREGANES; DBO.ADINGRESO; DBO.INENTIDAD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Cirugias_Realizadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Cirugias_Realizadas';
-- GO
