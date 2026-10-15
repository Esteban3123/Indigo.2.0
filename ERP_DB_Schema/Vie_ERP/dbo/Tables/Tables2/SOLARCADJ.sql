CREATE TABLE [dbo].[SOLARCADJ] (
    [AUTO]      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NOMARCADJ] VARCHAR (100) NOT NULL,
    [AUTOCOTI]  INT           NOT NULL,
    CONSTRAINT [PK_SOLARCADJ] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_SOLARCADJ_SOLCOTI] FOREIGN KEY ([AUTOCOTI]) REFERENCES [dbo].[SOLCOTI] ([AUTO])
);


GO
ALTER TABLE [dbo].[SOLARCADJ] NOCHECK CONSTRAINT [FK_SOLARCADJ_SOLCOTI];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la cotizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLARCADJ', @level2type = N'COLUMN', @level2name = N'AUTOCOTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del archivo guardado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLARCADJ', @level2type = N'COLUMN', @level2name = N'NOMARCADJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLARCADJ', @level2type = N'COLUMN', @level2name = N'AUTO';

