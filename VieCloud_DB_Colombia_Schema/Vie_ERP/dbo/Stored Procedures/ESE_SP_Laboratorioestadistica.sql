CREATE PROCEDURE [dbo].[ESE_SP_Laboratorioestadistica]
@FechaIni DateTime,
@FechaFin DateTime
AS

SELECT OL.NUMINGRES AS 'ingreso', OL.IPCODPACI AS 'Docpaciente', OL.CODCENATE AS 'csalud', OL.UFUCODIGO AS 'Ufuncional',   ING.IFECHAING AS 'Fechaingreso',OL.FECORDMED AS 'Fechasolicitud', OL.FECRECMUE AS 'Fechatoma', OL.FECHARESULT AS 'Fecharesul', OL.INTERPRET, OL.NUMFOLINT,HIS.FECHISPAC AS 'Fechainterpretacion'

FROM HCORDLABO  OL 

INNER JOIN ADINGRESO ING ON OL.NUMINGRES =ING.NUMINGRES
LEFT JOIN HCHISPACA HIS ON OL.NUMINGRES=HIS.NUMINGRES AND OL.IPCODPACI=HIS.IPCODPACI AND OL.NUMFOLINT=HIS.NUMEFOLIO

WHERE   (ING.IFECHAING >= @FechaIni) AND (ING.IFECHAING <= @FechaFin) 

ORDER BY ING.IFECHAING
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento estadístico de laboratorio clínico que genera un listado de todas las órdenes de exámenes de laboratorio solicitadas durante un rango de fechas de ingreso. Cruza las órdenes de laboratorio (HCORDLABO) con los ingresos o admisiones del paciente (ADINGRESO) para obtener la fecha de ingreso, y opcionalmente enlaza con la historia clínica (HCHISPACA) para recuperar la fecha de interpretación médica del resultado. Retorna por cada orden: el número de ingreso, cédula o documento del paciente, centro de salud, unidad funcional, fecha de ingreso, fecha de solicitud del examen, fecha de toma de muestra, fecha del resultado, interpretación y fecha de interpretación clínica. Se usa para reportes estadísticos y seguimiento operativo del servicio de laboratorio, filtrando por rango de fechas de admisión (@FechaIni, @FechaFin).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Laboratorioestadistica';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Laboratorioestadistica';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte estadístico de órdenes de laboratorio asociadas a ingresos ocurridos dentro de un rango de fechas, incluyendo datos de solicitud, toma de muestra, resultado e interpretación clínica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Laboratorioestadistica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas (inicio y fin) debe estar definido y aplicarse sobre la fecha de ingreso administrativo.; Las órdenes de laboratorio deben tener un ingreso asociado existente en la tabla de ingresos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Laboratorioestadistica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen órdenes de laboratorio cuyo ingreso cae dentro del rango de fechas indicado.; El cruce con historia clínica es opcional (LEFT JOIN): si no hay folio de interpretación coincidente, igualmente se reporta la orden.; El emparejamiento con la historia clínica exige coincidencia simultánea de ingreso, paciente y número de folio de interpretación.; Los resultados se ordenan cronológicamente por fecha de ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Laboratorioestadistica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de laboratorio; Ingreso del paciente; Centro de atención; Unidad funcional; Toma de muestra; Resultado de laboratorio; Interpretación clínica; Folio de interpretación; Historia clínica del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Laboratorioestadistica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando IFECHAING está entre @FechaIni y @FechaFin, retorna las órdenes de laboratorio cruzadas con el ingreso y, si existe, con la historia clínica que coincide en ingreso, paciente y folio de interpretación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Laboratorioestadistica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.ADINGRESO; dbo.HCHISPACA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Laboratorioestadistica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Laboratorioestadistica';
-- GO
