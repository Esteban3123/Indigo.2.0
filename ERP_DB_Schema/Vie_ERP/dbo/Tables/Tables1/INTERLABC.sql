CREATE TABLE [dbo].[INTERLABC] (
    [AUTO]         INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ORDEN_INDIGO] VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_INTERLABC] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
CREATE NONCLUSTERED INDEX [_dta_index_INTERLABC_7_585821199__K1_K2]
    ON [dbo].[INTERLABC]([AUTO] ASC, [ORDEN_INDIGO] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_INTERLABC__ORDEN_INDIGO__INC__AUTO]
    ON [dbo].[INTERLABC]([ORDEN_INDIGO] ASC)
    INCLUDE([AUTO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Orden Indigo  Paciente concatenado con el numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABC', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'AutoNumerico Identity', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABC', @level2type = N'COLUMN', @level2name = N'AUTO';

