CREATE TABLE [Inventory].[MedicalFormulaDetail] (
    [Id]                     INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MedicalFormulaId]       INT                                                                              NOT NULL,
    [AdmissionNumber]        VARCHAR (20)                                                                     NULL,
    [EntityId]               INT                                                                              NULL,
    [EntityCode]             VARCHAR (20)                                                                     NULL,
    [ContractCode]           VARCHAR (20)                                                                     NULL,
    [PlanCode]               VARCHAR (20)                                                                     NULL,
    [ProductCode]            VARCHAR (20)                                                                     NOT NULL,
    [ProductName]            VARCHAR (100)                                                                    NOT NULL,
    [RequestQuantity]        INT                                                                              NOT NULL,
    [DeliveryQuantity]       INT                                                                              NOT NULL,
    [PendingQuantity]        INT                                                                              NOT NULL,
    [NoPos]                  BIT                                                                              NOT NULL,
    [MeasurementUnitCode]    VARCHAR (20)                                                                     NULL,
    [HealthProfessionalCode] VARCHAR (20)                                                                     NULL,
    [HealthProfessionalName] VARCHAR (50)                                                                     NULL,
    [HealthProfessionalNit]  VARCHAR (50)                                                                     NULL,
    [IdeTipHis]              VARCHAR (10)                                                                     NULL,
    [NumFolio]               VARCHAR (20)                                                                     NULL,
    [PatientCode]            VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [SpecialtyCode]          VARCHAR (20)                                                                     NULL,
    [SpecialtyName]          VARCHAR (50)                                                                     NULL,
    [IsDeferred]             BIT                                                                              CONSTRAINT [DF_MedicalFormulaDetail_IsDeferred] DEFAULT ((0)) NOT NULL,
    [ProductType]            TINYINT                                                                          CONSTRAINT [DF_MedicalFormulaDetail_ProductType] DEFAULT ((1)) NOT NULL,
    [DiagnosticCode]         VARCHAR (4)                                                                      NULL,
    [TreatmentDays]          INT                                                                              DEFAULT ((0)) NOT NULL,
    [IDMipres]               VARCHAR (22)                                                                     NULL,
    [AuthorizationNumber]    VARCHAR (20)                                                                     NULL,
    CONSTRAINT [PK_MedicalFormulaDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MedicalFormulaDetail_MedicalFormula] FOREIGN KEY ([MedicalFormulaId]) REFERENCES [Inventory].[MedicalFormula] ([Id])
);


GO
ALTER TABLE [Inventory].[MedicalFormulaDetail] NOCHECK CONSTRAINT [FK_MedicalFormulaDetail_MedicalFormula];


GO
CREATE NONCLUSTERED INDEX [nci_wi_MedicalFormulaDetail_5B425D5142D003908D804679A58A21F2]
    ON [Inventory].[MedicalFormulaDetail]([MedicalFormulaId] ASC)
    INCLUDE([AdmissionNumber], [ContractCode], [DeliveryQuantity], [EntityCode], [EntityId], [HealthProfessionalCode], [HealthProfessionalName], [HealthProfessionalNit], [IdeTipHis], [IsDeferred], [MeasurementUnitCode], [NoPos], [NumFolio], [PatientCode], [PendingQuantity], [PlanCode], [ProductCode], [ProductName], [RequestQuantity], [SpecialtyCode], [SpecialtyName]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Producto 1 : Medicamento, 2 : Insumo   El valor del campo ProductCode, Cambia dependiendo del tipo (1) Codigo de Medicamento, (2) Codigo del Insumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetail', @level2type = N'COLUMN', @level2name = N'ProductType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el producto fue diferido', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetail', @level2type = N'COLUMN', @level2name = N'IsDeferred';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la especialidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetail', @level2type = N'COLUMN', @level2name = N'SpecialtyName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialista', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetail', @level2type = N'COLUMN', @level2name = N'SpecialtyCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetail', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del folio', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetail', @level2type = N'COLUMN', @level2name = N'NumFolio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre Interno del Tipo de Historia - lo trae de la tabla dbo.HCFARMEPD', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetail', @level2type = N'COLUMN', @level2name = N'IdeTipHis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nit del profesional de la salud', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetail', @level2type = N'COLUMN', @level2name = N'HealthProfessionalNit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del profesional de la salud', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetail', @level2type = N'COLUMN', @level2name = N'HealthProfessionalName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetail', @level2type = N'COLUMN', @level2name = N'HealthProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la unidad de medida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetail', @level2type = N'COLUMN', @level2name = N'MeasurementUnitCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifica si el producto - medicamento, es No POS | 1 = Si | 0 = No', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetail', @level2type = N'COLUMN', @level2name = N'NoPos';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad pendiente', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetail', @level2type = N'COLUMN', @level2name = N'PendingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad entregada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetail', @level2type = N'COLUMN', @level2name = N'DeliveryQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad requerida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetail', @level2type = N'COLUMN', @level2name = N'RequestQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetail', @level2type = N'COLUMN', @level2name = N'ProductName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetail', @level2type = N'COLUMN', @level2name = N'ProductCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del plan', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetail', @level2type = N'COLUMN', @level2name = N'PlanCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetail', @level2type = N'COLUMN', @level2name = N'ContractCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de entidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetail', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la entidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetail', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de admisión', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetail', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la Fórmula médica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetail', @level2type = N'COLUMN', @level2name = N'MedicalFormulaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetail', @level2type = N'COLUMN', @level2name = N'Id';

