/**** 
SP para listar la cabecera para la creación del JSON para las solicitudes de farmacia para UNIHEALTH
****/
CREATE PROCEDURE [dbo].[SP_ListarCabeceraSolicitudFarmacia]
(
  @Consecutivo as Int
)
WITH RECOMPILE
AS
BEGIN
	SET NOCOUNT ON;
		
			SELECT 
				RTRIM(A.UFUCODIGO) AS 'COD UNIDAD FUNCIONAL', RTRIM(I.UFUDESCRI) AS 'UNIDAD FUNCIONAL',				
				A.CODCONCEC AS 'ID', A.FECHAORDE AS 'FECHA ORDEN', DATEADD(HOUR, 24, A.FECHAORDE) AS 'FECHA FIN', 
				NULL AS 'OBSERVACION', RTRIM(A.NUMINGRES) AS 'INGRESO', A.IPCODPACI AS 'PACIENTE', RTRIM(B.IPNOMCOMP) AS 'NOMBRE PACIENTE', B.IPFECNACI AS 'FECHA NACIMIENTO',
				(SELECT TOP 1 TALLAPACI FROM HCEXFISIC WHERE IPCODPACI = A.IPCODPACI ORDER BY FECREGSIS DESC) AS 'TALLA', (SELECT TOP 1 PESOPACIE FROM HCEXFISIC WHERE IPCODPACI = A.IPCODPACI ORDER BY FECREGSIS DESC) AS 'PESO',
				RTRIM(A.CODPROSAL) AS 'PROFESIONAL', RTRIM(D.NOMMEDICO) AS 'NOMBRE PROFESIONAL', D.TIPPROFES AS 'COD TIPO PROFESIONAL', 
				CASE D.TIPPROFES WHEN 1 THEN 'Medico general' WHEN 2 THEN 'Medico Especialista' WHEN 3 THEN 'Enfermera' WHEN 4 THEN 'Auxiliar Enfermeria' WHEN 5 THEN 'Odontologo General' WHEN 6 THEN 'Odontologo Especialista' WHEN 7 THEN 'Nutricionista'
				WHEN 8 THEN 'Higienista' WHEN 9 THEN 'Psicologo' WHEN 10 THEN 'Trabajadora Social' WHEN 11 THEN 'Promotor de Saneamiento' WHEN 12 THEN 'Ingeniero Sanitario' WHEN 13 THEN 'Medico Veterinario' WHEN 14 THEN 'Ingeniero Alimento' WHEN 15 THEN 'Auxiliar Bacteriologo'
				WHEN 16 THEN 'Terapeuta' WHEN 17 THEN 'Optometra' WHEN 18 THEN 'Quimico Farmaceutico' WHEN 19 THEN 'Radiologo' WHEN 20 THEN 'Tecnologo Radiologo' WHEN 21 THEN 'Instrumentador Qx' WHEN 22 THEN 'Auxiliar Patologia' WHEN 23 THEN 'Otros' WHEN 24 THEN 'Medico Interno'
				WHEN 25 THEN 'Bacteriologo(a)' WHEN 26 THEN 'Patólogo(a)' WHEN 27 THEN 'Médico residente' END AS 'TIPO PROFESIONAL', ISNULL(A.ORDENQUIMIO,0) AS 'ONCOLOGIA'
			FROM HCFARMEPC A
				INNER JOIN INPACIENT B ON A.IPCODPACI = B.IPCODPACI
				INNER JOIN INPROFSAL D ON A.CODPROSAL = D.CODPROSAL
				INNER JOIN INUNIFUNC I ON I.UFUCODIGO = A.UFUCODIGO
			WHERE A.CODCONCEC = @Consecutivo 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera la información de cabecera de una solicitud de farmacia identificada por su número consecutivo de orden, para armar el JSON que se envía a UNIHEALTH. Consolida datos del encabezado de la orden médica de despacho farmacéutico (HCFARMEPC) con el nombre, fecha de nacimiento, talla y peso más reciente del paciente (INPACIENT, HCEXFISIC), el nombre y tipo de profesional que la prescribió (INPROFSAL) y la descripción de la unidad funcional o servicio donde se generó (INUNIFUNC). Retorna campos como número de ingreso, código y nombre del paciente, fecha de la orden, fecha límite de vigencia (24 horas después), indicador de si es orden oncológica/quimioterapia, y el tipo de profesional en texto legible. Se utiliza en el proceso de integración con el sistema externo UNIHEALTH para el despacho y dispensación de medicamentos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ListarCabeceraSolicitudFarmacia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ListarCabeceraSolicitudFarmacia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la cabecera de una solicitud/orden de farmacia identificada por su consecutivo, con datos de paciente, profesional, unidad funcional y antropometría, para construir un JSON de integración con UNIHEALTH.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarCabeceraSolicitudFarmacia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una orden de farmacia con el consecutivo indicado en HCFARMEPC.; El paciente, profesional y unidad funcional referenciados deben existir en INPACIENT, INPROFSAL e INUNIFUNC (joins INNER).; Para obtener talla/peso debe existir al menos un registro en HCEXFISIC del paciente; de lo contrario serán NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarCabeceraSolicitudFarmacia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La ''FECHA FIN'' siempre se calcula como FECHAORDE + 24 horas.; El indicador de oncología (ORDENQUIMIO) nunca es NULL en la salida; se reemplaza por 0 vía ISNULL.; La talla y el peso corresponden siempre al registro más reciente del paciente en HCEXFISIC (ordenado por FECREGSIS descendente, TOP 1).; El campo OBSERVACION siempre se devuelve como NULL.; Solo se retorna a lo sumo una fila por consecutivo (filtro por clave en HCFARMEPC).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarCabeceraSolicitudFarmacia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud/Orden de farmacia; Paciente; Profesional de salud; Unidad funcional; Examen físico (talla y peso); Tipo de profesional asistencial; Orden de quimioterapia/oncología; Ingreso hospitalario; Integración UNIHEALTH', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarCabeceraSolicitudFarmacia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFARMEPC: Cuando CODCONCEC = @Consecutivo, se retorna una fila con la cabecera de la solicitud de farmacia enriquecida con datos de paciente, profesional y unidad funcional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarCabeceraSolicitudFarmacia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si D.TIPPROFES entre 1 y 27 → Se traduce el código a una etiqueta textual del tipo de profesional (Medico general, Especialista, Enfermera, Odontólogo, Nutricionista, etc.). else Si TIPPROFES no está en el rango 1-27, el tipo de profesional se devuelve como NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarCabeceraSolicitudFarmacia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFARMEPC; dbo.INPACIENT; dbo.INPROFSAL; dbo.INUNIFUNC; dbo.HCEXFISIC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarCabeceraSolicitudFarmacia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarCabeceraSolicitudFarmacia';
-- GO
