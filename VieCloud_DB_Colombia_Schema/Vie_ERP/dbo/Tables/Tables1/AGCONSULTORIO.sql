CREATE TABLE [dbo].[AGCONSULTORIO] (
    [CODIGO]      NVARCHAR (255) NOT NULL,
    [CONSULTORIO] NVARCHAR (255) NULL,
    [SEDE]        NVARCHAR (255) NULL,
    CONSTRAINT [PK_AGCONSULTORIO__CODIGO] PRIMARY KEY CLUSTERED ([CODIGO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de consultorios disponibles en la institución, identificando cada espacio físico de atención y la sede a la que pertenece.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULTORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULTORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador único del consultorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULTORIO', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULTORIO', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción del consultorio, espacio físico o sala de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULTORIO', @level2type = N'COLUMN', @level2name = N'CONSULTORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULTORIO', @level2type = N'COLUMN', @level2name = N'CONSULTORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sede o sucursal de la institución donde está ubicado el consultorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULTORIO', @level2type = N'COLUMN', @level2name = N'SEDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULTORIO', @level2type = N'COLUMN', @level2name = N'SEDE';
