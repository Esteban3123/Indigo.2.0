CREATE TABLE [Payroll].[FunctionalUnit] (
    [Id]                        INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                      VARCHAR (20) NOT NULL,
    [Name]                      VARCHAR (50) NOT NULL,
    [BranchOfficeId]            INT          NOT NULL,
    [CostCenterId]              INT          NOT NULL,
    [ProductionCenterId]        INT          NULL,
    [AccountingStructureId]     INT          NULL,
    [UnitType]                  TINYINT      CONSTRAINT [DF_FunctionalUnit_UnitType] DEFAULT ((1)) NOT NULL,
    [State]                     BIT          NOT NULL,
    [CreationUser]              VARCHAR (20) NOT NULL,
    [CreationDate]              DATETIME     NOT NULL,
    [ModificationUser]          VARCHAR (20) NULL,
    [ModificationDate]          DATETIME     NULL,
    [TimeStamp]                 ROWVERSION   NOT NULL,
    [Turn]                      INT          NULL,
    [DeliveryTimePharmaService] DATETIME     NULL,
    CONSTRAINT [PK_FunctionalUnit_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FunctionalUnit_AccountingStructure] FOREIGN KEY ([AccountingStructureId]) REFERENCES [Payroll].[AccountingStructure] ([Id]),
    CONSTRAINT [FK_FunctionalUnit_BranchOffice] FOREIGN KEY ([BranchOfficeId]) REFERENCES [Payroll].[BranchOffice] ([Id]),
    CONSTRAINT [FK_FunctionalUnit_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id])
);


GO
ALTER TABLE [Payroll].[FunctionalUnit] NOCHECK CONSTRAINT [FK_FunctionalUnit_AccountingStructure];


GO
ALTER TABLE [Payroll].[FunctionalUnit] NOCHECK CONSTRAINT [FK_FunctionalUnit_BranchOffice];


GO
ALTER TABLE [Payroll].[FunctionalUnit] NOCHECK CONSTRAINT [FK_FunctionalUnit_CostCenter];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora de Entrega por Servicio Farmacéutico', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnit', @level2type = N'COLUMN', @level2name = N'DeliveryTimePharmaService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'turno', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnit', @level2type = N'COLUMN', @level2name = N'Turn';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnit', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnit', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnit', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnit', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnit', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnit', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Unidad Funcional:  1: Urgencias  2: Hospitalizacion  3: Apoyo Dx  4: Apoyo Terapeutico  5: Unidades de Cuidado Intensivo Adulto  6: Unidades de Cuidado Intermedio Adulto  7: Unidades de Cuidado Intensivo Pediatrica  8: Unidades de Cuidado Intermedio Pediatrica  9: Unidades de Cuidado Intensivo Neonatal  10: Unidades de Cuidado Intermedio Neonatal  11: Unidades de Cuidado Basico Neonatal  12: Unidad Renal  13 Unidad Oncologica  14: Unidad Medicina Nuclear  15: Consulta Externa  16: Unidad Mental  17: Unidad de Quemados  18: Unidad de Cuidado Paliativo  19: Cirugia  20: Laboratorio  21: Cardiologia No Invasiva  22: Cardiologia Invasiva  23: Gineco-Obstetricia  24: Consulta Externa - Gineco-Obstetricia  25: Otras', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnit', @level2type = N'COLUMN', @level2name = N'UnitType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Estructura Contable', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnit', @level2type = N'COLUMN', @level2name = N'AccountingStructureId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el centro de produccion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnit', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnit', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FK Id de la sucursal a la que pertenece', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnit', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnit', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnit', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla de unidades funcionales', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnit', @level2type = N'COLUMN', @level2name = N'Id';

