CREATE TABLE [dbo].[ADPARAMET] (
    [CODCENATE]                 CHAR (10)    NOT NULL,
    [TRINUMCLA]                 INT          NULL,
    [TRINUMTR1]                 INT          NULL,
    [TRINUMTR2]                 INT          NULL,
    [TRINUMTR3]                 INT          NULL,
    [TRINUMTR4]                 INT          NULL,
    [VIGMESING]                 INT          NULL,
    [INGAUTOMA]                 BIT          NULL,
    [INGHOSPIT]                 BIT          NULL,
    [PERMODTRI]                 BIT          NULL,
    [SALARIMIN]                 MONEY        NULL,
    [INDAUDFOR]                 NUMERIC (18) NOT NULL,
    [SERVACINTE]                CHAR (1)     NULL,
    [NUMHORATE]                 INT          NULL,
    [TRINUMTR5]                 INT          NULL,
    [SERRASANTI]                CHAR (20)    NULL,
    [NUMHORHOS]                 INT          NULL,
    [IDDESCRIPCIONRELACIONADA]  INT          NULL,
    [CREARPACIENTEDATOSBASICOS] BIT          NULL,
    [AllowPreTriage]            BIT          NULL,
    CONSTRAINT [PK_ADPARAMET] PRIMARY KEY CLUSTERED ([CODCENATE] ASC),
    CONSTRAINT [FK_ADPARAMET_INCUPSIPS] FOREIGN KEY ([SERRASANTI]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);


GO
ALTER TABLE [dbo].[ADPARAMET] NOCHECK CONSTRAINT [FK_ADPARAMET_INCUPSIPS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permitir crear paciente triage con datos básicos:   1 - SI   2 - NO  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'CREARPACIENTEDATOSBASICOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permitir Ingresos Hospitalarios:  True = Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'INGHOSPIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Crear Ingreso Automatico en Triage:  True = Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'INGAUTOMA';

