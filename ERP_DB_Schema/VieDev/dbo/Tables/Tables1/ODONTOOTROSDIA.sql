CREATE TABLE [dbo].[ODONTOOTROSDIA] (
    [ID]            INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDODONTOGRAMA] INT            NOT NULL,
    [CODDIAGNO]     CHAR (4)       NOT NULL,
    [OBSDIAGNO]     NVARCHAR (250) NULL,
    CONSTRAINT [PK_ODONTOOTROSDIA__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTOOTROSDIA_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_ODONTOOTROSDIA_ODONTOGCTR] FOREIGN KEY ([IDODONTOGRAMA]) REFERENCES [dbo].[ODONTOGCTR] ([ID])
);


GO
ALTER TABLE [dbo].[ODONTOOTROSDIA] NOCHECK CONSTRAINT [FK_ODONTOOTROSDIA_INDIAGNOS];


GO
ALTER TABLE [dbo].[ODONTOOTROSDIA] NOCHECK CONSTRAINT [FK_ODONTOOTROSDIA_ODONTOGCTR];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación general del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSDIA', @level2type = N'COLUMN', @level2name = N'OBSDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el código del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSDIA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id de odontograma ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSDIA', @level2type = N'COLUMN', @level2name = N'IDODONTOGRAMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSDIA', @level2type = N'COLUMN', @level2name = N'ID';

