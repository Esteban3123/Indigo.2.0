CREATE TABLE [dbo].[ODONTOGCTRLTRA] (
    [ID]        INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCONTROL] INT NOT NULL,
    CONSTRAINT [PK_ODONTOGCTRLTRA__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTOGCTRLTRA_ODONTOGCTRLTRA] FOREIGN KEY ([IDCONTROL]) REFERENCES [dbo].[ODONTOGCTR] ([ID])
);


GO
ALTER TABLE [dbo].[ODONTOGCTRLTRA] NOCHECK CONSTRAINT [FK_ODONTOGCTRLTRA_ODONTOGCTRLTRA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el ID control', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLTRA', @level2type = N'COLUMN', @level2name = N'IDCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLTRA', @level2type = N'COLUMN', @level2name = N'ID';

