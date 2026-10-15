CREATE TABLE [dbo].[HCPARALEINT] (
    [CODCENATE]           CHAR (10) NOT NULL,
    [CODSERIPS]           CHAR (20) NOT NULL,
    [TIEMAXATE]           TINYINT   NULL,
    [UNITIEATE]           TINYINT   NULL,
    [TIEMAXATE2]          TINYINT   NULL,
    [UNITIEATE2]          TINYINT   NULL,
    [ID]                  INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [USUARIOCREACION]     CHAR (20) NULL,
    [FECHACREACION]       DATETIME  NULL,
    [USUARIOMODIFICACION] CHAR (20) NULL,
    [FECHAMODIFICACION]   DATETIME  NULL,
    CONSTRAINT [PK_HCPARALEINT] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPARALEINT_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPARALEINT_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCPARALEINT_SEGusuaru_1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [FK_HCPARALEINT_SEGusuaru_2] FOREIGN KEY ([USUARIOMODIFICACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [IX_HCPARALEINT] UNIQUE NONCLUSTERED ([CODCENATE] ASC, [CODSERIPS] ASC)
);


GO
ALTER TABLE [dbo].[HCPARALEINT] NOCHECK CONSTRAINT [FK_HCPARALEINT_ADCENATEN];


GO
ALTER TABLE [dbo].[HCPARALEINT] NOCHECK CONSTRAINT [FK_HCPARALEINT_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCPARALEINT] NOCHECK CONSTRAINT [FK_HCPARALEINT_SEGusuaru_1];


GO
ALTER TABLE [dbo].[HCPARALEINT] NOCHECK CONSTRAINT [FK_HCPARALEINT_SEGusuaru_2];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEINT', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEINT', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEINT', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEINT', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID o Autonumérico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEINT', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Interconsultas - Unidad de Tiempo Maximo de Atencion 2 Instancia (1-Minutos,2-Horas,3-Dias)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEINT', @level2type = N'COLUMN', @level2name = N'UNITIEATE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo Maximo de segunda instancia para la entrega del resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEINT', @level2type = N'COLUMN', @level2name = N'TIEMAXATE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Interconsultas - Unidad de Tiempo Maximo de Atencion (1-Minutos,2-Horas,3-Dias)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEINT', @level2type = N'COLUMN', @level2name = N'UNITIEATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo Maximo de primera instancia para la entrega del resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEINT', @level2type = N'COLUMN', @level2name = N'TIEMAXATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEINT', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALEINT', @level2type = N'COLUMN', @level2name = N'CODCENATE';

