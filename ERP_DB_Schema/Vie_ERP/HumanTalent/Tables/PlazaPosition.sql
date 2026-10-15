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
ALTER TABLE [HumanTalent].[PlazaPosition] NOCHECK CONSTRAINT [fk_PlazaPositions];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad inicial que se registra, no se modifica ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'OccupiedAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado: 1-Vacante, 2-ocupado, 3-Proceso, 4-Cancelado, 5-Suspendido', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Lugar de trabajo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'WorkplaceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Apoyo a la aprovación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'SupportAprobation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de aprobación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'AprobationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la Plaza autorizada', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'AuthorizesPlazaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Creación de la Plaza de la Razón', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'ReasonPlazaCreationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Beneficios legales adicionales', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'Extralegalbenefits';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'id del Tipo de Plaza', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'TypePlazaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de Horas de trabajo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'WorkignHourId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Intensidad horaria', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'HourlyIntensity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Tipo de salario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'TypeSalaryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración del contrato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'ContractDuration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tipo de contrato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'typeContracId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presupuesto', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'Budget';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Importe total', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'TotalAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Salario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'Salary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la posición', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'PositionsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PlazaPosition', @level2type = N'COLUMN', @level2name = N'Id';

