CREATE TABLE [dbo].[AGFARMEONCOD] (
    [ID]             INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDAGFARMAONCOC] INT             NOT NULL,
    [CODPRODUC]      CHAR (20)       NOT NULL,
    [CANPEDPRO]      INT             NOT NULL,
    [CANENTPRO]      INT             NOT NULL,
    [CANPENPRO]      INT             NOT NULL,
    [ESTADO]         INT             NOT NULL,
    [TIPO]           INT             NOT NULL,
    [CODVIAADM]      VARCHAR (20)    NULL,
    [CODUNIMED]      VARCHAR (20)    NULL,
    [DOSIS]          NUMERIC (18, 2) NULL,
    [OBSERVACI]      VARCHAR (200)   NULL,
    CONSTRAINT [PK_AGFARMEONCOD__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AGCITAPROD_HCVIAADMI] FOREIGN KEY ([CODVIAADM]) REFERENCES [dbo].[HCVIAADMI] ([CODVIAADM]),
    CONSTRAINT [FK_AGCITAPROD_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_AGCITAPROD_INUNIMEDI] FOREIGN KEY ([CODUNIMED]) REFERENCES [dbo].[INUNIMEDI] ([CODUNIMED]),
    CONSTRAINT [FK_AGFARMEONCOD_AGFARMEONCOC] FOREIGN KEY ([IDAGFARMAONCOC]) REFERENCES [dbo].[AGFARMEONCOC] ([ID])
);


GO
ALTER TABLE [dbo].[AGFARMEONCOD] NOCHECK CONSTRAINT [FK_AGCITAPROD_HCVIAADMI];


GO
ALTER TABLE [dbo].[AGFARMEONCOD] NOCHECK CONSTRAINT [FK_AGCITAPROD_IHLISTPRO];


GO
ALTER TABLE [dbo].[AGFARMEONCOD] NOCHECK CONSTRAINT [FK_AGCITAPROD_INUNIMEDI];


GO
ALTER TABLE [dbo].[AGFARMEONCOD] NOCHECK CONSTRAINT [FK_AGFARMEONCOD_AGFARMEONCOC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'DOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de Medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Via Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'CODVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Medicamentos  2 - Medicamento Como Insumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Producto:  1. Pendiente  2. Entregado  3. Anulado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Pendiente de Entregar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'CANPENPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Entregada del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'CANENTPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Solicitada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID relacion tabla cabecera de Solicitudes medicamento/insumos oncología', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'IDAGFARMAONCOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'ID';

