CREATE TABLE [Inventory].[MedicalFormula] (
    [Id]                                     INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Number]                                 VARCHAR (20)                                                                     NOT NULL,
    [PatientCode]                            VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [PatientFirstName]                       VARCHAR (20) MASKED WITH (FUNCTION = 'partial(0, "FirstName_Ofuscado", 0)')      NOT NULL,
    [PatientSecondName]                      VARCHAR (20) MASKED WITH (FUNCTION = 'partial(0, "SecondName_Ofuscado", 0)')     NULL,
    [PatientFirstLastName]                   VARCHAR (20) MASKED WITH (FUNCTION = 'partial(0, "FirstLastName_Ofuscado", 0)')  NOT NULL,
    [PatientSecondLastName]                  VARCHAR (20) MASKED WITH (FUNCTION = 'partial(0, "SecondLastName_Ofuscado", 0)') NULL,
    [PatientName]                            VARCHAR (100) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)')          NOT NULL,
    [AdmissionNumber]                        VARCHAR (20)                                                                     NULL,
    [CareCenterCode]                         VARCHAR (20)                                                                     NOT NULL,
    [FunctionalUnidCode]                     VARCHAR (20)                                                                     NOT NULL,
    [FunctionalUnitName]                     VARCHAR (50)                                                                     NULL,
    [Date]                                   DATETIME                                                                         NOT NULL,
    [CreationUser]                           VARCHAR (20)                                                                     NOT NULL,
    [CreationDate]                           DATETIME                                                                         NOT NULL,
    [ModificationUser]                       VARCHAR (20)                                                                     NULL,
    [ModificationDate]                       DATETIME                                                                         NULL,
    [IsManual]                               BIT                                                                              CONSTRAINT [DF_MedicalFormula_IsManual] DEFAULT ((0)) NOT NULL,
    [IPSCode]                                VARCHAR (200)                                                                    NULL,
    [WarehouseId]                            INT                                                                              NULL,
    [CareGroupId]                            INT                                                                              NULL,
    [BillingAuthorizationId]                 INT                                                                              NULL,
    [DateFormulation]                        DATETIME                                                                         NULL,
    [PerformsHealthProfessionalThirdPartyId] INT                                                                              NULL,
    CONSTRAINT [PK_MedicalFormula__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MedicalFormula_PerformsHealthProfessional] FOREIGN KEY ([PerformsHealthProfessionalThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_MedicalFormula_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[MedicalFormula] NOCHECK CONSTRAINT [FK_MedicalFormula_PerformsHealthProfessional];


GO
ALTER TABLE [Inventory].[MedicalFormula] NOCHECK CONSTRAINT [FK_MedicalFormula_Warehouse];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_MedicalFormula__Number]
    ON [Inventory].[MedicalFormula]([Number] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_MedicalFormula_IsManual_Number_PatientCode_PatientName_CareCenterCode_FunctionalUnitName_Date]
    ON [Inventory].[MedicalFormula]([IsManual] ASC)
    INCLUDE([Number], [PatientCode], [PatientName], [CareCenterCode], [FunctionalUnitName], [Date], [CreationUser], [CreationDate], [IPSCode]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la Autorización de factura', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormula', @level2type = N'COLUMN', @level2name = N'BillingAuthorizationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Grupo de atención', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormula', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del almacén', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormula', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código IPS que se llena cuando es manual', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormula', @level2type = N'COLUMN', @level2name = N'IPSCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite saber si la formula medica viene de dispensación automatica o manual', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormula', @level2type = N'COLUMN', @level2name = N'IsManual';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormula', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormula', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormula', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormula', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormula', @level2type = N'COLUMN', @level2name = N'Date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormula', @level2type = N'COLUMN', @level2name = N'FunctionalUnitName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de unidad funcional', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormula', @level2type = N'COLUMN', @level2name = N'FunctionalUnidCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Centro de atención', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormula', @level2type = N'COLUMN', @level2name = N'CareCenterCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del ingreso del paciente', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormula', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'nombre de paciente', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormula', @level2type = N'COLUMN', @level2name = N'PatientName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Segundo apellido del paciente', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormula', @level2type = N'COLUMN', @level2name = N'PatientSecondLastName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Primer apellido del paciente', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormula', @level2type = N'COLUMN', @level2name = N'PatientFirstLastName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Segundo nombre del paciente', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormula', @level2type = N'COLUMN', @level2name = N'PatientSecondName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Primer nombre del paciente', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormula', @level2type = N'COLUMN', @level2name = N'PatientFirstName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del paciente', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormula', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero que identifica el diferido', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormula', @level2type = N'COLUMN', @level2name = N'Number';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormula', @level2type = N'COLUMN', @level2name = N'Id';

