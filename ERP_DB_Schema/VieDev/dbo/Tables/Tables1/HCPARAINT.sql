CREATE TABLE [dbo].[HCPARAINT] (
    [CODCONCEC] INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE] CHAR (10)       NOT NULL,
    [VALMEDESP] BIT             NOT NULL,
    [VALMEDGEN] BIT             NOT NULL,
    [CALFOLIOS] INT             NULL,
    [CALFDESDE] DECIMAL (18, 1) NULL,
    [CALFHASTA] DECIMAL (18, 1) NULL,
    CONSTRAINT [PK_HCPARAINT] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_HCPARAINT_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE])
);


GO
ALTER TABLE [dbo].[HCPARAINT] NOCHECK CONSTRAINT [FK_HCPARAINT_ADCENATEN];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Calificacion Folios Academicos   1: Exigir Calificación  2: Calificación Opcional  3: Ninguno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAINT', @level2type = N'COLUMN', @level2name = N'CALFOLIOS';

