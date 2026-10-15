CREATE TABLE [dbo].[ADACTIVID] (
    [codactivi]  CHAR (4)     NOT NULL,
    [desactivi]  CHAR (80)    NOT NULL,
    [INDAUDFOR]  NUMERIC (18) NOT NULL,
    [RIESGOAGRE] BIT          NULL,
    [ESTADO]     INT          CONSTRAINT [DF_ADACTIVID_ESTADO] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_ADactivid] PRIMARY KEY CLUSTERED ([codactivi] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado 1-> Activo 2->Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACTIVID', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Riesgo de agresión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACTIVID', @level2type = N'COLUMN', @level2name = N'RIESGOAGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACTIVID', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la Actividad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACTIVID', @level2type = N'COLUMN', @level2name = N'desactivi';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Actividad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACTIVID', @level2type = N'COLUMN', @level2name = N'codactivi';

