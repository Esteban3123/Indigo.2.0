CREATE TABLE [dbo].[CHHOMCAMA] (
    [CODCONCEC] INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODICAMAS] INT       NOT NULL,
    [CODTIPEST] CHAR (3)  NOT NULL,
    [HCAMAHOMO] CHAR (10) NOT NULL,
    [CODCENATE] CHAR (10) NOT NULL,
    [UFUCODIGO] CHAR (10) NOT NULL,
    [NUMCAMHOS] CHAR (10) NOT NULL,
    CONSTRAINT [PK_CHHOMCAMA] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_CHHOMCAMA_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_CHHOMCAMA_CHTIPESTA] FOREIGN KEY ([CODTIPEST]) REFERENCES [dbo].[CHTIPESTA] ([CODTIPEST]),
    CONSTRAINT [FK_CHHOMCAMA_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del número de cama en el hospital, referencia física de la cama asignada al paciente durante su ingreso o atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHHOMCAMA', @level2type = N'COLUMN', @level2name = N'NUMCAMHOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Numero de la Cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHHOMCAMA', @level2type = N'COLUMN', @level2name = N'NUMCAMHOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHHOMCAMA', @level2type = N'COLUMN', @level2name = N'NUMCAMHOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (servicio, área, piso) donde se encuentra ubicada la cama; FK a INUNIFUNC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHHOMCAMA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHHOMCAMA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHHOMCAMA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención o institución prestadora de salud donde reside la cama; FK a ADCENATEN.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHHOMCAMA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHHOMCAMA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHHOMCAMA', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de homologación de la cama desde interfaz externa, mapeo de cama a sistemas integrados o terceros.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHHOMCAMA', @level2type = N'COLUMN', @level2name = N'HCAMAHOMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la cama homologada de Interfaz ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHHOMCAMA', @level2type = N'COLUMN', @level2name = N'HCAMAHOMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHHOMCAMA', @level2type = N'COLUMN', @level2name = N'HCAMAHOMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del tipo de estancia (hospitalización, urgencia, cuidados intensivos, etc.); FK a CHTIPESTA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHHOMCAMA', @level2type = N'COLUMN', @level2name = N'CODTIPEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Tipo de Estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHHOMCAMA', @level2type = N'COLUMN', @level2name = N'CODTIPEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHHOMCAMA', @level2type = N'COLUMN', @level2name = N'CODTIPEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código numérico identificador de la cama, clave interna para gestión de camas en el centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHHOMCAMA', @level2type = N'COLUMN', @level2name = N'CODICAMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Numero de la Cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHHOMCAMA', @level2type = N'COLUMN', @level2name = N'CODICAMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHHOMCAMA', @level2type = N'COLUMN', @level2name = N'CODICAMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo único autoincrementable, clave primaria de registro de homologación de cama (PK IDENTITY).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHHOMCAMA', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHHOMCAMA', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHHOMCAMA', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Homologación de camas hospitalarias: relaciona una cama física de una unidad funcional y centro de atención con su cama equivalente en otro sistema o nomenclatura, permitiendo la correspondencia entre distintas codificaciones de camas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHHOMCAMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHHOMCAMA';
