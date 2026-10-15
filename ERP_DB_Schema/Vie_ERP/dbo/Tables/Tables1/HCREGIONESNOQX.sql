CREATE TABLE [dbo].[HCREGIONESNOQX] (
    [ID]             INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AUTOHCORPNOQ]   INT           NOT NULL,
    [NOMREGTRATA]    VARCHAR (100) NOT NULL,
    [DOSTOTAL]       INT           NOT NULL,
    [UNMEDDOSISTO]   INT           NOT NULL,
    [DOSTOTALAPROB]  INT           NOT NULL,
    [DOSFRACC]       INT           NOT NULL,
    [UNMEDDOSFRAC]   INT           NOT NULL,
    [NUMEROFRACCION] INT           NOT NULL,
    [CODDIAGNO]      CHAR (4)      NULL,
    CONSTRAINT [PK_HCREGIONESNOQX] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCREGIONESNOQX_HCREGIONESNOQX] FOREIGN KEY ([AUTOHCORPNOQ]) REFERENCES [dbo].[HCORDPRON] ([AUTO]),
    CONSTRAINT [FK_HCREGIONESNOQX_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);


GO
ALTER TABLE [dbo].[HCREGIONESNOQX] NOCHECK CONSTRAINT [FK_HCREGIONESNOQX_HCREGIONESNOQX];


GO
ALTER TABLE [dbo].[HCREGIONESNOQX] NOCHECK CONSTRAINT [FK_HCREGIONESNOQX_INDIAGNOS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo diagnostico Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de Fracción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'NUMEROFRACCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de medidad de Dosis Fracción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'UNMEDDOSFRAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis fraccion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'DOSFRACC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis total aprobado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'DOSTOTALAPROB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de medida de la Dosis Total', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'UNMEDDOSISTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis Total', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'DOSTOTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la Region de tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'NOMREGTRATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'AUTOHCORPNOQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGIONESNOQX', @level2type = N'COLUMN', @level2name = N'ID';

