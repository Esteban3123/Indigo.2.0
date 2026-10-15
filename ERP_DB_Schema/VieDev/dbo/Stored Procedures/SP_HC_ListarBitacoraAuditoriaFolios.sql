

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