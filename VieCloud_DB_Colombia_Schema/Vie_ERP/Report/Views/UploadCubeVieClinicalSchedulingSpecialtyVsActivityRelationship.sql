
--CREATE PROCEDURE [Scheduling].[SP_RELACION_ESPECIALIDAD_ACTIVIDAD]
--AS

CREATE view [Report].[UploadCubeVieClinicalSchedulingSpecialtyVsActivityRelationship] as 

SELECT DISTINCT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
	esp.CODESPECI AS 'CODIGO ESPECIALIDAD',--[CodEspecialidad],
	esp.DESESPECI AS 'ESPECIALIDAD',--[Especialidad], 
	act.CODACTMED AS 'CODIGO ACTVIDAD',--[CodActividad],
	act.DESACTMED AS 'ACTIVIDAD',--[Actividad],
	isnull(act.DURAACTIV,'') AS 'DURACION ACTIVIDAD',--[DuracionActividad]
    CAST(GETDATE() AS DATE) [FECHA BUSQUEDA],
    CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM dbo.AGESPACTI as rel
inner join dbo.INESPECIA as esp on rel.CODESPECI =esp.CODESPECI 
inner join dbo.AGACTIMED as act on rel.CODACTMED =act.CODACTMED 
--where esp.CODESPECI in ('055','041')

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Exponer el catálogo de relaciones vigentes entre especialidades médicas y actividades médicas habilitadas para agendamiento, enriquecido con descripciones, duración y metadatos de carga para alimentar un cubo de reportes.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingSpecialtyVsActivityRelationship';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas de catálogo de especialidades y actividades médicas deben estar pobladas y con códigos consistentes con la tabla de relación especialidad-actividad.; El servidor SQL debe soportar la conversión de zona horaria ''Pakistan Standard Time'' (SQL Server 2016+).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingSpecialtyVsActivityRelationship';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen pares especialidad-actividad que existan simultáneamente en el catálogo de especialidades y en el catálogo de actividades médicas (uso de INNER JOIN).; Se eliminan duplicados mediante DISTINCT, garantizando una única fila por combinación especialidad-actividad.; Si la duración de la actividad es nula, se reemplaza por cadena vacía.; La fecha de búsqueda corresponde siempre a la fecha actual del servidor (sin componente de hora).; La marca de última actualización se calcula convirtiendo la hora actual a la zona horaria ''Pakistan Standard Time''.; El identificador de compañía se obtiene del nombre de la base de datos en ejecución, truncado a 9 caracteres.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingSpecialtyVsActivityRelationship';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Especialidad médica; Actividad médica; Agendamiento de citas; Duración de actividad', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingSpecialtyVsActivityRelationship';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalSchedulingSpecialtyVsActivityRelationship: Devuelve un conjunto distinto de pares especialidad-actividad solo cuando existe coincidencia entre la tabla de relación y ambos catálogos (INNER JOIN sobre código de especialidad y código de actividad).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingSpecialtyVsActivityRelationship';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGESPACTI; dbo.INESPECIA; dbo.AGACTIMED', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingSpecialtyVsActivityRelationship';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingSpecialtyVsActivityRelationship';
GO
