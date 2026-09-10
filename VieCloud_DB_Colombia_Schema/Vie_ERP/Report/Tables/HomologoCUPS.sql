CREATE TABLE [Report].[HomologoCUPS] (
    [Id]                  INT           NULL,
    [Cups]                VARCHAR (20)  NULL,
    [DescripcionCups]     VARCHAR (300) NULL,
    [CupsHomologo]        VARCHAR (20)  NULL,
    [DescripcionHomologo] VARCHAR (300) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción homologo', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'HomologoCUPS', @level2type = N'COLUMN', @level2name = N'DescripcionHomologo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CUPS homologo', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'HomologoCUPS', @level2type = N'COLUMN', @level2name = N'CupsHomologo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción CUPS', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'HomologoCUPS', @level2type = N'COLUMN', @level2name = N'DescripcionCups';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CUPS', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'HomologoCUPS', @level2type = N'COLUMN', @level2name = N'Cups';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'HomologoCUPS', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de homologación ubicada en el esquema de reportes que establece equivalencias entre códigos CUPS (Clasificación Única de Procedimientos en Salud) y sus códigos homólogos. Cada registro asocia un código CUPS original con su descripción y un código CUPS equivalente (homólogo) con su respectiva descripción, permitiendo la correspondencia entre distintas versiones o catálogos de procedimientos médicos para fines de reporte.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'HomologoCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'HomologoCUPS';
GO
