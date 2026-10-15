CREATE TABLE [dbo].[ADAUTSEDX] (
    [NUMCONSEC] NUMERIC (18) IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONCEC] NUMERIC (18) NOT NULL,
    [CODDIAGNO] CHAR (4)     NOT NULL,
    [CODDIAPRI] BIT          NOT NULL,
    CONSTRAINT [PK_ADAUTSEDX] PRIMARY KEY CLUSTERED ([NUMCONSEC] ASC),
    CONSTRAINT [FK_ADAUTSEDX_ADAUTSERC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[ADAUTSERC] ([CODCONCEC]),
    CONSTRAINT [FK_ADAUTSEDX_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);


GO
ALTER TABLE [dbo].[ADAUTSEDX] NOCHECK CONSTRAINT [FK_ADAUTSEDX_ADAUTSERC];


GO
ALTER TABLE [dbo].[ADAUTSEDX] NOCHECK CONSTRAINT [FK_ADAUTSEDX_INDIAGNOS];


GO
CREATE NONCLUSTERED INDEX [IX_ADAUTSEDX]
    ON [dbo].[ADAUTSEDX]([CODCONCEC] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Diagnostico Principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSEDX', @level2type = N'COLUMN', @level2name = N'CODDIAPRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSEDX', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo Interno de la Cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSEDX', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo Interno de tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSEDX', @level2type = N'COLUMN', @level2name = N'NUMCONSEC';

