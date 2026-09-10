CREATE TABLE [Common].[Customer] (
    [Id]                                         INT                                                                         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Nit]                                        VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Nit_Ofuscado", 0)')       NOT NULL,
    [Name]                                       VARCHAR (100) MASKED WITH (FUNCTION = 'partial(0, "Customer_Ofuscado", 0)') NOT NULL,
    [EPSCode]                                    VARCHAR (20)                                                                NULL,
    [ThirdPartyId]                               INT                                                                         CONSTRAINT [DF_Customer_ThirdPartyId] DEFAULT ((4)) NOT NULL,
    [MainAccountReceivableId]                    INT                                                                         NOT NULL,
    [Term]                                       INT                                                                         NOT NULL,
    [State]                                      BIT                                                                         NOT NULL,
    [CreationUser]                               VARCHAR (20)                                                                NOT NULL,
    [CreationDate]                               DATETIME                                                                    NOT NULL,
    [ModificationUser]                           VARCHAR (20)                                                                NULL,
    [ModificationDate]                           DATETIME                                                                    NULL,
    [TimeStamp]                                  ROWVERSION                                                                  NOT NULL,
    [BasicBillingDeteriorationClassificationId]  INT                                                                         NULL,
    [HealthInvoiceDeteriorationClassificationId] INT                                                                         NULL,
    CONSTRAINT [PK_Customer__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Customer_BasicBillingDeterioration] FOREIGN KEY ([BasicBillingDeteriorationClassificationId]) REFERENCES [Portfolio].[PortfolioDeteriorationClassification] ([Id]),
    CONSTRAINT [FK_Customer_HealthInvoiceDeterioration] FOREIGN KEY ([HealthInvoiceDeteriorationClassificationId]) REFERENCES [Portfolio].[PortfolioDeteriorationClassification] ([Id]),
    CONSTRAINT [FK_Customer_MainAccounts] FOREIGN KEY ([MainAccountReceivableId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_Customer_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [UQ_Customer__Nit] UNIQUE NONCLUSTERED ([Nit] ASC)
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Common].[Customer].[Nit]
    WITH (LABEL = 'Confidential - Financial', INFORMATION_TYPE = 'Financial');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Common].[Customer].[Name]
    WITH (LABEL = 'Confidential', INFORMATION_TYPE = 'Name');



GO
CREATE NONCLUSTERED INDEX [IX_Customer_ThirdPartyId]
    ON [Common].[Customer]([ThirdPartyId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal interna (TIMESTAMP) generada automáticamente en creación, registro o modificación del tercero/cliente. Controla versioning y concurrencia en replicación SQL Server.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del tercero/cliente (DATETIME). Actualización más reciente del registro. Opcional.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o cuenta que realizó la última modificación del registro (VARCHAR 20). Trazabilidad de auditoría. Opcional.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro del tercero/cliente (DATETIME). Marca temporal de registro inicial. Requerido.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o cuenta que creó el registro del tercero/cliente (VARCHAR 20). Trazabilidad de auditoría. Requerido.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del tercero/cliente: 1=Activo, 0=Inactivo (BIT). Indica si el cliente está habilitado para transacciones. Requerido.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del concepto general  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plazo o período de crédito concedido al cliente en días (INT). Define el número de días para pago de facturas, ingreso, atención o servicios. Requerido.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Term';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plazo de la cuenta (En dias)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Term';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Term';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta por cobrar principal del cliente (INT FK → GeneralLedger.MainAccounts). Referencia contable para registrar documentos de cuentas por cobrar. Requerido.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'MainAccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta por cobrar del cliente, este campo se usa cuando se hace un documento de Cuenta por Cobrar', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'MainAccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'MainAccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo o rol del tercero (INT FK → Common.ThirdParty). Define si es proveedor, cliente, asegurador, etc. Por defecto: 4. Requerido.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Tercero', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Entidad Promotora de Salud (EPS) a la que pertenece o está afiliado el tercero/cliente. Referencia para cobro en salud. Opcional (VARCHAR 20).', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'EPSCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la eps', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'EPSCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'EPSCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o razón social del tercero/cliente (VARCHAR 100, PII - Customer_Ofuscado). Denominación comercial, legal o personal del tercero. Requerido.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del tercero', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de Identificación Tributaria (VARCHAR 25, PII - Identificación_Ofuscado). Documento único del tercero/cliente, equivalente a RUT, cédula o número de identificación fiscal. Único y requerido.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Nit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nit del tercero.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Nit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Nit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT) del tercero/cliente en el sistema. Clave primaria de la tabla Customer.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de terceros', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de riesgo o deterioro de cartera para facturas de salud/RIPS (INT FK → Portfolio.PortfolioDeteriorationClassification). Categoría de morosidad/glosa en facturación de servicios sanitarios. Opcional.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'HealthInvoiceDeteriorationClassificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificación de deterioro para la Factura Salud.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'HealthInvoiceDeteriorationClassificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'HealthInvoiceDeteriorationClassificationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de riesgo o deterioro de cartera para facturas básicas (INT FK → Portfolio.PortfolioDeteriorationClassification). Categoría de morosidad/glosa en facturación comercial. Opcional.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'BasicBillingDeteriorationClassificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificación de deterioro para la Factura Básica.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'BasicBillingDeteriorationClassificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'BasicBillingDeteriorationClassificationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clientes o entidades pagadoras registradas en el sistema: EPS, aseguradoras, empresas y demás terceros a quienes se les factura. Incluye datos de identificación, clasificación contable y condiciones de cartera.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer';
