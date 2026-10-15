CREATE TABLE [dbo].[PARACAMED] (
    [ID]            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE]     CHAR (10)       NOT NULL,
    [CODPRODUC]     CHAR (20)       NOT NULL,
    [CODVIAADM]     VARCHAR (20)    NULL,
    [CODFORMED]     CHAR (3)        NULL,
    [DOSISPROD]     NUMERIC (18, 2) NULL,
    [CODUNIMED]     CHAR (3)        NULL,
    [FRECUENCI]     INT             NULL,
    [UNIFRECUE]     CHAR (1)        NULL,
    [DURACIDOS]     CHAR (20)       NULL,
    [CANPEDPRO]     INT             NOT NULL,
    [JUSTIFICACION] VARCHAR (MAX)   NULL,
    [VALDURFIJ]     INT             NULL,
    [UNIDURFIJ]     CHAR (1)        NULL,
    [INDAPLMED]     VARCHAR (MAX)   NULL,
    [CARGAUTO]      INT             NOT NULL,
    [DOSISPRFN]     NUMERIC (18, 2) NULL,
    [CODUNIMFN]     CHAR (3)        NULL,
    [DESADMINI]     VARCHAR (MAX)   NULL,
    [TIPFORMED]     CHAR (1)        NULL,
    [TOTPROUNI]     NUMERIC (18, 2) NULL,
    CONSTRAINT [PK_PARACAMED] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PARACAMED_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_PARACAMED_HCVIAADMI] FOREIGN KEY ([CODVIAADM]) REFERENCES [dbo].[HCVIAADMI] ([CODVIAADM])
);


GO
ALTER TABLE [dbo].[PARACAMED] NOCHECK CONSTRAINT [FK_PARACAMED_ADCENATEN];


GO
ALTER TABLE [dbo].[PARACAMED] NOCHECK CONSTRAINT [FK_PARACAMED_HCVIAADMI];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Total del producto por unidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'TOTPROUNI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Formulacion del medicamento:  1 Peso  2 Volumen  3 Peso-Volumen  4 Unidad de Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'TIPFORMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad del Valor de la duracion fija:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Duracion de la Dosis   Fija   tratamientos continuo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'DURACIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad del Valor de la Frecuencia:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';

