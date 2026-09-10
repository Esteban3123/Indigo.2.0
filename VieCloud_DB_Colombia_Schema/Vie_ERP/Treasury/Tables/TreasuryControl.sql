CREATE TABLE [Treasury].[TreasuryControl] (
    [Id]             INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DocumentNumber] VARCHAR (100) NOT NULL,
    [DocumentType]   INT           NOT NULL,
    [DocumentUser]   VARCHAR (50)  NOT NULL,
    [DocumentDate]   DATETIME      NOT NULL,
    [Timestamp]      ROWVERSION    NOT NULL,
    CONSTRAINT [PK_TreasuryControl__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_TreasuryControl__DocumentNumber__DocumentType] UNIQUE NONCLUSTERED ([DocumentNumber] ASC, [DocumentType] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) del evento de tesorería: instante exacto de creación, registro o modificación del documento de control. Auditoría de fecha-hora del documento.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryControl', @level2type = N'COLUMN', @level2name = N'Timestamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryControl', @level2type = N'COLUMN', @level2name = N'Timestamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryControl', @level2type = N'COLUMN', @level2name = N'Timestamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación del documento de tesorería (DATETIME). Fecha del comprobante, recibo, egreso o movimiento de caja registrado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryControl', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se creo el documento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryControl', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryControl', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó o registró el documento de tesorería (VARCHAR 50). Identificación del operador, cajero o administrativo responsable del movimiento.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryControl', @level2type = N'COLUMN', @level2name = N'DocumentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que creo el documento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryControl', @level2type = N'COLUMN', @level2name = N'DocumentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryControl', @level2type = N'COLUMN', @level2name = N'DocumentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento de tesorería (INT): 1=Recibo de caja, 2=Comprobante de egreso, 3=Notas, 4=Consignaciones, 5=Reembolso, 6=Cruce CXC-CXP, 7=Dispersión de fondos/Programación de pagos. Clasificación del movimiento financiero.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryControl', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo del documento   Recibo de caja = 1  comprobante de egreso =2  Notas = 3  Consignaciones = 4  Reembolso =5  CruceCXC-CXP = 6  Dispercion de fondos y Programacion de pagos =7', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryControl', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryControl', @level2type = N'COLUMN', @level2name = N'DocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o consecutivo único del documento de tesorería (VARCHAR 100). Identificador secuencial del comprobante, recibo o egreso. Clave única con DocumentType.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryControl', @level2type = N'COLUMN', @level2name = N'DocumentNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'consecutivo del documento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryControl', @level2type = N'COLUMN', @level2name = N'DocumentNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryControl', @level2type = N'COLUMN', @level2name = N'DocumentNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (INT IDENTITY) de la tabla TreasuryControl. Clave primaria única del registro de control de tesorería.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryControl', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryControl', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryControl', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de control de documentos del módulo de tesorería: guarda cada documento financiero procesado (recibos, pagos, comprobantes) con su número, tipo, usuario responsable y fecha de emisión. Permite auditar y rastrear movimientos de caja y tesorería.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryControl';
