CREATE TABLE [dbo].[HCPARBALA] (
    [CODCENATE]           CHAR (10)    NOT NULL,
    [UFUCODIGO]           CHAR (10)    NOT NULL,
    [COBALACIBA]          CHAR (3)     NOT NULL,
    [DESBALACIB]          VARCHAR (50) NOT NULL,
    [ESTADO]              BIT          NOT NULL,
    [ID]                  INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [USUARIOCREACION]     CHAR (20)    NULL,
    [FECHACREACION]       DATETIME     NULL,
    [USUARIOMODIFICACION] CHAR (20)    NULL,
    [FECHAMODIFICACION]   DATETIME     NULL,
    CONSTRAINT [PK_HCPARBALA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPARBALA_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPARBALA_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCPARBALA_SEGusuaru_1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [FK_HCPARBALA_SEGusuaru_2] FOREIGN KEY ([USUARIOMODIFICACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [IX_HCPARBALA] UNIQUE NONCLUSTERED ([CODCENATE] ASC, [UFUCODIGO] ASC, [COBALACIBA] ASC)
);


GO
ALTER TABLE [dbo].[HCPARBALA] NOCHECK CONSTRAINT [FK_HCPARBALA_ADCENATEN];


GO
ALTER TABLE [dbo].[HCPARBALA] NOCHECK CONSTRAINT [FK_HCPARBALA_INUNIFUNC];


GO
ALTER TABLE [dbo].[HCPARBALA] NOCHECK CONSTRAINT [FK_HCPARBALA_SEGusuaru_1];


GO
ALTER TABLE [dbo].[HCPARBALA] NOCHECK CONSTRAINT [FK_HCPARBALA_SEGusuaru_2];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARBALA', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARBALA', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARBALA', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARBALA', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID o Autonumérico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARBALA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'estado del registo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARBALA', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Balance de Acido Base', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARBALA', @level2type = N'COLUMN', @level2name = N'DESBALACIB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del balance de acido base', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARBALA', @level2type = N'COLUMN', @level2name = N'COBALACIBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARBALA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARBALA', @level2type = N'COLUMN', @level2name = N'CODCENATE';

