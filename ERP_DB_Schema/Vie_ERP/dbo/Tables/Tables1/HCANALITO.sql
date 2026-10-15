CREATE TABLE [dbo].[HCANALITO] (
    [ID]           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE]    CHAR (10)     NOT NULL,
    [CODIGO]       VARCHAR (3)   NOT NULL,
    [NOMBRE]       VARCHAR (150) NOT NULL,
    [ESTADO]       TINYINT       NOT NULL,
    [FECHCREACION] DATETIME      NOT NULL,
    [USUCREACION]  CHAR (20)     NOT NULL,
    [FECHMODIF]    DATETIME      NULL,
    [USUMODIF]     CHAR (20)     NULL,
    [SIGLA]        VARCHAR (150) NULL,
    CONSTRAINT [PK_HCANALITO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCANALITO_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE])
);


GO
ALTER TABLE [dbo].[HCANALITO] NOCHECK CONSTRAINT [FK_HCANALITO_ADCENATEN];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sigla del Analito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANALITO', @level2type = N'COLUMN', @level2name = N'SIGLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANALITO', @level2type = N'COLUMN', @level2name = N'USUMODIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANALITO', @level2type = N'COLUMN', @level2name = N'FECHMODIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANALITO', @level2type = N'COLUMN', @level2name = N'USUCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creacion Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANALITO', @level2type = N'COLUMN', @level2name = N'FECHCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 ->Activo  2 -> inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANALITO', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Analito como viene de la interfaz con el fin de asegurar la Homologacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANALITO', @level2type = N'COLUMN', @level2name = N'NOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Analito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANALITO', @level2type = N'COLUMN', @level2name = N'CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Centro Atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANALITO', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANALITO', @level2type = N'COLUMN', @level2name = N'ID';

