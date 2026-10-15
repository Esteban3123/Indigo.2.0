CREATE TABLE [HumanTalent].[OrganizationalUnit] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BranchOfficeId]   INT          NOT NULL,
    [Code]             VARCHAR (20) NOT NULL,
    [Name]             VARCHAR (80) NOT NULL,
    [TypeOrgUnitsId]   INT          NOT NULL,
    [CostCenterId]     INT          NOT NULL,
    [headId]           INT          NOT NULL,
    [AreaId]           INT          NOT NULL,
    [State]            BIT          NOT NULL,
    [CreationUser]     VARCHAR (20) CONSTRAINT [DF_OrganizationalUnit_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]     DATETIME     CONSTRAINT [DF_OrganizationalUnit_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser] VARCHAR (20) NULL,
    [ModificationDate] DATETIME     NULL,
    [FunctionalUnitId] INT          NOT NULL,
    CONSTRAINT [PK_OrganizationalUnit] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK__Organizat__AreaI__111A4D4A] FOREIGN KEY ([AreaId]) REFERENCES [HumanTalent].[Area] ([Id]),
    CONSTRAINT [FK__Organizat__CostC__10262911] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_OrganizationalUnit_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [UQ__Organiza__A25C5AA737F4D188] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
ALTER TABLE [HumanTalent].[OrganizationalUnit] NOCHECK CONSTRAINT [FK__Organizat__AreaI__111A4D4A];


GO
ALTER TABLE [HumanTalent].[OrganizationalUnit] NOCHECK CONSTRAINT [FK__Organizat__CostC__10262911];


GO
ALTER TABLE [HumanTalent].[OrganizationalUnit] NOCHECK CONSTRAINT [FK_OrganizationalUnit_FunctionalUnit];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad funcional (payroll.FuncionalUnit), se almacena para homologar al crear el contrato desde Talento Human (Web).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'OrganizationalUnit', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'OrganizationalUnit', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'OrganizationalUnit', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'OrganizationalUnit', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'OrganizationalUnit', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la Unidad Organizativa: 1 - Activo, 0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'OrganizationalUnit', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del área (Carga las áreas creadas)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'OrganizationalUnit', @level2type = N'COLUMN', @level2name = N'AreaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Origen - Indica si la Unidad Organizativa es de tipo Origen. Sólo puede haber un registro en True.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'OrganizationalUnit', @level2type = N'COLUMN', @level2name = N'headId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Centro de Costo (Carga los Centros de Costo Creados', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'OrganizationalUnit', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de unidad Organizacional', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'OrganizationalUnit', @level2type = N'COLUMN', @level2name = N'TypeOrgUnitsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del área', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'OrganizationalUnit', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del área', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'OrganizationalUnit', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion  de  la sucursal', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'OrganizationalUnit', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del área', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'OrganizationalUnit', @level2type = N'COLUMN', @level2name = N'Id';

