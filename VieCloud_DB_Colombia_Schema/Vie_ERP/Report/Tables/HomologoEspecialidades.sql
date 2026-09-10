CREATE TABLE [Report].[HomologoEspecialidades] (
    [CodEspecia]                CHAR (3)  NULL,
    [Especialidad]              CHAR (60) NULL,
    [CodEspeciaHomologo]        CHAR (3)  NULL,
    [EspecialidadHomologo]      CHAR (60) NULL,
    [CodEspeciaHomologo REPS]   CHAR (3)  NULL,
    [EspecialidadHomologo REPS] CHAR (60) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre especialidad homologo REPS', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'HomologoEspecialidades', @level2type = N'COLUMN', @level2name = N'EspecialidadHomologo REPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código especialidad homologo REPS', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'HomologoEspecialidades', @level2type = N'COLUMN', @level2name = N'CodEspeciaHomologo REPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la especialidad homologo', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'HomologoEspecialidades', @level2type = N'COLUMN', @level2name = N'EspecialidadHomologo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código especialidad homologo', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'HomologoEspecialidades', @level2type = N'COLUMN', @level2name = N'CodEspeciaHomologo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la especialidad', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'HomologoEspecialidades', @level2type = N'COLUMN', @level2name = N'Especialidad';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'HomologoEspecialidades', @level2type = N'COLUMN', @level2name = N'CodEspecia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de homologación que mapea las especialidades médicas internas (código y nombre propio) a sus equivalentes en dos catálogos externos: un homólogo genérico y el registro oficial REPS (Registro Especial de Prestadores de Servicios de Salud). Sirve como tabla de referencia para procesos de reporte, permitiendo traducir los códigos internos de especialidad al estándar REPS exigido en informes regulatorios.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'HomologoEspecialidades';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'HomologoEspecialidades';
GO
