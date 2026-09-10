CREATE TABLE [Budget].[RevenueType] (
    [Id]                        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BudgetaryValidityId]       INT           NOT NULL,
    [Code]                      VARCHAR (20)  NOT NULL,
    [Name]                      VARCHAR (100) NULL,
    [Type]                      TINYINT       NULL,
    [IncomeSource]              TINYINT       NULL,
    [ExpenditureDefinition]     TINYINT       NULL,
    [ExpenditureClassification] TINYINT       NULL,
    [Status]                    BIT           CONSTRAINT [DF_EarningsType_Status] DEFAULT ((1)) NOT NULL,
    [CreationUser]              VARCHAR (20)  CONSTRAINT [DF_RevenueType_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]              DATETIME      CONSTRAINT [DF_RevenueType_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]          VARCHAR (20)  NULL,
    [ModificationDate]          DATETIME      NULL,
    [TimeStamp]                 ROWVERSION    NOT NULL,
    CONSTRAINT [PK_RevenueType__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RevenueType_BudgetaryValidity] FOREIGN KEY ([BudgetaryValidityId]) REFERENCES [Budget].[BudgetaryValidity] ([Id])
);




GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_RevenueType__Code__BudgetaryValidityId__Type]
    ON [Budget].[RevenueType]([Code] ASC, [BudgetaryValidityId] ASC, [Type] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sello de tiempo (TIMESTAMP) - Marca automática del instante de creación, registro o última modificación del tipo de ingreso/gasto. Auditoria de cambios', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sello de tiempo. Guarda el instante tiempo de la creación, registro o modificación de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Modificación (DATETIME nullable) - Fecha y hora del último cambio registrado en el tipo de ingreso/gasto', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de Modificación (VARCHAR 20, nullable) - Identificador del usuario que realizó la última actualización del registro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Creación (DATETIME) - Fecha y hora de creación inicial del tipo de ingreso/gasto. Valor por defecto: getdate()', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de Creación (VARCHAR 20) - Identificador del usuario que creó el registro de tipo de ingreso/gasto. Valor por defecto: 999', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del Registro (BIT) - True=Activo, False=Inactivo. Indica si el tipo de ingreso/gasto está habilitado en la vigencia presupuestal', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro: True-Activo, False-Inactivo', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de Gastos (TINYINT) - Categoría del gasto: 1=Funcionamiento, 2=Inversión, 3=Servicios de Deuda, 4=Gastos Personal, 5=Generales, 6=Mantenimiento Hospitalario, 7=Transferencias, 8=Operación Comercial, 9=Disponibilidad Final', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'ExpenditureClassification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificacion de Gastos   1 - Funcionamiento  2 - Inversion  3 - Servicios de la deuda  4 - Gastos del personal  5- Gastos Generales  6 - Mantenimiento Hospitalario  7 - Transferencias  8 - Gastos de operacion comercial y de prestacion de servicios  9 - Disponibilidad Final', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'ExpenditureClassification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'ExpenditureClassification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Definición de Gastos (TINYINT) - Origen presupuestal: 1=Aportes Nacionales, 2=Ingresos Propios. Clasificación para asignación de recursos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'ExpenditureDefinition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Definicion de Gastos 1 - Aportes Nacionales 2 - Ingresos Propios', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'ExpenditureDefinition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'ExpenditureDefinition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen del Tipo de Ingreso (TINYINT) - Fuente presupuestal: 1=Aportes Nacionales, 2=Ingresos Propios, 3=Disponibilidad Inicial, 4=Venta de Servicios, 5=SGP, 6=Otros, 7=Capital, 8=Total Ingresos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'IncomeSource';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Origen del tipo de ingreso:   1 - Aportes Nacionales  2 - Ingresos propios  3 - Disponibilidad inicial  4 - Venta de servicios  5 - Sistema general de participaciones  6 - Otros ingresos  7 - Ingresos capital  8 - Total ingresos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'IncomeSource';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'IncomeSource';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Registro (TINYINT) - Clasificación: 1=Ingreso (Ingresos), 2=Gasto (Egresos). Define si es movimiento de ingresos o gastos presupuestales', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de registro 1 - Ingreso 2 - Gasto', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del Tipo de Ingreso/Gasto (VARCHAR 100, nullable) - Denominación descriptiva del tipo de ingreso o gasto presupuestal', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del tipo de ingreso', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Tipo de Ingreso/Gasto (VARCHAR 20) - Código único alfanumérico para identificar y clasificar el tipo de ingreso o gasto en la contabilidad presupuestal', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del tipo de ganacia', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de Vigencia Presupuestal (INT, FK) - Referencia a [Budget].[BudgetaryValidity]. Vigencia fiscal a la que pertenece este tipo de ingreso/gasto. Ejemplo: 2024, 2025', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'BudgetaryValidityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Vigencia del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'BudgetaryValidityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'BudgetaryValidityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del Registro (INT, PK, IDENTITY) - Identificador único secuencial del tipo de ingreso o gasto en la tabla RevenueType', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipos de ingresos o rentas presupuestarias asociadas a una vigencia presupuestal. Permite clasificar y codificar las fuentes de ingreso, su definición de gasto y clasificación de egreso para la gestión presupuestal.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RevenueType';
