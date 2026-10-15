CREATE TABLE [dbo].[RIASDIAGNOS] (
    [ID]        INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDRIAS]    INT      NOT NULL,
    [CODDIAGNO] CHAR (4) NOT NULL,
    CONSTRAINT [PK_RIASDIAGNOS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RIASDIAGNOS_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_RIASDIAGNOS_RIAS] FOREIGN KEY ([IDRIAS]) REFERENCES [dbo].[RIAS] ([ID])
);


GO
ALTER TABLE [dbo].[RIASDIAGNOS] NOCHECK CONSTRAINT [FK_RIASDIAGNOS_INDIAGNOS];


GO
ALTER TABLE [dbo].[RIASDIAGNOS] NOCHECK CONSTRAINT [FK_RIASDIAGNOS_RIAS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASDIAGNOS', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID RIAS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASDIAGNOS', @level2type = N'COLUMN', @level2name = N'IDRIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASDIAGNOS', @level2type = N'COLUMN', @level2name = N'ID';

