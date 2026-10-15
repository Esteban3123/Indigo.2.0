CREATE TABLE [dbo].[HCPCEPLANVAL] (
    [IDPLANVAL]    INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPCECONTROL] INT NOT NULL,
    [CODVALORAPCE] INT NOT NULL,
    CONSTRAINT [PK_HCPCEPLANVAL] PRIMARY KEY CLUSTERED ([IDPLANVAL] ASC),
    CONSTRAINT [FK_HCPCEPLANVAL_HCPCECONTROL] FOREIGN KEY ([IDPCECONTROL]) REFERENCES [dbo].[HCPCECONTROL] ([ID]),
    CONSTRAINT [FK_HCPCEPLANVAL_HCVALORAPCE] FOREIGN KEY ([CODVALORAPCE]) REFERENCES [dbo].[HCVALORAPCE] ([CODVALORAPCE])
);


GO
ALTER TABLE [dbo].[HCPCEPLANVAL] NOCHECK CONSTRAINT [FK_HCPCEPLANVAL_HCPCECONTROL];


GO
ALTER TABLE [dbo].[HCPCEPLANVAL] NOCHECK CONSTRAINT [FK_HCPCEPLANVAL_HCVALORAPCE];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la valoración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEPLANVAL', @level2type = N'COLUMN', @level2name = N'CODVALORAPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro de control', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEPLANVAL', @level2type = N'COLUMN', @level2name = N'IDPCECONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEPLANVAL', @level2type = N'COLUMN', @level2name = N'IDPLANVAL';

