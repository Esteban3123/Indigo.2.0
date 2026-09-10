CREATE TABLE [MixingStation].[ContractExternalClients] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                 VARCHAR (20)  NOT NULL,
    [ContractType]         TINYINT       NOT NULL,
    [DocumentDate]         DATETIME      NOT NULL,
    [InitialDate]          DATETIME      NOT NULL,
    [EndDate]              DATETIME      NOT NULL,
    [ContractObject]       VARCHAR (MAX) NULL,
    [ContractNumber]       VARCHAR (20)  NOT NULL,
    [CustomerId]           INT           NOT NULL,
    [SaleMode]             TINYINT       NOT NULL,
    [TechnicalSupervision] BIT           CONSTRAINT [DF__ContractE__Techn__32C6252F] DEFAULT ((0)) NOT NULL,
    [ProductRateId]        INT           NOT NULL,
    [Clauses]              VARCHAR (MAX) NULL,
    [Annexes]              VARCHAR (MAX) NULL,
    [QuoteNumber]          VARCHAR (20)  NULL,
    [QuoteDate]            DATETIME      NULL,
    [ActNumber]            VARCHAR (20)  NULL,
    [ActDate]              DATETIME      NULL,
    [NegotiationType]      VARCHAR (200) NULL,
    [ApprovedBy]           VARCHAR (50)  NULL,
    [Status]               BIT           NOT NULL,
    [CreationUser]         VARCHAR (20)  NOT NULL,
    [CreationDate]         DATETIME      NOT NULL,
    [ModificationUser]     VARCHAR (20)  NULL,
    [ModificationDate]     DATETIME      NULL,
    [TimeStamp]            ROWVERSION    NOT NULL,
    [ManagesMaquila]       BIT           NULL,
    [WarehouseId]          INT           NULL,
    CONSTRAINT [PK_ContractExternalClients] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContractExternalClients_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Common].[Customer] ([Id]),
    CONSTRAINT [FK_ContractExternalClients_ProductRate] FOREIGN KEY ([ProductRateId]) REFERENCES [Inventory].[ProductRate] ([Id]),
    CONSTRAINT [FK_ContractExternalClients_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del almacén de maquila o tercerización asociado al contrato. FK a Inventory.Warehouse. Tipo: INT. Referencia a ubicación física de gestión de productos.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Almacen Maquila', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (1=Sí, 0=No) que señala si el contrato contempla gestión de almacén de maquila o tercerización de inventario. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ManagesMaquila';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Maneja Almacen Maquila', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ManagesMaquila';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ManagesMaquila';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal automática (TIMESTAMP) del evento de creación, modificación o registro del contrato. Registra el instante exacto en SQL Server para auditoría y sincronización.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro del contrato. Tipo: DATETIME. Documenta cambios posteriores a la creación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o cuenta que realizó la última modificación del contrato. Tipo: VARCHAR(20). Trazabilidad de cambios.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación o registro inicial del contrato en el sistema. Tipo: DATETIME.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o cuenta que creó el registro del contrato. Tipo: VARCHAR(20). Trazabilidad de origen.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del contrato: 1=Activo, 0=Inactivo. Tipo: BIT. Indica vigencia operacional del acuerdo.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro 1 - Activo 0- Inactivo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o código del usuario/responsable que aprobó el contrato. Tipo: VARCHAR(50). Autorización de ejecución.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ApprovedBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aprobado por', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ApprovedBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ApprovedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación o tipo de negociación comercial del contrato (ej: directa, licitación, acuerdo marco). Tipo: VARCHAR(200).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'NegotiationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de negociación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'NegotiationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'NegotiationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del acta de suscripción, ejecución o cierre del contrato. Tipo: DATETIME.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ActDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del acta', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ActDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ActDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único o consecutivo del acta de contrato. Tipo: VARCHAR(20). Referencia documental legal.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ActNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de acta', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ActNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ActNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de emisión de la cotización previa que originó el contrato. Tipo: DATETIME.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'QuoteDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha cotización', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'QuoteDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'QuoteDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de la cotización o presupuesto que precede al contrato. Tipo: VARCHAR(20). Trazabilidad comercial.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'QuoteNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de cotización', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'QuoteNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'QuoteNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Documentos, anexos, apéndices o archivos adicionales adjuntos al contrato. Tipo: VARCHAR(MAX). Contenido textual o referencias.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'Annexes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Anexos', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'Annexes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'Annexes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clausulas, términos y condiciones específicas del contrato. Tipo: VARCHAR(MAX). Descripción de obligaciones y derechos.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'Clauses';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clausulas', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'Clauses';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'Clauses';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la plantilla o tarifa de productos aplicable al contrato. FK a Inventory.ProductRate. Tipo: INT. Define precios y condiciones comerciales.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ProductRateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la plantilla de productos', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ProductRateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ProductRateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modalidad o forma de pago del contrato: 1=Crédito, 2=Contado. Tipo: TINYINT. Condición comercial.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'SaleMode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modalidad de venta:  1. Crédito  2. Contado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'SaleMode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'SaleMode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del cliente o tercero externo contratante. FK a Common.Customer. Tipo: INT. Referencia a la contraparte.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'CustomerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del cliente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'CustomerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'CustomerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único, código o referencia legal del contrato. Tipo: VARCHAR(20). Identificador documental principal.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ContractNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número o código del contrato', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ContractNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ContractNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del objeto, alcance, servicios o productos que cubre el contrato. Tipo: VARCHAR(MAX). Propósito del acuerdo.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ContractObject';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Objeto del contrato', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ContractObject';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ContractObject';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vencimiento o terminación vigencia del contrato. Tipo: DATETIME. Límite temporal del acuerdo.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha final del contrato', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio o vigencia del contrato. Tipo: DATETIME. Comienzo del período de ejecución.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha inicial del contrato', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de emisión o suscripción del documento del contrato. Tipo: DATETIME.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha documento', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del contrato: 1=Fijo (tarifa/plazo fijo), 2=Variable (sujeto a cambios). Tipo: TINYINT.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ContractType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de contrato:  1. Fijo  2. Variable', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ContractType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'ContractType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o clave alfanumérica de identificación del contrato. Tipo: VARCHAR(20). Referencia abreviada.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del contrato', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable del registro de contrato. Tipo: INT IDENTITY. Clave primaria.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contratos con clientes externos de la estación de mezcla. Registra la información comercial y legal de cada contrato: tipo, vigencia, objeto, cláusulas, anexos, cotización asociada, acta, modalidad de venta, tarifa de productos y estado del acuerdo con el cliente.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el contrato requiere supervisión técnica. Valor verdadero (1) significa que aplica supervisión técnica; falso (0) que no aplica.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'TechnicalSupervision';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClients', @level2type = N'COLUMN', @level2name = N'TechnicalSupervision';
