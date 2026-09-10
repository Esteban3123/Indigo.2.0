CREATE TABLE [dbo].[HCPLAOTRPROIMG] (
    [ID]             INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCPLAOTRPROC] INT             NOT NULL,
    [IMAGEN]         VARBINARY (MAX) NOT NULL,
    CONSTRAINT [PK_HCPLAOTRPROIMG] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de almacenamiento binario (VARBINARY MAX) para imágenes adjuntas a procedimientos; soporta archivos de imagen en diversos formatos (JPG, PNG, DICOM, etc.); datos sensibles de salud (PII médico).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROIMG', @level2type = N'COLUMN', @level2name = N'IMAGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo donde almacena imagenes adjuntas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROIMG', @level2type = N'COLUMN', @level2name = N'IMAGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROIMG', @level2type = N'COLUMN', @level2name = N'IMAGEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) que referencia la cabecera de la tabla HCPLAOTRPROC; vincula cada imagen al procedimiento, examen o documento clínico padre; entero no nulo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROIMG', @level2type = N'COLUMN', @level2name = N'IDHCPLAOTRPROC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id cabecera tabla HCPLAOTRPROC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROIMG', @level2type = N'COLUMN', @level2name = N'IDHCPLAOTRPROC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROIMG', @level2type = N'COLUMN', @level2name = N'IDHCPLAOTRPROC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (IDENTITY INT) de cada registro de imagen en detalle; clave primaria clustered; secuencial por inserción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROIMG', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id autonumerico detalle de imagenes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROIMG', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROIMG', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena las imágenes adjuntas asociadas a otros procedimientos registrados en la historia clínica del paciente. Cada registro vincula un archivo de imagen (fotografía, captura o documento escaneado) con el procedimiento correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROIMG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROIMG';
