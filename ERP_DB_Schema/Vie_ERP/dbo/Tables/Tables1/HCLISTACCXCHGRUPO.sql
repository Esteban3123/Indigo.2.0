CREATE TABLE [dbo].[HCLISTACCXCHGRUPO] (
    [ID]          INT IDENTITY (1, 1) NOT NULL,
    [IDHCLISTACC] INT NOT NULL,
    [IDCHGRUPO]   INT NOT NULL,
    CONSTRAINT [PK_HCLISTACCXCHGRUPO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCLISTACCXCHGRUPO_CHGRUPO] FOREIGN KEY ([IDCHGRUPO]) REFERENCES [dbo].[CHGRUPO] ([ID]),
    CONSTRAINT [FK_HCLISTACCXCHGRUPO_HCLISTACC] FOREIGN KEY ([IDHCLISTACC]) REFERENCES [dbo].[HCLISTACC] ([CODCONSEC])
);


GO
ALTER TABLE [dbo].[HCLISTACCXCHGRUPO] NOCHECK CONSTRAINT [FK_HCLISTACCXCHGRUPO_CHGRUPO];


GO
ALTER TABLE [dbo].[HCLISTACCXCHGRUPO] NOCHECK CONSTRAINT [FK_HCLISTACCXCHGRUPO_HCLISTACC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del grupo de lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHGRUPO', @level2type = N'COLUMN', @level2name = N'IDCHGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID cabecera lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHGRUPO', @level2type = N'COLUMN', @level2name = N'IDHCLISTACC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHGRUPO', @level2type = N'COLUMN', @level2name = N'ID';

