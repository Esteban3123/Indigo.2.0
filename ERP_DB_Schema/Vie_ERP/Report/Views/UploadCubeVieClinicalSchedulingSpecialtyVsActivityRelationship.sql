
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