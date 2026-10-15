CREATE TABLE [Billing].[MipresCode] (
    [Id]                   INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ServiceOrderDetailId] INT          NOT NULL,
    [Code]                 VARCHAR (20) NOT NULL,
    [CreationUser]         VARCHAR (20) CONSTRAINT [DF_MipresCode_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]         DATETIME     CONSTRAINT [DF_MipresCode_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [IdMipres]             VARCHAR (20) NULL,
    CONSTRAINT [PK_MipresCode] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MipresCode_ServiceOrderDetail] FOREIGN KEY ([ServiceOrderDetailId]) REFERENCES [Billing].[ServiceOrderDetail] ([Id])
);

GO
-- Índice optimizado para consultas que requieren ServiceOrderDetailId, Code e IdMipres
CREATE UNIQUE NONCLUSTERED INDEX [IX_MipresCode] 
ON [Billing].[MipresCode] ([ServiceOrderDetailId] ASC, [Code] ASC)
INCLUDE ([IdMipres]);


GO
ALTER TABLE [Billing].[MipresCode] NOCHECK CONSTRAINT [FK_MipresCode_ServiceOrderDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el Id del mipres', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'MipresCode', @level2type = N'COLUMN', @level2name = N'IdMipres';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'MipresCode', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'MipresCode', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código Mipres', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'MipresCode', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la órden de servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'MipresCode', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro del código Mipres.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'MipresCode', @level2type = N'COLUMN', @level2name = N'Id';

