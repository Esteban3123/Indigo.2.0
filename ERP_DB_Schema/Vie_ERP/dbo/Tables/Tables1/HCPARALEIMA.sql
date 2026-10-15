CREATE TABLE [dbo].[HCPARALEIMA] (
    [CODCENATE]           CHAR (10) NOT NULL,
    [CODSERIPS]           CHAR (20) NOT NULL,
    [IMAREAEXA]           TINYINT   NULL,
    [UNITIEREA]           TINYINT   NULL,
    [IMAREAEXA2]          TINYINT   NULL,
    [UNITIEREA2]          TINYINT   NULL,
    [IMAENTRES]           TINYINT   NULL,
    [UNITIERES]           TINYINT   NULL,
    [IMAENTRES2]          TINYINT   NULL,
    [UNITIERES2]          TINYINT   NULL,
    [ID]                  INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [USUARIOCREACION]     CHAR (20) NULL,
    [FECHACREACION]       DATETIME  NULL,
    [USUARIOMODIFICACION] CHAR (20) NULL,
    [FECHAMODIFICACION]   DATETIME  NULL,
    CONSTRAINT [PK_HCPARALEIMA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPARALEIMA_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPARALEIMA_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCPARALEIMA_SEGusuaru_1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [FK_HCPARALEIMA_SEGusuaru_2] FOREIGN KEY ([USUARIOMODIFICACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [IX_HCPARALEIMA] UNIQUE NONCLUSTERED ([CODCENATE] ASC, [CODSERIPS] ASC)
);


GO
ALTER TABLE [dbo].[HCPARALEIMA] NOCHECK CONSTRAINT [FK_HCPARALEIMA_ADCENATEN];


GO
ALTER TABLE [dbo].[HCPARALEIMA] NOCHECK CONSTRAINT [FK_HCPARALEIMA_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCPARALEIMA] NOCHECK CONSTRAINT [FK_HCPARALEIMA_SEGusuaru_1];


GO
ALTER TABLE [dbo].[HCPARALEIMA] NOCHECK CONSTRAINT [FK_HCPARALEIMA_SEGusuaru_2];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID o Autonumérico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Imagenologia - Unidad de Tiempo Maximo de Entrega de Resultado 2 Instancia (1-Minutos,2-Horas,3-Dias)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'UNITIERES2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Imagenologia - Tiempo Maximo de Entrega de Resultado 2 Instancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'IMAENTRES2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Imagenologia - Unidad de Tiempo Maximo de Entrega de Resultado (1-Minutos,2-Horas,3-Dias)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'UNITIERES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Imagenologia - Tiempo Maximo de Entrega de Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'IMAENTRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Imagenologia - Unidad de Tiempo Maximo de Realizacion del Examen 2 Instancia (1-Minutos,2-Horas,3-Dias)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'UNITIEREA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Imagenologia - Tiempo Maximo de Realizacion del Examen 2 Instancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'IMAREAEXA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Imagenologia - Unidad de Tiempo Maximo de Realizacion del Examen (1-Minutos,2-Horas,3-Dias)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'UNITIEREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Imagenologia - Tiempo Maximo de Realizacion del Examen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'IMAREAEXA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEIMA', @level2type = N'COLUMN', @level2name = N'CODCENATE';

