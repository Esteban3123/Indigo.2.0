CREATE TABLE [dbo].[ODONTODIENTE] (
    [ID]              INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDODONTOCONTROL] INT         NOT NULL,
    [DIENTE]          TINYINT     NOT NULL,
    [TIPOODONTOGRAMA] VARCHAR (1) NOT NULL,
    CONSTRAINT [PK_ODONTODIENTE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTODIENTE_ODONTOCONTROL] FOREIGN KEY ([IDODONTOCONTROL]) REFERENCES [dbo].[ODONTOCONTROL] ([ID])
);


GO
ALTER TABLE [dbo].[ODONTODIENTE] NOCHECK CONSTRAINT [FK_ODONTODIENTE_ODONTOCONTROL];


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ODONTODIENTE]
    ON [dbo].[ODONTODIENTE]([IDODONTOCONTROL] ASC, [DIENTE] ASC, [TIPOODONTOGRAMA] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'V - Valoracion  T - Tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTE', @level2type = N'COLUMN', @level2name = N'TIPOODONTOGRAMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del diente del odontograma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTE', @level2type = N'COLUMN', @level2name = N'DIENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla ODONTOCONTROL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTE', @level2type = N'COLUMN', @level2name = N'IDODONTOCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTE', @level2type = N'COLUMN', @level2name = N'ID';

