CREATE TABLE [dbo].[ADPOBESPE] (
    [ID]             INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGO]         VARCHAR (2)   NOT NULL,
    [DESCRIPCION]    VARCHAR (200) NOT NULL,
    [ESTADO]         BIT           NOT NULL,
    [RIESGO]         VARCHAR (500) NULL,
    [INTERVENCION]   VARCHAR (MAX) NULL,
    [TIPOPOBESP]     VARCHAR (3)   CONSTRAINT [DF_ADPOBESPE_TIPOPOBESP] DEFAULT ((1)) NOT NULL,
    [TIPOPOESPERIES] INT           NULL,
    CONSTRAINT [PK_ADPOBESPE] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Población con Riesgo  1-> Si  2->No  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE', @level2type = N'COLUMN', @level2name = N'TIPOPOESPERIES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de población', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE', @level2type = N'COLUMN', @level2name = N'TIPOPOBESP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'intervención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE', @level2type = N'COLUMN', @level2name = N'INTERVENCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE', @level2type = N'COLUMN', @level2name = N'RIESGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado   1: activo  0: inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE', @level2type = N'COLUMN', @level2name = N'CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumérico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPE', @level2type = N'COLUMN', @level2name = N'ID';

