CREATE TABLE [Contract].[ContractPackageService] (
    [Id]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ContractPackageId]     INT             NOT NULL,
    [CUPSEntityId]          INT             NOT NULL,
    [ContractDescriptionId] INT             NULL,
    [Quantity]              INT             NOT NULL,
    [ApplyCondition]        BIT             NOT NULL,
    [CreationUser]          VARCHAR (20)    NOT NULL,
    [CreationDate]          DATETIME        NOT NULL,
    [ModificationUser]      VARCHAR (20)    NULL,
    [ModificationDate]      DATETIME        NULL,
    [UnitValue]             NUMERIC (20, 2) CONSTRAINT [DF__ContractP__UnitV__6ACC1DDE] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ContractPackageService] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContractPackageService_ContractDescriptions] FOREIGN KEY ([ContractDescriptionId]) REFERENCES [Contract].[ContractDescriptions] ([Id]),
    CONSTRAINT [FK_ContractPackageService_ContractPackage] FOREIGN KEY ([ContractPackageId]) REFERENCES [Contract].[ContractPackage] ([Id]),
    CONSTRAINT [FK_ContractPackageService_CUPSEntity] FOREIGN KEY ([CUPSEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id])
);


GO
ALTER TABLE [Contract].[ContractPackageService] NOCHECK CONSTRAINT [FK_ContractPackageService_ContractDescriptions];


GO
ALTER TABLE [Contract].[ContractPackageService] NOCHECK CONSTRAINT [FK_ContractPackageService_ContractPackage];


GO
ALTER TABLE [Contract].[ContractPackageService] NOCHECK CONSTRAINT [FK_ContractPackageService_CUPSEntity];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor unitario asignado', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractPackageService', @level2type = N'COLUMN', @level2name = N'UnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractPackageService', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractPackageService', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractPackageService', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractPackageService', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo booleano donde 0 - No aplica condición, 1 - Aplica condición.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractPackageService', @level2type = N'COLUMN', @level2name = N'ApplyCondition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece la cantidad asignada al CUPS.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractPackageService', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la descripción de contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractPackageService', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la entidad del CUPS.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractPackageService', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del paquete de contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractPackageService', @level2type = N'COLUMN', @level2name = N'ContractPackageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractPackageService', @level2type = N'COLUMN', @level2name = N'Id';

