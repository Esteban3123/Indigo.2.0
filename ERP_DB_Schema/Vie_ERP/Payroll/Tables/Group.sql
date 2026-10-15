CREATE TABLE [Payroll].[Group] (
    [Id]                     INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                   VARCHAR (20)  NOT NULL,
    [CompanyId]              INT           NOT NULL,
    [PayrollParameterId]     INT           NOT NULL,
    [Name]                   VARCHAR (150) NOT NULL,
    [Liquidation]            TINYINT       NOT NULL,
    [LastDateLiquidation]    DATETIME      NOT NULL,
    [NextDateLiquidation]    DATETIME      NOT NULL,
    [Month]                  CHAR (1)      NOT NULL,
    [HandlingProvisions]     BIT           NOT NULL,
    [ContractClass]          TINYINT       NOT NULL,
    [State]                  BIT           NOT NULL,
    [CreationUser]           VARCHAR (20)  NULL,
    [CreationDate]           DATETIME      NULL,
    [ModificationUser]       VARCHAR (20)  NULL,
    [ModificationDate]       DATETIME      NULL,
    [TimeStamp]              ROWVERSION    NOT NULL,
    [AdditionalVacationDays] TINYINT       CONSTRAINT [DF_Group_AdditionalVacationDays] DEFAULT ((0)) NOT NULL,
    [SMMLVAmountExemption]   INT           NULL,
    CONSTRAINT [PK_Group__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Group_Company] FOREIGN KEY ([CompanyId]) REFERENCES [Payroll].[Company] ([Id]),
    CONSTRAINT [FK_Group_PayrollParameter] FOREIGN KEY ([PayrollParameterId]) REFERENCES [Payroll].[PayrollParameter] ([Id]),
    CONSTRAINT [IX_Group] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
ALTER TABLE [Payroll].[Group] NOCHECK CONSTRAINT [FK_Group_Company];


GO
ALTER TABLE [Payroll].[Group] NOCHECK CONSTRAINT [FK_Group_PayrollParameter];


GO
CREATE NONCLUSTERED INDEX [IX_Group__Code__State]
    ON [Payroll].[Group]([Code] ASC, [State] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Group__PayrollParameterId]
    ON [Payroll].[Group]([PayrollParameterId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de SMMLV Exonerado SENA, ICBF y Salud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'SMMLVAmountExemption';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Grupo 1- Activo 0- Inactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clases de contratos 1 - Otros 2 - Aprendizaje 3 - Laboral Fijo 4 - Laboral Indefinido 5-Aprendiz Sena', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'ContractClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Maneja provisiones 1- Si 0-No', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'HandlingProvisions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Meses de  1 - 30Dias   2- Dias Calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fceha Proxima Liquidacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'NextDateLiquidation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de ultima liquidacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'LastDateLiquidation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Liquidacion 1- Mensual 2- Quincenal', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'Liquidation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del parámetro de nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'PayrollParameterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la empresa', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'CompanyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla de Grupos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'Id';

