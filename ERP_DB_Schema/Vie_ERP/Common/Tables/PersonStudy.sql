CREATE TABLE [Common].[PersonStudy] (
    [Id]                             INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PersonId]                       INT            NOT NULL,
    [StudyTypeId]                    INT            NOT NULL,
    [IsFormal]                       BIT            NOT NULL,
    [Name]                           VARCHAR (100)  NOT NULL,
    [StudyCenterId]                  INT            NOT NULL,
    [ProfessionalCardNumber]         VARCHAR (20)   NULL,
    [ProfessionalCardExpeditionDate] DATE           NULL,
    [ProfessionalCardOnProcess]      BIT            NULL,
    [StartDate]                      DATE           NOT NULL,
    [EndingDate]                     DATE           NULL,
    [GraduationDate]                 DATE           NULL,
    [Status]                         TINYINT        NULL,
    [IsInternal]                     BIT            NULL,
    [AverageGrade]                   DECIMAL (2, 1) NULL,
    [StudyValue]                     INT            NULL,
    [StudyValueCompanyPercentage]    INT            NULL,
    [Length]                         INT            NOT NULL,
    [TimeUnitId]                     INT            NOT NULL,
    [CityId]                         INT            NOT NULL,
    [State]                          BIT            NOT NULL,
    CONSTRAINT [PK_EmployeeStudy] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EmployeeStudy_Person] FOREIGN KEY ([PersonId]) REFERENCES [Common].[Person] ([Id]),
    CONSTRAINT [FK_EmployeeStudy_StudyCenter] FOREIGN KEY ([StudyCenterId]) REFERENCES [Payroll].[StudyCenter] ([Id]),
    CONSTRAINT [FK_EmployeeStudy_StudyType] FOREIGN KEY ([StudyTypeId]) REFERENCES [Payroll].[StudyType] ([Id]),
    CONSTRAINT [FK_PersonStudy_City] FOREIGN KEY ([CityId]) REFERENCES [Common].[City] ([Id]),
    CONSTRAINT [FK_PersonStudy_TimeUnit] FOREIGN KEY ([TimeUnitId]) REFERENCES [Common].[TimeUnit] ([Id])
);


GO
ALTER TABLE [Common].[PersonStudy] NOCHECK CONSTRAINT [FK_EmployeeStudy_Person];


GO
ALTER TABLE [Common].[PersonStudy] NOCHECK CONSTRAINT [FK_EmployeeStudy_StudyCenter];


GO
ALTER TABLE [Common].[PersonStudy] NOCHECK CONSTRAINT [FK_EmployeeStudy_StudyType];


GO
ALTER TABLE [Common].[PersonStudy] NOCHECK CONSTRAINT [FK_PersonStudy_City];


GO
ALTER TABLE [Common].[PersonStudy] NOCHECK CONSTRAINT [FK_PersonStudy_TimeUnit];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro, 0 inactivo, 1 activo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ciudad donde se realiza el estudio', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'CityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de tiempo (FK)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'TimeUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Duracion del estudio', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'Length';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje del valor del estudio asumido por la empresa', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'StudyValueCompanyPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del estudio', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'StudyValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Promedio de notas', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'AverageGrade';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estudio proveeido por la empresa 1 - Si 0 - No', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'IsInternal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del estudio   1 - Estudia actualmente   2 - Estudio Interrumpido  3 - Estudio Terminado  4 - Graduado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Graduacion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'GraduationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de terminacion de estudio', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'EndingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de inicio de estudio', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'StartDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tarjeta profesional en tramite 1 - Si 0 - No', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'ProfessionalCardOnProcess';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de expedicion de la tarjeta profesional', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'ProfessionalCardExpeditionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de la tarjeta profesional', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'ProfessionalCardNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro docente (FK)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'StudyCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del titulo o estudio', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de educacion 1 - Formal 0 - No formal', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'IsFormal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de estudio FK', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'StudyTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Persona FK', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'PersonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del estudio del empleado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'Id';

