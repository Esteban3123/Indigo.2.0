CREATE TABLE [EHR].[HCORDINSUMOS] (
    [ID]            INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SchemesId]     INT       NOT NULL,
    [IDHCORDQUIMIO] INT       NOT NULL,
    [CODIGOINSUMO]  CHAR (20) NOT NULL,
    [CICLO]         INT       NOT NULL,
    [DIA]           INT       NOT NULL,
    [CANTIDAD]      INT       NOT NULL,
    CONSTRAINT [PK_HCORDINSUMOS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCORDINSUMOS_HCORDQUIMIO] FOREIGN KEY ([IDHCORDQUIMIO]) REFERENCES [EHR].[HCORDQUIMIO] ([ID]),
    CONSTRAINT [FK_HCORDINSUMOS_IHLISTPRO] FOREIGN KEY ([CODIGOINSUMO]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_HCORDINSUMOS_Schemes] FOREIGN KEY ([SchemesId]) REFERENCES [EHR].[Schemes] ([Id])
);


GO
ALTER TABLE [EHR].[HCORDINSUMOS] NOCHECK CONSTRAINT [FK_HCORDINSUMOS_HCORDQUIMIO];


GO
ALTER TABLE [EHR].[HCORDINSUMOS] NOCHECK CONSTRAINT [FK_HCORDINSUMOS_IHLISTPRO];


GO
ALTER TABLE [EHR].[HCORDINSUMOS] NOCHECK CONSTRAINT [FK_HCORDINSUMOS_Schemes];


GO
CREATE NONCLUSTERED INDEX [IX_HCORDINSUMOS_CICLO_IDHCORDQUIMIO_CODIGOINSUMO]
    ON [EHR].[HCORDINSUMOS]([CICLO] ASC, [IDHCORDQUIMIO] ASC, [CODIGOINSUMO] ASC)
    INCLUDE([CANTIDAD], [DIA]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad de Insumo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDINSUMOS', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'DIA', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDINSUMOS', @level2type = N'COLUMN', @level2name = N'DIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'N° del Ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDINSUMOS', @level2type = N'COLUMN', @level2name = N'CICLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Insumo relacion con la tabla de IHLISTRPO', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDINSUMOS', @level2type = N'COLUMN', @level2name = N'CODIGOINSUMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la cabecera de ordenamiento de quimioterapia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDINSUMOS', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con el Esquema parametrizado', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDINSUMOS', @level2type = N'COLUMN', @level2name = N'SchemesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDINSUMOS', @level2type = N'COLUMN', @level2name = N'ID';

