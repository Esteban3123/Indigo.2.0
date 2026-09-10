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



GO





GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_Group__Code__State]
    ON [Payroll].[Group]([Code] ASC, [State] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Group__PayrollParameterId]
    ON [Payroll].[Group]([PayrollParameterId] ASC);


GO
ALTER INDEX [IX_Group__PayrollParameterId]
    ON [Payroll].[Group] DISABLE;




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de SMMLV (salarios mínimos) exonerados en aportes SENA, ICBF y salud (INT, PII sensible)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'SMMLVAmountExemption';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad de SMMLV Exonerado SENA, ICBF y Salud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'SMMLVAmountExemption';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'SMMLVAmountExemption';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) para control de versiones concurrentes, registro automático de cambios', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación (DATETIME, auditoría)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación (VARCHAR 20, auditoría)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del grupo (DATETIME, auditoría)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro del grupo (VARCHAR 20, auditoría)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del grupo: 1=Activo (en vigencia), 0=Inactivo (archivado/deshabilitado)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Grupo 1- Activo 0- Inactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de contrato: 1=Otros, 2=Aprendizaje, 3=Laboral Fijo, 4=Laboral Indefinido, 5=Aprendiz SENA', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'ContractClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clases de contratos 1 - Otros 2 - Aprendizaje 3 - Laboral Fijo 4 - Laboral Indefinido 5-Aprendiz Sena', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'ContractClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'ContractClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de manejo de provisiones laborales: 1=Sí gestiona provisiones, 0=No gestiona', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'HandlingProvisions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Maneja provisiones 1- Si 0-No', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'HandlingProvisions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'HandlingProvisions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de mes: 1=30 días, 2=Días calendario (afecta cálculo de jornadas)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Meses de  1 - 30Dias   2- Dias Calendario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha programada de la próxima liquidación de nómina (DATETIME)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'NextDateLiquidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fceha Proxima Liquidacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'NextDateLiquidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'NextDateLiquidation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la última liquidación de nómina realizada para este grupo (DATETIME)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'LastDateLiquidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de ultima liquidacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'LastDateLiquidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'LastDateLiquidation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia de liquidación: 1=Mensual, 2=Quincenal', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'Liquidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquidacion 1- Mensual 2- Quincenal', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'Liquidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'Liquidation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo del grupo de nómina, identificación interna de la estructura de pago', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del conjunto de parámetros de nómina asociados (FK a Payroll.PayrollParameter)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'PayrollParameterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del parámetro de nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'PayrollParameterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'PayrollParameterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la empresa propietaria del grupo (FK a Payroll.Company)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'CompanyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la empresa', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'CompanyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'CompanyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico único del grupo de nómina (VARCHAR 20, clave única)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (IDENTITY) del grupo de nómina en el sistema de payroll', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla de Grupos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupos de nómina de la empresa: agrupa empleados bajo un mismo esquema de liquidación, periodicidad de pago y manejo de provisiones laborales. Permite configurar parámetros como tipo de contrato, fechas de liquidación y días de vacaciones adicionales por grupo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días adicionales de vacaciones reconocidos al grupo de empleados, más allá de los días legales obligatorios.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'AdditionalVacationDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'AdditionalVacationDays';
