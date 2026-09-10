CREATE TABLE [Treasury].[ConstitutionCashSmaller] (
    [Id]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                  VARCHAR (20)    NOT NULL,
    [DocumentDate]          DATE            NOT NULL,
    [DocumentType]          TINYINT         NOT NULL,
    [CashRegisterSmallerId] INT             NOT NULL,
    [SourceType]            TINYINT         NOT NULL,
    [CashRegisterId]        INT             NULL,
    [EntityBankAccountId]   INT             NULL,
    [Value]                 DECIMAL (18, 2) NOT NULL,
    [OperatingUnitId]       INT             NOT NULL,
    [Status]                TINYINT         NOT NULL,
    [CreationUser]          VARCHAR (20)    NOT NULL,
    [CreationDate]          DATETIME        NOT NULL,
    [ModificationUser]      VARCHAR (20)    NULL,
    [ModificationDate]      DATETIME        NULL,
    [ConfirmationUser]      VARCHAR (20)    NULL,
    [ConfirmationDate]      DATETIME        NULL,
    [AnnulmentUser]         VARCHAR (20)    NULL,
    [AnnulmentDate]         DATETIME        NULL,
    [TimeStamp]             ROWVERSION      NOT NULL,
    CONSTRAINT [PK_ConstitutionCashSmaller__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConstitutionCashSmaller_CashRegisters] FOREIGN KEY ([CashRegisterSmallerId]) REFERENCES [Treasury].[CashRegisters] ([Id]),
    CONSTRAINT [FK_ConstitutionCashSmaller_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_FundCashSmaller_CashReceipts] FOREIGN KEY ([CashRegisterId]) REFERENCES [Treasury].[CashRegisters] ([Id]),
    CONSTRAINT [FK_FundCashSmaller_EntityBankAccounts] FOREIGN KEY ([EntityBankAccountId]) REFERENCES [Treasury].[EntityBankAccounts] ([Id])
);




GO



GO



GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ConstitutionCashSmaller__Code]
    ON [Treasury].[ConstitutionCashSmaller]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) del evento: registro, creación, modificación o anulación del movimiento de caja menor. Captura el instante exacto de cada cambio.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de anulación (DATE) del documento de caja menor. Registra cuándo se anuló el movimiento.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Anulación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la anulación del documento de caja menor. Identificador del operario responsable.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Anulación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de confirmación (DATE) del movimiento de caja menor. Marca el instante de validación del documento.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Confirmación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que confirmó el movimiento de caja menor. Identificador del operario que validó.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Confirmación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación (DATE) del documento. Registra cuándo se realizaron cambios en el movimiento.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que modificó el documento de caja menor. Identificador del operario que editó.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación (DATETIME) del movimiento de caja menor. Registra el instante inicial del documento.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el movimiento de caja menor (VARCHAR 20). Identificador del operario responsable.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del documento de caja menor (TINYINT): 1=Registrado, 2=Confirmado, 3=Anulado. Control de flujo de aprobación.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del documento  1 - Registrado  2 - Confirmado  3 - Anulado', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT FK) de la unidad operativa, centro de atención o sucursal donde ocurrió el movimiento. Referencia a [Common].[OperatingUnit].', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto del movimiento de caja menor (DECIMAL 18,2). Importe en dinero del aumento o disminución registrada.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT FK) de la cuenta bancaria de la entidad usada en pago o transferencia. Referencia a [Treasury].[EntityBankAccounts].', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'EntityBankAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Entidad Bancaria con la cual se realizara el pago', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'EntityBankAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'EntityBankAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT FK) del registro de caja mayor asociado. Referencia a [Treasury].[CashRegisters] para trazabilidad contable.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'CashRegisterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro de caja.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'CashRegisterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'CashRegisterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de fuente del flujo de caja (TINYINT): 1=Caja Mayor, 2=Cuenta Bancaria. Origen del dinero.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'SourceType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Tipo de fuente de entrada o salida del dinero  1 - Caja Mayor  2 - Cuenta Bancaria', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'SourceType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'SourceType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT FK) de la caja menor afectada. Solo listan cajas tipo menor. Referencia a [Treasury].[CashRegisters].', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'CashRegisterSmallerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la caja mejor, Solo se deben listar cajas de tipo menor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'CashRegisterSmallerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'CashRegisterSmallerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de movimiento (TINYINT): 1=Aumento de caja menor, 2=Disminución de caja menor. Naturaleza del documento.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Tipo de documento  1 - Aumento de Caja Menor  2 - Disminucion de Caja Menor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'DocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del documento (DATE). Día en que ocurrió contablemente el movimiento de caja menor.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Documento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del movimiento (VARCHAR 20). Número secuencial o identificador legible del documento.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY). Clave primaria del registro de movimiento de caja menor.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de constituciones de caja menor: documenta cada operación de apertura o dotación de fondos a una caja menor, indicando el monto, la fuente de los recursos (caja principal o cuenta bancaria), la unidad operativa responsable y el estado del proceso (pendiente, confirmado, anulado).', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller';
