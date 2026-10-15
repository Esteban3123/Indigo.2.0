CREATE TABLE [dbo].[HCPAEXFIC] (
    [AUTO]      INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE] CHAR (10) NOT NULL,
    [CODESPECI] CHAR (3)  NOT NULL,
    [HCEXFISIC] CHAR (50) NOT NULL,
    CONSTRAINT [PK_HCPAEXFIC] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCPAEXFIC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPAEXFIC_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI])
);


GO
ALTER TABLE [dbo].[HCPAEXFIC] NOCHECK CONSTRAINT [FK_HCPAEXFIC_ADCENATEN];


GO
ALTER TABLE [dbo].[HCPAEXFIC] NOCHECK CONSTRAINT [FK_HCPAEXFIC_INESPECIA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la historia ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAEXFIC', @level2type = N'COLUMN', @level2name = N'HCEXFISIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAEXFIC', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAEXFIC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAEXFIC', @level2type = N'COLUMN', @level2name = N'AUTO';

