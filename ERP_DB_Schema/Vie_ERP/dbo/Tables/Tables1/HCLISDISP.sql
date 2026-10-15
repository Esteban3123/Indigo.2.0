CREATE TABLE [dbo].[HCLISDISP] (
    [AUTO]       INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONSECC] INT       NOT NULL,
    [CODSERMED]  CHAR (20) NOT NULL,
    CONSTRAINT [PK_HCDISPLIS] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCDISPLIS_HCLISTACC] FOREIGN KEY ([CODCONSECC]) REFERENCES [dbo].[HCLISTACC] ([CODCONSEC])
);


GO
ALTER TABLE [dbo].[HCLISDISP] NOCHECK CONSTRAINT [FK_HCDISPLIS_HCLISTACC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Servicio o Medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISDISP', @level2type = N'COLUMN', @level2name = N'CODSERMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISDISP', @level2type = N'COLUMN', @level2name = N'CODCONSECC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISDISP', @level2type = N'COLUMN', @level2name = N'AUTO';

