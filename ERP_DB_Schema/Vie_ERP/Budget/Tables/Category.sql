CREATE TABLE [Budget].[Category] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BudgetaryValidityId] INT           NOT NULL,
    [ItemType]            TINYINT       NOT NULL,
    [Code]                VARCHAR (40)  NULL,
    [AlternativeCode]     VARCHAR (40)  NULL,
    [Name]                VARCHAR (300) NOT NULL,
    [CategoryOwnerId]     INT           NULL,
    [Auxiliary]           BIT           NOT NULL,
    [FinancialSourceId]   INT           NULL,
    [PAC]                 BIT           NULL,
    [StatusPAC]           TINYINT       CONSTRAINT [DF_Category_StatusPAC] DEFAULT ((1)) NULL,
    [Used]                BIT           NOT NULL,
    [FutureValidity]      BIT           NULL,
    [InvertionProject]    BIT           NULL,
    [BalanceDeficit]      BIT           NULL,
    [IncomeCxP]           BIT           NULL,
    [ReservePacStatus]    TINYINT       NULL,
    [StatusPacCxP]        TINYINT       NULL,
    [Status]              BIT           NOT NULL,
    [CreationUser]        VARCHAR (20)  CONSTRAINT [DF_Category_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]        DATETIME      CONSTRAINT [DF_Category_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]    VARCHAR (20)  NULL,
    [ModificationDate]    DATETIME      NULL,
    [TimeStamp]           ROWVERSION    NOT NULL,
    [CCPETCodeId]         INT           NULL,
    [CPCCodeId]           INT           NULL,
    [FundSituation]       CHAR (1)      CONSTRAINT [DF_Category_FundSituation] DEFAULT ('C') NOT NULL,
    [Validity]            TINYINT       CONSTRAINT [DF_Category_Validity] DEFAULT ((1)) NOT NULL,
    [PublicPolicyId]      INT           NULL,
    CONSTRAINT [PK_Category__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Budget_CCPETCodeId] FOREIGN KEY ([CCPETCodeId]) REFERENCES [Budget].[CCPET] ([Id]),
    CONSTRAINT [FK_Budget_CPCCodeId] FOREIGN KEY ([CPCCodeId]) REFERENCES [Budget].[CPCCatalog] ([Id]),
    CONSTRAINT [FK_BudgetItem_FinancialSource] FOREIGN KEY ([FinancialSourceId]) REFERENCES [Budget].[FinancialSource] ([Id]),
    CONSTRAINT [FK_BudgetItem_Validity] FOREIGN KEY ([BudgetaryValidityId]) REFERENCES [Budget].[BudgetaryValidity] ([Id]),
    CONSTRAINT [FK_Category_Category] FOREIGN KEY ([CategoryOwnerId]) REFERENCES [Budget].[Category] ([Id]),
    CONSTRAINT [FK_Category_PublicPolicy] FOREIGN KEY ([PublicPolicyId]) REFERENCES [Budget].[PublicPolicy] ([Id])
);


GO
ALTER TABLE [Budget].[Category] NOCHECK CONSTRAINT [FK_Budget_CCPETCodeId];


GO
ALTER TABLE [Budget].[Category] NOCHECK CONSTRAINT [FK_Budget_CPCCodeId];


GO
ALTER TABLE [Budget].[Category] NOCHECK CONSTRAINT [FK_BudgetItem_FinancialSource];


GO
ALTER TABLE [Budget].[Category] NOCHECK CONSTRAINT [FK_BudgetItem_Validity];


GO
ALTER TABLE [Budget].[Category] NOCHECK CONSTRAINT [FK_Category_Category];


GO
ALTER TABLE [Budget].[Category] NOCHECK CONSTRAINT [FK_Category_PublicPolicy];


GO
CREATE NONCLUSTERED INDEX [IX_Category__FinancialSourceId]
    ON [Budget].[Category]([FinancialSourceId] ASC);


GO
CREATE NONCLUSTERED INDEX [iCategoryOwnerId_Budget_Category_2D9229AD]
    ON [Budget].[Category]([CategoryOwnerId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Category_Unique]
    ON [Budget].[Category]([BudgetaryValidityId] ASC, [ItemType] ASC, [Code] ASC, [FinancialSourceId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Category__BudgetaryValidityId]
    ON [Budget].[Category]([BudgetaryValidityId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla  Budget.PublicPolicy', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'PublicPolicyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vigencia:   1 - Vigencia Actual   2 - Reservas  3 - Cuentas por pagar  4 - Vigencias futuras - Vigencia Actual  5 - Vigencias futuras - Reservas  6 - Vigencias futuras - Cuentas por pagar', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'Validity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Situación de Fondos:  C - Con situación de Fondos  S - Sin Situación de Fondos ', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'FundSituation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla CatalogoCPC', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'CPCCodeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla CCPET', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'CCPETCodeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro, (activo  = true, inactivo = false)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'OBTIENE Ó ESTABLECE EL ESTADO DEL PAC DE CUENTAS POR PAGAR PARA EL RUBRO (REGISTRADO = 1,CONFIRMADO = 2,ANULADO = 3)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'StatusPacCxP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'OBTIENE Ó ESTABLECE EL ESTADO DEL PAC DE RESERVAS PARA EL RUBRO (REGISTRADO = 1,CONFIRMADO = 2,ANULADO = 3)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'ReservePacStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'DETERMINA SI EL RUBRO INGRESO ES DE CXP', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'IncomeCxP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'DETERMINA SI EL RUBRO PERTENECE AL REPORTE DE DEFICIT O EQUILIBRIO PTAL', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'BalanceDeficit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Para saber si maneja proyectos de inversion', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'InvertionProject';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Para saber si el rubro controla vigencias futuras', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'FutureValidity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Para saber si el rubro ha sido usado en algun proceso', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'Used';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obtiene o establece el estado del PAC para el Rubro  1 - Registrado  2 - Confirmado  3 - Anulado', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'StatusPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Para saber si maneja control de PAC', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'PAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la fuente de financiación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'FinancialSourceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Para saber si el rubro es auxiliar', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'Auxiliary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Rubro padre', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'CategoryOwnerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código alternativo', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'AlternativeCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'OBTIENE Ó ESTABLECE EL TIPO DE RUBRO (INGRESO = 1,GASTO = 2)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'ItemType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Vigencia del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'BudgetaryValidityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'Id';

