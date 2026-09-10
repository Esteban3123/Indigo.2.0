CREATE TABLE [Inventory].[InventoryContract] (
    [Id]                         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                       VARCHAR (20)    NOT NULL,
    [OperatingUnitId]            INT             NOT NULL,
    [ContractTypeId]             INT             NOT NULL,
    [InitialDate]                DATETIME        NOT NULL,
    [EndDate]                    DATETIME        NOT NULL,
    [SupplierId]                 INT             NOT NULL,
    [SupplierDistributionLineId] INT             NOT NULL,
    [ContractNumber]             VARCHAR (20)    NOT NULL,
    [Description]                VARCHAR (300)   NOT NULL,
    [PaymentMethod]              VARCHAR (200)   NOT NULL,
    [DeliveryMethod]             VARCHAR (200)   NULL,
    [DeliveryPlace]              VARCHAR (200)   NULL,
    [SourceOrder]                TINYINT         NOT NULL,
    [PurchaseProcess]            TINYINT         NOT NULL,
    [Exclusivity]                BIT             NOT NULL,
    [OnlyGuarantee]              BIT             NOT NULL,
    [TechnicalSupervicion]       VARCHAR (100)   NULL,
    [SupervisionExecution]       VARCHAR (100)   NULL,
    [Clauses]                    VARCHAR (MAX)   NULL,
    [Attachments]                VARCHAR (MAX)   NULL,
    [Availability]               VARCHAR (200)   NULL,
    [Resolution]                 VARCHAR (200)   NULL,
    [ResolutionDate]             DATE            NULL,
    [QuoteNumber]                VARCHAR (50)    NULL,
    [QuoteDate]                  DATE            NULL,
    [RecordNumber]               VARCHAR (50)    NULL,
    [RecordDate]                 DATE            NULL,
    [NegotiationType]            VARCHAR (200)   NULL,
    [Approved]                   VARCHAR (100)   NULL,
    [Deadline]                   DATE            NULL,
    [ValidityDate]               DATE            NULL,
    [Value]                      DECIMAL (18, 2) NOT NULL,
    [DiscountValue]              DECIMAL (18, 2) NOT NULL,
    [IvaValue]                   DECIMAL (18, 2) NOT NULL,
    [TotalValue]                 DECIMAL (18, 2) NOT NULL,
    [Status]                     TINYINT         NOT NULL,
    [CreationUser]               VARCHAR (20)    NOT NULL,
    [CreationDate]               DATETIME        NOT NULL,
    [ModificationUser]           VARCHAR (20)    NULL,
    [ModificationDate]           DATETIME        NULL,
    [ConfirmationUser]           VARCHAR (20)    NULL,
    [ConfirmationDate]           DATETIME        NULL,
    [AnnulmentUser]              VARCHAR (20)    NULL,
    [AnnulmentDate]              DATETIME        NULL,
    [TimeStamp]                  ROWVERSION      NOT NULL,
    [DocumentDate]               DATETIME        NULL,
    [ManageProducts]             BIT             CONSTRAINT [DF_InventoryContract_ManageProducts] DEFAULT ((1)) NOT NULL,
    [ModificationValue]          DECIMAL (18, 2) CONSTRAINT [DF_InventoryContract_ModificationValue] DEFAULT ((0)) NOT NULL,
    [CurrencyId]                 INT             CONSTRAINT [DF__Inventory__Curre__64C92BC5] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_InventoryContract__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventoryContract_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_InventoryContract_InventoryContractType] FOREIGN KEY ([ContractTypeId]) REFERENCES [Inventory].[InventoryContractType] ([Id]),
    CONSTRAINT [FK_InventoryContract_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_InventoryContract_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_InventoryContract_SuppliersDistributionLines] FOREIGN KEY ([SupplierDistributionLineId]) REFERENCES [Common].[SuppliersDistributionLines] ([Id])
);




GO



GO



GO



GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_InventoryContract__Code]
    ON [Inventory].[InventoryContract]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la moneda del contrato (FK → Common.Currency). Tipo: INT. Búsqueda: divisa, moneda, peso, dólar, euro.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la moneda del contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor decimal de modificaciones contractuales realizadas mediante actos administrativos posteriores. Tipo: DECIMAL(18,2). Búsqueda: adición, modificatoria, acto administrativo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ModificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de las modificaciones realizadas al contrato a través de otro si', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ModificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ModificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: si TRUE, el contrato maneja líneas de productos detallados; si FALSE, es contrato por valor total global. Tipo: BIT. Búsqueda: productos, líneas, detalle.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ManageProducts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si maneja productos o solo es un contrato por un valor total', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ManageProducts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ManageProducts';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del documento fuente para generación de interfaces e intercambios de datos. Tipo: DATETIME. Búsqueda: fecha documento, interfaz, carga.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del documento con el cual se hará interfaces', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo automática (TIMESTAMP) que registra el instante exacto de creación, registro o modificación del contrato. Búsqueda: timestamp, auditoría, versión, cambio.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se ejecutó la anulación o cancelación del contrato. Tipo: DATETIME. Búsqueda: anulación, cancelación, rescisión.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario (cédula/login) que ejecutó la anulación del contrato. Tipo: VARCHAR(20). Búsqueda: usuario anulación, quién anuló.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se confirmó formalmente el contrato en el sistema. Tipo: DATETIME. Búsqueda: confirmación, validación, vigencia.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario (cédula/login) que confirmó el contrato. Tipo: VARCHAR(20). Búsqueda: usuario confirmación, quién confirmó.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación registrada en el contrato. Tipo: DATETIME. Búsqueda: modificación, cambio, actualización.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario (cédula/login) que realizó la última modificación. Tipo: VARCHAR(20). Búsqueda: usuario modificación, quién modificó.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación inicial del registro de contrato en el sistema. Tipo: DATETIME. Búsqueda: creación, fecha inicio, registro.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario (cédula/login) que creó el contrato. Tipo: VARCHAR(20). Búsqueda: usuario creación, quién creó, autor.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado numérico del contrato: 1=Registrado, 2=Confirmado, 3=Anulado, 4=Legalizado. Tipo: TINYINT. Búsqueda: estado, estatus, vigencia.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado:   1. Registrado  2. Confirmado   3. Anulado  4. Legalizado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total contractual calculado como (Value - DiscountValue + IvaValue) para contratos con productos, o valor global si es sin detalle. Tipo: DECIMAL(18,2). Búsqueda: total, monto, valor final.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total del contrato, se obtiene tomando (Value - DiscountValue + IvaValue), o, es el valor del contrato si este no maneja productos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total del IVA/impuesto sumado de todos los detalles de línea del contrato. Tipo: DECIMAL(18,2). Búsqueda: IVA, impuesto, tributario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'IvaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del iva, es la sumatoria de todos los valores del iva de los detalles', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'IvaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'IvaValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor acumulado de descuentos comerciales aplicados a todas las líneas del contrato. Tipo: DECIMAL(18,2). Búsqueda: descuento, rebaja, reducción.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'DiscountValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del descuento del contrato, es la sumatoria de todos los descuento de los detalles', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'DiscountValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'DiscountValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor base o monto sumado de todos los detalles de línea del contrato sin impuestos ni descuentos. Tipo: DECIMAL(18,2). Búsqueda: valor base, monto, precio.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de el contrato, es la sumatoria de todos los valores de los detalles', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vigencia o entrada en vigor del contrato. Tipo: DATE. Búsqueda: vigencia, efectividad, inicio contrato.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ValidityDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha de vigencia', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ValidityDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ValidityDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha límite o plazo máximo de cumplimiento del contrato. Tipo: DATE. Búsqueda: fecha límite, plazo, vencimiento.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Deadline';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha limite', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Deadline';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Deadline';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del aprobador (usuario, cargo o nombre) autorizado para validar el contrato. Tipo: VARCHAR(100). Búsqueda: aprobado por, autorización.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Approved';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica por quien fue aprobado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Approved';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Approved';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo o modalidad de negociación empleada en la contratación (ej: privada, pública, mixta). Tipo: VARCHAR(200). Búsqueda: negociación, modalidad contratación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'NegotiationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de negociacion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'NegotiationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'NegotiationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del acta o documento de registro formal del contrato. Tipo: DATE. Búsqueda: acta, fecha acta, documento formal.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'RecordDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de acta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'RecordDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'RecordDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o identificador del acta de registro contractual. Tipo: VARCHAR(50). Búsqueda: acta, número acta, expediente.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'RecordNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de acta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'RecordNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'RecordNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de emisión de la cotización o presupuesto que originó el contrato. Tipo: DATE. Búsqueda: cotización, presupuesto, oferta.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'QuoteDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la cotizacion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'QuoteDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'QuoteDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de referencia de la cotización o presupuesto comercial. Tipo: VARCHAR(50). Búsqueda: cotización, presupuesto, oferta.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'QuoteNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de cotización', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'QuoteNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'QuoteNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de emisión de la resolución administrativa que ampara o aprueba el contrato. Tipo: DATE. Búsqueda: resolución, decreto, acto administrativo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ResolutionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha resolución', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ResolutionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ResolutionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número, referencia o contenido de la resolución administrativa de aprobación contractual. Tipo: VARCHAR(200). Búsqueda: resolución, decreto, expediente.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Resolution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resolucion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Resolution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Resolution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado o condición de disponibilidad de recursos, productos o servicios bajo el contrato. Tipo: VARCHAR(200). Búsqueda: disponibilidad, stock, servicio activo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Availability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Disponibilidad del contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Availability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Availability';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de texto largo (MAX) con rutas, referencias o contenido de anexos documentales adjuntos. Tipo: VARCHAR(MAX). Búsqueda: anexos, documentos, archivos adjuntos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Attachments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Anexos del contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Attachments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Attachments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de texto largo (MAX) con las cláusulas, términos y condiciones especiales del contrato. Tipo: VARCHAR(MAX). Búsqueda: cláusulas, términos, condiciones.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Clauses';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clausulas del contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Clauses';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Clauses';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o cargo del supervisor responsable del seguimiento ejecutivo del contrato. Tipo: VARCHAR(100). Búsqueda: supervisión, ejecución, seguimiento.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'SupervisionExecution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Supervicion de la ejecución', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'SupervisionExecution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'SupervisionExecution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o cargo del supervisor técnico responsable de la calidad y cumplimiento técnico. Tipo: VARCHAR(100). Búsqueda: supervisión técnica, calidad, revisión.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'TechnicalSupervicion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Supervicion Técnica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'TechnicalSupervicion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'TechnicalSupervicion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: si TRUE, el contrato cubre únicamente cobertura de garantía del proveedor. Tipo: BIT. Búsqueda: garantía, cobertura, póliza.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'OnlyGuarantee';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el contrato maneja garantia unica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'OnlyGuarantee';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'OnlyGuarantee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: si TRUE, el contrato otorga exclusividad de suministro al proveedor. Tipo: BIT. Búsqueda: exclusividad, monopolio, proveedor único.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Exclusivity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el contrato maneja exclusividad ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Exclusivity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Exclusivity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de proceso de compras: 1=Compra directa, 2=Negociación conjunta. Tipo: TINYINT. Búsqueda: proceso compras, modalidad adquisición.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'PurchaseProcess';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el proceso de compras  1 - Directa  2 - Negociacion Conjunta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'PurchaseProcess';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'PurchaseProcess';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen por cuantía: 1=Orden simple, 2=Llamado a oferta, 3=Licitación pública. Tipo: TINYINT. Búsqueda: cuantía, licitación, modalidad compra.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'SourceOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el origen segun de la Cuantia  1 - Orden Simple  2 - Llamado Oferta  3 - Licitacion Publica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'SourceOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'SourceOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección, ubicación o lugar físico de entrega de productos o servicios contratados. Tipo: VARCHAR(200). Búsqueda: entrega, dirección, ubicación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'DeliveryPlace';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lugar de entrega de los pedidos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'DeliveryPlace';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'DeliveryPlace';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Método, modalidad o forma de entrega (ej: transporte, digital, presencial). Tipo: VARCHAR(200). Búsqueda: método entrega, logística, transporte.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'DeliveryMethod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Método de entrega', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'DeliveryMethod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'DeliveryMethod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Forma, modalidad o medio de pago acordado (ej: transferencia, cheque, efectivo). Tipo: VARCHAR(200). Búsqueda: forma pago, pago, tesorería.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'PaymentMethod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'forma de pago del contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'PaymentMethod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'PaymentMethod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual general del objeto, alcance y características principales del contrato. Tipo: VARCHAR(300). Búsqueda: descripción, resumen, objeto contrato.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único o identificador oficial asignado al contrato por la institución. Tipo: VARCHAR(20). Búsqueda: número contrato, referencia, ID contrato.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ContractNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el numero del contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ContractNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ContractNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la línea de distribución del proveedor (FK → Common.SuppliersDistributionLines). Tipo: INT. Búsqueda: línea distribución, canal proveedor.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'SupplierDistributionLineId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la linea de distribucion del proveedor', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'SupplierDistributionLineId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'SupplierDistributionLineId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del proveedor o suplidor con el que se establece el contrato (FK → Common.Supplier). Tipo: INT. Búsqueda: proveedor, suplidor, contratista.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'SupplierId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del proveedor con quien se establecio el contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'SupplierId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'SupplierId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final o de vencimiento del período de vigencia contractual. Tipo: DATETIME. Búsqueda: fecha fin, vencimiento, término.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha final del contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial o de inicio de vigencia del contrato. Tipo: DATETIME. Búsqueda: fecha inicio, vigencia, comienzo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha inicial del contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de contrato de inventarios (FK → Inventory.InventoryContractType). Tipo: INT. Búsqueda: tipo contrato, clasificación contrato.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ContractTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de contrato de inventarios', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ContractTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ContractTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad operativa o centro de atención responsable (FK → Common.OperatingUnit). Tipo: INT. Búsqueda: unidad operativa, centro, sucursal.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico único o identificador interno del contrato para clasificación. Tipo: VARCHAR(20). Búsqueda: código, clave, identificador.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico único (clave primaria) del registro de contrato de inventarios. Tipo: INT IDENTITY. Búsqueda: ID, clave primaria, identificador único.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del contrato de inventarios', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contratos de inventario con proveedores para la adquisición de insumos y medicamentos. Registra las condiciones comerciales, vigencias, valores, métodos de pago y entrega pactados con cada proveedor por unidad operativa.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract';
