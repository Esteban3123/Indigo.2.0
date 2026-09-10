

CREATE PROCEDURE [dbo].[SP_HC_ListarBitacoraAuditoriaFolios]
(
@Identificacion Varchar(25),
@TipoFecha int,
@FechaInicial date,
@FechaFinal date
)
AS
BEGIN
	SET NOCOUNT ON;

	if @TipoFecha = 1 begin  --Rango de fechas
			SELECT a.*, rtrim(b.NOMCENATE) as NombreCentroAtencion, case ConsultationPlatform when 1 then 'Vie Cloud Platform' when 2 then 'Indira - Imagenología' when 3 then 'Indira - Laboratorio' when 4 then 'Indira - Patologia' end as Plataforma, case FormConsulted when 1 then 'Formulario de consulta historias' when 2 then 'Dashboard paciente' end as Formulario, RTRIM(C.UFUDESCRI) AS NombreUnidadFuncional, CONCAT('Latitud: ', Latitudelocation, ' Longitud: ', lengthlocation ) as Localizacion, rtrim(d.NOMUSUARI) NombreUsuario, RTRIM(E.NOMMEDICO) as NombreMedico, E.TARJETAPR  TarjetaProfesional, dbo.[TipoProfesionMedico](E.TIPPROFES) TipoProfesionMedico 
			FROM MedicalHistory.AuditConsultationFolios a 
					INNER join SEGusuaru d on a.CODUSUARI = d.CODUSUARI
					left join ADCENATEN b on a.CODCENATE = b.CODCENATE
					left join INUNIFUNC C on a.UFUCODIGO = C.UFUCODIGO
					left join INPROFSAL E on a.CODPROSAL = E.CODPROSAL

			where a.IPCODPACI = @Identificacion and RegisterDate between @FechaInicial and DATEADD(minute,59,DATEADD(hh,23,CAST(@FechaFinal AS DATETIME))) ORDER BY A.RegisterDate desc

end	 else if @TipoFecha = 2 begin  --Mes y año

			SELECT a.*, rtrim(b.NOMCENATE) as NombreCentroAtencion, case ConsultationPlatform when 1 then 'Vie Cloud Platform' when 2 then 'Indira - Imagenología' when 3 then 'Indira - Laboratorio' when 4 then 'Indira - Patologia' end as Plataforma, case FormConsulted when 1 then 'Formulario de consulta historias' when 2 then 'Dashboard paciente' end as Formulario, RTRIM(C.UFUDESCRI) AS NombreUnidadFuncional, CONCAT('Latitud: ', Latitudelocation, ' Longitud: ', lengthlocation ) as Localizacion, rtrim(d.NOMUSUARI) NombreUsuario, RTRIM(E.NOMMEDICO) as NombreMedico, E.TARJETAPR  TarjetaProfesional, dbo.[TipoProfesionMedico](E.TIPPROFES) TipoProfesionMedico 
			FROM MedicalHistory.AuditConsultationFolios a 
					INNER join SEGusuaru d on a.CODUSUARI = d.CODUSUARI
					left join ADCENATEN b on a.CODCENATE = b.CODCENATE
					left join INUNIFUNC C on a.UFUCODIGO = C.UFUCODIGO
					left join INPROFSAL E on a.CODPROSAL = E.CODPROSAL
			where a.IPCODPACI = @Identificacion and MONTH(a.RegisterDate) = MONTH(@FechaInicial) and year(a.RegisterDate) = year(@FechaFinal) ORDER BY A.RegisterDate desc

	end

end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista la bitácora de auditoría de accesos a folios de historia clínica para un paciente específico, filtrando por su cédula o identificación y un período de tiempo que puede ser un rango de fechas exacto o un mes y año determinado. Combina los registros de auditoría de AuditConsultationFolios con información del usuario que realizó la consulta, el centro de atención, la unidad funcional, el profesional de salud involucrado y la geolocalización desde donde se hizo el acceso. Permite saber quién consultó los formularios clínicos de un paciente, desde qué plataforma (Vie Cloud, Imagenología, Laboratorio o Patología), qué formulario se abrió (historia clínica o dashboard del paciente), y en qué momento, con fines de trazabilidad, control de acceso y auditoría de privacidad de la información clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarBitacoraAuditoriaFolios';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarBitacoraAuditoriaFolios';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista la bitácora de auditoría de accesos a folios de historia clínica de un paciente, filtrada por rango de fechas o por mes/año, enriquecida con datos del usuario, centro de atención, unidad funcional y profesional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarBitacoraAuditoriaFolios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe suministrarse la identificación del paciente a auditar; Debe indicarse el modo de filtro temporal (1=rango de fechas, 2=mes/año); Para modo rango: fecha inicial y final deben ser válidas y coherentes; Para modo mes/año: el mes se toma de la fecha inicial y el año de la fecha final', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarBitacoraAuditoriaFolios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan registros de auditoría asociados al paciente identificado; Siempre se requiere existencia del usuario consultante en SEGusuaru (INNER JOIN); auditorías sin usuario válido no se devuelven; Centro de atención, unidad funcional y profesional de salud son opcionales (LEFT JOIN) y no excluyen registros si faltan; El resultado se ordena de forma descendente por fecha de registro (más reciente primero); El rango de fechas incluye el día final completo hasta las 23:59; Si el tipo de fecha no es 1 ni 2, no se devuelve ningún resultado; La localización se presenta concatenando latitud y longitud en un solo texto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarBitacoraAuditoriaFolios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Auditoría de historia clínica; Folios de historia clínica; Paciente; Centro de atención; Unidad funcional; Profesional de salud; Tarjeta profesional; Tipo de profesión médica; Plataforma de consulta; Localización geográfica (latitud/longitud)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarBitacoraAuditoriaFolios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MedicalHistory.AuditConsultationFolios: Cuando @TipoFecha=1, devuelve auditorías del paciente cuya RegisterDate esté entre @FechaInicial y @FechaFinal+23:59, ordenadas desc por RegisterDate; [RETURN_RESULT] MedicalHistory.AuditConsultationFolios: Cuando @TipoFecha=2, devuelve auditorías del paciente cuyo mes de RegisterDate coincida con el mes de @FechaInicial y cuyo año coincida con el año de @FechaFinal', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarBitacoraAuditoriaFolios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Modo de filtro por rango de fechas → Filtra registros cuya fecha de registro esté entre la fecha inicial y la final (esta última extendida hasta las 23:59 del día) else Si el modo es por mes/año, filtra registros cuyo mes coincida con el de la fecha inicial y cuyo año coincida con el de la fecha final; si Traducción del código de plataforma de consulta → 1=Vie Cloud Platform, 2=Indira - Imagenología, 3=Indira - Laboratorio, 4=Indira - Patología; si Traducción del código de formulario consultado → 1=Formulario de consulta historias, 2=Dashboard paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarBitacoraAuditoriaFolios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.TipoProfesionMedico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarBitacoraAuditoriaFolios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MedicalHistory.AuditConsultationFolios; dbo.SEGusuaru; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarBitacoraAuditoriaFolios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarBitacoraAuditoriaFolios';
-- GO
