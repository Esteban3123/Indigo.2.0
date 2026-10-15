CREATE TABLE [dbo].[CHGENTARI] (
    [CODCONCEC]                      INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODICAMAS]                      INT       NOT NULL,
    [CODTIPEST]                      CHAR (3)  NOT NULL,
    [TIPLIQEST]                      TINYINT   NOT NULL,
    [NUMHOREST]                      TINYINT   NOT NULL,
    [GENCUPS]                        INT       NULL,
    [GENCUPS2]                       INT       NOT NULL,
    [CODCENATE]                      CHAR (10) NOT NULL,
    [UFUCODIGO]                      CHAR (10) NOT NULL,
    [IDDESCRIPCIONRELACIONADA_CUPS]  INT       NULL,
    [IDDESCRIPCIONRELACIONADA_CUPS2] INT       NULL,
    [USUARIOCREACION]                CHAR (20) NULL,
    [FECHACREACION]                  DATETIME  NULL,
    [USUARIOMODIFICACION]            CHAR (20) NULL,
    [FECHAMODIFICACION]              DATETIME  NULL,
    CONSTRAINT [PK_CHGENTARI] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_CHGENTARI_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_CHGENTARI_CHCAMASHO] FOREIGN KEY ([CODICAMAS]) REFERENCES [dbo].[CHCAMASHO] ([CODICAMAS]),
    CONSTRAINT [FK_CHGENTARI_CHGENTARI] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_CHGENTARI_CHTIPESTA] FOREIGN KEY ([CODTIPEST]) REFERENCES [dbo].[CHTIPESTA] ([CODTIPEST]),
    CONSTRAINT [FK_CHGENTARI_SEGusuaru_1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [FK_CHGENTARI_SEGusuaru_2] FOREIGN KEY ([USUARIOMODIFICACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);


GO
ALTER TABLE [dbo].[CHGENTARI] NOCHECK CONSTRAINT [FK_CHGENTARI_ADCENATEN];


GO
ALTER TABLE [dbo].[CHGENTARI] NOCHECK CONSTRAINT [FK_CHGENTARI_CHCAMASHO];


GO
ALTER TABLE [dbo].[CHGENTARI] NOCHECK CONSTRAINT [FK_CHGENTARI_CHGENTARI];


GO
ALTER TABLE [dbo].[CHGENTARI] NOCHECK CONSTRAINT [FK_CHGENTARI_CHTIPESTA];


GO
ALTER TABLE [dbo].[CHGENTARI] NOCHECK CONSTRAINT [FK_CHGENTARI_SEGusuaru_1];


GO
ALTER TABLE [dbo].[CHGENTARI] NOCHECK CONSTRAINT [FK_CHGENTARI_SEGusuaru_2];


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_CHGENTARI]
    ON [dbo].[CHGENTARI]([CODICAMAS] ASC, [CODTIPEST] ASC, [TIPLIQEST] ASC, [CODCENATE] ASC, [UFUCODIGO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción relacionada de CUPS de hospitalización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_CUPS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción relacionada de CUPS de observación de urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_CUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo CUPS Vie  1. Si el tipo de liquidacion es 1 - Urgencias, se especifica el codigo CUPS para la liquidacion de Hospitalizacion de urgencias.  2. Si el tipo de liquidacion es 2 - Hospitalizacion, se especifica el codigo CUPS para la liquidacion de Hospitalizacion.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'GENCUPS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo CUPS de Vie  1. Si el tipo de liquidacion es 1 - Urgencias, se especifica el codigo CUPS para liquidacion de Observacion. Solo se diligencia este campo en tipo liquidacion 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'GENCUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Horas Minima de Estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'NUMHOREST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de liquidacion de estancia  1: Observacion Urgencias  2: Recuperacion Post-Quirurgico  3: Hospitalaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'TIPLIQEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Tipo de Estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'CODTIPEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'CODICAMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHGENTARI', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

