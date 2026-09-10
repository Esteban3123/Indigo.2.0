CREATE TABLE [dbo].[SOLPLANTI] (
    [PLANTAUTO] INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PLANTCODI] VARCHAR (4)  NOT NULL,
    [PLANTDESC] VARCHAR (50) NOT NULL,
    [UFUCODIGO] CHAR (10)    NOT NULL,
    CONSTRAINT [PK_SOLPLANTI] PRIMARY KEY CLUSTERED ([PLANTAUTO] ASC),
    CONSTRAINT [FK_SOLPLANTI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (centro de atención, departamento o servicio médico). Clave foránea que vincula la plantilla a la estructura organizativa del centro de salud. Búsqueda: unidad funcional, departamento, servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLANTI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el codigo de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLANTI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLANTI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o nombre de la plantilla de solicitud. Especifica el tipo o propósito de la plantilla (ej: plantilla de laboratorio, imagen, procedimiento). Búsqueda: descripción plantilla, nombre plantilla, tipo solicitud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLANTI', @level2type = N'COLUMN', @level2name = N'PLANTDESC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene la descripcion de la plantilla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLANTI', @level2type = N'COLUMN', @level2name = N'PLANTDESC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLANTI', @level2type = N'COLUMN', @level2name = N'PLANTDESC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de la plantilla de solicitud (VARCHAR 4). Identificador corto alfanumérico para clasificación y referencia rápida. Búsqueda: código plantilla, plantilla ID, código solicitud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLANTI', @level2type = N'COLUMN', @level2name = N'PLANTCODI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el codigo de la plantilla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLANTI', @level2type = N'COLUMN', @level2name = N'PLANTCODI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLANTI', @level2type = N'COLUMN', @level2name = N'PLANTCODI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY). Clave primaria técnica del registro, generado automáticamente por la base de datos. Uso interno: índice, auditoría, integridad referencial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLANTI', @level2type = N'COLUMN', @level2name = N'PLANTAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLANTI', @level2type = N'COLUMN', @level2name = N'PLANTAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLANTI', @level2type = N'COLUMN', @level2name = N'PLANTAUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de plantillas o tipos de solicitudes de planta/piso, vinculadas a una unidad funcional. Permite identificar y clasificar las plantillas disponibles para gestionar solicitudes dentro de cada área o servicio del centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLANTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLANTI';
