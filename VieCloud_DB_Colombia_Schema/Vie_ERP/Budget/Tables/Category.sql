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



GO



GO



GO



GO



GO



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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la política pública asociada al rubro presupuestal. Referencia FK a Budget.PublicPolicy. Vincula el rubro a políticas públicas de salud o financieras.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'PublicPolicyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla  Budget.PublicPolicy', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'PublicPolicyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'PublicPolicyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vigencia presupuestal: 1=Vigencia Actual, 2=Reservas, 3=Cuentas por Pagar, 4=Vigencias Futuras-Vigencia Actual, 5=Vigencias Futuras-Reservas, 6=Vigencias Futuras-Cuentas por Pagar. TINYINT, determina el período fiscal y tipo de disponibilidad del rubro.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'Validity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vigencia:   1 - Vigencia Actual   2 - Reservas  3 - Cuentas por pagar  4 - Vigencias futuras - Vigencia Actual  5 - Vigencias futuras - Reservas  6 - Vigencias futuras - Cuentas por pagar', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'Validity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'Validity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Situación de fondos: C=Con situación de fondos disponible, S=Sin situación de fondos. CHAR(1), indicador de disponibilidad financiera para el rubro en el presupuesto.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'FundSituation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Situación de Fondos:  C - Con situación de Fondos  S - Sin Situación de Fondos ', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'FundSituation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'FundSituation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del código CPC (Clasificación de Procedimientos Clínicos). Referencia FK a Budget.CPCCatalog. Vincula el rubro a catálogos de procedimientos clínicos.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'CPCCodeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla CatalogoCPC', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'CPCCodeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'CPCCodeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del código CCPET (Clasificación de Gastos). Referencia FK a Budget.CCPET. Vincula el rubro a la clasificación estructural de gastos presupuestales.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'CCPETCodeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla CCPET', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'CCPETCodeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'CCPETCodeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal de auditoria (TIMESTAMP). Registra automáticamente el instante exacto de creación, modificación o cambio de estado del rubro en la base de datos.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del rubro (DATETIME). Permite rastrear cuándo se actualizó el registro presupuestal.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario que realizó la última modificación (VARCHAR 20). Trazabilidad de cambios en el rubro.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación inicial del rubro presupuestal (DATETIME, default=getdate()). Punto de origen del registro.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario que creó el rubro (VARCHAR 20, default=999). Trazabilidad del origen del registro presupuestal.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del rubro: true=activo/vigente, false=inactivo/bloqueado (BIT). Controla si el rubro se aplica en procesos de presupuesto y facturación.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro, (activo  = true, inactivo = false)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del PAC (Plan Anual de Compras) para cuentas por pagar del rubro (TINYINT): 1=Registrado, 2=Confirmado, 3=Anulado. Indica el ciclo administrativo de compras.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'StatusPacCxP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'OBTIENE Ó ESTABLECE EL ESTADO DEL PAC DE CUENTAS POR PAGAR PARA EL RUBRO (REGISTRADO = 1,CONFIRMADO = 2,ANULADO = 3)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'StatusPacCxP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'StatusPacCxP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del PAC para reservas presupuestales del rubro (TINYINT): 1=Registrado, 2=Confirmado, 3=Anulado. Controla la reserva de fondos.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'ReservePacStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'OBTIENE Ó ESTABLECE EL ESTADO DEL PAC DE RESERVAS PARA EL RUBRO (REGISTRADO = 1,CONFIRMADO = 2,ANULADO = 3)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'ReservePacStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'ReservePacStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que indica si el rubro de ingreso está asociado a cuentas por pagar (CxP). Determina tratamiento contable de ingresos diferidos.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'IncomeCxP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'DETERMINA SI EL RUBRO INGRESO ES DE CXP', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'IncomeCxP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'IncomeCxP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que indica si el rubro forma parte del reporte de déficit o equilibrio presupuestal. Incluye el rubro en análisis financiero crítico.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'BalanceDeficit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'DETERMINA SI EL RUBRO PERTENECE AL REPORTE DE DEFICIT O EQUILIBRIO PTAL', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'BalanceDeficit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'BalanceDeficit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que indica si el rubro gestiona o controla proyectos de inversión. Vincula a capital y proyectos de infraestructura.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'InvertionProject';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Para saber si maneja proyectos de inversion', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'InvertionProject';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'InvertionProject';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que indica si el rubro controla vigencias futuras. Permite proyecciones y reservas multianuales.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'FutureValidity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Para saber si el rubro controla vigencias futuras', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'FutureValidity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'FutureValidity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que indica si el rubro ha sido utilizado en algún proceso presupuestal o contable. Determina si puede ser eliminado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'Used';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Para saber si el rubro ha sido usado en algun proceso', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'Used';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'Used';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del Plan Anual de Compras (PAC) para el rubro (TINYINT, default=1): 1=Registrado, 2=Confirmado, 3=Anulado. Refleja aprobación de gastos.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'StatusPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Obtiene o establece el estado del PAC para el Rubro  1 - Registrado  2 - Confirmado  3 - Anulado', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'StatusPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'StatusPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que indica si el rubro aplica control de PAC (Plan Anual de Compras). Somete el gasto a revisión y aprobación previa.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'PAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Para saber si maneja control de PAC', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'PAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'PAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la fuente de financiación del rubro (INT, FK a Budget.FinancialSource). Especifica origen del presupuesto (SGP, recursos propios, etc).', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'FinancialSourceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la fuente de financiación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'FinancialSourceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'FinancialSourceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que indica si el rubro es auxiliar o de soporte. Rubros auxiliares no son de línea directa pero facilitan clasificación contable.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'Auxiliary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Para saber si el rubro es auxiliar', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'Auxiliary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'Auxiliary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del rubro padre o superior (INT, FK recursiva a Budget.Category). Establece jerarquía y estructura de rubros presupuestales.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'CategoryOwnerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Rubro padre', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'CategoryOwnerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'CategoryOwnerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción completa del rubro presupuestal (VARCHAR 300). Etiqueta legible para búsqueda y reportes presupuestales.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alternativo del rubro (VARCHAR 40). Equivalente o sinonimia con otros códigos internos o normativos.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'AlternativeCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código alternativo', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'AlternativeCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'AlternativeCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código principal del rubro presupuestal (VARCHAR 40). Identificador alfanumérico único para clasificación de ingresos o gastos en ERP.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de rubro presupuestal (TINYINT): 1=Ingreso, 2=Gasto. Clasifica el movimiento como entrada o salida de recursos financieros.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'ItemType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'OBTIENE Ó ESTABLECE EL TIPO DE RUBRO (INGRESO = 1,GASTO = 2)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'ItemType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'ItemType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la vigencia presupuestal del rubro (INT, FK a Budget.BudgetaryValidity). Vincula el rubro al año o período fiscal.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'BudgetaryValidityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Vigencia del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'BudgetaryValidityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'BudgetaryValidityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del rubro presupuestal (INT IDENTITY, PK). Clave primaria para referencia de categorías y rubros en presupuesto.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Categorías presupuestales de una vigencia fiscal: rubros, ítems de ingreso o gasto, proyectos de inversión y fuentes de financiación que conforman el presupuesto institucional. Permite organizar el plan presupuestal con códigos, auxiliares, PAC y situación de fondos.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category';
