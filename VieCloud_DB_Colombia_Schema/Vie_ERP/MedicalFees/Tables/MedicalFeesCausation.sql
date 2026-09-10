CREATE TABLE [MedicalFees].[MedicalFeesCausation] (
    [Id]                           INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AdmissionNumber]              CHAR (10)                                                                        NOT NULL,
    [PatientCode]                  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [HealthProfessionalCode]       CHAR (20)                                                                        NOT NULL,
    [ThirdPartyId]                 INT                                                                              NOT NULL,
    [MedicalFeesContractId]        INT                                                                              NULL,
    [CausationDate]                DATETIME                                                                         CONSTRAINT [DF_MedicalFeesCausation_CausationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [InvoiceDetailId]              INT                                                                              NOT NULL,
    [ServiceOrderId]               INT                                                                              NOT NULL,
    [ServiceOrderDetailId]         INT                                                                              NOT NULL,
    [ServiceOrderDetailSurgicalId] INT                                                                              NULL,
    [AmountPayable]                NUMERIC (18)                                                                     NOT NULL,
    [MedicalFeesContractValue]     NUMERIC (18)                                                                     NULL,
    [InvoiceQuantity]              INT                                                                              NOT NULL,
    [TotalAmountPayable]           NUMERIC (18)                                                                     NOT NULL,
    [TotalAmountPayableReal]       NUMERIC (18)                                                                     NOT NULL,
    [PercentageCashed]             NUMERIC (5, 2)                                                                   NOT NULL,
    [MedicalFeePaid]               BIT                                                                              NOT NULL,
    [Status]                       TINYINT                                                                          CONSTRAINT [DF_MedicalFeesCausation_Status] DEFAULT ((1)) NOT NULL,
    [InvoiceReversal]              BIT                                                                              CONSTRAINT [DF_MedicalFeesCausation_InvoiceReversal] DEFAULT ((0)) NOT NULL,
    [ObjectionAccepted]            BIT                                                                              CONSTRAINT [DF_MedicalFeesCausation_ObjectionAccepted] DEFAULT ((0)) NOT NULL,
    [ValueObjectionAccepted]       DECIMAL (18)                                                                     CONSTRAINT [DF_MedicalFeesCausation_ValueObjectionAccepted] DEFAULT ((0)) NOT NULL,
    [ReassessmentForReversal]      BIT                                                                              CONSTRAINT [DF_MedicalFeesCausation_Reassessment] DEFAULT ((0)) NOT NULL,
    [ReassessmentForObjection]     BIT                                                                              CONSTRAINT [DF_MedicalFeesCausation_ReassessmentForReversal1] DEFAULT ((0)) NOT NULL,
    [CreationUser]                 VARCHAR (20)                                                                     NOT NULL,
    [CreationDate]                 DATETIME                                                                         NOT NULL,
    [ModificationUser]             VARCHAR (20)                                                                     NULL,
    [LiquidationUser]              VARCHAR (20)                                                                     NULL,
    [LiquidationDate]              VARCHAR (20)                                                                     NULL,
    [ModificationDate]             DATETIME                                                                         NULL,
    [ConfirmationUser]             VARCHAR (20)                                                                     NULL,
    [ConfirmationDate]             DATETIME                                                                         NULL,
    [AnnulmentUser]                VARCHAR (20)                                                                     NULL,
    [AnnulmentDate]                DATETIME                                                                         NULL,
    [TimeStamp]                    ROWVERSION                                                                       NOT NULL,
    [CausationRecognitionId]       INT                                                                              NULL,
    CONSTRAINT [PK_MedicalFeesCausation__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MedicalFeesCausation_CausationRecognition] FOREIGN KEY ([CausationRecognitionId]) REFERENCES [MedicalFees].[CausationRecognition] ([Id]),
    CONSTRAINT [FK_MedicalFeesCausation_InvoiceDetail] FOREIGN KEY ([InvoiceDetailId]) REFERENCES [Billing].[InvoiceDetail] ([Id]),
    CONSTRAINT [FK_MedicalFeesCausation_MedicalFeesContract] FOREIGN KEY ([MedicalFeesContractId]) REFERENCES [MedicalFees].[MedicalFeesContract] ([Id]),
    CONSTRAINT [FK_MedicalFeesCausation_ServiceOrder] FOREIGN KEY ([ServiceOrderId]) REFERENCES [Billing].[ServiceOrder] ([Id]),
    CONSTRAINT [FK_MedicalFeesCausation_ServiceOrderDetail] FOREIGN KEY ([ServiceOrderDetailId]) REFERENCES [Billing].[ServiceOrderDetail] ([Id]),
    CONSTRAINT [FK_MedicalFeesCausation_ServiceOrderDetailSurgical] FOREIGN KEY ([ServiceOrderDetailSurgicalId]) REFERENCES [Billing].[ServiceOrderDetailSurgical] ([Id]),
    CONSTRAINT [FK_MedicalFeesCausation_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [MedicalFees].[MedicalFeesCausation].[PatientCode]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');



GO
CREATE NONCLUSTERED INDEX [IX_MedicalFeesCausation__ServiceOrderDetailId__INC__AdmissionNumber__Id__InvoiceQuantity__PatientCode__TotalAmountPayable]
    ON [MedicalFees].[MedicalFeesCausation]([ServiceOrderDetailId] ASC)
    INCLUDE([AdmissionNumber], [Id], [InvoiceQuantity], [PatientCode], [TotalAmountPayable]);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_MedicalFeesCausation]
    ON [MedicalFees].[MedicalFeesCausation]([InvoiceDetailId] ASC, [ServiceOrderId] ASC, [ServiceOrderDetailId] ASC, [ServiceOrderDetailSurgicalId] ASC, [Status] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_MedicalFeesCausation__ServiceOrderDetailSurgicalId__ServiceOrderDetailId__MedicalFeesContractId__InvoiceDetailId__INC__Amount]
    ON [MedicalFees].[MedicalFeesCausation]([ServiceOrderDetailSurgicalId] ASC, [ServiceOrderDetailId] ASC, [MedicalFeesContractId] ASC, [InvoiceDetailId] ASC)
    INCLUDE([Id], [AmountPayable], [TotalAmountPayable], [TotalAmountPayableReal], [PercentageCashed], [Status], [CreationUser], [CreationDate], [ModificationUser], [ModificationDate], [ConfirmationUser], [ConfirmationDate]);


GO
ALTER INDEX [IX_MedicalFeesCausation__ServiceOrderDetailSurgicalId__ServiceOrderDetailId__MedicalFeesContractId__InvoiceDetailId__INC__Amount]
    ON [MedicalFees].[MedicalFeesCausation] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_MedicalFeesCausation__InvoiceDetailId__ServiceOrderDetailId__INC__AmountPayable__CausationDate__ConfirmationDate__Confirmatio]
    ON [MedicalFees].[MedicalFeesCausation]([InvoiceDetailId] ASC, [ServiceOrderDetailId] ASC)
    INCLUDE([Id], [MedicalFeesContractId], [CausationDate], [AmountPayable], [TotalAmountPayable], [TotalAmountPayableReal], [PercentageCashed], [Status], [CreationUser], [CreationDate], [ModificationUser], [ModificationDate], [ConfirmationUser], [ConfirmationDate]);


GO
ALTER INDEX [IX_MedicalFeesCausation__InvoiceDetailId__ServiceOrderDetailId__INC__AmountPayable__CausationDate__ConfirmationDate__Confirmatio]
    ON [MedicalFees].[MedicalFeesCausation] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_MedicalFeesCausation__InvoiceDetailId]
    ON [MedicalFees].[MedicalFeesCausation]([InvoiceDetailId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_MedicalFeesCausation__CausationRecognitionId]
    ON [MedicalFees].[MedicalFeesCausation]([CausationRecognitionId] ASC)
    WHERE [CausationRecognitionId] IS NOT NULL;


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal TIMESTAMP (SQL Server), registra automáticamente el instante de creación, modificación o cambio de estado de la causación (auditoría interna).', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATETIME de anulación de la causación; se registra cuando el usuario ejecuta la acción de anular el registro.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario VARCHAR(20) que ejecutó la anulación de la causación de honorarios.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATETIME de confirmación; marca cuándo se genera la cuenta por pagar asociada a la causación.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario VARCHAR(20) que confirmó la causación y generó la obligación contable (cuenta por pagar).', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATETIME de última modificación del registro de causación.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha VARCHAR(20) de liquidación; indica cuándo la causación fue incluida en un proceso de liquidación de honorarios.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'LiquidationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de liquidación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'LiquidationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'LiquidationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario VARCHAR(20) que realizó la liquidación e incluyó esta causación en el documento de pago.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'LiquidationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de liquidación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'LiquidationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'LiquidationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario VARCHAR(20) que realizó la última modificación al registro.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATETIME de creación del registro de causación (coincide con CausationDate).', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario VARCHAR(20) que creó el registro de causación.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT que indica si el registro fue reliquidado y descontado en una nueva liquidación por objeción/glosa aceptada.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ReassessmentForObjection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece si el registro ya fue descontado en una nueva liquidacion por objecion de glosa aceptada.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ReassessmentForObjection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ReassessmentForObjection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT que indica si el registro fue reliquidado e incluido en nueva liquidación por anulación de factura.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ReassessmentForReversal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece si el registro ya fue reliquidado e incluido en una nueva liquidacion, por anulacion de factura', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ReassessmentForReversal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ReassessmentForReversal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor DECIMAL(18) que la IPS acepta descontar al profesional de la salud por concepto de glosa aceptada.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ValueObjectionAccepted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor aceptado por la IPS que se descontara al profesional de la salud', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ValueObjectionAccepted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ValueObjectionAccepted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT que confirma si la IPS aceptó la objeción/glosa en primera, segunda evaluación o conciliación; activa descuento automático en próxima liquidación.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ObjectionAccepted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el item causado fue aceptado por la IPS en el proceso de glosas, bien sea en la evaluacion de primera o segunda vez, o en conciliacion.    Una vez se establezca el valor aceptado por la IPS el sistema verificará el parametro de Descuento Automatico por Aceptacion de Glosas del contrato relacionado al profesional responsable de la realizacion del procedimiento, y marcara este campo en verdadero para que se agregue como descuento en la siguiente liquidacion.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ObjectionAccepted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ObjectionAccepted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT que identifica si la factura que contenía este servicio causado fue anulada (solo se activa si el estado era Liquidado o Confirmado).', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'InvoiceReversal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica si la factura donde se encontraba el servicio causado fue anulada, esta bandera solo se activa si al momento de la anulacion de la factura el estado del registro es Liquidado o Confirmado', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'InvoiceReversal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'InvoiceReversal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT que especifica estado de la causación: 1=Causado, 2=Liquidado, 3=Confirmado, 4=Anulado; governa transiciones de ciclo de vida.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el estado de la causacion  1 - Causado  2 - Liquidado  3 - Confirmado  4 - Anulado    Causado: Es el primer estado, es cuando se realiza la causacion del servicio    Liquidado: Se establece cuando el registro de causacion es incluida en una  liquidacion sin confirmar    Confirmado: Se establece cuando se genera la cuenta por pagar    Anulado: Se establece en varios eventos, primero cuando el registro se encuentra en estado causado y se realiza la anulacion de una factura, segundo cuando el registro se encuentra en estado liquidado y la columna InvoiceReversal se encuentra en estado Verdadero y la accion del usuario corresponde a Recalcular', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera BIT que indica si el honorario médico asociado a esta causación ya fue pagado/abonado.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'MedicalFeePaid';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece si el honorario medico en el caso de que aplique ya fue causado ', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'MedicalFeePaid';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'MedicalFeePaid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje NUMERIC(5,2) cobrado sobre el valor de la causación (puede variar según contrato o políticas).', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'PercentageCashed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el porcentaje cobrado', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'PercentageCashed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'PercentageCashed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor NUMERIC(18) causado originalmente; inmutable, referencia para auditar cambios posteriores en TotalAmountPayable.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'TotalAmountPayableReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor causado real, es decir el valor que se causo inicialmente ya que TotalAmountPayable puede cambiar ya que puede cambiar el porcentaje del valor cobrado', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'TotalAmountPayableReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'TotalAmountPayableReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total NUMERIC(18) a pagar = AmountPayable × InvoiceQuantity; varía si el porcentaje cobrado cambia.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'TotalAmountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total valor a pagar - Amount Payable X InvoiceQuantity', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'TotalAmountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'TotalAmountPayable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad INT de servicios facturados en la orden (multiplicador para cálculo del total causado).', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'InvoiceQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad de factura', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'InvoiceQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'InvoiceQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor NUMERIC(18) calculado según tarifa del contrato y tipo de servicio (referencia para auditar liquidación).', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'MedicalFeesContractValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor calculado segun el contrato y los tipos de tarifa', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'MedicalFeesContractValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'MedicalFeesContractValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor unitario NUMERIC(18) a pagar según liquidación y/o digitación manual (base para cálculo total).', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'AmountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor unitario a pagar segun la liquidacion y/o digitado manualmente', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'AmountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'AmountPayable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT a Billing.ServiceOrderDetailSurgical; identifica el detalle quirúrgico (cirujano, ayudantes, insumos Qx).', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailSurgicalId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el id de los agregados del Qx', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailSurgicalId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailSurgicalId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT a Billing.ServiceOrderDetail; identifica el detalle específico de la orden de servicio (procedimiento, examen, producto).', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del detalle de la orden de servicio', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT a Billing.ServiceOrder; identifica la orden de servicio que generó esta causación.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ServiceOrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la orden de servicio', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ServiceOrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ServiceOrderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT a Billing.InvoiceDetail; vincula la causación al detalle de la factura emitida (NULL si aún sin facturar).', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'InvoiceDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el id del detalle de la factura', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'InvoiceDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'InvoiceDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATETIME de causación del registro; coincide con CreationDate, inicio del reconocimiento de derecho.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'CausationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha de la causacion del registro, este campo corresponde al mismo campo de fecha de creacion', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'CausationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'CausationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT a MedicalFees.MedicalFeesContract; identifica el contrato que regula tarifa y políticas de pago.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'MedicalFeesContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del contrato', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'MedicalFeesContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'MedicalFeesContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT a Common.ThirdParty; identifica el tercero pagador (puede ser IPS, EPS, o tercero del contrato del profesional).', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tercero segun como se haya especificado en el contrato. Puede ser el tercero del contrato o el del medico que ejecuto la orden.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(20) del profesional de la salud que realizó el servicio/administró el producto (origen: tabla Crystal INPROFSAL); para Qx, código del cirujano principal.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'HealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Codigo del Profesional de la Salud que Realiza el Servicio y/o Administra el Producto.   Estos datos se sacan de la tabla de Crystal INPROFSAL  Para Qx Se especifica el codigo del profesional cirujano', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'HealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'HealthProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código VARCHAR(25) MASKED del paciente, cédula/documento de identidad (origen: Crystal INPACIENT); PII ofuscado.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del paciente, esto se saca de tabla de INPACIENT de Crystal', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número CHAR(10) del ingreso/atención del paciente (origen: Crystal ADINGRESO); vincula causación a episodio clínico.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero del ingreso del paciente, esto se saca de la tabla ADINGRESO de Crystal', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador INT PRIMARY KEY IDENTITY; clave única de cada registro de causación de honorarios.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el id de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT a MedicalFees.CausationRecognition; ID del reconocimiento contable asociado; NULL indica aún no reconocido contablemente.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'CausationRecognitionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del reconocimiento de causación asociado. NULL indica que la causación aún no ha sido reconocida contablemente.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'CausationRecognitionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'CausationRecognitionId';


GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Causación de honorarios médicos por atención. Registra los valores a pagar a profesionales de salud por servicios prestados en una orden, vinculando la admisión del paciente, el contrato de honorarios, la facturación y el estado de pago, reversión u objeción de cada causación.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation';
