CREATE TABLE [dbo].[HCLISTACD] (
    [CODCONSEC]    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONSECC]   INT           NOT NULL,
    [DESPREENC]    VARCHAR (250) NOT NULL,
    [CAMPOTEXTO]   BIT           NULL,
    [CAMPOFECHA]   BIT           NULL,
    [CAMPOADJUNTO] BIT           NULL,
    [TIPOCAMPO]    INT           NULL,
    CONSTRAINT [PK_HCLISTACD] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC),
    CONSTRAINT [FK_HCLISTACD_HCLISTACC] FOREIGN KEY ([CODCONSECC]) REFERENCES [dbo].[HCLISTACC] ([CODCONSEC])
);


GO
ALTER TABLE [dbo].[HCLISTACD] NOCHECK CONSTRAINT [FK_HCLISTACD_HCLISTACC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el tipo de campo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACD', @level2type = N'COLUMN', @level2name = N'TIPOCAMPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'campo adjunto  si      no ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACD', @level2type = N'COLUMN', @level2name = N'CAMPOADJUNTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'campo fecha ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACD', @level2type = N'COLUMN', @level2name = N'CAMPOFECHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el campo de texto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACD', @level2type = N'COLUMN', @level2name = N'CAMPOTEXTO';

