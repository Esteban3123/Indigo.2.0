CREATE TABLE [Billing].[ElectronicsProperties] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [EntityId]         INT           NOT NULL,
    [EntityName]       VARCHAR (250) NULL,
    [EntityCode]       VARCHAR (50)  NULL,
    [StatusRIPS]       TINYINT       NOT NULL,
    [CUV]              VARCHAR (MAX) NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [ConditionSalesId] INT           NULL,
    CONSTRAINT [PK_ElectronicsProperties] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Billing_ElectronicsProperties_EntityName_EntityId]
    ON [Billing].[ElectronicsProperties]([EntityName] ASC, [EntityId] ASC)
    INCLUDE([Id], [EntityCode], [CUV], [StatusRIPS]) WITH (FILLFACTOR = 90);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena la condicion de venta de la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsProperties', @level2type = N'COLUMN', @level2name = N'ConditionSalesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsProperties', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsProperties', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsProperties', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsProperties', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Validacion este es dado unicamente si el rips fue procesado correctamente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsProperties', @level2type = N'COLUMN', @level2name = N'CUV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado en el cual se encuentra el RIPS Electronico 
1- Regsitrado
2 - Procesada correctamente   
3 - Procesada Erroneamente(contiene errores) 
4 - Anulado (Registro de servicio anulado) 
99- Excepcion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsProperties', @level2type = N'COLUMN', @level2name = N'StatusRIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del documento proveniente de una integracion externa', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsProperties', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la entidad desde la cual se genero el registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsProperties', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del documento que genero el registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsProperties', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsProperties', @level2type = N'COLUMN', @level2name = N'Id';

