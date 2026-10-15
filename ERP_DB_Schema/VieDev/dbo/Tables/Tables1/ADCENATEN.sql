CREATE TABLE [dbo].[ADCENATEN] (
    [CODCENATE] CHAR (10)       NOT NULL,
    [NOMCENATE] CHAR (100)      NOT NULL,
    [CODIPSSEC] CHAR (13)       NOT NULL,
    [NIVATENCI] INT             NOT NULL,
    [DIRCENATE] CHAR (80)       NOT NULL,
    [INDNUMTEL] CHAR (5)        NOT NULL,
    [NUMTELCEN] CHAR (7)        NOT NULL,
    [NUMEXTTEL] CHAR (6)        NULL,
    [NUMCELCEN] CHAR (10)       NULL,
    [DEPMUNCOD] CHAR (5)        NOT NULL,
    [INDAUDFOR] NUMERIC (18)    NOT NULL,
    [LATITUD]   NUMERIC (18, 6) NULL,
    [LONGITUD]  NUMERIC (18, 6) NULL,
    CONSTRAINT [PK_ADcenaten] PRIMARY KEY CLUSTERED ([CODCENATE] ASC),
    CONSTRAINT [FK_CODCENATE_CODCENATE] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE])
);


GO
ALTER TABLE [dbo].[ADCENATEN] NOCHECK CONSTRAINT [FK_CODCENATE_CODCENATE];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Longitud centro atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'LONGITUD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la  Latitud centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'LATITUD';

