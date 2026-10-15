CREATE TABLE [dbo].[ADCONCOED] (
    [CODCONCEC] NUMERIC (18) NOT NULL,
    [NUMCONCIT] CHAR (20)    NOT NULL,
    CONSTRAINT [PK_ADCONCOED] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC, [NUMCONCIT] ASC),
    CONSTRAINT [FK_ADCONCOED_ADCONCOEX] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[ADCONCOEX] ([CODCONCEC])
);


GO
ALTER TABLE [dbo].[ADCONCOED] NOCHECK CONSTRAINT [FK_ADCONCOED_ADCONCOEX];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero Consecutivo Cita en Interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOED', @level2type = N'COLUMN', @level2name = N'NUMCONCIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Consecutivo Interno (Autonumerico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOED', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

