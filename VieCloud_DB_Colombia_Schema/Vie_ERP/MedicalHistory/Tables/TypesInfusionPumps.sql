CREATE TABLE [MedicalHistory].[TypesInfusionPumps] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [Supplier]          VARCHAR (200)  NOT NULL,
    [Model]             VARCHAR (300)  NOT NULL,
    [IdPump]            INT            NOT NULL,
    [PurgeVolumenxPump] DECIMAL (7, 2) NOT NULL,
    [State]             BIT            NOT NULL,
    [Code]              VARCHAR (200)  NOT NULL,
    CONSTRAINT [PK_TypesInfusionPumps] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único identificador de la bomba de infusión (VARCHAR 200). Identificador técnico del equipo médico de infusión.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TypesInfusionPumps', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo Bomba De Infusión', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TypesInfusionPumps', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TypesInfusionPumps', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo de la bomba de infusión (BIT: 1=Activo/Disponible, 0=Inactivo/No disponible). Indica operatividad del equipo.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TypesInfusionPumps', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el estado 1 = si  0 = No', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TypesInfusionPumps', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TypesInfusionPumps', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen de purga/lavado por bomba de infusión en mililitros (DECIMAL 7,2). Cantidad de solución para desinfetar líneas.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TypesInfusionPumps', @level2type = N'COLUMN', @level2name = N'PurgeVolumenxPump';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el volumen de la bomba', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TypesInfusionPumps', @level2type = N'COLUMN', @level2name = N'PurgeVolumenxPump';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TypesInfusionPumps', @level2type = N'COLUMN', @level2name = N'PurgeVolumenxPump';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico de la bomba de infusión (INT). Referencia única del equipo dentro del inventario de bombas.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TypesInfusionPumps', @level2type = N'COLUMN', @level2name = N'IdPump';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id de Bombas De Infusión', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TypesInfusionPumps', @level2type = N'COLUMN', @level2name = N'IdPump';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TypesInfusionPumps', @level2type = N'COLUMN', @level2name = N'IdPump';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modelo o versión técnica de la bomba de infusión (VARCHAR 300). Especificación técnica del equipo para trazabilidad médica.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TypesInfusionPumps', @level2type = N'COLUMN', @level2name = N'Model';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el modelo  Bombas De Infusión', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TypesInfusionPumps', @level2type = N'COLUMN', @level2name = N'Model';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TypesInfusionPumps', @level2type = N'COLUMN', @level2name = N'Model';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Proveedor/fabricante de la bomba de infusión (VARCHAR 200). Nombre del distribuidor o empresa manufacturera del equipo médico.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TypesInfusionPumps', @level2type = N'COLUMN', @level2name = N'Supplier';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el proveedor tipos bombas infusion', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TypesInfusionPumps', @level2type = N'COLUMN', @level2name = N'Supplier';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TypesInfusionPumps', @level2type = N'COLUMN', @level2name = N'Supplier';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria autoincrementable (INT IDENTITY). Identificador único consecutivo del registro de configuración en tabla maestra.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TypesInfusionPumps', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TypesInfusionPumps', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TypesInfusionPumps', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de bombas de infusión utilizadas en historia clínica: registra los modelos disponibles por proveedor, el volumen de purga asociado a cada bomba y su estado de disponibilidad para uso en procedimientos de infusión.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TypesInfusionPumps';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'TypesInfusionPumps';
