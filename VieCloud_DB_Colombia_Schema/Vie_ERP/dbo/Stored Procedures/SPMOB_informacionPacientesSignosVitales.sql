
CREATE PROCEDURE [dbo].[SPMOB_informacionPacientesSignosVitales]
(
@Paciente Varchar(25),
@Ingreso Char(20)
)
AS
BEGIN
	SET NOCOUNT ON;

SELECT  a.TENARTSIS,a.TENARTDIA,a.TEMPERPAC,a.FRECARPAC, a.FECREGSIS,DATEDIFF(DAY,b.IPFECNACI,[Common].[GETDATE]()) as Edad , dbo.ObtenerFechaFormateada(FECREGSIS) as FechaRegistro
FROM HCEXFISIC a inner JOIN
inpacient b on a.IPCODPACI=b.IPCODPACI
WHERE a.IPCODPACI=@Paciente	and a.NUMINGRES=@Ingreso

end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera los signos vitales registrados para un paciente durante un ingreso hospitalario específico. Recibe como parámetros la cédula del paciente y el número de ingreso, y retorna la tensión arterial sistólica y diastólica, la temperatura corporal, la frecuencia cardíaca y la fecha y hora del registro. Complementa la información con la edad del paciente calculada desde su fecha de nacimiento hasta la fecha actual, consultando el examen físico (HCEXFISIC) cruzado con los datos maestros del paciente (INPACIENT). Se utiliza en la aplicación móvil para visualizar el historial de signos vitales del paciente durante su hospitalización o atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOB_informacionPacientesSignosVitales';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOB_informacionPacientesSignosVitales';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera los signos vitales más recientes registrados de un paciente en un ingreso específico, junto con su edad calculada en días y la fecha de registro formateada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOB_informacionPacientesSignosVitales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir en inpacient para poder calcular la edad mediante el JOIN.; Debe existir registro en HCEXFISIC asociado al paciente y número de ingreso indicados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOB_informacionPacientesSignosVitales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La edad se expresa siempre en días (DATEDIFF DAY entre fecha de nacimiento y fecha actual del sistema).; Solo se exponen registros de signos vitales que correspondan simultáneamente al paciente y al ingreso solicitados.; La fecha de registro se devuelve formateada mediante una función centralizada de formateo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOB_informacionPacientesSignosVitales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Signos vitales; Tensión arterial sistólica; Tensión arterial diastólica; Temperatura; Frecuencia cardiaca/respiratoria; Edad del paciente; Examen físico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOB_informacionPacientesSignosVitales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCEXFISIC: Cuando coinciden IPCODPACI e NUMINGRES, retorna tensión arterial sistólica/diastólica, temperatura, frecuencia, fecha de registro, edad en días y fecha formateada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOB_informacionPacientesSignosVitales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE; dbo.ObtenerFechaFormateada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOB_informacionPacientesSignosVitales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCEXFISIC; dbo.inpacient', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOB_informacionPacientesSignosVitales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOB_informacionPacientesSignosVitales';
-- GO
