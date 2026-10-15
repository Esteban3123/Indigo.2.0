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
    CONSTRAINT [PK_MedicalFeesCausation__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MedicalFeesCausation_InvoiceDetail] FOREIGN KEY ([InvoiceDetailId]) REFERENCES [Billing].[InvoiceDetail] ([Id]),
    CONSTRAINT [FK_MedicalFeesCausation_MedicalFeesContract] FOREIGN KEY ([MedicalFeesContractId]) REFERENCES [MedicalFees].[MedicalFeesContract] ([Id]),
    CONSTRAINT [FK_MedicalFeesCausation_ServiceOrder] FOREIGN KEY ([ServiceOrderId]) REFERENCES [Billing].[ServiceOrder] ([Id]),
    CONSTRAINT [FK_MedicalFeesCausation_ServiceOrderDetail] FOREIGN KEY ([ServiceOrderDetailId]) REFERENCES [Billing].[ServiceOrderDetail] ([Id]),
    CONSTRAINT [FK_MedicalFeesCausation_ServiceOrderDetailSurgical] FOREIGN KEY ([ServiceOrderDetailSurgicalId]) REFERENCES [Billing].[ServiceOrderDetailSurgical] ([Id]),
    CONSTRAINT [FK_MedicalFeesCausation_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [MedicalFees].[MedicalFeesCausation] NOCHECK CONSTRAINT [FK_MedicalFeesCausation_InvoiceDetail];


GO
ALTER TABLE [MedicalFees].[MedicalFeesCausation] NOCHECK CONSTRAINT [FK_MedicalFeesCausation_MedicalFeesContract];


GO
ALTER TABLE [MedicalFees].[MedicalFeesCausation] NOCHECK CONSTRAINT [FK_MedicalFeesCausation_ServiceOrder];


GO
ALTER TABLE [MedicalFees].[MedicalFeesCausation] NOCHECK CONSTRAINT [FK_MedicalFeesCausation_ServiceOrderDetail];


GO
ALTER TABLE [MedicalFees].[MedicalFeesCausation] NOCHECK CONSTRAINT [FK_MedicalFeesCausation_ServiceOrderDetailSurgical];


GO
ALTER TABLE [MedicalFees].[MedicalFeesCausation] NOCHECK CONSTRAINT [FK_MedicalFeesCausation_ThirdParty];


GO
CREATE NONCLUSTERED INDEX [IX_MedicalFeesCausation__ServiceOrderDetailSurgicalId__ServiceOrderDetailId__MedicalFeesContractId__InvoiceDetailId__INC__Amount]
    ON [MedicalFees].[MedicalFeesCausation]([ServiceOrderDetailSurgicalId] ASC, [ServiceOrderDetailId] ASC, [MedicalFeesContractId] ASC, [InvoiceDetailId] ASC)
    INCLUDE([Id], [AmountPayable], [TotalAmountPayable], [TotalAmountPayableReal], [PercentageCashed], [Status], [CreationUser], [CreationDate], [ModificationUser], [ModificationDate], [ConfirmationUser], [ConfirmationDate]);


GO
CREATE NONCLUSTERED INDEX [IX_MedicalFeesCausation__InvoiceDetailId__ServiceOrderDetailId__INC__AmountPayable__CausationDate__ConfirmationDate__Confirmatio]
    ON [MedicalFees].[MedicalFeesCausation]([InvoiceDetailId] ASC, [ServiceOrderDetailId] ASC)
    INCLUDE([Id], [MedicalFeesContractId], [CausationDate], [AmountPayable], [TotalAmountPayable], [TotalAmountPayableReal], [PercentageCashed], [Status], [CreationUser], [CreationDate], [ModificationUser], [ModificationDate], [ConfirmationUser], [ConfirmationDate]);


GO
CREATE NONCLUSTERED INDEX [IX_MedicalFeesCausation__InvoiceDetailId]
    ON [MedicalFees].[MedicalFeesCausation]([InvoiceDetailId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el item causado fue aceptado por la IPS en el proceso de glosas, bien sea en la evaluacion de primera o segunda vez, o en conciliacion.    Una vez se establezca el valor aceptado por la IPS el sistema verificará el parametro de Descuento Automatico por Aceptacion de Glosas del contrato relacionado al profesional responsable de la realizacion del procedimiento, y marcara este campo en verdadero para que se agregue como descuento en la siguiente liquidacion.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ObjectionAccepted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el estado de la causacion  1 - Causado  2 - Liquidado  3 - Confirmado  4 - Anulado    Causado: Es el primer estado, es cuando se realiza la causacion del servicio    Liquidado: Se establece cuando el registro de causacion es incluida en una  liquidacion sin confirmar    Confirmado: Se establece cuando se genera la cuenta por pagar    Anulado: Se establece en varios eventos, primero cuando el registro se encuentra en estado causado y se realiza la anulacion de una factura, segundo cuando el registro se encuentra en estado liquidado y la columna InvoiceReversal se encuentra en estado Verdadero y la accion del usuario corresponde a Recalcular', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Codigo del Profesional de la Salud que Realiza el Servicio y/o Administra el Producto.   Estos datos se sacan de la tabla de Crystal INPROFSAL  Para Qx Se especifica el codigo del profesional cirujano', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'HealthProfessionalCode';

