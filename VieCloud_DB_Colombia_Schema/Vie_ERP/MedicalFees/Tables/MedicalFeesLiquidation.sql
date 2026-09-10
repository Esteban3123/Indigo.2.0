CREATE TABLE [MedicalFees].[MedicalFeesLiquidation] (
    [Id]                          INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                        VARCHAR (20) NOT NULL,
    [OperatingUnitId]             INT          NOT NULL,
    [LiquidationType]             TINYINT      NOT NULL,
    [MedicalFeesContractId]       INT          NULL,
    [HealthProfessionalCode]      CHAR (20)    NULL,
    [SupplierId]                  INT          NOT NULL,
    [SuppliersDistributionLineId] INT          NOT NULL,
    [CostCenterId]                INT          NULL,
    [InitialDate]                 DATETIME     NOT NULL,
    [EndDate]                     DATETIME     NOT NULL,
    [BillNumber]                  VARCHAR (20) NOT NULL,
    [DocumentDate]                DATETIME     NOT NULL,
    [FilingUnitId]                INT          NOT NULL,
    [SupplierTypeId]              INT          NOT NULL,
    [AccountPayableId]            INT          NULL,
    [Status]                      TINYINT      NOT NULL,
    [CreationUser]                VARCHAR (20) NOT NULL,
    [CreationDate]                DATETIME     NOT NULL,
    [ModificationUser]            VARCHAR (20) NULL,
    [ModificationDate]            DATETIME     NULL,
    [ConfirmationUser]            VARCHAR (20) NULL,
    [ConfirmationDate]            DATETIME     NULL,
    [AnnulmentUser]               VARCHAR (20) NULL,
    [AnnulmentDate]               DATETIME     NULL,
    [TimeStamp]                   ROWVERSION   NOT NULL,
    CONSTRAINT [PK_MedicalFeesLiquidation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MedicalFeesLiquidation_AccountPayable] FOREIGN KEY ([AccountPayableId]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_MedicalFeesLiquidation_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_MedicalFeesLiquidation_FilingUnit] FOREIGN KEY ([FilingUnitId]) REFERENCES [Payments].[FilingUnit] ([Id]),
    CONSTRAINT [FK_MedicalFeesLiquidation_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_MedicalFeesLiquidation_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_MedicalFeesLiquidation_SuppliersDistributionLines] FOREIGN KEY ([SuppliersDistributionLineId]) REFERENCES [Common].[SuppliersDistributionLines] ([Id]),
    CONSTRAINT [FK_MedicalFeesLiquidation_SupplierType] FOREIGN KEY ([SupplierTypeId]) REFERENCES [Common].[SupplierType] ([Id])
);




GO



GO



GO



GO





GO



GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal TIMESTAMP (auditoria) que registra el instante exacto de creación, modificación o cambio de estado de la liquidación de honorarios médicos en la base de datos.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se anuló o canceló la liquidación de honorarios médicos, dejando sin efecto la cuenta por pagar generada.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del usuario (VARCHAR 20) que ejecutó la anulación de la liquidación de honorarios médicos, registrado en el sistema.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se confirmó o validó la liquidación de honorarios médicos, pasando al estado confirmado.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del usuario (VARCHAR 20) responsable de confirmar y autorizar la liquidación de honorarios médicos.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) del último cambio o actualización realizado a la liquidación de honorarios médicos.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del usuario (VARCHAR 20) que efectuó la última modificación en los datos de la liquidación de honorarios médicos.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se creó o registró inicialmente la liquidación de honorarios médicos en el sistema.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del usuario (VARCHAR 20) que creó e ingresó la liquidación de honorarios médicos al sistema.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual (TINYINT) de la liquidación: 1=Registrado, 2=Confirmado, 3=Anulado. Controla el flujo de la liquidación de honorarios médicos.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro  1 - Registrado  2 - Confirmado  3 - Anulado', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la cuenta por pagar (Payments.AccountPayable) generada al liquidar los honorarios médicos del proveedor o profesional.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'AccountPayableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id de la cuenta por pagar que se genera al liquidar', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'AccountPayableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'AccountPayableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del tipo de proveedor (Common.SupplierType) en último nivel jerárquico; define si es agremiación, médico independiente, clínica u otro.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'SupplierTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de proveedor, este se obtiene de la tabla SupplierDetailType pero en esta tabla solo estan los padres y en este campos solo se pueden seleccionar los hijos de ultimo nivel de esos registros, es decir que el tipo de proveedor que seleccionen debe estar en el ultimo nivel en la jerarquia', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'SupplierTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'SupplierTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la unidad de radicación (Payments.FilingUnit) donde se registra y tramita la liquidación de honorarios médicos.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'FilingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de radicacion', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'FilingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'FilingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) del documento o factura que genera la cuenta por pagar en la liquidación de honorarios médicos.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de factura o comprobante (VARCHAR 20) emitido por el proveedor, referencia del documento que se liquida.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'BillNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Numero de la factura', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'BillNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'BillNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final (DATETIME) del período o rango de atención/servicios que se incluyen en la liquidación de honorarios médicos.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha final del rango que se va a liquidar', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial (DATETIME) del período o rango de atención/servicios que se incluyen en la liquidación de honorarios médicos.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial que se va a liquidar', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del centro de costo (Payroll.CostCenter) asociado, solo se completa si la línea de distribución requiere imputación por centro.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del centro de costo, solo se llena si la cuenta contable de la linea de distribucion maneja centor de costo', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la línea de distribución del proveedor (Common.SuppliersDistributionLines) que define la imputación contable y financiera.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'SuppliersDistributionLineId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la linea de distribucion del proveedor', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'SuppliersDistributionLineId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'SuppliersDistributionLineId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del proveedor o profesional de salud (Common.Supplier) al que se genera la cuenta por pagar de honorarios médicos.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'SupplierId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del proveedor al que se le va generar la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'SupplierId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'SupplierId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud o médico (CHAR 20), obtenido de INPROFSAL; obligatorio solo si la liquidación es de tipo ''''Médicos''''.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'HealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Codigo del Profesional   Estos datos se sacan de la tabla de Crystal INPROFSAL    Este campos solo se pedira si el tipo de liquidacion es de Medico', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'HealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'HealthProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del contrato de honorarios médicos que se liquida cuando el tipo de liquidación es ''''Agremiación''''.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'MedicalFeesContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del contrato que se va a liquidar cuando sea de tipo agremiacion', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'MedicalFeesContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'MedicalFeesContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de liquidación de honorarios médicos (TINYINT): 1=Agremiación, 2=Médicos. Define si es liquidación de agremiación o de médicos independientes.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'LiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de la liquidacion de honorarios medicos  1 - Agremiacion  2 - Medicos', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'LiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'LiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la unidad operativa o centro de atención (Common.OperatingUnit) donde se originan los servicios liquidados.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico único (VARCHAR 20) que identifica la liquidación de honorarios médicos dentro del sistema.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la liquidacion de honorarios medicos', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) y clave primaria de la liquidación de honorarios médicos, generado automáticamente por el sistema.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la liquidacion de honorarios medicos', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Liquidaciones de honorarios médicos. Registra cada liquidación de pagos a profesionales de la salud o proveedores por servicios prestados, incluyendo el período liquidado, el número de factura, el tipo de liquidación, el estado del proceso y la trazabilidad de auditoría (creación, modificación, confirmación y anulación).', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesLiquidation';
