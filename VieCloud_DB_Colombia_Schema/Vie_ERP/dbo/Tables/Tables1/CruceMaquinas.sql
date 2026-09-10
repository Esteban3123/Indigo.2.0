CREATE TABLE [dbo].[CruceMaquinas] (
    [Serial]        VARCHAR (50)  NULL,
    [Placa]         VARCHAR (50)  NULL,
    [Clasificacion] VARCHAR (50)  NULL,
    [Responsable]   VARCHAR (100) NULL,
    [Oficina]       VARCHAR (100) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ubicación física, unidad funcional o centro de atención donde se encuentra asignada la máquina. Tipo: VARCHAR(100)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CruceMaquinas', @level2type = N'COLUMN', @level2name = N'Oficina';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la oficina de la máquina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CruceMaquinas', @level2type = N'COLUMN', @level2name = N'Oficina';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CruceMaquinas', @level2type = N'COLUMN', @level2name = N'Oficina';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o identificación del profesional o persona responsable de la custodia y mantenimiento de la máquina. Tipo: VARCHAR(100)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CruceMaquinas', @level2type = N'COLUMN', @level2name = N'Responsable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el responsable de la máquina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CruceMaquinas', @level2type = N'COLUMN', @level2name = N'Responsable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CruceMaquinas', @level2type = N'COLUMN', @level2name = N'Responsable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación o categoría de la máquina (ej: laboratorio, imagenología, quirúrgica, administrativa). Tipo: VARCHAR(50)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CruceMaquinas', @level2type = N'COLUMN', @level2name = N'Clasificacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la clasificación de la máquina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CruceMaquinas', @level2type = N'COLUMN', @level2name = N'Clasificacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CruceMaquinas', @level2type = N'COLUMN', @level2name = N'Clasificacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Placa o matrícula de identificación física de la máquina, registro administrativo. Tipo: VARCHAR(50)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CruceMaquinas', @level2type = N'COLUMN', @level2name = N'Placa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la placa de la máquina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CruceMaquinas', @level2type = N'COLUMN', @level2name = N'Placa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CruceMaquinas', @level2type = N'COLUMN', @level2name = N'Placa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de serie único de la máquina, identificador de fabricante para trazabilidad y garantía. Tipo: VARCHAR(50)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CruceMaquinas', @level2type = N'COLUMN', @level2name = N'Serial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el serial de la máquina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CruceMaquinas', @level2type = N'COLUMN', @level2name = N'Serial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CruceMaquinas', @level2type = N'COLUMN', @level2name = N'Serial';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de equipos o máquinas institucionales utilizados en el cruce o validación de información, incluyendo su identificación, clasificación y responsable asignado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CruceMaquinas';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CruceMaquinas';
