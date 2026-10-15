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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 ->Activo  2 -> inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANALITO', @level2type = N'COLUMN', @level2name = N'ESTADO';

