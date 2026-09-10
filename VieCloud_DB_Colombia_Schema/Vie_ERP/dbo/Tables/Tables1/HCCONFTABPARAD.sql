CREATE TABLE [dbo].[HCCONFTABPARAD] (
    [ID]               INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCCONFTABPARAC] INT NOT NULL,
    [IDHCTABPARAC]     INT NOT NULL,
    CONSTRAINT [PK_HCCONFTABPARAD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCCONFTABPARAD_HCCONFTABPARAC] FOREIGN KEY ([IDHCCONFTABPARAC]) REFERENCES [dbo].[HCCONFTABPARAC] ([ID]),
    CONSTRAINT [FK_HCCONFTABPARAD_HCTABPARAC] FOREIGN KEY ([IDHCTABPARAC]) REFERENCES [dbo].[HCTABPARAC] ([ID])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la tabla HCTABPARAC que almacena registros de paraclinicos (exámenes de laboratorio, imagenes diagnosticas, estudios complementarios). Tipo INT, vincula cada configuración al paraclinico específico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAD', @level2type = N'COLUMN', @level2name = N'IDHCTABPARAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla  HCTABPARAC = Tabla Paraclinicos ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAD', @level2type = N'COLUMN', @level2name = N'IDHCTABPARAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAD', @level2type = N'COLUMN', @level2name = N'IDHCTABPARAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la tabla cabecera HCCONFTABPARAC que contiene la configuración maestra de paraclinicos. Tipo INT, referencia la configuración padre que agrupa detalles de paraclinicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAD', @level2type = N'COLUMN', @level2name = N'IDHCCONFTABPARAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla cabecera propia HCCONFTABPARAC = Configuracion Tabla Paraclinicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAD', @level2type = N'COLUMN', @level2name = N'IDHCCONFTABPARAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAD', @level2type = N'COLUMN', @level2name = N'IDHCCONFTABPARAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (PK, IDENTITY INT). Clave primaria única de la tabla de detalle de configuración de paraclinicos, generado automáticamente al insertar registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumérico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el detalle de los parámetros de configuración asociados a las tablas paraclínicas de historia clínica, vinculando cada configuración de tabla paraclínica con sus parámetros específicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAD';
