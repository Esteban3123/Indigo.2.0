CREATE TABLE [HumanTalent].[PlazaPosition] (
    [Id]                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                  VARCHAR (20)  NOT NULL,
    [PositionsId]           INT           NOT NULL,
    [Salary]                NUMERIC (18)  NOT NULL,
    [TotalAmount]           INT           NOT NULL,
    [Budget]                TINYINT       NOT NULL,
    [typeContracId]         INT           NOT NULL,
    [ContractDuration]      VARCHAR (20)  NOT NULL,
    [TypeSalaryId]          INT           NOT NULL,
    [HourlyIntensity]       VARCHAR (20)  NOT NULL,
    [WorkignHourId]         INT           NOT NULL,
    [TypePlazaId]           INT           NOT NULL,
    [Extralegalbenefits]    VARCHAR (50)  NULL,
    [ReasonPlazaCreationId] INT           NOT NULL,
    [AuthorizesPlazaId]     INT           NULL,
    [AprobationDate]        DATETIME      NULL,
    [SupportAprobation]     VARCHAR (300) NULL,
    [WorkplaceId]           INT           NOT NULL,
    [Observations]          VARCHAR (50)  NULL,
    [State]                 INT           NULL,
    [CreationUser]          VARCHAR (20)  NOT NULL,
    [CreationDate]          DATETIME      NOT NULL,
    [ModificationUser]      VARCHAR (20)  NULL,
    [ModificationDate]      DATETIME      NULL,
    [OccupiedAmount]        INT           CONSTRAINT [DF__PlazaPosi__Occup__1832F5DB] DEFAULT ((0)) NULL,
    CONSTRAINT [PK__PlazaPos__3214EC07256C9487] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_PlazaPositions] FOREIGN KEY ([PositionsId]) REFERENCES [HumanTalent].[Positions] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de plazas ocupadas registrada sin modificación posterior (INT, auditoriable para seguimiento de ocupación).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'OccupiedAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad inicial que se registra, no se modifica ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'OccupiedAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'OccupiedAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del último cambio realizado a la plaza (DATETIME, auditoría de cambios).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación de la plaza (VARCHAR 20, trazabilidad de cambios).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de plaza (DATETIME, auditoría de creación).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro de plaza (VARCHAR 20, trazabilidad de creación).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la plaza: 1-Vacante, 2-Ocupado, 3-En Proceso, 4-Cancelado, 5-Suspendido (INT, estado del puesto).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado: 1-Vacante, 2-ocupado, 3-Proceso, 4-Cancelado, 5-Suspendido', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas y observaciones adicionales sobre la plaza o puesto de trabajo (VARCHAR 50).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del lugar de trabajo, centro de atención o unidad funcional donde se ubica la plaza (INT, FK).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'WorkplaceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Lugar de trabajo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'WorkplaceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'WorkplaceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Documento, justificación o soporte técnico que respalda la aprobación de la plaza (VARCHAR 300).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'SupportAprobation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Apoyo a la aprovación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'SupportAprobation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'SupportAprobation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de aprobación oficial de la plaza creada (DATETIME, hito de autorización).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'AprobationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de aprobación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'AprobationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'AprobationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la plaza autorizada o persona que autoriza (INT, referencia de autorización).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'AuthorizesPlazaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Plaza autorizada', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'AuthorizesPlazaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'AuthorizesPlazaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo o razón de la creación de la plaza (INT, clasificación de causa).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'ReasonPlazaCreationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Creación de la Plaza de la Razón', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'ReasonPlazaCreationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'ReasonPlazaCreationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Beneficios adicionales, extralegales o complementarios asociados a la plaza (VARCHAR 50).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'Extralegalbenefits';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Beneficios legales adicionales', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'Extralegalbenefits';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'Extralegalbenefits';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo o categoría de plaza (planta, temporal, contratista, etc.) (INT, FK).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'TypePlazaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del Tipo de Plaza', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'TypePlazaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'TypePlazaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del esquema de horario laboral asignado a la plaza (INT, FK).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'WorkignHourId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de Horas de trabajo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'WorkignHourId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'WorkignHourId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Intensidad u horas de dedicación semanal/mensual de la plaza (VARCHAR 20, ej: tiempo completo, medio tiempo).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'HourlyIntensity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Intensidad horaria', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'HourlyIntensity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'HourlyIntensity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de estructura salarial o escala de la plaza (INT, FK).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'TypeSalaryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Tipo de salario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'TypeSalaryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'TypeSalaryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración del contrato asociado a la plaza (VARCHAR 20, ej: indefinido, 1 año, temporal).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'ContractDuration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Duración del contrato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'ContractDuration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'ContractDuration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de contrato laboral (indefinido, fijo, temporal, contratista, etc.) (INT, FK).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'typeContracId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de contrato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'typeContracId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'typeContracId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presupuesto asignado o disponible para la plaza (TINYINT, indicador de partida presupuestal).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'Budget';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Presupuesto', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'Budget';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'Budget';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Importe total o cantidad máxima presupuestada para la plaza (INT, monto en pesos).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'TotalAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Importe total', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'TotalAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'TotalAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del salario mensual o remuneración base de la plaza (NUMERIC 18, en moneda local).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'Salary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Salario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'Salary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'Salary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del cargo o posición asociada a la plaza (INT, FK a [Positions]).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'PositionsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la posición', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'PositionsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'PositionsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de identificación de la plaza (VARCHAR 20, ej: PLZ-001-RH, equivalente laboral).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable de la plaza (INT IDENTITY, clave primaria).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de plazas o cargos de talento humano: guarda la información de cada plaza laboral autorizada en la organización, incluyendo el salario, tipo de contrato, jornada, presupuesto, cantidad de cargos aprobados y ocupados, lugar de trabajo y estado de aprobación de cada plaza.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition';
