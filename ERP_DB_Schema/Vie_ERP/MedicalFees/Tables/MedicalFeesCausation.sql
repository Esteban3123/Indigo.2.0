CREATE TABLE [MedicalFees].[MedicalFeesCausation] (
    [Id]                           INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AdmissionNumber]              CHAR (10)                                                                        NOT NULL,
    [PatientCode]                  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [HealthProfessionalCode]       CHAR (20)                                                                        NOT NULL,
    [ThirdPartyId]                 INT                                                                              NOT NULL,
    [MedicalFeesContractId]        INT                                                                              NULL,
    [CausationDate]                DATETIME                                                                         CONSTRAINT [DF_MedicalFeesCausation_CausationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [InvoiceDetailId]              INT                                                                              NULL,
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
    [CausationRecognitionId]       INT                                                                              NULL,
    [TimeStamp]                    ROWVERSION                                                                       NOT NULL,
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
ALTER TABLE [MedicalFees].[MedicalFeesCausation] NOCHECK CONSTRAINT [FK_MedicalFeesCausation_CausationRecognition];


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
CREATE NONCLUSTERED INDEX [IX_MedicalFeesCausation__InvoiceDetailId__ServiceOrderDetailId__INC__AmountPayable__CausationDate__ConfirmationDate__Confirmatio]
    ON [MedicalFees].[MedicalFeesCausation]([InvoiceDetailId] ASC, [ServiceOrderDetailId] ASC)
    INCLUDE([Id], [MedicalFeesContractId], [CausationDate], [AmountPayable], [TotalAmountPayable], [TotalAmountPayableReal], [PercentageCashed], [Status], [CreationUser], [CreationDate], [ModificationUser], [ModificationDate], [ConfirmationUser], [ConfirmationDate]);


GO
CREATE NONCLUSTERED INDEX [IX_MedicalFeesCausation__InvoiceDetailId]
    ON [MedicalFees].[MedicalFeesCausation]([InvoiceDetailId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_MedicalFeesCausation__CausationRecognitionId]
    ON [MedicalFees].[MedicalFeesCausation]([CausationRecognitionId] ASC)
    WHERE [CausationRecognitionId] IS NOT NULL;


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de liquidación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'LiquidationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de liquidación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'LiquidationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Establece si el registro ya fue descontado en una nueva liquidacion por objecion de glosa aceptada.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ReassessmentForObjection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Establece si el registro ya fue reliquidado e incluido en una nueva liquidacion, por anulacion de factura', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ReassessmentForReversal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor aceptado por la IPS que se descontara al profesional de la salud', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ValueObjectionAccepted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el item causado fue aceptado por la IPS en el proceso de glosas, bien sea en la evaluacion de primera o segunda vez, o en conciliacion.    Una vez se establezca el valor aceptado por la IPS el sistema verificará el parametro de Descuento Automatico por Aceptacion de Glosas del contrato relacionado al profesional responsable de la realizacion del procedimiento, y marcara este campo en verdadero para que se agregue como descuento en la siguiente liquidacion.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ObjectionAccepted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica si la factura donde se encontraba el servicio causado fue anulada, esta bandera solo se activa si al momento de la anulacion de la factura el estado del registro es Liquidado o Confirmado', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'InvoiceReversal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el estado de la causacion  1 - Causado  2 - Liquidado  3 - Confirmado  4 - Anulado    Causado: Es el primer estado, es cuando se realiza la causacion del servicio    Liquidado: Se establece cuando el registro de causacion es incluida en una  liquidacion sin confirmar    Confirmado: Se establece cuando se genera la cuenta por pagar    Anulado: Se establece en varios eventos, primero cuando el registro se encuentra en estado causado y se realiza la anulacion de una factura, segundo cuando el registro se encuentra en estado liquidado y la columna InvoiceReversal se encuentra en estado Verdadero y la accion del usuario corresponde a Recalcular', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Establece si el honorario medico en el caso de que aplique ya fue causado ', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'MedicalFeePaid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el porcentaje cobrado', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'PercentageCashed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor causado real, es decir el valor que se causo inicialmente ya que TotalAmountPayable puede cambiar ya que puede cambiar el porcentaje del valor cobrado', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'TotalAmountPayableReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total valor a pagar - Amount Payable X InvoiceQuantity', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'TotalAmountPayable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad de factura', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'InvoiceQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor calculado segun el contrato y los tipos de tarifa', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'MedicalFeesContractValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor unitario a pagar segun la liquidacion y/o digitado manualmente', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'AmountPayable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Establece el id de los agregados del Qx', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailSurgicalId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id del detalle de la orden de servicio', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la orden de servicio', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ServiceOrderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Establece el id del detalle de la factura', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'InvoiceDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la fecha de la causacion del registro, este campo corresponde al mismo campo de fecha de creacion', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'CausationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id del contrato', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'MedicalFeesContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tercero segun como se haya especificado en el contrato. Puede ser el tercero del contrato o el del medico que ejecuto la orden.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Codigo del Profesional de la Salud que Realiza el Servicio y/o Administra el Producto.   Estos datos se sacan de la tabla de Crystal INPROFSAL  Para Qx Se especifica el codigo del profesional cirujano', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'HealthProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del paciente, esto se saca de tabla de INPACIENT de Crystal', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero del ingreso del paciente, esto se saca de la tabla ADINGRESO de Crystal', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Establece el id de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del reconocimiento de causación asociado. NULL indica que la causación aún no ha sido reconocida contablemente.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesCausation', @level2type = N'COLUMN', @level2name = N'CausationRecognitionId';


GO

