CREATE TABLE [Contract].[RateManual] (
    [Id]                           INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                         VARCHAR (20)   NOT NULL,
    [Name]                         VARCHAR (100)  NOT NULL,
    [Type]                         TINYINT        NOT NULL,
    [RoundService]                 INT            NOT NULL,
    [ContractMinimumWageId]        INT            NULL,
    [PercentageNoBloodyRoom]       NUMERIC (5, 2) NULL,
    [MaterialNoBloodyIPSServiceId] INT            NULL,
    [Status]                       BIT            NOT NULL,
    [CreationUser]                 VARCHAR (20)   NOT NULL,
    [CreationDate]                 DATETIME       NOT NULL,
    [ModificationUser]             VARCHAR (20)   NULL,
    [ModificationDate]             DATETIME       NULL,
    [TimeStamp]                    ROWVERSION     NOT NULL,
    [LiquidateAllMIVIE]            BIT            NULL,
    CONSTRAINT [PK_RateManual__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RateManual_ContractMinimumWage] FOREIGN KEY ([ContractMinimumWageId]) REFERENCES [Contract].[ContractMinimumWage] ([Id]),
    CONSTRAINT [FK_RateManual_IPSService] FOREIGN KEY ([MaterialNoBloodyIPSServiceId]) REFERENCES [Contract].[IPSService] ([Id])
);




GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_RateManual__Code]
    ON [Contract].[RateManual]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT NULL) de liquidación total de cirugías MIVIE (Mínimamente Invasivo Videoasistido): 1=Sí (liquidar todas), 0=No; parámetro de facturación en procedimientos quirúrgicos minimamente invasivos.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'LiquidateAllMIVIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si se desea liquidar todas las cirugías MIVIE, 1 - Si, 0 - No.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'LiquidateAllMIVIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'LiquidateAllMIVIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal interna (TIMESTAMP) de SQL Server para control de versiones optimista; refleja el estado de creación, registro o modificación del registro.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME NULL) de última modificación; registro del instante en que se actualizó el manual tarifario.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario editor (VARCHAR 20 NULL) que realizó la última modificación del manual; auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro; marca temporal de ingreso inicial del manual tarifario.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario creador (VARCHAR 20) que registró el manual tarifario en el sistema; auditoría de origen.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de vigencia (BIT): 1=Activo/Vigente, 0=Inactivo/Suspendido; controla si el manual tarifario está disponible para nuevos contratos y liquidaciones.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del manual tarifario  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia (FK INT NULL) al servicio IPS de materiales [Contract].[IPSService] cobrable en procedimientos no cruentos; parámetro exclusivo de manuales SOAT.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'MaterialNoBloodyIPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del servicio IPS de tipo material que se va cobrar cuando el procedimiento sea no cruento, este dato solo se pide el manual es SOAT', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'MaterialNoBloodyIPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'MaterialNoBloodyIPSServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje (NUMERIC 5,2 NULL) de tarifa de sala para procedimientos no cruentos; aplica solo en manuales SOAT para facturaciónde servicios sin incisión.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'PercentageNoBloodyRoom';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el porcentaje de la sala que se va cobrar cuando el item sea No Cruento, este dato solo se pide el manual es SOAT', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'PercentageNoBloodyRoom';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'PercentageNoBloodyRoom';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia (FK INT NULL) al salario mínimo base del contrato [Contract].[ContractMinimumWage]; aplica solo en manuales SOAT para cálculo de honorarios.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'ContractMinimumWageId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del salario minimo del cual se basa para sacar el valor. Aplica solo a SOAT', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'ContractMinimumWageId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'ContractMinimumWageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Criterio de redondeo (INT) para valores de contrato: 1=Peso, 10=Décima, 100=Centésima, 1000=Milésima; usa fórmula Round(valor/redondeo)*redondeo.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'RoundService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica como se va redondear los valores del contrato  1 -- Peso  10 -- Decima  100 -- Centecima  1000 -- Milesima    La formula que se usa es Round(valor / redondeo) * redondeo  Round(2005/ 10) * 10', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'RoundService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'RoundService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de manual tarifario (TINYINT): 1=ISS 2001, 2=ISS 2004, 3=SOAT; define el esquema de liquidación y normativa aplicable.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo del manual tarifario  1 - ISS 2001  2 - ISS 2004  3 - SOAT', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo (VARCHAR 100) del manual tarifario; etiqueta legible para ISS 2001, ISS 2004, SOAT u otros esquemas de facturación.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del manual tarifario', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico (VARCHAR 20) del manual tarifario; identificador funcional único para búsqueda y referencia de tarifas.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del manual tarifario', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico único (INT IDENTITY) de la tarifa manual del contrato; clave primaria de la tabla RateManual.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tarifas manuales de contratación. Registra los manuales tarifarios (como ISS, SOAT, particulares u otros) usados en los contratos para valorizar servicios de salud, incluyendo reglas de redondeo, porcentajes aplicables a habitación no cruenta y referencias a salario mínimo.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual';
