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
CREATE NONCLUSTERED INDEX [IX_ADCENATEN]
    ON [dbo].[ADCENATEN]([CODCENATE] ASC, [NOMCENATE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Longitud centro atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'LONGITUD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la  Latitud centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'LATITUD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Municipio y Departamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero Telefonico Movil', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'NUMCELCEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Extension', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'NUMEXTTEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero Telefonico del Prestador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'NUMTELCEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicativo del Numero Telefonico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'INDNUMTEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Direccion del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'DIRCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nivel de Atencion IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'NIVATENCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la IPS ante la Secretaria Departamental de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'CODIPSSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'NOMCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCENATEN', @level2type = N'COLUMN', @level2name = N'CODCENATE';

