CREATE TABLE [dbo].[ODONTOCONTROLVALO] (
    [ID]              INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDODONTOCONTROL] INT     NOT NULL,
    [CEO_C]           TINYINT NOT NULL,
    [CEO_E]           TINYINT NOT NULL,
    [CEO_O]           TINYINT NOT NULL,
    [CPO_C]           TINYINT NOT NULL,
    [CPO_P]           TINYINT NOT NULL,
    [CPO_O]           TINYINT NOT NULL,
    CONSTRAINT [PK_ODONTOVALORACION] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTOCONTROLVALO_ODONTOCONTROL] FOREIGN KEY ([IDODONTOCONTROL]) REFERENCES [dbo].[ODONTOCONTROL] ([ID])
);


GO
ALTER TABLE [dbo].[ODONTOCONTROLVALO] NOCHECK CONSTRAINT [FK_ODONTOCONTROLVALO_ODONTOCONTROL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiene relación con la tabla ODONTOCONTROL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROLVALO', @level2type = N'COLUMN', @level2name = N'IDODONTOCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROLVALO', @level2type = N'COLUMN', @level2name = N'ID';

