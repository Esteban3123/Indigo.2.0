CREATE TABLE [dbo].[ODONTOGCTRLVAL] (
    [ID]        INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCONTROL] INT     NOT NULL,
    [CEO_C]     TINYINT NOT NULL,
    [CEO_E]     TINYINT NOT NULL,
    [CEO_O]     TINYINT NOT NULL,
    [CPO_C]     TINYINT NOT NULL,
    [CPO_P]     TINYINT NOT NULL,
    [CPO_O]     TINYINT NOT NULL,
    CONSTRAINT [PK_ODONTOGCTRLVAL__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTOGCTRLVAL_ODONTOGCTR] FOREIGN KEY ([IDCONTROL]) REFERENCES [dbo].[ODONTOGCTR] ([ID])
);


GO
ALTER TABLE [dbo].[ODONTOGCTRLVAL] NOCHECK CONSTRAINT [FK_ODONTOGCTRLVAL_ODONTOGCTR];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el CPO Obturado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL', @level2type = N'COLUMN', @level2name = N'CPO_O';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el CPOPerdido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL', @level2type = N'COLUMN', @level2name = N'CPO_P';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el CPO Cariado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL', @level2type = N'COLUMN', @level2name = N'CPO_C';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el valor  CEO_O', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL', @level2type = N'COLUMN', @level2name = N'CEO_O';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el valor  CEO_E', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL', @level2type = N'COLUMN', @level2name = N'CEO_E';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el valor  CEO_C', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL', @level2type = N'COLUMN', @level2name = N'CEO_C';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el id de control odontograma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL', @level2type = N'COLUMN', @level2name = N'IDCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL', @level2type = N'COLUMN', @level2name = N'ID';

