CREATE TABLE [dbo].[ANTVARIABLESD] (
    [ID]        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPADRE]   INT           NOT NULL,
    [IDHIJO]    INT           NOT NULL,
    [EXPRESION] VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_ANTVARIABLESD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ANTVARIABLESD_ANTVARIABLES] FOREIGN KEY ([IDPADRE]) REFERENCES [dbo].[ANTVARIABLES] ([ID]),
    CONSTRAINT [FK_ANTVARIABLESD_ANTVARIABLES1] FOREIGN KEY ([IDHIJO]) REFERENCES [dbo].[ANTVARIABLES] ([ID])
);


GO
ALTER TABLE [dbo].[ANTVARIABLESD] NOCHECK CONSTRAINT [FK_ANTVARIABLESD_ANTVARIABLES];


GO
ALTER TABLE [dbo].[ANTVARIABLESD] NOCHECK CONSTRAINT [FK_ANTVARIABLESD_ANTVARIABLES1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla ANTVARIABLES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDHIJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLESD', @level2type = N'COLUMN', @level2name = N'ID';

