CREATE TABLE [dbo].[HCESCADETALLE] (
    [ID]         INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCESCALAID] INT         NOT NULL,
    [RESULTADO]  INT         NOT NULL,
    [TIPOESCALA] INT         NULL,
    [AREAEVALU]  VARCHAR (1) NULL,
    CONSTRAINT [PK_HCESCADETALLE_1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCESCADETALLE_HCESCALAS1] FOREIGN KEY ([HCESCALAID]) REFERENCES [dbo].[HCESCALAS] ([ID])
);


GO
ALTER TABLE [dbo].[HCESCADETALLE] NOCHECK CONSTRAINT [FK_HCESCADETALLE_HCESCALAS1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Solo cuando la escala sea Clasificación EPOC se llena en estee campo la siguiente información:  1 - Escala Disena  2 - Escala CAT  18 - Exacerbaciones  Esta información viene del formulario de Clasificación EPOC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCADETALLE', @level2type = N'COLUMN', @level2name = N'TIPOESCALA';

